using System;
using Xamarin.Forms;

namespace MemoTech
{
	public class AlbumListCell : ViewCell
	{
		public string Title { get; private set; }
		public int Index { get; private set; }

		public AlbumListCell(string title, int index)
		{
			Title = title;
			Index = index;
		}
	}
}
