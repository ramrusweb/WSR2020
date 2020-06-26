using System.Windows;
using WSR2020.accounts.signin;
using WSR2020.accounts.signup;
using WSR2020.personalarea;

namespace WSR2020
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            OpenPage(pages.SignIn); // Открываем авторизацию.

            // Получение данных из таблицы.
            //var db = new DB();
            //DataTable dt_user = db.Select("SELECT * FROM [dbo].[Users]");
            ////  Перебор данных.
            //foreach (DataRow row in dt_user.Rows)
            //{
            //    // Вывод данных.
            //    string User = Convert.ToString(row["Login"]);
            //    string Pass = Convert.ToString(row["Password"]);
            //}

        }

        // Список доступных страниц.
        public enum pages
        {
            SignIn, // Авторизация.
            SignUp, // Регистрация.
            Admin // Личный кабинет.
        }

        // Метод открытия страниц.
        public void OpenPage(pages pages)
        {
            if (pages == pages.SignIn) // Если страница открытия логин.
                frame.Navigate(new SignIn(this)); // Открываем.
            else if (pages == pages.SignUp) // Если страница открытия Регистрация.
                frame.Navigate(new SignUp(this)); // Открываем.
            else if (pages == pages.Admin) // Если страница Личный кабинет.
                frame.Navigate(new Admin(this)); // Открываем.
        }
    }
}

