using Xamarin.Forms;
using System;
using MemoTech.Scripts.ViewModel;
using MemoTech.Scripts.Utility;

namespace MemoTech
{

	public partial class MemoTechPage : ContentPage
	{

        private MemoTechViewModel viewModel = new MemoTechViewModel();

        private Button mainButton = null;
        private Button stop = null;

		public MemoTechPage()
		{
			InitializeComponent();

			Title = "TitlePage";

			mainButton = this.FindByName<Button>("MainButton");
            stop = this.FindByName<Button>("StopButton");

            var memory = this.FindByName<Button>("NextMemory");

            mainButton.Clicked += async (sender, arg) =>
			{
                if(viewModel.buttonState == State.Share)
                {
                    //仮処理で0
                    await Navigation.PushAsync(new AlbumPage(0));
                }
                Clicked();
            };

            stop.IsEnabled = false;
            stop.Clicked += (sender, arg) =>
            {
                Clicked();
                viewModel.StopButton();
            };

            memory.Clicked += async (sender, arg) =>
            {
                await Navigation.PushAsync(new MemoryPage());
            };
        }

        private void Clicked()
        {
            var state = viewModel.MainButton();
            mainButton.Text = viewModel.StateTitle;
            stop.IsEnabled = (state == State.Start) ? false : true;
        }

	}
}
