using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RestaurantService.Models
{
    [Table("Categorias")]
    public class Categoria
    {
        public int Id { get; set; }
        [Required(ErrorMessage="Debe completar este campo")]
        public string Nombre { get; set; }
        public string Portada { get; set; }
    }
}