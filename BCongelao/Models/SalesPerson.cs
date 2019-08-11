using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BCongelao.Models
{
    [Table("SalesPerson")]
    public class SalesPerson
    {
        public int SalesPersonId { get; set; }

        [Required(ErrorMessage = "Campo requerido."), Display(Name = "Nombre Vendedor(a)")]
        public string SalesPersonName { get; set; }

        [Required(ErrorMessage = "Campo requerido."), Display(Name = "Teléfono")]
        public string Phone { get; set; }

        [Display(Name = "Dirección")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Campo requerido."), Display(Name = "Estado de vendedor")]
        public Enum.StatusSalesPerson StatusSalesPerson { get; set; }

        [DataType(DataType.Date), Display(Name = "Fecha de creación")]
        public DateTime CreatedDate { get; set; }

        public string UserId { get; set; }

        public List<Sale> Sales { get; set; }
    }
}
