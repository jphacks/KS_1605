using System;
using Android;
using Android.Bluetooth;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MemoTech.Droid
{
	public class BluetoothManager : Java.Lang.Object, BluetoothAdapter.ILeScanCallback
	{

		protected BluetoothManager manager;
		protected BluetoothAdapter adapter;

		private List<BluetoothDevice> discoveredDevices = new List<BluetoothDevice>();
		private static BluetoothManager instance;

		public event EventHandler<DeviceDiscoveredEventArgs> DeviceDiscovered = delegate {};
		public event EventHandler<DeviceConnectionEventArgs> DeviceConnected = delegate {};
		public event EventHandler<DeviceConnectionEventArgs> DeviceDisconnected = delegate {};
		public event EventHandler<ServiceDiscoveredEventArgs> ServiceDiscovered = delegate {};

		public List<BluetoothDevice> DiscoveredDevices
		{
			get { return discoveredDevices; }
		}

		public static BluetoothManager Instance
		{
			get { return instance; } set{ instance = value; }
		}

		static BluetoothManager()
		{
			Instance = new BluetoothManager();
		}

		protected BluetoothManager()
		{
			var appContext = Android.App.Application.Context;
			manager = (BluetoothManager)appContext.GetSystemService("bluetooth");
		}

		public void OnLeScan(BluetoothDevice device, int rssi, byte[] scanRecord)
		{
			if (!DeviceExistsInDiscoveredList(device))
			{
				discoveredDevices.Add(device);
			}
			DeviceDiscovered(this, new DeviceDiscoveredEventArgs { Device = device, Rssi = rssi, ScanRecord = scanRecord });
		}

		protected bool DeviceExistsInDiscoveredList(BluetoothDevice device)
		{
			foreach (var d in discoveredDevices)
			{
				if (device.Address == d.Address)
				{
					return true;
				}
			}
			return false;
		}

		protected class GattaCallback : BluetoothGattCallback
		{
			protected BluetoothManager parent;

			public GattaCallback(BluetoothManager p)
			{
				parent = p;
			}

			public override void OnConnectionStateChange(BluetoothGatt gatt, GattStatus status, ProfileState newState)
			{
				base.OnConnectionStateChange(gatt, status, newState);

				switch (newState)
				{
					case ProfileState.Disconnected:
						parent.DeviceDisconnected(this, new DeviceConnectionEventArgs() { Device = gatt.Device });
						break;
					case ProfileState.Connecting:

						break;
					case ProfileState.Connected:

						break;
					case ProfileState.Disconnecting:

						break;
				}
			}
		}
	}
	}
}
