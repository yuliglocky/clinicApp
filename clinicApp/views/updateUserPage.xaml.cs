using clinicApp.Conexion;
using clinicApp.Models;

namespace clinicApp.views;

public partial class updateUserPage : ContentPage
{
    private int _userId;
    private readonly Database _database;
    private User _currentUser;
    private Action _onUpdate;

    public updateUserPage(int userId, Action onUpdate)
    {
        InitializeComponent();
        _userId = userId;
        _database = App.Database;
        _onUpdate = onUpdate;
        LoadUserData();
    }


    private async void LoadUserData()
    {
        _currentUser = await _database.GetUserByIdAsync(_userId);

        if (_currentUser != null)
        {
            UserNameEntry.Text = _currentUser.Nombre ?? string.Empty;
            UserLastNameEntry.Text = _currentUser.Apellido ?? string.Empty;
            UserEmailEntry.Text = _currentUser.Email ?? string.Empty;
            UserPhoneEntry.Text = _currentUser.Telefono ?? string.Empty;
            UserCedulaEntry.Text = _currentUser.cedula ?? string.Empty;
            UserAddressEntry.Text = _currentUser.direccion ?? string.Empty;
            UserMedicamentosEntry.Text = _currentUser.medicamentos ?? string.Empty;
            UserAlergiasEntry.Text = _currentUser.alergias ?? string.Empty;

        }
    }
    

    private async void OnUpdateClicked(object sender, EventArgs e)
    {
        if (_currentUser != null)
        {
            _currentUser.Nombre = UserNameEntry.Text;
            _currentUser.Apellido = UserLastNameEntry.Text;
            _currentUser.Email = UserEmailEntry.Text;
            _currentUser.Telefono = UserPhoneEntry.Text;
            _currentUser.cedula = UserCedulaEntry.Text;
            _currentUser.direccion = UserAddressEntry.Text;
            _currentUser.medicamentos = UserMedicamentosEntry.Text;
            _currentUser.alergias = UserAlergiasEntry.Text;

            await _database.SaveUserAsync(_currentUser);
            await DisplayAlert("Actualización", "Usuario actualizado con éxito", "OK");
            _onUpdate?.Invoke(); // Llamar a la acción de actualización
            await Navigation.PopAsync(); // Volver a la página anterior
        }
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (_currentUser != null)
        {
            await _database.DeleteUserAsync(_currentUser);
            await DisplayAlert("Eliminación", "Usuario eliminado", "OK");
            _onUpdate?.Invoke(); // Llamar a la acción de actualización
            await Navigation.PopAsync(); // Volver a la página anterior
        }
    }
}