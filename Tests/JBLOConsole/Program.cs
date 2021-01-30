using System;
using System.Xml;
using RestSharp;

namespace JBLOConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string fullName = UserInfo.GenerateFullName();
            string email = UserInfo.GenerateEmail();
            string password = UserInfo.GeneratePassword();

            Console.WriteLine($"Full name: {fullName}");
            Console.WriteLine($"Email: {email}");
            Console.WriteLine($"Password: {password}");
            
        }
    }
}
