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
    /// Логика взаимодействия для add_client.xaml
    /// </summary>
    public partial class add_client : Window
    {
        public add_client()
        {
            InitializeComponent();
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
            //Application.Current.Shutdown();
            //mw.Close();
            this.Close();
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

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBox_GotFocus;
        }
        public string leDb_string = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=.\\db.mdb";

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            OleDbConnection cn = new OleDbConnection(leDb_string);

            cn.Open();

            // OleDbDataAdapter da = new OleDbDataAdapter("select * from client", cn);
            // OleDbCommandBuilder cb = new OleDbCommandBuilder(da);

            string [] str = new string[6];

            str[0] = t_surname.Text;
            str[1] = t_firsname.Text;
            str[2] = t_patronymic.Text;
            str[3] = t_birht.Text; //26.05.2021
            str[4] = t_passport.Text;
            str[5] = t_adress.Text;


            OleDbCommand oleDbCommand = new OleDbCommand($"insert into client([surname], [firstname], [patronymic], [Date_of_Birth], [passport_ID], [Address]) values('{str[0]}', '{str[1]}', '{str[2]}', '{str[3]}', '{str[4]}', '{str[5]}')", cn);

            oleDbCommand.ExecuteNonQuery();

            cn.Close();


            // DataSet ds = new DataSet();

            //da.Fill(ds, "Code_Clienta");


            //DatagridXAML.ItemsSource = ds.Tables["Code_Clienta"].DefaultView;
            //goJob.ItemsSource = ds.Tables["Code_Clienta"].DefaultView;



            MessageBox.Show("Добавленно");




            ProgramStatic.dbase.remove();
            ProgramStatic.dbase.conect();

        }
    }
}
