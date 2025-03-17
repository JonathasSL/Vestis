using AutoMapper;
using System.Reflection;
using Vestis._02_Application.Models;
using Vestis._03_Domain.Entities;

namespace Vestis._02_Application.Mapping;

public class BaseModelToBaseEntityConverter<Tid> : ITypeConverter<BaseModel<Tid>, BaseEntity<Tid>>
    where Tid : struct
{
    public BaseEntity<Tid> Convert(BaseModel<Tid> source, BaseEntity<Tid> destination, ResolutionContext context)
    {
        destination ??= new BaseEntity<Tid>();

        SetId();
        SetCreatedDate();
        SetUpdatedDate();
        SetDeletedDate();

        return destination;

        void SetId()
        {
            var setIdMethod = destination.GetType().GetMethod("SetId", BindingFlags.Instance | BindingFlags.NonPublic);
            if (setIdMethod is not null)
            {
                if (!source.Id.Equals(null) && !source.Id.Equals(default(Tid)))
                {
                    try
                    {
                        setIdMethod.Invoke(destination, new object[] { source.Id });
                    }
                    catch (TargetInvocationException ex)
                    {
                        throw new InvalidOperationException("Failed to set entity ID.", ex);
                    }
                }
            }
        }

        void SetCreatedDate()
        {
            var setCreateMethod = destination.GetType().GetMethod("SetCreatedDate", BindingFlags.Instance | BindingFlags.NonPublic);
            if (setCreateMethod is not null)
            {
                if (!source.CreatedDate.Equals(default) && !source.CreatedDate.Equals(null))
                {
                    try
                    {
                        setCreateMethod.Invoke(destination, new object[] { source.CreatedDate });
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidOperationException("Failed to set entity CreatedDate.", ex);
                    }
                }
            }
        }

        void SetUpdatedDate()
        {
            var updatedDateProperty = destination.GetType().GetProperty("UpdatedDate", BindingFlags.Instance | BindingFlags.NonPublic);
            if (updatedDateProperty is not null)
            {
                if (!source.UpdatedDate.Equals(null) && !source.Equals(default))
                {
                    try
                    {
                        var setter = updatedDateProperty.GetSetMethod();

                        if (setter is not null)
                        {
                            setter.Invoke(destination, new object[] { source.UpdatedDate });
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidOperationException("Failed to set entity UpdatedDate.", ex);
                    }
                }
            }
        }

        void SetDeletedDate()
        {
            var updatedDateProperty = destination.GetType().GetProperty("DeletedDate", BindingFlags.Instance | BindingFlags.NonPublic);
            if (updatedDateProperty is not null)
            {
                if (!source.DeletedDate.Equals(null) && !source.DeletedDate.Equals(default))
                {
                    try
                    {
                        var setter = updatedDateProperty.GetSetMethod();

                        if (setter is not null)
                        {
                            setter.Invoke(destination, new object[] { source.DeletedDate });
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidOperationException("Failed to set entity DeletedDate.", ex);
                    }
                }
            }
        }
    }
}

