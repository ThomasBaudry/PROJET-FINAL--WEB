using Microsoft.AspNetCore.Mvc;
using System.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using GestionCegepWeb.Utils;
using PROJET_FINAL__WEB.Models;
using System;

namespace PROJET_FINAL__WEB.Controllers
{
    public class GarderieController : Controller
    {
        [Route("")]
        [Route("Garderie")]
        [Route("Garderie/Index")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                //Préparation des données pour la vue...
                JsonValue jsonResponse = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Garderie");
                ViewBag.ListeGarderies = JsonConvert.DeserializeObject<List<GarderieDTO>>(jsonResponse.ToString()).ToArray();
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            return View();
        }

        [Route("/Garderie/AjouterCegep")]
        [HttpPost]
        public async Task<IActionResult> AjouterGarderie([FromForm] GarderieDTO garderieDTO)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Garderie/AjouterGarderie", garderieDTO);
            }
            catch (Exception e)
            {
                TempData["MessageErreur"] = e.Message;
            }

            //Lancement de l'action Index...
            return RedirectToAction("Index", "Garderie");
        }

        [Route("/Garderie/FormulaireModifierGarderie")]
        [HttpGet]
        public async Task<IActionResult> FormulaireModifierGarderie([FromQuery] string nomGarderie)
        {
            try
            {
                if (TempData["MessageErreur"] != null)
                    ViewBag.MessageErreur = TempData["MessageErreur"];
                JsonValue jsonResponse = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Garderie/ObtenirGarderie?nomGarderie=" + nomGarderie);
                GarderieDTO cegep = JsonConvert.DeserializeObject<GarderieDTO>(jsonResponse.ToString());
                return View(cegep);
            }
            catch (Exception e)
            {
                TempData["MessageErreur"] = "Impossible d'ouvrir le formulaire de modification. " + e.Message;
            }
            return RedirectToAction("Index");
        }

        [Route("/Garderie/ModifierGarderie")]
        [HttpPost]
        public async Task<IActionResult> ModifierGarderie([FromForm] GarderieDTO garderieDTO)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Garderie/ModifierGarderie", garderieDTO);
            }
            catch (Exception e)
            {
                TempData["MessageErreur"] = "Impossible de procéder à la modification." + e.Message;
                return RedirectToAction("FormulaireModifierGarderie", "Cegep", new { nomGarderie = garderieDTO.Nom });
            }
            //Lancement de l'action Index...
            return RedirectToAction("Index");
        }
        [Route("/Garderie/SupprimerGarderie")]
        [HttpPost]
        public async Task<IActionResult> SupprimerGarderie([FromForm] string nomGarderie)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Garderie/SupprimerGarderie?nomGarderie=" + nomGarderie, null);
            }
            catch (Exception e)
            {
                TempData["MessageErreur"] = e.Message;
            }

            return RedirectToAction("Index");
        }

        [Route("/Garderie/ViderListeGarderie")]
        [HttpPost]
        public async Task<IActionResult> ViderListeGarderie()
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Garderie/ViderListeGarderie", null);
            }
            catch (Exception e)
            {
                TempData["MessageErreur"] = e.Message;
            }

            return RedirectToAction("Index");
        }
    }
}