using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace SnowProCorp.DAL
{
    public class Shipment
    {
        public Guid Id { get; set; }
        [Display(Name = "Adresse")]
        public string Address { get; set; }
        [ScriptIgnore]
        public virtual ICollection<ProducedItem> Items { get; set; }
        public virtual ProductionFactory Factory { get; set; }
        [Display(Name = "Status")]
        public ShipmentStatus Status { get; set; }
        [Display(Name = "Ordering date")]
        public DateTime OrderingDate { get; set; }
        [Display(Name = "Ordered by")]
        public string OrderedBy { get; set; }
        [Display(Name = "Picked up date")]
        public DateTime? PickedUpDate { get; set; }
        [Display(Name = "Delivered date")]
        public DateTime? DeliveredDate { get; set; }
    }
}
