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
			list.ItemsSource = employees;
		}
	}

	public class Employee{
	    public string DisplayName {get; set;}
	}
}
