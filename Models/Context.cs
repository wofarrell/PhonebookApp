using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
  
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
   

using System.IO;

namespace Models;

public class PhonebookContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<PhoneNumber> PhoneNumbers { get; set; }

    public string DbPath { get; }

    public PhonebookContext()
    {
        // Define the path where the MDF file will be created
        //var basePath = Directory.GetParent(AppContext.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;
        //var dbFolder = Path.Combine(basePath, "DataBases");
        //var dbFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PhoneBookApp");

        /*
        // Ensure the folder exists
        if (!Directory.Exists(dbFolder))
        {
            Directory.CreateDirectory(dbFolder);
        }
        */

        // MDF file path
        //DbPath = Path.Combine(dbFolder, "phonebook.mdf");
        //var dbFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "phonebookapp.mdf");

        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        //var path = @"C:\Users\Sandwich\OneDrive\Coding\PhoneBookApp\DataBases";
        DbPath = "C:\\Databases\\phonebook.mdf";
        //System.IO.Path.Join(path, "phonebook.mdf");
    }


protected override void OnConfiguring(DbContextOptionsBuilder options)
{
    options.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;
                           Database=Phonebook;
                           Integrated Security=True;
                           MultipleActiveResultSets=True;");
}
        /*
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer($@"Data Source=(localdb)\.\SharedLocalDB;AttachDbFilename={DbPath};Integrated Security=True;MultipleActiveResultSets=True;");
    }
    */
}

