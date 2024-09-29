namespace clinicApp.views.components;

public partial class DoctorView : ContentView
{
	public DoctorView()
	{
		InitializeComponent();
        LoadDoctor();   

	}


    private async void LoadDoctor()
    {
        var doctor = await App.Database.GetDoctorsAsync();
       DoctorListView.ItemsSource = doctor;
    }

    private async void OnDoctor(object sender, EventArgs e)
    {
        var button = sender as Button;

        Action refreshAction = LoadDoctor;

        await Navigation.PushModalAsync(new DoctorPage(refreshAction));
    }

    private async void OnDetailsClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var doctorId = (int)button.CommandParameter;

        Action refreshAction = LoadDoctor;


        // Navegar a la página de detalles
        await Navigation.PushAsync(new updateDoctorPage(doctorId, refreshAction));
    }


}