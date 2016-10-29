using System;
using System.Diagnostics.Contracts;
using Android.App;
using Android.Content;
using Android.OS;
using Java.Lang;
using Xamarin.Android;
using MemoTech.Droid.Scripts.Utility;

namespace MemoTech.Droid
{
	[Service]
	public class ScanActivity : Service
	{

		public override IBinder OnBind(Intent intent)
		{
			return null;
			//throw new NotImplementedException();
		}

		public override StartCommandResult OnStartCommand(Android.Content.Intent intent, StartCommandFlags flags, int startId)
		{
			MemoTech.Scripts.Utility.ConnectLog.ConnectionCount = 0;
			var t = new Thread(() =>
			{
				while (true)
				{
					Thread.Sleep(60000);
					BluetoothLEManager.Instance.BeginScanningForDevices();
					Thread.Sleep(5000);
					BluetoothLEManager.Instance.StopScanningForDevices();
					MemoTech.Scripts.Utility.ConnectLog.ConnectionCount += 1;
					Console.WriteLine("Scan!");
					if (BluetoothLEManager.Instance.Check == MemoTech.Scripts.Utility.State.Share)
					{
						BluetoothLEManager.Instance.Check = MemoTech.Scripts.Utility.State.Start;
						StopSelf();
					}
				}
			});


			t.Start();

			return StartCommandResult.Sticky;
		}
	}
}
