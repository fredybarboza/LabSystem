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

namespace LaboratorioBogado.Estudios
{
    /// <summary>
    /// Lógica de interacción para PedidosPage.xaml
    /// </summary>
    public partial class PedidosPage : Window
    {
        ObservableCollection<DetallePedido> ListaDetallePedido = new ObservableCollection<DetallePedido>();

        public PedidosPage()
        {
            InitializeComponent();
        }

        private void HemoglobinaCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("1");
        }

        private void HemoglobinaCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("1");
        }

        private void cargaDetalle(string id_analisis)
        {
            DetallePedido dp = new DetallePedido();
            dp.IdAnalisis = id_analisis;
            ListaDetallePedido.Add(dp);
        }
        

        private void removeDetalle(string id_analisis)
        {
            for (int i = 0; i < ListaDetallePedido.Count; i++)
            {
                if (ListaDetallePedido[i].IdAnalisis == id_analisis)
                {
                    ListaDetallePedido.Remove(ListaDetallePedido[i]);
                }
            }
        }

        
    }
}
