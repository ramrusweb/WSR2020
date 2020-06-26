using System.Data;
using System.Data.SqlClient;
using WSR2020.accounts.signup;
using WSR2020.db;

namespace WSR2020.handlers
{
    public class UserLoginCheck
    {
        public void uLC()
        {
            // Если юзер с таким логином существует, не регистрируем.
            //bool userLoginCheck()
            //{
            //    var msgbox = new MsgBox();
            //    var sqlcomm = new SqlCommand();
            //    var db = new DB();
            //    var signup = new SignUp();
            //    DataTable dt_user = db.Select("SELECT * FROM [Users] WHERE [Login] = @uL");
            //    sqlcomm.Parameters.Add("@uL", SqlDbType.NVarChar).Value = signup.signUpUser.Text;
            //    if (dt_user.Rows.Count > 0)
            //    {
            //        msgbox.errMsgBox(errmsg: "Пользователь с таким логином уже существует, введите другой!");
            //        return true;
            //    }
            //    else return false;
            //}
        }
    }
}
