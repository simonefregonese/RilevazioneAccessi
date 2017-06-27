﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RilevazioneAccessi.Models;
using RilevazioneAccessi.Data;
using RilevazioneAccessi.Models.ViewModels;
using System.Net.Http;

namespace RilevazioneAccessi.Controllers
{
    public class AccessController : Controller
    {
        private IDataAccess _data;

        public AccessController(IDataAccess dataAccess)
        {
            this._data = dataAccess;
        }

        /*public IActionResult GetAll()
        {
            var accessi = _data.ListAccessi();
            var passaggi = new List<RilevazioniViewModel>();

            foreach (var accesso in accessi)
            {
                passaggi.Add(new RilevazioniViewModel()
                {
                    accesso = new Accessi()
                    {
                        Id = accesso.Id,
                        DataOra = accesso.DataOra,
                        Gate = accesso.Gate.Trim(),
                        Id_tipoaccesso = accesso.Id_tipoaccesso,
                        Id_utente = accesso.Id_utente,
                        Sospettato = accesso.Sospettato

                    },
                    tipoAccesso = new TipiAccesso()
                    {
                        Id = accesso.Id_tipoaccesso,
                        Valore = _data.GetValore(accesso.Id_tipoaccesso)
                    }
                });
            }

            return Json(passaggi);
        }*/

        public JsonResult GetAll()
        {
            return Json(_data.GetUtenti());
        }

        public JsonResult Check(int id)
        {
            return Json(_data.GetUtenteById(id));
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
