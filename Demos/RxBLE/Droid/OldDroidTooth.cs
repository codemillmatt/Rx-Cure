//using System;
//using System.Collections.Generic;
//using Android.Bluetooth;
//using System.Threading.Tasks;
//using Android.Bluetooth.LE;
//using System.Text;
//
//namespace BroodMinder.Droid
//{
//	public class DroidTooth : Java.Lang.Object ,BluetoothAdapter.ILeScanCallback//,IBroodToothScanner
//	{
//		protected BluetoothManager _manager;
//		protected BluetoothAdapter _adapter;
//		bool isScanning = false;
//		IDictionary<String,BroodToothDevice> DeviceCache;
//
//		public DroidTooth ()
//		{
//			var appContext = Android.App.Application.Context;
//			// get a reference to the bluetooth system service
//			this._manager = (BluetoothManager) appContext.GetSystemService("bluetooth");
//			this._adapter = this._manager.Adapter;
//
//			DeviceCache = new Dictionary<String,BroodToothDevice> ();
//		}
//
//		#region IBroodToothFinder implementation
//
//		public event EventHandler<BroodToothDeviceDiscoveredEventArgs> DeviceDiscovered = delegate{};
//		public event EventHandler<EventArgs> ScanTimedOut = delegate{};
//
//		public void StopScan()
//		{
//			if (isScanning) {
//				isScanning = false;
//
//				_adapter.StopLeScan (this);
//				DeviceCache.Clear ();
//			}
//		}
//
//		public async void ScanForDevices ()
//		{
//			if (!isScanning) {
//				isScanning = true;
//				_adapter.StartLeScan (this);
//			}
//		}
//
//		#endregion
//
//		private readonly Object CacheLock = new Object ();
//
//		public void OnLeScan (BluetoothDevice device, int rssi, byte[] scanRecord)
//		{						
//			if (scanRecord[8] == 141 && scanRecord[9] == 2) {
//
//				var broodDevice = new BroodToothDevice () {
//					AdvertisementData = scanRecord,
//					UUID = device.Address,
//					DeviceName = device.Name,
//					RSSI = rssi
//				};
//
//				bool update;
//				lock(CacheLock)
//				{
//					bool newItem;
//
//					update = (newItem = !DeviceCache.ContainsKey (device.Address)) 
//						|| DeviceCache [device.Address].Elapsed != broodDevice.Elapsed;
//
//					if (update) {
//						if (newItem)
//							DeviceCache.Add (device.Address, broodDevice);
//						else
//							DeviceCache [device.Address] = broodDevice;
//					}
//				}
//
//				if(update)
//					DeviceDiscovered (this, new BroodToothDeviceDiscoveredEventArgs (broodDevice)); 					
//			}
//		}			
//
//	}
//}