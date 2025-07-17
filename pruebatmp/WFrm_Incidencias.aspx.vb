Imports System.Data.SqlClient
Imports System.Data

Partial Class WFrm_Incidencias
    Inherits System.Web.UI.Page

    Protected Sub btnProbarConexion_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim connectionString As String = "Server=172.16.39.64;Database=SimadOC;User Id=Sa;Password=Semuigen2025;"
        Dim connection As New SqlConnection(connectionString)

        Try
            connection.Open()
            lblResultado.CssClass = "mensaje text-success fs-5"
            lblResultado.Text = "✅ Conexión exitosa a la base de datos."
        Catch ex As Exception
            lblResultado.CssClass = "mensaje text-danger fs-5"
            lblResultado.Text = "❌ Error de conexión: " & ex.Message
        Finally
            connection.Close()
        End Try
    End Sub

    Protected Sub btnMostrarVista_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim connectionString As String = "Server=172.16.39.64;Database=SimadOC;User Id=Sa;Password=Semuigen2025;"
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
                lblResultado.CssClass = "mensaje text-danger fs-5"
                lblResultado.Text = "❌ Error al cargar los datos: " & ex.Message
            Finally
                connection.Close()
            End Try
        End Using
    End Sub
End Class
