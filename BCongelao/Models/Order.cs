using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BCongelao.Models
{
    [Table("Order")]
    public class Order
    {
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Campo requerido."), Display(Name = "Comercio de compra")]
        public Enum.Comercio ComercioId { get; set; }

        [DataType(DataType.Date), Display(Name = "Fecha de Orden")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Cantidad")]
        public decimal Quantity { get; set; }

        [Display(Name = "Precio sin imp")]
        public decimal TotalNoITBIS { get; set; }

        [Display(Name = "ITBIS")]
        public decimal ITBIS { get; set; }

        [Display(Name = "Total")]
        public decimal Total { get; set; }

        [Required(ErrorMessage = "Campo requerido."), Display(Name = "Estado de orden")]
        public Enum.StatusOrder StatusOrder { get; set; }

        [DataType(DataType.Date), Display(Name = "Fecha de creación")]
        public DateTime CreatedDate { get; set; }

        public string UserId { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}
