using clinicApp.Conexion;
using clinicApp.Models;


namespace clinicApp.views;

public partial class updateCitas : ContentPage
{
    private readonly Database _database;
    private readonly int _citaId;
    private Action _onUpdate;
    private Cita _currentCita;
    public updateCitas(int citaId, Action onUpdate)
    {
        InitializeComponent();
        _database = App.Database;
        _citaId = citaId;
        _onUpdate = onUpdate;
        LoadCitaDetails();
        LoadDoctors();

    }
    private async void LoadCitaDetails()
    {
        _currentCita = await _database.GetCitaByIdAsync(_citaId);
        if (_currentCita != null)
        {
            var doctor = await _database.GetDoctorByIdAsync(_currentCita.DoctorId);
            var paciente = await _database.GetUserByIdAsync(_currentCita.PacienteId);

            DoctorLabel.Text = doctor?.Nombre ?? "No disponible";
            PacienteLabel.Text = paciente?.Nombre ?? "No disponible";
            ConsultaDatePicker.Date = _currentCita.FechaConsulta; // Cargar la fecha de consulta
        }
    }

    private async void LoadDoctors()
    {
        var doctors = await _database.GetDoctorsAsync();
        DoctorPicker.ItemsSource = doctors.Select(d => d.Nombre).ToList();
        DoctorPicker.SelectedIndexChanged += (s, e) =>
        {
            if (DoctorPicker.SelectedItem != null)
            {
                var selectedDoctor = DoctorPicker.SelectedItem as string;
                var doctor = doctors.FirstOrDefault(d => d.Nombre == selectedDoctor);
                if (doctor != null)
                {
                    _currentCita.DoctorId = doctor.Id;
                }
            }
        };
    }

    private async void OnUpdateClicked(object sender, EventArgs e)
    {
        if (_currentCita != null)
        {
            _currentCita.FechaConsulta = ConsultaDatePicker.Date;

            await _database.SaveCitaAsync(_currentCita);
            await DisplayAlert("Éxito", "Cita actualizada", "OK");
            _onUpdate?.Invoke();
            await Navigation.PopAsync();
        }
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        var confirm = await DisplayAlert("Confirmar", "¿Estás seguro de que quieres eliminar esta cita?", "Sí", "No");
        if (confirm)
        {
            if (_currentCita != null)
            {
                _currentCita.IsDeleted = true;
                await _database.SaveCitaAsync(_currentCita);
                await DisplayAlert("Éxito", "Cita eliminada", "OK");
                _onUpdate?.Invoke();
                await Navigation.PopAsync(); // Volver a la página anterior
            }
        }
    }
}
