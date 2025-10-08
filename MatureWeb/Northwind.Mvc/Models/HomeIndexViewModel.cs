using Northwind.EntityModels;
namespace Northwind.Mvc.Models;

public record HomeIndexViewModel(int VistorCount, IList<Category> Categories, IList<Product> Products);


