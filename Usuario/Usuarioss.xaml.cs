namespace GALTMA.Usuario;

public partial class Usuarioss : TabbedPage
{
	public Usuarioss(string eUS, string eConta)
	{
		InitializeComponent();

		var Info = new InfoJugador(eUS, eConta)
		{
			Title = "General",
			IconImageSource="general.svg"
		};
		var Resulatdos = new Resultados( eUS, eConta)
		{
			Title="Resultados",
			IconImageSource = "result.svg"
        };

		var TipoCuerpo = new TipoCuerpo(eUS,eConta)
		{
            Title = "Tipo De Cuerpo",
            IconImageSource = "resumen.svg"
        };

		this.Children.Add(Info);
		this.Children.Add(Resulatdos);
		this.Children.Add(TipoCuerpo);
	}
}