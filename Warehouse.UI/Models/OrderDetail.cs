
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace Warehouse.Models
{

using System;
    using System.Collections.Generic;
    
public partial class OrderDetail
{

    public int Id { get; set; }

    public int OrderId { get; set; }

    public Nullable<int> ProductId { get; set; }

    public string ProductName { get; set; }

    public string ProductImage { get; set; }

    public string ProductAlias { get; set; }

    public string MoreInfo { get; set; }

    public int Count { get; set; }

    public decimal Price { get; set; }

    public decimal Subtotal { get; set; }



    public virtual Order Order { get; set; }

    public virtual Product Product { get; set; }

}

}