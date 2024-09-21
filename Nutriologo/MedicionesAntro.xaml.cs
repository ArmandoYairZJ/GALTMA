using GALTMA.DBop;

namespace GALTMA.Nutriologo;

public partial class MedicionesAntro : ContentPage
{
    public string eUS;
    public string eConta;

    public MedicionesAntro(string eUs, string eConta)
	{
		InitializeComponent();
        this.eUS = eUs;
        this.eConta = eConta;

        //Pliegues
        //Evento para calcular Triceps
        ePrimeraP.TextChanged += CalcularResultadoTriceps;
    

        //Subescapular
        ePrimeraS.TextChanged += CalcularResultadoSubescapular;
       

        //Biceps
        ePrimeraB.TextChanged += CalcularResultadoBiceps;
      
        //Cresta Iliaca
        ePrimeraCL.TextChanged += CalcularResultadoCrestaIliaca;
       

        //Supraespinal
        ePrimeraSL.TextChanged += CalcularResultadoSupraespinal;
       

        //Adbominal
        ePrimeraAB.TextChanged += CalcularResultadoAdbominal;
     

        //Pectoral
        ePrimeraPEC.TextChanged += CalcularResultadoPectoral;
  

        //Axial Medial
        ePrimeraAX.TextChanged += CalcularResultadoAxialMedial;
      

        //Muslo Frontal
        ePrimeraMSF.TextChanged += CalcularResultadoMusloFrontal;


        //Pantorilla
        ePrimeraPANTO.TextChanged += CalcularResultadoPantorilla;
  

        //Circuferencias


        //Cuello
        ePrimeraCUE.TextChanged += CalcularResultadoCuello;
       

        //Brazo Relajado
        ePrimeraBRJ.TextChanged += CalcularResultadoBRelajado;
      

        //Brazo Contraido    
        ePrimeraBCO.TextChanged += CalcularResultadoBRContraido;
     

        //Antebrazo
        ePrimeraATB.TextChanged += CalcularResultadoAnteBrazo;


        //Muñeca
        ePrimeraMNC.TextChanged += CalcularResultadoMN;
   

        //Mesoesternal
        ePrimeraMSL.TextChanged += CalcularResultadoMesoesternal;
       

        //Cintura
        ePrimeraCTA.TextChanged += CalcularResultadoCintura;
     

        //Abdomen
        ePrimeraABC.TextChanged += CalcularResultadoABD;
      

        //Cadera
        ePrimeraCDR.TextChanged += CalcularResultadoCADERA;
        

        //Muslo(1cm)
        ePrimeraMCM.TextChanged += CalcularResultadoMuslo1;
       

        //Muslo Medio
        ePrimeraMMO.TextChanged += CalcularResultadoMusloMedio;


        //Pantorilla Medial
        ePrimeraPTM.TextChanged += CalcularResultadoPantoMedial;
 

        //Tobillo
        ePrimeraTLL.TextChanged += CalcularResultadoTobillo;
     

        //Diametro

        //Biepicondilio del Humero
        ePrimeraBDH.TextChanged += CalcularResultadoHUMERO;
   

        //Biestiloideo de la muñeca
        ePrimeraBDLM.TextChanged += CalcularResultadomuneca;
        

        //Bicondileo Femur
        ePrimeraBOF.TextChanged += CalcularResultadoFemur;
     

        //Bimaleolar
        ePrimeraBMLA.TextChanged += CalcularResultadoBimaleolar;



    }



    private void btnBuscar_Clicked(object sender, EventArgs e)
    {
        try
        {
            OpMySQL opMySQL = new OpMySQL();

            int playera = int.Parse(eplayera.Text);

            List<string> datosJugador = opMySQL.BuscarJugador(eUS, eConta, playera);

            if (datosJugador != null && datosJugador.Count > 0)
            {
                eNom.Text = datosJugador[0];
                eApellidoPa.Text = datosJugador[1];
                eApellidoMa.Text = datosJugador[2];
                ePosicion.Text = datosJugador[3];
                eEstatura.Text = datosJugador[4];
                eEdad.Text = datosJugador[5];

            }
            else
            {
                DisplayAlert("Error", "No se encontró ningún jugador con el número de camiseta proporcionado.", "Aceptar");
            }
        }
        catch (FormatException)
        {
            DisplayAlert("Error", "Ingrese un número de camiseta válido.", "Aceptar");
        }

    }


    //Calculos para PLIEGUES

    //Tricep
    private void CalcularResultadoTriceps(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ePrimeraP.Text))
        {
            eResulrP.Text = ""; 
            return;
        }

        if (double.TryParse(ePrimeraP.Text, out double valor1))
        {
            double resultado = tricep(valor1);
            eResulrP.Text = resultado.ToString();
        }
        else
        {
            eResulrP.Text = "Error";
        }
    }

    private double tricep(double valor1)
    {
        return valor1;
    }


    //Subescapular
    private void CalcularResultadoSubescapular(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ePrimeraS.Text))
        {
            eResultS.Text = "";
            return;
        }

        if (double.TryParse(ePrimeraS.Text, out double valor1) )
        {
            double resultado = subescapular(valor1);
            eResultS.Text = resultado.ToString();
        }
        else
        {
            eResultS.Text = "Error";
        }
    }

    private double subescapular(double valor1)
    {
        return valor1;
    }

    //Biceps
    private void CalcularResultadoBiceps(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ePrimeraB.Text))
        {
            eResulrB.Text = "";
            return;
        }

        if (double.TryParse(ePrimeraB.Text, out double valor1) )
        {
            double resultado = Biceps(valor1);
            eResulrB.Text = resultado.ToString();
        }
        else
        {
            eResulrB.Text = "Error";
        }
    }

    private double Biceps(double valor1)
    {
        return valor1;
    }

    //CrestaIliaca

    private void CalcularResultadoCrestaIliaca(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ePrimeraCL.Text))
        {
            eResultCL.Text = "";
            return;
        }

        if (double.TryParse(ePrimeraCL.Text, out double valor1) )
        {
            double resultado = CrestaIliaca(valor1);
            eResultCL.Text = resultado.ToString();
        }
        else
        {
            eResultCL.Text = "Error";
        }
    }

    private double CrestaIliaca(double valor1)
    {
        return valor1;
    }

    //Supraespinal

    private void CalcularResultadoSupraespinal(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ePrimeraSL.Text))
        {
            eResultSL.Text = "";
            return;
        }

        if (double.TryParse(ePrimeraSL.Text, out double valor1))
        {
            double resultado = Supraespinal(valor1);
            eResultSL.Text = resultado.ToString();
        }
        else
        {
            eResultSL.Text = "Error";
        }
    }

    private double Supraespinal(double valor1)
    {
        return valor1;
    }

    //Adbominal

    private void CalcularResultadoAdbominal(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ePrimeraAB.Text) )
        {
            eResultAB.Text = "";
            return;
        }

        if (double.TryParse(ePrimeraAB.Text, out double valor1))
        {
            double resultado = Adbominal(valor1);
            eResultAB.Text = resultado.ToString();
        }
        else
        {
            eResultAB.Text = "Error";
        }
    }

    private double Adbominal(double valor1)
    {
        return valor1;
    }

    //Pectoral

    private void CalcularResultadoPectoral(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ePrimeraPEC.Text))
        {
            eResultPEC.Text = "";
            return;
        }

        if (double.TryParse(ePrimeraPEC.Text, out double valor1))
        {
            double resultado = Pectoral(valor1);
            eResultPEC.Text = resultado.ToString();
        }
        else
        {
            eResultPEC.Text = "Error";
        }
    }

    private double Pectoral(double valor1)
    {
        return valor1;
    }

    //AxialMedial

    private void CalcularResultadoAxialMedial(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ePrimeraAX.Text))
        {
            eResultAX.Text = "";
            return;
        }

        if (double.TryParse(ePrimeraAX.Text, out double valor1))
        {
            double resultado = AxialMedial(valor1);
            eResultAX.Text = resultado.ToString();
        }
        else
        {
            eResultAX.Text = "Error";
        }
    }

    private double AxialMedial(double valor1)
    {
        return valor1;
    }

    //MusloFrontal

    private void CalcularResultadoMusloFrontal(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ePrimeraMSF.Text) )
        {
            eResultMSF.Text = "";
            return;
        }

        if (double.TryParse(ePrimeraMSF.Text, out double valor1) )
        {
            double resultado = MusloFrontal(valor1);
            eResultMSF.Text = resultado.ToString();
        }
        else
        {
            eResultMSF.Text = "Error";
        }
    }

    private double MusloFrontal(double valor1)
    {
        return valor1;
    }

    //Pantorilla

    private void CalcularResultadoPantorilla(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ePrimeraPANTO.Text) )
        {
            eResultPANTO.Text = "";
            return;
        }

        if (double.TryParse(ePrimeraPANTO.Text, out double valor1) )
        {
            double resultado = Pantorilla(valor1);
            eResultPANTO.Text = resultado.ToString();
        }
        else
        {
            eResultPANTO.Text = "Error";
        }
    }

    private double Pantorilla(double valor1)
    {
        return valor1;
    }

    //Calculos para CIRCUNFERENCIAS

    //Cuello
    private void CalcularResultadoCuello(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ePrimeraCUE.Text) )
        {
            eResultCUE.Text = "";
            return;
        }

        if (double.TryParse(ePrimeraCUE.Text, out double valor1) )
        {
            double resultado = Cuello(valor1);
            eResultCUE.Text = resultado.ToString();
        }
        else
        {
            eResultCUE.Text = "Error";
        }
    }

    private double Cuello(double valor1)
    {
        return valor1;
    }

    //Brazo Relajado
    private void CalcularResultadoBRelajado(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ePrimeraBRJ.Text))
        {
            eResultBRJ.Text = "";
            return;
        }

        if (double.TryParse(ePrimeraBRJ.Text, out double valor1) )
        {
            double resultado = BrazoRelajado(valor1);
            eResultBRJ.Text = resultado.ToString();
        }
        else
        {
            eResultBRJ.Text = "Error";
        }
    }

    private double BrazoRelajado(double valor1)
    {
        return valor1;
    }


    //Brazo Contraido    
    private void CalcularResultadoBRContraido(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ePrimeraBCO.Text))
        {
            eResultBCO.Text = "";
            return;
        }

        if (double.TryParse(ePrimeraBCO.Text, out double valor1))
        {
            double resultado = BrazoContraido(valor1);
            eResultBCO.Text = resultado.ToString();
        }
        else
        {
            eResultBCO.Text = "Error";
        }
    }

    private double BrazoContraido(double valor1)
    {
        return valor1;
    }

    //Antebrazo
    private void CalcularResultadoAnteBrazo(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ePrimeraATB.Text))
        {
            eResultATB.Text = "";
            return;
        }

        if (double.TryParse(ePrimeraATB.Text, out double valor1) )
        {
            double resultado = AnteBrazo(valor1);
            eResultATB.Text = resultado.ToString();
        }
        else
        {
            eResultATB.Text = "Error";
        }
    }

    private double AnteBrazo(double valor1)
    {
        return valor1;
    }

    //Muñeca
    private void CalcularResultadoMN(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ePrimeraMNC.Text))
        {
            eResultMNC.Text = "";
            return;
        }

        if (double.TryParse(ePrimeraMNC.Text, out double valor1) )
        {
            double resultado = MN(valor1);
            eResultMNC.Text = resultado.ToString();
        }
        else
        {
            eResultMNC.Text = "Error";
        }
    }

    private double MN(double valor1)
    {
        return valor1;
    }

    //Mesoesternal
    private void CalcularResultadoMesoesternal(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ePrimeraMSL.Text) )
        {
            eResultMSL.Text = "";
            return;
        }

        if (double.TryParse(ePrimeraMSL.Text, out double valor1) )
        {
            double resultado = Mesoesternal(valor1);
            eResultMSL.Text = resultado.ToString();
        }
        else
        {
            eResultMSL.Text = "Error";
        }
    }

    private double Mesoesternal(double valor1)
    {
        return valor1;
    }

    //Cintura
    private void CalcularResultadoCintura(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ePrimeraCTA.Text) )
        {
            eResultCTA.Text = "";
            return;
        }

        if (double.TryParse(ePrimeraCTA.Text, out double valor1))
        {
            double resultado = Cintura(valor1);
            eResultCTA.Text = resultado.ToString();
        }
        else
        {
            eResultCTA.Text = "Error";
        }
    }

    private double Cintura(double valor1)
    {
        return valor1;
    }


    //Abdomen
    private void CalcularResultadoABD(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ePrimeraABC.Text))
        {
            eResultABC.Text = "";
            return;
        }

        if (double.TryParse(ePrimeraABC.Text, out double valor1) )
        {
            double resultado = ABD(valor1);
            eResultABC.Text = resultado.ToString();
        }
        else
        {
            eResultABC.Text = "Error";
        }
    }

    private double ABD(double valor1)
    {
        return valor1;
    }


    //Cadera
    private void CalcularResultadoCADERA(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ePrimeraCDR.Text) )
        {
            eResultACDR.Text = "";
            return;
        }

        if (double.TryParse(ePrimeraCDR.Text, out double valor1) )
        {
            double resultado = CADERA(valor1);
            eResultACDR.Text = resultado.ToString();
        }
        else
        {
            eResultACDR.Text = "Error";
        }
    }

    private double CADERA(double valor1)
    {
        return valor1;
    }


    //Muslo(1cm)
    private void CalcularResultadoMuslo1(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ePrimeraMCM.Text) )
        {
            eResultMCM.Text = "";
            return;
        }

        if (double.TryParse(ePrimeraMCM.Text, out double valor1) )
        {
            double resultado = muslo1(valor1);
            eResultMCM.Text = resultado.ToString();
        }
        else
        {
            eResultMCM.Text = "Error";
        }
    }

    private double muslo1(double valor1)
    {
        return valor1;
    }


    //Muslo Medio
    private void CalcularResultadoMusloMedio(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ePrimeraMMO.Text) )
        {
            eResultMMO.Text = "";
            return;
        }

        if (double.TryParse(ePrimeraMMO.Text, out double valor1) )
        {
            double resultado = MusloMedio(valor1);
            eResultMMO.Text = resultado.ToString();
        }
        else
        {
            eResultMMO.Text = "Error";
        }
    }

    private double MusloMedio(double valor1)
    {
        return valor1;
    }
    //Pantorilla Medial

    private void CalcularResultadoPantoMedial(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ePrimeraPTM.Text) )
        {
            eResultPTM.Text = "";
            return;
        }

        if (double.TryParse(ePrimeraPTM.Text, out double valor1) )
        {
            double resultado = PantoMedial(valor1);
            eResultPTM.Text = resultado.ToString();
        }
        else
        {
            eResultPTM.Text = "Error";
        }
    }

    private double PantoMedial(double valor1)
    {
        return valor1;
    }

    //Tobillo

    private void CalcularResultadoTobillo(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ePrimeraTLL.Text))
        {
            eResultTLL.Text = "";
            return;
        }

        if (double.TryParse(ePrimeraTLL.Text, out double valor1) )
        {
            double resultado = Tobillo(valor1);
            eResultTLL.Text = resultado.ToString();
        }
        else
        {
            eResultTLL.Text = "Error";
        }
    }

    private double Tobillo(double valor1)
    {
        return valor1;
    }


    //Calculos para DIAMETRO

    //Biepicondilio del Humero
    private void CalcularResultadoHUMERO(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ePrimeraBDH.Text))
        {
            eResultBDH.Text = "";
            return;
        }

        if (double.TryParse(ePrimeraBDH.Text, out double valor1) )
        {
            double resultado = HUMERO(valor1);
            eResultBDH.Text = resultado.ToString();
        }
        else
        {
            eResultBDH.Text = "Error";
        }
    }

    private double HUMERO(double valor1)
    {
        return valor1;
    }


    //Biestiloideo de la muñeca
    private void CalcularResultadomuneca(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ePrimeraBDLM.Text) )
        {
            eResultBDLM.Text = "";
            return;
        }

        if (double.TryParse(ePrimeraBDLM.Text, out double valor1))
        {
            double resultado = muneca(valor1);
            eResultBDLM.Text = resultado.ToString();
        }
        else
        {
            eResultBDLM.Text = "Error";
        }
    }

    private double muneca(double valor1)
    {
        return valor1;
    }


    //Bicondileo Femur
    private void CalcularResultadoFemur(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ePrimeraBOF.Text) )
        {
            eResultBOF.Text = "";
            return;
        }

        if (double.TryParse(ePrimeraBOF.Text, out double valor1) )
        {
            double resultado = Femur(valor1);
            eResultBOF.Text = resultado.ToString();
        }
        else
        {
            eResultBOF.Text = "Error";
        }
    }

    private double Femur(double valor1)
    {
        return valor1;
    }


    //Bimaleolar
    private void CalcularResultadoBimaleolar(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ePrimeraBMLA.Text))
        {
            eResultBMLA.Text = "";
            return;
        }

        if (double.TryParse(ePrimeraBMLA.Text, out double valor1) )
        {
            double resultado = Bimaleolar(valor1);
            eResultBMLA.Text = resultado.ToString();
        }
        else
        {
            eResultBMLA.Text = "Error";
        }
    }

    private double Bimaleolar(double valor1)
    {
        return valor1;
    }


    //Boton para alamcenaar todos los datos 
    //Error cual no se pero hay error
    private async void btnGuardarRefistro_Clicked(object sender, EventArgs e)
    {
        try {

            string eidnutri = eUS;
            int idNutri = int.Parse(eidnutri);

            string Idplayer = eplayera.Text;
            int IdPlayer = int.Parse(Idplayer);

            string peso = ePeso.Text;
            decimal Peso = decimal.Parse(peso);

            string esta = eEstatura.Text;
            decimal Esta = decimal.Parse(esta);

            string triceps = eResulrP.Text;
            decimal Triceps = decimal.Parse(triceps);

            string subescapular = eResultS.Text;
            decimal Subescapular = decimal.Parse(subescapular);

            string biceps = eResulrB.Text;
            decimal Biceps = decimal.Parse(biceps);

            string crestaIliaca = eResultCL.Text;
            decimal CrestaIliaca = decimal.Parse(crestaIliaca);

            string supraespiral = eResultSL.Text;
            decimal Supraespiral = decimal.Parse(supraespiral);

            string abdominal = eResultAB.Text;
            decimal Abdominal = decimal.Parse(abdominal);

            string pectoral = eResultPEC.Text;
            decimal Pectoral = decimal.Parse(pectoral);

            string axialMedial = eResultAX.Text;
            decimal AxialMedial = decimal.Parse(axialMedial);

            string musloFrontal = eResultMSF.Text;
            decimal MusloFrontal = decimal.Parse (musloFrontal);

            string pantorilla = eResultPANTO.Text;
            decimal Pantorilla = decimal.Parse(pantorilla);

            string cuello = eResultCUE.Text;
            decimal Cuello = decimal.Parse(cuello);

            string brazoRelajado = eResultBRJ.Text;
            decimal BrazoRelajado = decimal.Parse(brazoRelajado);

            string brazoContraiodo = eResultBCO.Text;
            decimal BrazoContraiodo = decimal.Parse(brazoContraiodo);

            string antebrazo = eResultATB.Text;
            decimal Antebrazo = decimal.Parse(antebrazo);

            string muneca = eResultMNC.Text;
            decimal Muneca = decimal.Parse(muneca);

            string mesoesternal = eResultMSL.Text;
            decimal Mesoesternal = decimal.Parse(mesoesternal);

            string cintura = eResultCTA.Text;
            decimal Cintura = decimal.Parse(cintura);   

            string abdomen = eResultABC.Text;
            decimal Abdomen = decimal.Parse(abdomen);

            string cadera = eResultACDR.Text;
            decimal Cadera = decimal.Parse(cadera);

            string muslo1cm = eResultMCM.Text;
            decimal Muslo1cm = decimal.Parse(muslo1cm);

            string musloMedio = eResultMMO.Text;
            decimal MusloMedio = decimal.Parse(musloMedio);

            string pantorillaMedial = eResultPTM.Text;
            decimal PantorillaMedial = decimal.Parse(pantorillaMedial);

            string tobillo = eResultTLL.Text;
            decimal Tobillo = decimal.Parse(tobillo);

            string biepicondiliodelhumero = eResultBDH.Text;
            decimal Biepicondiliodelhumero = decimal.Parse(biepicondiliodelhumero);

            string biestiloidedelamuneca = eResultBDLM.Text;
            decimal Biestiloidedelamuneca = decimal.Parse(biestiloidedelamuneca);

            string bicondileFemur = eResultBOF.Text;
            decimal BicondileFemur = decimal.Parse(bicondileFemur);

            string bimaleolar = eResultBMLA.Text;
            decimal Bimaleolar = decimal.Parse(bimaleolar);

            OpMySQL opMySQL = new OpMySQL();
            bool result = opMySQL.MedidasAntro(Peso, Esta, Triceps, Subescapular, Biceps, CrestaIliaca, Supraespiral, Abdominal, Pectoral, AxialMedial, MusloFrontal, Pantorilla,
                Cuello, BrazoRelajado, BrazoContraiodo, Antebrazo, Muneca, Mesoesternal, Cintura, Abdomen, Cadera, Muslo1cm, MusloMedio, PantorillaMedial, Tobillo, Biepicondiliodelhumero,
                 Biestiloidedelamuneca, BicondileFemur, Bimaleolar, idNutri, IdPlayer, eUS, eConta);

        

            if (result)
            {
                await DisplayAlert("Listo","Datos del usuario Guardado con exito","Finalizar");
                clear();
            }

            else
            {
                await DisplayAlert("Error", "Sucedio Algun Error al rellenar los datos", "OK");

            }

        }
        catch (Exception ex)
        {
            await DisplayAlert("ERROR",$"OCURRIO UN ERROR {ex.Message}","OK");
        }
    }


    public void clear()
    {
        //Agregar limpieza en los datos medios
        eNom.Text = string.Empty;
        eApellidoPa.Text = string.Empty;
        eApellidoMa.Text = string.Empty;
        ePosicion.Text = string.Empty;
        eEstatura.Text = string.Empty;
        eEdad.Text = string.Empty;
        ePeso.Text = string.Empty;
        eplayera.Text = string.Empty;
        eResulrP.Text = string.Empty;
        eResultS.Text = string.Empty;
        eResulrB.Text = string.Empty;
        eResultCL.Text = string.Empty;
        eResultSL.Text = string.Empty;
        eResultAB.Text = string.Empty;
        eResultPEC.Text = string.Empty;
        eResultAX.Text = string.Empty;
        eResultMSF.Text = string.Empty;
        eResultPANTO.Text = string.Empty;
        eResultCUE.Text = string.Empty;
        eResultBRJ.Text = string.Empty;
        eResultBCO.Text = string.Empty;
        eResultATB.Text = string.Empty;
        eResultMNC.Text = string.Empty;
        eResultMSL.Text = string.Empty;
        eResultCTA.Text = string.Empty;
        eResultABC.Text = string.Empty;
        eResultACDR.Text = string.Empty;
        eResultMCM.Text = string.Empty;
        eResultMMO.Text = string.Empty;
        eResultPTM.Text = string.Empty;
        eResultTLL.Text = string.Empty;
        eResultBDH.Text = string.Empty;
        eResultBDLM.Text = string.Empty;
        eResultBOF.Text = string.Empty;
        eResultBMLA.Text = string.Empty;

        ePrimeraP.Text = string.Empty;
        ePrimeraS.Text = string.Empty;
        ePrimeraB.Text = string.Empty;
        ePrimeraCL.Text = string.Empty;
        ePrimeraSL.Text = string.Empty;
        ePrimeraAB.Text = string.Empty;
        ePrimeraPEC.Text = string.Empty;
        ePrimeraAX.Text = string.Empty;
        ePrimeraMSF.Text = string.Empty;
        ePrimeraPANTO.Text = string.Empty;
        ePrimeraCUE.Text = string.Empty;
        ePrimeraBRJ.Text = string.Empty;
        ePrimeraBCO.Text = string.Empty;
        ePrimeraATB.Text = string.Empty;
        ePrimeraMNC.Text = string.Empty;
        ePrimeraMSL.Text = string.Empty;
        ePrimeraMSL.Text = string.Empty;
        ePrimeraCTA.Text = string.Empty;
        ePrimeraABC.Text = string.Empty;
        ePrimeraCDR.Text = string.Empty;
        ePrimeraMCM.Text = string.Empty;
        ePrimeraMMO.Text = string.Empty;
        ePrimeraPTM.Text = string.Empty;
        ePrimeraTLL.Text = string.Empty;
        ePrimeraBDH.Text = string.Empty;
        ePrimeraBDLM.Text = string.Empty;
        ePrimeraBOF.Text = string.Empty;
        ePrimeraBMLA.Text = string.Empty;



    }
}
