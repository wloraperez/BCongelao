using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BCongelao.Models
{
    [Table("Sale")]
    public class Sale
    {
        public int SaleId { get; set; }

        [Display(Name = "Vendedor")]
        public int SalesPersonId { get; set; }
        [ForeignKey("SalesPersonId"), Display(Name = "Vendedor")]
        public SalesPerson SalesPerson { get; set; }

        [Display(Name = "Cliente")]
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId"), Display(Name = "Cliente")]
        public Customer Customer { get; set; }

        [Display(Name = "Descripción")]
        public string CustomerDescription { get; set; }

        [DataType(DataType.Date), Display(Name = "Fecha de Venta")]
        public DateTime SaleDate { get; set; }

        [Display(Name = "Tipo de pago")]
        public Enum.PaymentType PaymentType { get; set; }

        [Display(Name = "Cantidad")]
        public decimal Quantity { get; set; }

        [Display(Name = "Delivery")]
        public decimal Delivery { get; set; }

        [Display(Name = "Precio sin imp")]
        public decimal TotalNoITBIS { get; set; }

        [Display(Name = "ITBIS")]
        public decimal ITBIS { get; set; }

        [Display(Name = "Descuento")]
        public decimal Discount { get; set; }

        [Display(Name = "Total")]
        public decimal Total { get; set; }

        [Display(Name = "Pagado")]
        public decimal Paid { get; set; }

        [Display(Name = "Deuda")]
        public decimal Debt { get; set; }

        [Required(ErrorMessage = "Campo requerido."), Display(Name = "Estado de venta")]
        public Enum.StatusSale StatusSale { get; set; }

        [DataType(DataType.Date), Display(Name = "Fecha de creación")]
        public DateTime CreatedDate { get; set; }

        public string UserId { get; set; }

        [NotMapped, Display(Name = "Pago total?")]
        public bool PaidTotal { get; set; }

        public List<SaleDetail> SaleDetails { get; set; }
    }
}
