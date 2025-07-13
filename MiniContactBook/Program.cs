// Import necessary system libraries
using System;
using System.Collections.Generic;
using System.IO;

// Define a class to store contact information (name and phone)
class Contact
{
    public string Name { get; set; }   // Property to store name
    public string Phone { get; set; }  // Property to store phone number

    // Constructor to initialize a contact with a name and phone number
    public Contact(string name, string phone)
    {
        Name = name;
        Phone = phone;
    }
}

class Program
{
    // List to hold all contacts in memory during the program session
    static List<Contact> contacts = new List<Contact>();

    // File path used to save/load contacts
    static string filePath = "contacts.txt";

    static void Main()
    {
        // Load existing contacts from the file when the program starts
        LoadContactsFromFile();

        bool running = true;

        // Main program loop
        while (running)
        {
            Console.Clear();  // Clear the console screen

            // Display the main menu
            Console.WriteLine("=== Mini Contact Book ===");
            Console.WriteLine("1. Add Contact");
            Console.WriteLine("2. View Contacts");
            Console.WriteLine("3. Edit Contact");
            Console.WriteLine("4. Delete Contact");
            Console.WriteLine("5. Load Contacts");
            Console.WriteLine("6. Exit");
            Console.Write("Select an option: ");

            // Read the user’s input and respond accordingly
            switch (Console.ReadLine())
            {
                case "1":
                    AddContact(); break;
                case "2":
                    ViewContacts(); break;
                case "3":
                    EditContact(); break;
                case "4":
                    DeleteContact(); break;
                case "5":
                    LoadContactsFromFile();
                    Console.WriteLine("Contacts loaded!");
                    Console.ReadKey(); break;
                case "6":
                    running = false; break;
                default:
                    Console.WriteLine("Invalid choice. Press any key to try again...");
                    Console.ReadKey(); break;
            }
        }
    }

    // Adds a new contact to the list
    static void AddContact()
    {
        Console.Write("Enter name: ");
        string name = Console.ReadLine()!; // Get name input from user

        Console.Write("Enter phone number: ");
        string phone = Console.ReadLine()!; // Get phone input from user

        // Add the new contact to the list
        contacts.Add(new Contact(name, phone));

        // Save the updated list to file
        SaveContactsToFile();

        Console.WriteLine("Contact added! Press any key to return to menu.");
        Console.ReadKey();
    }

    // Displays all contacts in the list
    static void ViewContacts()
    {
        Console.WriteLine("\n--- Contact List ---");

        if (contacts.Count == 0)
        {
            Console.WriteLine("No contacts found."); // If list is empty
        }
        else
        {
            for (int i = 0; i < contacts.Count; i++)
            {
                // Display contacts in numbered list
                Console.WriteLine($"{i + 1}. {contacts[i].Name} - {contacts[i].Phone}");
            }
        }

        Console.WriteLine("Press any key to return to menu.");
        Console.ReadKey();
    }

    // Allows editing of an existing contact
    static void EditContact()
    {
        ViewContacts(); // First show contacts to pick from
        Console.Write("\nEnter the number of the contact to edit: ");

        if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= contacts.Count)
        {
            Console.Write("Enter new name (leave blank to keep unchanged): ");
            string newName = Console.ReadLine()!;

            Console.Write("Enter new phone (leave blank to keep unchanged): ");
            string newPhone = Console.ReadLine()!;

            // Update the contact if the input isn't empty
            if (!string.IsNullOrWhiteSpace(newName))
                contacts[index - 1].Name = newName;

            if (!string.IsNullOrWhiteSpace(newPhone))
                contacts[index - 1].Phone = newPhone;

            SaveContactsToFile(); // Save after editing
            Console.WriteLine("Contact updated.");
        }
        else
        {
            Console.WriteLine("Invalid number.");
        }

        Console.WriteLine("Press any key to return to menu.");
        Console.ReadKey();
    }

    // Deletes a contact from the list
    static void DeleteContact()
    {
        ViewContacts(); // Show the list of contacts
        Console.Write("\nEnter the number of the contact to delete: ");

        if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= contacts.Count)
        {
            contacts.RemoveAt(index - 1); // Remove the contact by index
            SaveContactsToFile(); // Save after deleting
            Console.WriteLine("Contact deleted.");
        }
        else
        {
            Console.WriteLine("Invalid number.");
        }

        Console.WriteLine("Press any key to return to menu.");
        Console.ReadKey();
    }

    // Saves all contacts to a file (overwrites existing)
    static void SaveContactsToFile()
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var contact in contacts)
            {
                // Write each contact in "Name|Phone" format
                writer.WriteLine($"{contact.Name}|{contact.Phone}");
            }
        }
    }

    // Loads contacts from the file into the list
    static void LoadContactsFromFile()
    {
        contacts.Clear(); // Clear current list before loading

        if (File.Exists(filePath))
        {
            // Read each line of the file
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                // Each line is expected to be in "Name|Phone" format
                string[] parts = line.Split('|');
                if (parts.Length == 2)
                {
                    // Create and add contact to list
                    contacts.Add(new Contact(parts[0], parts[1]));
                }
            }
        }
    }
}
