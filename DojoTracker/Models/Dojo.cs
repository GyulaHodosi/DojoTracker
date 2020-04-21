using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DojoTracker.Models
{
    public class Dojo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Difficulty { get; set; }
        public string Url { get; set; }
    }
}
