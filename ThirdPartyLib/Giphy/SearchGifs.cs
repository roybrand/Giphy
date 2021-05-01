using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ThirdPartyLib.Models;

namespace ThirdPartyLib.Giphy
{
    public class SearchGifs : ISearchGifs
    {
        private string _searchCriteria;
        const string _giphyKey = "SEQsGZ0mwuKaJQg8DuE0ZFMYKMqRi7Aw";
        //private List<Uri> requests = new List<Uri>();
        private Dictionary<string, Uri> requests = new Dictionary<string, Uri>();
        private readonly IMemoryCache _cache;

        public SearchGifs(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<IEnumerable<GiphyModel>> GetGifsBySearchCriteria(string searchCritera)
        {
            _searchCriteria = searchCritera;            

            BuildRequests();            

            return  await GetGyphsAsync ();         

        }

        private Dictionary<string, Uri> BuildRequests()
        {

            foreach (string c in SplitCriteria())
            {
                var url = new Uri($"http://api.giphy.com/v1/gifs/search?api_key={_giphyKey}&q={c}&limit=80");

                requests.Add(c, url);
            }

            return requests;

        }

        private List<string> SplitCriteria()
        {
            string[] words = _searchCriteria.Split(",");

            List<string> list_lines = new List<string>(words);

            return list_lines;
        }

        

        private async Task<IEnumerable<GiphyModel>> GetGyphsAsync()
        {
            var tasks = requests.Select(async request =>   
            {
                var giphyModel = await _cache.GetOrCreateAsync(request.Key, async (entry) =>
                 {

                     using (var client = new HttpClient())
                     {
                         var response = await client.GetAsync(request.Value.AbsoluteUri);

                         string json;
                         using (var content = response.Content)
                         {
                             json = await content.ReadAsStringAsync();
                         }

                         return JsonConvert.DeserializeObject<GiphyModel>(json);
                     }
                 });
                return giphyModel;
            });

            var giphyArray = await Task.WhenAll(tasks);

            return giphyArray;
        }       

    }
}
