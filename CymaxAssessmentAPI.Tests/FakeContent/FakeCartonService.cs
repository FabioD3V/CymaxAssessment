using CymaxAssessmentAPI.Facade;
using CymaxAssessmentAPI.Services;
using System;
using System.Threading.Tasks;

namespace CymaxAssessmentAPI.Tests.FakeContent
{
    public class FakeCartonService : ICartonService
    {
        public async Task<double> GetCartonOffer(IBaseClientRequest clientRequest)
        {
            Random random = new Random();
            var fakeTotal = Math.Round(random.NextDouble() * (100 - 1) + 1, 2);
            return fakeTotal;
        }
    }
}
