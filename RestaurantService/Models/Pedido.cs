using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RestaurantService.Models
{
    [Table("Pedidos")]
    public class Pedido
    {
        public int Id { get; set; }    

        [ForeignKey("IdMensajero")]
        public Mensajero Mensajero { get; set; }
        public int IdMensajero { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }
        public int IdUsuario { get; set; }

        public DateTime FechaPedido { get; set; }
        public int PrecioTotal { get; set; }
        public string Estado { get; set; }
    }
}