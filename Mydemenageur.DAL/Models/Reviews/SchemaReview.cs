using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.DAL.Models.Reviews
{
    public class SchemaReview : Schema
    {
        public string VeryGoodReview { get; set; }
        public string GoodReview { get; set; }
        public string MediumReview { get; set; }
        public string BadReview { get; set; }
    }
}
