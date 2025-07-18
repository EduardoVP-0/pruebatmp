Imports System.Data
Imports System.Data.SqlClient

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
    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim connectionString As String = "Server=172.16.39.64;Database=SimadOC;User Id=Sa;Password=Semuigen2025;"

        Using connection As New SqlConnection(connectionString)
            Dim query As String = "INSERT INTO TbIP_Justificaciones" &
            "(Num_Justificacion, Tipo_Justificacion, Fecha_Justificacion, Num_Memo_Justificacion, Motivo_Justificacion," &
             "idusuario_Captura, Fecha_Captura, Periodo_Vacacional, Lugar_Exp)" &
             "VALUES (@NumJust, @TipoJust, @FechaJust, @NumMemo, @Motivo, @IdUsuario, @FechaCap, @Periodo, @Lugar)"

            Dim cmd As New SqlCommand(query, connection)
            cmd.Parameters.AddWithValue("@NumJust", txtNumJustificacion.Text)
            cmd.Parameters.AddWithValue("@TipoJust", txtTipoJustificacion.Text)
            cmd.Parameters.AddWithValue("@FechaJust", Convert.ToDateTime(txtFechaJustificacion.Text))
            cmd.Parameters.AddWithValue("@NumMemo", txtNumMemo.Text)
            cmd.Parameters.AddWithValue("@Motivo", txtMotivo.Text)
            cmd.Parameters.AddWithValue("@IdUsuario", txtIdUsuario.Text)
            cmd.Parameters.AddWithValue("@FechaCap", Convert.ToDateTime(txtFechaCaptura.Text))
            cmd.Parameters.AddWithValue("@Periodo", txtPeriodo.Text)
            cmd.Parameters.AddWithValue("@Lugar", txtLugar.Text)

            Try
                connection.Open()
                cmd.ExecuteNonQuery()
                lblResultado.CssClass = "mensaje text-success fs-5"
                lblResultado.Text = "✅ Justificación agregada correctamente."

                ' Limpiar campos
                txtNumJustificacion.Text = ""
                txtTipoJustificacion.Text = ""
                txtFechaJustificacion.Text = ""
                txtNumMemo.Text = ""
                txtMotivo.Text = ""
                txtIdUsuario.Text = ""
                txtFechaCaptura.Text = ""
                txtPeriodo.Text = ""
                txtLugar.Text = ""
            Catch ex As Exception
                lblResultado.CssClass = "mensaje text-danger fs-5"
                lblResultado.Text = "❌ Error al agregar: " & ex.Message
            Finally
                connection.Close()
            End Try
        End Using
    End Sub

End Class
