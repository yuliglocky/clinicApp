using clinicApp.views.components;
using Microsoft.Win32;

namespace clinicApp.views;

public partial class InicioPage : ContentPage
{
	public InicioPage()
	{
		InitializeComponent();
        onPacientesView();
        ChangeButtonStyle(btnPacientes);
    }

    private async void InicioClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());

    }
    private void OnCitasButtonClicked(object sender, EventArgs e)
    {
        var clickedButton = sender as Button; // Convertir sender a Button
        if (clickedButton != null)
        {
            onCitasView();
        }
        ChangeButtonStyle(btnCitas);
    }


    private void AnadirClicked(object sender, EventArgs e)
    {
        var clickedButton = sender as Button; // Convertir sender a Button
        if (clickedButton != null)
        {
            anadirPacienteView();
        }
    }

    private void OnMensajesButtonClicked(object sender, EventArgs e)
    {
        var clickedButton = sender as Button; // Convertir sender a Button
        if (clickedButton != null)
        {
            onMensajesView();
        }
    }


    // PARTES DEL PARCIAL BOTON PARA LLAMAR LA FUNCION DOCTOR

 
    private void OnPacientesButtonClicked(object sender, EventArgs e)
    {
        var clickedButton = sender as Button; // Convertir sender a Button
        if (clickedButton != null)
        {
            onPacientesView();
        }
        ChangeButtonStyle(btnPacientes);
    }

    private void  OnViewDoctorClicked(object sender, EventArgs e)
    {
        var clickedButton = sender as Button; // Convertir sender a Button
        if (clickedButton != null)
        {
            onDoctorView();

        }
        ChangeButtonStyle(btnPersonalMedico);
    }

    private void OnViewMedicamentosClicked(object sender, EventArgs e)
    {
        var clickedButton = sender as Button; // Convertir sender a Button
        if (clickedButton != null)
        {
            onMedicamentosView();

        }
        ChangeButtonStyle(btnMedicamentos);
    }



    private void onCitasView()
    {
        // Cargar y mostrar la vista de inicio de sesión
        ContentContainer.Content = new citasView();
    }

    private void anadirPacienteView()
    {
        // Cargar y mostrar la vista de inicio de sesión
        ContentContainer.Content = new FormularioPaciente();
    }


    private void onPacientesView()
    {
        // Cargar y mostrar la vista de registro
        ContentContainer.Content = new PacientesView();
    }



    private void onDoctorView()
    {
        // Cargar y mostrar la vista de registro
        ContentContainer.Content =  new DoctorView();   
    }

    private void onMedicamentosView()
    {
        // Cargar y mostrar la vista de registro
        ContentContainer.Content = new medicamentosView();
    }
    private void onMensajesView()
    {
        // Cargar y mostrar la vista de registro
        ContentContainer.Content = new MensajesView();
    }

    private void ChangeButtonStyle(Button pressedButton)
    {
        // Resetear todos los botones a su estilo inicial
        ResetButtonStyles();

        // Cambiar el estilo del botón presionado
        pressedButton.BackgroundColor = Color.FromArgb("#4F7396");
        pressedButton.TextColor = Colors.White;
    }

    private void ResetButtonStyles()
    {
        
        btnPacientes.BackgroundColor = Color.FromArgb("#DDDDDD");
        btnPacientes.TextColor = Color.FromArgb("#1C4266");

        btnCitas.BackgroundColor = Color.FromArgb("#DDDDDD");
        btnCitas.TextColor = Color.FromArgb("#1C4266");

        btnPersonalMedico.BackgroundColor = Color.FromArgb("#DDDDDD");
        btnPersonalMedico.TextColor = Color.FromArgb("#1C4266");

        btnMedicamentos.BackgroundColor = Color.FromArgb("#DDDDDD");
        btnMedicamentos.TextColor = Color.FromArgb("#1C4266");
    }

    

}