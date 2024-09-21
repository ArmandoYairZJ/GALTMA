namespace GALTMA.ServicioRegistro;

public partial class SerVici : TabbedPage
{
	public SerVici(string eUS, string eConta)
	{
		InitializeComponent();


		var serviRegistro = new ServicioRegistro(eUS, eConta)
		{

			Title="Registro",
			IconImageSource = "agre.svg"
		};

		var ReNutri = new NutrioRegistro(eUS, eConta)
		{

			Title="Alta Nutriologo",
			IconImageSource="nutrialta.svg"
		};


        var Dabaja = new DaDBaja(eUS, eConta) 
		{

            Title = "Dar De Baja",
            IconImageSource = "eli.svg"

        };


		this.Children.Add( serviRegistro );	
		this.Children.Add( ReNutri );
		this.Children.Add(Dabaja);



	}
}