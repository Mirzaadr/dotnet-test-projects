using System;
using System.Collections.Generic;

namespace CustomerApp.DAL.Entities;

public class Customer
{
    public int Customerid { get; set; }
    public string Customercode { get; set; } = null!;
    public string Customername { get; set; } = null!;
    public string Customeraddress { get; set; } = null!;
    public int Createdby { get; set; }
    public DateTime Createdat { get; set; }
    public int? Modifiedby { get; set; }
    public DateTime? Modifiedat { get; set; }

    // private Customer(
    //     int id,
    //     string code,
    //     string name,
    //     string address,
    //     int createdBy,
    //     DateTime createdAt,
    //     int? modifiedBy,
    //     DateTime? modifiedAt
    // )
    // {
    //     Customerid = id;
    //     Customercode = code;
    //     Customername = name;
    //     Customeraddress = address;
    //     Createdby = createdBy;
    //     Createdat = createdAt;
    //     Modifiedby = modifiedBy;
    //     Modifiedat = modifiedAt;
    // }

    // public static Customer Create(
    //     string code,
    //     string name,
    //     string address
    // )
    // {
    //     return new(
    //         id: 0,
    //         code: code,
    //         name: name,
    //         address: address,
    //         createdBy: 0,
    //         createdAt: DateTime.UtcNow,
    //         null,
    //         null
    //     );
    // }
}
