using clinicApp.Conexion;
using clinicApp.Models;
using System.Security.Cryptography.X509Certificates;

namespace clinicApp.views;

public partial class PacientesPage : ContentPage
{
    private Action _onUpdate;
    private readonly Database? _database;
    public PacientesPage(Action onUpdate)
    {
		InitializeComponent();
        _database = App.Database;
        _onUpdate = onUpdate;
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

        _onUpdate?.Invoke();


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

    // Método para cerrar la página modal
    private async void OnCloseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    

}