using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Zanimljiva_Geografija_Tim14
{
    internal class Country
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
        public Dictionary<string, object> Flag { get; set; }

        [JsonProperty("coatOfArms")]
        public Dictionary<string, string> CoatOfArms { get; set; }

        [JsonProperty("currencies")]
        public Dictionary<string, Dictionary<string, string>> Currencies { get; set; }

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

        public Country()
        {

        }

        public Country(Dictionary<string, object> name, List<string> capitals, string region, string subRegion, int population, Dictionary<string, object> flag, Dictionary<string, string> coatOfArms, Dictionary<string, Dictionary<string, string>> currencies, Dictionary<string, string> maps, List<string> continents, decimal[] latLng, Dictionary<string, string> languagesDictionary)
        {
            NameDictionary = name;
            Capitals = capitals;
            Region = region;
            SubRegion = subRegion;
            Population = population;
            Flag = flag;
            CoatOfArms = coatOfArms;
            Currencies = currencies;
            Maps = maps;
            Continents = continents;
            LatLng = latLng;
            LanguagesDictionary = languagesDictionary;
        }
    }
}
