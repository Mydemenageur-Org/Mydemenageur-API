using Mydemenageur.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Services
{
    public class MydemenageurSettings : IMydemenageurSettings
    {
        public string ApiSecret { get; set; }
    }
}
