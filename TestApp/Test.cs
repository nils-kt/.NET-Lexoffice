#region Usings

using System;
using NET_Lexoffice;

#endregion

namespace TestApp
{
    internal class Test
    {
        private static void Main(string[] args)
        {
            Lexoffice lo = new Lexoffice("abcd");
            Console.WriteLine(lo.Contacts.GetAllContacts().Result);
        }
    }
}