# Estágio de Build (Compilação)
# Usa a imagem do SDK para compilar a aplicação
# Substitua 'MyApp.csproj' pelo nome real do seu arquivo .csproj
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia o arquivo de projeto e restaura dependências
# Isso permite que o Docker use o cache da camada para restauração de pacotes
COPY ["./Vestis/Vestis.01-Presentation.csproj", "./Vestis/"]
RUN dotnet restore

# Copia todo o código-fonte restante
COPY . .
WORKDIR "/src/Vestis/"

# Publica a aplicação em modo Release
RUN dotnet publish -c Release -o /app/publish

# Estágio Final (Runtime)
# Usa a imagem de runtime menor e mais segura
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Exponha a porta que o Render usará
# Por padrão, ASP.NET Core usa 8080 em contêineres a partir do .NET 8+
EXPOSE 8080 

# Copia os arquivos publicados do estágio de build
COPY --from=build /app/publish .

# Define a variável de ambiente para que o Kestrel escute a porta 8080
# Isso é importante para ambientes de contêiner como o Render.
ENV ASPNETCORE_URLS=http://+8080

# Define o ponto de entrada para iniciar a aplicação
# Substitua 'MyApp.dll' pelo nome da sua DLL de saída (geralmente o nome do projeto + .dll)
ENTRYPOINT ["dotnet", "Vestis.dll"]