
using System;

namespace MunicipalServicesApp
{
    public class Issue
    {
        public int RequestId { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string MediaPath { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
