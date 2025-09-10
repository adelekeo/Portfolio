using Microsoft.EntityFrameworkCore; // To use Include method.
//using Northwind.EntityModels;
using WorkingWithEFCore.AutoGen; // To use Northwind, Category, Product.
partial class Program
{
    private static void QueryingCategories()
    {
        using NorthwindDb db = new();
        SectionTitle("Categories and how many products they have");
        // A query to get all categories and their related products.
        // This is a query definition. Nothing has executed against the database.
        IQueryable<Category>? categories = db.Categories?
          .Include(c => c.Products);
        // You could call any of the following LINQ methods and nothing will be executed against the database:
        // Where, GroupBy, Select, SelectMany, OfType, OrderBy, ThenBy, Join, GroupJoin, Take, Skip, Reverse.
        // Usually, methods that return IEnumerable or IQueryable support deferred execution.
        // Usually, methods that return a single value do not support deferred execution.
        if (categories is null || !categories.Any())
        {
            Fail("No categories found.");
            return;
        }
        // Enumerating the query converts it to SQL and executes it against the database.
        // Execute query and enumerate results.
        foreach (Category c in categories)
        {
            WriteLine($"{c.CategoryName} has {c.Products.Count} products.");
        }
    }

    private static void FilteredIncludes()
    {
        using NorthwindDb db = new();
        SectionTitle("Products with a minimum number of units in stock");
        string? input;
        int stock;
        do
        {
            Write("Enter a minimum for units in stock: ");
            input = ReadLine();
        } while (!int.TryParse(input, out stock));
        IQueryable<Category>? categories = db.Categories?.Include(c => c.Products.Where(p => p.UnitsInStock >= stock));
        if (categories is null || !categories.Any())
        {
            Fail("No categories found.");
            return;
        }
        foreach (Category c in categories)
        {
            WriteLine("{0} has {1} products with a minimum {2} units in stock.", arg0: c.CategoryName, arg1: c.Products.Count, arg2: stock);
            foreach (Product p in c.Products)
            {
                WriteLine($"  {p.ProductName} has {p.UnitsInStock} units in stock.");
            }
        }
    }

    




}
