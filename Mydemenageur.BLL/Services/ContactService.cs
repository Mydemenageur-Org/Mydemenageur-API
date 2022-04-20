using System;
using System.Diagnostics;
using System.Net;
using Mydemenageur.BLL.Services.Interfaces;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Mydemenageur.BLL.Services
{
	public class ContactService : IContactService
	{
		public async Task<bool> SendAsync( string from, string subject, string plainTextContent, string htmlContent )
		{
			var client = new SendGridClient(Environment.GetEnvironmentVariable("SENDGRID_APIKEY"));
			var message = MailHelper.CreateSingleEmail(MailHelper.StringToEmailAddress(Environment.GetEnvironmentVariable("SENDGRID_EMAIL")),
				MailHelper.StringToEmailAddress(from), $"Contact: {subject}", plainTextContent, htmlContent);
			var response = await client.SendEmailAsync(message);
			Debug.Write(response.StatusCode);
			return response.StatusCode == HttpStatusCode.Accepted;
		}
	}
}
