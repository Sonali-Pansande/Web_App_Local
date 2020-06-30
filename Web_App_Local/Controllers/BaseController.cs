using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web_App_Local.CustomFilters;

namespace Web_App_Local.Controllers
{
    [ServiceFilter(typeof(CustomExceptionFilter))]
    public class BaseController : Controller
    {
    }
}
