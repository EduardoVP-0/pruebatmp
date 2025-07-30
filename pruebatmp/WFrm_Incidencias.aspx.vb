Imports System.Data
Imports System.Data.SqlClient

Partial Class WFrm_Incidencias
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarTipoJustificacion()
            CargarTipoJustificacion1()
            CargarEmpleados()
            btnMostrarVista_Click(Nothing, Nothing) ' ← Se llama automáticamente al iniciar la página
        End If
    End Sub


    Private Sub CargarTipoJustificacion()
        Dim connectionString As String = "Server=172.16.34.9;Database=SimadOC;User Id=Sa;Password=Seigen2019;"
        Dim query As String = "SELECT checktype, Desc_Tipo_Incidencia FROM TblC_Tipo_Incidencia"

        Using connection As New SqlConnection(connectionString)
            Dim cmd As New SqlCommand(query, connection)
            Dim adapter As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()

            Try
                connection.Open()
                adapter.Fill(dt)

                ddlTipoJustificacion.DataSource = dt
                ddlTipoJustificacion.DataTextField = "Desc_Tipo_Incidencia"
                ddlTipoJustificacion.DataValueField = "checktype"
                ddlTipoJustificacion.DataBind()

                ddlTipoJustificacion.Items.Insert(0, New ListItem("-- Tipo de Justificacion --", ""))
            Catch ex As Exception
                lblResultado.CssClass = "mensaje text-danger fs-5"
                lblResultado.Text = "❌ Error al cargar tipos de justificacion: " & ex.Message
            End Try
        End Using
    End Sub

    Private Sub CargarTipoJustificacion1()
        Dim connectionString As String = "Server=172.16.34.9;Database=SimadOC;User Id=Sa;Password=Seigen2019;"
        Dim query As String = "SELECT checktype, Desc_Tipo_Incidencia FROM TblC_Tipo_Incidencia"

        Using connection As New SqlConnection(connectionString)
            Dim cmd As New SqlCommand(query, connection)
            Dim adapter As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()

            Try
                connection.Open()
                adapter.Fill(dt)

                ddlTipoJustificacion.DataSource = dt
                ddlTipoJustificacion.DataTextField = "Desc_Tipo_Incidencia"
                ddlTipoJustificacion.DataValueField = "checktype"
                ddlTipoJustificacion.DataBind()
                ddlTipoJustificacion.Items.Insert(0, New ListItem("-- Tipo de Justificación --", ""))

                ddlTipoNueva.DataSource = dt
                ddlTipoNueva.DataTextField = "Desc_Tipo_Incidencia"
                ddlTipoNueva.DataValueField = "checktype"
                ddlTipoNueva.DataBind()
                ddlTipoNueva.Items.Insert(0, New ListItem("-- Tipo --", ""))

            Catch ex As Exception
                lblResultado.CssClass = "mensaje text-danger fs-5"
                lblResultado.Text = "❌ Error al cargar tipos de justificación: " & ex.Message
            End Try
        End Using
    End Sub

    ' NUEVO: Cargar empleados desde la vista
    Private Sub CargarEmpleados()
        Dim connectionString As String = "Server=172.16.34.9;Database=SimadOC;User Id=Sa;Password=Seigen2019;"
        Dim query As String = "SELECT Userid, NomEmpleado FROM VwC_Empleados WHERE status = 1 ORDER BY NomEmpleado"

        Using connection As New SqlConnection(connectionString)
            Dim cmd As New SqlCommand(query, connection)
            Dim adapter As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()

            Try
                connection.Open()
                adapter.Fill(dt)

                ddlEmpleados.DataSource = dt
                ddlEmpleados.DataTextField = "NomEmpleado"
                ddlEmpleados.DataValueField = "Userid"
                ddlEmpleados.DataBind()
                ddlEmpleados.Items.Insert(0, New ListItem("-- Selecciona un Empleado --", ""))
            Catch ex As Exception
                lblResultado.CssClass = "mensaje text-danger fs-5"
                lblResultado.Text = "❌ Error al cargar empleados: " & ex.Message
            End Try
        End Using
    End Sub

    ' MODIFICADO: Cuando selecciona un empleado, rellenar también el PIN
    Protected Sub ddlEmpleados_SelectedIndexChanged(sender As Object, e As EventArgs)

        txtPIN.Text = ddlEmpleados.SelectedValue
    End Sub

    Protected Sub btnMostrarVista_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim connectionString As String = "Server=172.16.34.9;Database=SimadOC;User Id=Sa;Password=Seigen2019;"
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
        Dim connectionString As String = "Server=172.16.34.9;Database=SimadOC;User Id=Sa;Password=Seigen2019;"

        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Dim transaction = connection.BeginTransaction()

            Try
                ' Obtener la fecha máxima de la tabla temporal
                Dim tabla As DataTable = ObtenerTablaTemporal()
                Dim fechaMax As DateTime = Date.MinValue

                For Each fila As DataRow In tabla.Rows
                    Dim fecha As DateTime = Convert.ToDateTime(fila("Fecha"))
                    If fecha > fechaMax Then
                        fechaMax = fecha
                    End If
                Next

                ' Calcular siguiente día hábil (no sábado ni domingo)
                Dim diaPresenta As DateTime = fechaMax.AddDays(1)
                While diaPresenta.DayOfWeek = DayOfWeek.Saturday OrElse diaPresenta.DayOfWeek = DayOfWeek.Sunday
                    diaPresenta = diaPresenta.AddDays(1)
                End While

                ' Insertar en TblP_Justificaciones2
                Dim queryJust As String = "INSERT INTO TblP_Justificaciones2 " &
                "(Num_Justificacion, Tipo_Justificacion, Fecha_Justificacion, Num_Memo_Justificacion, Motivo_Justificacion, " &
                "idusuario_Captura, Fecha_Captura, Periodo_Vacacional, Lugar_Exp, Dia_Presenta) " &
                "VALUES (@NumJust, @TipoJust, @FechaJust, @NumMemo, @Motivo, @IdUsuario, @FechaCap, @Periodo, @Lugar, @DiaPresenta)"

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
                cmdJust.Parameters.AddWithValue("@DiaPresenta", diaPresenta)
                cmdJust.ExecuteNonQuery()

                ' Insertar las fechas en checkinout_justif2
                For Each fila As DataRow In tabla.Rows
                    Dim queryCheck As String = "INSERT INTO checkinout_justif2 (Num_Justificacion, checktime, checktype, pin) " &
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

                ' Limpiar campos
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
                ddlEmpleados.SelectedIndex = 0

                ViewState("TablaFechas") = Nothing
                gvFechasAgregadas.DataSource = Nothing
                gvFechasAgregadas.DataBind()

                btnMostrarVista_Click(Nothing, Nothing)
                upVista.Update()
            Catch ex As Exception
                transaction.Rollback()
                lblResultado.CssClass = "mensaje text-danger fs-5"
                lblResultado.Text = "❌ Error al guardar: " & ex.Message
            End Try
        End Using
    End Sub

    ' Sincronizar tipo justificación en ambos combos
    Protected Sub ddlTipoJustificacion_SelectedIndexChanged(sender As Object, e As EventArgs)
        ddlTipoNueva.SelectedValue = ddlTipoJustificacion.SelectedValue
        If ddlTipoJustificacion.SelectedItem.Text.ToUpper() = "VACACIONES" OrElse ddlTipoJustificacion.SelectedItem.Text.ToUpper() = "VACACIONES BASE" Then
            txtPeriodo.Enabled = True
        Else
            txtPeriodo.SelectedIndex = 0
            txtPeriodo.Enabled = False
        End If
    End Sub


End Class