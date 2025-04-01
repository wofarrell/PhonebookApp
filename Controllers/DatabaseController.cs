using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Models;

namespace Controllers;

internal class DatabaseController
{

    public async void ViewContacts()
    {

        using (var db = new PhonebookContext())
        {

            Console.WriteLine("Querying for a Contact");
            var Contact = await db.Contacts
                .OrderBy(b => b.ContactId)
                .ToListAsync();

            foreach (var b in Contact)
            {
                Console.WriteLine($"{b.ContactId}");
                Console.WriteLine($"{b.FirstName}");
                Console.WriteLine($"{b.LastName}");
                Console.WriteLine($"{b.Email}");
                
                var PhoneNumber = await db.PhoneNumbers
                    .SingleAsync(c => c.ContactId == b.ContactId);

                Console.WriteLine($"{PhoneNumber.TenDigitNumber}");
                Console.WriteLine($"{PhoneNumber.Location}");
           
            }



        }
    }

    public async void AddContact(string firstname, string lastname, string email, int tendigitnumber, string location)
    {
        //This takes in the information that will be popualted in table dbo.Contacts and table dbo.PhoneNumbers
        using (var db = new PhonebookContext())
        {
            //This inserts the first portion of the info, and will automatically create the ContactId we need for the PhoneNumber db insert
            Console.WriteLine("Inserting a new Contact");
            db.Add(new Contact
            {
                FirstName = firstname,
                LastName = lastname,
                Email = email,
            });

            await db.SaveChangesAsync();

            //Pulls the single entity created above (using email for both) so that we can use the ContactId to connect the tables
            var ContactEntity = await db.Contacts
                .SingleAsync(b => b.Email == email);

            int contactId = ContactEntity.ContactId;

            //Inserts the phonenumber information into the PhoneNumber DB
            db.Add(new PhoneNumber
            {
                TenDigitNumber = tendigitnumber,
                Location = location,
                ContactId = contactId
            }
            );
            await db.SaveChangesAsync();

        }
    }

    public async void UpdateContact()
    {
        using var db = new PhonebookContext();
        var Contact = await db.Contacts
            .OrderBy(b => b.ContactId)
            .FirstAsync();
        Console.WriteLine("Updating the Contact and adding a Phone Number");
        Contact.LastName = "UpdatedLastName";
        Contact.PhoneNumbers.Add(
            new PhoneNumber { TenDigitNumber = 0123456789, Location = "Home" });
        await db.SaveChangesAsync();

    }

    public void DeleteContact()
    {
        using var db = new PhonebookContext();

        Console.WriteLine("Delete the blog");
        //db.Remove();
        //await db.SaveChangesAsync();
    }


}