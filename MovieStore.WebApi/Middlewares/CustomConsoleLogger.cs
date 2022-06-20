using MovieStore.WebApi.Interfaces;

namespace MovieStore.WebApi.Middlewares
{
    public class CustomConsoleLogger : ICustomLogger
    {
        public void Write(string message)
        {
            Console.WriteLine("[ Console logger ] - " + message);
        }
    }
}
