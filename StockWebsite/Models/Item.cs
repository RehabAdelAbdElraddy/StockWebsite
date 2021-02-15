namespace StockWebsite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Item")]
    public partial class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemCode { get; set; }

        [Required]
        [StringLength(255)]
        public string ItemName { get; set; }

        [Column(TypeName = "money")]
        public decimal? ItemPrice { get; set; }

        public int? ItemQuantity { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }

    }
}
