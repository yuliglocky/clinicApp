using clinicApp.Conexion;
using clinicApp.Models;
namespace clinicApp.views.components;

public partial class MedicamentosPage : ContentPage
{
    private Action _onUpdate;
    private readonly Database? _database;
    public MedicamentosPage(Action onUpdate)
	{
		InitializeComponent();
        _database = App.Database;
        _onUpdate = onUpdate;
    }

    private async void OnCloseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private async void OnSaveMedicamentosClicked(object sender, EventArgs e)
    {

        var medicamentos = new Medicamentos
        {
            Nombre = MedicamentosEntry.Text,
            Proveedor = ProveedorEntry.Text,
            cantidad = CantidadEntry.Text,
            FechaRegistro = DiaRegistroPicker.Date,
            FechaCaducidad = DiaCducidadPicker.Date
        };
        await _database.SaveMedicamentosAsync(medicamentos);
        await Application.Current.MainPage.DisplayAlert("Éxito", "Medicamentos guardada", "OK");
        ClearFields();
        // Limpiar campos
        DiaRegistroPicker.Date = DateTime.Now;
        DiaCducidadPicker.Date = DateTime.Now;

        await Navigation.PopModalAsync();
        _onUpdate?.Invoke();
    }

    private void ClearFields()
    {
        MedicamentosEntry.Text = string.Empty;
        ProveedorEntry.Text = string.Empty;
        CantidadEntry.Text = string.Empty;
    }

}