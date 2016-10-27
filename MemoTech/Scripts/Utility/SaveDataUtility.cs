using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MemoTech.Scripts.Utility
{
    class SaveDataUtility
    {
        /// <summary>
        /// 型を指定してデータをセーブできる
        /// </summary>
        /// <typeparam name="T">セーブするデータ型</typeparam>
        /// <param name="dataKey">セーブデータのキー</param>
        /// <param name="variable">対象の変数</param>
        public static void Save<T>(string dataKey, T variable) where T : IComparable
        {
			Application.Current.Properties[dataKey] = variable;
        }

        /// <summary>
        /// 型を指定してデータをロードする
        /// </summary>
        /// <typeparam name="T">ロードするデータ型</typeparam>
        /// <param name="dataKey">セーブされたデータのキー</param>
        /// <returns></returns>
        public static T Load<T>(string dataKey) where T : IComparable
        {
            return (T)Application.Current.Properties[dataKey];
        }

        /// <summary>
        /// セーブデータがあるかチェックします
        /// </summary>
        /// <param name="dataKey">セーブされたデータのキー</param>
        /// <returns></returns>
        public static bool CheckData(string dataKey)
        {
			if (Application.Current == null) return false;
            if (Application.Current.Properties.ContainsKey(dataKey))
            {
                return true;
            } else {
                return false;
            }
        }

        /// <summary>
        /// セーブデータを全てクリアする
        /// </summary>
        public static void Clear()
        {
            Application.Current.Properties.Clear();
        }
    }
}
