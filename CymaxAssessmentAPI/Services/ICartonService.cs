using CymaxAssessmentAPI.Facade;
using System.Threading.Tasks;

namespace CymaxAssessmentAPI.Services
{
    public interface ICartonService
    {
        Task<double> GetCartonOffer(IBaseClientRequest clientRequest);
    }
}
