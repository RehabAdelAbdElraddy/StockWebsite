namespace StockWebsite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Invoice_Item
    {
        [Key]
        [Column(Order = 0)]
     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvoiceCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ItemCode { get; set; }
    }
}
