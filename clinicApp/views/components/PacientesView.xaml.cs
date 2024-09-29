using clinicApp.Conexion;
using clinicApp.Models;

namespace clinicApp.views.components;

public partial class PacientesView : ContentView
{
    private readonly Database? _database;
    public Command<int> NavigateCommand { get; set; }
    public PacientesView()
	{
		InitializeComponent();
        _database = App.Database;
        // Carga los usuarios al iniciar la p�gina
        LoadUsers();
      
    }

    private async void LoadUsers()
    {
        var users = await _database.GetUsersAsync();
        UsersListView.ItemsSource = users;
    }


    private async void OnAddPacienteClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        

        // Crear una acci�n que se ejecute al volver a la p�gina anterior
        Action refreshAction = LoadUsers;

        await Navigation.PushModalAsync(new PacientesPage(refreshAction));
    }

    // Navegar a la p�gina de actualizaci�n pasando el ID
    // M�todo para manejar el clic en "Ver Detalles"
    private async void OnViewDetailsClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var userId = (int)button.CommandParameter;

        // Crear una acci�n que se ejecute al volver a la p�gina anterior
        Action refreshAction = LoadUsers;

        await Navigation.PushAsync(new updateUserPage(userId, refreshAction));
    }



}