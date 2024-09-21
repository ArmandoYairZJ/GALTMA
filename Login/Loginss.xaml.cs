namespace GALTMA.Login;

using GALTMA.Usuario;
using GALTMA.Nutriologo;
using GALTMA.ServicioRegistro;
using GALTMA.DBop;

public partial class Loginss : ContentPage
{
	public Loginss()
	{
		InitializeComponent();
	}

    private async void btnEntrar_Clicked(object sender, EventArgs e)
    {
	
		string uss = eUS.Text;
		string conn = eConta.Text;

		OpMySQL opMySQL = new OpMySQL();


		if(opMySQL.veriCredencial(uss, conn))
		{
          
             if (uss == "Registro")
            {
                await Navigation.PushModalAsync(new SerVici(uss, conn)); 
            }

            else if (opMySQL.InSesion(uss, conn))
            {
                if (opMySQL.VeriJugador(uss, conn))
                {
                    await Navigation.PushModalAsync(new Usuarioss(uss, conn));
                }
                else if (opMySQL.VeriNutri(uss, conn))
                {
                    await Navigation.PushModalAsync(new Nutriologo(uss, conn));
                }
            }

        }
		else {
            
             await DisplayAlert("Aviso", "Contraseña o usuario incorrecta digite una contraseña valida", "ok"); 
		}
    }
}