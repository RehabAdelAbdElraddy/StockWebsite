namespace StockWebsite.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class StockModel : DbContext
    {
        public StockModel()
            : base("name=StockDB")
        {
        }

        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Invoice_Item> Invoice_Item { get; set; }
        public virtual DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().HasMany(t => t.Invoices).WithMany(t => t.Items).Map(m =>
              {
                  m.ToTable("Invoice_Item");
                  m.MapLeftKey("ItemCode");
                  m.MapRightKey("InvoiceCode");
              });
              

            modelBuilder.Entity<Item>()
                .Property(e => e.ItemPrice)
                .HasPrecision(19, 4);
        }

        public System.Data.Entity.DbSet<StockWebsite.Models.ItemsViewModel> ItemsViewModels { get; set; }
    }
}
