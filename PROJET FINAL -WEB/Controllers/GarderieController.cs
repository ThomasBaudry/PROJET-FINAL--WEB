using Microsoft.AspNetCore.Mvc;
using System.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using GestionCegepWeb.Utils;
using PROJET_FINAL__WEB.Models;
using System;

/// <summary>
/// Namespace pour les controleurs de vue.
/// </summary>
namespace PROJET_FINAL__WEB.Controllers
{
    /// <summary>
    /// Classe représentant le controleur de vue des Garderies.
    /// </summary>
    public class GarderieController : Controller
    {
        /// <summary>
        /// Méthode de service appelé lors de l'action Index.
        /// Rôles de l'action : 
        ///   -Afficher la liste des Garderies.
        ///   -Afficher le formulaire pour l'ajout d'une Garderie.
        /// </summary>
        /// <returns>ActionResult suite aux traitements des données.</returns>
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

        /// <summary>
        /// Méthode de service appelé lors de l'action AjouterGarderie.
        /// Rôles de l'action : 
        ///   -Ajouter une Garderie.
        /// </summary>
        /// <param name="garderieDTO">Le DTO de la Garderie.</param>
        /// <returns>IActionResult</returns>
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

        /// <summary>
        /// Action FormulaireModifierGarderie.
        /// Permet d'afficher le formulaire pour la modification d'une Garderie.
        /// </summary>
        /// <param name="nomGarderie">Nom de la Garderie.</param>
        /// <returns>IActionResult</returns>
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

        /// <summary>
        /// Action ModifierGarderie.
        /// Permet de modifier une Garderie.
        /// </summary>
        /// <param name="garderieDTO">La Garderie a modifier.</param>
        /// <returns>ActionResult</returns>
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

        /// <summary>
        /// Action SupprimerGarderie.
        /// Permet de supprimer une Garderie.
        /// </summary>
        /// <param name="nomGarderie">Le nom de la Garderie.</param>
        /// <returns>ActionResult</returns>
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

        /// <summary>
        /// Action ViderListeGarderie.
        /// Permet de vider la liste des Garderies.
        /// </summary>
        /// <returns>ActionResult</returns>
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