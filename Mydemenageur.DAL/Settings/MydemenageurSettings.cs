using Mydemenageur.DAL.Settings.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.DAL.Settings
{
    public class MydemenageurSettings : IMydemenageurSettings
    {
        public string ApiSecret { get; set; }
    }
}
