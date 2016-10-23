using System;
using Android.Bluetooth;

namespace MemoTech.Droid
{
	public class DeviceConnectionEventArgs : EventArgs
	{
		public BluetoothDevice Device;

		public DeviceConnectionEventArgs() : base()
		{
		}
	}
}
