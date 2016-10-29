using System;
using Android.Bluetooth;

namespace MemoTech.Droid
{
	public class DeviceDiscoveredEventArgs : EventArgs
	{
		public BluetoothDevice Device;
		public int Rssi;
		public byte[] ScanRecord;

		public DeviceDiscoveredEventArgs() : base()
		{
		}
	}
}
