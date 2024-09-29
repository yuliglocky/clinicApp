using System.Diagnostics;
using clinicApp.Conexion;
using clinicApp.views;


namespace clinicApp
{
    public partial class MainPage : ContentPage
    {

        private readonly Database? _database;
        public MainPage()
        {
            InitializeComponent();
            _database = App.Database;
        }

        private async void InicioClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InicioPage());

        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var username = UsernameEntry.Text;
            var password = PasswordEntry.Text;

            Debug.WriteLine(password);
            Debug.WriteLine(username);

            if (await ValidateUserAsync(username, password))
            {
                // Navegar a la página principal o realizar cualquier otra acción
                await Navigation.PushAsync(new InicioPage()); 
            }
            else
            {
                // Mostrar un mensaje de error
                await DisplayAlert("Error", "Usuario o contraseña incorrectos", "OK");
            }
        }

        private async Task<bool> ValidateUserAsync(string username, string password)
        {
            var user = await _database.GetAdminByUsernameAsync(username);
            return user != null && user.Password == password;
        }


    }

}
