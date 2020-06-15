using Remotion.Linq.Parsing.ExpressionVisitors.MemberBindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Varesin.Mvc.Models.Instagram
{
    public class CreateInstaPostViewModel
    {
        public List<string> FileIds { get; set; }
        public List<string> Tags { get; set; }
        public string Caption { get; set; }
        public bool DisableComment { get; set; }
        public string Type { get; set; }
    }
}
