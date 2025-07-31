<%@ Page Language="VB" AutoEventWireup="true" CodeFile="WFrm_Incidencias.aspx.vb" Inherits="WFrm_Incidencias" %> 

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Incidencias con Estilo</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>

    <style>
        /* --- Tu mismo estilo original --- */
        :root {
            --verde-gob: #009887;
            --rosa-gob: #C90166;
            --rojo-gob: #AE192D;
            --beige-gob: #D3C2B4;
            --negro: #000000;
        }
        body {
            background: linear-gradient(135deg, var(--beige-gob), #ffffff);
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            padding: 50px;
        }
        .card-custom {
            background-color: #ffffff;
            border-left: 6px solid var(--verde-gob);
            border-radius: 15px;
            animation: fadeInUp 1s ease-in-out;
            box-shadow: 0 10px 25px rgba(0, 0, 0, 0.2);
        }
        h2, h5 {
            color: var(--rojo-gob) !important;
            font-weight: 700;
        }
        .btn-animated {
            transition: all 0.3s ease-in-out;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
            font-weight: bold !important;
            color: white !important;
        }
        .btn-animated:hover {
            transform: scale(1.05);
            box-shadow: 0 6px 12px rgba(0, 0, 0, 0.25);
        }
        #btnMostrarFormulario {
            background-color: var(--rosa-gob) !important;
        }
        .btn-close {
            background-color: #000;
        }
        .mensaje {
            animation: fadeIn 1s;
            font-weight: bold;
            text-align: center;
            color: var(--negro) !important;
        }
        .gridview-container {
            animation: fadeIn 1.5s ease-in;
            margin-top: 20px;
        }
        .gridview-custom th {
            background-color: var(--verde-gob) !important;
            color: white !important;
            padding: 10px;
            text-align: left;
        }
        .gridview-custom td {
            padding: 8px;
            background-color: white;
        }
        .gridview-custom tr:nth-child(even) {
            background-color: var(--beige-gob);
        }
        .gridview-custom tr:hover {
            background-color: #f1f1f1;
        }
        @keyframes fadeInUp {
            from {
                opacity: 0;
                transform: translateY(30px);
            }
            to {
                opacity: 1;
                transform: translateY(0);
            }
        }
        @keyframes fadeIn {
            from {
                opacity: 0;
            }
            to {
                opacity: 1;
            }
        }
        .table-wrapper {
            overflow-x: auto;
            max-width: 100%;
        }
        .gridview-custom {
            min-width: 800px;
        }
        html, body {
            height: 100vh;
            overflow: hidden;
            margin: 0;
            padding: 0;
        }
        .gridview-scrollable {
            max-height: 60vh;
            overflow-y: auto;
            overflow-x: auto;
            border: 1px solid #ccc;
            border-radius: 8px;
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
                    <asp:Button ID="btnMostrarFormulario" runat="server" Text="Agregar Justificación"
                        CssClass="btn btn-animated mt-2" OnClientClick="mostrarModal(); return false;" />
                </div>

                <asp:Label ID="lblResultado" runat="server" CssClass="mensaje text-white fs-5" />

                <!-- Vista principal -->
                <div class="gridview-container gridview-scrollable">
                    <asp:UpdatePanel ID="upVista" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="gvVista" runat="server" AutoGenerateColumns="true"
                                CssClass="table table-bordered gridview-custom w-100" GridLines="None"
                                OnRowDataBound="gvVista_RowDataBound" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

        <!-- Modal -->
        <div class="modal fade" id="modalFormulario" tabindex="-1" aria-labelledby="modalFormularioLabel" aria-hidden="true">
            <div class="modal-dialog modal-xl modal-dialog-scrollable">
                <div class="modal-content">
                    <div class="modal-header bg-light">
                        <h5 class="modal-title" id="modalFormularioLabel">Agregar Justificación</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar" onclick="cerrarYLimpiarModal()"></button>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="upFormulario" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
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
                                        <!-- Motivo más grande -->
                                        <asp:TextBox ID="txtMotivo" runat="server" CssClass="form-control"
                                            TextMode="MultiLine" Rows="4" placeholder="Motivo de Justificación"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6 mb-2">
                                        <asp:DropDownList ID="ddlEmpleados" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpleados_SelectedIndexChanged" />
                                    </div>
                                    <div class="col-md-6 mb-2">
                                        <asp:TextBox ID="txtIdUsuario" runat="server" CssClass="form-control" placeholder="ID Usuario Captura"></asp:TextBox>
                                    </div>
                                    <asp:TextBox ID="txtPIN" runat="server" CssClass="form-control" placeholder="PIN" Style="display: none;" />
                                    
                                    <div class="col-md-4 mb-2">
                                        <asp:DropDownList ID="txtPeriodo" runat="server" CssClass="form-control" Enabled="false">
                                            <asp:ListItem Text="-- Selecciona el periodo --" Value="" />
                                            <asp:ListItem Text="PRIMER PERIODO VACACIONAL 2025" Value="PRIMER PERIODO VACACIONAL 2025" />
                                            <asp:ListItem Text="SEGUNDO PERIODO VACACIONAL 2025" Value="SEGUNDO PERIODO VACACIONAL 2025" />
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-4 mb-2">
                                        <asp:TextBox ID="txtLugar" runat="server" CssClass="form-control" placeholder="Lugar de Expedición"></asp:TextBox>
                                    </div>
                                </div>

                                <!-- Fechas agregadas -->
                                <div class="row align-items-end mt-3">
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

                                <div class="text-center mt-3">
                                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar Justificación"
                                        CssClass="btn btn-success btn-animated" OnClick="btnGuardar_Click" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <!-- Scripts originales -->
    <script>
        function mostrarModal() {
            var modal = new bootstrap.Modal(document.getElementById('modalFormulario'));
            modal.show();
        }
        function cerrarYLimpiarModal() {
            // Limpiar campos
            document.getElementById('<%= txtNumJustificacion.ClientID %>').value = '';
            document.getElementById('<%= txtFechaJustificacion.ClientID %>').value = '';
            document.getElementById('<%= txtNumMemo.ClientID %>').value = '';
            document.getElementById('<%= txtMotivo.ClientID %>').value = '';
            document.getElementById('<%= txtIdUsuario.ClientID %>').value = '';
            
            document.getElementById('<%= txtPeriodo.ClientID %>').selectedIndex = 0;
            document.getElementById('<%= txtLugar.ClientID %>').value = '';
            document.getElementById('<%= txtPIN.ClientID %>').value = '';
            document.getElementById('<%= txtFechaNueva.ClientID %>').value = '';
            document.getElementById('<%= ddlTipoJustificacion.ClientID %>').selectedIndex = 0;
            document.getElementById('<%= ddlTipoNueva.ClientID %>').selectedIndex = 0;
            document.getElementById('<%= ddlEmpleados.ClientID %>').selectedIndex = 0;

            // Cerrar modal
            var modal = bootstrap.Modal.getInstance(document.getElementById('modalFormulario'));
            modal.hide();
        }
        document.addEventListener("DOMContentLoaded", function () {
            const ddlTipo = document.getElementById('<%= ddlTipoJustificacion.ClientID %>');
            const ddlPeriodo = document.getElementById('<%= txtPeriodo.ClientID %>');

            ddlTipo.addEventListener("change", function () {
                const seleccion = ddlTipo.options[ddlTipo.selectedIndex].text.toUpperCase();
                if (seleccion === "VACACIONES" || seleccion === "VACACIONES BASE") {
                    ddlPeriodo.disabled = false;
                } else {
                    ddlPeriodo.selectedIndex = 0;
                    ddlPeriodo.disabled = true;
                }
            });
        });
    </script>
</body>
</html>
