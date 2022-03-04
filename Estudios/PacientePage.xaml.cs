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
    /// Lógica de interacción para PacientePage.xaml
    /// </summary>
    public partial class PacientePage : Window
    {
        ConnectionDB conDB = new ConnectionDB();

        public PacientePage()
        {
            InitializeComponent();
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
                    if (masculinoRadioButton.IsChecked == true || femeninoRadioButton.IsChecked == true)
                    {
                        sql = "INSERT INTO `pacientes` (`ci`,`nombre`, `apellido`,`fecha_nacimiento`, `edad`, `sexo`) VALUES ('" + ciTextBox.Text + "','" + nombreTextBox.Text + "', '" + apellidoTextBox.Text + "', '" + fechaNacDatePicker.SelectedDate.ToString() + "', '" + edadTextBox.Text + "', '" + s + "')";
                        conDB.ExecuteSQL(sql);
                        Succes suc = new Succes();
                        suc.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("complete todos los datos");
                }

            }
            else
            {
                MessageBox.Show("ESTE N° DE CÉDULA YA FUE REGISTRADO");
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            guardarPaciente();
        }
    }
}
