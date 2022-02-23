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
    /// Lógica de interacción para ImprimirPage.xaml
    /// </summary>
    public partial class ImprimirPage : Window
    {
        ConnectionDB conDB = new ConnectionDB();

        public ImprimirPage(string id_pedido)
        {
            InitializeComponent();
            imprimir(id_pedido);

        }

        private void imprimir(string id_pedido)
        {
            readHemograma(id_pedido);
            readQuimica(id_pedido);
        }

        private void readQuimica(string id_pedido)
        {
            MySqlDataReader reade = conDB.ListSql("select glicemia, urea, acido_urico, colesterol_total, trigliceridos, creatinina, got, gpt, fosfatasa_alcalina, amilasa,  observacion from quimica where id_pedido=" + id_pedido);
            while (reade.Read())
            {
                Impresion espacio = new Impresion();
                espacio.Parametro = "";
                espacio.Valor = "";
                espacio.Unidad = "";
                espacio.ValorReferencia = "";
                imprimirDataGrid.Items.Add(espacio);

                Impresion i = new Impresion();
                i.Parametro = "QUIMICA";
                i.Valor = "";
                i.Unidad = "";
                i.ValorReferencia = "";
                imprimirDataGrid.Items.Add(i);

                if (reade.GetValue(0).ToString() != "")
                {
                    Impresion glic = new Impresion();
                    glic.Parametro = "Glicemia";
                    glic.Valor = reade.GetValue(0).ToString();
                    imprimirDataGrid.Items.Add(glic);
                }

                if (reade.GetValue(1).ToString() != "")
                {
                    Impresion urea = new Impresion();
                    urea.Parametro = "Urea";
                    urea.Valor = reade.GetValue(1).ToString();
                    imprimirDataGrid.Items.Add(urea);
                }

                if (reade.GetValue(2).ToString() != "")
                {
                    Impresion au = new Impresion();
                    au.Parametro = "Acido Úrico";
                    au.Valor = reade.GetValue(2).ToString();
                    imprimirDataGrid.Items.Add(au);
                }

                if (reade.GetValue(3).ToString() != "")
                {
                    Impresion ct = new Impresion();
                    ct.Parametro = "Colesterol Total";
                    ct.Valor = reade.GetValue(3).ToString();
                    imprimirDataGrid.Items.Add(ct);
                }

                if (reade.GetValue(4).ToString() != "")
                {
                    Impresion trig = new Impresion();
                    trig.Parametro = "Trigliceridos";
                    trig.Valor = reade.GetValue(4).ToString();
                    imprimirDataGrid.Items.Add(trig);
                }

                if (reade.GetValue(5).ToString() != "")
                {
                    Impresion cre = new Impresion();
                    cre.Parametro = "Creatinina";
                    cre.Valor = reade.GetValue(5).ToString();
                    imprimirDataGrid.Items.Add(cre);
                }

                if (reade.GetValue(6).ToString() != "")
                {
                    Impresion got = new Impresion();
                    got.Parametro = "G.O.T";
                    got.Valor = reade.GetValue(6).ToString();
                    imprimirDataGrid.Items.Add(got);
                }

                if (reade.GetValue(7).ToString() != "")
                {
                    Impresion gpt = new Impresion();
                    gpt.Parametro = "G.P.T";
                    gpt.Valor = reade.GetValue(7).ToString();
                    imprimirDataGrid.Items.Add(gpt);
                }

                if (reade.GetValue(8).ToString() != "")
                {
                    Impresion fa = new Impresion();
                    fa.Parametro = "Fosfatasa Alcalina";
                    fa.Valor = reade.GetValue(8).ToString();
                    imprimirDataGrid.Items.Add(fa);
                }

                if (reade.GetValue(9).ToString() != "")
                {
                    Impresion am = new Impresion();
                    am.Parametro = "Amilasa";
                    am.Valor = reade.GetValue(9).ToString();
                    imprimirDataGrid.Items.Add(am);
                }

                if (reade.GetValue(10).ToString() != "")
                {
                    Impresion obs = new Impresion();
                    obs.Parametro = "Observación";
                    obs.Valor = reade.GetValue(10).ToString();
                    imprimirDataGrid.Items.Add(obs);
                }
            }
        }

        private void readHemograma(string id_pedido)
        {
            MySqlDataReader reade = conDB.ListSql("select hemoglobina, hematocrito, gr, gb, plaquetas, eritro1h, eritro2h, neutrofilos, linfocitos, monocitos, eosinofilos,  basofilos, observacion from hemogramas where id_pedido=" + id_pedido);
            while (reade.Read())
            {
                Impresion i = new Impresion();
                i.Parametro = "HEMOGRAMA";
                i.Valor = "";
                i.Unidad = "";
                i.ValorReferencia = "";
                imprimirDataGrid.Items.Add(i);

                if (reade.GetValue(0).ToString()!="")
                {
                    Impresion hemo = new Impresion();
                    hemo.Parametro = "Hemoglobina";
                    hemo.Valor = reade.GetValue(0).ToString();
                    hemo.Unidad = "g/dl";
                    imprimirDataGrid.Items.Add(hemo);
                }

                if (reade.GetValue(1).ToString() != "") {
                    Impresion hema = new Impresion();
                    hema.Parametro = "Hematocrito";
                    hema.Valor = reade.GetValue(1).ToString();
                    imprimirDataGrid.Items.Add(hema);
                }

                if (reade.GetValue(2).ToString() != "")
                {
                    Impresion gr = new Impresion();
                    gr.Parametro = "Glóbulos Rojos";
                    gr.Valor = reade.GetValue(2).ToString();
                    gr.Unidad = "mm³";
                    imprimirDataGrid.Items.Add(gr);
                }

                if (reade.GetValue(3).ToString() != "")
                {
                    Impresion gb = new Impresion();
                    gb.Parametro = "Glóbulos Blancos";
                    gb.Valor = reade.GetValue(3).ToString();
                    imprimirDataGrid.Items.Add(gb);
                }

                if (reade.GetValue(4).ToString() != "")
                {
                    Impresion plaq = new Impresion();
                    plaq.Parametro = "Plaquetas";
                    plaq.Valor = reade.GetValue(4).ToString();
                    imprimirDataGrid.Items.Add(plaq);
                }

                if (reade.GetValue(5).ToString() != ""||reade.GetValue(6).ToString() != "")
                {
                    Impresion eritro = new Impresion();
                    eritro.Parametro = "ERITROSEDIMENTACIÓN";
                    eritro.Valor = "";
                    imprimirDataGrid.Items.Add(eritro);

                    Impresion erit_1 = new Impresion();
                    erit_1.Parametro = "1° Hora";
                    erit_1.Valor = reade.GetValue(5).ToString();
                    imprimirDataGrid.Items.Add(erit_1);

                    Impresion erit_2 = new Impresion();
                    erit_2.Parametro = "2° Hora";
                    erit_2.Valor = reade.GetValue(6).ToString();
                    imprimirDataGrid.Items.Add(erit_2);
                }

                if (reade.GetValue(7).ToString() != "")
                {
                    Impresion neu = new Impresion();
                    neu.Parametro = "Neutrofilos";
                    neu.Valor = reade.GetValue(7).ToString();
                    imprimirDataGrid.Items.Add(neu);
                }

                if (reade.GetValue(8).ToString() != "")
                {
                    Impresion lin = new Impresion();
                    lin.Parametro = "Linfocitos";
                    lin.Valor = reade.GetValue(8).ToString();
                    imprimirDataGrid.Items.Add(lin);
                }

                if (reade.GetValue(9).ToString() != "")
                {
                    Impresion mon = new Impresion();
                    mon.Parametro = "Monocitos";
                    mon.Valor = reade.GetValue(9).ToString();
                    imprimirDataGrid.Items.Add(mon);
                }

                if (reade.GetValue(10).ToString() != "")
                {
                    Impresion eos = new Impresion();
                    eos.Parametro = "Eosinofilos";
                    eos.Valor = reade.GetValue(10).ToString();
                    imprimirDataGrid.Items.Add(eos);
                }

                if (reade.GetValue(11).ToString() != "")
                {
                    Impresion bas = new Impresion();
                    bas.Parametro = "Basofilos";
                    bas.Valor = reade.GetValue(11).ToString();
                    imprimirDataGrid.Items.Add(bas);
                }

                if (reade.GetValue(12).ToString() != "")
                {
                    Impresion obs = new Impresion();
                    obs.Parametro = "Observación";
                    obs.Valor = reade.GetValue(12).ToString();
                    imprimirDataGrid.Items.Add(obs);
                }





            }



        }

        private void ImprimirButton_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();

            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(impresionGeneralGrid, "My First Print Job");
            }
        }
    }
}
