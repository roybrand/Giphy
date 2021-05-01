using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThirdPartyLib.Models;

namespace ThirdPartyLib.Services
{
    public interface IGiphyServices
    {
        public Task<IEnumerable<GiphyModel>> GetGifsBySearchCriteria(string searchCriteria);
    }
}
