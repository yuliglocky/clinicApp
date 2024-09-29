using clinicApp.Conexion;
using clinicApp.Models;
namespace clinicApp.views.components;

public partial class CitaPage : ContentPage
{
    private Action _onUpdate;
    private readonly Database? _database;
    public CitaPage(Action onUpdate)
	{
		InitializeComponent();
        _database = App.Database;
        _onUpdate = onUpdate;
        LoadPickers();
    }

    private async void OnCloseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private async void LoadPickers()
    {
        // Obtener doctores y pacientes desde la base de datos
        var doctors = await _database.GetDoctorsAsync();
        var pacientes = await _database.GetUsersAsync();

        // Configurar los pickers con los datos obtenidos
        DoctorPicker.ItemsSource = doctors;
        PacientePicker.ItemsSource = pacientes;

        // Configurar los display members de los pickers
        DoctorPicker.ItemDisplayBinding = new Binding("Nombre");
        PacientePicker.ItemDisplayBinding = new Binding("Nombre");
    }

    private async void OnSaveCitaClicked(object sender, EventArgs e)
    {
        // Verificar que se ha seleccionado un doctor y un paciente
        if (DoctorPicker.SelectedIndex == -1 || PacientePicker.SelectedIndex == -1)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Por favor, seleccione un doctor y un paciente.", "OK");
            return;
        }

        var selectedDoctor = (Doctor)DoctorPicker.SelectedItem;
        var selectedPaciente = (User)PacientePicker.SelectedItem;

        if (selectedDoctor == null || selectedPaciente == null)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Doctor o paciente no encontrado.", "OK");
            return;
        }

        var cita = new Cita
        {
            DoctorId = selectedDoctor.Id, // Aquí usamos el ID del doctor
            PacienteId = selectedPaciente.Id, // Aquí usamos el ID del paciente
            FechaConsulta = DiaConsultaPicker.Date,
            FechaCita = DiaCitaPicker.Date
        };

        await _database.SaveCitaAsync(cita);
        await Application.Current.MainPage.DisplayAlert("Éxito", "Cita guardada", "OK");

        // Limpiar campos
        DoctorPicker.SelectedIndex = -1;
        PacientePicker.SelectedIndex = -1;
        DiaConsultaPicker.Date = DateTime.Now;
        DiaCitaPicker.Date = DateTime.Now;

        _onUpdate?.Invoke();
    }

}