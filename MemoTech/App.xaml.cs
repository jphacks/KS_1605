using Xamarin.Forms;

namespace MemoTech
{
	public partial class App : Application
	{
		public MemoTechPage startPage = new MemoTechPage();

		public App()
		{
			InitializeComponent();

			MainPage = new NavigationPage(startPage)
			{
				BarBackgroundColor = Color.FromRgba(0.2, 0.6, 0.86, 1),
				BarTextColor = Color.White
			};
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
