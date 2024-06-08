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

public class LinkedListNode<Item>
{
    public Item Value { get; set; }
    public LinkedListNode<Item> Next { get; set; }

    public LinkedListNode(Item value)
    {
        Value = value;
        Next = null;
    }
}

public class LinkedList<Item>
{
    public LinkedListNode<Item> Head { get; private set; }
    public LinkedListNode<Item> Tail { get; private set; }

    public void AddLast(Item value)
    {
        LinkedListNode<Item> newNode = new LinkedListNode<Item>(value);
        if (Head == null)
        {
            Head = newNode;
            Tail = newNode;
        }
        else
        {
            Tail.Next = newNode;
            Tail = newNode;
        }
    }

    public bool Remove(Item value)
    {
        if (Head == null) return false;

        if (Head.Value.Equals(value))
        {
            Head = Head.Next;
            if (Head == null)
                Tail = null;
            return true;
        }

        LinkedListNode<Item> current = Head;
        while (current.Next != null && !current.Next.Value.Equals(value))
        {
            current = current.Next;
        }

        if (current.Next == null) return false;

        current.Next = current.Next.Next;
        if (current.Next == null)
            Tail = current;
        return true;
    }

    public LinkedListNode<Item> Find(Item value)
    {
        LinkedListNode<Item> current = Head;
        while (current != null)
        {
            if (current.Value.Equals(value))
                return current;
            current = current.Next;
        }
        return null;
    }

    public void ForEach(Action<Item> action)
    {
        LinkedListNode<Item> current = Head;
        while (current != null)
        {
            action(current.Value);
            current = current.Next;
        }
    }
}

public class MedicineInventory
{
    private Dictionary<string, LinkedListNode<Medicine>> inventory;
    private LinkedList<Medicine> medicineList;

    public MedicineInventory()
    {
        inventory = new Dictionary<string, LinkedListNode<Medicine>>();
        medicineList = new LinkedList<Medicine>();
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
            medicineList.AddLast(newMedicine);
        }
        Console.WriteLine("Медикамент добавлен!");
    }

    public void RemoveMedicine(string name)
    {
        if (inventory.ContainsKey(name))
        {
            LinkedListNode<Medicine> node = inventory[name];
            inventory.Remove(name);
            medicineList.Remove(node.Value);
            Console.WriteLine("Медикамент удален!");
        }
        else
        {
            Console.WriteLine("Медикамент не найден!");
        }
    }

    public void PrintAllMedicines()
    {
        Console.WriteLine("Хранилище медикаментов:");
        medicineList.ForEach(medicine =>
        {
            Console.WriteLine($"Название: {medicine.Name}, Количество: {medicine.Quantity}, Стоимость за штуку: ${medicine.PricePerUnit}, Дата порчи: {medicine.ExpiryDate.ToShortDateString()}");
        });
    }

    public void FindMedicineByName(string name)
    {
        Console.WriteLine($"Поиск медикамента по имени: {name}");
        if (inventory.ContainsKey(name))
        {
            Medicine medicine = inventory[name].Value;
            Console.WriteLine($"Название: {medicine.Name}, Количество: {medicine.Quantity}, Стоимость за штуку: ${medicine.PricePerUnit}, Дата порчи: {medicine.ExpiryDate.ToShortDateString()}");
        }
        else
        {
            Console.WriteLine("Медикамент не найден!");
        }
    }

    public void UpdateMedicineQuantity(string name, int newQuantity)
    {
        if (inventory.ContainsKey(name))
        {
            inventory[name].Value.Quantity = newQuantity;
            Console.WriteLine("Количество медикаментов обновлено!");
        }
        else
        {
            Console.WriteLine("Медикамент не найден!");
        }
    }

    public void ViewExpiringMedicines(DateTime expiryThreshold)
    {
        Console.WriteLine($"Медикаменты испортившиеся до {expiryThreshold.ToShortDateString()}:");
        medicineList.ForEach(medicine =>
        {
            if (medicine.ExpiryDate < expiryThreshold)
            {
                Console.WriteLine($"Название: {medicine.Name}, Дата порчи: {medicine.ExpiryDate.ToShortDateString()}");
            }
        });
    }

    public void CalculateTotalCost()
    {
        decimal totalCost = 0;
        medicineList.ForEach(medicine =>
        {
            totalCost += medicine.Quantity * medicine.PricePerUnit;
        });
        Console.WriteLine($"Стоимость всех медикаментов: ${totalCost}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        MedicineInventory inventory = new MedicineInventory();
        Console.WriteLine("Добро пожаловать в приложение по управлению медикаментами!");
        while (true)
        {
            Console.WriteLine("\n1. Добавить медикамент");
            Console.WriteLine("2. Убрать медикамент");
            Console.WriteLine("3. Показать все медикаменты");
            Console.WriteLine("4. Поиск медикамента по названию");
            Console.WriteLine("5. Обновить количество медикамента");
            Console.WriteLine("6. Посмотреть испорченные медикаменты по дате");
            Console.WriteLine("7. Посчитать общую стоимость медикаментов");
            Console.WriteLine("8. Выход");
            Console.Write("\nВыберите опцию: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("\nНазвание медикамента: ");
                    string name = Console.ReadLine();
                    Console.Write("Количество: ");
                    int quantity = int.Parse(Console.ReadLine());
                    Console.Write("Стоимость за штуку: ");
                    decimal pricePerUnit = decimal.Parse(Console.ReadLine());
                    Console.Write("Дата порчи (месяц/день/год): ");
                    DateTime expiryDate = DateTime.Parse(Console.ReadLine());
                    inventory.AddMedicine(name, quantity, pricePerUnit, expiryDate);
                    break;
                case 2:
                    Console.Write("\nМедикамент, который вы хотите удалить: ");
                    string nameToRemove = Console.ReadLine();
                    inventory.RemoveMedicine(nameToRemove);
                    break;
                case 3:
                    inventory.PrintAllMedicines();
                    break;
                case 4:
                    Console.Write("\nПоиск медикамента по названию: ");
                    string nameToFind = Console.ReadLine();
                    inventory.FindMedicineByName(nameToFind);
                    break;
                case 5:
                    Console.Write("\nНазвание медикамента для изменения количества: ");
                    string nameToUpdate = Console.ReadLine();
                    Console.Write("Введите новое количество: ");
                    int newQuantity = int.Parse(Console.ReadLine());
                    inventory.UpdateMedicineQuantity(nameToUpdate, newQuantity);
                    break;
                case 6:
                    Console.Write("\nВведите дату истечения (месяц/день/год): ");
                    DateTime expiryThreshold = DateTime.Parse(Console.ReadLine());
                    inventory.ViewExpiringMedicines(expiryThreshold);
                    break;
                case 7:
                    inventory.CalculateTotalCost();
                    break;
                case 8:
                    Console.WriteLine("\nСпасибо за использование приложения!");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("\nНеправильная опция! Выберите другую!");
                    break;
            }
        }
    }
}
