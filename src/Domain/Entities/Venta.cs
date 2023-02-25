using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;
public class Venta
{
    /*public Guid Id { get; set; }

    //Cliente
    public Guid ClienteId { get; set; }

    //public virtual Cliente Cliente { get; set; }

    //Producto
    public Guid ProductoId { get; set; }

    //public virtual Producto Producto { get; set; }

    public DateTime Fecha { get; set; }

    */
    [Key]
    public Guid Id { get; set; }

    public Guid ClienteId { get; set; }

    public string Nombre { get; set; }

    public string Telefono { get; set; }

    public string Correo { get; set; }

    public Guid ProductoId { get; set; }

    public string Descripcion { get; set; }

    public decimal Precio { get; set; }

    public int Cantidad { get; set; }

    public DateTime Fecha { get; set; }

}

