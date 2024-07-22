using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistema_Votaciones
{
    public partial class Votaciones : System.Web.UI.Page
    {
        // Método que se ejecuta al cargar la página
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verifica si la página no es una recarga para cargar los candidatos
            if (!IsPostBack)
            {
                CargarCandidatos();
            }
        }

        // Método que se ejecuta al hacer clic en el botón de registrar voto
        protected void btnRegistrarVoto_Click(object sender, EventArgs e)
        {
            // Valida la fecha de nacimiento
            DateTime fechaNacimiento;
            if (!DateTime.TryParse(txtFechaNacimiento.Text, out fechaNacimiento))
            {
                cvFechaNacimiento.ErrorMessage = "Fecha inválida. Use el formato YYYY-MM-DD o DD-MM-YYYY.";
                cvFechaNacimiento.IsValid = false;
                return;
            }

            // Verifica si el votante es mayor de 18 años
            if (fechaNacimiento.AddYears(18) > DateTime.Now)
            {
                cvFechaNacimiento.ErrorMessage = "Debe ser mayor de 18 años.";
                cvFechaNacimiento.IsValid = false;
                return;
            }

            // Obtiene la cadena de conexión de la configuración
            string connectionString = ConfigurationManager.ConnectionStrings["BDVotacionesConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Consulta SQL para insertar el voto
                string query = "INSERT INTO Votos (IDCandidato, IDVotante) VALUES (@IDCandidato, @IDVotante)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Agrega parámetros a la consulta
                    cmd.Parameters.AddWithValue("@IDCandidato", ddlCandidatos.SelectedValue);
                    cmd.Parameters.AddWithValue("@IDVotante", txtIDVotante.Text);

                    // Abre la conexión y ejecutar la consulta
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            // Limpia los campos del formulario
            LimpiarCampos();
        }

        // Método para cargar los candidatos en el DropDownList
        private void CargarCandidatos()
        {
            // Obtiene la cadena de conexión de la configuración
            string connectionString = ConfigurationManager.ConnectionStrings["BDVotacionesConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Consulta SQL para obtener los candidatos con su partido
                string query = "SELECT C.Id, C.Nombre + ' ' + C.Apellido1 + ' ' + C.Apellido2 + ' (' + P.Nombre + ')' AS NombreCompleto FROM Candidatos C JOIN Partidos P ON C.IDPartido = P.Id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Abre la conexión y ejecutar la consulta
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    // Establece la fuente de datos del DropDownList
                    ddlCandidatos.DataSource = reader;
                    ddlCandidatos.DataTextField = "NombreCompleto";
                    ddlCandidatos.DataValueField = "Id";
                    ddlCandidatos.DataBind();
                }
            }
        }

        // Método para limpiar los campos del formulario
        private void LimpiarCampos()
        {
            txtIDVotante.Text = "";
            txtNombre.Text = "";
            txtApellido1.Text = "";
            txtApellido2.Text = "";
            txtFechaNacimiento.Text = "";
            txtEmail.Text = "";
            txtDireccion.Text = "";
            ddlCandidatos.SelectedIndex = 0;
        }

        // Validador personalizado para la fecha de nacimiento
        protected void cvFechaNacimiento_ServerValidate(object source, ServerValidateEventArgs args)
        {
            DateTime fechaNacimiento;
            // Valida el formato de la fecha de nacimiento
            if (!DateTime.TryParseExact(args.Value, new[] { "yyyy-MM-dd", "dd-MM-yyyy" }, null, System.Globalization.DateTimeStyles.None, out fechaNacimiento))
            {
                args.IsValid = false;
            }
            else
            {
                // Verifica si el votante es mayor de 18 años
                args.IsValid = fechaNacimiento.AddYears(18) <= DateTime.Now;
            }
        }
    }
}