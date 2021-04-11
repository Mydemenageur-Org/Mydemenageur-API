using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.API.Services.Interfaces
{
    public interface IMydemenageurSettings
    {
        string ApiSecret { get; set; }
    }
}
