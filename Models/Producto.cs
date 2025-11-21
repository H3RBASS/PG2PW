// Archivo: Models/Producto.cs

using System.ComponentModel.DataAnnotations;
namespace Proyecto.Models
{
    public class Producto
{
    public int Id { get; set; }
    
    [Required]
    public string Nombre { get; set; } = string.Empty;

    public string Descripcion { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a cero")]
    public decimal Precio { get; set; }

    public int Stock { get; set; }
}
}
