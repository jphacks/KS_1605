﻿using System;
using System.Collections.Generic;
using System.Linq;

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
			var kind = KindList(loadData);
			var dic = new Dictionary<T, int>();
			foreach (var k in kind)
			{
				dic.Add(k, 0);
			}
			foreach (var data in loadData) 
			{
				for (int i = 0; i < kind.Count; i++) 
				{
					if (data.Equals(kind[i])) 
					{
						dic[kind[i]] += 1;
					}
				}
			}
			var cacheOrder = new List<int>();
			cacheOrder = dic.Values.ToList();
			cacheOrder.Sort();

			var result = new List<T>();
			foreach (var order in cacheOrder) 
			{
				foreach (var d in dic) 
				{
					if (order == d.Value && result.IndexOf(d.Key) == -1)
					{
						result.Add(d.Key);
					}
				}
			}
			return result;
		}

		private static List<T> KindList<T>(List<T> list)
		{
			var result = new List<T>();
			foreach (var kind in list)
			{
				if (result.IndexOf(kind) == -1)
				{
					result.Add(kind);
				}
			}
			return result;
		}

	}
}
