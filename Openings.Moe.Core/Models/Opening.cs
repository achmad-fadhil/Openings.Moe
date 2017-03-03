using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Openings.Moe.Core.Models
{
    public class Opening
    {
        public string Title { get; set; }
        public string Source { get; set; }
        public string File { get; set; }
        public Song Song { get; set; }
    }
}
