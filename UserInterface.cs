using Controllers;
using Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Threading.Tasks;


namespace PhoneBookApp;


internal class UserInterface
{
    DatabaseController databaseController = new();


    internal async Task MainMenu()
    {


        bool menuBool = true;
        while (menuBool)
        {

            bool validEntry = false;
            string menuchoice = "";
            //Console.Clear();
            Console.WriteLine("Phonebook Console App");
            Console.WriteLine("Please make a selection by number");

            Console.WriteLine("1. View Contacts \n2. Add a Contact \n3. Update a Contact \n4. Delete Contact \n'exit'");

            string? inputChoice = "";
            do
            {
                inputChoice = Console.ReadLine();
                {
                    if (inputChoice != null && (inputChoice == "1" || inputChoice == "2" || inputChoice == "3" || inputChoice == "4" || inputChoice == "exit"))
                    {
                        //if (inputChoice == "1" || inputChoice == "2" || inputChoice == "3" || inputChoice == "4")
                        menuchoice = inputChoice;
                        validEntry = true;
                    }
                    else
                    {
                        Console.WriteLine("\nplease enter a valid input");
                    }
                }
            } while (validEntry == false);

            switch (menuchoice)
            {
                case "1": //Manage flashcard stacks
                    await ViewContacts();
                    break;

                case "2": //Manage flashcards
                    await AddContact();
                    break;

                case "3":  //Start a study session                 
                    await UpdateContact();
                    break;

                case "4": //View study sessions
                    await DeleteContact();
                    break;

                case "exit": //exit
                    menuBool = false;
                    break;
            }
        }
    }


    internal async Task ViewContacts()
    {
        await databaseController.ViewContacts();
        Console.WriteLine("Press any key to return to the Main Menu.");
        //await Task.Delay(2000);
        Console.ReadLine();
    }

    internal async Task AddContact()
    {
        string firstname = "";
        string lastname = "";
        string email = "";
        long tendigitnumber = 0123456789;
        string location = "";

        Console.WriteLine("Adding a Contact Requires a First Name, Last Name, Email, and Phone Number. If at any point you want to quit this process, type 'exit'.");


        //Entering First name (can't be null or have any integers in the name)
        string? firstNameStr = "";
        bool firstNameSelectBool = false;
        do
        {
            Console.WriteLine("Enter a First Name");
            firstNameStr = Console.ReadLine();
            if (firstNameStr != null && firstNameStr.Any(char.IsDigit) == false)
            {
                firstname = firstNameStr;
                firstNameSelectBool = true;
            }
            if (firstNameStr == "exit")
            {
                Console.WriteLine("\nexiting to Main Menu");
                break;
            }
            if (firstNameStr.Any(char.IsDigit) == true)
            {
                Console.WriteLine("\nplease enter a valid First Name. No integers allowed in names.");
            }

        } while (firstNameSelectBool == false);

        Console.WriteLine($" First Name entered:{firstname}");

        //Entering last name (can't be null or have any integers in the name)
        string? lastNameStr = "";
        bool lastNameSelectBool = false;
        do
        {
            Console.WriteLine("Enter a Last Name");
            lastNameStr = Console.ReadLine();
            if (lastNameStr != null && lastNameStr.Any(char.IsDigit) == false)
            {
                lastname = lastNameStr;
                lastNameSelectBool = true;
            }
            if (lastNameStr == "exit")
            {
                Console.WriteLine("\nexiting to Main Menu");
                break;
            }
            if (lastNameStr.Any(char.IsDigit) == true)
            {
                Console.WriteLine("\nplease enter a valid First Name. No integers allowed in names.");
            }

        } while (lastNameSelectBool == false);

        Console.WriteLine($" Last Name entered:{lastname}");

        string? emailStr = "";
        bool emailSelectBool = false;
        do
        {
            Console.WriteLine("Enter an Email in format 'email@domain.com'");
            emailStr = Console.ReadLine();
            if (emailStr != null
                && emailStr.Any(char.IsDigit) == false
                && emailStr.Contains("@")
                && emailStr.IndexOf("@") != 0
                && emailStr.Contains(".com")
                && emailStr.IndexOf(".") > emailStr.IndexOf("@") + 1
                )
            {
                email = emailStr;
                emailSelectBool = true;
            }

            if (!emailStr.Contains("@") || emailStr.IndexOf("@") == 0 || !emailStr.Contains(".com") || emailStr.IndexOf(".") < emailStr.IndexOf("@"))
            {
                Console.WriteLine("\nplease enter a valid email address with format 'email@domain.com'");
            }

            if (emailStr == "exit")
            {
                Console.WriteLine("\nexiting to Main Menu");
                break;
            }


        } while (emailSelectBool == false);

        string? phoneStr = "";
        bool phoneSelectBool = false;
        do
        {
            Console.WriteLine("Enter a ten digit phone number as one integer");
            phoneStr = Console.ReadLine();
            bool VariableEntry = long.TryParse(phoneStr, out long phoneInt);
            if (phoneStr != null && phoneStr.Length == 10 && VariableEntry == true && phoneStr.IndexOf("0") != 0)
            {
                if (phoneInt != 0)
                {
                    tendigitnumber = phoneInt;
                    phoneSelectBool = true;
                }
                else
                {
                    Console.WriteLine("\nplease enter a valid ten digit phone number");
                }
            }
            if (phoneStr == null || phoneStr.Length != 10 || VariableEntry != true && phoneStr.IndexOf("0") == 0)
            {
                Console.WriteLine("\nplease enter a valid ten digit phone number");
            }
            if (phoneStr == "exit")
            {
                Console.WriteLine("\nexiting to Main Menu");
                break;
            }

        } while (phoneSelectBool == false);


        string? locationStr = "";
        bool locationSelectBool = false;
        do
        {
            Console.WriteLine("Enter a phone location");
            locationStr = Console.ReadLine();
            if (locationStr != null && locationStr.Any(char.IsDigit) == false)
            {
                location = locationStr;
                locationSelectBool = true;
            }
            if (locationStr == "exit")
            {
                Console.WriteLine("\nexiting to Main Menu");
                break;
            }
            if (locationStr.Any(char.IsDigit) == true)
            {
                Console.WriteLine("\nplease enter a valid phone location. No integers allowed in names.");
            }

        } while (locationSelectBool == false);

        /*
        Manual Entry testing

        string firstname = "Tom";
        string lastname = "Bombadil";
        string email = "tommy@gmail.com";
        long tendigitnumber = 0123456789;
        string location = "Home";
         */

        await databaseController.AddContact(firstname, lastname, email, tendigitnumber, location);

    }

    internal async Task UpdateContact()
    {
        bool exitBool = false;

        await databaseController.ViewContacts();

        int contactid = 0;
        string firstname = "";
        string lastname = "";
        string email = "";
        long tendigitnumber = 0;
        string location = "";

        while (exitBool == false)
        {
        //get contact ID - need to rewrite to call the db to ensure it exists
        Console.WriteLine("\nChoose a contact by number from the list above to update, or type 'exit'");
        string? contactStr = "";
        bool contactSelectBool = false;
        do
        {
            contactStr = Console.ReadLine();
            bool VariableEntry = int.TryParse(contactStr, out int contactInt);
            if (contactStr != null && VariableEntry == true && contactStr.IndexOf("0") != 0)
            {
                if (contactInt != 0)
                {
                    contactid = contactInt;
                    contactSelectBool = true;
                }
                else
                {
                    Console.WriteLine("\nplease enter a valid cotnactid");
                }
            }
            if (contactStr == null || VariableEntry != true && contactStr.IndexOf("0") == 0)
            {
                Console.WriteLine("\nplease enter a valid contactid");
            }
            if (contactStr == "exit")
            {
                Console.WriteLine("\nexiting to Main Menu");
                exitBool = true;
                break;
            }

        } while (contactSelectBool == false);

        Console.WriteLine("Choose the number of the field of the contact that you want to update below, and choose 'Update' to update the contact:");


        bool updateMenuBool = false;
        while (updateMenuBool == false)
        {
            Console.WriteLine("1. First Name \n2. Last Name \n3. Email \n4. Phone Number \n5. Location \n6. Run Update Command \n'exit'");
            bool validEntry = false;
            string menuchoice = "";
            string? inputChoice = "";
            do
            {
                inputChoice = Console.ReadLine();
                {
                    if (inputChoice != null && (inputChoice == "1" || inputChoice == "2" || inputChoice == "3" || inputChoice == "4" || inputChoice == "5" || inputChoice == "6" || inputChoice == "exit"))
                    {
                        menuchoice = inputChoice;
                        validEntry = true;
                    }
                    else
                    {
                        Console.WriteLine("\nplease enter a valid input");
                    }
                }
            } while (validEntry == false);


            switch (menuchoice)
            {

                case "1": //Get value to update first name to 
                    string? firstNameStr = "";
                    bool firstNameSelectBool = false;
                    do
                    {
                        Console.WriteLine("Enter a First Name");
                        firstNameStr = Console.ReadLine();
                        if (firstNameStr != null && firstNameStr.Any(char.IsDigit) == false)
                        {
                            firstname = firstNameStr;
                            firstNameSelectBool = true;
                        }
                        if (firstNameStr == "exit")
                        {
                            Console.WriteLine("\nexiting to Main Menu");
                            break;
                        }
                        if (firstNameStr.Any(char.IsDigit) == true)
                        {
                            Console.WriteLine("\nplease enter a valid First Name. No integers allowed in names.");
                        }

                    } while (firstNameSelectBool == false);

                    Console.WriteLine($" First Name entered:{firstname}");
                    break;

                case "2": //Get value to update last name to 
                          //Entering last name (can't be null or have any integers in the name)
                    string? lastNameStr = "";
                    bool lastNameSelectBool = false;
                    do
                    {
                        Console.WriteLine("Enter a Last Name");
                        lastNameStr = Console.ReadLine();
                        if (lastNameStr != null && lastNameStr.Any(char.IsDigit) == false)
                        {
                            lastname = lastNameStr;
                            lastNameSelectBool = true;
                        }
                        if (lastNameStr == "exit")
                        {
                            Console.WriteLine("\nexiting to Main Menu");
                            break;
                        }
                        if (lastNameStr.Any(char.IsDigit) == true)
                        {
                            Console.WriteLine("\nplease enter a valid First Name. No integers allowed in names.");
                        }

                    } while (lastNameSelectBool == false);

                    Console.WriteLine($" Last Name entered:{lastname}");
                    break;

                case "3":  //Get value to update email to                 
                    string? emailStr = "";
                    bool emailSelectBool = false;
                    do
                    {
                        Console.WriteLine("Enter an Email in format 'email@domain.com'");
                        emailStr = Console.ReadLine();
                        if (emailStr != null
                            && emailStr.Any(char.IsDigit) == false
                            && emailStr.Contains("@")
                            && emailStr.IndexOf("@") != 0
                            && emailStr.Contains(".com")
                            && emailStr.IndexOf(".") > emailStr.IndexOf("@") + 1
                            )
                        {
                            email = emailStr;
                            emailSelectBool = true;
                        }

                        if (!emailStr.Contains("@") || emailStr.IndexOf("@") == 0 || !emailStr.Contains(".com") || emailStr.IndexOf(".") < emailStr.IndexOf("@"))
                        {
                            Console.WriteLine("\nplease enter a valid email address with format 'email@domain.com'");
                        }

                        if (emailStr == "exit")
                        {
                            Console.WriteLine("\nexiting to Main Menu");
                            break;
                        }

                    } while (emailSelectBool == false);
                    break;

                case "4": //Get value to update phone number to 
                    string? phoneStr = "";
                    bool phoneSelectBool = false;
                    do
                    {
                        Console.WriteLine("Enter a ten digit phone number as one integer");
                        phoneStr = Console.ReadLine();
                        bool VariableEntry = long.TryParse(phoneStr, out long phoneInt);
                        if (phoneStr != null && phoneStr.Length == 10 && VariableEntry == true && phoneStr.IndexOf("0") != 0)
                        {
                            if (phoneInt != 0)
                            {
                                tendigitnumber = phoneInt;
                                phoneSelectBool = true;
                            }
                            else
                            {
                                Console.WriteLine("\nplease enter a valid ten digit phone number");
                            }
                        }
                        if (phoneStr == null || phoneStr.Length != 10 || VariableEntry != true && phoneStr.IndexOf("0") == 0)
                        {
                            Console.WriteLine("\nplease enter a valid ten digit phone number");
                        }
                        if (phoneStr == "exit")
                        {
                            Console.WriteLine("\nexiting to Main Menu");
                            break;
                        }

                    } while (phoneSelectBool == false);
                    break;

                case "5": //get value to update location
                    string? locationStr = "";
                    bool locationSelectBool = false;
                    do
                    {
                        Console.WriteLine("Enter a phone location");
                        locationStr = Console.ReadLine();
                        if (locationStr != null && locationStr.Any(char.IsDigit) == false)
                        {
                            location = locationStr;
                            locationSelectBool = true;
                        }
                        if (locationStr == "exit")
                        {
                            Console.WriteLine("\nexiting to Main Menu");
                            break;
                        }
                        if (locationStr.Any(char.IsDigit) == true)
                        {
                            Console.WriteLine("\nplease enter a valid phone location. No integers allowed in names.");
                        }

                    } while (locationSelectBool == false);
                    break;

                case "6": //exit loop to update the contact
                    updateMenuBool = true;
                    break;

                case "exit": //exit
                    updateMenuBool = true;
                    exitBool = true;
                    break;
            }
        }

        //PARAMS (contactId, string firstname = "", string lastname = "", string email = "", long tendigitnumber = 0)
        await databaseController.UpdateContact(contactid, firstname, lastname, email, tendigitnumber, location);
        exitBool = true;
        Console.WriteLine("Press any key to return to the Main Menu.");
        Console.ReadLine();
        }
    }

    internal async Task DeleteContact()
    {
        await databaseController.ViewContacts();
        //PARAMS (string firstname = "", string lastname = "", string email = "", long tendigitnumber = 0)
        
        
        bool exitBool = false;
        int contactid = 0;

        while (exitBool == false)
        {
        
        string? contactStr = "";
        bool contactSelectBool = false;
        do
        {
            Console.WriteLine("\nChoose a contact by number from the list above to update, or type 'exit'");
            contactStr = Console.ReadLine();
            bool VariableEntry = int.TryParse(contactStr, out int contactInt);
            if (contactStr != null && VariableEntry == true && contactStr.IndexOf("0") != 0)
            {
                if (contactInt != 0)
                {
                    contactid = contactInt;
                    contactSelectBool = true;
                    exitBool = true;
                }
                else
                {
                    Console.WriteLine("\nplease enter a valid cotnactid");
                }
            }
            if (contactStr == null || VariableEntry != true && contactStr.IndexOf("0") == 0)
            {
                Console.WriteLine("\nplease enter a valid contactid");
            }
            if (contactStr == "exit")
            {
                Console.WriteLine("\nexiting to Main Menu");
                exitBool = true;
                break;
            }

        } while (contactSelectBool == false);
        }
        await databaseController.DeleteContact(contactid);
    }

}