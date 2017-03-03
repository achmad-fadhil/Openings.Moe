using Openings.Moe.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Openings.Moe.Core.Services
{
    interface IOpeningService
    {
        Task<List<Opening>> RetrieveAllOpenings();

        Task<OpeningDetail> RetrieveOpeningDetail(string filename);
    }
}
