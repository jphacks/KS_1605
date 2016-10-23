using Xamarin.Forms;
using System;

namespace MemoTech
{

	public partial class MemoTechPage : ContentPage
	{
		public MemoTechPage()
		{
			InitializeComponent();

			Title = "TitlePage";

			var start = this.FindByName<Button>("Start");
			start.Clicked += async (sender, arg) =>
			{
				await Navigation.PushAsync(new SharePage());
			};

			Content = start;
		}
	}
}
