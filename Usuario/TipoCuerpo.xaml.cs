using GALTMA.DBop;
using System.Data;

namespace GALTMA.Usuario;

public partial class TipoCuerpo : ContentPage
{
	
    public string eUS;
    public string eConta;
    public TipoCuerpo(string eUs, string eConta)
	{
		InitializeComponent();
		this.eUS = eUs;	
		this.eConta = eConta;

        this.InvalidateMeasure();

        //Mostrar resultados
        MostrarResultados(eUs, eConta);

    }

    public async void MostrarResultados(string eUs, string eConta)
    {
        try
        {
            int essu = int.Parse(eUs);
            OpMySQL opMySQL = new OpMySQL();
            DataTable med = opMySQL.mostrartc(eUs, eConta, essu);

            if (med != null && med.Rows.Count > 0)
            {
                ScrollView scrollView = new ScrollView();
                StackLayout stackLayout = new StackLayout();
                stackLayout.Padding = new Thickness(10);
                stackLayout.Spacing = 10;
                stackLayout.VerticalOptions = LayoutOptions.FillAndExpand;


                Label lblHisto = new Label
                {
                    Text = "Tipo de cuerpo",
                    HorizontalOptions = LayoutOptions.Center,
                    FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))

                };
                stackLayout.Children.Add(lblHisto);
                lblHisto.TextColor = Color.FromHex("#000000");

                foreach (DataRow row in med.Rows)
                {

                    Label lblAguaPorce = new Label();
                    lblAguaPorce.Text = $"Porcentaje De Agua: {row["AguaPorce"]}";

                    Label lblGrasaPorce = new Label();
                    lblGrasaPorce.Text = $"Porcentaje De Grasa: {row["GrasaPorce"]}";

                    Label lblTMusculoPorce = new Label();
                    lblTMusculoPorce.Text = $"Porcentaje De Musculo: {row["MusculoPorce"]}";

                    Label lblSubescapular = new Label();
                    lblSubescapular.Text = $"Porcentaje De Masa Osea: {row["MasaOseaPorce"]}";

                    Label lblMasaResidualPorce = new Label();
                    lblMasaResidualPorce.Text = $"Porcentaje De Masa Residual: {row["MasaResidualPorce"]}";

                    Label lblEndomorfia = new Label();
                    lblEndomorfia.Text = $"Endomorfia: {row["Endomorfia"]}";

                    Label lblMesomorfia = new Label();
                    lblMesomorfia.Text = $"Mesomorfia: {row["Mesomorfia"]}";

                    Label lblEctomorfia = new Label();
                    lblEctomorfia.Text = $"Ectomorfia: {row["Ectomorfia"]}";

                    Frame frameProducto = new Frame();
                    frameProducto.CornerRadius = 10;
                    frameProducto.Padding = 10;
                    frameProducto.BackgroundColor = Color.FromHex("#000000");

                    StackLayout frameContent = new StackLayout();
                    frameContent.Orientation = StackOrientation.Horizontal;
                    frameContent.Children.Add(new StackLayout
                    {
                        Children = { lblAguaPorce, lblGrasaPorce , lblTMusculoPorce, lblSubescapular, lblMasaResidualPorce, lblEndomorfia, lblMesomorfia, lblEctomorfia }
                    });
                    frameContent.BackgroundColor = Color.FromHex("#000000");



                    frameProducto.Content = frameContent;
                    stackLayout.Children.Add(frameProducto);
                }

                scrollView.Content = stackLayout;
                Content = scrollView;
            }
            else
            {
                await DisplayAlert("Advertencia", "No hay Mediciones disponibles", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("ERROR", $"OCURRIÓ UN ERROR: {ex.Message}", "OK");
        }
    }
}