using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MemoTech
{
	public class MemoryViewModel
	{
		private List<AlbumListCell> listData = new List<AlbumListCell>();

		public List<AlbumListCell> AlbumList { get { return listData; } set { listData = value; } }

		public MemoryViewModel()
		{
			listData.Add(new AlbumListCell("Memory Album 1", 1));
			listData.Add(new AlbumListCell("Memory Album 2", 2));
			listData.Add(new AlbumListCell("Memory Album 3", 3));
		}
	}
}
