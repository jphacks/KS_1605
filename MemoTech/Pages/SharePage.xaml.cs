using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MemoTech
{
	public partial class SharePage : ContentPage
	{
		public SharePage()
		{
			InitializeComponent();

			Title = "SharePage";

			var share = this.FindByName<Button>("BLEShare");
			share.Clicked += async (sender, arg) =>
			{
				//0は仮処理
				await Navigation.PushAsync(new AlbumPage(0));
			};
		}
	}
}
