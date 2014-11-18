using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SnowProCorp.ShipmentsWeb.Models;

namespace SnowProCorp.ShipmentsWeb.ViewModel
{
    public class GenericPagingInfo<T> 
    {
        public PagingInfo PagingInfo { get; set; }
        public T Data { get; set; }
    }
}