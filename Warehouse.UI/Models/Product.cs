
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
    
public partial class Product
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Product()
    {

        this.OrderDetails = new HashSet<OrderDetail>();

        this.UserLikeProducts = new HashSet<UserLikeProduct>();

        this.UserLoveProducts = new HashSet<UserLoveProduct>();

    }


    public int Id { get; set; }

    public string Name { get; set; }

    public Nullable<int> CategoryId { get; set; }

    public decimal Price { get; set; }

    public string Color { get; set; }

    public string Size { get; set; }

    public string Alias_SEO { get; set; }

    public string Image { get; set; }

    public string Slider { get; set; }

    public int Likes { get; set; }

    public int LoveTurns { get; set; }

    public System.DateTime DateCreated { get; set; }

    public bool Status { get; set; }

    public bool Display { get; set; }

    public string Content { get; set; }



    public virtual Category Category { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<OrderDetail> OrderDetails { get; set; }

    public virtual PromotionProduct PromotionProduct { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<UserLikeProduct> UserLikeProducts { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<UserLoveProduct> UserLoveProducts { get; set; }

}

}
