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

using System.Data.OleDb;
using System.Data;


namespace trpo_juvilir_v1
{
    /// <summary>
    /// Логика взаимодействия для database.xaml
    /// </summary>
    public partial class database : Window
    {


        public class Writedb
        {
            public string Code_Clienta { get; set; }
            public string surname { get; set; }
            public string firstname { get; set; }
            public string patronymic { get; set; }
            public string Date_of_Birth { get; set; }
            public string passport_ID { get; set; }
            public string Address { get; set; }


            public Writedb()
            {

            }

            public Writedb(string Code_Clienta, string surname, string firstname, string patronymic, string Date_of_Birth,
                string passport_ID, string Address)
            {
                this.Code_Clienta = Code_Clienta;
                this.surname = surname;
                this.firstname = firstname;
                this.patronymic = patronymic;
                this.Date_of_Birth = Date_of_Birth;
                this.passport_ID = passport_ID;
                this.Address = Address;
            }
        }
        
        public void conect()
        {
            OleDbConnection cn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=C:\\Users\\regman\\source\\repos\\trpo_juvilir_v1\\trpo_juvilir_v1\\db.mdb");

            cn.Open();

            OleDbDataAdapter da = new OleDbDataAdapter("select * from client", cn);
            OleDbCommandBuilder cb = new OleDbCommandBuilder(da);

            DataSet ds = new DataSet();

            da.Fill(ds, "Code_Clienta");


            //DatagridXAML.ItemsSource = ds.Tables["Code_Clienta"].DefaultView;
            goJob.ItemsSource = ds.Tables["Code_Clienta"].DefaultView;
            cn.Close();
        }

        public void conect2()
        {
            OleDbConnection cn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=C:\\Users\\regman\\source\\repos\\trpo_juvilir_v1\\trpo_juvilir_v1\\db.mdb");

            cn.Open();

            OleDbDataAdapter da = new OleDbDataAdapter("select * from master", cn);
            OleDbCommandBuilder cb = new OleDbCommandBuilder(da);

            DataSet ds = new DataSet();

            da.Fill(ds, "Code_master");


            //DatagridXAML.ItemsSource = ds.Tables["Code_Clienta"].DefaultView;
            goJob2.ItemsSource = ds.Tables["Code_master"].DefaultView;
            cn.Close();
            
        }

        public database()
        {
            InitializeComponent();

            conect();
            conect2();

            

            // Writedb vlad = new Writedb("0", "Vlad", "galko", "andreii4", "kogdato", "123132", "Smorgon");

            // DatagridXAML.Items.Add(vlad);
        }

        private void toolbar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void CloseBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //MainWindow mw = new MainWindow();
            Application.Current.Shutdown();
           // mw.Close();
        }

        private void ShowBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Hide();
            WindowState = WindowState.Minimized;
        }


        private void min_max_size()
        {
            if (this.WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
            }
        }
        private void HideBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            min_max_size();
            
        }

        private void toolbar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
                min_max_size();
           
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataRowView drv = goJob.SelectedItem as DataRowView;
            if (drv != null)
            {
                DataView dataView = goJob.ItemsSource as DataView;
                dataView.Table.Rows.Remove(drv.Row);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            add_client add_Client = new add_client();
            add_Client.Show();
        }
    }
}
