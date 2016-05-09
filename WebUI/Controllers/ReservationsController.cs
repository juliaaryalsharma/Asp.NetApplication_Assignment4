using MidTermSolution.Contracts.Repositories;
using MidTermSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class ReservationsController : Controller
    {
        // GET: Reservations
         IRepositoryBase<Reservation> reservations;


        public ReservationsController(IRepositoryBase<Reservation>reservations)
        {

            this.reservations = reservations;
        }

        public ActionResult Index()
        {
            var model = reservations.GetAll();
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var reservation = reservations.GetById(id);
            return View(reservation);
        }


    }
}
