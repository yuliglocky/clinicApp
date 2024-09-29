using clinicApp.Conexion;
using clinicApp.Models;

namespace clinicApp.views.components;

public partial class medicamentosView : ContentView
{
    private Medicamentos _currentMedicamentos;
    private readonly Database _database;
    public medicamentosView()
	{
		InitializeComponent();
        LoadMedicamentos(); 
       _database = App.Database;
	}


    private async void LoadMedicamentos()
    {
        var medicemntos = await App.Database.GetMedicamentosAsync(); // Obtener las citas desde la base de datos
        MedicamentosListView.ItemsSource = medicemntos; // Asignar medicamentos al CollectionView
    }

   private async void OnMedicamento(object sender, EventArgs e)
    {
        var button = sender as Button;

        Action refreshAction = LoadMedicamentos;

        await Navigation.PushModalAsync(new MedicamentosPage(refreshAction));
    }

    public async void DisminuirCantidad(object sender, EventArgs e)
    {
        var button = sender as Button;
        var medicamento = button?.BindingContext as Medicamentos;

        if (medicamento != null && int.TryParse(medicamento.cantidad, out int cantidad) && cantidad > 0)
        {
            medicamento.cantidad = (cantidad - 1).ToString();
            await App.Database.SaveMedicamentosAsync(medicamento);
            LoadMedicamentos();

            if (cantidad -1 <= 5)
            {
                await Application.Current.MainPage.DisplayAlert("Reabastecimiento requerido", $"El medicamento '{medicamento.Nombre}' tiene solo '{medicamento.cantidad} unidades. Considera reabastecerlo.'", "OK");
            }
        }
    }

    public async void AumentarCantidad(object sender, EventArgs e)
    {
        var button = sender as Button;
        var medicamento = button?.BindingContext as Medicamentos;

        if (medicamento != null && int.TryParse(medicamento.cantidad, out int cantidad))
        {
            medicamento.cantidad = (cantidad + 1).ToString();

            await App.Database.SaveMedicamentosAsync(medicamento);

            LoadMedicamentos();
        }
    }

    //private async void OnDeleteClicked(object sender, EventArgs e)
    //{
    //    var confirm = await DisplayAlert("Confirmar", "¿Estás seguro de que quieres eliminar esta cita?", "Sí", "No");
    //    if (confirm)
    //    {
    //        if (_currentMedicamentos != null)
    //        {
    //            _currentMedicamentos.IsDeleted = true;
    //            await _database.SaveCitaAsync(_currentMedicamentos);
    //            await DisplayAlert("Éxito", "Cita eliminada", "OK");
               
    //        }
    //    }
    //}

}