using clinicApp.Conexion;
using clinicApp.Models;

namespace clinicApp.views.components;

public partial class DoctorPage : ContentPage
{
    private Action _onUpdate;
    private readonly Database? _database;
    public DoctorPage(Action onUpdate)
	{
		InitializeComponent();
        _database = App.Database;
        _onUpdate = onUpdate;

        string[] turnos = { "Ma�ana", "Tarde", "Noche" };

        string[] especialidades = { "Medico General", "Enfermeras", "Tecnicos" };

        TurnoPicker.ItemsSource = turnos;

        EspecialidadPicker.ItemsSource = especialidades;

    }

    private async void OnCloseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private async void OnSaveDoctorClicked(object sender, EventArgs e)
    {
        // Validar que no haya caracteres especiales
        if (ContainsSpecialCharacters(NombreEntry.Text) || ContainsSpecialCharacters(TelefonoEntry.Text))
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Los campos no deben contener caracteres especiales.", "OK");
            return;
        }

        var doctor = new Doctor
        {
            Nombre = NombreEntry.Text,
            Telefono = TelefonoEntry.Text,
            Turno = TurnoPicker.SelectedItem.ToString(),
            Especialidad = EspecialidadPicker.SelectedItem.ToString(),

        };

        await App.Database.SaveDoctorAsync(doctor); // Guarda el doctor en la base de datos
        await Application.Current.MainPage.DisplayAlert("�xito", "Doctor guardado", "OK");

        // Limpiar los campos despu�s de guardar
        ClearFields();

        await Navigation.PopModalAsync();
        _onUpdate?.Invoke();
    }

    // M�todo para verificar si hay caracteres especiales
    private bool ContainsSpecialCharacters(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;
        // Expresi�n regular para permitir solo letras, n�meros y espacios
        return System.Text.RegularExpressions.Regex.IsMatch(input, @"[^a-zA-Z0-9 ]");
    }

    // M�todo para limpiar los campos
    private void ClearFields()
    {
        NombreEntry.Text = string.Empty;
        TelefonoEntry.Text = string.Empty;
        TurnoPicker.SelectedItem = null;
        EspecialidadPicker.SelectedItem = null;
    }

}