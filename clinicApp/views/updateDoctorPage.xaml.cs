using System.Numerics;
using clinicApp.Conexion;
using clinicApp.Models;

namespace clinicApp.views;

public partial class updateDoctorPage : ContentPage
{

    private readonly Database _database;
    private readonly int _doctorId;
    private Action _onUpdate;
    private Doctor _currentDoctor;
    public updateDoctorPage(int doctorId, Action onUpdate)
	{
		InitializeComponent();
        _database = App.Database;
        _doctorId = doctorId;
        _onUpdate = onUpdate;
        LoadDoctorData();
    }
    private async void LoadDoctorData()
    {
        _currentDoctor = await _database.GetDoctorByIdAsync(_doctorId);

        if (_currentDoctor != null)
        {
            DoctorLabel.Text = _currentDoctor.Nombre ?? "No disponible";
            EspecialidadLabel.Text = _currentDoctor.Especialidad ?? string.Empty;

            DoctorTelefonoEntry.Text = _currentDoctor.Telefono ?? string.Empty;
        }
        else
        {
            await DisplayAlert("Error", "No se encontr� el doctor", "OK");
            await Navigation.PopAsync();
        }
    }

    private async void OnUpdateClicked(object sender, EventArgs e)
    {
        if (_currentDoctor != null)
        {
            
           
            _currentDoctor.Telefono = DoctorTelefonoEntry.Text;
            // Obtener el valor seleccionado en el Picker
            _currentDoctor.Turno = DoctorTurnoPicker.SelectedItem?.ToString();
            await _database.SaveDoctorAsync(_currentDoctor);
            await DisplayAlert("Actualizaci�n", "Doctor actualizado con �xito", "OK");
            _onUpdate?.Invoke(); // Llamar a la acci�n de actualizaci�n
            await Navigation.PopAsync(); 
        }
    }
    private async void OnDeleteDoctorClicked(object sender, EventArgs e)
    {
        // Eliminar l�gicamente el doctor
        var confirm = await DisplayAlert("Confirmar", "�Est�s seguro de eliminar este doctor?", "S�", "No");
        if (confirm)
        {
            await _database.DeleteDoctorAsync(_currentDoctor);
            _onUpdate?.Invoke(); 
            await DisplayAlert("�xito", "Doctor eliminado correctamente", "OK");
            await Navigation.PopAsync(); 
        }
    }
}