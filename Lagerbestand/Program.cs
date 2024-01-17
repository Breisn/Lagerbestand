using Lagerbestand;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

class Program
{
    static void Main()
    {
        using (var dbContext = new AppDbContext())
        {
            dbContext.Database.EnsureCreated();

            while (true)
            {
                Console.WriteLine("1. Produkt hinzufügen");
                Console.WriteLine("2. Produkt entfernen");
                Console.WriteLine("3. Lagerbestand anzeigen");
                Console.WriteLine("4. Beenden");

                Console.Write("Wählen Sie eine Option: ");
                var choice = Console.ReadLine();
                Console.WriteLine();


                switch (choice)
                {
                    case "1":
                        Console.Write("Produktname eingeben: ");
                        var name = Console.ReadLine();
                        Console.Write("Menge eingeben: ");
                        var quantity = int.Parse(Console.ReadLine());
                        Console.Write("Ort eingeben: ");
                        var location = Console.ReadLine();
                        Console.WriteLine();


                        var newProduct = new Product { Name = name, Quantity = quantity, Location = location };
                        dbContext.Products.Add(newProduct);
                        dbContext.SaveChanges();
                        break;

                    case "2":
                        Console.Write("Produkt-ID zum Entfernen eingeben: ");
                        var productIdToRemove = int.Parse(Console.ReadLine());
                        var productToRemove = dbContext.Products.Find(productIdToRemove);
                        if (productToRemove != null)
                        {
                            dbContext.Products.Remove(productToRemove);
                            dbContext.SaveChanges();
                        }
                        break;

                    case "3":
                        var products = dbContext.Products.ToList();
                        Console.WriteLine("Lagerbestand:");
                        foreach (var product in products)
                        {
                            Console.WriteLine($"{product.Id}. {product.Name} - Menge: {product.Quantity}, Ort: {product.Location}");
                            Console.WriteLine();

                        }
                        break;

                    case "4":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Ungültige Option. Bitte erneut versuchen.");
                        Console.WriteLine();
                        break;
                }
                Console.WriteLine();
            }
        }
    }
}
