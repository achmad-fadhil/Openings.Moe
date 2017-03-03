using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Views;

namespace Openings.Moe.UI.Droid.Views
{
    [Activity(Label = "Openings.Moe", ScreenOrientation = ScreenOrientation.Portrait)]
    class OpeningListView : MvxActivity
    {
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.View_OpeningList);
        }
    }
}