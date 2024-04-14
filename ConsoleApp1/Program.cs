using Azure;
using Chonye.Domain.Models;
using Microsoft.Extensions.Configuration;

internal class Program
{
    private static void Main(string[] args)
    {
        

        Console.WriteLine("Por favor digite su nombre:");
        string? nombre = Console.ReadLine();
        createInvitees(nombre);

    }

    public static void createInvitees(string pName)
    {
        var invitee1 = new Invitee
        {            
            Name = "Alejandro",
            Company = "CT Telecomunicaciones",
            Industry = "Cleaning Supplies"
        };

        var invitee2 = new Invitee
        {
            Name = "Jane Smith",
            Company = "XYZ Corp.",
            Industry = "Finance"
        };

        var invitee3 = new Invitee
        {
            Name = "Alex Johnson",
            Company = "123 Industries",
            Industry = "Manufacturing"
        };

        // Display the generated data
        //DisplayInvitee(invitee1);
        //Console.WriteLine();
        //DisplayInvitee(invitee2);
        //Console.WriteLine();
        //DisplayInvitee(invitee3);
        if (pName.Equals(invitee1.Name, StringComparison.InvariantCultureIgnoreCase))
        {
            DisplayInvitee(invitee1);
        }
        else
        {
            DisplayInvitee(invitee2);
        }

    }

    static void DisplayInvitee(Invitee invitee)
    {
        Console.WriteLine($"Name: {invitee.Name}");
        Console.WriteLine($"Company: {invitee.Company}");
        Console.WriteLine($"Sector: {invitee.Industry}");
        Console.Read();
    }
}