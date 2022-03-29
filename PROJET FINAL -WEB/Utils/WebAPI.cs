using System;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Json;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

/// <summary>
/// Namespace contenant les classes d'utilisation autres.
/// </summary>
namespace GestionCegepWeb.Utils
{
    /// <summary>
    /// Classe représentant l'outil de communication vers le service Web distant.
    /// </summary>
    public class WebAPI
    {
        /// <summary>
        /// Instance unique de la classe.
        /// </summary>
        private static WebAPI instance;

        /// <summary>
        /// Le constructeur privé de la classe.
        /// </summary>
        private WebAPI(){}

        /// <summary>
        /// Méthode permettant d'obtenir l'instance unique de la classe.
        /// </summary>
        public static WebAPI Instance
        {
            get
            {
                if (instance == null)
                    instance = new WebAPI();
                return instance;
            }
        }

        /// <summary>
        /// Méthode permettant d'exécuter une commande GET.
        /// </summary>
        /// <param name="url">L'url pour la commande GET.</param>
        /// <returns>Le retour de la commande en format JSON.</returns>
        public async Task<JsonValue> ExecuteGetAsync(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

            WebResponse response = await request.GetResponseAsync();

            Stream stream = response.GetResponseStream();

            JsonValue json = await Task.Run(() => JsonValue.Load(stream));

            return json;
        }

        /// <summary>
        /// Méthode permettant d'envoyer une commande POST.
        /// </summary>
        /// <param name="url">L'url pour la commande POST.</param>
        /// <param name="dto">Le DTO désiré.</param>
        public async Task PostAsync(string url, object dto)
        {
            HttpClient client = new HttpClient
            {
                MaxResponseContentBufferSize = 256000
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(new Uri(url), content);

            if (response.IsSuccessStatusCode)
                Console.Error.WriteLine("L'appel POST a réussi.");
            else
                Console.Error.WriteLine("Erreur : " + response.ReasonPhrase);
        }
    }
}