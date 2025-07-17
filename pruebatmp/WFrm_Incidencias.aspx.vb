Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Data

Partial Class WFrm_Incidencias
    Inherits System.Web.UI.Page

    Protected Sub btnProbarConexion_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Cadena de conexión: cámbiala con tus datos
        Dim connectionString As String = "Server=172.16.39.64;Database=SimadOC;User Id=Sa;Password=Semuigen2025;"



        Dim connection As New SqlConnection(connectionString)

        Try
            connection.Open()
            lblResultado.Text = "✅ Conexión exitosa a la base de datos"
        Catch ex As Exception
            lblResultado.Text = "❌ Error de conexión: " & ex.Message
        Finally
            connection.Close()
        End Try
    End Sub
    Protected Sub btnMostrarVista_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Cadena de conexión (puedes usar la del web.config)
        Dim connectionString As String = "Server=172.16.39.64;Database=SimadOC;User Id=Sa;Password=Semuigen2025;"
        ' O:
        ' Dim connectionString As String = ConfigurationManager.ConnectionStrings("MiConexionSQL").ConnectionString

        ' Tu consulta SELECT de la vista
        Dim query As String = "SELECT * FROM VwRpt_Incidencias"

        Using connection As New SqlConnection(connectionString)
            Dim cmd As New SqlCommand(query, connection)
            Dim adapter As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()

            Try
                connection.Open()
                adapter.Fill(dt)

                gvVista.DataSource = dt
                gvVista.DataBind()

            Catch ex As Exception
                ' Maneja el error
                ' Puedes usar un Label para mostrar el error si quieres
            Finally
                connection.Close()
            End Try
        End Using
    End Sub

End Class
