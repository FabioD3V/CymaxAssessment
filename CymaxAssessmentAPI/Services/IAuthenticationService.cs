using CymaxAssessmentAPI.Models;

namespace CymaxAssessmentAPI.Services
{
    public interface IAuthenticationService
    {
        string Authenticate(UserCredentials userCredentials);
    }
}
