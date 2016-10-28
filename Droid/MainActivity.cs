using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Bluetooth;

using System.Threading.Tasks;
using System.Collections.Generic;
using MemoTech.Droid.Scripts.Utility;
using MemoTech.Scripts.Utility;

namespace MemoTech.Droid
{
	[Activity(Label = "MemoTech.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);

			var app = new App();

			Bind(app);

			LoadApplication(app);
		}

		private void Bind(App target) 
		{
			target.startPage.Main.Clicked += (sender, e) =>
			{
				Console.WriteLine("Main Button Clicked for Android Process");
				Console.WriteLine(BluetoothLEManager.Instance);
				if (!BluetoothLEManager.Instance.IsScanning)
				{
					BluetoothLEManager.Instance.BeginScanningForDevices();
				} else {
					BluetoothLEManager.Instance.StopScanningForDevices();
				}
			};

			target.startPage.Stop.Clicked += (sender, e) =>
			{
				StopScanning();
			};
		}

		private void StopScanning() 
		{
			new Task(() => {
				if (BluetoothLEManager.Instance.IsScanning)
				{
					BluetoothLEManager.Instance.StopScanningForDevices();
				}
			}).Start();
		}
	}
}
