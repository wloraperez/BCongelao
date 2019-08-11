using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BCongelao.Models
{
    [Table("Balance")]
    public class Balance
    {
        public int BalanceId { get; set; }

        [Required(ErrorMessage = "Campo requerido."), Display(Name = "Tipo de pago")]
        public Enum.PaymentType PaymentType { get; set; }

        public decimal Total { get; set; }

        [DataType(DataType.Date), Display(Name = "Fecha de creación")]
        public DateTime CreatedDate { get; set; }

        [DataType(DataType.Date), Display(Name = "Fecha de actualización")]
        public DateTime UpdateDate { get; set; }

        public string UserId { get; set; }
    }
}
