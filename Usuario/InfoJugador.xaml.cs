using GALTMA.DBop;

namespace GALTMA.Usuario;

public partial class InfoJugador : ContentPage
{
	
    public string eUS;
    public string eConta;
    public InfoJugador(string eUs, string eConta)
    {
        InitializeComponent();
        this.eUS = eUs;
        this.eConta = eConta;
        cargardatos();
    }

    public void cargardatos()
    {
        try
        {
            OpMySQL opMySQL = new OpMySQL();

            int playera = int.Parse(eUS);
            List<string> datosJugador = opMySQL.BuscarJugador(eUS, eConta, playera);

            if (datosJugador != null && datosJugador.Count > 0)
            {
                ePlayera.Text = eUS;
                eNom.Text = datosJugador[0];
                eApellidoPa.Text = datosJugador[1];
                eApellidoMa.Text = datosJugador[2];
                ePosicion.Text = datosJugador[3];
                eEstatura.Text = datosJugador[4];
                eEdad.Text = datosJugador[5];

            }
            else
            {
                DisplayAlert("Error", "No sea podido iniciar", "Aceptar");
            }
        }
        catch (FormatException)
        {
            DisplayAlert("Error", "Ingrese un número de camiseta válido.", "Aceptar");
        }
    }
}