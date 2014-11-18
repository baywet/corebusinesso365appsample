using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace SnowProCorp.DAL
{
    public class ProductionFactory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        [ScriptIgnore]
        public virtual ICollection<ProducedItem> ProducedItems { get; set; }
        public int ProductionLines { get; set; }
        [ScriptIgnore]
        public virtual ICollection<Shipment> Shipments { get; set; }
    }
}
