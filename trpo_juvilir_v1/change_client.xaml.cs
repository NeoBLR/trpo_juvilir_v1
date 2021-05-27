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

using System.Data.OleDb;
using System.Data;

namespace trpo_juvilir_v1
{
    /// <summary>
    /// Логика взаимодействия для change_client.xaml
    /// </summary>
    public partial class change_client : Window
    {
        public change_client()
        {
            InitializeComponent();
        }
        public string leDb_string = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=.\\db.mdb";

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            OleDbConnection cn = new OleDbConnection(leDb_string);

            cn.Open();

            // OleDbDataAdapter da = new OleDbDataAdapter("select * from client", cn);
            // OleDbCommandBuilder cb = new OleDbCommandBuilder(da);

            string[] str = new string[6];

            str[0] = t_surname.Text;
            str[1] = t_firsname.Text;
            str[2] = t_patronymic.Text;
            str[3] = t_birht.Text; //26.05.2021
            str[4] = t_passport.Text;
            str[5] = t_adress.Text;


            string zapros = $"UPDATE client SET surname = '{str[0]}', firstname = '{str[1]}', patronymic = '{str[2]}', Date_of_Birth = '{str[3]}', passport_ID = '{str[4]}', Address = '{str[5]}' WHERE Code_Clienta = {ProgramStatic.dbase.selected_id}";

            //OleDbCommand oleDbCommand = new OleDbCommand($"UPDATE client SET surname = '{t_surname}', firstname = '{t_firsname}', patronymic = '{t_patronymic}', Date_of_Birth = '{t_birht}', passport_ID = '{t_passport}', Address = '{t_adress}' WHERE Code_Clienta = {ProgramStatic.dbase.selected_id}", cn);

            MessageBox.Show(zapros);

            OleDbCommand oleDbCommand = new OleDbCommand(zapros, cn);
            oleDbCommand.ExecuteNonQuery();

            cn.Close();

            ProgramStatic.dbase.remove();
            ProgramStatic.dbase.conect();


            //UPDATE goods SET price = 150 WHERE num = 2

            //MessageBox.Show("Ok");
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
            //this.Close();
            this.Hide();
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

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBox_GotFocus;
        }


    }
}
