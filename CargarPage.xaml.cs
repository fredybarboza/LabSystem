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
    /// Lógica de interacción para CargarPage.xaml
    /// </summary>
    public partial class CargarPage : Window
    {
        ConnectionDB conDB = new ConnectionDB();
        string pedido = "";
        public CargarPage(string id_pedido)
        {
            InitializeComponent();

            //INICIALIZAR TABCONTROl
            visibilityEstudios(false);

            pedido = id_pedido;
            initTabControl(id_pedido);

            estadoHemogramaTextBox.IsReadOnly = true;

            inicializarButtons();


        }

        //Muestra o oculta el tabcontrol
        private void visibilityEstudios(bool a)
        {
            if (a == false)
            {
                estudiosTabControl.Visibility = Visibility.Hidden;


                tabItem1.Visibility = Visibility.Collapsed;
                grid1.Visibility = Visibility.Collapsed;

                tabItem2.Visibility = Visibility.Collapsed;
                grid2.Visibility = Visibility.Collapsed;

                tabItem3.Visibility = Visibility.Collapsed;
                grid3.Visibility = Visibility.Collapsed;

                tabItem4.Visibility = Visibility.Collapsed;
                grid4.Visibility = Visibility.Collapsed;

                //HEMOGRAMA
                activarDesactivarHemograma();

                //SANGRE
                activarDesactivarQuimica();
               

            }
            else
            {
                estudiosTabControl.Visibility = Visibility.Visible;
            }
        }

        //INICIALIZA EL TABCONTROL DE CARGA
        private void initTabControl(string id_pedido)
        {


            MySqlDataReader reade = conDB.ListSql("select id_analisis from detallepedidos where id_pedido=" + id_pedido);
            while (reade.Read())
            {
                string g = getGrupo(reade.GetValue(0).ToString());

                switch (g)//ASIGNAR A GRUPO DE ANALISIS
                {
                    case "1": visibilityHemograma(reade.GetValue(0).ToString()); break;
                    case "2": visibilityQuimica(reade.GetValue(0).ToString()); ; break;
                    default: hemoglobinaTextBox.Text = "SIN GRUPO"; break;
                }
            }

            initDateHemograma(id_pedido);
        }


        private string getGrupo(string id_analisis)
        {
            string id_grupo = "";
            MySqlDataReader reade = conDB.ListSql("select id_grupo_estudios from estudios where id=" + id_analisis);
            while (reade.Read())
            {
                id_grupo = reade.GetValue(0).ToString();
            }

            return id_grupo;
        }

        //VALIDAR BUTTON
        private void ValidarButton_Click(object sender, RoutedEventArgs e)
        {
            //hemograma
            if (tabItem1.IsSelected == true)
            {
                bool n = validateHemograma();
                if (n == true)
                {
                    estadoHemogramaTextBox.Text = "VALIDADO";
                    editarButton.IsEnabled = false;
                    guardarButton.IsEnabled = false;
                    validarButton.IsEnabled = false;
                    imprimirButton.IsEnabled = true;
                    MessageBox.Show("VALIDADO");
                }
                else
                {
                    MessageBox.Show("Complete todos los campos", "ERROR DE VALIDACION", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        //editar button
        private void EditarButton_Click(object sender, RoutedEventArgs e)
        {
            validarButton.IsEnabled = false;
            imprimirButton.IsEnabled = false;
            editarButton.IsEnabled = false;
            guardarButton.IsEnabled = true;
            if (tabItem1.IsSelected == true)
            {
                estadoHemogramaTextBox.Text = "EDITANDO";
                controlTextBoxHemograma(false);
            }

            if (tabItem2.IsSelected == true)
            {
                estadoQuimicaTextBox.Text = "EDITANDO";
            }

        }

        //GUARDAR BUTTON
        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            editarButton.IsEnabled = true;
            validarButton.IsEnabled = true;
            imprimirButton.IsEnabled = false;
            guardarButton.IsEnabled = false;
            if (tabItem1.IsSelected == true)
            {
                guardarHemograma();
            }

            if (tabItem2.IsSelected == true)
            {

            }

        }

        private void EstudiosTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            controlButtons(true);

            if (tabItem1.IsSelected)
            {
                checkStatus(estadoHemogramaTextBox.Text);
            }

            if (tabItem2.IsSelected)
            {
                checkStatus(estadoQuimicaTextBox.Text);
            }
        }


        private void checkStatus(string s)
        {
            switch (s)
            {
                case "GUARDADO": guardarButton.IsEnabled = false; imprimirButton.IsEnabled = false; break;
                case "EDITANDO": validarButton.IsEnabled = false; guardarButton.IsEnabled = true; editarButton.IsEnabled = false; imprimirButton.IsEnabled = false; break;
                case "VALIDADO": guardarButton.IsEnabled = false; editarButton.IsEnabled = false; validarButton.IsEnabled = false; imprimirButton.IsEnabled = true; break;
                case "SIN GUARDAR": imprimirButton.IsEnabled = false; editarButton.IsEnabled = false; validarButton.IsEnabled = false; break;
            }
        }


        private void inicializarButtons()
        {
            if (tabItem1.IsSelected == true)
            {
                checkStatus(estadoHemogramaTextBox.Text);
            }

            if (tabItem2.IsSelected == true)
            {
                checkStatus(estadoQuimicaTextBox.Text);
            }
        }

        private void controlButtons(bool a)
        {
            guardarButton.IsEnabled = a;
            editarButton.IsEnabled = a;
            validarButton.IsEnabled = a;
            imprimirButton.IsEnabled = a;
        }

        

        /*--------METODOS Y EVENTOS PARA EL PANEL HEMOGRAMA-------*/

        //validacion hemograma
        private bool validateHemograma()
        {
            bool n = true;
            if (hemoglobinaTextBox.Opacity == 1.0 && hemoglobinaTextBox.Text == "") { n = false; }
            if (hematocritoTextBox.Opacity == 1.0 && hematocritoTextBox.Text == "") { n = false; }
            if (grTextBox.Opacity == 1.0 && grTextBox.Text == "") { n = false; }
            if (gbTextBox.Opacity == 1.0 && gbTextBox.Text == "") { n = false; }
            if (plaquetasTextBox.Opacity == 1.0 && plaquetasTextBox.Text == "") { n = false; }
            if (h1TextBox.Opacity == 1.0 && h1TextBox.Text == "") { n = false; }
            if (h2TextBox.Opacity == 1.0 && h2TextBox.Text == "") { n = false; }
            if (neuTextBox.Opacity == 1.0 && neuTextBox.Text == "") { n = false; }
            if (linTextBox.Opacity == 1.0 && linTextBox.Text == "") { n = false; }
            if (monoTextBox.Opacity == 1.0 && monoTextBox.Text == "") { n = false; }
            if (eoTextBox.Opacity == 1.0 && eoTextBox.Text == "") { n = false; }
            if (basTextBox.Opacity == 1.0 && basTextBox.Text == "") { n = false; }

            return n;
        }

        //muestra los datos del hemograma si ya existen en la bd
        private void initDateHemograma(string id_pedido)
        {
            int c = 0;
            MySqlDataReader reade = conDB.ListSql("select hemoglobina, hematocrito, gr, gb, plaquetas, eritro1h, eritro2h, neutrofilos, linfocitos, monocitos, eosinofilos,  basofilos, observacion from hemogramas where id_pedido=" + id_pedido);

            while (reade.Read())
            {
                c++;
                hemoglobinaTextBox.Text = reade.GetValue(0).ToString();
                hematocritoTextBox.Text = reade.GetValue(1).ToString();
                grTextBox.Text = reade.GetValue(2).ToString();
                gbTextBox.Text = reade.GetValue(3).ToString();
                plaquetasTextBox.Text = reade.GetValue(4).ToString();
                h1TextBox.Text = reade.GetValue(5).ToString();
                h2TextBox.Text = reade.GetValue(6).ToString();
                neuTextBox.Text = reade.GetValue(7).ToString();
                linTextBox.Text = reade.GetValue(8).ToString();
                monoTextBox.Text = reade.GetValue(9).ToString();
                eoTextBox.Text = reade.GetValue(10).ToString();
                basTextBox.Text = reade.GetValue(11).ToString();
                observacionTextBox.Text = reade.GetValue(12).ToString();



                controlTextBoxHemograma(true);

                estadoHemogramaTextBox.Text = "GUARDADO";

            }

            if (c == 0)
            {
                estadoHemogramaTextBox.Text = "SIN GUARDAR";
            }

        }
        //metodo para activar o desactivar textbox hemograma
        private void controlTextBoxHemograma(bool a)
        {
            if (hemoglobinaTextBox.Opacity==1.0){ hemoglobinaTextBox.IsReadOnly = a; }
            if (hematocritoTextBox.Opacity == 1.0) { hematocritoTextBox.IsReadOnly = a; }
            if (grTextBox.Opacity == 1.0) { grTextBox.IsReadOnly = a; }
            if (gbTextBox.Opacity == 1.0) { gbTextBox.IsReadOnly = a; }
            if (plaquetasTextBox.Opacity == 1.0) { plaquetasTextBox.IsReadOnly = a; }
            if (h1TextBox.Opacity == 1.0) { h1TextBox.IsReadOnly = a; }
            if (h2TextBox.Opacity == 1.0) { h2TextBox.IsReadOnly = a; }
            if (neuTextBox.Opacity == 1.0) { neuTextBox.IsReadOnly = a; }
            if (linTextBox.Opacity == 1.0) { linTextBox.IsReadOnly = a; }
            if (monoTextBox.Opacity == 1.0) { monoTextBox.IsReadOnly = a; }
            if (eoTextBox.Opacity == 1.0) { eoTextBox.IsReadOnly = a; }
            if (basTextBox.Opacity == 1.0) { basTextBox.IsReadOnly = a; }
            if (observacionTextBox.Opacity == 1.0) { observacionTextBox.IsReadOnly = a; }
        }

        //activa o desactiva la carga de datos según la selección
        private void visibilityHemograma(string id_analisis)
        {
            tabItem1.Visibility = Visibility.Visible;
            grid1.Visibility = Visibility.Visible;
            tabItem1.IsSelected = true;
            visibilityEstudios(true);

            switch (id_analisis)
            {
                case "1": ActivarDesactivarHemoglobina(false, 1.0); break;
                case "2": ActivarDesactivarHematocrito(false, 1.0); break;
                case "3": ActivarDesactivarGR(false, 1.0); break;
                case "4": ActivarDesactivarGB(false, 1.0); ; break;
                case "5": ActivarDesactivarPlaquetas(false, 1.0); ; break;
                case "6": ActivarDesactivarEritro(false, 1.0); ; break;
                case "7": ActivarDesactivarNeutrofilos(false, 1.0); ; break;
                case "8": ActivarDesactivarLinfocitos(false, 1.0); ; break;
                case "9": ActivarDesactivarMonocitos(false, 1.0); ; break;
                case "10": ActivarDesactivarEosinofilos(false, 1.0); ; break;
                case "11": ActivarDesactivarBasofilos(false, 1.0); ; break;
            }

        }

        //guardar nuevo hemograma o guardar edicion
        private void guardarHemograma()
        {
            string id_hemograma = "";
            MySqlDataReader reade = conDB.ListSql("select id from hemogramas where id_pedido=" + pedido);
            while (reade.Read())
            {
                id_hemograma = reade.GetValue(0).ToString();
            }

            if (tabItem1.Visibility == Visibility.Visible)
            {
                if (id_hemograma == "") {
                    string sql = "";
                    sql = "INSERT INTO `hemogramas` (`id_pedido`,`ci`, `fecha`, `hemoglobina`, `hematocrito`, `gr`, `gb`, `plaquetas`, `eritro1h`, `eritro2h`, `neutrofilos`, `linfocitos`, `monocitos`, `eosinofilos`, `basofilos`, `observacion`) VALUES ('" + pedido + "', '" + 5128575 + "', '" + DateTime.Now.ToString("dd-MM-yyyy") + "', '" + hemoglobinaTextBox.Text + "', '" + hematocritoTextBox.Text + "', '" + grTextBox.Text + "', '" + gbTextBox.Text + "', '" + plaquetasTextBox.Text + "', '" + h1TextBox.Text + "', '" + h2TextBox.Text + "', '" + neuTextBox.Text + "', '" + linTextBox.Text + "', '" + monoTextBox.Text + "', '" + eoTextBox.Text + "', '" + basTextBox.Text + "', '" + observacionTextBox.Text + "')";
                    conDB.ExecuteSQL(sql);
                    estadoHemogramaTextBox.Text = "GUARDADO";
                    initDateHemograma(pedido);
                }
                else//editar
                {
                    string sql = "";
                    sql = "UPDATE `hemogramas` SET `hemoglobina` = '" + hemoglobinaTextBox.Text + "', `hematocrito` = '" + hematocritoTextBox.Text + "', `gr` = '" + grTextBox.Text + "', `gb` = '" + gbTextBox.Text + "', `plaquetas` = '" + plaquetasTextBox.Text + "', `eritro1h` = '" + h1TextBox.Text + "', `eritro2h` = '" + h2TextBox.Text + "', `neutrofilos` = '" + neuTextBox.Text + "', `linfocitos` = '" + linTextBox.Text + "', `monocitos` = '" + monoTextBox.Text + "', `eosinofilos` = '" + eoTextBox.Text + "', `basofilos` = '" + basTextBox.Text + "', `observacion` = '" + observacionTextBox.Text + "' WHERE `id_pedido`= " + pedido;
                    conDB.ExecuteSQL(sql);

                    estadoHemogramaTextBox.Text = "GUARDADO";

                    initDateHemograma(pedido);
                }
            }
        }



        /*--------FIN METODOS Y EVENTOS PARA EL PANEL HEMOGRAMA-------*/



        /*--------METODOS Y EVENTOS PARA EL PANEL QUIMICA-------*/

        //activa o desactiva la carga de datos segun la selección
        private void visibilityQuimica(string id_analisis)
        {
            tabItem2.Visibility = Visibility.Visible;
            grid2.Visibility = Visibility.Visible;
            tabItem2.IsSelected = true;
            visibilityEstudios(true);
            switch (id_analisis)
            {
                case "12": ActivarDesactivarGlicemia(false, 1.0); ; break;
                case "13": ActivarDesactivarUrea(false, 1.0); ; break;
                case "14": ActivarDesactivarAcidoUrico(false, 1.0); ; break;
                case "15": ActivarDesactivarColesterol(false, 1.0); ; break;
                case "16": ActivarDesactivarTrigliceridos(false, 1.0); ; break;
                case "17": ActivarDesactivarCreatinina(false, 1.0); ; break;
                case "18": ActivarDesactivarGOT(false, 1.0); ; break;
                case "19": ActivarDesactivarGPT(false, 1.0); ; break;
                case "20": ActivarDesactivarFosfatasa(false, 1.0); ; break;
                case "21": ActivarDesactivarAmilasa(false, 1.0); ; break;
            }

        }

        //guardar quimica
        private void guardarQuimica()
        {
            if (tabItem2.Visibility == Visibility.Visible)
            {
                string sql = "";
                sql = "INSERT INTO `quimica` (`id_pedido`,`ci`, `fecha`, `glicemia`, `urea`, `acido_urico`, `colesterol_total`, `trigliceridos`, `creatinina`, `got`, `gpt`, `fosfatasa_alcalina`, `amilasa`, `observacion`) VALUES ('" + pedido + "', '" + 5128575 + "', '" + DateTime.Now.ToString("dd-MM-yyyy") + "' , '" + glicemiaTextBox.Text + "', '" + ureaTextBox.Text + "', '" + acidouricoTextBox.Text + "', '" + colesterolTextBox.Text + "', '" + trigliceridosTextBox.Text + "', '" + creatininaTextBox.Text + "', '" + gotTextBox.Text + "', '" + gptTextBox.Text + "', '" + fosfatasaTextBox.Text + "', '" + acidouricoTextBox.Text + "', '" + observacion2TextBox.Text + "')";
                conDB.ExecuteSQL(sql);
            }
        }

        /*--------FIN METODOS Y EVENTOS PARA EL PANEL QUIMICA-------*/



        /*----METODOS Y EVENTOS REPETITIVOS-----------*/

        private void activarDesactivarHemograma()
        {
            ActivarDesactivarHemoglobina(true, 0.3);
            ActivarDesactivarHematocrito(true, 0.3);
            ActivarDesactivarGR(true, 0.3);
            ActivarDesactivarGB(true, 0.3);
            ActivarDesactivarPlaquetas(true, 0.3);
            ActivarDesactivarEritro(true, 0.3);
            ActivarDesactivarNeutrofilos(true, 0.3);
            ActivarDesactivarLinfocitos(true, 0.3);
            ActivarDesactivarMonocitos(true, 0.3);
            ActivarDesactivarEosinofilos(true, 0.3);
            ActivarDesactivarBasofilos(true, 0.3);
        }

        private void activarDesactivarQuimica()
        {
            ActivarDesactivarGlicemia(true, 0.3);
            ActivarDesactivarUrea(true, 0.3);
            ActivarDesactivarAcidoUrico(true, 0.3);
            ActivarDesactivarColesterol(true, 0.3);
            ActivarDesactivarTrigliceridos(true, 0.3);
            ActivarDesactivarCreatinina(true, 0.3);
            ActivarDesactivarGOT(true, 0.3);
            ActivarDesactivarGPT(true, 0.3);
            ActivarDesactivarFosfatasa(true, 0.3);
            ActivarDesactivarAmilasa(true, 0.3);
        }

        /*FUNCIONES PARA HABILITAR Y DESHABILITAR LAS OPCIONES INDIVIDUALES*/

        //HEMOGRAMA
        private void ActivarDesactivarHemoglobina(bool a, double b)
        {
            hemoglobinaTextBox.IsReadOnly = a;
            hemoglobinaTextBox.Opacity = b;
            hemoglobinaLabel.Opacity = b;
            u1.Opacity = b;
            vr1.Opacity = b;
        }

        private void ActivarDesactivarHematocrito(bool a, double b)
        {
            hematocritoTextBox.IsReadOnly = a;
            hematocritoTextBox.Opacity = b;
            hematocritoLabel.Opacity = b;
            u2.Opacity = b;
            vr2.Opacity = b;
        }

        private void ActivarDesactivarGR(bool a, double b)
        {
            grTextBox.IsReadOnly = a;
            grTextBox.Opacity = b;
            grLabel.Opacity = b;
            u3.Opacity = b;
            vr3.Opacity = b;
        }

        private void ActivarDesactivarGB(bool a, double b)
        {
            gbTextBox.IsReadOnly = a;
            gbTextBox.Opacity = b;
            gbLabel.Opacity = b;
            u4.Opacity = b;
            vr4.Opacity = b;
        }

        private void ActivarDesactivarPlaquetas(bool a, double b)
        {
            plaquetasTextBox.IsReadOnly = a;
            plaquetasTextBox.Opacity = b;
            plaquetasLabel.Opacity = b;
            u5.Opacity = b;
            vr5.Opacity = b;
        }

        private void ActivarDesactivarEritro(bool a, double b)
        {
            h1TextBox.IsReadOnly = a;
            h2TextBox.IsReadOnly = a;
            h1TextBox.Opacity = b;
            h2TextBox.Opacity = b;
            eritroLabel.Opacity = b;
            u6.Opacity = b;
            u7.Opacity = b;
        }

        private void ActivarDesactivarNeutrofilos(bool a, double b)
        {
            neuTextBox.IsReadOnly = a;
            neuTextBox.Opacity = b;
            neuLabel.Opacity = b;
            u8.Opacity = b;
        }

        private void ActivarDesactivarLinfocitos(bool a, double b)
        {
            linTextBox.IsReadOnly = a;
            linTextBox.Opacity = b;
            linLabel.Opacity = b;
            u9.Opacity = b;
        }

        private void ActivarDesactivarMonocitos(bool a, double b)
        {
            monoTextBox.IsReadOnly = a;
            monoTextBox.Opacity = b;
            monoLabel.Opacity = b;
            u10.Opacity = b;
        }

        private void ActivarDesactivarEosinofilos(bool a, double b)
        {
            eoTextBox.IsReadOnly = a;
            eoTextBox.Opacity = b;
            eoLabel.Opacity = b;
            u11.Opacity = b;
        }

        private void ActivarDesactivarBasofilos(bool a, double b)
        {
            basTextBox.IsReadOnly = a;
            basTextBox.Opacity = b;
            basLabel.Opacity = b;
            u12.Opacity = b;
        }


        //ANALISIS DE SANGRE
        private void ActivarDesactivarGlicemia(bool a, double b)
        {
            glicemiaTextBox.IsReadOnly = a;
            glicemiaTextBox.Opacity = b;
            glicemiaLabel.Opacity = b;
            u13.Opacity = b;
        }

        private void ActivarDesactivarUrea(bool a, double b)
        {
            ureaTextBox.IsReadOnly = a;
            ureaTextBox.Opacity = b;
            ureaLabel.Opacity = b;
            u14.Opacity = b;
        }

        private void ActivarDesactivarAcidoUrico(bool a, double b)
        {
            acidouricoTextBox.IsReadOnly = a;
            acidouricoTextBox.Opacity = b;
            acidoUricoLabel.Opacity = b;
            u15.Opacity = b;
        }

        private void ActivarDesactivarColesterol(bool a, double b)
        {
            colesterolTextBox.IsReadOnly = a;
            colesterolTextBox.Opacity = b;
            colesterolLabel.Opacity = b;
            u16.Opacity = b;
        }

        private void ActivarDesactivarTrigliceridos(bool a, double b)
        {
            trigliceridosTextBox.IsReadOnly = a;
            trigliceridosTextBox.Opacity = b;
            trigliceridosLabel.Opacity = b;
            u17.Opacity = b;
        }

        private void ActivarDesactivarCreatinina(bool a, double b)
        {
            creatininaTextBox.IsReadOnly = a;
            creatininaTextBox.Opacity = b;
            creatininaLabel.Opacity = b;
            u18.Opacity = b;
        }

        private void ActivarDesactivarGOT(bool a, double b)
        {
            gotTextBox.IsReadOnly = a;
            gotTextBox.Opacity = b;
            gotLabel.Opacity = b;
            u19.Opacity = b;
        }

        private void ActivarDesactivarGPT(bool a, double b)
        {
            gptTextBox.IsReadOnly = a;
            gptTextBox.Opacity = b;
            gptLabel.Opacity = b;
            u20.Opacity = b;
        }

        private void ActivarDesactivarFosfatasa(bool a, double b)
        {
            fosfatasaTextBox.IsReadOnly = a;
            fosfatasaTextBox.Opacity = b;
            fosfatasaLabel.Opacity = b;
            u21.Opacity = b;
        }

        private void ActivarDesactivarAmilasa(bool a, double b)
        {
            amilasaTextBox.IsReadOnly = a;
            amilasaTextBox.Opacity = b;
            amilasaLabel.Opacity = b;
            u22.Opacity = b;
        }

        /*---FIN METODOS Y EVENTOS REPETITIVOS--------*/

    }
}
