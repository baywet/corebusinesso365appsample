using System;
using Sindicait;
using WebActivator;
using SnowProCorp.DAL;
using System.Collections.Generic;
using System.Linq;

//[assembly: PostApplicationStartMethod(typeof(SnowProCorp.ShipmentsWeb.App_Start.SindicaitStartCode), "Start")]

namespace SnowProCorp.ShipmentsWeb.App_Start
{
    public class SindicaitStartCode
    {
        public static void Start()
        {
            // TODO: Replace the title, description (optional),
            // entryData and entryMapper properties below.

            DataFeeds.Register<ProductionContext, Shipment>(
                "data/feeds/shipments",
                "shipments",
                db => db.Shipments.Include("Factory").Where(x => x.Factory != null).OrderByDescending(x => x.OrderingDate).Take(100),
                x => new FeedEntry()
                {
                    Authors = new List<string>() { x.OrderedBy },
                    Content = x.Address,
                    Published = x.OrderingDate,
                    Updated = x.OrderingDate,
                    Id = x.Id.ToString(),
                    Title = x.Factory == null ? string.Empty : x.Factory.Name,
                    AlternateLinkUri = "https://snowprocorpshipments.azurewebsites.net/Shipment/Details/" + x.Id.ToString() + "?SPHostUrl=https%3A%2F%2Fbaywet.sharepoint.com%2Fsites%2FSnowProCorp"
                },
                "shipments sent",
                "shipments",
                db => db.Shipments.Max(x => x.OrderingDate)
            );
        }
    }
}