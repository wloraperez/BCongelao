using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BCongelao.Models
{
    public partial class Enum
    {
        public enum StatusProduct
        {
            Activo,
            Inactivo
        }

        public enum ProductType
        {
            Compra = 0,
            Venta = 1
        }

        public enum CategoryProduct
        {
            [Display(Name = "Bebidas alcohólicas")]
            BebidasAlc = 0,
            [Display(Name = "Lácteos")]
            Lacteos = 1,
            [Display(Name = "Frutas")]
            Frutas = 2,
            [Display(Name = "Endulzantes")]
            Endulzantes = 3,
            [Display(Name = "Bebidas no alcohólicas")]
            BebidasNoAlc = 4,
            [Display(Name = "Comestibles")]
            Comestibles = 5,
            [Display(Name = "Helado")]
            Helado = 100,
            [Display(Name = "Sorbete")]
            Sorbete = 101,
            [Display(Name = "Otros")]
            Otros = 200
        }

        public enum UnidadMedida
        {
            [Display(Name = "Gramo")]
            g = 0,
            [Display(Name = "Mililitro")]
            ml = 1,
            [Display(Name = "Onza")]
            oz = 2,
            [Display(Name = "Libra")]
            lb = 3,
            [Display(Name = "Paquete")]
            pq = 4,
            [Display(Name = "Kilogramo")]
            kg = 5,
            [Display(Name = "Litro")]
            lt = 6
        }

        public enum Envase
        {
            [Display(Name = "3.5 Oz")]
            Envase3_5 = 10,
            [Display(Name = "8 Oz")]
            Envase8 = 20,
            [Display(Name = "9 Oz")]
            Vaso9 = 30,
            [Display(Name = "24 Oz")]
            Envase24 = 50,
            [Display(Name = "4 Kg")]
            Envase4kg = 60,
            [Display(Name = "1 Kg")]
            Envase1kg = 70
        }

        public enum Comercio
        {
            [Display(Name = "Venta Libre")]
            Venta_libre = 100,
            [Display(Name = "Supermercado Bravo")]
            SBravo = 200,
            [Display(Name = "Supermercado La sirena")]
            SLasirena = 201,
            [Display(Name = "Supermercado Olé")]
            SOle = 202,
            [Display(Name = "Super Pola")]
            SPola = 203,
            [Display(Name = "Supermercado Jumbo")]
            SJumbo = 204,
            [Display(Name = "Supermercado Nacional")]
            SNacional = 205,
            [Display(Name = "Supermercado La Cadena")]
            SLacadena = 206,
            [Display(Name = "Licormart")]
            ALicorMart = 300,
            [Display(Name = "Drinks to go")]
            ADrinkstogo = 301,
            [Display(Name = "La Licorera")]
            ALalicorera = 302,
            [Display(Name = "Ron Depot")]
            ARondepot = 303,
            [Display(Name = "Calle")]
            Calle = 400,
            [Display(Name = "Otro")]
            Otros = 500
        }

        public enum StatusOrder
        {
            Activa,
            Inactiva,
            Anulada
        }

        public enum StatusSale
        {
            Activa,
            Inactiva,
            Anulada
        }

        public enum StatusCustomer
        {
            Activo,
            Inactivo
        }

        public enum StatusSalesPerson
        {
            Activo,
            Inactivo
        }

        public enum PaymentType
        {
            [Display(Name = "Caja principal")]
            Caja = 0,
            [Display(Name = "Efectivo Wilmer")]
            Efectivo_W = 1,
            [Display(Name = "Efectivo Raquel")]
            Efectivo_R = 2,
            [Display(Name = "BHD Raquel")]
            Transf_BHD_R = 10,
            [Display(Name = "BHD Wilmer")]
            Transf_BHD_W = 11,
            [Display(Name = "Scotiabank")]
            Transf_Scotia = 12,
            [Display(Name = "Banreservas W")]
            Transf_Banreservas_W = 13,
            [Display(Name = "Gasto")]
            Gasto = 50
        }
    }
}
