namespace GrosvenorPracticum
{
    public class Console : IConsole
    {
        public string ReadLine()
        {
            return System.Console.ReadLine();
        }

        public void WriteLine(string output)
        {
            System.Console.WriteLine(output);
        }
    }
}