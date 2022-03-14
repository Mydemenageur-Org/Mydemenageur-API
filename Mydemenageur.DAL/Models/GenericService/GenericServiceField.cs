using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.DAL.Models.GenericService
{
    public class Attributes
    {
        public string Label { get; set; }
        public string Value { get; set; }
    }
    public class GenericServiceField
    {
        [Required]
        public string Type { get; set; }
        public string Value { get; set; }
        public string Placeholder { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Label { get; set; }
        public Attributes[] Attributes { get; set; }
        public bool IsDone { get; set; } = false;
    }
}
