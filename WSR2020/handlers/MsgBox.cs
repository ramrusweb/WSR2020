using System.Windows;

namespace WSR2020.handlers
{
    public class MsgBox
    {
        // MessageBox: Ошибка.
        public void errMsgBox(string errmsg)
        {
            MessageBox.Show($"{errmsg}", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        // MessageBox: Успешно.
        public void succMsgBox(string succmsg)
        {
            MessageBox.Show($"{succmsg}", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
        // MessageBox: Предупреждение.
        public void warnMsgBox(string warnmsg)
        {
            MessageBox.Show($"{warnmsg}", "Предупреждение!", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
