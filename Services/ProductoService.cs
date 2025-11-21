// Archivo: Services/ProductoService.cs
namespace Proyecto.Models // <-- Este es el namespace
{
    public class ProductoService
{
    public async Task<List<Producto>> GetProductosAsync()
    {
        // ----------------------------------------------------------------
        // *** IMPORTANTE: Reemplaza esta lógica de datos de prueba ***
        // *** por tu lógica real de lectura de la base de datos ***
        // ----------------------------------------------------------------
        
        // Simulación de una llamada asíncrona a la base de datos
        await Task.Delay(500); 

        // Datos de prueba:
        return new List<Producto>
        {
            new Producto { Id = 1, Nombre = "Laptop Gamer X1", Descripcion = "Potente portátil para juegos.", Precio = 1250.00m, Stock = 15 },
            new Producto { Id = 2, Nombre = "Monitor 4K Ultra", Descripcion = "Pantalla de 32 pulgadas con resolución 4K.", Precio = 450.50m, Stock = 8 },
            new Producto { Id = 3, Nombre = "Teclado Mecánico RGB", Descripcion = "Teclado con switches rojos e iluminación RGB.", Precio = 75.99m, Stock = 30 },
            new Producto { Id = 4, Nombre = "Ratón Inalámbrico", Descripcion = "Ratón ergonómico de alta precisión.", Precio = 25.00m, Stock = 50 }
        };
    }
}   
}