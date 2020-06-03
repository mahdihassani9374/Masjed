using System;
using System.Collections.Generic;
using System.Text;
using Varesin.Domain.Enumeration;

namespace Varesin.Domain.DTO.Report
{
    public class ReportFileDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public FileType Type { get; set; }
        public string FileName { get; set; }
        public long Length { get; set; }
        public int CountDownload { get; set; }
        public ReportDto Report { get; set; }
        public int ReportId { get; set; }
    }
}
