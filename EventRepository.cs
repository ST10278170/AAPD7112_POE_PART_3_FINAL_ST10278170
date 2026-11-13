
using System;
using System.Collections.Generic;

namespace MunicipalServicesApp
{
    public static class EventRepository
    {
        public static List<EventItem> Events { get; } = new List<EventItem>
        {
            new EventItem
            {
                Title = "Community Clean-Up Day",
                Category = "Community",
                EventDate = new DateTime(2026, 12, 5),
                Description = "Residents gather to clean parks and public spaces.",
                Priority = 2
            },

            new EventItem
            {
                Title = "Community Health Fair",
                Category = "Health",
                EventDate = new DateTime(2026, 12, 10),
                Description = "Free health screenings and wellness information.",
                Priority = 1
            },

            new EventItem
            {
                Title = "Youth Soccer Tournament",
                Category = "Sports",
                EventDate = new DateTime(2026, 12, 15),
                Description = "Local youth teams compete in a community tournament.",
                Priority = 3
            },

            new EventItem
            {
                Title = "Water Maintenance Notice",
                Category = "Infrastructure",
                EventDate = new DateTime(2026, 12, 20),
                Description = "Scheduled maintenance affecting water supply.",
                Priority = 1
            },

            new EventItem
            {
                Title = "Fire Awareness Workshop",
                Category = "Safety",
                EventDate = new DateTime(2027, 1, 10),
                Description = "Learn fire prevention and emergency procedures.",
                Priority = 2
            },

            new EventItem
            {
                Title = "Neighbourhood Watch Meeting",
                Category = "Safety",
                EventDate = new DateTime(2027, 1, 17),
                Description = "Monthly community safety discussion.",
                Priority = 2
            }
        };
    }
}
