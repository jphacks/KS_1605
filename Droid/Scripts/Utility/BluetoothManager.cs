using System;
using Android;
using Android.Bluetooth;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemoTech.Droid
{
	public class BluetoothLEManager : Java.Lang.Object, BluetoothAdapter.ILeScanCallback
	{

		private BluetoothManager manager;
		//bluetoothの接続に必要
		private BluetoothAdapter adapter;
		//gatt = サーバーのようなもの : Callbackどのようなサービスを使うのか決定して返す
		private GattaCallback gattCallback;
		private Dictionary<BluetoothDevice, BluetoothGatt> connectedDevices = new Dictionary<BluetoothDevice, BluetoothGatt>();
		private bool isScanning = false;
		private const int scanTimeout = 10000;
		private List<BluetoothDevice> discoveredDevices = new List<BluetoothDevice>();
		//シングルトン
		private static BluetoothLEManager instance;

		//CustomEventのdelegate
		public event EventHandler ScanTimeoutElapsed = delegate{};
		public event EventHandler<DeviceDiscoveredEventArgs> DeviceDiscovered = delegate {};
		public event EventHandler<DeviceConnectionEventArgs> DeviceConnected = delegate {};
		public event EventHandler<DeviceConnectionEventArgs> DeviceDisconnected = delegate {};
		public event EventHandler<ServiceDiscoveredEventArgs> ServiceDiscovered = delegate {};

		public bool IsScanning
		{
			get { return isScanning; }
		}

		public List<BluetoothDevice> DiscoveredDevices
		{
			get { return discoveredDevices; }
		}

		public static BluetoothLEManager Instance
		{
			get { return instance; }
		}

		public Dictionary<BluetoothDevice, BluetoothGatt> ConnectedDevices
		{
			get { return connectedDevices; }
		}

		static BluetoothLEManager()
		{
			instance = new BluetoothLEManager();
		}

		protected BluetoothLEManager()
		{
			var appContext = Android.App.Application.Context;
			manager = (BluetoothManager)appContext.GetSystemService("bluetooth");
			adapter = manager.Adapter;

			gattCallback = new GattaCallback(this);
		}

		/// <summary>
		/// Devicesのスキャンを開始する
		/// </summary>
		/// <returns>The scanning for devices.</returns>
		public async Task BegineScanningForDevices()
		{
			discoveredDevices.Clear();

			isScanning = true;
			adapter.StartLeScan(this);

			await Task.Delay(10000);

			if (isScanning)
			{
				adapter.StopLeScan(this);
				ScanTimeoutElapsed(this, new EventArgs());
			}
		}

		/// <summary>
		/// Deviecesのスキャンを終了する
		/// </summary>
		public void StopScanningForDevices()
		{
			isScanning = false;
			adapter.StopLeScan(this);
		}

		/// <summary>
		/// デフォルトで用意されているメソッド Scanのときに呼ばれる
		/// </summary>
		public void OnLeScan(BluetoothDevice device, int rssi, byte[] scanRecord)
		{
			if (!DeviceExistsInDiscoveredList(device))
			{
				Console.WriteLine(device);
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

		public void ConnectToDevice(BluetoothDevice device)
		{
			device.ConnectGatt(Android.App.Application.Context, true, gattCallback);
		}

		public void DisconnectDevice(BluetoothDevice device)
		{
			ConnectedDevices[device].Disconnect();
			ConnectedDevices[device].Close();
		}

		public BluetoothDevice GetConnectedDeviceByName(string deviceName)
		{
			foreach (var device in connectedDevices) 
			{
				if (device.Key.Name == deviceName) 
				{
					return device.Key;
				}
			}
			return null;
		}

		protected class GattaCallback : BluetoothGattCallback
		{
			protected BluetoothLEManager parent;

			public GattaCallback(BluetoothLEManager p)
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
