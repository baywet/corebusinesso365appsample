using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowProCorp.DAL
{
    [Flags]
    public enum ShipmentStatus
    {
        OrderReceived = 1,
        InPreparation = 2,
        PickepUp = 4,
        Delivered = 8,
        Lost = 16
    }
}
