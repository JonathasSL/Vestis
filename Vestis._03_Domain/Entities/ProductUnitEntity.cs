using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestis._03_Domain.Enumerators;

namespace Vestis._03_Domain.Entities;

public class ProductUnitEntity : BaseEntity<Guid>
{
    public virtual ProductEntity Product { get; private set; }
    public string Code { get; private set; }
    public ProductUnitStatusEnum Status { get; private set; }
    public decimal? RentedPrice { get; private set; }
    public DateTime? RentedDate { get; private set; }
    public DateTime? ExpectedReturnDate { get; private set; }
    //ClientEntity? CurrentClient { get; set; }

    [Obsolete("For ORM use only", true)]
    public ProductUnitEntity() { }

    public ProductUnitEntity(ProductEntity product, string code)
    {
        this.Product = product;
        this.Code = code;
        this.Status = ProductUnitStatusEnum.Available;
    }

    public void Rent(decimal? price, DateTime rentedDate, DateTime expectedReturn)
    {
        if (rentedDate >  expectedReturn)
            throw new ArgumentException("Rented date cannot be after expected return date.");


        if (rentedDate > DateTime.Now)
            this.Status = ProductUnitStatusEnum.Reserverd;
        else
            this.Status = ProductUnitStatusEnum.Rented;

        this.RentedPrice = price;
        this.RentedDate = rentedDate;
        this.ExpectedReturnDate = expectedReturn;
    }
}
