using ThirdPartyLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace ThirdPartyLib.Giphy
{
    public interface ISearchGifs
    {
        Task<IEnumerable<GiphyModel>> GetGifsBySearchCriteria(string s);
    }
}
