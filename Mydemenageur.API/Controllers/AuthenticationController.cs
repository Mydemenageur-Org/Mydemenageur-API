using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mydemenageur.DAL.Models.Users;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using IAuthenticationService = Mydemenageur.BLL.Services.Interfaces.IAuthenticationService;

namespace Mydemenageur.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Login a user to the API
        /// </summary>
        /// <remarks>The username can be both the actual username or the user's email</remarks>
        /// <param name="loginModel"></param>
        /// <response code="400">The user doesn't exist or the password doesn't match</response>
        /// <response code="200">Return the logged user with valid token</response>
        /// <returns></returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(MyDemenageurUser), StatusCodes.Status200OK)]
        public async Task<ActionResult<MyDemenageurUser>> Login([FromBody] LoginModel loginModel)
        {
            MyDemenageurUser user = await _authenticationService.LoginAsync(loginModel.Email, loginModel.Password);

            if (user == null)
            {
                return BadRequest("The username or password is incorrect");
            }

            return Ok(user);
        }

        /// <summary>
        /// Register the new user to the database 
        /// </summary>
        /// <param name="registerModel"></param>
        /// <response code="400">There was one or more errors during registration validation</response>
        /// <response code="200">Return the newly registrated user's id</response>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<ActionResult<string>> Register([FromBody] RegisterModel registerModel)
        {
            MyDemenageurUser user;

            try
            {
                user = await _authenticationService.RegisterAsync(registerModel);
            }
            catch (Exception e)
            {
                return BadRequest($"Error during the user registration: {e.Message}");
            }


             return Content("Félicitation ton compte a été crée");
        }

        /// <summary>
        /// Register the new user to the database 
        /// </summary>
        /// <param name="createOrConnectUserModel"></param>
        /// <response code="400">There was one or more errors during registration validation</response>
        /// <response code="200">Return the newly registrated user's id</response>
        /// <returns></returns>
        [HttpPost("login-firebase")]
        public async Task<ActionResult<MyDemenageurUser>> TokenizeFirebaseUser([FromBody] FirebaseUserModel createOrConnectUserModel)
        {
            MyDemenageurUser tokenFirebaseUser;

            try
            {
                tokenFirebaseUser = await _authenticationService.TokenizeFirebaseUser(createOrConnectUserModel);
            } 
            catch(Exception e)
            {
                return BadRequest($"Error during the user registration: {e.Message}");
            }

            return Ok(tokenFirebaseUser);
        }


        /// <summary>
        /// Update user's password
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <response code="400">There was one or more errors during the password's udpate process</response>
        /// <response code="200">Return the id of the user that changed his password</response>
        /// <returns></returns>
        [HttpPost("change-password/{id:length(24)}")]
        public async Task<ActionResult<string>> UpdatePassword(string id, [FromBody] UpdatePassword password)
        {   
            try
            {
                var identifier = await _authenticationService.UpdatePassword(id, password.password);
            } catch(Exception error)
            {
                return BadRequest($"Error during the user's password update: {error.Message}");
            }

            return NoContent();
        }

        /// <summary>
        /// Disconnect the user
        /// </summary>
        /// <param name="id"></param>
        /// <response code="400">There was one or more errors during the disconnection process</response>
        /// <response code="200">Return the id of the user that got disconnected</response>
        /// <returns></returns>
        [HttpPost("logout/{id:length(24)}")]
        public async Task<ActionResult<string>> Logout(string id)
        {
            string identifier = "";
            try
            {
                identifier = await _authenticationService.LogoutAsync(id);
            }
            catch(Exception e)
            {
                return BadRequest($"Error during the user logout: {e.Message}");
            }

            return NoContent();
        }
        
        /// <summary>
        /// Verify the token validity
        /// </summary>
        /// <param name="token"></param>
        /// <response code="400">Token is invalid, user is not authenticated</response>
        /// <response code="200">Token is valid, return the user id</response>
        /// <returns></returns>
        [HttpPost("valid")]
        public async Task<ActionResult<string>> TokenValidity()
        {
            string id;

            try
            {
                id = await _authenticationService.TokenValidity(Request.Headers["token"]);
            } 
            catch(Exception e)
            {
                return BadRequest($"Token invalid, couldn't find any users: {e.Message}");
            }

            return Ok(id);
        }
        
        /// <summary>
        /// Generate a forgotten password token
        /// </summary>
        /// <param name="forgotPassword"></param>
        /// <returns></returns>
        [HttpPost("forgot-password")]
        public async Task<ActionResult<CallbackForgotPassword>> ForgotPassword([FromBody] ForgotPassword forgotPassword)
        {
            try
            {
                return await _authenticationService.ForgotPassword(forgotPassword);
            }
            catch(Exception e)
            {
                return BadRequest($"Error during the generation of forgot token: {e.Message}");
            }

            return NoContent();
        }
        
        /// <summary>
        /// Reset forgotten password
        /// </summary>
        /// <param name="resetPassword"></param>
        /// <returns></returns>
        [HttpPut("forgot-password")]
        public async Task<ActionResult<string>> ResetPassword([FromBody] ResetPassword resetPassword)
        {
            try
            {
                return await _authenticationService.ResetPassword(resetPassword);
            }
            catch(Exception e)
            {
                return BadRequest($"Error during the password reset: {e.Message}");
            }

            return NoContent();
        }
    }
}
