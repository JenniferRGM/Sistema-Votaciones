using System;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace Sistema_Votaciones
{
    public partial class Resultados : System.Web.UI.Page
    {
        // Método que se ejecuta cuando la página se carga
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar si la página se carga por primera vez y no es una recarga (postback)
            if (!IsPostBack)
            {
                // Cargar los resultados de las elecciones
                CargarResultados();
            }
        }

        // Método para cargar los resultados de las elecciones
        private void CargarResultados()
        {
            // Obtener la cadena de conexión de la configuración web
            string connectionString = WebConfigurationManager.ConnectionStrings["BDVotacionesConnectionString"].ConnectionString;

            // Usar la conexión con la base de datos
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Consulta SQL para obtener los resultados de las elecciones
                string query = @"
                    SELECT C.Cedula, C.Nombre, C.Apellido1, C.Apellido2, C.Direccion, P.Nombre AS Partido, R.CantidadVotos, FORMAT(R.Porcentaje, 'N2') AS Porcentaje
                    FROM Resultados R
                    JOIN Candidatos C ON R.IDCandidato = C.Id
                    JOIN Partidos P ON C.IDPartido = P.Id
                    ORDER BY R.Porcentaje DESC";

                // Crear el comando SQL con la consulta
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Abrir la conexión
                    con.Open();

                    // Ejecutar la consulta y obtener los resultados
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Asignar los resultados al GridView para mostrarlos en la página
                    gvResultados.DataSource = reader;
                    gvResultados.DataBind();

                    // Cerrar el reader después de usarlo
                    reader.Close();
                }

                // Consulta SQL para obtener el ganador de las elecciones
                string winnerQuery = @"
                    SELECT TOP 1 C.Nombre, C.Apellido1, C.Apellido2, P.Nombre AS Partido
                    FROM Resultados R
                    JOIN Candidatos C ON R.IDCandidato = C.Id
                    JOIN Partidos P ON C.IDPartido = P.Id
                    ORDER BY R.Porcentaje DESC";

                // Crear el comando SQL para obtener el ganador
                using (SqlCommand winnerCmd = new SqlCommand(winnerQuery, con))
                {
                    // Ejecutar la consulta para obtener el ganador
                    SqlDataReader winnerReader = winnerCmd.ExecuteReader();

                    // Verificar si hay resultados y asignar el nombre del ganador al label
                    if (winnerReader.Read())
                    {
                        string ganador = $"{winnerReader["Nombre"]} {winnerReader["Apellido1"]} {winnerReader["Apellido2"]} del partido {winnerReader["Partido"]}";
                        lblGanador.InnerText = ganador;
                    }

                    // Cerrar el reader después de usarlo
                    winnerReader.Close();
                }
            }
        }
    }
}
