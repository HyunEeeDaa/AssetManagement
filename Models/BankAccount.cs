﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace AssetManagement.Models;

public partial class BankAccount
{
    public int Id { get; set; }

    public string CardNumber { get; set; }

    public string CardHolder { get; set; }

    public string BankInistitution { get; set; }

    public string Type { get; set; }

    public DateTime ExpiryDate { get; set; }

    public int AccountId { get; set; }

    public virtual Account Account { get; set; }
}