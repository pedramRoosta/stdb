﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShirazTronic.Models;

namespace ShirazTronic.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<CompanyInfos> CompanyInfo { get; set; }
        public DbSet<Category> Category{ get; set; }
        public DbSet<SubCategory> SubCategory{ get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<ProductImage> ProductImage { get; set; }
        public DbSet<Specification> Specification { get; set; }
        public DbSet<SpecificationValue> SpecificationValue { get; set; }
        public DbSet<ProductSpecification> ProductSpecification { get; set; }
        public DbSet<AppUser> AppUser { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
    }
}
