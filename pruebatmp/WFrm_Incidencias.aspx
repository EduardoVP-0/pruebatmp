<%@ Page Language="VB" AutoEventWireup="true" CodeFile="WFrm_Incidencias.aspx.vb" Inherits="WFrm_Incidencias" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Incidencias con Estilo</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>

    <style>
        body {
            background: linear-gradient(135deg, #667eea, #764ba2);
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            padding: 50px;
        }

        .card-custom {
            animation: fadeInUp 1s ease-in-out;
            border-radius: 15px;
            box-shadow: 0 10px 25px rgba(0, 0, 0, 0.2);
        }

        @keyframes fadeInUp {
            from { opacity: 0; transform: translateY(30px); }
            to { opacity: 1; transform: translateY(0); }
        }

        .btn-animated {
            transition: all 0.3s ease-in-out;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
        }

        .btn-animated:hover {
            transform: scale(1.05);
            box-shadow: 0 6px 12px rgba(0, 0, 0, 0.25);
        }

        .mensaje {
            animation: fadeIn 1s;
            font-weight: bold;
            text-align: center;
        }

        @keyframes fadeIn {
            from { opacity: 0; }
            to { opacity: 1; }
        }

        .gridview-container {
            animation: fadeIn 1.5s ease-in;
            margin-top: 20px;
        }

        .gridview-custom th {
            background-color: #4e54c8;
            color: white;
            padding: 10px;
            text-align: left;
        }

        .gridview-custom td {
            padding: 8px;
            background-color: white;
        }

        .gridview-custom tr:nth-child(even) {
            background-color: #f8f9fa;
        }

        .gridview-custom tr:hover {
            background-color: #e9ecef;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />

        <div class="container">
            <div class="card card-custom p-4">
                <h2 class="text-center text-dark mb-4">Consulta de Incidencias</h2>

                <div class="text-center mb-3">
                    <asp:Button ID="btnProbarConexion" runat="server" Text="Probar Conexión"
                        CssClass="btn btn-primary btn-animated me-2" OnClick="btnProbarConexion_Click" />

                    <asp:Button ID="btnMostrarVista" runat="server" Text="Mostrar Incidencias"
                        CssClass="btn btn-success btn-animated" OnClick="btnMostrarVista_Click" />
                    <asp:Button ID="btnMostrarFormulario" runat="server" Text="Agregar Justificación"
                        CssClass="btn btn-warning btn-animated mt-2" OnClientClick="toggleFormulario(); return false;" />
                </div>

                <asp:Label ID="lblResultado" runat="server" CssClass="mensaje text-white fs-5" />

                <div id="formularioAgregar" style="display: none;" class="mt-4">
                    <asp:UpdatePanel ID="upFormulario" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="card card-body bg-light">
                                <h5 class="text-center text-dark mb-3">Nueva Justificación</h5>

                                <div class="row">
                                    <div class="col-md-4 mb-2">
                                        <asp:TextBox ID="txtNumJustificacion" runat="server" CssClass="form-control" placeholder="Número Justificación"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4 mb-2">
                                        <asp:DropDownList ID="ddlTipoJustificacion" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoJustificacion_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-4 mb-2">
                                        <asp:TextBox ID="txtFechaJustificacion" runat="server" CssClass="form-control" TextMode="DateTimeLocal" placeholder="Fecha Justificación"></asp:TextBox>
                                    </div>

                                    <div class="col-md-6 mb-2">
                                        <asp:TextBox ID="txtNumMemo" runat="server" CssClass="form-control" placeholder="Número de Memo"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6 mb-2">
                                        <asp:TextBox ID="txtMotivo" runat="server" CssClass="form-control" placeholder="Motivo de Justificación"></asp:TextBox>
                                    </div>

                                    <!-- NUEVO: DropDownList de empleados -->
                                    <div class="col-md-6 mb-2">
                                        <asp:DropDownList ID="ddlEmpleados" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpleados_SelectedIndexChanged" />
                                    </div>
                                    <div class="col-md-6 mb-2">
                                        <asp:TextBox ID="txtIdUsuario" runat="server" CssClass="form-control" placeholder="ID Usuario Captura"></asp:TextBox>
                                    </div>
                                    <div>
                                        <asp:TextBox ID="txtPIN" runat="server" CssClass="form-control" placeholder="PIN" Style="display:none;" />
                                    </div>

                                    <div class="col-md-4 mb-2">
                                        <asp:TextBox ID="txtFechaCaptura" runat="server" CssClass="form-control" TextMode="DateTimeLocal" placeholder="Fecha Captura"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4 mb-2">
                                        <asp:TextBox ID="txtPeriodo" runat="server" CssClass="form-control" placeholder="Periodo Vacacional"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4 mb-2">
                                        <asp:TextBox ID="txtLugar" runat="server" CssClass="form-control" placeholder="Lugar de Expedición"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="text-center mt-3">
                                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar Justificación"
                                        CssClass="btn btn-success btn-animated" OnClick="btnGuardar_Click" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <!-- Agregar fechas a la tabla con UpdatePanel -->
                    <asp:UpdatePanel ID="upFechas" runat="server">
                        <ContentTemplate>
                            <div class="row align-items-end">
                                <div class="col-md-4 mb-2">
                                    <asp:TextBox ID="txtFechaNueva" runat="server" CssClass="form-control" TextMode="Date" placeholder="Fecha para agregar"></asp:TextBox>
                                </div>
                                <div class="col-md-4 mb-2">
                                    <asp:DropDownList ID="ddlTipoNueva" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                </div>
                                <div class="col-md-4 mb-2">
                                    <asp:Button ID="btnAgregarFecha" runat="server" Text="Agregar Fecha" CssClass="btn btn-primary btn-animated w-100" OnClick="btnAgregarFecha_Click" />
                                </div>
                            </div>

                            <asp:GridView ID="gvFechasAgregadas" runat="server" AutoGenerateColumns="False"
                                CssClass="table table-striped table-bordered mt-3" GridLines="None"
                                OnRowCommand="gvFechasAgregadas_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                                    <asp:BoundField DataField="DiaSemana" HeaderText="Día" />
                                    <asp:BoundField DataField="Tipo" HeaderText="Tipo Justificación" />
                                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                    <asp:ButtonField ButtonType="Button" CommandName="Eliminar" Text="Quitar"
                                        ControlStyle-CssClass="btn btn-danger btn-sm" />
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div class="gridview-container">
                    <asp:GridView ID="gvVista" runat="server" AutoGenerateColumns="true"
                        CssClass="table table-bordered gridview-custom" GridLines="None" />
                </div>
            </div>
        </div>
    </form>

    <script>
        function toggleFormulario() {
            var form = document.getElementById("formularioAgregar");
            form.style.display = (form.style.display === "none") ? "block" : "none";
        }
    </script>
</body>
</html>

