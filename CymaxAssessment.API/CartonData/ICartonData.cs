using CymaxAssessment.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CymaxAssessment.API.CartonData
{
    public interface ICartonData
    {
        List<Carton> GetCartons();
    }
}
