using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LaboratorioBogado.Utils;
using MySql.Data.MySqlClient;
using LaboratorioBogado.Estudios;
using System.Collections.ObjectModel;

namespace LaboratorioBogado
{
    /// <summary>
    /// Lógica de interacción para DetallePage.xaml
    /// </summary>
    public partial class DetallePage : Window
    {
        ConnectionDB conDB = new ConnectionDB();
        ObservableCollection<DetallePedido> ListaDetallePedido = new ObservableCollection<DetallePedido>();

        public DetallePage(string a)
        {
            InitializeComponent();
            getDetallePedido(a);
        }

        private void getDetallePedido(string a)
        {
            MySqlDataReader reade = conDB.ListSql("select id_analisis from detallepedidos where id_pedido="+a);
            while (reade.Read())
            {
                DetallePedido dp = new DetallePedido();
                dp.Nombre = getNombreEstudio(reade.GetValue(0).ToString());
                detallepedidoDataGrid.Items.Add(dp);
            }
        }

        private string getNombreEstudio(string id_estudio)
        {
            string nombre = "";
            MySqlDataReader reade = conDB.ListSql("select nombre from estudios where id=" + id_estudio);
            while (reade.Read())
            {
                nombre = reade.GetValue(0).ToString();
            }
            return nombre;
        }
    }
}
