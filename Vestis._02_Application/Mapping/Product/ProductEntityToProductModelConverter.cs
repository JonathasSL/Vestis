using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestis._02_Application.Models.Product;
using Vestis._03_Domain.Entities;

namespace Vestis._02_Application.Mapping.Product;

public class ProductEntityToProductModelConverter : ITypeConverter<ProductEntity, ProductModel>
{
    public ProductModel Convert(ProductEntity source, ProductModel destination, ResolutionContext context)
    {
        destination ??= new ProductModel();
        destination.Id = source.Id;
        destination.CreatedDate = source.CreatedDate;
        destination.UpdatedDate = source.UpdatedDate;
        destination.DeletedDate = source.DeletedDate;

        //destination.StudioId = source.StudioId;
        destination.Name = source.Name;
        destination.Description = source.Description;
        destination.Category = source.Category;
        destination.Price = source.Price;
        destination.UnitCount = source.UnitCount;
        destination.ImgUrl = source.ImgUrl;

        return destination;
    }
}
