using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Models;

namespace Controllers;

internal class DatabaseController
{
    public async Task ViewContacts()
    {
        using (var db = new PhonebookContext())
        {
            Console.WriteLine("Querying for all Contacts");
            var Contact = await db.Contacts
                .OrderBy(b => b.ContactId)
                .ToListAsync();

            foreach (var b in Contact)
            {
                var PhoneNumber = await db.PhoneNumbers
                    .SingleAsync(c => c.ContactId == b.ContactId);

                Console.WriteLine($"| {b.ContactId} | " + $"{b.FirstName} | " + $"{b.LastName} | " + $"{b.Email} | " + $"{PhoneNumber.TenDigitNumber} | " + $"{PhoneNumber.Location} | \n");

                /*
                Console.WriteLine($"{b.ContactId}");
                Console.WriteLine($"{b.FirstName}");
                Console.WriteLine($"{b.LastName}");
                Console.WriteLine($"{b.Email}");

                var PhoneNumber = await db.PhoneNumbers
                    .SingleAsync(c => c.ContactId == b.ContactId);

                Console.WriteLine($"{PhoneNumber.TenDigitNumber}");
                Console.WriteLine($"{PhoneNumber.Location}");
                */
            }
        }

    }

    public async Task AddContact(string firstname, string lastname, string email, long tendigitnumber, string location)
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

    public async Task UpdateContact(int contactid, string firstname = "", string lastname = "", string email = "", long tendigitnumber = 0, string location = "")
    {
        using (var db = new PhonebookContext())
        {
            var Contact = await db.Contacts
                .SingleOrDefaultAsync(b => b.ContactId == contactid);
            Console.WriteLine("Current contact information\n");
            //writes all contact info to console

            var PhoneNumber = await db.PhoneNumbers
                .SingleAsync(c => c.ContactId == Contact.ContactId);
            Console.WriteLine($"| {Contact.ContactId} | " + $"{Contact.FirstName} | " + $"{Contact.LastName} | " + $"{Contact.Email} | " + $"{PhoneNumber.TenDigitNumber} | " + $"{PhoneNumber.Location} | \n");

            Console.WriteLine("Updating the Contact with provided information");
            //Need to write what will be updated
            if (!string.IsNullOrWhiteSpace(lastname))
                Contact.LastName = lastname;

            if (!string.IsNullOrWhiteSpace(firstname))
                Contact.FirstName = firstname;

            if (!string.IsNullOrWhiteSpace(email))
                Contact.Email = email;

            //add only updated information
            if (PhoneNumber != null)
            {
                if (tendigitnumber != 0)
                    PhoneNumber.TenDigitNumber = tendigitnumber;

                if (!string.IsNullOrWhiteSpace(location))
                    PhoneNumber.Location = location;
            }
            else
            {
                Console.WriteLine("No phone number found for this contact. Cannot update.");
            }
            await db.SaveChangesAsync();

            Console.WriteLine("Updated Contact below:");
            Console.WriteLine($"| {Contact.ContactId} | " + $"{Contact.FirstName} | " + $"{Contact.LastName} | " + $"{Contact.Email} | " + $"{PhoneNumber.TenDigitNumber} | " + $"{PhoneNumber.Location} | \n");

        }
    }


    public async Task DeleteContact(int contactid, string firstname = "", string lastname = "", string email = "", long tendigitnumber = 0)
    {
        using (var db = new PhonebookContext())
        {
            //First filter the results by input, which is going to be a string this time.
            var Contact = await db.Contacts
                        .SingleAsync(c => c.ContactId == contactid);
            db.Remove(Contact);
            db.SaveChanges();
        }
        Console.WriteLine("Deleted the contact");
        //db.Remove();
        //await db.SaveChangesAsync();
    }


}