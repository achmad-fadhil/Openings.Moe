using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Openings.Moe.Core.Services
{
    public interface IDialogService
    {
        Task<IBlockingDialog> showLoadingDialog(string message);
        void CloseDialog(IBlockingDialog dialog);
    }

    public interface IBlockingDialog
    {
        void Close();
    }
}
