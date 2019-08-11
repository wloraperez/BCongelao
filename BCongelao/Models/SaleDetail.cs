using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BCongelao.Models
{
    [Table("SaleDetail")]
    public class SaleDetail
    {
        public int SaleDetailId { get; set; }

        [Required(ErrorMessage = "Campo requerido."), Display(Name = "ID de Venta")]
        public int SaleId { get; set; }
        [ForeignKey("SaleId"), Display(Name = "ID de venta")]
        public Sale Sale { get; set; }

        [Required(ErrorMessage = "Campo requerido."), Display(Name = "Producto")]
        public int ProductId { get; set; }
        [ForeignKey("ProductId"), Display(Name = "Producto")]
        public Product Product { get; set; }

        [Required(ErrorMessage = "Campo requerido."), Display(Name = "Cantidad")]
        public decimal Quantity { get; set; }

        [Required(ErrorMessage = "Campo requerido."), Display(Name = "Precio Unitario")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Descuento")]
        public decimal Discount { get; set; }

        [Required(ErrorMessage = "Campo requerido."), Display(Name = "ITBIS")]
        public decimal ITBIS { get; set; }

        [Required(ErrorMessage = "Campo requerido."), Display(Name = "Total")]
        public decimal Total { get; set; }

        public string UserId { get; set; }
    }
}
