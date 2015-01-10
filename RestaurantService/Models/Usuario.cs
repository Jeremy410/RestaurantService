using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RestaurantService.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        public int Id { get; set; }

        [ForeignKey("TipoId")]
        public Tipo_Usuario TipoUsuario { get; set; }
        [Required]
        public int TipoId { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public string Telefono { get; set; }
        [Required]
        public string Entrada { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Clave { get; set; }
    }
}