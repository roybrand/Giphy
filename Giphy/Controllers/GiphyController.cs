using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThirdPartyLib.Services;

namespace Giphy.Controllers
{
    public class GiphyController : Controller
    {
        private readonly IGiphyServices _giphyServices;

        public GiphyController(IGiphyServices giphyServices)
        {
            _giphyServices = giphyServices;
        }

        [HttpGet]
        [Route("v1/giphy/search/{searchCritera}")]
        public async Task<IActionResult> SearchGif(string searchCritera)
        {
            var result = await _giphyServices.GetGifsBySearchCriteria(searchCritera);

            return Ok(result);
        }
    }
}
