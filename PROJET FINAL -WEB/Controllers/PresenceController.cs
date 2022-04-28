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
    /// Classe représentant le controleur de vue des Présences.
    /// </summary>
    public class PresenceController : Controller
    {
        /// <summary>
        /// Méthode de service appelé lors de l'action Index.
        /// Rôles de l'action : 
        ///   -Afficher la liste des Présences.
        ///   -Afficher le formulaire pour l'ajout d'une Présence.
        /// </summary>
        /// <returns>ActionResult suite aux traitements des données.</returns>
        [Route("Presence")]
        [Route("Presence/Index")]
        [Route("Presence/ObtenirListePresence")]
        [HttpGet]
        public async Task<IActionResult> Index(string nomGarderie)
        {
            //Mettre le if et son contenu en commentaire avant de lancer les tests fonctionnels...
            //**Le contenu du ELSE doit toutefois rester actif...**//
            //Si erreur provenant d'une autre action
            if (TempData["MessageErreur"] != null)
                ViewBag.MessageErreur = TempData["MessageErreur"];
            else
            {
                try
                {
                    if (nomGarderie == null)
                    {
                        JsonValue jsonResponseGarderieNom = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Garderie/ObtenirListeGarderie");
                        nomGarderie = JsonConvert.DeserializeObject<List<GarderieDTO>>(jsonResponseGarderieNom.ToString()).ToArray()[0].Nom;
                    }
                    JsonValue jsonResponseEnfantNom = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Enfant/ObtenirListeEnfant");
                    EnfantDTO enfant = JsonConvert.DeserializeObject<List<EnfantDTO>>(jsonResponseEnfantNom.ToString()).ToArray()[0];

                    //Préparation des données pour la vue...
                    ViewBag.NomGarderie = nomGarderie;
                    ViewBag.Enfant = enfant;

                    JsonValue jsonResponseGarderie = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Garderie/ObtenirListeGarderie");
                    ViewBag.ListeGarderies = JsonConvert.DeserializeObject<List<GarderieDTO>>(jsonResponseGarderie.ToString()).ToArray();

                    JsonValue jsonResponsePresence = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Presence/ObtenirListePresence?nomGarderie=" + nomGarderie);
                    ViewBag.ListePresences = JsonConvert.DeserializeObject<List<PresenceDTO>>(jsonResponsePresence.ToString()).ToArray();

                    JsonValue jsonResponseEnfant = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Enfant/ObtenirListeEnfant");
                    ViewBag.ListeEnfants = JsonConvert.DeserializeObject<List<EnfantDTO>>(jsonResponseEnfant.ToString()).ToArray();

                }
                catch (Exception e)
                {
                    ViewBag.MessageErreur = e.Message;
                }


            }
            //Retour de la vue...
            return View();
        }

        /// <summary>
        /// Méthode de service appelé lors de l'action AjouterPresence.
        /// Rôles de l'action : 
        ///   -Ajouter une Présence.
        /// </summary>
        /// <param name="presenceDTO">Le DTO de la Présence.</param>
        /// <returns>IActionResult</returns>
        [Route("/Presence/AjouterPresence")]
        [HttpPost]
        public async Task<IActionResult> AjouterPresence([FromForm] string nomGarderie, [FromForm] PresenceDTO presenceDTO, [FromForm] string enfantInfo)
        {
            try
            {
                string[] enfant = enfantInfo.Split(";");
                presenceDTO.Enfant = new EnfantDTO(enfant[0], enfant[1], enfant[2], null, null, null, null);

                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Presence/AjouterPresence?nomGarderie=" + nomGarderie, presenceDTO);
            }
            catch (Exception e)
            {
                //Mettre cette ligne en commentaire avant de lancer les tests fonctionnels
                TempData["MessageErreur"] = e.Message;
            }

            //Lancement de l'action Index...
            return RedirectToAction("Index", "Presence");
        }
    }
}