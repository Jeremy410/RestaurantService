using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RestaurantService.Models
{
    [Table("Comidas")]
    public class Comida
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Detalle { get; set; }
        public string Portada { get; set; }
        [Required]
        public int Precio { get; set; }

        [ForeignKey("IdCategoria")]
        public Categoria Categoria { get; set; }
        [Required]
        public int IdCategoria { get; set; }
    }
}