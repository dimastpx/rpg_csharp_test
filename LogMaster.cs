namespace Test;
public class LogMaster
{
    const bool isDebug = true;
    public static void Log(string str)
    {
        if (isDebug)
        {
            Console.WriteLine($"Лог >> {str}");
        }
    }
}
