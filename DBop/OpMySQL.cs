using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using MySqlConnector;

namespace GALTMA.DBop
{
    internal class OpMySQL
    {


        /* Creamos la conexion*/
        public string SRADD = "192.168.0.9";
        public string db = "galtma";

        public MySqlConnection condb(string NombreUsuario, string UsContrasena)
        {
            string connectionString = $"Server={SRADD};Database={db};User={NombreUsuario};Password={UsContrasena};SslMode=None;";
            return new MySqlConnection(connectionString);
        }

        /* Registros de Usuarios */
        /* Verificar Credenciales de los Usuarios */
        public bool veriCredencial(string NombreUsuario, string UsContrasena)
        {

            using (MySqlConnection conexion = condb(NombreUsuario, UsContrasena))
            {

                try
                {
                    conexion.Open();
                    Console.WriteLine("Conexión exitosa");
                    return true;
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Error al conectar con la base de datos: " + ex.Message);
                    return false;
                }
            }

        }

        /* Inicion de sesion para Jugadores y Nutriologos */

        public bool InSesion(string NombreUsuario, string UsContrasena)
        {

            using (MySqlConnection conex = condb(NombreUsuario, UsContrasena))
            {
                try
                {
                    if (VeriJugador(NombreUsuario, UsContrasena))
                    {
                        conex.Open();
                        return true;
                    }
                    else if (VeriNutri(NombreUsuario, UsContrasena))
                    {
                        conex.Open();
                        return true;
                    }
                    else
                    {
                        Console.Write("Error en la conexion");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al conectar con la base de datos: " + ex.Message);
                    return false;
                }

            }


        }

        public bool VeriJugador(string NombreUsuario, string UsContrasena)
        {
            using (MySqlConnection conex = condb(NombreUsuario, UsContrasena))
            {

                try
                {
                    conex.Open();
                    string cd = "SELECT COUNT(*) FROM jugador WHERE Playera = @playera AND contrasena = @contrasena";

                    using (var comando = new MySqlCommand(cd, conex))
                    {
                        comando.Parameters.AddWithValue("@playera", NombreUsuario);
                        comando.Parameters.AddWithValue("@contrasena", UsContrasena);

                        int resultado = Convert.ToInt32(comando.ExecuteScalar());

                        return resultado > 0;
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Error al conectar con la base de datos: " + ex.Message);
                    return false;
                }
            }

        }

        public bool VeriNutri(string NombreUsuario, string UsContrasena)
        {
            using (MySqlConnection conex = condb(NombreUsuario, UsContrasena))
            {

                try
                {
                    conex.Open();
                    string cd = "SELECT COUNT(*) FROM nutriologo WHERE id_Nutri = @id_Nutri AND contrasena = @contrasena";

                    using (var comando = new MySqlCommand(cd, conex))
                    {
                        comando.Parameters.AddWithValue("@id_Nutri", NombreUsuario);
                        comando.Parameters.AddWithValue("@contrasena", UsContrasena);

                        int resultado = Convert.ToInt32(comando.ExecuteScalar());

                        return resultado > 0;
                    }

                }

                catch (Exception ex)
                {
                    Console.WriteLine("Error al conectar con la base de datos: " + ex.Message);
                    return false;
                }
            }

        }


        /* Apartado para registro de usuario */

        public bool AgregarJugadores(string NombreUsuario, string UsContrasena, int playera, string Nombre, string ApellidoPaterno, string ApellidoMaterno, string Posicion, decimal Estatura, string contrasena, int edad)
        {
            MySqlConnection conexion = condb(NombreUsuario, UsContrasena);
            conexion.Open();

            try
            {
                string cd = "Insert Into Jugador(Playera, Nombre, ApellidoPaterno, ApellidoMaterno, posicion, Estatura, Contrasena, edad) " +
                    " Values(@playera, @Nombre, @ApellidoPaterno, @ApellidoMaterno, @Posicion, @Estatura, @contrasena, @edad) ";
                MySqlCommand com;
                com = new MySqlCommand(cd, conexion);
                com.Parameters.AddWithValue("@playera", playera);
                com.Parameters.AddWithValue("@Nombre", Nombre);
                com.Parameters.AddWithValue("@ApellidoPaterno", ApellidoPaterno);
                com.Parameters.AddWithValue("@ApellidoMaterno", ApellidoMaterno);
                com.Parameters.AddWithValue("@Posicion", Posicion);
                com.Parameters.AddWithValue("@Estatura", Estatura);
                com.Parameters.AddWithValue("@contrasena", contrasena);
                com.Parameters.AddWithValue("@edad", edad);

                com.ExecuteNonQuery();
                conexion.Close();
                return true;
            }

            catch (Exception ex)
            {

                Console.WriteLine("Se produjo un error" + ex.Message);
                return false;

            }
        }

        /* Apartado registro del nutriologo */

        public bool AgregarNutri(string NombreUsuario, string UsContrasena, int idNutri, string Nombre, string ApellidoPaterno, string ApellidoMaterno, string contrasena)
        {
            MySqlConnection conexion = condb(NombreUsuario, UsContrasena);
            conexion.Open();

            try
            {
                string cd = "Insert Into Nutriologo(id_Nutri, Nombre, ApellidoPaterno, ApellidoMaterno, Contrasena)" +
                    " Values(@id_Nutri, @Nombre, @ApellidoPaterno, @ApellidoMaterno, @Contrasena)";
                MySqlCommand com;
                com = new MySqlCommand(cd, conexion);
                com.Parameters.AddWithValue("@id_Nutri", idNutri);
                com.Parameters.AddWithValue("@Nombre", Nombre);
                com.Parameters.AddWithValue("@ApellidoPaterno", ApellidoPaterno);
                com.Parameters.AddWithValue("@ApellidoMaterno", ApellidoMaterno);
                com.Parameters.AddWithValue("@Contrasena", contrasena);
                com.ExecuteNonQuery();
                conexion.Close();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error" + ex.Message);
                return false;

            }

        }

        /* Dar de baja usuario */

        public List<string> BuscarJugador(string NombreUsuario, string UsContrasena, int playera)
        {
            List<string> datosJugador = new List<string>();
            try
            {
                using (MySqlConnection conexion = condb(NombreUsuario, UsContrasena))
                {
                    conexion.Open();
                    string consulta = "SELECT * FROM jugador WHERE playera = @playera";
                    using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@playera", playera);

                        using (MySqlDataReader reader = comando.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                datosJugador.Add(reader["Nombre"].ToString());
                                datosJugador.Add(reader["ApellidoPaterno"].ToString());
                                datosJugador.Add(reader["ApellidoMaterno"].ToString());
                                datosJugador.Add(reader["posicion"].ToString());
                                datosJugador.Add(reader["Estatura"].ToString());
                                datosJugador.Add(reader["edad"].ToString());
                            }
                        }
                    }
                }
                return datosJugador;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error: " + ex.Message);
                return null;
            }
        }

        public bool eliminaJu(string NombreUsuario, string UsContrasena, int playera)
        {
            try
            {
                using (MySqlConnection conexion = condb(NombreUsuario, UsContrasena))
                {
                    conexion.Open();

                    // Eliminar el jugador
                    string consultaJugador = "DELETE FROM jugador WHERE playera = @playera";
                    using (MySqlCommand comandoJugador = new MySqlCommand(consultaJugador, conexion))
                    {
                        comandoJugador.Parameters.AddWithValue("@playera", playera);

                        int filasAfectadasJugador = comandoJugador.ExecuteNonQuery();

                        // Si se eliminó el jugador, eliminar el rol asociado
                        if (filasAfectadasJugador > 0)
                        {
                            string sqlEliminarRol = $"DROP USER '{playera}'@'%';";
                            MySqlCommand comandoEliminarRol = new MySqlCommand(sqlEliminarRol, conexion);
                            comandoEliminarRol.ExecuteNonQuery();
                        }

                        return filasAfectadasJugador > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error: " + ex.Message);
                return false;
            }
        }



        /* Registro de datos de Nutriologo para jugadores */

        public bool MedidasAntro(decimal peso, decimal esta, decimal triceps, decimal Subescapular, decimal Biceps, decimal CrestaIliaca, decimal SupraEspiral,
            decimal Abdominal, decimal Pectoral, decimal AxialMedial, decimal MusloFrontal, decimal Pantorilla, decimal cuello, decimal BrazoRelajado, decimal BrazoContraido, decimal Antebrazo,
            decimal Muneca, decimal Mesoesternal, decimal Cintura, decimal abc, decimal cadera, decimal muslo1cm, decimal MusloMedio, decimal PantoMedia, decimal tobillo, decimal biepihumero, decimal biesmuneca,
            decimal bioFemur, decimal Bimaleolar, int idnutri, int idjuga, string UsuarioNutri, string UsContrasena)
        {
    
            using (MySqlConnection conexion = condb(UsuarioNutri, UsContrasena))
            {
                try
                {
                    conexion.Open();
                    string cd = "Insert Into mediciones(Peso, Esta, Triceps, Subescapular, Biceps, CrestaIliaca, Supraespinal, Abdominal, Pectoral, AxialMedial, MusloFrontal, Pantorilla, Cuello, BrazoRelajado, BrazoContraido, Antebrazo, Muneca, Mesoesternal, Cintura, Abdomen, Cadera, Muslo1cm, MusloMedio, PantorillaMedial, Tobillo, Biepicondiliodelhumero, Biestiloideodelamuneca, BicondileoFemur, Bimaleolar, idNutri, idJuga) " +
                                              "Values (@Peso, @Esta, @Triceps, @Subescapular, @Biceps, @CrestaIliaca, @Supraespinal, @Abdominal, @Pectoral, @AxialMedial, @MusloFrontal, @Pantorilla, @Cuello, @BrazoRelajado, @BrazoContraido, @Antebrazo, @Muneca, @Mesoesternal, @Cintura, @Abdomen, @Cadera, @Muslo1cm, @MusloMedio, @PantorillaMedial, @Tobillo, @Biepicondiliodelhumero, @Biestiloideodelamuneca, @BicondileoFemur, @Bimaleolar, @idNutri, @idJuga)";
                    MySqlCommand com; 
                    com = new MySqlCommand(cd, conexion);
                    com.Parameters.AddWithValue("@Peso", peso);
                    com.Parameters.AddWithValue("@Esta", esta);
                    com.Parameters.AddWithValue("@Triceps", triceps);
                    com.Parameters.AddWithValue("@Subescapular", Subescapular);
                    com.Parameters.AddWithValue("@Biceps", Biceps);
                    com.Parameters.AddWithValue("@CrestaIliaca", CrestaIliaca);
                    com.Parameters.AddWithValue("@Supraespinal", SupraEspiral);
                    com.Parameters.AddWithValue("@Abdominal", Abdominal);
                    com.Parameters.AddWithValue("@Pectoral", Pectoral);
                    com.Parameters.AddWithValue("@AxialMedial", AxialMedial);
                    com.Parameters.AddWithValue("@MusloFrontal", MusloFrontal);
                    com.Parameters.AddWithValue("@Pantorilla", Pantorilla);
                    com.Parameters.AddWithValue("@Cuello", cuello);
                    com.Parameters.AddWithValue("@BrazoRelajado", BrazoRelajado);
                    com.Parameters.AddWithValue("@BrazoContraido", BrazoContraido);
                    com.Parameters.AddWithValue("@Antebrazo", Antebrazo);
                    com.Parameters.AddWithValue("@Muneca", Muneca);
                    com.Parameters.AddWithValue("@Mesoesternal", Mesoesternal);
                    com.Parameters.AddWithValue("@Cintura", Cintura);
                    com.Parameters.AddWithValue("@Abdomen", abc);
                    com.Parameters.AddWithValue("@Cadera", cadera);
                    com.Parameters.AddWithValue("@Muslo1cm", muslo1cm);
                    com.Parameters.AddWithValue("@MusloMedio", MusloMedio);
                    com.Parameters.AddWithValue("@PantorillaMedial", PantoMedia);
                    com.Parameters.AddWithValue("@Tobillo", tobillo);
                    com.Parameters.AddWithValue("@Biepicondiliodelhumero", biepihumero);
                    com.Parameters.AddWithValue("@Biestiloideodelamuneca", biesmuneca);
                    com.Parameters.AddWithValue("@BicondileoFemur", bioFemur);
                    com.Parameters.AddWithValue("@Bimaleolar", Bimaleolar);
                    com.Parameters.AddWithValue("@idNutri", idnutri);
                    com.Parameters.AddWithValue("@idJuga", idjuga);
                    com.ExecuteNonQuery();
                    conexion.Close();

                    return true;
                }
                catch (MySqlException sqlEx)
                {
                    Console.WriteLine("Error de SQL: " + sqlEx.Message);
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error general: " + ex.Message);
                    return false;
                }
            }
    

            
        }

        /* Roles */

        public Boolean crearlRol(string NombreUsuario, string UsContrasena, int playera, string contrasena)
        {
            Boolean Error = true;
            try
            {
                using (MySqlConnection conn = condb(NombreUsuario, UsContrasena))
                {
                    conn.Open();

                    string sql = $"CREATE USER '{playera}'@'%'  IDENTIFIED WITH mysql_native_password BY  '{contrasena}';" +
                                 $"GRANT ALL PRIVILEGES ON galtma.* TO '{playera}'@'%';";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error: " + ex.Message);
                Error = false;
            }
            return Error;
        }


        /* Mostrar Resultados de las mediciones */

        public DataTable MostrarResul(string NombreUsuario, string Contrasena, int idJuga)
        {
            DataTable MSRT = new DataTable();

            using (MySqlConnection conn = condb(NombreUsuario, Contrasena))
            {
                conn.Open();
                try
                {
                    string query = "Select Peso, Esta, Triceps, Subescapular, Biceps, CrestaIliaca, Supraespinal, Abdominal, Pectoral, " +
                        " AxialMedial, MusloFrontal, Pantorilla, Cuello, BrazoRelajado, BrazoContraido, Antebrazo, Muneca, Mesoesternal, Cintura, " +
                        " Abdomen, Cadera, Muslo1cm, MusloMedio, PantorillaMedial," +
                        " Tobillo, Biepicondiliodelhumero, Biestiloideodelamuneca, BicondileoFemur, Bimaleolar  From mediciones where idJuga=@idJuga";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idJuga", idJuga);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            MSRT.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Se produjo un error: " + ex.Message);
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }

            return MSRT;
        }


        //Traerme Mediciones expeficicas 
        public List<string> BuscarUltimaMedicion(string nombreUsuario, string usContrasena, int idJuga)
        {
            List<string> datosMedicion = new List<string>();
            try
            {
                using (MySqlConnection conexion = condb(nombreUsuario, usContrasena))
                {
                    conexion.Open();

                    string consultaUltimoRenglon = "SELECT MAX(renglon) FROM mediciones WHERE IdJuga = @IdJuga";
                    int ultimoRenglon = 0;

                    using (MySqlCommand comandoUltimoRenglon = new MySqlCommand(consultaUltimoRenglon, conexion))
                    {
                        comandoUltimoRenglon.Parameters.AddWithValue("@IdJuga", idJuga);

                        object result = comandoUltimoRenglon.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            ultimoRenglon = Convert.ToInt32(result);
                        }
                    }

                    if (ultimoRenglon > 0)
                    {
                        string consultaMediciones = "SELECT Jugador.Nombre, mediciones.Biceps, mediciones.Triceps, mediciones.CrestaIliaca, mediciones.Subescapular FROM mediciones inner join Jugador on Jugador.Playera = mediciones.Idjuga WHERE IdJuga = @IdJuga AND renglon = @Renglon";
                        using (MySqlCommand comandoMediciones = new MySqlCommand(consultaMediciones, conexion))
                        {
                            comandoMediciones.Parameters.AddWithValue("@IdJuga", idJuga);
                            comandoMediciones.Parameters.AddWithValue("@Renglon", ultimoRenglon);

                            using (MySqlDataReader reader = comandoMediciones.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    datosMedicion.Add(reader["Nombre"].ToString());
                                    datosMedicion.Add(reader["Biceps"].ToString());
                                    datosMedicion.Add(reader["Triceps"].ToString());
                                    datosMedicion.Add(reader["CrestaIliaca"].ToString());
                                    datosMedicion.Add(reader["Subescapular"].ToString());

                                }
                            }
                        }
                    }
                }
                return datosMedicion;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error: " + ex.Message);
                return null;
            }
        }

        //Buscamos el peso
        public List<string> BuscarPeso(string nombreUsuario, string usContrasena, int idJuga)
        {
            List<string> datosMedicion = new List<string>();
            try
            {
                using (MySqlConnection conexion = condb(nombreUsuario, usContrasena))
                {
                    conexion.Open();

                    string consultaUltimoRenglon = "SELECT MAX(renglon) FROM mediciones WHERE IdJuga = @IdJuga";
                    int ultimoRenglon = 0;

                    using (MySqlCommand comandoUltimoRenglon = new MySqlCommand(consultaUltimoRenglon, conexion))
                    {
                        comandoUltimoRenglon.Parameters.AddWithValue("@IdJuga", idJuga);

                        object result = comandoUltimoRenglon.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            ultimoRenglon = Convert.ToInt32(result);
                        }
                    }

                    if (ultimoRenglon > 0)
                    {
                        string consultaMediciones = "SELECT mediciones.Peso FROM mediciones inner join Jugador on Jugador.Playera = mediciones.Idjuga WHERE IdJuga = @IdJuga AND renglon = @Renglon";
                        using (MySqlCommand comandoMediciones = new MySqlCommand(consultaMediciones, conexion))
                        {
                            comandoMediciones.Parameters.AddWithValue("@IdJuga", idJuga);
                            comandoMediciones.Parameters.AddWithValue("@Renglon", ultimoRenglon);

                            using (MySqlDataReader reader = comandoMediciones.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    datosMedicion.Add(reader["Peso"].ToString());
                                   

                                }
                            }
                        }
                    }
                }
                return datosMedicion;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error: " + ex.Message);
                return null;
            }
        }

        //Buscar medidas para mesomorfia
        public List<string> mesoforia(string nombreUsuario, string usContrasena, int idJuga)
        {
            List<string> meso = new List<string>();
            try
            {
                using (MySqlConnection conexion = condb(nombreUsuario, usContrasena))
                {
                    conexion.Open();

                    string consultaUltimoRenglon = "SELECT MAX(renglon) FROM mediciones WHERE IdJuga = @IdJuga";
                    int ultimoRenglon = 0;

                    using (MySqlCommand comandoUltimoRenglon = new MySqlCommand(consultaUltimoRenglon, conexion))
                    {
                        comandoUltimoRenglon.Parameters.AddWithValue("@IdJuga", idJuga);

                        object result = comandoUltimoRenglon.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            ultimoRenglon = Convert.ToInt32(result);
                        }
                    }

                    if (ultimoRenglon > 0)
                    {
                        string consultaMediciones = "SELECT mediciones.Biepicondiliodelhumero, mediciones.Biestiloideodelamuneca, mediciones.BicondileoFemur, mediciones.Bimaleolar, mediciones.BrazoContraido, mediciones.PantorillaMedial, mediciones.Esta FROM mediciones inner join Jugador on Jugador.Playera = mediciones.Idjuga WHERE IdJuga = @IdJuga AND renglon = @Renglon";
                        using (MySqlCommand comandoMediciones = new MySqlCommand(consultaMediciones, conexion))
                        {
                            comandoMediciones.Parameters.AddWithValue("@IdJuga", idJuga);
                            comandoMediciones.Parameters.AddWithValue("@Renglon", ultimoRenglon);

                            using (MySqlDataReader reader = comandoMediciones.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    meso.Add(reader["Biepicondiliodelhumero"].ToString());
                                    meso.Add(reader["Biestiloideodelamuneca"].ToString());
                                    meso.Add(reader["BicondileoFemur"].ToString());
                                    meso.Add(reader["Bimaleolar"].ToString());
                                    meso.Add(reader["BrazoContraido"].ToString());
                                    meso.Add(reader["PantorillaMedial"].ToString());
                                    meso.Add(reader["Esta"].ToString());

                                }
                            }
                        }
                    }
                }
                return meso;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error: " + ex.Message);
                return null;
            }
        }

        //Buscar medidas para ecto
        public List<string> ecto(string nombreUsuario, string usContrasena, int idJuga)
        {
            List<string> meso = new List<string>();
            try
            {
                using (MySqlConnection conexion = condb(nombreUsuario, usContrasena))
                {
                    conexion.Open();

                    string consultaUltimoRenglon = "SELECT MAX(renglon) FROM mediciones WHERE IdJuga = @IdJuga";
                    int ultimoRenglon = 0;

                    using (MySqlCommand comandoUltimoRenglon = new MySqlCommand(consultaUltimoRenglon, conexion))
                    {
                        comandoUltimoRenglon.Parameters.AddWithValue("@IdJuga", idJuga);

                        object result = comandoUltimoRenglon.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            ultimoRenglon = Convert.ToInt32(result);
                        }
                    }

                    if (ultimoRenglon > 0)
                    {
                        string consultaMediciones = "SELECT mediciones.peso, mediciones.esta  FROM mediciones inner join Jugador on Jugador.Playera = mediciones.Idjuga WHERE IdJuga = @IdJuga AND renglon = @Renglon";
                        using (MySqlCommand comandoMediciones = new MySqlCommand(consultaMediciones, conexion))
                        {
                            comandoMediciones.Parameters.AddWithValue("@IdJuga", idJuga);
                            comandoMediciones.Parameters.AddWithValue("@Renglon", ultimoRenglon);

                            using (MySqlDataReader reader = comandoMediciones.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    meso.Add(reader["peso"].ToString());
                                    meso.Add(reader["esta"].ToString());
                               
                                }
                            }
                        }
                    }
                }
                return meso;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error: " + ex.Message);
                return null;
            }
        }

        //Insetar Resultados 

        public bool Resultados( string nombreUsuario, string UsContrasena, double Agua, double grasa, double musculoPorce, 
            double MasaosPorce, double masresidual, double endo, double meso, double ecto, int juga )
        {

            using (MySqlConnection conexion = condb(nombreUsuario, UsContrasena))
            {
                try
                {
                    conexion.Open();
                    string cd = "Insert into resultados(AguaPorce, GrasaPorce, MusculoPorce, MasaOseaPorce, MasaResidualPorce, Endomorfia, Mesomorfia, Ectomorfia, idJuga) " +
                        "values (@AguaPorce, @GrasaPorce, @MusculoPorce, @MasaOseaPorce, @MasaResidualPorce, @Endomorfia, @Mesomorfia, @Ectomorfia, @idJuga)";
                    MySqlCommand com;
                    com = new MySqlCommand(cd, conexion);
                    com.Parameters.AddWithValue("@AguaPorce", Agua);
                    com.Parameters.AddWithValue("@GrasaPorce", grasa);
                    com.Parameters.AddWithValue("@MusculoPorce", musculoPorce);
                    com.Parameters.AddWithValue("@MasaOseaPorce", MasaosPorce);
                    com.Parameters.AddWithValue("@MasaResidualPorce", masresidual);
                    com.Parameters.AddWithValue("@Endomorfia", endo);
                    com.Parameters.AddWithValue("@Mesomorfia", meso);
                    com.Parameters.AddWithValue("@Ectomorfia", ecto);
                    com.Parameters.AddWithValue("@idJuga", juga );

                    com.ExecuteNonQuery();
                    conexion.Close();

                    return true;
                }
                catch (MySqlException sqlEx)
                {
                    Console.WriteLine("Error de SQL: " + sqlEx.Message);
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error general: " + ex.Message);
                    return false;
                }
            }



        }

        //Mostrar Resultados de tipo de cuerpo
        public DataTable mostrartc(string NombreUsuario, string Contrasena, int IdJuga)
        {
            DataTable mostrartc = new DataTable();

            using (MySqlConnection conn = condb(NombreUsuario, Contrasena))
            {
                conn.Open();
                try
                {
                    string query = "SELECT AguaPorce, GrasaPorce, MusculoPorce, MasaOseaPorce, MasaResidualPorce, Endomorfia, Mesomorfia, Ectomorfia FROM resultados WHERE idJuga = @idJuga";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idJuga", IdJuga); 

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            mostrartc.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Se produjo un error: " + ex.Message);
                    throw; 
                }
                finally
                {
                    conn.Close(); 
                }
            }

            return mostrartc;
        }


    }
}
