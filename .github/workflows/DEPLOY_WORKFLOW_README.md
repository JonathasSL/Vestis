# Manual Azure Deployment Workflow

This document describes the manual deployment workflow (`deploy-to-azure-manual.yml`) for deploying the Vestis backend application to Azure App Service with database migrations.

## Overview

The workflow provides a complete deployment pipeline that:
1. **Builds** the .NET application with tests
2. **Deploys** the application to Azure App Service
3. **Runs** database migrations on the deployed application
4. **Restarts** the App Service to ensure the application is running with the latest changes

## Trigger

This workflow is triggered **manually** via the GitHub Actions UI using `workflow_dispatch`. It does not run automatically on commits or pull requests.

## Required Secrets

The following secrets must be configured in your GitHub repository settings:

### Azure Authentication Secrets
- `AZUREAPPSERVICE_CLIENTID_2ADFF4852E41459CAE2BCB454BDC6573`: Azure service principal client ID
- `AZUREAPPSERVICE_TENANTID_8B4C81BD5F31455484435C3CF38984ED`: Azure tenant ID
- `AZUREAPPSERVICE_SUBSCRIPTIONID_9B4FAF157A734E3EB1C281D35553F021`: Azure subscription ID

### Additional Required Secrets
- `AZURE_RESOURCE_GROUP`: The name of the Azure Resource Group containing the App Service (e.g., "vestis-prod-rg")

## Environment Configuration

The workflow supports three environments:
- **development**: For development/testing deployments
- **staging**: For pre-production validation
- **production**: For production deployments (default)

Environment-specific protection rules can be configured in GitHub repository settings under Settings > Environments.

## Workflow Jobs

### 1. Build Job
- **Runs on**: Windows runner
- **Purpose**: Compiles the application and runs tests
- **Steps**:
  - Checkout code
  - Setup .NET 9.x
  - Restore dependencies
  - Build in Release configuration
  - Run tests
  - Publish application
  - Upload build artifact

### 2. Deploy Job
- **Runs on**: Windows runner
- **Depends on**: Build job
- **Purpose**: Deploys the application to Azure App Service
- **Steps**:
  - Download build artifact
  - Login to Azure using service principal
  - Deploy to Azure Web App using official `azure/webapps-deploy@v3` action

### 3. Migrate Job
- **Runs on**: Ubuntu runner
- **Depends on**: Deploy job
- **Purpose**: Executes database migrations on the deployed application
- **Steps**:
  - Login to Azure
  - Run `dotnet Vestis._01_Presentation.dll migrate_database` via Kudu API
- **Note**: This job uses the Kudu REST API to execute commands directly on the App Service

### 4. Restart App Job
- **Runs on**: Ubuntu runner
- **Depends on**: Migrate job
- **Condition**: Only runs if migration succeeds
- **Purpose**: Restarts the App Service and verifies it's running
- **Steps**:
  - Login to Azure
  - Restart App Service using Azure CLI
  - Verify App Service state

## How to Use

1. Navigate to the **Actions** tab in your GitHub repository
2. Select **"Manual Deploy to Azure with Migrations"** from the workflow list
3. Click **"Run workflow"** button
4. Select the target environment (development/staging/production)
5. Click **"Run workflow"** to start the deployment

## Job Dependencies and Flow

```
Build
  ↓
Deploy (requires Build)
  ↓
Migrate (requires Deploy)
  ↓
Restart App (requires Migrate to succeed)
```

Each job will only run if the previous job succeeds, ensuring a safe deployment process.

## Monitoring

You can monitor the workflow execution in the GitHub Actions UI:
- View logs for each job
- See the status of each step
- Identify any failures or issues
- Review deployment history

## Troubleshooting

### Migration Failures
If the migration job fails:
1. Check the job logs for error messages
2. Verify database connection strings are configured in App Service configuration
3. Ensure the App Service has network access to the database
4. Check if there are any pending migration conflicts

### Deployment Failures
If the deployment job fails:
1. Verify Azure credentials are correct and not expired
2. Check that the App Service name and resource group are correct
3. Ensure the service principal has appropriate permissions
4. Review App Service deployment logs in Azure Portal

### Restart Failures
If the restart job fails:
1. Check if the App Service is accessible
2. Verify the resource group name is correct
3. Ensure the service principal has restart permissions

## Security Considerations

- All Azure credentials are stored as GitHub Secrets
- The workflow uses Azure OIDC authentication (federated credentials)
- Kudu API credentials are retrieved dynamically during workflow execution
- Environment protection rules can gate deployments to production
- Concurrency controls prevent simultaneous deployments to the same environment

## Maintenance

When updating this workflow:
1. Test changes in development environment first
2. Keep the job dependencies and conditionals intact
3. Maintain consistent naming conventions
4. Update this documentation if adding new steps or changing behavior
