using System.Data;
using System.Windows;
using System.Windows.Controls;
using WSR2020.db;
using WSR2020.handlers;
using System.Data.SqlClient;
using System.IO;
using WSR2020.accounts.signup;
using System.Windows.Media.Imaging;
using System;

namespace WSR2020.personalarea
{
    public partial class Admin : Page
    {
        public MainWindow mainWindow;
        MsgBox msgbox = new MsgBox();
        DB db = new DB();
        public Admin(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            _Collapse_xExit_DragNDropHeader();
            getImageAndUsername();
            welcomeLb.Content = "test";
        }
        public Admin() {}

        // Обработчики кнопок Свернуть и Закрыть; Петескивание окна.
        void _Collapse_xExit_DragNDropHeader()
        {
            _Collapse.Click += (s, a) => { mainWindow.WindowState = WindowState.Minimized; };
            xExit.Click += (s, a) => { mainWindow.Close(); };
            LK_MLBD.MouseLeftButtonDown += (s, a) => { mainWindow.DragMove(); };
        }

        // Получение изображения и имени пользователя.
        string Id;
        public void getImageAndUsername()
        {
            // DataTable dt_user = db.Select("SELECT * FROM [Users] WHERE [Id] = '"+ Id +"'", db.getConn());

            var sqlcomm = new SqlCommand("SELECT * FROM[Users] WHERE[Id] = '"+ Id +"'", db.getConn());
            sqlcomm.Parameters.Add(Id, SqlDbType.Int).Value = Globals.GlobalUserId;
            var adapter = new SqlDataAdapter(sqlcomm);
            var dt_user = new DataTable();

            if (dt_user.Rows.Count > 0)
            {
                //// Отображение изображения пользователя.
                //byte[] pic = (byte[])dt_user.Rows[0]["Picture"];

                //byte[] data;
                //var encoder = new JpegBitmapEncoder();
                //encoder.Frames.Add(BitmapFrame.Create(BitmapSource source));
                //using(var ms = new MemoryStream(pic))
                //{
                //    encoder.Save(ms);
                //    data = ms.ToArray();
                //}

                // Отображение имени пользователя.
                // welcomeLb.Content = "привет";
                // welcomeLb.Content = "Добро пожаловать (" + dt_user.Rows[0]["Login"]. + ")";
                welcomeLb.Content = "Добро пожаловать '" + dt_user.Rows[0]["Login"] + "'";
            }
                
            

        }
    }
}
