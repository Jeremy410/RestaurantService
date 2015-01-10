using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RestaurantService.Models
{
    public class RestaurantServiceDb : DbContext
    {
        public DbSet<Comida> Comidas { get; set; }
        public DbSet<Tipo_Usuario> Tipo_personas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Mensajero> Mensajeros { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Detalle_Pedido> Detalles_Pedido { get; set; }
     
    }
}