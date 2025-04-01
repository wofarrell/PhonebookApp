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

        /*
        using var db = new PhonebookContext();

        // Note: This sample requires the database to be created before running.
        Console.WriteLine($"Database path: {db.DbPath}.");

        // Create
        Console.WriteLine("Inserting a new Contact");
        db.Add(new Contact
        {
            FirstName = "Name2",
            LastName = "Name2",
            Email = "Name1Name2@gmail.com"
        });
        await db.SaveChangesAsync();

        // Read
        Console.WriteLine("Querying for a Contact");
        var Contact = await db.Contacts
            .OrderBy(b => b.ContactId)
            .FirstAsync();

        // Update
        Console.WriteLine("Updating the Contact and adding a Phone Number");
        Contact.LastName = "UpdatedLastName";
        Contact.PhoneNumbers.Add(
            new PhoneNumber { TenDigitNumber = 0123456789, Location = "Home" });
        await db.SaveChangesAsync();

        // Delete
        /*
        Console.WriteLine("Delete the blog");
        db.Remove();
        await db.SaveChangesAsync();

        */
        Console.WriteLine("Commands finished, check DB");
    }
}