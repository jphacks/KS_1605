using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MemoTech
{
	public partial class AlbumPage : ContentPage
	{
		/// <summary>
		/// アルバムのページ
		/// </summary>
		/// <param name="index">Index = Album Index</param>
		public AlbumPage(int index)
		{
			InitializeComponent();

			Title = "AlbumPage";
		}
	}
}
