using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace KursShop.Pages
{
    /// <summary>
    /// Логика взаимодействия для login.xaml
    /// </summary>
    public partial class login : Page
    {
        private bool LoginOperation = true;

        private int QuantityLogin = 0; //Количество попыток войти

        public static bool ActiveKapcha { get; set; }

        public static bool FalseRegister { get; set; }

        public login()
        {
            InitializeComponent();

            ActiveKapcha = false;

            FalseRegister = true;

            LvLPassword.Visibility = Visibility.Collapsed;
            CheckLoginBox.Visibility = Visibility.Collapsed;
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (LoginOperation)
            {
                try
                {
                    var currentUser = App.Context.Clients.FirstOrDefault(p => p.login == LoginBox.Text && p.password == PasswordBox.Text);

                    if (ActiveKapcha)
                    {
                        MessageBox.Show("Введите капчу!", "Внимание!");
                    }
                    else if (currentUser.login.Trim() == LoginBox.Text & currentUser.password.Trim() == PasswordBox.Text)
                    {
                        if (QuantityLogin > 3)
                        {
                            ActiveKapcha = true;
                            Kapcha kapcha = new Kapcha();
                            kapcha.Show();
                        }

                        MainWindow.IdClients = currentUser.login;
                        MainWindow.IdRoleUser = Convert.ToInt32(currentUser.id_role);

                        App.CurrentUser = currentUser;
                        NavigationService.Navigate(new ProductsList());
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль. Попробуйте еще раз");

                        QuantityLogin += 1;

                        if (QuantityLogin > 1)
                        {
                            ActiveKapcha = true;
                            Kapcha kapcha = new Kapcha();
                            kapcha.Show();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Нет соединения с сервером!");
                }
            }
            else
            {
                try
                {
                    if (FalseRegister)
                    {
                        if (CheckLoginBox.Text == "Логин занят другим пользователем")
                        {
                            MessageBox.Show("Данный логин занят другим пользователем", "Внимание!");
                        }
                        else if (CheckLoginBox.Text == "Использование пробелов запрещено!")
                        {
                            MessageBox.Show("Использование пробелов запрещено!", "Внимание!");
                        }
                        else if (LvLPassword.Text != "")
                        {
                            MessageBox.Show("Введите хороший корректный пароль", "Внимание!");
                        }
                    }
                    else
                    {
                        if(LoginBox.Text == "")
                        {
                            MessageBox.Show("Введите логин", "Внимание!");
                        }
                        else if (LoginBox.Text.Length < 5)
                        {
                            MessageBox.Show("Логин слишком короткий", "Внимание!");
                        }
                        else if (PasswordBox.Text == "")
                        {
                            MessageBox.Show("Введите пароль", "Внимание!");
                        }
                        else if (PasswordBox.Text.Length > 10)
                        {
                            MessageBox.Show("Пароль не может превышать 10 символов!", "Внимание!");
                        }
                        else
                        {
                            var UserAdd = new Entities.Clients
                            {
                                login = LoginBox.Text,
                                password = PasswordBox.Text,
                                id_role = 1 //User
                            };
                            App.Context.Clients.Add(UserAdd);
                            App.Context.SaveChanges();
                            MessageBox.Show("Вы успешно зарегистрировались!");

                            var currentUser = App.Context.Clients.FirstOrDefault(p => p.login == LoginBox.Text && p.password == PasswordBox.Text);
                            MainWindow.IdClients = currentUser.login;

                            NavigationService.Navigate(new ProductsList());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Нет соединения с сервером!");
                }
            }
        }

        private void BtnSmenLogin_Click(object sender, RoutedEventArgs e)
        {
            if (ActiveKapcha)
            {
                MessageBox.Show("Введите капчу!", "Внимание!");
            }
            else
            {
                if (LoginOperation)
                {
                    BtnLogin.Content = "Зарегистрироваться";
                    RegistrAndLogin.Text = "Уже зарегистрированы?";
                    BtnSmenLogin.Content = "Войти";
                    LoginTextBlock.Text = "Регистрация";
                    LoginOperation = false;
                    LvLPassword.Visibility = Visibility.Visible;
                    CheckLoginBox.Visibility = Visibility.Visible;
                }
                else
                {
                    BtnLogin.Content = "Войти";
                    RegistrAndLogin.Text = "Не зарегистрированы?";
                    BtnSmenLogin.Content = "Зарегистрироваться";
                    LoginTextBlock.Text = "Авторизация";
                    LoginOperation = true;
                    LvLPassword.Visibility = Visibility.Collapsed;
                    CheckLoginBox.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void PasswordBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                AppLog appLog = new AppLog();
                LvLPassword.Text = appLog.UpdateLvLPassword(PasswordBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Нет соединения с сервером!");
            }
        }

        private void LoginBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                AppLog appLog = new AppLog();
                CheckLoginBox.Text = appLog.CheckLogin(LoginBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Нет соединения с сервером!");
            }
        }
    }
}
