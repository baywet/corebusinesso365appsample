using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace SnowProCorp.DAL
{

    public class ProducedItemType
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [ScriptIgnore]
        public virtual ICollection<ProducedItem> ProducedItems { get; set; }
    }
}
