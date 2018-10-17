using System;

using example_dotnet_utils;

namespace example_dotnet
{
    class Program
    {
        static void Main(string[] args)
        {
            Say.hello("example");
            Console.WriteLine("Hello World!");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword("my-secret-password");
            BCrypt.Net.BCrypt.Verify("wrong-password", hashedPassword);
        }
    }
}
