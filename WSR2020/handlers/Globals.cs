namespace WSR2020.handlers
{
    public class Globals
    {
        // Получение или установка глобального id пользователя.
        public static int GlobalUserId { get; private set; }
        public static void SetGlobalUserId(int userId) { GlobalUserId = userId; }
    }
}
