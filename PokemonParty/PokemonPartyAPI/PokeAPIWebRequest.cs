using System.Net.Http;
using System.Threading.Tasks;
using PokeAPI.Net.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



namespace PokeAPI.Net
{
    /// <summary>
    /// Retrieves data from PokeAPI service
    /// </summary>
    public class PokeAPIWebRequest
    {
        private static readonly HttpClient client;
        private static readonly Uri baseuri = new Uri("https://pokeapi.co/api/v2/type/");

        static PokeAPIWebRequest()
        {
             client = new HttpClient();
        }

        /// <summary>
        /// Massages data from PokeAPI service to generate random lineup
        /// </summary>
        public static async Task<string> GenerateTeamAsync(List<string> param)
        {
            int teamCount = 5;
            int typeCount = param.Count;
            Random rnd = new Random();

            var teamList = new List<string>();
            var pokemonList = new List<TypePokemon>();
            string pokeListResult = String.Empty;
            
            var result = await GetTypeByNameAsync(param);

            if(result != null)
            {
                for (int i = teamList.Count; i < teamCount; i++)
                {
                    int arrNum = rnd.Next(typeCount);
                    pokemonList = result.ToList()[arrNum].Pokemon;

                    int pokemonArrNum = rnd.Next(pokemonList.Count);
                    pokeListResult = pokemonList[pokemonArrNum].Pokemon["name"].ToString();

                    teamList.Add(pokeListResult);
                }
            }
            
            return String.Join(", ", teamList);
        }

        /// <summary>
        /// Web Request to get pokemon list
        /// </summary>
        private static async Task<IEnumerable<Types>> GetTypeByNameAsync(List<string> param)
        {
            try
            {
                string apiEndpoint = string.Empty;
                string responseBody = string.Empty;
                JArray responseArr = new JArray();

                HttpResponseMessage response;

                for (int i = 0; i < param.Count; i++)
                {
                    apiEndpoint = baseuri.ToString() + param[i];
                    response = await client.GetAsync(apiEndpoint, HttpCompletionOption.ResponseHeadersRead);
                    response.EnsureSuccessStatusCode();

                    responseBody = await response.Content.ReadAsStringAsync();
                    responseArr.Add(JToken.Parse(responseBody));
                }

                var repositories = JsonConvert.DeserializeObject<IEnumerable<Types>>(responseArr.ToString());

                return repositories;

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            
        }
    }
}