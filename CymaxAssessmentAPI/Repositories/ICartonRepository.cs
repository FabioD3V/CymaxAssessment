using CymaxAssessmentAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CymaxAssessmentAPI.Repositories
{
    public interface ICartonRepository
    {
        Task<List<Carton>> GetCartons();
    }
}
