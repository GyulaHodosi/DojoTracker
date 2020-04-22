using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DojoTracker.Models
{
    public class Solution
    {
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        [ForeignKey(nameof(Dojo))]
        public int DojoId { get; set; }
        public string Code { get; set; }
    }
}
