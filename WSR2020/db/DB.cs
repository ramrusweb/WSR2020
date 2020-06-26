using System.Data;
using System.Data.SqlClient;

namespace WSR2020.db
{
    public class DB
    {
        // Строка подключения.
        static string connStr = @"Data Source=DESKTOP-00BHIKN\SQLEXPRESS;Initial Catalog=WSR2020;Integrated Security=True";
        public SqlConnection sqlconn = new SqlConnection(connStr);

        // Метод открытия подключения.
        public void openConn()
        {
            if (sqlconn.State == ConnectionState.Closed)
                sqlconn.Open();
        }
        // Метод закрытия подключения.
        public void closeConn()
        {
            if (sqlconn.State == ConnectionState.Open)
                sqlconn.Close();
        }
        // Метод возврата соединения.
        public SqlConnection getConn() { return sqlconn; }

        // Функция подключения к БД и обработка запросов.
        public DataTable Select(string selectSQL,  object GetConn)
        {
            // Создание таблицы в приложении.
            var dt = new DataTable("Users");
            // Подключение к БД.
            sqlconn = new SqlConnection(connStr);
            // Открытие БД.
            sqlconn.Open();
            // Создание команды. 
            var sqlcomm = sqlconn.CreateCommand();
            // Присвоение текста команде.
            sqlcomm.CommandText = selectSQL;
            GetConn = getConn();
            // Создание обработчика.
            var sda = new SqlDataAdapter();
            sda.SelectCommand = sqlcomm;
            // Возврат таблицы с результатом.
            sda.Fill(dt);
            sqlconn.Close();
            return dt;
        }
    }

}
