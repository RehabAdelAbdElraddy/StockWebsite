using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace StockWebsite.Models
{
    public class ItemsViewModel
    {

        [Key]
        public int Id { get; set; }
        public IEnumerable<SelectListItem> Items { get; set; }
        public IEnumerable<string> SelectedItems { get; set; }
        public IEnumerable<Invoice> invoices { get; set; }

    }
}