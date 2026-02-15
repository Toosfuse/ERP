using System;
using System.Threading.Tasks;
using Kavenegar;

namespace ERP.Services
{
    public class SmsService : ISmsService
    {
        public async Task<bool> SendVerificationCode(string phoneNumber, string code)
        {
            try
            {
                //// پیادهسازی کاوهنگار
                //var api = new KavenegarApi("5851516355506631746F436F54666D30474D35736C392F5A3270794E2F62696830364B4879615A2B4365773D");
                
                //var message = $"سلام\nکد تایید ورود به سیستم چت:\n{code}\n\nاعتبار: 5 دقیقه\nتوس فیوز";
                //api.Send("2000660110", phoneNumber, message);

         
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SMS Error: {ex.Message}");
                return false;
            }
        }
    }
}
