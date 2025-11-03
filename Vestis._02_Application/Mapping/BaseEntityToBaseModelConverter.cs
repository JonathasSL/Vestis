using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestis._02_Application.Models;
using Vestis._03_Domain.Entities;

namespace Vestis._02_Application.Mapping;

public class BaseEntityToBaseModelConverter : ITypeConverter<BaseEntity<Guid>, BaseModel<Guid>>
{
    public BaseModel<Guid> Convert(BaseEntity<Guid> source, BaseModel<Guid> destination, ResolutionContext context)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (destination is null)
        {
            destination = new BaseModel<Guid>
            {
                Id = source.Id,
                CreatedDate = source.CreatedDate,
                UpdatedDate = source.UpdatedDate,
                DeletedDate = source.DeletedDate
            };
        }
        else
        {
            destination.Id = source.Id;
            destination.CreatedDate = source.CreatedDate;
            destination.UpdatedDate = source.UpdatedDate;
            destination.DeletedDate = source.DeletedDate;
        }
        return destination;
    }
}
