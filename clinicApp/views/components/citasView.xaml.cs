using clinicApp.Conexion;

namespace clinicApp.views.components;

public partial class citasView : ContentView
{
    private readonly Database? _database;
    public citasView()
	{
		InitializeComponent();

        _database = App.Database;
        LoadCitas();
    }

    private async void LoadCitas()
    {
        var citas = await App.Database.GetCitasAsync(); // Obtener las citas desde la base de datos
        CitasListView.ItemsSource = citas; // Asignar citas al CollectionView
    }

    public async void OnCita(object sender, EventArgs e)
    {
        var button = sender as Button;

        Action refreshAction = LoadCitas;

        await Navigation.PushModalAsync(new CitaPage(refreshAction));
    }

    private async void OnDetailsClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var citaId = (int)button.CommandParameter;
        Action refreshAction = LoadCitas;

        // Navegar a la página de detalles
        await Navigation.PushAsync(new updateCitas(citaId, refreshAction));
    }


}