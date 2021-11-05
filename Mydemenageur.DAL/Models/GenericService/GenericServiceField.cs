using System;
using System.Collections.Generic;
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
        public string FieldId { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string Placeholder { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public Attributes[] Attributes { get; set; }
    }
}
