using GALTMA.DBop;
using System.Data;

namespace GALTMA.Usuario;

public partial class Resultados : ContentPage
{
	
    public string eUS;
    public string eConta;
    public Resultados(string eUs, string eConta)
	{
		InitializeComponent();
		this.eUS = eUs;
		this.eConta = eConta;

        //Actualizar en tiempo real
        this.InvalidateMeasure();

        //Mostrar el ciclo de las mediciones
        MostrarResultados(eUs, eConta);

    }

    public async void MostrarResultados( string eUs, string eConta)
    {
        try
        {
            int essu = int.Parse(eUs);

            OpMySQL opMySQL = new OpMySQL();
            DataTable med = opMySQL.MostrarResul(eUS, eConta, essu);

            if (med != null && med.Rows.Count > 0)
            {
                ScrollView scrollView = new ScrollView();
                StackLayout stackLayout = new StackLayout();
                stackLayout.Padding = new Thickness(10);
                stackLayout.Spacing = 10;
                stackLayout.VerticalOptions = LayoutOptions.FillAndExpand;


                Label lblHisto = new Label
                {
                    Text = "Resultados De Las Mediciones",
                    HorizontalOptions = LayoutOptions.Center,
                    FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))

                };
                stackLayout.Children.Add(lblHisto);
                lblHisto.TextColor = Color.FromHex("#000000");

                foreach (DataRow row in med.Rows)
                {


                    Label lblPeso = new Label();
                    lblPeso.Text = $"Peso: {row["Peso"]}";

                    Label lblEsta = new Label();
                    lblEsta.Text = $"Estatura: {row["Esta"]}";

                    Label lblTriceps = new Label();
                    lblTriceps.Text = $"Triceps: {row["Triceps"]}";

                    Label lblSubescapular = new Label();
                    lblSubescapular.Text = $"Subescapular: {row["Subescapular"]}";

                    Label lblBiceps = new Label();
                    lblBiceps.Text = $"Biceps: {row["Biceps"]}";

                    Label lblCrestaIliaca = new Label();
                    lblCrestaIliaca.Text = $"Cresta Iliaca: {row["CrestaIliaca"]}";

                    Label lblSupraespinal = new Label();
                    lblSupraespinal.Text = $"Supraespinal: {row["Supraespinal"]}";

                    Label lblAbdominal = new Label();
                    lblAbdominal.Text = $"Abdominal: {row["Abdominal"]}";

                    Label lblPectoral = new Label();
                    lblPectoral.Text = $"Pectoral: {row["Pectoral"]}";

                    Label lblAxialMedial = new Label();
                    lblAxialMedial.Text = $"Axial Medial: {row["AxialMedial"]}";

                    Label lblMusloFrontal = new Label();
                    lblMusloFrontal.Text = $"Muslo Frontal: {row["MusloFrontal"]}";

                    Label lblPantorilla = new Label();
                    lblPantorilla.Text = $"Pantorilla: {row["Pantorilla"]}";

                    Label lblCuello = new Label();
                    lblCuello.Text = $"Cuello: {row["Cuello"]}";

                    Label lblBrazoRelajado = new Label();
                    lblBrazoRelajado.Text = $"Brazo Relajado: {row["BrazoRelajado"]}";

                    Label lblBrazoContraido = new Label();
                    lblBrazoContraido.Text = $"Brazo Contraido: {row["BrazoContraido"]}";

                    Label lblAntebrazo = new Label();
                    lblAntebrazo.Text = $"Antebrazo: {row["Antebrazo"]}";

                    Label lblMuneca = new Label();
                    lblMuneca.Text = $"Muñeca: {row["Muneca"]}";

                    Label lblMesoesternal = new Label();
                    lblMesoesternal.Text = $"Mesoesternal: {row["Mesoesternal"]}";

                    Label lblCintura = new Label();
                    lblCintura.Text = $"Cintura: {row["Cintura"]}";

                    Label lblAbdomen = new Label();
                    lblAbdomen.Text = $"Abdomen: {row["Abdomen"]}";

                    Label lblCadera = new Label();
                    lblCadera.Text = $"Cadera: {row["Cadera"]}";


                    Label lblMuslo1cm = new Label();
                    lblMuslo1cm.Text = $"Muslo 1cm: {row["Muslo1cm"]}";

                    Label lblMusloMedio = new Label();
                    lblMusloMedio.Text = $"Muslo Medio: {row["MusloMedio"]}";

                    Label lblPantorillaMedial = new Label();
                    lblPantorillaMedial.Text = $"Pantorilla Medial: {row["PantorillaMedial"]}";

                    Label lblTobillo = new Label();
                    lblTobillo.Text = $"Tobillo: {row["Tobillo"]}";

                    Label lblBiepicondiliodelhumero = new Label();
                    lblBiepicondiliodelhumero.Text = $"Biepicondilio Del Humero: {row["Biepicondiliodelhumero"]}";

                    Label lblBiestiloideodelamuneca = new Label();
                    lblBiestiloideodelamuneca.Text = $"Biestiloideo De La Muñeca: {row["Biestiloideodelamuneca"]}";

                    Label lblBicondileoFemur = new Label();
                    lblBicondileoFemur.Text = $"BicondileoFemur: {row["BicondileoFemur"]}";

                    Label lblBimaleolar = new Label();
                    lblBimaleolar.Text = $"Bimaleolar: {row["Bimaleolar"]}";


                    Frame frameProducto = new Frame();
                    frameProducto.CornerRadius = 10;
                    frameProducto.Padding = 10;
                    frameProducto.BackgroundColor = Color.FromHex("#000000");

                    StackLayout frameContent = new StackLayout();
                    frameContent.Orientation = StackOrientation.Horizontal;
                    frameContent.Children.Add(new StackLayout { Children = { lblPeso, lblEsta, lblTriceps, lblSubescapular, lblBiceps, lblCrestaIliaca, lblSupraespinal, lblAbdominal, lblPectoral, lblAxialMedial,
                        lblMusloFrontal, lblPantorilla, lblCuello, lblBrazoRelajado, lblBrazoContraido, lblAntebrazo, lblMuneca, lblMesoesternal, lblCintura, lblAbdomen, lblCadera, lblMuslo1cm, lblMusloMedio,
                        lblPantorillaMedial, lblTobillo, lblBiepicondiliodelhumero, lblBiestiloideodelamuneca, lblBicondileoFemur, lblBimaleolar} });
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