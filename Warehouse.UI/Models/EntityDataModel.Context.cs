﻿

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
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


public partial class hotellte_WarehouseEntities : DbContext
{
    public hotellte_WarehouseEntities()
        : base("name=hotellte_WarehouseEntities")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<historyBankCharging> historyBankChargings { get; set; }

    public virtual DbSet<InfoShop> InfoShops { get; set; }

    public virtual DbSet<MailBox> MailBoxes { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<PromotionProduct> PromotionProducts { get; set; }

    public virtual DbSet<Province> Provinces { get; set; }

    public virtual DbSet<Slide> Slides { get; set; }

    public virtual DbSet<Subscriber> Subscribers { get; set; }

    public virtual DbSet<UserLikeProduct> UserLikeProducts { get; set; }

    public virtual DbSet<UserLoveProduct> UserLoveProducts { get; set; }

    public virtual DbSet<Ward> Wards { get; set; }

}

}

