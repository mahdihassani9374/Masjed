namespace Varesin.Domain.DTO.Report
{
    public class ReportEditDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? WorkingGroupId { get; set; }
        public string PrimaryPicture { get; set; }
    }
}
