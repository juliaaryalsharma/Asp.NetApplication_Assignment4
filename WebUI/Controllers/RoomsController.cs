using MidTermSolution.Contracts.Repositories;
using MidTermSolution.Models;
using MidTermSolution.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class RoomsController : Controller
    {
        IRepositoryBase<Customer> customers;
        IRepositoryBase<Room> rooms;
        IRepositoryBase<Booking> bookings;
        IRepositoryBase<BookingRoom> bookingsrooms;
        IRepositoryBase<Offer> offers;
        IRepositoryBase<BookingOffer> bookingOffers;
        IRepositoryBase<OfferType> offerTypes;
        BookingService bookingService;




        public RoomsController( IRepositoryBase<Customer> customers,
                                IRepositoryBase<Room> rooms,
                                IRepositoryBase<Booking> bookings,
                                IRepositoryBase<BookingRoom> bookingrooms,
                                IRepositoryBase<Offer> offers,
                                IRepositoryBase<BookingOffer> bookingOffers,
                                IRepositoryBase<OfferType> offerTypes)
        {
            this.customers = customers;
            this.rooms = rooms;
            this.bookings = bookings;
            this.bookingsrooms = bookingrooms;
            this.offers = offers;
            this.bookingOffers = bookingOffers;
            this.offerTypes = offerTypes;

            bookingService = new BookingService(this.bookings, this.bookingsrooms, this.offers,
                                      this.offerTypes, this.bookingOffers);
        }
        // GET: Rooms
        public ActionResult AddToBooking(int id)
        {
            bookingService.AddToBooking(this.HttpContext, id, 1);//always add one to the booking
            return RedirectToAction("BookingSummary");
        }
        //new functions
        public ActionResult QuantityInBooking()
        {
            var result = bookingService.QuantityInBooking(this.HttpContext);
            return Json(result);
        }
        //new booking summary method
        public ActionResult BookingSummary()
        {
            ViewBag.QuantityInBooking = bookingService.QuantityInBooking(this.HttpContext);
            ViewBag.AmountInBooking = bookingService.AmountInBooking(this.HttpContext);
            var model = bookingService.GetBooking(this.HttpContext);

             //orders.GetAll().Where(p => p.CustomerId == id).OrderBy(p => p.OrderId).ToList();
            //var offers = bookingOffers.GetAll().Where(p=>p.BookingID = model.BookingID)
            ViewBag.OffersInBooking = model.BookingOffers.ToList();

            return View(model.BookingRooms);
        }
        public ActionResult DeleteFromBooking(int id)
        {
            BookingRoom bookingRoom = bookingService.GetBookingRoomById(id);
            if (bookingRoom == null)
            {
                return HttpNotFound();
            }
            return View(bookingRoom);
        }

        [HttpPost, ActionName("DeleteFromBooking")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            bookingService.RemoveFromBooking(id);
            return RedirectToAction("BookingSummary");
        }
        public ActionResult BookingRoomDetails(int? id)
        {
            var room = rooms.GetById(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }
        public ActionResult BookingRoomAdd(string searchString, string sortOrder)
        {
            var room = rooms.GetAll();

            if (!String.IsNullOrEmpty(searchString))
            {
                room = room.Where(s => s.RoomDescription.Contains(searchString));
            }

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";

            switch (sortOrder)
            {
                case "name_desc":
                    room = room.OrderByDescending(s => s.RoomDescription);
                    break;
                case "Price":
                    room = room.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    room = room.OrderByDescending(s => s.Price);
                    break;
                default:
                    room = room.OrderBy(s => s.RoomDescription);
                    break;
            }

            return View(room);
        }
        // GET: list with filter
        public ActionResult Index(string searchString, string sortOrder)
        {
            var room = rooms.GetAll();

            if (!String.IsNullOrEmpty(searchString))
            {
                room = room.Where(s => s.RoomDescription.Contains(searchString));
            }

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";

            switch (sortOrder)
            {
                case "name_desc":
                    room = room.OrderByDescending(s => s.RoomDescription);
                    break;
                case "Price":
                    room = room.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    room = room.OrderByDescending(s => s.Price);
                    break;
                default:
                    room = room.OrderBy(s => s.RoomDescription);
                    break;
            }

            return View(room);
        }

        // GET: /Details/5
        public ActionResult Details(int? id)
        {
            var room = rooms.GetById(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }
        public ActionResult EditBookingRoom(int id = 0)
        {
            BookingRoom item = bookingService.GetBookingRoom(this.HttpContext, id);

            var prdlst = rooms.GetAll();
            var query = from p in prdlst
                        select p.RoomID;

            ViewBag.pl = query.ToList();
            return View(item);
        }
        [HttpPost]
        public ActionResult EditBookingRoom(BookingRoom item)
        {
            bookingsrooms.Update(item);
            bookingsrooms.Commit();

            return RedirectToAction("BookingSummary");
        }

        // Offer Add View
        public ActionResult OfferAdd()
        {
            var _offers = offers.GetAll();
            return View(_offers);
        }

        // AddOfferToBooking method
        public ActionResult AddOfferToBooking(string code)
        {
            bookingService.AddOffer(code, this.HttpContext);
            return RedirectToAction("BookingSummary");
        }

        public ActionResult DeleteOfferFromBooking(int id)
        {
            bookingService.DeleteOffer(id, this.HttpContext);
            return RedirectToAction("BookingSummary");
        }
    }
}
