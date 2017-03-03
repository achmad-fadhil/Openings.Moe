using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.Droid.Target;
using Android.Net;

namespace Openings.Moe.UI.Droid
{
    class MvxVideoViewUriTargetBinding : MvxAndroidTargetBinding
    {
        public MvxVideoViewUriTargetBinding(object target) : base(target)
        {
        }

        public override Type TargetType
        {
            get
            {
                return typeof(string);
            }
        }

        protected override void SetValueImpl(object target, object value)
        {
            var videoView = target as VideoView;
            videoView.SetVideoURI(Android.Net.Uri.Parse((string)value));
        }
    }
}