using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SnowProCorp.DAL;
using SnowProCorp.ShipmentsWeb.Models;
using SnowProCorp.ShipmentsWeb.ViewModel;

namespace SnowProCorp.ShipmentsWeb.Controllers
{
    public class ShipmentController : Controller
    {

        // GET: /Shipment/
        public ActionResult Index()
        {
            using (ProductionContext db = new ProductionContext())
                return View(db.Shipments.ToList());
        }

        public ActionResult List(int pageNumber = 0, int pageSize = 50)
        {
            using (ProductionContext db = new ProductionContext())
            {
                var shipments = db.Shipments.OrderByDescending(o => o.OrderingDate).Skip(pageNumber * pageSize).Take(pageSize).ToList();
                var pageInfo = new PagingInfo()
                {
                    CurrentPageIndex = pageNumber,
                    ItemsPerPage = pageSize,
                    TotalItems = db.Shipments.Count()
                };
                var vm = new GenericPagingInfo<List<Shipment>>()
                {
                    Data = shipments,
                    PagingInfo = pageInfo
                };
                return View("_List", vm);
            }
        }

        // GET: /Shipment/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (ProductionContext db = new ProductionContext())
            {
                Shipment shipment = db.Shipments.Find(id);
                if (shipment == null)
                {
                    return HttpNotFound();
                }
                return View(shipment);
            }
        }
    }
}
