using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SnowProCorp.ShipmentsWeb.Controllers
{
    public class BaseController : Controller
    {
        [NonAction]
        public JsonResult JsonSuccessNotification(string message)
        {
            return JsonNotification(message, NotificationType.Success);
        }

        [NonAction]
        public JsonResult JsonInfoNotification(string message)
        {
            return JsonNotification(message, NotificationType.Info);
        }

        [NonAction]
        public JsonResult JsonErrorNotification(string message)
        {
            return JsonNotification(message, NotificationType.Error);
        }

        private JsonResult JsonNotification(string message, NotificationType notifType)
        {
            return new JsonResult()
            {
                Data = new
                {
                    type = notifType.ToString(),
                    message = message
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }

    public enum NotificationType : uint
    {
        Success=1,
        Info = 2,
        Error = 3
    }
}