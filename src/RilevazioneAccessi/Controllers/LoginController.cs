using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RilevazioneAccessi.Data;

namespace RilevazioneAccessi.Controllers
{
    public class LoginController : Controller
    {
        private IDataAccess _data;

        public LoginController(IDataAccess dataAccess)
        {
            this._data = dataAccess;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string user, string passw)
        {
            bool isAuthenticated = _data.isAuthenticated(user, passw);
            if(isAuthenticated)
                return RedirectToAction("","");
            return RedirectToAction("","");
        } 
    }
}
