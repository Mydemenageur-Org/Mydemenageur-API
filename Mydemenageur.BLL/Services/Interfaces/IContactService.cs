using System.Threading.Tasks;

namespace Mydemenageur.BLL.Services.Interfaces
{
    public interface IContactService
    {
        Task<bool> SendAsync(string from, string subject, string plainTextContent, string htmlContent);
    }
}
