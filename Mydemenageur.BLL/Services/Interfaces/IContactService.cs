
using Mydemenageur.DAL.Models.Users;
using System.Threading.Tasks;

namespace Mydemenageur.BLL.Services.Interfaces
{
    public interface IContactService
    {
        Task<bool> ContactAsync(string from, string subject, string message);
    }
}
