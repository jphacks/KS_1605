using System;
using System.Diagnostics;
using Xamarin.Forms;
using MvvmCross.Platform;
using MvvmCross.Plugins.BLE;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;

namespace MemoTech.Scripts.Utility
{
	public class BluetoothLEManager
	{
		private static IBluetoothLE ble = CrossBluetoothLE.Current;
		private static IAdapter adapter = CrossBluetoothLE.Current.Adapter;

		public static IBluetoothLE BLE
		{
			get { return ble; }
		}

		public static IAdapter Adapter
		{
			get { return adapter; }
		}
		
	}
}
