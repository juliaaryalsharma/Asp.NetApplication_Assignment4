using MidTermSolution.Contracts.Data;
using MidTermSolution.Contracts.Repositories;
using MidTermSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepositoryBase<Room> rooms;
        IRepositoryBase<Customer> customers;
        IRepositoryBase<Reservation> reservations;

        public HomeController(IRepositoryBase<Customer> customers, IRepositoryBase<Room> rooms, IRepositoryBase<Reservation> reservations)
        {
            this.customers = customers;
            this.rooms = rooms;
            this.reservations = reservations;
        }

        //public ActionResult Index()
        //{
        //    CustomerRepository customers = new CustomerRepository(new DataContext());
        //    ProductRepository products = new ProductRepository(new DataContext());
        //    Orders orders = new Orders(new DataContext());
        //    return View();
        //}

        public ActionResult Index()
        {
            var roomList = rooms.GetAll();

            return View(roomList);
            //return View();
        }

        public ActionResult Details(int id)
        {
            var room = rooms.GetById(id);
            return View(room);
        }

        public ActionResult About()
        {
            ViewBag.Message = "KAS believes in quality.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "We are committed for quick service.";

            return View();
        }
    }
}
