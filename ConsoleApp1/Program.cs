using System;
using System.Collections.Generic;

public class Medicine
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal PricePerUnit { get; set; }
    public DateTime ExpiryDate { get; set; }

    public Medicine(string name, int quantity, decimal pricePerUnit, DateTime expiryDate)
    {
        Name = name;
        Quantity = quantity;
        PricePerUnit = pricePerUnit;
        ExpiryDate = expiryDate;
    }
}

public class MedicineInventory
{
    private Dictionary<string, LinkedListNode<Medicine>> inventory;

    public MedicineInventory()
    {
        inventory = new Dictionary<string, LinkedListNode<Medicine>>();
    }

    public void AddMedicine(string name, int quantity, decimal pricePerUnit, DateTime expiryDate)
    {
        if (inventory.ContainsKey(name))
        {
            LinkedListNode<Medicine> node = inventory[name];
            node.Value.Quantity += quantity;
        }
        else
        {
            Medicine newMedicine = new Medicine(name, quantity, pricePerUnit, expiryDate);
            LinkedListNode<Medicine> newNode = new LinkedListNode<Medicine>(newMedicine);
            inventory.Add(name, newNode);
        }
        Console.WriteLine("Medicine added!");
    }

    public void RemoveMedicine(string name)
    {
        if (inventory.ContainsKey(name))
        {
            inventory.Remove(name);
            Console.WriteLine("Medicine removed!");
        }
        else
        {
            Console.WriteLine("Medicine not found!");
        }
    }

    public void PrintAllMedicines()
    {
        Console.WriteLine("Medicine inventory:");
        foreach (var kvp in inventory)
        {
            Medicine medicine = kvp.Value.Value;
            Console.WriteLine($"Name: {medicine.Name}, Quantity: {medicine.Quantity}, Price per unit: ${medicine.PricePerUnit}, Expiry Date: {medicine.ExpiryDate.ToShortDateString()}");
        }
    }

    public void FindMedicineByName(string name)
    {
        Console.WriteLine($"Searching for medicine by name: {name}");
        if (inventory.ContainsKey(name))
        {
            Medicine medicine = inventory[name].Value;
            Console.WriteLine($"Name: {medicine.Name}, Quantity: {medicine.Quantity}, Price per unit: ${medicine.PricePerUnit}, Expiry Date: {medicine.ExpiryDate.ToShortDateString()}");
        }
        else
        {
            Console.WriteLine("Medicine not found!");
        }
    }

    public void UpdateMedicineQuantity(string name, int newQuantity)
    {
        if (inventory.ContainsKey(name))
        {
            inventory[name].Value.Quantity = newQuantity;
            Console.WriteLine("Medicine quantity updated!");
        }
        else
        {
            Console.WriteLine("Medicine not found!");
        }
    }

    public void ViewExpiringMedicines(DateTime expiryThreshold)
    {
        Console.WriteLine($"Medicines expiring before {expiryThreshold.ToShortDateString()}:");
        foreach (var kvp in inventory)
        {
            Medicine medicine = kvp.Value.Value;
            if (medicine.ExpiryDate < expiryThreshold)
            {
                Console.WriteLine($"Name: {medicine.Name}, Expiry Date: {medicine.ExpiryDate.ToShortDateString()}");
            }
        }
    }

    public void CalculateTotalCost()
    {
        decimal totalCost = 0;
        foreach (var kvp in inventory)
        {
            Medicine medicine = kvp.Value.Value;
            totalCost += medicine.Quantity * medicine.PricePerUnit;
        }
        Console.WriteLine($"Total cost of all medicines: ${totalCost}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        MedicineInventory inventory = new MedicineInventory();

        Console.WriteLine("Welcome to the medicine management application!");

        while (true)
        {
            Console.WriteLine("\n1. Add Medicine");
            Console.WriteLine("2. Remove Medicine");
            Console.WriteLine("3. Print All Medicines");
            Console.WriteLine("4. Find Medicine by Name");
            Console.WriteLine("5. Update Medicine Quantity");
            Console.WriteLine("6. View Expiring Medicines by Date");
            Console.WriteLine("7. Calculate Total Cost of Medicines");
            Console.WriteLine("8. Exit");

            Console.Write("\nSelect an option: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("\nEnter medicine name: ");
                    string name = Console.ReadLine();
                    Console.Write("Quantity: ");
                    int quantity = int.Parse(Console.ReadLine());
                    Console.Write("Price per unit: ");
                    decimal pricePerUnit = decimal.Parse(Console.ReadLine());
                    Console.Write("Expiry Date (month/day/year): ");
                    DateTime expiryDate = DateTime.Parse(Console.ReadLine());
                    inventory.AddMedicine(name, quantity, pricePerUnit, expiryDate);
                    break;
                case 2:
                    Console.Write("\nEnter the medicine to remove: ");
                    string nameToRemove = Console.ReadLine();
                    inventory.RemoveMedicine(nameToRemove);
                    break;
                case 3:
                    inventory.PrintAllMedicines();
                    break;
                case 4:
                    Console.Write("\nEnter medicine name to find: ");
                    string nameToFind = Console.ReadLine();
                    inventory.FindMedicineByName(nameToFind);
                    break;
                case 5:
                    Console.Write("\nEnter medicine name to update quantity: ");
                    string nameToUpdate = Console.ReadLine();
                    Console.Write("Enter new quantity: ");
                    int newQuantity = int.Parse(Console.ReadLine());
                    inventory.UpdateMedicineQuantity(nameToUpdate, newQuantity);
                    break;
                case 6:
                    Console.Write("\nEnter expiry date threshold (month/day/year): ");
                    DateTime expiryThreshold = DateTime.Parse(Console.ReadLine());
                    inventory.ViewExpiringMedicines(expiryThreshold);
                    break;
                case 7:
                    inventory.CalculateTotalCost();
                    break;
                case 8:
                    Console.WriteLine("\nThank you for using the application!");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("\nInvalid option! Please select another one!");
                    break;
            }
        }
    }
}
