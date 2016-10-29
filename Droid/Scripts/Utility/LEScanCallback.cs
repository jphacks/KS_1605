using System;
using Android;
using Android.Bluetooth;
using Android.Bluetooth.LE;


namespace MemoTech.Droid.Scripts.Utility
{
	public class LEScanCallback : ScanCallback
	{

		public LEScanCallback()
		{
			
		}

		public override void OnScanResult(ScanCallbackType callbackType, ScanResult result)
		{
			var device = result.Device;
			//if (device.Type != BluetoothDeviceType.Le) return;
			Console.WriteLine("OnScanResult : " + result.Rssi + " " + device.Name + " " + device + " " + device.Address + " " + device.BondState + " " + device.Type + " " + callbackType + " " + device.PeerReference);
			BluetoothLEManager.Instance.DiscoveredDevices.Add(device.ToString());
		}
	}
}
