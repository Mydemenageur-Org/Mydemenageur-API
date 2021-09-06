using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.Users;
using Mydemenageur.API.Services.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Mydemenageur.API.DP.Interface;

namespace Mydemenageur.API.Services
{
    public class HelpService
    {
        private readonly IDPHelp _dpHelp;
        private readonly IMongoCollection<Help> _helps;
        
        public HelpService(IDPHelp dpHelp)
        {
            _dpHelp = dpHelp;
            _helps = _dpHelp.Obtain();
        }




    }
}
