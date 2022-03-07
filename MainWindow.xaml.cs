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
            
            readServicios(true);
            filtrarServicioComboBox.ItemsSource = ls2;
            filtrarServicioComboBox.DisplayMemberPath = "Nombre";
            filtrarServicioComboBox.SelectedValuePath = "Id";

            readEstudios();
            filtrarEstudioComboBox.ItemsSource = ListaEstudios;
            filtrarEstudioComboBox.DisplayMemberPath = "Nombre";
            filtrarEstudioComboBox.SelectedValuePath = "Id";


        
            fechaPedidosTextBox.Text = DateTime.Now.ToString("dd-MM-yyyy");

            

            

            

            


            

           getPedidos();

            

            visibilityHistorialHemograma(false);

            //PEDIDOS
            pedidosDataGrid.SelectedValuePath = "Id";

            

            

           

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
                MessageBox.Show("METODO GUARDAR DETALLE");
            }
            
            ListaDetallePedido.Clear();
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
          
            MySqlDataReader reade = conDB.ListSql("select id, hemoglobina, hematocrito, gr, gb, plaquetas, eritro1h, eritro2h, neutrofilos, linfocitos, monocitos, eosinofilos,  basofilos, observacion from hemogramas where ci=" + ciHistorialTextBox.Text);
            Hemograma hem = new Hemograma();
            hem.Fecha = "REFERENCIA";
            hemogramaDataGrid.Items.Add(hem);

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
            MySqlDataReader reade = conDB.ListSql("select p.id, p.orden, p.id_paciente, pac.nombre, pac.apellido, serv.nombre from pedidos as p inner join detallepedidos as dp  on p.id = dp.id_pedido inner join servicios as serv on p.servicio = serv.id inner join pacientes as pac on p.id_paciente = pac.ci inner join estudios as e on e.id = dp.id_analisis where p.fecha = '" + DateTime.Now.ToString("dd-MM-yyyy") + "' group by id; ");
            while (reade.Read())
            {
                Pedido p = new Pedido();
                p.Id = reade.GetValue(0).ToString();
                p.Orden = reade.GetValue(1).ToString();
                p.Ci = reade.GetValue(2).ToString();
                p.Nombre = reade.GetValue(3).ToString();
                p.Apellido = reade.GetValue(4).ToString();
                p.Servicio = reade.GetValue(5).ToString();

                pedidosDataGrid.Items.Add(p);
                
            }

        }

        private void updatePedidos()
        {
            MySqlDataReader reade = conDB.ListSql("select p.id, p.orden, p.id_paciente, pac.nombre, pac.apellido, serv.nombre from pedidos as p inner join detallepedidos as dp  on p.id = dp.id_pedido inner join servicios as serv on p.servicio = serv.id inner join pacientes as pac on p.id_paciente = pac.ci inner join estudios as e on e.id = dp.id_analisis where p.fecha = '" + DateTime.Now.ToString("dd-MM-yyyy") + "' group by id; ");
            while (reade.Read())
            {
                Pedido p = new Pedido();
                p.Id = reade.GetValue(0).ToString();
                p.Orden = reade.GetValue(1).ToString();
                p.Ci = reade.GetValue(2).ToString();
                p.Nombre = reade.GetValue(3).ToString();
                p.Apellido = reade.GetValue(4).ToString();
                p.Servicio = reade.GetValue(5).ToString();

                pedidosDataGrid.Items.Add(p);
            }
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

            if (filtrarFechaDatePecker.SelectedDate!=null){ codigo = codigo + 3; fechaPedidosTextBox.Text = Convert.ToDateTime(filtrarFechaDatePecker.SelectedDate).ToString("dd-MM-yyyy"); }

            switch (codigo)
            {
                case 0: MessageBox.Show("SELECCIONE ALGUN PARAMETRO DE FILTRO"); break;
                case 7: filterServicios(fechaPedidosTextBox.Text); break;
                case 2: filterEstudios(fechaPedidosTextBox.Text); break;
                case 3: filterFecha(Convert.ToDateTime(filtrarFechaDatePecker.SelectedDate).ToString("dd-MM-yyyy")); break;
                case 9: filterServicioEstudio(fechaPedidosTextBox.Text); break;
                case 10: filterServicios(Convert.ToDateTime(filtrarFechaDatePecker.SelectedDate).ToString("dd-MM-yyyy")); break;
                case 5: filterEstudios(Convert.ToDateTime(filtrarFechaDatePecker.SelectedDate).ToString("dd-MM-yyyy")); break;
                case 12: filterServicioEstudio(Convert.ToDateTime(filtrarFechaDatePecker.SelectedDate).ToString("dd-MM-yyyy")); break;
            }

        }

        private void filterServicioEstudio(string fecha)
        {
            pedidosDataGrid.Items.Clear();
            MySqlDataReader reade = conDB.ListSql("select p.id, p.orden, p.id_paciente, pac.nombre, pac.apellido, serv.nombre from pedidos as p inner join detallepedidos as dp  on p.id = dp.id_pedido inner join servicios as serv on p.servicio = serv.id inner join pacientes as pac on p.id_paciente = pac.ci inner join estudios as e on e.id = dp.id_analisis where  p.servicio = '" + filtrarServicioComboBox.SelectedValue.ToString() + "' and e.id_grupo_estudios = '" + filtrarEstudioComboBox.SelectedValue.ToString() + "' and p.fecha = '" + fecha + "' group by id; ");
            while (reade.Read())
            {
                Pedido p = new Pedido();
                p.Id = reade.GetValue(0).ToString();
                p.Orden = reade.GetValue(1).ToString();
                p.Ci = reade.GetValue(2).ToString();
                p.Nombre = reade.GetValue(3).ToString();
                p.Apellido = reade.GetValue(4).ToString();
                p.Servicio = reade.GetValue(5).ToString();

                pedidosDataGrid.Items.Add(p);
            }
        }

        private void filterFecha(string fecha)
        {
            pedidosDataGrid.Items.Clear();
            MySqlDataReader reade = conDB.ListSql("select p.id, p.orden, p.id_paciente, pac.nombre, pac.apellido, serv.nombre from pedidos as p inner join detallepedidos as dp  on p.id = dp.id_pedido inner join servicios as serv on p.servicio = serv.id inner join pacientes as pac on p.id_paciente = pac.ci inner join estudios as e on e.id = dp.id_analisis where p.fecha = '" + fecha + "' group by id; ");
            while (reade.Read())
            {
                Pedido p = new Pedido();
                p.Id = reade.GetValue(0).ToString();
                p.Orden = reade.GetValue(1).ToString();
                p.Ci = reade.GetValue(2).ToString();
                p.Nombre = reade.GetValue(3).ToString();
                p.Apellido = reade.GetValue(4).ToString();
                p.Servicio = reade.GetValue(5).ToString();

                pedidosDataGrid.Items.Add(p);
            }
        }

        private void filterServicios(string fecha)
        {
            pedidosDataGrid.Items.Clear();
            MySqlDataReader reade = conDB.ListSql("select p.id, p.orden, p.id_paciente, pac.nombre, pac.apellido, serv.nombre from pedidos as p inner join detallepedidos as dp  on p.id = dp.id_pedido inner join servicios as serv on p.servicio = serv.id inner join pacientes as pac on p.id_paciente = pac.ci inner join estudios as e on e.id = dp.id_analisis where p.fecha = '"+ fecha +"' and p.servicio = '"+ filtrarServicioComboBox.SelectedValue.ToString() +"'  group by id; ");
            while (reade.Read())
            {
                Pedido p = new Pedido();
                p.Id = reade.GetValue(0).ToString();
                p.Orden = reade.GetValue(1).ToString();
                p.Ci = reade.GetValue(2).ToString();
                p.Nombre = reade.GetValue(3).ToString();
                p.Apellido = reade.GetValue(4).ToString();
                p.Servicio = reade.GetValue(5).ToString();

                pedidosDataGrid.Items.Add(p);
            }

        }

        

        private void filterEstudios(string fecha)
        {
            pedidosDataGrid.Items.Clear();
            MySqlDataReader reade = conDB.ListSql("select p.id, p.orden, p.id_paciente, pac.nombre, pac.apellido, serv.nombre from pedidos as p inner join detallepedidos as dp on p.id = dp.id_pedido inner join servicios as serv on p.servicio = serv.id inner join pacientes as pac on p.id_paciente = pac.ci inner join estudios as e on e.id = dp.id_analisis where p.fecha = '" + fecha + "' and e.id_grupo_estudios = '"+filtrarEstudioComboBox.SelectedValue+"' group by id; ");
            while (reade.Read())
            {
                Pedido p = new Pedido();
                p.Id = reade.GetValue(0).ToString();
                p.Orden = reade.GetValue(1).ToString();
                p.Ci = reade.GetValue(2).ToString();
                p.Nombre = reade.GetValue(3).ToString();
                p.Apellido = reade.GetValue(4).ToString();
                p.Servicio = reade.GetValue(5).ToString();

                pedidosDataGrid.Items.Add(p);
            }
        }

        //MOSTRAR TODO BUTTON
        private void MostrarTodoButton_Click(object sender, RoutedEventArgs e)
        {
            filtrarEstudioComboBox.SelectedValue = null;
            filtrarServicioComboBox.SelectedValue = null;
            filtrarFechaDatePecker.SelectedDate = null;
            fechaPedidosTextBox.Text = DateTime.Now.ToString("dd-MM-yyyy");
            getPedidos();
        }

        private void AgregarPedidoButton_Click(object sender, RoutedEventArgs e)
        {
            PedidosPage pg = new PedidosPage();
            pg.ShowDialog();
        }

        private void SolicitudButton_Click(object sender, RoutedEventArgs e)
        {
            PedidosPage pg = new PedidosPage();
            pg.ShowDialog();
        }

        //cancelar
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            PedidosPage pp = new PedidosPage(); 
        }

        private void EstudiosButton_Click(object sender, RoutedEventArgs e)
        {
            updatePedidos();
            pedidosTabItem.IsSelected = true;
            
        }

        private void NuevoPacienteButton_Click(object sender, RoutedEventArgs e)
        {
            PacientePage pp = new PacientePage();
            pp.ShowDialog();
        }

        private void NuevoPedidoButton_Click(object sender, RoutedEventArgs e)
        {
            PedidosPage pp = new PedidosPage();
            pp.ShowDialog();
        }

        

        

















        /*---------------------------------------------------------*/

        /*----FIN METODOS Y EVENTOS REPETITIVOS-----------*/
    }
}
