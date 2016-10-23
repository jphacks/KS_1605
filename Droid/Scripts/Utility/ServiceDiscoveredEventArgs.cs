using System;
using Android.Bluetooth;
namespace MemoTech.Droid
{
	public class ServiceDiscoveredEventArgs : EventArgs
	{
		public BluetoothGatt Gatt;

		public ServiceDiscoveredEventArgs() : base()
		{
		}
	}
}
