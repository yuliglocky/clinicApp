using clinicApp.Conexion;
using clinicApp.Models;

namespace clinicApp.views.components;

public partial class FormularioPaciente : ContentView
{

    private readonly Database? _database;
    public FormularioPaciente()
	{
		InitializeComponent();
        _database = App.Database;
    }
    private async void OnAddUserClicked(object sender, EventArgs e)
    {
        var user = new Models.User
        {
            Nombre = NombreEntry.Text,
            Apellido = ApellidoEntry.Text,
            Email = EmailEntry.Text,
            Telefono = TelefonoEntry.Text,
            cedula = CedulaEntry.Text,
            direccion = DireccionEntry.Text,
            medicamentos = MedicamentosEntry.Text,
            alergias = AlergiasEntry.Text,

        };

        await _database.CreateUserAsync(user);

        // Mostrar mensaje de confirmación
        await DisplayAlert("Paciente Agregado", "El paciente ha sido agregado correctamente.", "OK");
        
        // Opcional: Limpiar los campos después de agregar
        ClearFields();
        


    }

    // Elimina este método si ya tienes DisplayAlert disponible.
    private async Task DisplayAlert(string title, string message, string cancel)
    {
        await Application.Current.MainPage.DisplayAlert(title, message, cancel);
    }

    private void ClearFields()
    {
        // Limpia los campos de texto
        NombreEntry.Text = string.Empty;
        ApellidoEntry.Text = string.Empty;
        EmailEntry.Text = string.Empty;
        TelefonoEntry.Text = string.Empty;
        CedulaEntry.Text = string.Empty;
        DireccionEntry.Text = string.Empty;
        MedicamentosEntry.Text = string.Empty;
        AlergiasEntry.Text = string.Empty;  
    }


    private void OnUserSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is User selectedUser)
        {
           
        }
    }

   

}