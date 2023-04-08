using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Zanimljiva_Geografija_Tim14
{
    public class Country
    {
        [JsonProperty("name")]
        public Dictionary<string, object> NameDictionary { get; set; }

        [JsonProperty("capital")]
        public List<string> Capitals { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("subregion")]
        public string SubRegion { get; set; }

        [JsonProperty("population")]
        public int Population { get; set; }

        [JsonProperty("flags")]
        public Dictionary<string, string> Flag { get; set; }

        [JsonProperty("coatOfArms")]
        public Dictionary<string, string> CoatOfArms { get; set; }

        [JsonProperty("currencies")]
        public Dictionary<string, Dictionary<string, string>> CurrenciesDictionary { get; set; }

        [JsonProperty("maps")]
        public Dictionary<string, string> Maps { get; set; }

        [JsonProperty("continents")]
        public List<string> Continents { get; set; }

        [JsonProperty("latlng")]
        public decimal[] LatLng { get; set; }

        [JsonProperty("languages")]
        public Dictionary<string, string> LanguagesDictionary { get; set; }

        public string OfficialName
        {
            get => (string)NameDictionary["official"];
            set => NameDictionary["official"] = value;
        }

        public List<string> Languages => LanguagesDictionary.Values.ToList();

        public List<string> GetAllCurrencies()
        {
            List<string> currencies = new List<string>();
            foreach (Dictionary<string, string> dict in CurrenciesDictionary.Values)
            {
                currencies.Add($"{dict["name"]} ({dict["symbol"]})");
            }

            return currencies;
        }

        public Country()
        {

        }

        public Country(Dictionary<string, object> name, List<string> capitals, string region, string subRegion, int population, Dictionary<string, string> flag, Dictionary<string, string> coatOfArms, Dictionary<string, Dictionary<string, string>> currenciesDictionary, Dictionary<string, string> maps, List<string> continents, decimal[] latLng, Dictionary<string, string> languagesDictionary)
        {
            NameDictionary = name;
            Capitals = capitals;
            Region = region;
            SubRegion = subRegion;
            Population = population;
            Flag = flag;
            CoatOfArms = coatOfArms;
            CurrenciesDictionary = currenciesDictionary;
            Maps = maps;
            Continents = continents;
            LatLng = latLng;
            LanguagesDictionary = languagesDictionary;
        }
    }
}
