using System;
using Android.OS;
using Android;
using Android.Content;
using Android.Bluetooth;
using Android.Bluetooth.LE;
using System.Collections.Generic;
using System.Threading.Tasks;

using MemoTech.Scripts.Utility;
using Java.Util;

namespace MemoTech.Droid.Scripts.Utility
{
	//[assembly: UsesFeature("android.hardware.bluetooth", Required=true)]
	public class BluetoothLEManager : Java.Lang.Object, BluetoothAdapter.ILeScanCallback
	{

		private BluetoothManager manager;
		//bluetoothの接続に必要
		private BluetoothAdapter adapter;
		private LEScanCallback leCallback = new LEScanCallback();
		//gatt = サーバーのようなもの : Callbackどのようなサービスを使うのか決定して返す
		private GattaCallback gattCallback;
		private Dictionary<BluetoothDevice, BluetoothGatt> connectedDevices = new Dictionary<BluetoothDevice, BluetoothGatt>();
		private bool isScanning = false;
		private const int scanTimeout = 10000;
		//接続されたことのあるデバイス
		private List<string> discoveredDevices = new List<string>();
		private Dictionary<BluetoothDevice, IList<BluetoothGattService>> services = new Dictionary<BluetoothDevice, IList<BluetoothGattService>>();
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

		public List<string> DiscoveredDevices
		{
			get { return discoveredDevices; }
		}

		public BluetoothManager Manager
		{
			get { return manager; }
		}

		public Dictionary<BluetoothDevice, BluetoothGatt> ConnectedDevices
		{
			get { return connectedDevices; }
		}

		public Dictionary<BluetoothDevice, IList<BluetoothGattService>> Services
		{ 
			get { return services; }
		}

		public static BluetoothLEManager Instance
		{
			get { return instance; }
		}

		static BluetoothLEManager()
		{
			instance = new BluetoothLEManager();
		}

		protected BluetoothLEManager()
		{
			var appContext = Android.App.Application.Context;
			manager = (BluetoothManager)appContext.GetSystemService(Context.BluetoothService);
			adapter = manager.Adapter;

			gattCallback = new GattaCallback(this);
		}

		/// <summary>
		/// Devicesのスキャンを開始する
		/// </summary>
		/// <returns>The scanning for devices.</returns>
		public async Task BeginScanningForDevices()
		{
			discoveredDevices.Clear();
			isScanning = true;
			if (Build.VERSION.SdkInt < BuildVersionCodes.Lollipop)
			{
				adapter.StartLeScan(this);
			}
			else {
				adapter.BluetoothLeScanner.StartScan(leCallback);
			}

			await Task.Delay(scanTimeout);

			if (isScanning)
			{
				SaveDataUtility.SaveArray("scaned", discoveredDevices);
				if (Build.VERSION.SdkInt < BuildVersionCodes.Lollipop)
				{
					adapter.StopLeScan(this);
				}
				else {
					adapter.BluetoothLeScanner.StopScan(leCallback);
				}
				ScanTimeoutElapsed(this, new EventArgs());
				if (SaveDataUtility.CheckData("scaned")) SaveDataUtility.LoadArray<List<string>>("scaned").ForEach(_ => Console.WriteLine("Load : "+_));
			}
		}

		/// <summary>
		/// Deviecesのスキャンを終了する
		/// </summary>
		public void StopScanningForDevices()
		{
			SaveDataUtility.SaveArray("scaned", discoveredDevices);
			isScanning = false;
			if (Build.VERSION.SdkInt < BuildVersionCodes.Lollipop)
			{
				adapter.StopLeScan(this);
			}
			else {
				adapter.BluetoothLeScanner.StopScan(leCallback);
			}
		}

		/// <summary>
		/// デフォルトで用意されているメソッド Scanのときに呼ばれる
		/// </summary>
		public void OnLeScan(BluetoothDevice device, int rssi, byte[] scanRecord)
		{
			//Console.WriteLine("Search : " + device.Name + " " + device.Address + " " + device.BondState);

			//Console.WriteLine(device+" "+"Search : " + device.Type + " " + device.Name + " " + device.Class.SimpleName);
			if (!ConnectLog.Check(discoveredDevices, device.ToString()))
			{
				Console.WriteLine("OnLeScan : " + device.Name + " " + device.Address + " " + device.BondState);
				discoveredDevices.Add(device.ToString());
			}
			DeviceDiscovered(this, new DeviceDiscoveredEventArgs { Device = device, Rssi = rssi, ScanRecord = scanRecord });
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
			Console.WriteLine("DeviceName " + deviceName);
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
						parent.connectedDevices.Add(gatt.Device, gatt);
						parent.DeviceConnected(this, new DeviceConnectionEventArgs() { Device = gatt.Device });
						break;
					case ProfileState.Disconnecting:

						break;
				}
			}

			public override void OnServicesDiscovered(BluetoothGatt gatt, GattStatus status)
			{
				base.OnServicesDiscovered(gatt, status);

				if (!parent.services.ContainsKey(gatt.Device))
					parent.Services.Add(gatt.Device, parent.connectedDevices[gatt.Device].Services);
				else
					parent.services[gatt.Device] = parent.connectedDevices[gatt.Device].Services;

				parent.ServiceDiscovered(this, new ServiceDiscoveredEventArgs()
				{
					Gatt = gatt
				});
			}
		}
	}
}
