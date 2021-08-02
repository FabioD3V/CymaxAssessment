using CymaxAssessmentAPI.Facade;
using CymaxAssessmentAPI.Models;
using CymaxAssessmentAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CymaxAssessmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartonController : ControllerBase
    {
        private readonly ICartonService _cartonService;

        public CartonController(ICartonService cartonService)
        {
            _cartonService = cartonService;
        }

        // Please notice that I've made use of the requested APIs found in the assessment for the sake of simplicity
        // and easier correlation to what API (1, 2 or 3) the endpoint refers to.
        [HttpPost("ClientRequestFromCompanyAPI1")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public ActionResult<double> GetCartonOfferAPI1([FromBody] ClientRequestAPI1 clientRequest)
        {
            return ProcessedRequest(clientRequest);
        }

        [HttpPost("ClientRequestFromCompanyAPI2")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public ActionResult<double> GetCartonOfferAPI2([FromBody] ClientRequestAPI2 clientRequest)
        {
            return ProcessedRequest(clientRequest);
        }

        [HttpPost("ClientRequestFromCompanyAPI3")]
        [Consumes("application/xml")]
        [Produces("application/xml")]
        public ActionResult<double> GetCartonOfferAPI3(ClientRequestAPI3 clientRequest)
        {
            return ProcessedRequest(clientRequest);
        }

        private ActionResult<double> ProcessedRequest(IBaseClientRequest clientRequest)
        {
            if (ModelState.IsValid)
            {
                var result = _cartonService.GetCartonOffer(clientRequest);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result.Result);
            }

            if (ModelState.IsValid) return Ok();
            return BadRequest();
        }
    }
}
