using System;
using System.Collections.Generic;

namespace DojoTracker.Models
{
    public class UserStatistics
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public string Email { get; set; }
        public IEnumerable<int> CompletedDojoIds { get; set; }
        public int Score { get; set; }
        public DateTime LastCompleted { get; set; }
        
    }
}