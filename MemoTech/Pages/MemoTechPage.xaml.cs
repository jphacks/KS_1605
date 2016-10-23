using Xamarin.Forms;
using System;

namespace MemoTech
{

	public partial class MemoTechPage : ContentPage
	{
		private State buttonState = State.Start;
		private enum State
		{
			Start = 0,
			Share = 1
		}
		private string[] stateTitle = { "Start", "Share" };

		public MemoTechPage()
		{
			InitializeComponent();

			Title = "TitlePage";

			var mainButton = this.FindByName<Button>("MainButton");
            var stop = this.FindByName<Button>("StopButton");
            var memory = this.FindByName<Button>("NextMemory");


            mainButton.Clicked += async (sender, arg) =>
			{
				switch (buttonState)
				{
					case State.Start:
						buttonState = State.Share;
                        stop.IsEnabled = true;
						break;
					case State.Share:
						buttonState = State.Start;
						//仮処理で0
						await Navigation.PushAsync(new AlbumPage(0));
						break;
				}
				mainButton.Text = stateTitle[(int)buttonState];
			};

            stop.IsEnabled = false;
            stop.Clicked += async (sender, arg) =>
            {
                buttonState = State.Start;
                mainButton.Text = stateTitle[(int)State.Start];
                stop.IsEnabled = false;
            };

            memory.Clicked += async (sender, arg) =>
            {
                await Navigation.PushAsync(new MemoryPage());
            };
        }
	}
}
