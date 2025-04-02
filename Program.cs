using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Models;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
using Microsoft.Identity.Client;
using PhoneBookApp;

class Program
{
    private static async Task Main(string[] args)
    {

            
        UserInterface userInterface = new();
        userInterface.MainMenu();
        
        Console.WriteLine("Commands finished, check DB");
    }
}