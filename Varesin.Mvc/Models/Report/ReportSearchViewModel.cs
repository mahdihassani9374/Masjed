namespace Varesin.Mvc.Models.Report
{
    public class ReportSearchViewModel
    {
        public string Title { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
        public int? WorkingGroupId { get; set; }
    }
}
