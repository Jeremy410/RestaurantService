using RestaurantService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RestaurantService.Controllers
{
    public class DetalleController : Controller
    {
        private RestaurantServiceDb db = new RestaurantServiceDb();
        //
        // GET: /Detalle/
        public ActionResult DetalleCategoria(int Categoria = 0)
        {
            var Comidas = from r in db.Comidas
                          where r.IdCategoria == Categoria
                          select r;

            if (Session["entrada"] != null)
            {
                foreach (var com in Comidas)
                {
                    if (ValidarProducto(com.Id) == true)
                    {
                        string Id = Convert.ToString(com.Id);
                        ViewData[Id] = true;
                    }
                }
            }

            ViewBag.Categoria = Categoria;
            return View(Comidas);
        }
        public ActionResult DetalleCompra()
        {
            int IdPedido = 0;
            int IdUser = IdUsuario();

            var IdPedidoTemp =  from r in db.Pedidos 
                                where r.Estado == "Incompleto" && r.IdUsuario == IdUser
                                select r;

            if(IdPedidoTemp.Count() > 0)
                IdPedido = IdPedidoTemp.First().Id;

            var Listado = from i in db.Detalles_Pedido

                          where i.IdPedido == IdPedido
                          select i;

            return ValidarGet(View(Listado));
        }

        public ActionResult Delete(int IdComida = 0) 
        {
            int IdPedido = 0;
            int IdUser = IdUsuario();

            var IdPedidoTemp = from r in db.Pedidos
                               where r.Estado == "Incompleto" && r.IdUsuario == IdUser
                               select r;

            if (IdPedidoTemp.Count() > 0)
                IdPedido = IdPedidoTemp.First().Id;

            var IdDetalle = from r in db.Detalles_Pedido
                            where r.IdPedido == IdPedido && r.IdComida == IdComida
                            select r;

            Detalle_Pedido Pedido = db.Detalles_Pedido.Find((int)IdDetalle.First().Id);
            db.Detalles_Pedido.Remove(Pedido);
            db.SaveChanges();

            return RedirectToAction("DetalleCompra");
        }

        public ActionResult Comprar(int Id = 0, int Precio = 0, int Categoria = 0 )
        {

            int IdUser = IdUsuario();
            //GET PEDIDO
            var pedidoModel = from r in db.Pedidos
                              where r.Estado == "Incompleto" && r.IdUsuario == IdUser
                              select r;

            //Agregando Nuevo Detalle
            if (pedidoModel.Count() == 1)
            {
                Detalle_Pedido nuevoDetalle = new Detalle_Pedido()
                {
                    IdPedido = pedidoModel.First().Id,
                    IdComida = Id,
                    Precio = Precio,
                    Cantidad = 1,
                    PrecioTotal = Precio
                };

                db.Detalles_Pedido.Add(nuevoDetalle);
                db.SaveChanges();
            }
            //Agregando NUevo Pedido y Detalle
            else
            {
                Pedido nuevoPedido = new Pedido()
                {
                    IdMensajero = 1,
                    IdUsuario = IdUser,
                    FechaPedido = DateTime.Now,
                    PrecioTotal = 0,
                    Estado = "Incompleto"
                };

                db.Pedidos.Add(nuevoPedido);
                db.SaveChanges();

                var IdPedidoTemp = from r in db.Pedidos
                                   where r.Estado == "Incompleto" && r.IdUsuario == IdUser
                                   select r;

                Detalle_Pedido nuevoDetalle = new Detalle_Pedido()
                {
                    Id = IdPedidoTemp.First().Id,
                    IdComida = Id,
                    Precio = Precio,
                    Cantidad = 1,
                    PrecioTotal = Precio
                };

                db.Detalles_Pedido.Add(nuevoDetalle);
                db.SaveChanges();
            }

            return RedirectToAction("DetalleCategoria", new { Categoria = Categoria});
        }

        public Boolean ValidarProducto(int IdComida = 0) {

            int IdUser = IdUsuario();
            int IdPedido = 0;

            //GET PEDIDO Id
            var pedidoModel = from r in db.Pedidos
                              where r.Estado == "Incompleto" && r.IdUsuario == IdUser
                              select r;
            try { 
                IdPedido = pedidoModel.First().Id; 
            }
            catch (System.InvalidOperationException) {}

            //COMPROBANDO
            var DetalleModel = from r in db.Detalles_Pedido
                               where r.IdPedido == IdPedido && r.IdComida == IdComida
                               select r;

            if(DetalleModel.Count() > 0)
            {
                return true;
            }

            return false;
        }

        public int IdUsuario() 
        {
            string EntradaUsuario = "";
            try
            {
               EntradaUsuario = Convert.ToString(Session["entrada"]);
            }catch(Exception){}

            int IdUsuario = 0;
            //GET USER ID
            var consulta = from u in db.Usuarios
                           where u.Entrada == EntradaUsuario
                           select u;
            if(consulta.Count() > 0)
            IdUsuario = consulta.First().Id;

            return IdUsuario;
        }

        public ActionResult PlatoArroz()
        {
            return View();
        }

        public ActionResult PlatoEnsalada()
        {
            return View();
        }

        public ActionResult PlatoCarne()
        {
            return View();
        }

        public ActionResult ValidarGet(ActionResult View)
        {
            //VALIDADOR GET BEGIN
            if (Session["entrada"] == null)
                return RedirectToAction("Index", "Home");

            return View;
        }
	}
}