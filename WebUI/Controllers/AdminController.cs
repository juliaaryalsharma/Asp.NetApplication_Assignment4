using MidTermSolution.Contracts.Repositories;
using MidTermSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class AdminController : Controller
    {
        IRepositoryBase<Customer> customers;
        IRepositoryBase<Room> rooms;
        IRepositoryBase<Reservation> reservations;
        IRepositoryBase<Offer> offers;
        IRepositoryBase<OfferType> offerTypes;


        public AdminController( IRepositoryBase<Customer> customers,
                                IRepositoryBase<Room> rooms,
                                IRepositoryBase<Reservation>reservations,
                                IRepositoryBase<Offer> offers,
                                IRepositoryBase<OfferType> offerTypes)
        {
            this.customers = customers;
            this.rooms = rooms;
            this.reservations = reservations;
            this.offers = offers;
            this.offerTypes = offerTypes;
        }


        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RoomsList()
        {
            var model = rooms.GetAll();
            return View(model);
        }

        public ActionResult CreateRoom()
        {
            var model = new Room();
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateRoom(Room room)
        {
            rooms.Insert(room);
            rooms.Commit();
            return RedirectToAction("RoomsList");
        }




        public ActionResult CustomersList()
        {
            var model = customers.GetAll();
            return View(model);
        }

        public ActionResult CreateCustomer()
        {
            var model = new Customer();
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateCustomer(Customer customer)
        {
            customers.Insert(customer);
            customers.Commit();
            return RedirectToAction("CustomersList");
        }

        public ActionResult ReservationsList()
        {
            var model = reservations.GetAll();
            return View(model);
        }

        public ActionResult CreateReservation()
        {
            var model = new Reservation();
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateReservation(Reservation reservation)
        {
            reservations.Insert(reservation);
            reservations.Commit();
            return RedirectToAction("ReservationsList");
        }




        //Room detail method
        public ActionResult DetailsRoom(int id)
        {
            var room = rooms.GetById(id);
            return View(room);
        }

        //customer detail method
        public ActionResult DetailsCustomer(int id)
        {
            var customer = customers.GetById(id);
            return View(customer);
        }

        //Reservation detail method
        public ActionResult DetailsReservation(int id)
        {
            var reservation = reservations.GetById(id);
            return View(reservation);
        }

        //edit Room method
        public ActionResult EditRoom(int id)
        {
            Room room = rooms.GetById(id);
            return View(room);
        }
        [HttpPost]
        public ActionResult EditRoom(Room room)
        {
            rooms.Update(room);
            rooms.Commit();

            return RedirectToAction("RoomsList");
        }
        //edit customer method
        public ActionResult EditCustomer(int id)
        {
            Customer customer = customers.GetById(id);
            return View(customer);
        }
        [HttpPost]
        public ActionResult EditCustomer(Customer customer)
        {
            customers.Update(customer);
            customers.Commit();

            return RedirectToAction("CustomersList");
        }

        //edit Reservation method
        public ActionResult EditReservation(int id)
        {
            Reservation Reservation = reservations.GetById(id);
            return View(Reservation);
        }
        [HttpPost]
        public ActionResult EditReservation(Reservation Reservation)
        {
            reservations.Update(Reservation);
            reservations.Commit();

            return RedirectToAction("ReservationsList");
        }
        //delete Room method
        public ActionResult DeleteRoom(int id)
        {
            Room room = rooms.GetById(id);
            return View(room);
        }

        [HttpPost, ActionName("DeleteRoom")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoomConfirm(int id)
        {
            rooms.Delete(rooms.GetById(id));
            //Rooms.Delete(Room);
            rooms.Commit();
            return RedirectToAction("RoomsList");
        }
        //delete customer method
        public ActionResult DeleteCustomer(int id)
        {
            Customer customer = customers.GetById(id);
            return View(customer);
        }

        [HttpPost, ActionName("DeleteCustomer")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCustomerConfirm(int id)
        {
            customers.Delete(customers.GetById(id));
            customers.Commit();
            return RedirectToAction("CustomerList");
        }

        //delete Reservation method
        public ActionResult DeleteReservation(int id)
        {
            Reservation Reservation = reservations.GetById(id);
            return View(Reservation);
        }

        [HttpPost, ActionName("DeleteReservation")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteReservationConfirm(int id)
        {
            reservations.Delete(reservations.GetById(id));
            //Reservations.Delete(Reservation);
            reservations.Commit();
            return RedirectToAction("ReservationsList");
        }

        //Offer CRUD
        public ActionResult OfferList()
        {
            var model = offers.GetAll();
            return View(model);
        }
        //offer detail method
        public ActionResult DetailsOffer(int id)
        {
            var model = offers.GetById(id);
            return View(model);
        }
        public ActionResult CreateOffer()
        {
            var model = new Offer();

            var typeList = offerTypes.GetAll();
            var query = from t in typeList
                        select t.OfferTypeId;
            ViewBag.types = query.ToList();

            var roomList = rooms.GetAll();
            var query2 = from r in roomList
                        select r.RoomID;
            ViewBag.rooms = query2.ToList();

            return View(model);
        }
        [HttpPost]
        public ActionResult CreateOffer(Offer offer)
        {
            offers.Insert(offer);
            offers.Commit();
            return RedirectToAction("OfferList");
        }

        public ActionResult EditOffer(int id)
        {
            Offer offer = offers.GetById(id);
            return View(offer);
        }
        [HttpPost]
        public ActionResult EditOffer(Offer offer)
        {
            offers.Update(offer);
            offers.Commit();
            return RedirectToAction("OfferList");
        }
        public ActionResult DeleteOffer(int id)
        {
            Offer Offer = offers.GetById(id);
            return View(Offer);
        }

        [HttpPost, ActionName("DeleteOffer")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteOfferConfirm(int id)
        {
            offers.Delete(offers.GetById(id));
            offers.Commit();
            return RedirectToAction("OfferList");
        }

        //Offer types CRUD
        public ActionResult OfferTypeList()
        {
            var model = offerTypes.GetAll();
            return View(model);
        }
        //offer detail method
        public ActionResult DetailsOfferType(int id)
        {
            var model = offerTypes.GetById(id);
            return View(model);
        }
        public ActionResult CreateOfferType()
        {
            var model = new OfferType();
            ViewBag.offerTypes = offerTypes.GetAll();
            ViewBag.rooms = rooms.GetAll();
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateOfferType(OfferType offerType)
        {
            offerTypes.Insert(offerType);
            offerTypes.Commit();
            return RedirectToAction("OfferTypeList");
        }

        public ActionResult EditOfferType(int id)
        {
            OfferType offerType = offerTypes.GetById(id);
            return View(offerType);
        }
        [HttpPost]
        public ActionResult EditOfferType(OfferType offerType)
        {
            offerTypes.Update(offerType);
            offerTypes.Commit();
            return RedirectToAction("OfferTypeList");
        }

        public ActionResult DeleteOfferType(int id)
        {
            OfferType OfferType = offerTypes.GetById(id);
            return View(OfferType);
        }

        [HttpPost, ActionName("DeleteOfferType")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteOfferTypeConfirm(int id)
        {
            offerTypes.Delete(offerTypes.GetById(id));
            offerTypes.Commit();
            return RedirectToAction("OfferTypeList");
        }
    }
}
