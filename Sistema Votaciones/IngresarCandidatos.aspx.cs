using System;
using System.Data.SqlClient; // Espacio de nombres para trabajar con SQL Server
using System.Web.Configuration; // Espacio de nombres para trabajar con la configuración de la aplicación web
using System.Data; // Espacio de nombres para trabajar con datos
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration; // Espacio de nombres para trabajar con la configuración de la aplicación

namespace Sistema_Votaciones
{
    public partial class IngresarCandidatos : System.Web.UI.Page
    {
        // Método que se ejecuta cuando la página se carga
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verifica si la página se está cargando por primera vez
            if (!IsPostBack)
            {
                // Cargar los partidos en el dropdown
                CargarPartidos();
            }
        }

        // Método que se ejecuta cuando se hace clic en el botón "Ingresar Candidato"
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Verifica si la página es válida
            if (Page.IsValid)
            {
                // Obtiene los valores de los campos de entrada
                string cedula = txtCedula.Text;
                string nombre = txtNombre.Text;
                string apellido1 = txtApellido1.Text;
                string apellido2 = txtApellido2.Text;
                string direccion = txtDireccion.Text;
                int idPartido = int.Parse(ddlPartidos.SelectedValue);

                // Obtiene la cadena de conexión desde el archivo de configuración
                string connectionString = ConfigurationManager.ConnectionStrings["BDVotacionesConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    // Consulta SQL para insertar un nuevo candidato
                    string query = "INSERT INTO Candidatos (Cedula, Nombre, Apellido1, Apellido2, Direccion, IDPartido) VALUES (@Cedula, @Nombre, @Apellido1, @Apellido2, @Direccion, @IDPartido)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Agrega los parámetros a la consulta SQL
                        cmd.Parameters.AddWithValue("@Cedula", cedula);
                        cmd.Parameters.AddWithValue("@Nombre", nombre);
                        cmd.Parameters.AddWithValue("@Apellido1", apellido1);
                        cmd.Parameters.AddWithValue("@Apellido2", apellido2);
                        cmd.Parameters.AddWithValue("@Direccion", direccion);
                        cmd.Parameters.AddWithValue("@IDPartido", idPartido);
                        con.Open(); // Abre la conexión a la base de datos
                        cmd.ExecuteNonQuery(); // Ejecuta la consulta SQL
                    }
                }
                LimpiarCampos(); // Limpia los campos de entrada
            }
        }

        // Método para validar la fecha de nacimiento
        protected void cvFechaNacimiento_ServerValidate(object source, ServerValidateEventArgs args)
        {
            DateTime fechaNacimiento;
            string[] formatos = { "yyyy-MM-dd", "dd-MM-yyyy" }; // Formatos de fecha permitidos

            // Intenta convertir el valor de entrada a una fecha
            if (DateTime.TryParseExact(args.Value, formatos, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out fechaNacimiento))
            {
                // Verifica si el candidato tiene al menos 18 años
                if (DateTime.Now.Year - fechaNacimiento.Year >= 18)
                {
                    args.IsValid = true; // La fecha es válida
                }
                else
                {
                    args.IsValid = false; // La fecha no es válida
                }
            }
            else
            {
                args.IsValid = false; // La fecha no es válida
            }
        }

        // Método para cargar los partidos en el dropdown
        private void CargarPartidos()
        {
            // Obtiene la cadena de conexión desde el archivo de configuración
            string connectionString = ConfigurationManager.ConnectionStrings["BDVotacionesConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Consulta SQL para obtener los partidos
                string query = "SELECT Id, Nombre FROM Partidos";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open(); // Abre la conexión a la base de datos
                    SqlDataReader reader = cmd.ExecuteReader(); // Ejecuta la consulta y obtiene un DataReader
                    ddlPartidos.DataSource = reader; // Establece la fuente de datos del dropdown
                    ddlPartidos.DataTextField = "Nombre"; // Campo que se mostrará en el dropdown
                    ddlPartidos.DataValueField = "Id"; // Campo que se usará como valor del dropdown
                    ddlPartidos.DataBind(); // Enlaza los datos al dropdown
                }
            }
            // Agrega un elemento por defecto al dropdown
            ddlPartidos.Items.Insert(0, new ListItem("Seleccione un partido", "0"));
        }

        // Método para limpiar los campos de entrada
        private void LimpiarCampos()
        {
            txtCedula.Text = "";
            txtNombre.Text = "";
            txtApellido1.Text = "";
            txtApellido2.Text = "";
            txtDireccion.Text = "";
            txtFechaNacimiento.Text = "";
            ddlPartidos.SelectedIndex = 0;
        }

        // Método que se ejecuta cuando se hace clic en el botón "Consultar Candidatos"
        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            // Obtiene la cadena de conexión desde el archivo de configuración
            string connectionString = ConfigurationManager.ConnectionStrings["BDVotacionesConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Consulta SQL para obtener los candidatos y sus partidos
                string query = @"
                    SELECT C.Cedula, C.Nombre, C.Apellido1, C.Apellido2, C.Direccion, P.Nombre AS Partido, C.FechaRegistro
                    FROM Candidatos C
                    JOIN Partidos P ON C.IDPartido = P.Id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open(); // Abre la conexión a la base de datos
                    SqlDataReader reader = cmd.ExecuteReader(); // Ejecuta la consulta y obtiene un DataReader
                    gvCandidatos.DataSource = reader; // Establece la fuente de datos del GridView
                    gvCandidatos.DataBind(); // Enlaza los datos al GridView
                }
            }
        }
    }
}
