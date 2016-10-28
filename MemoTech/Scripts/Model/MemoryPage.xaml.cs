using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace MemoTech
{
	public partial class MemoryPage : ContentPage
	{

		private MemoryViewModel viewModel = new MemoryViewModel();

		public MemoryPage()
		{
			InitializeComponent();

			Title = "MemoryPage";

			var list = this.FindByName<ListView>("AlbumList");

			list.ItemsSource = viewModel.AlbumList;
            
			list.ItemSelected += async (sender, e) =>
			{
				var cell = (AlbumListCell)sender;
				await Navigation.PushAsync(new AlbumPage(cell.Index));
			};
        }

        //実際のDelete処理にはなってないので後に追加
        public void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");
        }

    }
}
