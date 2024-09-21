using GALTMA.DBop;
using MySqlConnector;

namespace GALTMA.ServicioRegistro;

public partial class DaDBaja : ContentPage
{
    private string eUS;
    private string eConta;

    public DaDBaja(string eUS, string eConta)
	{
		InitializeComponent();
        this.eUS = eUS;
        this.eConta = eConta;
      
	}

  

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        OpMySQL opMySQL = new OpMySQL();

        try
        {
            int playera = int.Parse(eCamiseta.Text); 

            bool eliminado = opMySQL.eliminaJu(eUS, eConta, playera);

            if (eliminado)
            {
              await  DisplayAlert("Éxito", "El jugador fue eliminado", "Aceptar");

                clear();
            }
            else
            {
                await DisplayAlert("Error", "No se ha podido encontrar el jugador", "Aceptar");
            }
        }
        catch (FormatException)
        {
            await DisplayAlert("Error", "Por favor coloque un numero valido de jugador", "Aceptar");
        }
    }

    private void btnBuscar_Clicked(object sender, EventArgs e)
    {
        try
        {
            OpMySQL opMySQL = new OpMySQL();

            int playera = int.Parse(eCamiseta.Text); 

            List<string> datosJugador = opMySQL.BuscarJugador(eUS, eConta,playera);

            if (datosJugador != null && datosJugador.Count > 0)
            {
                eNombre.Text = datosJugador[0]; 
                eApellidoPaterno.Text = datosJugador[1];
                eApellidoMaterno.Text = datosJugador[2];
                ePosicion.Text = datosJugador[3];
                eEstatura.Text= datosJugador[4];
                eEdad.Text = datosJugador[5]; 
                                                  
            }
            else
            {
                DisplayAlert("Error", "No se encontró ningún jugador con el número de camiseta proporcionado.", "Aceptar");
            }
        }
        catch (FormatException)
        {
            DisplayAlert("Error", "Ingrese un número de camiseta válido.", "Aceptar");
        }
    }

    private void clear()
    {
        eCamiseta.Text = string.Empty;
        eNombre.Text = string.Empty;
        eApellidoPaterno.Text = string.Empty;
        eApellidoMaterno.Text = string.Empty;
        ePosicion.Text = string.Empty;
        eEstatura.Text = string.Empty;
        eEdad.Text = string.Empty;
    }
}