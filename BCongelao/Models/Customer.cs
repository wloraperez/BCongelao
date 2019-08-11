using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BCongelao.Models
{
    [Table("Customer")]
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Campo requerido."), Display(Name = "Nombre Cliente")]
        public string CustomerName { get; set; }

        [Display(Name = "Teléfono")]
        public string Phone { get; set; }

        [Display(Name = "Ubicación")]
        public string Location { get; set; }

        [Display(Name = "Dirección")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Campo requerido."), Display(Name = "Estado de cliente")]
        public Enum.StatusCustomer StatusCustomer { get; set; }

        [DataType(DataType.Date), Display(Name = "Fecha de creación")]
        public DateTime CreatedDate { get; set; }

        public string UserId { get; set; }

        public List<Sale> Sales { get; set; }
    }
}
