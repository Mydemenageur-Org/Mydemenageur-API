using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mydemenageur.DAL.Models.Users;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using IContactService = Mydemenageur.BLL.Services.Interfaces.IContactService;

namespace Mydemenageur.API.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController( IContactService contactService )
        {
            _contactService = contactService;
        }

        /// <summary>
        /// Send a contact mail
        /// </summary>
        /// <param name="from"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <response code="400">Email couldn't be sent</response>
        /// <response code="200">Email sent successfully</response>
        /// <returns></returns>
        [HttpPost( "contact" )]
        [ProducesResponseType( typeof( MyDemenageurUser ), StatusCodes.Status200OK )]
        public async Task<ActionResult<MyDemenageurUser>> Contact( string from, string subject, string message )
        {
            if (!await _contactService.ContactAsync( from, subject, message ))
            {
                return BadRequest( "Email couldn't be sent" );
            }

            return Ok("Email sent successfully");
        }
    }
}