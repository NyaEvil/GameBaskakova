using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.Eventing.Reader;
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
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace GameBaskakova
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        static private string Login;

        static private string Password;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Reg_Auth.SelectedIndex= 1;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Reg_Auth.SelectedIndex= 0;
        }

        private void LoginName_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (LoginName.Text == "Логин")
                LoginName.Text = "";
        }

        private void LoginPass_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (LoginPass.Text == "Пароль")
                LoginPass.Text = "";
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(gitlink.NavigateUri.ToString());
        }

        private void RegPass_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (RegPass.Text == "Пароль")
                RegPass.Text = "";
        }

        private void RegName_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (RegName.Text == "Логин")
                RegName.Text = "";
        }

        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            ConnectionCheck();
            var connection = new MySqlConnection("server=nyasqlgame.servegame.com;uid=root;pwd=BaskakovaGame123!;database=sys");
            connection.Open();
            string commandstr = $"SELECT * FROM sys.test WHERE name = '{LoginName.Text}' AND pass = '{LoginPass.Text}'";
            var commandexec = new MySqlCommand(commandstr, connection);
            var reader = commandexec.ExecuteReader();
            if (reader.Read())
            {
                MessageBox.Show("Авторизация успешна");
                Login = LoginName.Text;
                Password = LoginPass.Text;
                Application.Current.MainWindow = new GameBaskakova.Views.Window1();
                Application.Current.MainWindow.Show();
                this.Close();
            } else
            {
                MessageBox.Show("Что-то не так");
            }
            reader.Close();
            connection.Close();
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            ConnectionCheck();
            try
            {
                var connection = new MySqlConnection("server=nyasqlgame.servegame.com;uid=root;pwd=BaskakovaGame123!;database=sys");
                connection.Open();
                string commandstr = $"INSERT INTO sys.test VALUES (null,'{RegName.Text}', '{RegPass.Text}')";
                var commandexec = new MySqlCommand(commandstr, connection);
                commandexec.ExecuteNonQuery();
                commandstr = $"SELECT * FROM sys.test WHERE name = '{RegName.Text}' AND pass = '{RegPass.Text}'";
                commandexec = new MySqlCommand(commandstr, connection);
                var reader = commandexec.ExecuteReader();
                if (reader.Read())
                {
                    MessageBox.Show("Регистрация успешна");
                    Login = RegName.Text;
                    Password = RegPass.Text;
                    Application.Current.MainWindow = new GameBaskakova.Views.Window1();
                    Application.Current.MainWindow.Show();
                    this.Close();
                } else
                {
                    MessageBox.Show("Что-то пошло не так");
                }
                connection.Close();
            } 
            catch (Exception er)
            {
                var connection = new MySqlConnection("server=nyasqlgame.servegame.com;uid=root;pwd=BaskakovaGame123!;database=sys");
                connection.Open();
                var commandexec = new MySqlCommand("select max(idtest) from sys.test", connection);
                MySqlDataReader myReader;
                myReader = commandexec.ExecuteReader();
                myReader.Read();
                int i = Convert.ToInt32(myReader.GetString(0));
                myReader.Close();
                i -= 1;
                commandexec = new MySqlCommand($"ALTER TABLE sys.test AUTO_INCREMENT={i}", connection);
                commandexec.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show(er.ToString());
            }
        }

        private void ConCheck_Loaded(object sender, RoutedEventArgs e)
        {
            ConnectionCheck();
        }

        private void ConnectionCheck()
        {
            bool check = false;
            ConCheck.Maximum = 3;
            ConCheck.Value = 1;
            ConLabel.Content = "Загрузка";
            try
            {
                ConCheck.Value = 2;
                ConLabel.Content = "Попытка открыть соединение";
                var connection = new MySqlConnection("server=nyasqlgame.servegame.com;uid=root;pwd=BaskakovaGame123!;database=sys");
                connection.Open();
                var commandexec = new MySqlCommand("select max(idtest) from sys.test", connection);
                MySqlDataReader myReader = commandexec.ExecuteReader();
                check = myReader.Read();
                connection.Close();
                myReader.Close();
            }
            catch (Exception er)
            {
                ConCheck.Background = new SolidColorBrush(Colors.Red);
                MessageBox.Show(er.ToString(), "Ошибка подключения");
                ConLabel.Content = "Ошибка подключения";
            }
            finally
            {
                if (check)
                {
                    ConLabel.Content = "Соединение установлено";
                    ConCheck.Value = 3;
                }
            }
        }
    }
}
