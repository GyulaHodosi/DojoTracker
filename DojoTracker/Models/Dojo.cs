using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DojoTracker.Models
{
    public class Dojo
    {   
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Difficulty { get; set; }
        public string Url { get; set; }
        [NotMapped]
        public bool IsDone { get; set; }
    }
}
