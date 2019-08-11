using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BCongelao.Models
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }

        [Required(ErrorMessage = "Campo requerido."), Display(Name = "ID de orden")]
        public int OrderId { get; set; }
        [ForeignKey("OrderId"), Display(Name = "ID de orden")]
        public Order Order { get; set; }

        [Required(ErrorMessage = "Campo requerido."), Display(Name = "Producto")]
        public int ProductId { get; set; }
        [ForeignKey("ProductId"), Display(Name = "Producto")]
        public Product Product { get; set; }

        [Required(ErrorMessage = "Campo requerido."), Display(Name = "Cantidad")]
        public decimal Quantity { get; set; }

        [Required(ErrorMessage = "Campo requerido."), Display(Name = "Precio Unitario")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "Campo requerido."), Display(Name = "ITBIS")]
        public decimal ITBIS { get; set; }

        [Required(ErrorMessage = "Campo requerido."), Display(Name = "Total")]
        public decimal Total { get; set; }

        public string UserId { get; set; }
    }
}
