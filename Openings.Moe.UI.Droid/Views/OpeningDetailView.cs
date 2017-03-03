using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Views;

namespace Openings.Moe.UI.Droid.Views
{
    [Activity(Label = "Opening Detail", ScreenOrientation = ScreenOrientation.Portrait)]
    class OpeningDetailView : MvxActivity
    {
        private VideoView v;

        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.View_OpeningDetail);
            v = FindViewById<VideoView>(Resource.Id.videoView);
            v.Start();
        }

        protected override void OnDestroy()
        {
            v.StopPlayback();
            base.OnDestroy();
        }
    }
}