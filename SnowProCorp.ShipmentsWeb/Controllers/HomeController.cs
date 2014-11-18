using Microsoft.SharePoint.Client;
using SnowProCorp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using SnowProCorp.ShipmentsWeb.Models;
using SnowProCorp.ShipmentsWeb.ViewModel;

namespace SnowProCorp.ShipmentsWeb.Controllers
{
    public class HomeController : BaseController
    {
        [SharePointContextFilter]
        public ActionResult Index()
        {
            //User spUser = null;

            //var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);

            //using (var clientContext = spContext.CreateUserClientContextForSPHost())
            //{
            //    if (clientContext != null)
            //    {
            //        spUser = clientContext.Web.CurrentUser;

            //        clientContext.Load(spUser, user => user.Title);

            //        clientContext.ExecuteQuery();

            //        ViewBag.UserName = spUser.Title;
            //    }
            //}
            Session["UserName"] = GetCurrentSpUser().Title;
            
            ViewBag.UserName = Session["UserName"];

            return View();
        }

        private User GetCurrentSpUser()
        {
            User spUser = null;

            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);

            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                if (clientContext != null)
                {
                    spUser = clientContext.Web.CurrentUser;

                    clientContext.Load(spUser, user => user.Title, user=>user.LoginName);

                    clientContext.ExecuteQuery();

                    ViewBag.UserName = spUser.Title;
                    return spUser;
                }
            }
            throw new Exception("User not logged");
        }

        public ActionResult GetAvailableItems(int pageNumber = 1, int pageSize = 20, string columnOrderBy = "ProductionDate", string orderBy = default(string))
        {
            using (ProductionContext ctx = new ProductionContext())
            {
                ctx.ProducedItemsTypes.Load();
                var elems = ctx.ProducedItems.Include(x => x.Type).Include(x => x.Factory).Where(x => x.Shipment == null);
                if (string.IsNullOrEmpty(orderBy))
                    elems = elems.OrderBy(columnOrderBy);
                else
                    elems = elems.OrderByDescending(columnOrderBy);
                elems = elems.Skip(pageNumber * pageSize).Take(pageSize);

                var vm = new GenericPagingInfo<List<ProducedItem>>()
                {
                    Data = elems.ToList(),
                    PagingInfo = new PagingInfo()
                    {
                        CurrentPageIndex = pageNumber,
                        ItemsPerPage = pageSize,
                        TotalItems = ctx.ProducedItems.Count(x => x.Shipment == null)
                    }
                };

                return PartialView(vm);
            }
        }

        [HttpGet]
        public ActionResult GetAvailableProducedItems(int pageSize, int pageNumber, string columnOrderBy = "ProductionDate", string orderBy = default(string))
        {
            using (ProductionContext ctx = new ProductionContext())
            {
                ctx.ProducedItemsTypes.Load();
                var elems = ctx.ProducedItems.Include(x => x.Type).Include(x => x.Factory).Where(x => x.Shipment == null);
                elems = string.IsNullOrEmpty(orderBy) ? elems.OrderBy(columnOrderBy) : elems.OrderByDescending(columnOrderBy);

                return new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = elems.Skip(pageNumber * pageSize).Take(pageSize).ToList(),
                    MaxJsonLength = int.MaxValue
                };
            }
        }

        [HttpPost]
        public JsonResult Ship(string[] idToShip, string address)
        {
            try
            {
                using (var ctx = new ProductionContext())
                {
                    var guidList = idToShip.Select(i => new Guid(i)).ToList();
                    var productToShip = ctx.ProducedItems.Where(p => guidList.Contains(p.Id)).ToList();
                    var shipmentToAdd = new Shipment()
                    {
                        Id = Guid.NewGuid(),
                        Address = address,
                        Items = productToShip,
                        OrderingDate = DateTime.Now,
                        Status = ShipmentStatus.InPreparation,
                        OrderedBy = Session["UserName"] as string,
                        Factory = productToShip.First().Factory
                    };

                    ctx.Shipments.Add(shipmentToAdd);
                    ctx.SaveChanges();
                }
                //fais tes affaires la
                return JsonSuccessNotification("Shipments shipped with success");
            }
            catch (Exception e)
            {
                return JsonErrorNotification("Oops");
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}
