using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BCoreMvc.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace BCoreMvc.Controllers
{    
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();

            //return RedirectPermanent("Update/Index");
        }        
    }
}