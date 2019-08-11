using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace BCongelao.Models
{
    [Table("Product")]
    public class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Campo requerido."), Display(Name = "Tipo")]
        public Enum.ProductType ProductTypeId { get; set; }

        [Required(ErrorMessage = "Campo requerido."), Display(Name = "Categoría")]
        public Enum.CategoryProduct Category { get; set; }

        [Required(ErrorMessage = "Campo requerido."), Display(Name = "Nombre")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Campo requerido."), Display(Name = "Contiene Alcohol")]
        public bool Alcohol { get; set; }

        [Display(Name = "Comercio deseado")]
        public Enum.Comercio ComercioId { get; set; }

        [Required(ErrorMessage = "Campo requerido."), Display(Name = "Precio Unitario")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "Campo requerido."), Display(Name = "ITBIS")]
        public decimal ITBIS { get; set; }

        [Display(Name = "Envase")]
        public Enum.Envase Envase { get; set; }

        [Display(Name = "Cant en UM")]
        public decimal QuantityUnit { get; set; }

        [Display(Name = "Unidad de medida")]
        public Enum.UnidadMedida Unit { get; set; }

        [Display(Name = "Cant en UM2")]
        public decimal QuantityUnit2 { get; set; }

        [Display(Name = "Unidad de medida 2")]
        public Enum.UnidadMedida Unit2 { get; set; }

        [Display(Name = "Stock")]
        public decimal UnitsInStock { get; set; }

        [Required(ErrorMessage = "Campo requerido."), Display(Name = "Estado")]
        public Enum.StatusProduct StatusProduct { get; set; }

        [DataType(DataType.Date), Display(Name = "Fecha de creación")]
        public DateTime CreatedDate { get; set; }

        [DataType(DataType.Date), Display(Name = "Fecha de actualización")]
        public DateTime UpdatedDate { get; set; }

        public string UserId { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
        public List<SaleDetail> SaleDetails { get; set; }
    }
}
