using System;
using System.Collections.Generic;
using System.Text;
using dropShippingApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace dropShippingApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<CustomProduct> CustomProducts { get; set; }
        public DbSet<RosterProduct> RosterProducts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<PricingHistory> PricingHistories { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<QuestionMessage> QuestionMessages { get; set; }
        public DbSet<QuestionResponse> QuestionResponses { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamCreationRequest> TeamCreationRequests { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> ProductCategories { get; set; }
        public DbSet<Sort> ProductSorts { get; set; }
        public DbSet<Sort> TeamSorts { get; set; }
    }
}
