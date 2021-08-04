using CymaxAssessmentAPI.BaseModels;
using CymaxAssessmentAPI.Models;
using CymaxAssessmentAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CymaxAssessmentAPI.Controllers
{
    /// <summary>
    /// This is the controller for the requested assignment.
    /// For the sake of simplicity, I've named the endpoinst in a way we can easily
    /// connect it to it's requirement(e.g. for the API1 in the requirment I created 
    /// the ClientRequestFromCompanyAPI1-GetCartonOfferAPI1 endpoint.
    /// 
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CartonController : ControllerBase
    {
        private readonly ICartonService _cartonService;
        private readonly IAuthenticationService _authenticationService;

        // Use DI (dependency injection) for both Carton and Authentication services.
        public CartonController(ICartonService cartonService, IAuthenticationService authenticationService)
        {
            _cartonService = cartonService;
            _authenticationService = authenticationService;
        }
        
        [HttpPost("ClientRequestFromCompanyAPI1")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<double>> GetCartonOfferAPI1([FromBody] ClientRequestAPI1 clientRequest)
        {
            return await ProcessedRequest(clientRequest);
        }

        [HttpPost("ClientRequestFromCompanyAPI2")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<double>> GetCartonOfferAPI2([FromBody] ClientRequestAPI2 clientRequest)
        {
            return await ProcessedRequest(clientRequest);
        }

        [HttpPost("ClientRequestFromCompanyAPI3")]
        [Consumes("application/xml")]
        [Produces("application/xml")]
        public async Task<ActionResult<ResponseAPI3>> GetCartonOfferAPI3(ClientRequestAPI3 clientRequest)
        {
            var content = await ProcessedRequest(clientRequest);
            return GetSerializedResponse(content);
        }

        /// <summary>
        /// Method to serialize the response for the API3, which process with XML.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private static ActionResult<ResponseAPI3> GetSerializedResponse(ActionResult<double> content)
        {
            var contentResult = (OkObjectResult)content.Result;
            var contentValue = contentResult.Value;

            ResponseAPI3 response = new ResponseAPI3() { Quote = contentValue.ToString() };

            using (MemoryStream ms = new MemoryStream())
            {
                using (XmlTextWriter tw = new XmlTextWriter(ms, Encoding.UTF8))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(response.GetType());
                    tw.Formatting = Formatting.Indented;
                    xmlSerializer.Serialize(tw, response);
                    return response;
                }
            }
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] UserCredentials userCredentials)
        {
            var token = _authenticationService.Authenticate(userCredentials);

            if (token == null)
            {
                return Unauthorized(userCredentials.Username);
            }

            return Ok(token);
        }

        private async Task<ActionResult<double>> ProcessedRequest(IBaseClientRequest clientRequest)
        {
            if (ModelState.IsValid)
            {
                var result = await _cartonService.GetCartonOffer(clientRequest);

                if (result == 0)
                {
                    return NotFound(clientRequest);
                }

                return Ok(result);
            }

            if (ModelState.IsValid) return Ok();
            return BadRequest(clientRequest);
        }
    }
}
