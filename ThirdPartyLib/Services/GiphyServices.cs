using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThirdPartyLib.Giphy;
using ThirdPartyLib.Models;

namespace ThirdPartyLib.Services
{
    public class GiphyServices : IGiphyServices
    {
        private static ISearchGifs _searchGifs;
        public GiphyServices(ISearchGifs searchGifs)
        {
            _searchGifs = searchGifs;
        }

        public async Task<IEnumerable<GiphyModel>> GetGifsBySearchCriteria(string searchCriteria)
        {
            return await _searchGifs.GetGifsBySearchCriteria(searchCriteria);
        }
    }
}
