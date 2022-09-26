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

namespace authorization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TNSEntities cont = new TNSEntities();
        public string code = "";
        static System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        private string GenerateCode()
        {
            string pass = "";
            string[] Nums = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string[] UpperCase = { "B", "C", "D", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "V", "W", "X", "Z", "A", "E", "U", "Y" };
            string[] LowerCase = { "b", "c", "d", "f", "g", "h", "j", "k", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "z", "a", "e", "i", "o", "u", "y" };
            char[] Symb = { '!', '&', '#', '*', '@', '$' };
            Random rnd = new Random();
            pass += Symb[rnd.Next(Symb.Length)];
            pass += Nums[rnd.Next(Nums.Length)];
            pass += UpperCase[rnd.Next(UpperCase.Length)];
            for (int i = 0; i < 6; i++)
            {
                pass += LowerCase[rnd.Next(LowerCase.Length)];
            }
            return pass;
        }

        private void EnteringCode()
        {
            btnNewCode.IsEnabled = false;
            code = GenerateCode();
            tbxCodeToCopy.Text = code;
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            btnNewCode.IsEnabled = true;
            dispatcherTimer.Stop();
        }


        public MainWindow()
        {
            InitializeComponent();
        }

        private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int x = cont.Employee.Where(k => (k.Phone.ToString().Equals(tbxPhone.Text) && k.Password.ToString().Equals(Pswbx.Password))).ToList().Count;
                if (x == 1)
                {
                    CodePanel.Visibility = Visibility.Visible;
                    EnteringCode();
                }
                else
                {
                    MessageBox.Show("Данные введены неверно", "Ошибка");
                }
            }


        }

        private void btnNewCode_Click(object sender, RoutedEventArgs e)
        {
            EnteringCode();
        }

        private void tbxCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (tbxCode.Text == code)
                {
                    List<Employee> a = cont.Employee.Where(k => (k.Phone.ToString().Equals(tbxPhone.Text) && k.Password.ToString().Equals(Pswbx.Password))).ToList();
                    Employee emp = a[0];
                    List<Position> b = cont.Position.Where(k => k.ID.Equals(emp.Position)).ToList();
                    Position pos = b[0];
                    MessageBox.Show("Успешно\nВаша роль: " + pos.Name);
                }
                else
                {
                    MessageBox.Show("Код введён неверно");
                    tbxCode.Clear();
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            tbxPhone.Clear();
            Pswbx.Clear();
            tbxCode.Clear();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            if (CodePanel.Visibility == Visibility.Hidden)
            {
                int x = cont.Employee.Where(k => (k.Phone.ToString().Equals(tbxPhone.Text) && k.Password.ToString().Equals(Pswbx.Password))).ToList().Count;
                if (x == 1)
                {
                    CodePanel.Visibility = Visibility.Visible;
                    EnteringCode();
                }
                else
                {
                    MessageBox.Show("Данные введены неверно", "Ошибка");
                }
            }
            else
            {
                if (tbxCode.Text == code)
                {
                    List<Employee> a = cont.Employee.Where(k => (k.Phone.ToString().Equals(tbxPhone.Text) && k.Password.ToString().Equals(Pswbx.Password))).ToList();
                    Employee emp = a[0];
                    List<Position> b = cont.Position.Where(k => k.ID.Equals(emp.Position)).ToList();
                    Position pos = b[0];
                    MessageBox.Show("Успешно\nВаша роль: " + pos.Name);
                }
                else
                {
                    MessageBox.Show("Код введён неверно");
                    tbxCode.Clear();
                }
            }

        }
    }
}
