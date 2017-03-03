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
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Binding.Droid;

namespace Openings.Moe.UI.Droid
{
    class MyAndroidBindingBuilder : MvxAndroidBindingBuilder
    {
        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            base.FillTargetFactories(registry);

            registry.RegisterCustomBindingFactory<VideoView>("VideoUri",
                videoView => new MvxVideoViewUriTargetBinding(videoView));
        }
    }
}