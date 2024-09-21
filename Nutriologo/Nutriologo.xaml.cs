namespace GALTMA.Nutriologo;

public partial class Nutriologo : TabbedPage
{
	public Nutriologo(string eUS, string eConta)
	{
		InitializeComponent();

		var InfoNutri = new InfoNutri()
		{
			Title = "Información Club",
			IconImageSource = "nutri.svg"
		};


		var MdAntro  = new MedicionesAntro( eUS,  eConta)
		{
			Title = "Mediciones Antropometricas",
		//	IconImageSource = "medi.svg"
		};

		var MASA  = new Masa(eUS, eConta)
		{
			Title = "Masa",
			IconImageSource ="result.svg"
		
		};

		var res = new Resumen()
		{
			Title ="Resumen ",
			IconImageSource = "resumen.svg"

		};


		this.Children.Add(InfoNutri);
		this.Children.Add(MdAntro);
	    this.Children.Add(MASA);
		//this.Children.Add(res);

		

	}

    
}