using Microsoft.Win32;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using WSR2020.db;
using WSR2020.handlers;

namespace WSR2020.accounts.signin
{
    public partial class SignIn : Page
    {
        public MainWindow mainWindow;
        MsgBox msgbox = new MsgBox();
        DB db = new DB();
        public SignIn(MainWindow _mainWindow) // Принимаем ссылку на форму.
        {
            InitializeComponent();
            // Запоминаем ссылку на форму.
            mainWindow = _mainWindow;
            signInBtnHandler();
            _Collapse_xExit_DragNDropHeader();
        }
        // Обработчики кнопок Свернуть и Закрыть; Петескивание окна.
        void _Collapse_xExit_DragNDropHeader()
        {
            _Collapse.Click += (s, a) => { mainWindow.WindowState = WindowState.Minimized; };
            xExit.Click += (s, a) => { mainWindow.Close(); };
            signInMLBD.MouseLeftButtonDown += (s, a) => { mainWindow.DragMove(); };
        }
        // Обработчик кнопок на странице SignIn.xaml.
        void signInBtnHandler()
        {
            // Обработчик нажатия по кнопке Войти.
            signInBtn.Click += (s, a) =>
            {
                if (signInUser.Text.Length > 0) // Проверяем введен ли логин.
                {
                    if (signInPass.Password.Length > 0)
                    { // Ищем в БД юзера с такими данными.
                        DataTable dt_user = db.Select("SELECT * FROM [Users] WHERE [Login] = '" + signInUser.Text + "' AND [Password] = '" + signInPass.Password + "'", db.getConn()) ;
                        if (dt_user.Rows.Count > 0) // Если такая запись существует.
                        {
                            // Выборка пользователя по Id.
                            int userid = Convert.ToInt32(dt_user.Rows[0][0].ToString());
                            Globals.SetGlobalUserId(userid);

                            // Если все нормально, открой Личный кабинет.
                            mainWindow.OpenPage(MainWindow.pages.Admin);
                            msgbox.succMsgBox(succmsg: "Авторизация прошла успешно!");
                        }
                        else msgbox.errMsgBox(errmsg: "Пользователь не найден!");
                    }
                    else msgbox.warnMsgBox(warnmsg: "Введите пароль!");
                }
                else msgbox.warnMsgBox(warnmsg: "Введите логин!");
            };
            // Отображение страницы SignUp.xaml по нажатию на кнопку Регистрация.
            signInBtnReg.Click += (s, a) => { mainWindow.OpenPage(MainWindow.pages.SignUp); };
            // batton.Click += (s, a) => { mainWindow.OpenPage(MainWindow.pages.PersonalArea); };
        }
    }
}
