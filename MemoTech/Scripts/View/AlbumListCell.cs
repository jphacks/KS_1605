using System;
using Xamarin.Forms;

namespace MemoTech
{
	public class AlbumListCell : ViewCell
	{
		public string Title { get; private set; }

		public AlbumListCell(string title)
		{
			Title = title;
		}
	}
}
