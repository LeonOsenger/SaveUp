using SaveUp.ViewModels;

namespace SaveUp;

public partial class ListPage : ContentPage
{

    public ListPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        ListViewModel viewModel = BindingContext as ListViewModel;
        if(viewModel != null ) 
        {
            viewModel.LoadData();
        }
    }
}