<%@ Page Language="VB" AutoEventWireup="true" CodeFile="WFrm_Incidencias.aspx.vb" Inherits="WFrm_Incidencias" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Incidencias con Estilo</title>

    <!-- Bootstrap 5.3 -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>

    <!-- Animaciones propias -->
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
            from {
                opacity: 0;
                transform: translateY(30px);
            }
            to {
                opacity: 1;
                transform: translateY(0);
            }
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
        <div class="container">
            <div class="card card-custom p-4">
                <h2 class="text-center text-dark mb-4">📋 Consulta de Incidencias</h2>

                <div class="text-center mb-3">
                    <asp:Button ID="btnProbarConexion" runat="server" Text="🔌 Probar Conexión"
                        CssClass="btn btn-primary btn-animated me-2" OnClick="btnProbarConexion_Click" />

                    <asp:Button ID="btnMostrarVista" runat="server" Text="📊 Mostrar Incidencias"
                        CssClass="btn btn-success btn-animated" OnClick="btnMostrarVista_Click" />
                </div>

                <asp:Label ID="lblResultado" runat="server" CssClass="mensaje text-white fs-5" />

                <div class="gridview-container">
                    <asp:GridView ID="gvVista" runat="server" AutoGenerateColumns="true"
                        CssClass="table table-bordered gridview-custom" GridLines="None" />

                </div>
            </div>
        </div>
    </form>
</body>
</html>
