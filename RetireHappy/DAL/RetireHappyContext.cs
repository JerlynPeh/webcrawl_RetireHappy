﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using RetireHappy.Models;

namespace RetireHappy.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class RetireHappyContext : DbContext
    {
        public RetireHappyContext()
            : base("name=RetireHappyContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<AvgExpenditure> AvgExpenditures { get; set; }
        public virtual DbSet<ExpenditureList> ExpenditureLists { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<SavingsInfo> SavingsInfoes { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
    }
}
