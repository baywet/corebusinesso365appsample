using SnowProCorp.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnowProCorp.Factory
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void generateData_Click(object sender, EventArgs e)
        {
            var randomizer = new Random();
            using (ProductionContext ctx = new ProductionContext())
            {
                var years = new int[] { 2012, 2013, 2014 };
                var factories = ctx.ProductionFactories.ToList();
                var types = ctx.ProducedItemsTypes.ToList();
                var colors = new string[] { "red", "blue", "green", "orange", "purple", "pink", "black", "white", "cyan", "brown", "grey", "yellow" };
                var colorsCount = colors.Count();
                var orderes = new string[] { "john", "joe", "sarah", "kristen", "hannah", "michael", "matthew", "joshua" };
                var addresses = new string[] { "3455 rue durocher montreal", "Parc national du Mercantour, 04400 Uvernet-Fours, France", "Mont-Tremblant, QC, Canada", "4573 Chateau Boulevard Whistler, BC V0N 1B4, Canada", "2 Mount Allan Drive Kananaskis, AB T0L 2H0, Canada" };
                var statuses = new ShipmentStatus[] { ShipmentStatus.Delivered, ShipmentStatus.InPreparation, ShipmentStatus.Lost, ShipmentStatus.OrderReceived, ShipmentStatus.PickepUp };
                foreach (var year in years)
                {
                    for (var month = 1; month < 13; month++)
                    {
                        if (year == DateTime.Now.Year && month > DateTime.Now.Month)
                            break;

                        var numberOfProducedItemsForMonth = randomizer.Next(0, 1000);
                        for (var i = 0; i < numberOfProducedItemsForMonth; i++)
                        {
                            var productionDate = new DateTime(year, month, randomizer.Next(1, 28));
                            if (productionDate > DateTime.Now)
                                productionDate = DateTime.Now;

                            ctx.ProducedItems.Add(new ProducedItem()
                            {
                                Id = Guid.NewGuid(),
                                ProductionDate = productionDate,
                                Factory = factories[randomizer.Next(0, factories.Count)],
                                Type = types[randomizer.Next(0, types.Count)],
                                QualityOk = Convert.ToBoolean(randomizer.Next(0, 2)),
                                Size = randomizer.Next(28, 47),
                                Color = colors[randomizer.Next(0, colorsCount)],
                            });
                        }
                        ctx.SaveChanges();
                        foreach (var factory in ctx.ProductionFactories)
                        {
                            var numberOfDelevriesForMonth = randomizer.Next(0, 20);
                            for (var i = 0; i < numberOfDelevriesForMonth; i++)
                            {
                                var numberOfItemsInShipment = randomizer.Next(0, 10);
                                var shippedItems = ctx.ProducedItems.Where(x => x.Factory.Id == factory.Id && x.Shipment == null).Take(numberOfItemsInShipment).ToList();
                                var orderingDate = new DateTime(year, month, randomizer.Next(1, 28));
                                if (orderingDate > DateTime.Now)
                                    orderingDate = DateTime.Now;
                                var currentShipment = new Shipment()
                                {
                                    Items = shippedItems,
                                    Factory = factory,
                                    Id = Guid.NewGuid(),
                                    OrderedBy = orderes[randomizer.Next(0, orderes.Length)],
                                    OrderingDate = orderingDate,
                                    Address = addresses[randomizer.Next(0, addresses.Length)],
                                    Status = statuses[randomizer.Next(0, statuses.Length)]
                                };
                                if (currentShipment.Status >= ShipmentStatus.PickepUp)
                                    currentShipment.PickedUpDate = orderingDate == DateTime.Now ? DateTime.Now : currentShipment.OrderingDate.AddDays(1);
                                if (currentShipment.Status >= ShipmentStatus.Delivered && ShipmentStatus.Lost != currentShipment.Status)
                                    currentShipment.DeliveredDate = orderingDate == DateTime.Now ? DateTime.Now : currentShipment.OrderingDate.AddDays(randomizer.Next(0, 15));
                                ; ctx.Shipments.Add(currentShipment);
                                shippedItems.ForEach(x => x.Shipment = currentShipment);
                            }
                        }
                        ctx.SaveChanges();
                    }
                }
                ctx.SaveChanges();
            }
        }

        private void btnItinitiateSystem_Click(object sender, EventArgs e)
        {
            using (ProductionContext ctx = new ProductionContext())
            {
                ctx.ProducedItemsTypes.Add(new ProducedItemType() { Title = "Ski" });
                ctx.ProducedItemsTypes.Add(new ProducedItemType() { Title = "Snowboard" });
                ctx.ProducedItemsTypes.Add(new ProducedItemType() { Title = "Snowblade" });
                ctx.ProducedItemsTypes.Add(new ProducedItemType() { Title = "Ski Competition" });
                ctx.ProducedItemsTypes.Add(new ProducedItemType() { Title = "Ski Double Parabolique" });

                ctx.ProductionFactories.Add(new ProductionFactory() { Name = "DORVAL", Address = "1695 Av 55E, Dorval QC H9P 2W3", ProductionLines = 0 });
                ctx.ProductionFactories.Add(new ProductionFactory() { Name = "MONT TREMBLANT", Address = "1030 Emond Rue, Mont-Tremblant QC J8E 2M5", ProductionLines = 3 });
                ctx.ProductionFactories.Add(new ProductionFactory() { Name = "CALGARY", Address = "14-3919 Richmond Rd SW, Calgary AB T3E 4P2", ProductionLines = 10 });


                ctx.SaveChanges();
            }
        }
    }
}
