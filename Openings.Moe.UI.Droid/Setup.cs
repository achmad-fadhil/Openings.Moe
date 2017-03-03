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
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Platform;
using Openings.Moe.Core;
using MvvmCross.Binding.Droid;
using MvvmCross.Platform.IoC;

namespace Openings.Moe.UI.Droid
{
    class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new App();
        }

        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

            var types = CreatableTypes();

            types
                .EndingWith("Service")
                .Select(x => { System.Diagnostics.Debug.WriteLine($"IOC/Service: Registering {x}"); return x; })
                .AsInterfaces()
                .RegisterAsLazySingleton();
        }

        protected override MvxAndroidBindingBuilder CreateBindingBuilder()
        {
            return new MyAndroidBindingBuilder();
        }
    }
}