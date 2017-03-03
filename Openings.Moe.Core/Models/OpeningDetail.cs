using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Openings.Moe.Core.Models
{
    public class OpeningDetail
    {
        public bool Success { get; set; }
        public string Comment { get; set; }
        public string Filename { get; set; }
        public string Title { get; set; }
        public string Source { get; set; }
        public Song Song { get; set; }
        public string Subtitles { get; set; }
    }
}
