using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Zanimljiva_Geografija_Tim14
{
    internal class CountryService
    {
        private readonly string apiUrl = "https://restcountries.com/v3.1/independent?status=true&fields=name,currencies,capital,region,subregion,languages,latlng,continents,population,flags,maps,coatOfArms";

        public async Task<List<Country>> GetCountriesAsync()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                client.Dispose();
                throw new Exception("Failed to retrieve countries. Check your internet connection and try again.");
            }

            string jsonResponse = await response.Content.ReadAsStringAsync();
            List<Country> countries = JsonConvert.DeserializeObject<List<Country>>(jsonResponse);

            client.Dispose();
            return countries;
        }
    }
}
