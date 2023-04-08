using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace Zanimljiva_Geografija_Tim14
{
    internal class Country
    {
        public string Name { get; set; }
        public List<string> Capitals { get; set; }
        public List<string> Currencies { get; set; }
        public string Region { get; set; }
        public string SubRegion { get; set; }
        public int Population { get; set; }
        public List<string> Continents { get; set; }
        public Dictionary<string, string> Maps { get; set; }
        public decimal[] LatLng { get; set; } 
        public Dictionary<string, string> Flag { get; set; }
        public Dictionary<string, string> CoatOfArms { get; set; }

        public Country()
        {

        }

        public Country(string name, List<string> capitals, List<string> currencies, string region, string subRegion, int population, List<string> continents, Dictionary<string, string> maps, decimal[] latLng, Dictionary<string, string> flag, Dictionary<string, string> coatOfArms)
        {
            Name = name;
            Capitals = capitals;
            Currencies = currencies;
            Region = region;
            SubRegion = subRegion;
            Population = population;
            Continents = continents;
            Maps = maps;
            LatLng = latLng;
            Flag = flag;
            CoatOfArms = coatOfArms;
        }
    }
}
