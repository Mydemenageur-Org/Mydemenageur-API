using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.DAL.Models.Reviews
{
    public abstract class Schema
    {
        public long ReviewNumber { get; set; }
        public long ReviewRate { get; set; }
    }
}
