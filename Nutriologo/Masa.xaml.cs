using GALTMA.DBop;
using System;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace GALTMA.Nutriologo;

public partial class Masa : ContentPage
{
    public string eUS;
    public string eConta;
    public Masa(string eUs, string eConta)
	{
		InitializeComponent();

        this.eUS = eUs;
        this.eConta = eConta;

        //densidad
        eBicipital.TextChanged += MasaGrasa;
        eTricipal.TextChanged += MasaGrasa;
        eSuprailiaco.TextChanged += MasaGrasa;
        esubescapular.TextChanged += MasaGrasa;

        //masa grasa
        Epeso.TextChanged += masg;
        eGrasa.TextChanged += masg;
        eporce.TextChanged += masg;

        //masa libre
        EPESO.TextChanged += masalibre;
        EFFM.TextChanged += masalibre;

        //Porcentaje de masa
        eporce.TextChanged += porMasa;
        ersuk.TextChanged += porMasa;

        //Porcentaje de agua
        eagua.TextChanged += ActualizarPorcentajeAgua;
        Epeso.TextChanged += ActualizarPorcentajeAgua;
        eGrasa.TextChanged += ActualizarPorcentajeAgua;

        //Cacular musculo
        Epeso.TextChanged += CalcularMusculo;
        eMasaGrasa.TextChanged += CalcularMusculo;

        //Masa osea
        Epeso.TextChanged += CalcularMasaOsea;

        //Masa residual
        Epeso.TextChanged += CalcularMasaResidual;
        eGrasa.TextChanged += CalcularMasaResidual;

        //Calcular endoforia 
        eEndoforfia.TextChanged += CalcularEndomorfia;
        eBicipital.TextChanged += CalcularEndomorfia;
        eSuprailiaco.TextChanged += CalcularEndomorfia;
        eTricipal.TextChanged += CalcularEndomorfia;
        esubescapular.TextChanged += CalcularEndomorfia;

        //Calcular Mesomorfia
        eBiepicondiliodelhumero.TextChanged += CalcularMesomorfia;
        eBiestiloideodelamuneca.TextChanged += CalcularMesomorfia;
        eBicondileoFemur.TextChanged += CalcularMesomorfia;
        eBimaleolar.TextChanged += CalcularMesomorfia;
        eBrazoContraido.TextChanged += CalcularMesomorfia;
        ePantorillaMedial.TextChanged += CalcularMesomorfia;
        eAltura.TextChanged += CalcularMesomorfia;
        eMesomorfia.TextChanged += CalcularMesomorfia;

        //Calcular Ecto
        eAlturas.TextChanged += CalcularEctomorfia;
        ePesos.TextChanged += CalcularEctomorfia;
        eectomorfia.TextChanged += CalcularEctomorfia;

        //Actualizar tiempo real
        this.InvalidateMeasure();

    }

    //Obtener densidad
    public void MasaGrasa(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(eBicipital.Text) || string.IsNullOrWhiteSpace(eTricipal.Text) ||
            string.IsNullOrWhiteSpace(eSuprailiaco.Text) || string.IsNullOrWhiteSpace(esubescapular.Text))
        {
            eMasaGrasa.Text = "";
            return;
        }

        if (double.TryParse(eBicipital.Text, out double bici) &&
            double.TryParse(eTricipal.Text, out double trici) &&
            double.TryParse(eSuprailiaco.Text, out double supra) &&
            double.TryParse(esubescapular.Text, out double subes))
        {
            double dc = MasaCalculo(bici, trici, supra, subes);
            eMasaGrasa.Text = dc.ToString("F4");
        }
        else
        {
            eMasaGrasa.Text = "Error";
        }
    }

    public double MasaCalculo(double bici, double trici, double supra, double subes)
    {
        double masa = bici + trici + supra + subes;
        double dc = 1.1631 - 0.0632 * Math.Log10(masa);
        return dc;
    }

    //Obtener masagrasa

    public void masg(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Epeso.Text))
        {
            eGrasa.Text = "";
            eporce.Text = "";

            return;
        }

        if (double.TryParse(Epeso.Text, out double peso) && double.TryParse(eMasaGrasa.Text, out double dc))

        {
            double msg = masaCals(peso, dc);
            eGrasa.Text = msg.ToString("F4");
            eporce.Text = eMasaGrasa.Text;
        }
        else
        {
            eGrasa.Text = "Error";
            eporce.Text = "";
        }
    }
    public double masaCals(double peso, double dc)
    {
        double msg = peso * ((4.95 / dc) - 4.5);
        return msg;
    }
   

    //Masa libre

    public void masalibre( object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(EPESO.Text))
        {
            EFFM.Text = "";
            return;
        }

        if (double.TryParse(EPESO.Text, out double peso) && double.TryParse(eGrasa.Text, out double mslb))

        {
            double msg = masalibre(peso, mslb);
            EFFM.Text = msg.ToString("F4");
        }
        else
        {
            EFFM.Text = "Error";
        }
    }

    public double masalibre(double peso, double mslb)
    {
        double masalb = peso - mslb;
        return masalb;
    }


    // obtener el porcentaje de masa corporal 
    public void porMasa( object sender, TextChangedEventArgs e)
    {
        if(string.IsNullOrWhiteSpace(eporce.Text))
        {
            ersuk.Text = "";
            return;
        }

        if (double.TryParse(eporce.Text, out double dc))

        {
            double gc = pormasas(dc);
            ersuk.Text = gc.ToString("F4");
        }
        else
        {
            ersuk.Text = "Error";
        }
    }

    public double pormasas(double dc)
    {
        double gc = ((4.95 / dc) - 4.5) *100;
        return gc;
    }


    //Porcentaje de agua
    public void ActualizarPorcentajeAgua( object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Epeso.Text) || string.IsNullOrWhiteSpace(eGrasa.Text))
        {
            eagua.Text = "";
            return;
        }

        if (double.TryParse(Epeso.Text, out double pesoTotal) && double.TryParse(eGrasa.Text, out double masaGrasa))
        {
            double masaLibreDeGrasa = pesoTotal - masaGrasa;
            double agua = masaLibreDeGrasa * 0.73; 
            double porcentajeAguaTotal = (agua / pesoTotal) * 100; 

            eagua.Text = porcentajeAguaTotal.ToString("F2"); 
        }
        else
        {
            eagua.Text = "Error"; 
        }
    }

    //Metodo para determinar el musculo 
    public void CalcularMusculo(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Epeso.Text) || string.IsNullOrWhiteSpace(eMasaGrasa.Text))
        {
            eMusculo.Text = ""; 
            return;
        }

        if (double.TryParse(Epeso.Text, out double peso) && double.TryParse(eMasaGrasa.Text, out double masaGrasa))
        {
            double masaLibreDeGrasa = peso - masaGrasa;
            double musculo = masaLibreDeGrasa * 0.50; 
            eMusculo.Text = musculo.ToString("F4"); 
        }
        else
        {
            eMusculo.Text = "Error"; 
        }
    }

    //masa osea
    public void CalcularMasaOsea(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Epeso.Text))
        {
            emasaosea.Text = ""; 
            return;
        }

        if (double.TryParse(Epeso.Text, out double peso))
        {
           
            double masaOsea = peso * 0.025; 
            emasaosea.Text = masaOsea.ToString("F4"); 
        }
        else
        {
            emasaosea.Text = "Error"; 
        }
    }

    //Calcular masa residual 
    public void CalcularMasaResidual(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Epeso.Text) || string.IsNullOrWhiteSpace(eGrasa.Text))
        {
            emasaresidual.Text = ""; 
            return;
        }

        if (double.TryParse(Epeso.Text, out double pesoTotal) && double.TryParse(eGrasa.Text, out double masaGrasa))
        {
            double masaLibreDeGrasa = pesoTotal - masaGrasa;

            double masaResidual = masaLibreDeGrasa * 0.03;

            emasaresidual.Text = masaResidual.ToString("F4"); 
        }
        else
        {
            emasaresidual.Text = "Error"; 
        }
    }


    //Calcular Endormorfia 
    public void CalcularEndomorfia(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(eBicipital.Text) || string.IsNullOrWhiteSpace(eTricipal.Text) ||
        string.IsNullOrWhiteSpace(eSuprailiaco.Text) || string.IsNullOrWhiteSpace(esubescapular.Text))
        {
            eEndoforfia.Text = "";
            return;
        }

        if (double.TryParse(eBicipital.Text, out double bici) &&
            double.TryParse(eTricipal.Text, out double trici) &&
            double.TryParse(eSuprailiaco.Text, out double supra) &&
            double.TryParse(esubescapular.Text, out double subes))
        {
            double endomorfia = CalcularEndomorfia(bici, trici, supra, subes);
            eEndoforfia.Text = endomorfia.ToString("F2");
        }
        else
        {
            eEndoforfia.Text = "Error";
        }
    }

    public double CalcularEndomorfia(double bicipital, double tricipal, double suprailiaco, double subescapular)
    {
        double sumaPliegues = bicipital + tricipal + suprailiaco + subescapular;
        double raizSumaPliegues = Math.Sqrt(sumaPliegues);
        double endomorfia = 0.073 * sumaPliegues + 0.344 * raizSumaPliegues - 7.717;
        return endomorfia;
    }


    //Calcular Mesomorfia
    public void CalcularMesomorfia(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(eBiepicondiliodelhumero.Text) || string.IsNullOrWhiteSpace(eBiestiloideodelamuneca.Text) ||
            string.IsNullOrWhiteSpace(eBicondileoFemur.Text) || string.IsNullOrWhiteSpace(eBimaleolar.Text) ||
            string.IsNullOrWhiteSpace(eBrazoContraido.Text) || string.IsNullOrWhiteSpace(ePantorillaMedial.Text) ||
            string.IsNullOrWhiteSpace(eAltura.Text))
        {
            eMesomorfia.Text = "";
            return;
        }

        if (double.TryParse(eBiepicondiliodelhumero.Text, out double biepicondiliodelhumero) &&
            double.TryParse(eBiestiloideodelamuneca.Text, out double biestiloideodelamuneca) &&
            double.TryParse(eBicondileoFemur.Text, out double bicondileoFemur) &&
            double.TryParse(eBimaleolar.Text, out double bimaleolar) &&
            double.TryParse(eBrazoContraido.Text, out double brazoContraido) &&
            double.TryParse(ePantorillaMedial.Text, out double pantorillaMedial) &&
            double.TryParse(eAltura.Text, out double altura))
        {
            double mesomorfia = calcularMesomorfia(biepicondiliodelhumero, biestiloideodelamuneca, bicondileoFemur, bimaleolar, brazoContraido, pantorillaMedial, altura);
            eMesomorfia.Text = mesomorfia.ToString("F2");
        }
        else
        {
            eMesomorfia.Text = "Error";
        }
    }

    public double calcularMesomorfia(double biepicondiliodelhumero, double biestiloideodelamuneca, double bicondileoFemur, double bimaleolar, double brazoContraido, double pantorillaMedial, double altura)
    {
        double mesomorfia = 0.858 * biepicondiliodelhumero + 0.601 * bicondileoFemur + 0.188 * brazoContraido + 0.161 * pantorillaMedial - 0.131 * altura + 4.5;
        return mesomorfia;
    }


    //Cacular Ectomorfia
    public void CalcularEctomorfia(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(eAlturas.Text) || string.IsNullOrWhiteSpace(ePesos.Text))
        {
            eectomorfia.Text = "";
            return;
        }

        if (double.TryParse(eAlturas.Text, out double altura) && double.TryParse(ePesos.Text, out double peso))
        {
            double ectomorfia = CalcularEctomorfia(altura, peso);
            eectomorfia.Text = ectomorfia.ToString("F2");
        }
        else
        {
            eectomorfia.Text = "Error";
        }
    }

    public double CalcularEctomorfia(double altura, double peso)
    {
        double indicePonderal = altura / Math.Pow(peso, 1.0 / 3.0);
        double ectomorfia = 0.732 * indicePonderal - 28.58;
        return ectomorfia;
    }

    //Buscar
    private void btnBuscar_Clicked(object sender, EventArgs e)
       {
        try
        {
            OpMySQL opMySQL = new OpMySQL();

            int playera = int.Parse(eplayera.Text);

            List<string> ultimaMedi = opMySQL.BuscarUltimaMedicion(eUS, eConta, playera);
            List<string> BuscarPeso = opMySQL.BuscarPeso(eUS, eConta, playera);
            List<string> mslb = opMySQL.BuscarPeso(eUS, eConta, playera);
            List<string> meso = opMySQL.mesoforia(eUS, eConta, playera);
            List<string> ecto = opMySQL.ecto(eUS, eConta, playera);

            if (ultimaMedi != null && ultimaMedi.Count > 0)
            {
                eNom.Text = ultimaMedi[0];
                eBicipital.Text = ultimaMedi[1];
                eTricipal.Text = ultimaMedi[2];
                eSuprailiaco.Text = ultimaMedi[3];
                esubescapular.Text = ultimaMedi[4];
            }
            else
            {
                DisplayAlert("Error", "No se encontraron datos de mediciones.", "Aceptar");
                return; 
            }

            if (BuscarPeso != null && BuscarPeso.Count > 0)
            {
                Epeso.Text = BuscarPeso[0];
            }
            else
            {
                DisplayAlert("Error", "No se encontraron datos de peso.", "Aceptar");
            }

            if (mslb != null && mslb.Count > 0)
            {
                EPESO.Text = mslb[0];
            }
            else
            {
                DisplayAlert("Error", "No se encontraron datos de peso.", "Aceptar");
            }

            if (meso != null && meso.Count > 0)
            {
                eBiepicondiliodelhumero.Text = meso[0];
                eBiestiloideodelamuneca.Text = meso[1];
                eBicondileoFemur.Text = meso[2];
                eBimaleolar.Text = meso[3];
                eBrazoContraido.Text = meso[4];
                ePantorillaMedial.Text = meso[5];
                eAltura.Text = meso[6];
            }
            else
            {
                DisplayAlert("Error", "No se encontraron datos de mediciones.", "Aceptar");
                return;
            }

            if (ecto != null && ecto.Count > 0)
            {
                eAlturas.Text = meso[0];
                ePesos.Text = meso[1];
              
            }
            else
            {
                DisplayAlert("Error", "No se encontraron datos de mediciones.", "Aceptar");
                return;
            }
        }
        catch (FormatException)
        {
            DisplayAlert("Error", "Ingrese un número de camiseta válido.", "Aceptar");
        }
        catch (Exception ex) 
        {
            DisplayAlert("Error", $"Un error inesperado ocurrió: {ex.Message}", "Aceptar");
        }
    }

    private async void btnGuardarRefistro_Clicked(object sender, EventArgs e)
    {
        try
        {
            //Agua
            string agua = eagua.Text;
            double agus = double.Parse(agua);

            //GrasaPorce
            double gp = double.Parse(ersuk.Text);

            //MusculoPorce
            double musculop = double.Parse(eMusculo.Text);
            //MasaOseaPorce
            double msose = double.Parse(emasaosea.Text);

            //MasaResidualPorce
            double mrp = double.Parse(emasaresidual.Text);


            //Endormorfia
            double endo = double.Parse(eEndoforfia.Text);
            //Mesomorfia
            double meso = double.Parse(eMesomorfia.Text);
            //Ectomorfia
            double ecto = double.Parse(eectomorfia.Text);
            //idJuga
            int playera = int.Parse(eplayera.Text);

            OpMySQL opMySQL = new OpMySQL();
        bool result = opMySQL.Resultados(eUS, eConta, agus, gp, musculop, msose,  mrp, endo, meso, ecto, playera);

            
           if (result)
            {
                await DisplayAlert("Listo", "Datos del usuario Guardado con exito", "Finalizar");
                clear();
            }

            else
            {
                await DisplayAlert("Error", "Sucedio Algun Error al rellenar los datos", "OK");

            }

        }

        catch (FormatException)
        {
           await DisplayAlert("Error", "Algun dato no se guardo correctamente", "Aceptar");
        }
    }


    public void clear()
    {
        eplayera.Text = string.Empty;
        eNom.Text = string.Empty;
        eBicipital.Text = string.Empty;
        eTricipal.Text = string.Empty;
        eSuprailiaco.Text = string.Empty;
        esubescapular.Text = string.Empty;
        eMasaGrasa.Text = string.Empty;
        Epeso.Text = string.Empty;
        eGrasa.Text = string.Empty;
        EPESO.Text = string.Empty;
        EFFM.Text = string.Empty;
        eporce.Text = string.Empty;
        ersuk.Text = string.Empty;
        eagua.Text = string.Empty;
        eMusculo.Text = string.Empty;
        emasaosea.Text = string.Empty;
        emasaresidual.Text = string.Empty;
        eEndoforfia.Text = string.Empty;
        eBiepicondiliodelhumero.Text = string.Empty;
        eBiestiloideodelamuneca.Text = string.Empty;
        eBicondileoFemur.Text = string.Empty;
        eBimaleolar.Text = string.Empty;
        eBrazoContraido.Text = string.Empty;
        ePantorillaMedial.Text = string.Empty;
        eAltura.Text = string.Empty;
        eMesomorfia.Text = string.Empty;
        eAlturas.Text = string.Empty;
        ePesos.Text = string.Empty;
        eectomorfia.Text = string.Empty;
    }
}