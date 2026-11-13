using System;

namespace MunicipalServicesApp
{
    public class ServiceRequest
    {
        public int RequestId { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public string Location { get; set; }

        public string Status { get; set; }

        public DateTime DateSubmitted { get; set; }

        public ServiceRequest(
            int requestId,
            string description,
            string category,
            string location,
            string status,
            DateTime dateSubmitted)
        {
            RequestId = requestId;
            Description = description;
            Category = category;
            Location = location;
            Status = status;
            DateSubmitted = dateSubmitted;
        }

        public override string ToString()
        {
            return $"#{RequestId} - {Category} - {Status}";
        }
    }
}