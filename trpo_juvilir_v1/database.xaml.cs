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
        
        public void remove()
        {
            goJob.ItemsSource = null;
           //MessageBox.Show("removed");

        }

        public void conect()
        {
            OleDbConnection cn = new OleDbConnection(leDb_string);

            cn.Open();

            OleDbDataAdapter da = new OleDbDataAdapter("select * from client", cn);
            OleDbCommandBuilder cb = new OleDbCommandBuilder(da);

            DataSet ds = new DataSet();

            da.Fill(ds, "Code_Clienta");


            //DatagridXAML.ItemsSource = ds.Tables["Code_Clienta"].DefaultView;
            goJob.ItemsSource = ds.Tables["Code_Clienta"].DefaultView;
            cn.Close();

            //MessageBox.Show("connected");
        }

        public void conect2()
        {
            OleDbConnection cn = new OleDbConnection(leDb_string);

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
            add_Client.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            remove();
            conect();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

            //goJob.SelectedItem



            //MessageBox.Show($"id {clid} name {clname}");

            ProgramStatic.cclient.Show();
        }

        public int selected_id;
        private void goJob_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            DataGrid dg = (DataGrid)sender;
            DataRowView row_selected = dg.SelectedItem as DataRowView;

            if (row_selected != null)
            {
                selected_id = Convert.ToInt32(row_selected["Code_Clienta"].ToString());

                ProgramStatic.cclient.t_surname.Text = row_selected["surname"].ToString();
                ProgramStatic.cclient.t_firsname.Text = row_selected["firstname"].ToString();
                ProgramStatic.cclient.t_patronymic.Text = row_selected["patronymic"].ToString();
                ProgramStatic.cclient.t_birht.Text = row_selected["Date_of_Birth"].ToString();
                ProgramStatic.cclient.t_passport.Text = row_selected["passport_ID"].ToString();
                ProgramStatic.cclient.t_adress.Text = row_selected["Address"].ToString();


            }



        }
        public string leDb_string = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=.\\db.mdb";
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

            OleDbConnection cn = new OleDbConnection(leDb_string);

            cn.Open();

            // OleDbDataAdapter da = new OleDbDataAdapter("select * from client", cn);
            // OleDbCommandBuilder cb = new OleDbCommandBuilder(da);

            string[] str = new string[6];

            OleDbCommand oleDbCommand = new OleDbCommand($"DELETE FROM client WHERE Code_Clienta = {selected_id}", cn);

            oleDbCommand.ExecuteNonQuery();

            cn.Close();

            remove();
            conect();

        }
    }
}
