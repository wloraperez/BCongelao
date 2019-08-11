using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BCongelao.Models
{
    [Table("TransferPayment")]
    public class TransferPayment
    {
        public int TransferPaymentId { get; set; }

        [Required(ErrorMessage = "Campo requerido."), Display(Name = "Tipo de pago desde")]
        public Enum.PaymentType PaymentTypeFrom { get; set; }

        [Required(ErrorMessage = "Campo requerido."), Display(Name = "Tipo de hasta")]
        public Enum.PaymentType PaymentTypeTo { get; set; }

        public string Description { get; set; }

        public decimal Total { get; set; }

        [DataType(DataType.Date), Display(Name = "Fecha de creación")]
        public DateTime CreatedDate { get; set; }

        public string UserId { get; set; }
    }
}
