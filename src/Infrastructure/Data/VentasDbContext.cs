using System.Data;
using System.Data.SqlClient;

using Domain;

namespace Infrastructure;

public class VentasDbContext
{
    private readonly string _connectionString;
    public VentasDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<Venta> List()
    {
        var data = new List<Venta>();

        var con = new SqlConnection(_connectionString);
        //var cmd = new SqlCommand("SELECT [Id],[Nombre],[Direccion],[Telefono],[Correo] FROM [Venta]", con);
        var cmd = new SqlCommand("SELECT v.id, v.ClienteId, c.nombre, c.telefono, c.correo, v.ProductoId, p.descripcion, p.precio, p.cantidad, v.fecha FROM Producto p inner join Venta v on p.Id = v.ProductoId inner join cliente c on v.ClienteId = c.Id", con);
        try
        {
            con.Open();
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                data.Add(new Venta
                {
                    Id = (Guid)dr["Id"],
                    ClienteId = (Guid)dr["ClienteId"],
                    Nombre = (string)dr["Nombre"],
                    Telefono = (string)dr["Telefono"],
                    Correo = (string)dr["Correo"],
                    ProductoId = (Guid)dr["ProductoId"],
                    Descripcion = (string)dr["Descripcion"],
                    Precio = decimal.Parse(((decimal)dr["Precio"]).ToString("0.########")),
                    Cantidad = (int)dr["Cantidad"],
                    Fecha = (DateTime)dr["Fecha"]


                    /*Id = (Guid)dr["Id"],
                    ClienteId = (Guid)dr["ClienteId"],
                    Cliente = (Cliente)dr["Cliente"],
                    ProductoId = (Guid)dr["ProductoId"],
                    Producto = (Producto)dr["Producto"],
                    Fecha = (DateTime)dr["Fecha"]
                    */
                });
            }
            return data;
        }
        catch (Exception) { throw; }
        finally
        {
            con.Close();
        }
    }

    public Venta Details(Guid id)
    {
        var data = new Venta();

        var con = new SqlConnection(_connectionString);
        //var cmd = new SqlCommand("SELECT [Id],[ClienteId],[Cliente],[ProductoId],[Producto],[Fecha] FROM [Venta] WHERE [Id] = @Id", con);
        var cmd = new SqlCommand("SELECT v.id, v.ClienteId, c.nombre, c.telefono, c.correo, v.ProductoId, p.descripcion, p.precio, p.cantidad, v.fecha FROM Producto p inner join Venta v on p.Id = v.ProductoId inner join cliente c on v.ClienteId = c.Id  WHERE [v].[Id] = @Id", con);
        cmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = id;
        try
        {
            con.Open();
            var dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                data.Id = (Guid)dr["Id"];
                data.ClienteId = (Guid)dr["ClienteId"];
                data.Nombre = (string)dr["Nombre"];
                data.Telefono = (string)dr["Telefono"];
                data.Correo = (string)dr["Correo"];
                data.ProductoId = (Guid)dr["ProductoId"];
                data.Descripcion = (string)dr["Descripcion"];
                data.Precio = decimal.Parse(((decimal)dr["Precio"]).ToString("0.########"));
                data.Cantidad = (int)dr["Cantidad"];
                data.Fecha = (DateTime)dr["Fecha"];

                /*
                data.Id = (Guid)dr["Id"];
                data.ClienteId = (Guid)dr["ClienteId"];
                data.Cliente = (Cliente)dr["Cliente"];
                data.ProductoId = (Guid)dr["ProductoId"];
                data.Producto = (Producto)dr["Producto"];
                data.Fecha = (DateTime)dr["Fecha"];
                */
            }
            return data;
        }
        catch (Exception) { throw; }
        finally
        {
            con.Close();
        }
    }

    

    public void Create(Venta data)
    {
        // Checar con el profesor esta parte :u 
        var con = new SqlConnection(_connectionString);
        var cmd = new SqlCommand("INSERT INTO [Venta] ([ClienteId],[ProductoId],[Fecha]) VALUES (@ClienteId,@ProductoId,@Fecha)", con);
        cmd.Parameters.Add("ClienteId", SqlDbType.UniqueIdentifier).Value = data.ClienteId;
        cmd.Parameters.Add("ProductoId", SqlDbType.UniqueIdentifier).Value = data.ProductoId;
        cmd.Parameters.Add("Fecha", SqlDbType.DateTime).Value = data.Fecha;

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception) { throw; }
        finally
        {
            con.Close();
        }
    }
    

    public void Edit(Venta data)
    {
        var con = new SqlConnection(_connectionString);
        //var cmd = new SqlCommand("UPDATE [Venta] SET [ClienteId] = @ClienteId, [Cliente] = @Cliente, [ProductoId] = @ProductoId, [Producto] = @Producto, [Fecha] = @Fecha,  WHERE [Id] = @Id", con);

        var cmd = new SqlCommand("UPDATE [Venta] SET [ClienteId] = @ClienteId, [ProductoId] = @ProductoId, [Fecha] = CONVERT(datetime, @Fecha, 101)  WHERE [Id] = @Id", con);
        cmd.Parameters.Add("Id", SqlDbType.UniqueIdentifier).Value = data.Id;
        cmd.Parameters.Add("ClienteId", SqlDbType.UniqueIdentifier).Value = data.ClienteId;
        cmd.Parameters.Add("ProductoId", SqlDbType.UniqueIdentifier).Value = data.ProductoId;
        cmd.Parameters.Add("Fecha", SqlDbType.DateTime).Value = data.Fecha;

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception) { throw; }
        finally
        {
            con.Close();
        }
    }

    public void Delete(Guid id)
    {
        var con = new SqlConnection(_connectionString);
        var cmd = new SqlCommand("DELETE FROM [Venta] WHERE [Id] = @Id", con);
        cmd.Parameters.Add("Id", SqlDbType.UniqueIdentifier).Value = id;

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception) { throw; }
        finally
        {
            con.Close();
        }
    }




}
