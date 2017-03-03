using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Openings.Moe.Core.Services;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;

namespace Openings.Moe.UI.Droid.Services
{
    class DialogService : IDialogService
    {

        protected Activity CurrentActivity
        {
            get { return Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity; }
        }

        public void CloseDialog(IBlockingDialog dialog)
        {
            var blockingDialog = dialog as BlockingDialog;
            CurrentActivity.RunOnUiThread(() => blockingDialog.Close());
        }

        public async Task<IBlockingDialog> showLoadingDialog(string message)
        {
            AlertDialog dialog =
                await CurrentActivity.WaitOnUiThread(
                    () => ProgressDialog.Show(CurrentActivity, "", message, true, false));

            return new BlockingDialog(dialog);
        }
    }

    public class BlockingDialog : IBlockingDialog
    {
        private WeakReference Instance { get; set; }

        public BlockingDialog(AlertDialog instance)
        {
            Instance = new WeakReference(instance);
        }
        public void Close()
        {
            ((AlertDialog)Instance.Target).Dismiss();
        }
    }

    static class UIUtil
    {
        public static Task<T> WaitOnUiThread<T>(this Android.App.Activity act, Func<T> f)
        {
            TaskCompletionSource<T> tcs = new TaskCompletionSource<T>();
            act.RunOnUiThread(() =>
            {
                T result = default(T);
                try
                {
                    result = f();
                }
                catch (Exception e)
                {
                    // Ignore it. But it should be impossible for now
                }
                tcs.SetResult(result);
            });
            return tcs.Task;
        }
    }
}