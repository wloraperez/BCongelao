using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BCongelao.Models
{
    public class ProductLog
    {
        public int ProductLogId { get; set; }

        [Required(ErrorMessage = "Campo requerido."), Display(Name = "ID de producto")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Campo requerido."), Display(Name = "Tipo de producto")]
        public Enum.ProductType ProductTypeId { get; set; }

        [Required(ErrorMessage = "Campo requerido."), Display(Name = "Nombre de producto")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Campo requerido."), Display(Name = "Comercio deseado")]
        public Enum.Comercio ComercioId { get; set; }

        [Required(ErrorMessage = "Campo requerido."), Display(Name = "Cantidad")]
        public decimal Quantity { get; set; }

        [Required(ErrorMessage = "Campo requerido."), Display(Name = "Precio Unitario")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "Campo requerido."), Display(Name = "Cantidad en unidad de medida")]
        public decimal QuantityUnit { get; set; }

        [Required(ErrorMessage = "Campo requerido."), Display(Name = "Unidad de medida")]
        public Enum.UnidadMedida Unit { get; set; }

        [Required(ErrorMessage = "Campo requerido."), Display(Name = "Unidades en stock")]
        public decimal UnitsInStock { get; set; }

        [Required(ErrorMessage = "Campo requerido."), Display(Name = "Estado de producto")]
        public Enum.StatusProduct StatusProduct { get; set; }

        [DataType(DataType.Date), Display(Name = "Fecha de creación")]
        public DateTime CreatedDate { get; set; }

        [DataType(DataType.Date), Display(Name = "Fecha de actualización")]
        public DateTime UpdatedDate { get; set; }

        public string UserId { get; set; }
    }
}
