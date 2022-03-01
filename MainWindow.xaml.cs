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
using System.Windows.Navigation;
using System.Windows.Shapes;
using LaboratorioBogado.Utils;
using MySql.Data.MySqlClient;
using LaboratorioBogado.Estudios;
using System.Collections.ObjectModel;


namespace LaboratorioBogado
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ConnectionDB conDB = new ConnectionDB();
        ObservableCollection<DetallePedido> ListaDetallePedido = new ObservableCollection<DetallePedido>();
        ObservableCollection<Servicios> ListaServicios = new ObservableCollection<Servicios>();
        ObservableCollection<Servicios> ls2 = new ObservableCollection<Servicios>();
        ObservableCollection<Analisis> ListaEstudios = new ObservableCollection<Analisis>();




        public MainWindow()
        {
            InitializeComponent();
            InitData();
        }

        void InitData()
        {
            //INIT COMBO BOX SERVICIOS
            readServicios(false);
            servicioComboBox.ItemsSource = ListaServicios;
            servicioComboBox.DisplayMemberPath = "Nombre";
            servicioComboBox.SelectedValuePath = "Id";

            readServicios(true);
            filtrarServicioComboBox.ItemsSource = ls2;
            filtrarServicioComboBox.DisplayMemberPath = "Nombre";
            filtrarServicioComboBox.SelectedValuePath = "Id";

            readEstudios();
            filtrarEstudioComboBox.ItemsSource = ListaEstudios;
            filtrarEstudioComboBox.DisplayMemberPath = "Nombre";
            filtrarEstudioComboBox.SelectedValuePath = "Id";


            FechaActualLabel.Content = DateTime.Now.ToString("dd-MM-yyyy");
            fechaPedidosTextBox.Text = DateTime.Now.ToString("dd-MM-yyyy");

            edadTextBox.IsReadOnly = false;

            

            

            guardarButton.IsEnabled = false;

            buscarButton.IsEnabled = false;

            hemogramaGroupBox.Visibility = Visibility.Hidden;
            sangreGroupBox.Visibility = Visibility.Hidden;

            activarDesactivarDatos(true, 0.3);

            

            getPedidos();

            

            visibilityHistorialHemograma(false);

            //PEDIDOS
            pedidosDataGrid.SelectedValuePath = "Id";

            readOrden();

           

        }

        //READ N° ORDEN
        private void readOrden()
        {
            string lastorden = "";
            MySqlDataReader reade = conDB.ListSql("select orden from pedidos where fecha='"+ DateTime.Now.ToString("dd-MM-yyyy") + "' order by orden desc limit 1");
            while (reade.Read())
            {
                lastorden = reade.GetValue(0).ToString();
            }  
        }

        //READ SERVICIOS
        private void readServicios(bool a)
        {
          string[] serv = new string[10] { "TODOS", "Externo", "Int. Adulto", "Adulto", "Int. Pediatrico", "Urg. Pediatria", "Urg. Polivalente", "Urg. Respiratoria", "Int. Respiratorio", "Urgencia" };

            if (a==false) {
                for (int i = 1; i < 10; i++)
                {
                    Servicios s = new Servicios();
                    s.Id = i.ToString();
                    s.Nombre = serv[i];
                    ListaServicios.Add(s);
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    Servicios s = new Servicios();
                    s.Id = i.ToString();
                    s.Nombre = serv[i];
                    ls2.Add(s);
                }
            }
        }

        private void readEstudios()
        {
            string[] estudios = new string[3] { "TODOS", "Hemograma", "Quimica" };
            for (int i = 0; i < 3; i++)
            {
                Analisis a = new Analisis();
                a.Id = i.ToString();
                a.Nombre = estudios[i];
                ListaEstudios.Add(a);
            }

        }



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
        private void allOptionsSangre(bool a)
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
                if (ListaDetallePedido[i].IdAnalisis==id_analisis)
                {
                    ListaDetallePedido.Remove(ListaDetallePedido[i]);
                }
            }
        }

       

        public string returnMF()
        {
            if (masculinoRadioButton.IsChecked == true)
            {
                string s = "M";
                return s;
            }
            else
            {
                string s = "F";
                return s;
            }
        }

        //METODO PARA GUARDAR PACIENTE
        private void guardarPaciente()
        {
            string sql = "";
            string s = returnMF();

  
                MySqlDataReader reade = conDB.ListSql("select ci from pacientes where ci=" + ciTextBox.Text);
                if (reade.Read() == false)
                {
                    if (nombreTextBox.Text != "" && apellidoTextBox.Text != "" && fechaNacDatePicker.SelectedDate != null && edadTextBox.Text != "")
                    {
                        if (masculinoRadioButton.IsChecked == true || femeninoRadioButton.IsChecked == true) {
                            sql = "INSERT INTO `pacientes` (`ci`,`nombre`, `apellido`,`fecha_nacimiento`, `edad`, `sexo`) VALUES ('" + ciTextBox.Text + "','" + nombreTextBox.Text + "', '" + apellidoTextBox.Text + "', '" + fechaNacDatePicker.SelectedDate.ToString() + "', '" + edadTextBox.Text + "', '" + s + "')";
                            conDB.ExecuteSQL(sql);
                        }
                    }
                    else
                    {
                        MessageBox.Show("complete todos los datos");
                    }

                }
            
            
        }

        //OBTENER ID DE ULTIMO PEDIDO INGRESADO
        private string obtenerUltimoPedido()
        {
            string c="0";

            MySqlDataReader reade = conDB.ListSql("select id from pedidos");
            while(reade.Read())
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

        //GUARDAR PEDIDO
        private void guardarPedido()
        {
            string sql = "";
            sql = "INSERT INTO `pedidos` (`orden`, `fecha`, `id_paciente`, `servicio`) VALUES ('" + ordenTextBox.Text + "', '" + FechaActualLabel.Content + "', '" + ciTextBox.Text + "', '" + servicioComboBox.SelectedValue + "')";
            conDB.ExecuteSQL(sql);  
        }

        //GUARDAR BUTTON
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            guardarPaciente();
            guardarPedido();
            string id_ultimo_pedido = obtenerUltimoPedido();
            guardarDetallePedido(id_ultimo_pedido);
            getPedidos();
            readOrden();
        }

        //LIMPIAR DATOS
        void clearInput()
        {
            nombreTextBox.Text = "";
            apellidoTextBox.Text = "";
            edadTextBox.Text = "";
            fechaNacDatePicker.SelectedDate = null;
        }

        //ACTIVAR Y DESACTIVAR CARGA DE DATOS PERSONALES
        private void activarDesactivarDatos(bool a, double b)
        {
            //nombre
            nombreLabel.Opacity = b;
            nombreTextBox.IsReadOnly = a;
            nombreTextBox.Opacity = b;
            //apellido
            apellidoLabel.Opacity = b;
            apellidoTextBox.IsReadOnly = a;
            apellidoTextBox.Opacity = b;
            //edad
            edadLabel.Opacity = b;
            edadTextBox.IsReadOnly = a;
            edadTextBox.Opacity = b;
            //sexo
            sexoLabel.Opacity = b;
            masculinoRadioButton.Opacity = b;
            femeninoRadioButton.Opacity = b;
            //fecha_nacimiento
            fechaNacLabel.Opacity = b; 

            if (a == true)
            {
                masculinoRadioButton.IsEnabled = false;
                femeninoRadioButton.IsEnabled = false;
                fechaNacDatePicker.IsEnabled = false;
            }
            else
            {
                masculinoRadioButton.IsEnabled = true;
                femeninoRadioButton.IsEnabled = true;
            }
            
            //fecha de nacimiento
            if (a == false)
            {
                fechaNacDatePicker.IsEnabled = true;
            }
            
        }

        //BUSCAR BUTTON
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            resultadosCheckBox.IsChecked = false;
            string ci = ciTextBox.Text;
            int c = 0;
            MySqlDataReader reade = conDB.ListSql("select nombre, apellido, edad, fecha_nacimiento, sexo from pacientes where ci=" + ci);

            while (reade.Read())
            {
                nombreTextBox.Text = reade.GetValue(0).ToString();
                apellidoTextBox.Text = reade.GetValue(1).ToString();
                edadTextBox.Text = reade.GetValue(2).ToString();
                fechaNacDatePicker.SelectedDate = Convert.ToDateTime(reade.GetValue(3).ToString());
                string s = reade.GetValue(4).ToString();
                if (s == "M"){ masculinoRadioButton.IsChecked = true;} else { femeninoRadioButton.IsChecked = true; }

                c = 1;
            }

            if (c == 1){ activarDesactivarDatos(true, 1.0);} else { clearInput(); activarDesactivarDatos(false, 1.0);}      
            
        }
    
        //AGREGAR PEDIDO CHECKBOX
        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            hemogramaGroupBox.Visibility = Visibility.Visible;
            sangreGroupBox.Visibility = Visibility.Visible; 
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            allOptionsHemograma(false);
            allOptionsSangre(false);

            hemogramaGroupBox.Visibility = Visibility.Hidden;
            sangreGroupBox.Visibility = Visibility.Hidden;

            opcion1CheckBox.IsChecked = false;
            opcion2CheckBox.IsChecked = false;

            guardarButton.Visibility = Visibility.Visible;

           
        }

        private void CiTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (ciTextBox.Text == "")
            {
                guardarButton.IsEnabled = false;
                buscarButton.IsEnabled = false;
            }
            else
            {
                guardarButton.IsEnabled = true;
                buscarButton.IsEnabled = true;
            }
            activarDesactivarDatos(true, 0.3);
            clearInput();
        }


        /*------------HISTORIAL--------*/

        private void clearDateHistorial()
        {
            nombreHistorialTextBox.Text = "";
            apellidoHistorialTextBox.Text = "";
            fechaNacHistorialTextBox.Text = "";
            edadHistorialTextBox.Text = "";
        }

        private void visibilityHistorialHemograma(bool a)
        {
            if (a == false) {
                historialTabControl.Visibility = Visibility.Hidden;
                hemogramaHistorialTabItem.Visibility = Visibility.Hidden;
                gridHemogramaHistorial.Visibility = Visibility.Hidden;
            }
            else
            {
                historialTabControl.Visibility = Visibility.Visible;
                hemogramaHistorialTabItem.Visibility = Visibility.Visible;
                gridHemogramaHistorial.Visibility = Visibility.Visible;
            }
        }

        private void getHistorialHemograma()
        {
            hemogramaDataGrid.Items.Clear();

            MySqlDataReader reade = conDB.ListSql("select id, hemoglobina, hematocrito, gr, gb, plaquetas, eritro1h, eritro2h, neutrofilos, linfocitos, monocitos, eosinofilos,  basofilos, observacion from hemogramas where id=" + ciHistorialTextBox.Text);

                while (reade.Read())
                {
                    visibilityHistorialHemograma(true);

                    Hemograma h = new Hemograma();
                    h.Id = reade.GetValue(0).ToString();
                    h.Hemoglobina = reade.GetValue(1).ToString();
                    h.Hematocrito = reade.GetValue(2).ToString();
                    h.Gr = reade.GetValue(3).ToString();
                    h.Gb = reade.GetValue(4).ToString();
                    h.Plaquetas = reade.GetValue(5).ToString();
                    h.Eritro1h = reade.GetValue(6).ToString();
                    h.Eritro2h = reade.GetValue(7).ToString();
                    h.Neutrofilos = reade.GetValue(8).ToString();
                    h.Linfocitos = reade.GetValue(9).ToString();
                    h.Monocitos = reade.GetValue(10).ToString();
                    h.Eosinofilos = reade.GetValue(11).ToString();
                    h.Basofilos = reade.GetValue(12).ToString();
                    h.Observacion = reade.GetValue(13).ToString();

                    hemogramaDataGrid.Items.Add(h);

                }
            
        }

        private void ButtonBuscarHistorial_Click(object sender, RoutedEventArgs e)
        {
            visibilityHistorialHemograma(false);

            clearDateHistorial();

            int n = 0;
            MySqlDataReader reade = conDB.ListSql("select nombre, apellido, fecha_nacimiento, edad from pacientes where ci=" + ciHistorialTextBox.Text);
            while (reade.Read())
            {
                nombreHistorialTextBox.Text = reade.GetValue(0).ToString();
                apellidoHistorialTextBox.Text = reade.GetValue(1).ToString();
                fechaNacHistorialTextBox.Text = reade.GetValue(2).ToString();
                edadHistorialTextBox.Text = reade.GetValue(3).ToString();
                n = 1;
            }

            getHistorialHemograma();

            if (n==0)
            {
                MessageBox.Show("SIN COINCIDENCIAS");
            }
            
            
            
        }

        private void CiHistorialTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (ciHistorialTextBox.Text == "")
            {
                buttonBuscarHistorial.IsEnabled = false;
            }
            else
            {
                buttonBuscarHistorial.IsEnabled = true;
            }
        }

        /*----------FIN HISTORIAL----*/

        /*----------ESTUDIOS----------*/
        

        //METODO PARA OBTENER LOS PEDIDOS DEL DIA
        private void getPedidos()
        {
            pedidosDataGrid.Items.Clear();
            string fechaactual = DateTime.Now.ToString("dd-MM-yyyy");
            
       
            MySqlDataReader reade = conDB.ListSql("select id, orden, id_paciente, servicio from pedidos where fecha='"+ fechaactual +"' ");
            if (reade == null)
            {
                MessageBox.Show("ERROR DE SOBRECARGA");
            }
            else
            {
                while (reade.Read())
                {
                    Pedido p = new Pedido();

                    p.Id = reade.GetValue(0).ToString();
                    p.Orden = reade.GetValue(1).ToString();
                    p.Ci = reade.GetValue(2).ToString();

                    p.NombreApellido = getDatosPersonales(reade.GetValue(2).ToString());


                    p.Servicio = reade.GetValue(3).ToString();

                    pedidosDataGrid.Items.Add(p);
                }
            }
            
        }

        private string getDatosPersonales(string ci)
        {
            string nombreapellido = "";
            MySqlDataReader reade = conDB.ListSql("select nombre, apellido from pacientes where ci=" + ci);
            if (reade==null)
            {
                MessageBox.Show("error en datos personales");
            }
            else
            {
                while (reade.Read())
                {
                    string nombre = reade.GetValue(0).ToString();
                    string apellido = reade.GetValue(1).ToString();
                    nombreapellido = nombre + " " + apellido;
                } 
            }
            return nombreapellido;
        }

        private void DetalleButton_Click(object sender, RoutedEventArgs e)
        {
            string a = pedidosDataGrid.SelectedValue.ToString();
            DetallePage dp = new DetallePage(a);
            dp.ShowDialog();
        }

        private void CargarButton_Click(object sender, RoutedEventArgs e)
        {
            string id_pedido = pedidosDataGrid.SelectedValue.ToString();
            CargarPage cp = new CargarPage(id_pedido);
            cp.ShowDialog();
        }

        private void ImprimirButton_Click(object sender, RoutedEventArgs e)
        {
            string id_pedido = pedidosDataGrid.SelectedValue.ToString();
            ImprimirPage ip = new ImprimirPage(id_pedido);
            ip.ShowDialog();
        }
        /*---------FIN ESTUDIOS---------*/



        /*----METODOS Y EVENTOS REPETITIVOS-----------*/

        /*--------------SELECCION DE ESTUDIOS------------*/

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
            allOptionsSangre(true);
        }

        private void Opcion2CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            allOptionsSangre(false);
        }

        /*FUNCIONES PARA HABILITAR Y DESHABILITAR LAS OPCIONES INDIVIDUALES*/

        /* HEMOGRAMA SUBOPCIONES*/
        //HEMOGLOBINA
        private void HemoglobinaCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("1");
        }

        private void HemoglobinaCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("1");
        }

        //HEMATOCRITO
        private void HematocritoCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("2");
        }

        private void HematocritoCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("2");
        }

        //GLOBULOS ROJOS
        private void GrCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("3");
        }

        private void GrCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("3");
        }

        //GLOBULOS BLANCOS
        private void GbCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("4");
        }

        private void GbCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("4");
        }

        //PLAQUETAS
        private void PlaquetasCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("5");
        }

        private void PlaquetasCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("5");
        }

        //ERITROSEDIMENTACION
        private void EritroCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("6");
        }

        private void EritroCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("6");
        }

        //NEUTRÓFILOS
        private void NeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("7");
        }

        private void NeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("7");
        }

        //LINFOCITOS
        private void LinCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("8");
        }

        private void LinCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("8");
        }

        //MONOCITOS
        private void MonoCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("9");
        }

        private void MonoCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("9");
        }

        //EOSINOFILOS
        private void EosCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("10");
        }

        private void EosCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("10");
        }

        //BASOFILOS
        private void BasoCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("11");
        }

        private void BasoCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("11");
        }

        /*SUBOPCIONES ANALISIS DE SANGRE*/
        //GLICEMIA
        private void GlicemiaCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("12");
        }

        private void GlicemiaCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("12");
        }

        //UREA
        private void UreaCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("13");
        }

        private void UreaCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("13");
        }

        //ACIDO URICO
        private void AcidouCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("14");
        }

        private void AcidouCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("14");
        }

        //COLESTEROL TOTAL
        private void ColesterolCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("15");
        }

        private void ColesterolCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("15");
        }

        //TRIGLICÉRIDOS
        private void TrigliceridosCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("16");
        }

        private void TrigliceridosCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("16");
        }

        //CREATININA
        private void CreatininaCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("17");
        }

        private void CreatininaCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("17");
        }

        //GOT
        private void GotCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("18");
        }

        private void GotCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("18");
        }

        //GPT
        private void GptCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("19");
        }

        private void GptCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("19");
        }

        //FOSFATASA ALCALINA
        private void FosfatasaCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("20");
        }

        private void FosfatasaCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("20");
        }

        //AMILASA
        private void AmilasaCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cargaDetalle("21");
        }

        private void AmilasaCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            removeDetalle("21");
        }

        private void FiltrarButton_Click(object sender, RoutedEventArgs e)
        {
            int codigo = 0;
            if (filtrarServicioComboBox.SelectedValue!=null)
            {
                if (filtrarServicioComboBox.SelectedValue.ToString() != "0") { codigo = 7; }
            }

            if (filtrarEstudioComboBox.SelectedValue != null)
            {
                if (filtrarEstudioComboBox.SelectedValue.ToString() != "0") {  codigo = codigo + 2;}
            }

            if (filtrarFechaDatePecker.SelectedDate!=null){ codigo = codigo + 3;}

            switch (codigo)
            {
                case 0: MessageBox.Show("SELECCIONE ALGUN PARAMETRO DE FILTRO"); break;
                case 7: MessageBox.Show("FILTRAR POR SERVICIO"); break;
                case 2: MySqlDataReader reade = conDB.ListSql("select id from pedidos where fecha='" + fechaPedidosTextBox.Text + "' inner join  ");
                    while (reade.Read())
                    {
                        MessageBox.Show(reade.GetValue(0).ToString());
                    }

                    ; break;
                case 3: MessageBox.Show("FILTRAR POR FECHA"); break;
                case 9: MessageBox.Show("FILTRAR POR SERVICIO + ESTUDIO"); break;
                case 10: MessageBox.Show("FILTRAR POR SERVICIO + FECHA"); break;
                case 5: MessageBox.Show("FILTRAR POR ESTUDIO + FECHA"); break;
                case 12: MessageBox.Show("FILTRAR POR SERVICIO + ESTUDIO + FECHA"); break;
            }

        }



        /*---------------------------------------------------------*/

        /*----FIN METODOS Y EVENTOS REPETITIVOS-----------*/
    }
}
