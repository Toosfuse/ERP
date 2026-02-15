using System.Threading.Tasks;

namespace ERP.Services
{
    public interface ISmsService
    {
        Task<bool> SendVerificationCode(string phoneNumber, string code);
    }
}
