using System;
using System.Collections.Generic;
using System.Linq;
using EverydayHabit;
using Foundation;
using UIKit;
using Xamarin.Forms;

namespace TabbedPageWithNavigationPage.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();
			FormsMaterial.Init();
			LoadApplication(new App());
			return base.FinishedLaunching(app, options);
		}
	}
}

