using Mydemenageur.BLL.Services.Interfaces;
using System.Threading.Tasks;

namespace Mydemenageur.BLL.Services
{
	public class ContactService : IContactService
	{
		public async Task<bool> ContactAsync( string from, string subject, string message )
		{
			// Send email sendgrid
			return true;
		}
	}
}
