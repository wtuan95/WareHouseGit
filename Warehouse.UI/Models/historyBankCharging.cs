
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
    
public partial class historyBankCharging
{

    public int Id { get; set; }

    public string fullname { get; set; }

    public string phone { get; set; }

    public string email { get; set; }

    public string transaction_info { get; set; }

    public string order_code { get; set; }

    public Nullable<int> price { get; set; }

    public string payment_id { get; set; }

    public string payment_type { get; set; }

    public string error_text { get; set; }

    public string secure_code { get; set; }

    public System.DateTime date_trans { get; set; }

}

}
