using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BCongelao.Models
{
    public class SaleDebt
    {
        [Display(Name = "Tipo de pago")]
        public Enum.PaymentType PaymentType { get; set; }

        [Display(Name = "Deuda")]
        public decimal Debt { get; set; }
    }
}
