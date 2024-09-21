using GALTMA.DBop;

namespace GALTMA.ServicioRegistro;

public partial class ServicioRegistro : ContentPage
{

    public string eUS;
    public string eConta;


    public ServicioRegistro(string eUS, string eConta)
    {
        InitializeComponent();
        this.eUS = eUS;
        this.eConta = eConta;
    }

    private async void btnGuardarregistro_Clicked(object sender, EventArgs e)
    {
		try
		{
            string Num = eNumero.Text;
            int NumCamisa = int.Parse(Num);

           string Nombre = eNombre.Text;
           string ApelliPaterno = eApellidoPaterno.Text;
           string ApellidoMaterno = eApellidoMaterno.Text;
           string posicion = ePosicion.Text;
           

           string Estatura = eEstatura.Text;
           decimal Estatu = decimal.Parse(Estatura);

           string contrasena = eContra.Text;
           string confirmas = eContraConfi.Text;

           string edad = eEdad.Text;
            int edd = int.Parse(edad);

            if (contrasena != confirmas)
            {
                await DisplayAlert("Error", "Las contraseñas no son iguales!!!", "OK");
                return;
            }

            OpMySQL opMySQL = new OpMySQL();
            bool result = opMySQL.AgregarJugadores(eUS,eConta, NumCamisa, Nombre, ApelliPaterno, ApellidoMaterno, posicion, Estatu, contrasena, edd);

            if (result)
            {
                if (opMySQL.crearlRol(eUS,eConta, NumCamisa, contrasena))
                {
                    await DisplayAlert("Éxito", "Jugador Agregado Correctamente", "OK");
                    clear();

                }
                
            }
            else
            {
                await DisplayAlert("Error", "Sucedio Algun Error Al Agregar Al Jugador", "OK");
         
            }


        }
        catch (Exception ex) {

            await  DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");

        }
    }

    private void clear()
    {
        eNumero.Text = string.Empty;
        eNombre.Text = string.Empty;
        eApellidoPaterno.Text = string.Empty;
        eApellidoMaterno.Text = string.Empty;
        ePosicion.Text = string.Empty;
        eEstatura.Text = string.Empty;
        eContra.Text = string.Empty;
        eContraConfi.Text = string.Empty;
        eEdad.Text= string.Empty;

    }
}