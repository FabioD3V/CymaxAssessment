using CymaxAssessmentAPI.BaseModels;
using CymaxAssessmentAPI.Repositories;
using System.Threading.Tasks;

namespace CymaxAssessmentAPI.Services
{
    public class CartonService : ICartonService
    {
        private readonly ICartonRepository _cartonRepository;

        public CartonService(ICartonRepository cartonRepository)
        {
            _cartonRepository = cartonRepository;
        }

        public async Task<double> GetCartonOffer(IBaseClientRequest clientRequest)
        {
            var baseClientRequest = clientRequest.GetMappedRequest();

            var cartons = await _cartonRepository.GetCartons();
            double total = 0;

            foreach (var dimension in baseClientRequest.CartonDimensions)
            {
                total += cartons.Find(c => c.Dimension == dimension).Price;
            }

            return total;
        }
    }
}
