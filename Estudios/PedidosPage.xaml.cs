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
        ObservableCollection<Servicios> ListaServicios = new ObservableCollection<Servicios>();
        ConnectionDB conDB = new ConnectionDB();

        public PedidosPage()
        {
            InitializeComponent();
            initData();
        }

        void initData()
        {
            FechaActualLabel.Content = DateTime.Now.ToString("dd-MM-yyyy");
            //INIT COMBO BOX SERVICIOS
            readServicios();
            servicioComboBox.ItemsSource = ListaServicios;
            servicioComboBox.DisplayMemberPath = "Nombre";
            servicioComboBox.SelectedValuePath = "Id";
        }

        //READ SERVICIOS
        private void readServicios()
        {
            string[] serv = new string[10] { "TODOS", "Externo", "Int. Adulto", "Adulto", "Int. Pediatrico", "Urg. Pediatria", "Urg. Polivalente", "Urg. Respiratoria", "Int. Respiratorio", "Urgencia" };

                for (int i = 1; i < 10; i++)
                {
                    Servicios s = new Servicios();
                    s.Id = i.ToString();
                    s.Nombre = serv[i];
                    ListaServicios.Add(s);
                }
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

        //GUARDAR PEDIDO
        private void guardarPedido()
        {
            string sql = "";
            sql = "INSERT INTO `pedidos` (`orden`, `fecha`, `id_paciente`, `servicio`) VALUES ('" + ordenLabel.Content + "', '" + FechaActualLabel.Content + "', '" + ciTextBox.Text + "', '" + servicioComboBox.SelectedValue + "')";
            conDB.ExecuteSQL(sql);
        }

        //OBTENER ID DE ULTIMO PEDIDO INGRESADO
        private string obtenerUltimoPedido()
        {
            string c = "0";

            MySqlDataReader reade = conDB.ListSql("select id from pedidos");
            while (reade.Read())
            {
                c = reade.GetValue(0).ToString();
            }
            return c;
        }

        //GUARDAR DETALLE PEDIDO
        private void guardarDetallePedido(string lastid)
        {
            foreach (DetallePedido b in ListaDetallePedido)
            {
                string sql = "";
                sql = "INSERT INTO `detallepedidos` (`id_pedido`, `id_analisis`) VALUES ('" + lastid + "', '" + b.IdAnalisis + "')";
                conDB.ExecuteSQL(sql);
            }
            ListaDetallePedido.Clear();
            
        }

        //GUARDAR PEDIDO BUTTON
        private void GuardarPedidoButton_Click(object sender, RoutedEventArgs e)
        {
            if (ListaDetallePedido.Count()==0) {
                MessageBox.Show("SELECCIONE ALGÚN ESTUDIO");
            }
            else
            {
                guardarPedido();
                string id_ultimo_pedido = obtenerUltimoPedido();
                guardarDetallePedido(id_ultimo_pedido);
 
            }
        }

        

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            if (ciTextBox.Text!="") {
                MySqlDataReader reade = conDB.ListSql("select nombre, apellido, edad, fecha_nacimiento from pacientes where ci=" + ciTextBox.Text);

                if (reade != null)
                {
                    while (reade.Read())
                    {
                        nombreLabel.Content = reade.GetValue(0).ToString();
                        apellidoLabel.Content = reade.GetValue(1).ToString();
                        edadLabel.Content = reade.GetValue(2).ToString();
                        fechaNacLabel.Content = reade.GetValue(3).ToString();
                    }

                }
                else
                {
                    MessageBox.Show("ERROR EN LA CARGA DE DATOS", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("INGRESE UN N° DE DOCUMENTO", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        /*----METODOS Y EVENTOS REPETITIVOS-----------*/
        //FUNCION PARA MARCAR-DESMARCAR SUBOPCIONES DEL HEMOGRAMA
        private void allOptionsHemograma(bool a)
        {
            hemoglobinaCheckBox.IsChecked = a;
            hematocritoCheckBox.IsChecked = a;
            grCheckBox.IsChecked = a;
            gbCheckBox.IsChecked = a;
            plaquetasCheckBox.IsChecked = a;
            eritroCheckBox.IsChecked = a;
            neCheckBox.IsChecked = a;
            linCheckBox.IsChecked = a;
            monoCheckBox.IsChecked = a;
            eosCheckBox.IsChecked = a;
            basoCheckBox.IsChecked = a;
        }

        //FUNCION PARA MARCAR-DESMARCAR SUBOPCIONES DEL ANALISIS DE SANGRE
        private void allOptionsQuimica(bool a)
        {
            glicemiaCheckBox.IsChecked = a;
            ureaCheckBox.IsChecked = a;
            acidouCheckBox.IsChecked = a;
            colesterolCheckBox.IsChecked = a;
            trigliceridosCheckBox.IsChecked = a;
            creatininaCheckBox.IsChecked = a;
            gotCheckBox.IsChecked = a;
            gptCheckBox.IsChecked = a;
            fosfatasaCheckBox.IsChecked = a;
            amilasaCheckBox.IsChecked = a;
        }



        //HEMOGRAMA OPCION 1 CHECK BOX
        private void Opcion1CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            allOptionsHemograma(true);
        }

        private void Opcion1CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            allOptionsHemograma(false);
        }

        //QUIMICA OPCION 2 CHECK BOX
        private void Opcion2CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            allOptionsQuimica(true);
        }

        private void Opcion2CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            allOptionsQuimica(false);
        }



        /*FUNCIONES PARA HABILITAR Y DESHABILITAR LAS OPCIONES INDIVIDUALES*/

        /* HEMOGRAMA SUBOPCIONES*/
        //hemoglobina
        private void HemoglobinaCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("1");
        }

        private void HemoglobinaCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("1");
        }

        //hematocrito
        private void HematocritoCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("2");
        }

        private void HematocritoCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("2");
        }

        //glóbulos rojos
        private void GrCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("3");
        }

        private void GrCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("3");
        }

        //glóbulos blancos
        private void GbCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("4");
        }

        private void GbCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("4");
        }

        //plaquetas
        private void PlaquetasCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("5");
        }

        private void PlaquetasCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("5");
        }

        //eritrosedimentación
        private void EritroCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("6");
        }

        private void EritroCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("6");
        }

        //neutrófilos
        private void NeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("7");
        }

        private void NeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("7");
        }

        //linfocitos
        private void LinCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("8");
        }

        private void LinCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("8");
        }

        //monocitos
        private void MonoCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("9");
        }

        private void MonoCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("9");
        }

        //eosinofilos
        private void EosCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("10");
        }

        private void EosCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("10");
        }

        //basofilos
        private void BasoCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("11");
        }

        private void BasoCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("11");
        }

        /* QUIMICA SUBOPCIONES*/

        //glicemia
        private void GlicemiaCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("12");
        }

        private void GlicemiaCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("12");
        }

        //urea
        private void UreaCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("13");
        }

        private void UreaCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("13");
        }

        //ácido úrico
        private void AcidouCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("14");
        }

        private void AcidouCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("14");
        }


        //colesterol total
        private void ColesterolCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("15");
        }

        private void ColesterolCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("15");
        }

        //triglicéridos
        private void TrigliceridosCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("16");
        }

        private void TrigliceridosCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("16");
        }

        //creatinina
        private void CreatininaCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("17");
        }

        private void CreatininaCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("17");
        }

        //g.o.t
        private void GotCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("18");
        }

        private void GotCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("18");
        }

        //g.p.t
        private void GptCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("19");
        }

        private void GptCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("19");
        }

        //fosfatasa alcalina
        private void FosfatasaCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("20");
        }

        private void FosfatasaCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("20");
        }

        //amilasa
        private void AmilasaCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("21");
        }

        private void AmilasaCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("21");
        }

        
















































































        /*---- FIN METODOS Y EVENTOS REPETITIVOS-----------*/
    }
}
