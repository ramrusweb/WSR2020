using Microsoft.Win32;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WSR2020.db;
using WSR2020.handlers;

namespace WSR2020.accounts.signup
{
    public partial class SignUp : Page
    {
        public MainWindow mainWindow;
        MsgBox msgbox = new MsgBox();
        DB db = new DB();
        SqlCommand sqlcomm = new SqlCommand();
        bool boolTrue() { return true; }
        bool boolFalse() { return false; }

        public SignUp(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            signUpBtnHandler();
            xExit_Collapse();
            downloadImg();
        }
        public SignUp() { }
        // Обработчики кнопок Свернуть и Закрыть; Перетаскивание окна.
        void xExit_Collapse()
        {
            _Collapse.Click += (s, a) => { mainWindow.WindowState = WindowState.Minimized; };
            xExit.Click += (s, a) => { mainWindow.Close(); };
            signUpMLBD.MouseLeftButtonDown += (s, a) => { mainWindow.DragMove(); };
        }

        // Обработчик кнопок Регистрация и Назад.
        void signUpBtnHandler()
        {
            signUpBtn.Click += (s, a) =>
            {
                string sUU = signUpUser.Text.Trim().ToLowerInvariant();
                string sUUNS = signUpUserNameSurname.Text.Trim().ToLowerInvariant();
                string sUP = signUpPass.Password.Trim().ToLowerInvariant();
                string sUPC = signUpPassConf.Password.Trim().ToLowerInvariant();
                string lfie = "Поле Логин не заполнено!";
                string pl = "Недопустимая длина в поле Логин (допустимое значение от 4 до 32 символов)";
                // Проверка полей на пустоту.
                if (sUU == "" && sUUNS == "" && sUP == "" && sUPC == "") // Все поля на пустоту.
                    msgbox.warnMsgBox(warnmsg: "Ни одно поле не заполнено!");

                if (sUU.Length < 1) // Логин или эл. адрес.
                    msgbox.warnMsgBox(warnmsg: $"{lfie}");

                if (sUUNS.Length < 1) // Имя и Фамилия.
                    msgbox.warnMsgBox(warnmsg: $"{lfie}".Replace("Логин", "Имя и Фамилия"));

                if (sUP.Length < 1) // Пароль.
                    msgbox.warnMsgBox(warnmsg: $"{lfie}".Replace("Логин", "Пароль"));

                if (sUPC.Length < 1) // Подтверждение пароля.
                    msgbox.warnMsgBox(warnmsg: "Повторите пароль!");

                // Проверка длины полей.
                if (sUU.Length < 4 || sUU.Length > 32) // Логин.
                    msgbox.warnMsgBox(warnmsg: $"{pl}");

                if (sUP.Length < 4 || sUP.Length > 32) // Пароль.
                    msgbox.warnMsgBox(warnmsg: $"{pl}".Replace("Логин", "Пароль"));

                if (sUUNS.Length < 4 || sUUNS.Length > 32) // Имя и Фамилия.
                    msgbox.warnMsgBox(warnmsg: $"{pl}".Replace("Логин", "Имя и Фамилия"));
                
                if (sUPC.Length < 4 || sUPC.Length > 32) // Подтверждение.
                    msgbox.warnMsgBox(warnmsg: $"{pl}".Replace("Логин", "Подтвердите пароль"));

                // Проверка на соответствие логина.
                string[] dataLogin = sUU.Split('@'); // Делим строку на две части.
                if (dataLogin.Length == 2) // Проверяем есть ли у нас две части.
                {
                    string[] data2Login = dataLogin[1].Split('.'); // Делим вторую часть еще на две части.
                    if (data2Login.Length == 2)
                    {

                    }
                    else msgbox.warnMsgBox(warnmsg: "Укажите логин в формате x@x.x");
                }
                else msgbox.warnMsgBox(warnmsg: "Укажите логин в формате x@x.x");

                // Проверка на соответствие пароля.
                if (sUP.Length <= 5)
                {
                    msgbox.errMsgBox(errmsg: "Пароль слишком короткий, минимум 6 символов!");

                    bool en = true; // Англ. раскладка.
                    bool sym = false; // Символ.
                    bool num = false; // Цифра.

                    for (int i = 0; i < sUP.Length; i++) // Перебираем символы.
                    {
                        if (sUP[i] >= 'А' && sUP[i] <= 'Я') en = false; // Если русская раскладка.
                        if (sUP[i] >= '0' && sUP[i] <= '9') num = true; // Если цифры.
                        if (sUP[i] == '_' || sUP[i] == '-' || sUP[i] == '!') sym = true; // Если символ.
                    }

                    if (!en)
                        msgbox.warnMsgBox(warnmsg: "Доступна только английская раскладка!");
                    else if (!sym)
                        msgbox.warnMsgBox(warnmsg: "Добавьте один из следующих символов: - _ !");
                    else if (!num)
                        msgbox.warnMsgBox(warnmsg: "Добавьте хотя бы одну цифру!");

                    // Проверка на совпадение паролей. 
                    if (sUP != sUPC)
                        msgbox.errMsgBox(errmsg: "Пароли не совпадат!");
                }

                // Если пользователь ввел логин.
                if (userLoginCheck())
                    return;
                // Если пользователь ввел данные, регистрируем.
                sqlcomm = new SqlCommand("INSERT INTO [Users] ([Login], [Password], [Name_Surname]) VALUES (@Login, @Password, @Name_Surname)", db.getConn());

                // Заглушки для входных данных.
                sqlcomm.Parameters.Add("@Login", SqlDbType.NVarChar).Value = sUU;
                sqlcomm.Parameters.Add("@Password", SqlDbType.NVarChar).Value = sUP;
                sqlcomm.Parameters.Add("@Name_Surname", SqlDbType.NVarChar).Value = sUUNS;
                // sqlcomm.Parameters.Add("@Picture", SqlDbType.Image).Value = ;

                db.openConn();


                if (sqlcomm.ExecuteNonQuery() == 1)
                {
                    db.closeConn();
                    mainWindow.OpenPage(MainWindow.pages.SignIn);
                    msgbox.succMsgBox(succmsg: "Регистрация прошла успешно! Авторизуйтесь!");
                    boolTrue();
                }
                else
                {
                    db.closeConn();
                    msgbox.errMsgBox(errmsg: "Не удалось создать аккаунт!");
                    boolFalse();
                }
            };
            // Возврат назад.
            signUpBtnCancel.Click += (s, a) => { mainWindow.OpenPage(MainWindow.pages.SignIn); };
        }
        // Обработка Загрузки изображения.
        string imgfilename = "";
        public void downloadImg()
        {
            downloadImage.Click += async (s, a) =>
            {
                var opf = new OpenFileDialog();
                opf.Filter = "Select Image*.jpg;*.png)|*.jpg;*.png";

                bool? result = opf.ShowDialog();
                if (result == true)
                {
                    await Task.Delay(2000);
                    imgfilename = opf.FileName;
                    signUpImg.Source = new BitmapImage(new Uri(imgfilename));
                    selImg.Content = "Изображение загружено";
                    downloadImage.Width = 170;
                    downloadImage.Margin = new Thickness(210, 313, 0, 0);
                    downloadImage.Content = "Выбрать другое..";
                }
            };


        }
        // Кодирование изображения в массив байтов.
        public void bitmapSourceToByteArray(ref byte[] imgBuffer)
        {

                    byte[] BitmapSorceToByteArray (BitmapSource image)
                    {
                        using (var ms = new MemoryStream())
                        {
                            var encoder = new PngBitmapEncoder();
                            encoder.Frames.Add(BitmapFrame.Create(image));
                            encoder.Save(ms);
                            return ms.ToArray();
                        }
                    }
                    byte[] imageBuffer = BitmapSorceToByteArray((BitmapSource)signUpImg.Source);
                    imageBuffer = imgBuffer;
        }
        // Метод проверки логина.
        bool userLoginCheck()
        {
            string suul = "";
            DataTable dt = db.Select("SELECT * FROM [Users] WHERE [Login] = '"+ suul +"'", db.getConn());
            sqlcomm.Parameters.Add(suul, SqlDbType.NVarChar).Value = signUpUser.Text;
            if (dt.Rows.Count > 0)
            {
                msgbox.errMsgBox(errmsg: "Пользователь с таким логином уже существует, введите другой!");
                return true;
            } else return false;
                    
        }
    }
}
