using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using System.Threading.Tasks;
using System.Collections.Generic;
//using MemoTech.Scripts.Utility;
using MemoTech.Droid.Scripts.Utility;
using Xamarin.Forms;

namespace MemoTech.Droid
{
	[Activity(Label = "MemoTech.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		private const int REQUEST_ENABLE_BT = 2;

		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);

			var app = new App();

			Bind(app);

			LoadApplication(app);

			//Bluetoothの許可設定
			if (Build.VERSION.SdkInt > BuildVersionCodes.Lollipop) return;
			/*
			if (BluetoothLEManager.Instance.Manager.Adapter == null || !BluetoothLEManager.Instance.Manager.Adapter.IsEnabled)
			{
				Intent enableBtIntent = new Intent(BluetoothAdapter.ActionRequestEnable);
				StartActivityForResult(enableBtIntent, REQUEST_ENABLE_BT);
			}
			*/
		}

		private void Bind(App target) 
		{
			/*
			BluetoothLEManager.BLE.StateChanged += (s, e) =>
			{
				Console.WriteLine($"The bluetooth state changed to " + e.NewState);
			};
			*/

			target.startPage.Main.Clicked += (sender, e) =>
			{
				Console.WriteLine("Main Button Clicked for Android Process");

				if (!BluetoothLEManager.Instance.IsScanning)
				{
					BluetoothLEManager.Instance.BeginScanningForDevices();
				} else {
					BluetoothLEManager.Instance.StopScanningForDevices();
				}

				if (BluetoothLEManager.Instance.Check == MemoTech.Scripts.Utility.State.Start)
				{
					BluetoothLEManager.Instance.Check = MemoTech.Scripts.Utility.State.Share;
					StartService(new Intent("com.italangmong.memotech.droid.scanactivity"));
				}
				else {
					if (MemoTech.Scripts.Utility.SaveDataUtility.CheckData(BluetoothLEManager.saveKey))
					{
						MemoTech.Scripts.Utility.SaveDataUtility.LoadArray<List<string>>(BluetoothLEManager.saveKey).ForEach(_ => Console.WriteLine("Load : " + _));
						var dic = MemoTech.Scripts.Utility.ConnectLog.CountOrderSort<string>(BluetoothLEManager.saveKey);
						foreach (var a in dic.Keys)
						{
							Console.WriteLine("Sort : " + a);
						}
						foreach (var a in dic.Values)
						{
							Console.WriteLine("Sort : " + a);
						}
						MemoTech.Scripts.Utility.ConnectLog.FriendCheck(MemoTech.Scripts.Utility.ConnectLog.CountOrderSort<string>(BluetoothLEManager.saveKey)).ForEach(_ => Console.WriteLine("Friend : " + _));
					}

				}

				/*
				Console.WriteLine("State " + BluetoothLEManager.BLE.State);
				var adapter = BluetoothLEManager.Adapter;
				adapter.DeviceDiscovered += (s, a) => Console.WriteLine("Device : " + a.Device.Name);
				await adapter.StartScanningForDevicesAsync();
				*/
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
