using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Openings.Moe.Core.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Openings.Moe.Core.Services
{
    public class OpeningService : IOpeningService
    {

        public async Task<List<Opening>> RetrieveAllOpenings()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://openings.moe/");
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                );
                var response = await client.GetAsync("api/list.php");
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Opening>>(content).ToList();              
            }
        }

        public async Task<OpeningDetail> RetrieveOpeningDetail(string filename)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://openings.moe/");
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                );
                var response = await client.GetAsync($"api/details.php?file={filename}");
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<OpeningDetail>(content);                
            }
        }
    }
}
