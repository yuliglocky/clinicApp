using clinicApp.Conexion;
using clinicApp.Models;

namespace clinicApp.views.components;

public partial class anadirAdmin : ContentView
{

    private readonly  Database?  _database;
    public anadirAdmin()
	{
		InitializeComponent();
        _database = App.Database;
    }

    private async void OnSaveDoctorClicked(object sender, EventArgs e)
    {
        // Validar que no haya caracteres especiales
        if (ContainsSpecialCharacters(NombreEntry.Text) || ContainsSpecialCharacters(EspecialidadEntry.Text) || ContainsSpecialCharacters(TelefonoEntry.Text))
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Los campos no deben contener caracteres especiales.", "OK");
            return;
        }

        var doctor = new Doctor
        {
            Nombre = NombreEntry.Text,
            Especialidad = EspecialidadEntry.Text,
            Telefono = TelefonoEntry.Text
        };

        await App.Database.SaveDoctorAsync(doctor); // Guarda el doctor en la base de datos
        await Application.Current.MainPage.DisplayAlert("Éxito", "Doctor guardado", "OK");

        // Limpiar los campos después de guardar
        ClearFields();
    }

    // Método para verificar si hay caracteres especiales
    private bool ContainsSpecialCharacters(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;
        // Expresión regular para permitir solo letras, números y espacios
        return System.Text.RegularExpressions.Regex.IsMatch(input, @"[^a-zA-Z0-9 ]");
    }

    // Método para limpiar los campos
    private void ClearFields()
    {
        NombreEntry.Text = string.Empty;
        EspecialidadEntry.Text = string.Empty;
        TelefonoEntry.Text = string.Empty;
    }
}