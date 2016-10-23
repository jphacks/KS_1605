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

			var start = this.FindByName<Button>("NextShare");
			start.Clicked += async (sender, arg) =>
			{
				await Navigation.PushAsync(new SharePage());
			};

			var memory = this.FindByName<Button>("NextMemory");
			memory.Clicked += async (sender, arg) =>
			{
				await Navigation.PushAsync(new SharePage());
			};
		}
	}
}
