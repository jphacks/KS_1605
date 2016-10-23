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

			var employees = new ObservableCollection<Employee>();

			employees.Add(new Employee{ DisplayName="Memory Album 1"});
			employees.Add(new Employee{ DisplayName="Memory Album 2"});
			employees.Add(new Employee{ DisplayName="Memory Album 3"});

			var list = this.FindByName<ListView>("EmployeeView");
			list.IsPullToRefreshEnabled = true;

			list.ItemsSource = employees;

			list.ItemSelected += async (sender, e) =>
			{
				//0は仮処理
				await Navigation.PushAsync(new AlbumPage(0));
			};

		}

	}

	public class Employee{
	    public string DisplayName {get; set;}
	}
}
