Imports System.Data
Imports System.Data.SqlClient

Partial Class WFrm_Incidencias
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarTipoJustificacion()
            CargarTipoJustificacion1()
        End If
    End Sub

    Private Sub CargarTipoJustificacion()
        Dim connectionString As String = "Server=172.16.39.64;Database=SimadOC;User Id=Sa;Password=Semuigen2025;"
        Dim query As String = "SELECT checktype, Desc_Tipo_Indicencia FROM TblC_Tipo_Incidencia"

        Using connection As New SqlConnection(connectionString)
            Dim cmd As New SqlCommand(query, connection)
            Dim adapter As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()

            Try
                connection.Open()
                adapter.Fill(dt)

                ddlTipoJustificacion.DataSource = dt
                ddlTipoJustificacion.DataTextField = "Desc_Tipo_Indicencia"
                ddlTipoJustificacion.DataValueField = "checktype"
                ddlTipoJustificacion.DataBind()

                ddlTipoJustificacion.Items.Insert(0, New ListItem("-- Tipo de Justificacion --", ""))
            Catch ex As Exception
                lblResultado.CssClass = "mensaje text-danger fs-5"
                lblResultado.Text = "❌ Error al cargar tipos de justificación: " & ex.Message
            End Try
        End Using
    End Sub

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

    ' Cargar tipos de justificación también en ddlTipoNueva
    Private Sub CargarTipoJustificacion1()
        Dim connectionString As String = "Server=172.16.39.64;Database=SimadOC;User Id=Sa;Password=Semuigen2025;"
        Dim query As String = "SELECT checktype, Desc_Tipo_Indicencia FROM TblC_Tipo_Incidencia"

        Using connection As New SqlConnection(connectionString)
            Dim cmd As New SqlCommand(query, connection)
            Dim adapter As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()

            Try
                connection.Open()
                adapter.Fill(dt)

                ddlTipoJustificacion.DataSource = dt
                ddlTipoJustificacion.DataTextField = "Desc_Tipo_Indicencia"
                ddlTipoJustificacion.DataValueField = "checktype"
                ddlTipoJustificacion.DataBind()
                ddlTipoJustificacion.Items.Insert(0, New ListItem("-- Tipo de Justificación --", ""))

                ddlTipoNueva.DataSource = dt
                ddlTipoNueva.DataTextField = "Desc_Tipo_Indicencia"
                ddlTipoNueva.DataValueField = "checktype"
                ddlTipoNueva.DataBind()
                ddlTipoNueva.Items.Insert(0, New ListItem("-- Tipo --", ""))

            Catch ex As Exception
                lblResultado.CssClass = "mensaje text-danger fs-5"
                lblResultado.Text = "❌ Error al cargar tipos de justificación: " & ex.Message
            End Try
        End Using
    End Sub

    Private Function ObtenerTablaTemporal() As DataTable
        Dim tabla As DataTable = TryCast(ViewState("TablaFechas"), DataTable)
        If tabla Is Nothing Then
            tabla = New DataTable()
            tabla.Columns.Add("Fecha", GetType(String))
            tabla.Columns.Add("DiaSemana", GetType(String))
            tabla.Columns.Add("Tipo", GetType(String))
            tabla.Columns.Add("Descripcion", GetType(String))
            ViewState("TablaFechas") = tabla
        End If
        Return tabla
    End Function

    Protected Sub btnAgregarFecha_Click(sender As Object, e As EventArgs)
        If txtFechaNueva.Text = "" Or ddlTipoNueva.SelectedIndex = 0 Then
            lblResultado.CssClass = "mensaje text-danger fs-5"
            lblResultado.Text = "⚠️ Debes ingresar una fecha y un tipo de justificación."
            Exit Sub
        End If

        Dim tabla As DataTable = ObtenerTablaTemporal()
        Dim fecha As DateTime = Convert.ToDateTime(txtFechaNueva.Text)
        Dim dia As String = fecha.ToString("dddd", New System.Globalization.CultureInfo("es-MX"))
        Dim tipo As String = ddlTipoNueva.SelectedValue
        Dim descripcion As String = ddlTipoNueva.SelectedItem.Text

        ' Validar duplicado
        For Each fila As DataRow In tabla.Rows
            If fila("Fecha").ToString() = fecha.ToShortDateString() Then
                lblResultado.CssClass = "mensaje text-warning fs-5"
                lblResultado.Text = "⚠️ La fecha ya fue agregada."
                Return
            End If
        Next

        tabla.Rows.Add(fecha.ToShortDateString(), dia, tipo, descripcion)
        ViewState("TablaFechas") = tabla
        gvFechasAgregadas.DataSource = tabla
        gvFechasAgregadas.DataBind()
        lblResultado.Text = ""
    End Sub

    Protected Sub gvFechasAgregadas_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Eliminar" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim tabla As DataTable = ObtenerTablaTemporal()
            If index >= 0 AndAlso index < tabla.Rows.Count Then
                tabla.Rows.RemoveAt(index)
                ViewState("TablaFechas") = tabla
                gvFechasAgregadas.DataSource = tabla
                gvFechasAgregadas.DataBind()
            End If
        End If
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim connectionString As String = "Server=172.16.39.64;Database=SimadOC;User Id=Sa;Password=Semuigen2025;"

        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Dim transaction = connection.BeginTransaction()

            Try
                ' Guardar en TbIP_Justificaciones
                Dim queryJust As String = "INSERT INTO TbIP_Justificaciones" &
            "(Num_Justificacion, Tipo_Justificacion, Fecha_Justificacion, Num_Memo_Justificacion, Motivo_Justificacion," &
             "idusuario_Captura, Fecha_Captura, Periodo_Vacacional, Lugar_Exp)" &
             "VALUES (@NumJust, @TipoJust, @FechaJust, @NumMemo, @Motivo, @IdUsuario, @FechaCap, @Periodo, @Lugar)"

                Dim cmdJust As New SqlCommand(queryJust, connection, transaction)
                cmdJust.Parameters.AddWithValue("@NumJust", txtNumJustificacion.Text)
                cmdJust.Parameters.AddWithValue("@TipoJust", ddlTipoJustificacion.SelectedValue)
                cmdJust.Parameters.AddWithValue("@FechaJust", Convert.ToDateTime(txtFechaJustificacion.Text))
                cmdJust.Parameters.AddWithValue("@NumMemo", txtNumMemo.Text)
                cmdJust.Parameters.AddWithValue("@Motivo", txtMotivo.Text)
                cmdJust.Parameters.AddWithValue("@IdUsuario", txtIdUsuario.Text)
                cmdJust.Parameters.AddWithValue("@FechaCap", Convert.ToDateTime(txtFechaCaptura.Text))
                cmdJust.Parameters.AddWithValue("@Periodo", txtPeriodo.Text)
                cmdJust.Parameters.AddWithValue("@Lugar", txtLugar.Text)
                cmdJust.ExecuteNonQuery()

                ' Guardar en checkinout_justif
                Dim tabla As DataTable = ObtenerTablaTemporal()
                For Each fila As DataRow In tabla.Rows
                    Dim queryCheck As String = "INSERT INTO checkinout_justif (Num_Justificacion, checktime, checktype, pin) " &
                                           "VALUES (@NumJust, @Fecha, @Tipo, @PIN)"
                    Dim cmdCheck As New SqlCommand(queryCheck, connection, transaction)
                    cmdCheck.Parameters.AddWithValue("@NumJust", txtNumJustificacion.Text)
                    cmdCheck.Parameters.AddWithValue("@Fecha", Convert.ToDateTime(fila("Fecha")))
                    cmdCheck.Parameters.AddWithValue("@Tipo", fila("Tipo").ToString())
                    cmdCheck.Parameters.AddWithValue("@PIN", txtPIN.Text)
                    cmdCheck.ExecuteNonQuery()
                Next

                transaction.Commit()
                lblResultado.CssClass = "mensaje text-success fs-5"
                lblResultado.Text = "✅ Justificación y fechas guardadas correctamente."

                ' Limpiar
                txtNumJustificacion.Text = ""
                txtFechaJustificacion.Text = ""
                txtNumMemo.Text = ""
                txtMotivo.Text = ""
                txtIdUsuario.Text = ""
                txtFechaCaptura.Text = ""
                txtPeriodo.Text = ""
                txtLugar.Text = ""
                txtPIN.Text = ""
                txtFechaNueva.Text = ""
                ddlTipoJustificacion.SelectedIndex = 0
                ddlTipoNueva.SelectedIndex = 0

                ViewState("TablaFechas") = Nothing
                gvFechasAgregadas.DataSource = Nothing
                gvFechasAgregadas.DataBind()

            Catch ex As Exception
                transaction.Rollback()
                lblResultado.CssClass = "mensaje text-danger fs-5"
                lblResultado.Text = "❌ Error al guardar: " & ex.Message
            End Try
        End Using
    End Sub
    ' Nuevo método para sincronizar automáticamente el tipo de justificación de la fecha
    Protected Sub ddlTipoJustificacion_SelectedIndexChanged(sender As Object, e As EventArgs)
        ddlTipoNueva.SelectedValue = ddlTipoJustificacion.SelectedValue
    End Sub



End Class
