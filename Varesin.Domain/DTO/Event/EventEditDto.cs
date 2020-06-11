using System;

namespace Varesin.Domain.DTO.Event
{
    public class EventEditDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool MultiDay { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? Date { get; set; }
        public string PrimaryPicture { get; set; }
        public string Time { get; set; }
    }
}
