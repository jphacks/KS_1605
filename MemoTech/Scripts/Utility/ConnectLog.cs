using System;
using System.Collections.Generic;

namespace MemoTech.Scripts.Utility
{
	public class ConnectLog
	{
		private static int connectionCount = 0;

		public static int ConnectionCount
		{
			get { return connectionCount; }
		}

		/// <summary>
		/// 過去に接続したことのあるものか確認
		/// </summary>
		/// <param name="logs">Logs.</param>
		/// <param name="target">Target.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static bool Check<T>(List<T> logs, T target) 
		{
			for (int i = 0; i < logs.Count; i++) 
			{
				if (logs[i].Equals(target)) 
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// 検索した際のヒット回数順に並べる
		/// </summary>
		/// <param name="key">Key.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static List<T> CountOrderSort<T>(string key) 
		{
			var loadData = SaveDataUtility.LoadArray<List<T>>(key);
			var result = new List<T>();
			foreach (var data in loadData) 
			{
				if (Check(loadData, data)) 
				{
					
				}
			}
			return result;
		}

	}
}
