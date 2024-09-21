using GALTMA.DBop;

namespace GALTMA.ServicioRegistro;

public partial class NutrioRegistro : ContentPage
{
    private string eUS;
    private string eConta;
    public NutrioRegistro(string eUs, string eConta)
	{
		InitializeComponent();
        this.eUS = eUs;
        this.eConta = eConta;
    }

    private async void btnGuardarregistro_Clicked(object sender, EventArgs e)
    {
        try
        {
            string eidNutri = eId_Nutri.Text;
            int idNutri = int.Parse(eidNutri);
            string Nombre = eNombre.Text;
            string ApellidoPaterno = eApellidoPaterno.Text;
            string ApellidoMaterno = eApellidoMaterno.Text;
            string contrasena = eContra.Text;
            string confirmar = eContraConfi.Text;

            if (contrasena != confirmar)
            {
                await DisplayAlert("Error", "Las contraseñas no son iguales!!!", "OK");
                return;
            }

            OpMySQL opMySQL = new OpMySQL();
            bool result = opMySQL.AgregarNutri(eUS, eConta, idNutri, Nombre, ApellidoPaterno, ApellidoMaterno, contrasena);

            if (result)
            {
                if (opMySQL.crearlRol(eUS,eConta,idNutri,contrasena))
                {
                    await DisplayAlert("Éxito", "Nutriologo Agregado Correctamente", "OK");
                    clear();
                }
               
            }
            else
            {
                await DisplayAlert("Error", "Sucedio Algun Error Al Agregar Al Nutriologo", "OK");

            }

        }
        catch (Exception ex) { 
        
        await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
        }
    }


    public void clear()
    {
        eId_Nutri.Text = string.Empty;
        eNombre.Text = string.Empty;
        eApellidoPaterno.Text = string.Empty;
        eApellidoMaterno.Text = string.Empty;
        eContra.Text = string.Empty;
        eContraConfi.Text = string.Empty;
    }
}