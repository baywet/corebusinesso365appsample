using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowProCorp.DAL
{
    public class ProducedItem
    {
        public Guid Id { get; set; }
        public virtual ProducedItemType Type { get; set; }
        public double Size { get; set; }
        public string Color { get; set; }
        public bool QualityOk { get; set; }
        public DateTime ProductionDate { get; set; }
        [Required]
        public virtual ProductionFactory Factory { get; set; }
        public virtual Shipment Shipment { get; set; }
    }
}
