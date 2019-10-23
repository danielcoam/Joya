using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Joya.Models
{
    public class Jewelery
    {

        [Key]
        public int CatalogNumber { get; set; }
        public string Name { get; set; }
        public string ImageLocation { get; set; }

        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }

        public virtual Supplier Supplier { get; set; }
        public decimal Price { get; set; }


    }
}
