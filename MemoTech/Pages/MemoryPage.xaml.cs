using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace MemoTech
{
	public partial class MemoryPage : ContentPage
	{

		public MemoryPage()
		{
			InitializeComponent();

			Title = "MemoryPage";

			var employees = new ObservableCollection<AlbumList>();

			employees.Add(new AlbumList { Title="Memory Album 1"});
			employees.Add(new AlbumList { Title = "Memory Album 2"});
			employees.Add(new AlbumList { Title = "Memory Album 3"});

			var list = this.FindByName<ListView>("AlbumList");
			//list.IsPullToRefreshEnabled = true;

			list.ItemsSource = employees;
            
			list.ItemSelected += async (sender, e) =>
			{
				//0は仮処理
				await Navigation.PushAsync(new AlbumPage(0));
			};
        }

        //実際のDelete処理にはなってないので後に追加
        public void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");
        }

    }

	public class AlbumList
    {
	    public string Title {get; set;}
	}
}
