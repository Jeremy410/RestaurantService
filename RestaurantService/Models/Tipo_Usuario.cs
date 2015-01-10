using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RestaurantService.Models
{
    [Table("Tipo_Usuarios")]
    public class Tipo_Usuario
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
    }
}