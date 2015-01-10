using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RestaurantService.Models
{
    [Table("Detalles_Pedido")]
    public class Detalle_Pedido
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("IdPedido")]
        public Pedido Pedido { get; set; }
        public int IdPedido { get; set; }

        [ForeignKey("IdComida")]
        public Comida Comida { get; set; }
        public int IdComida { get; set; }
        public int Precio { get; set; }
        public int Cantidad { get; set; }
        public int PrecioTotal { get; set; }
 
    }
}