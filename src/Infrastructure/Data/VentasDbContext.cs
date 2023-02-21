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
        var cmd = new SqlCommand("SELECT [Id],[Nombre],[Direccion],[Telefono],[Correo] FROM [Venta]", con);
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
                    Cliente = (Cliente)dr["Cliente"],
                    ProductoId = (Guid)dr["ProductoId"],
                    Producto = (Producto)dr["Producto"],
                    Fecha = (DateTime)dr["Fecha"]
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
        var cmd = new SqlCommand("SELECT [Id],[ClienteId],[Cliente],[ProductoId],[Producto],[Fecha] FROM [Venta] WHERE [Id] = @Id", con);
        cmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = id;
        try
        {
            con.Open();
            var dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                data.Id = (Guid)dr["Id"];
                data.ClienteId = (Guid)dr["ClienteId"];
                data.Cliente = (Cliente)dr["Cliente"];
                data.ProductoId = (Guid)dr["ProductoId"];
                data.Producto = (Producto)dr["Producto"];
                data.Fecha = (DateTime)dr["Fecha"];
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
        var cmd = new SqlCommand("INSERT INTO [Venta] ([ClienteId],[Cliente],[ProductoId],[Producto],[Fecha]) VALUES (@ClienteId,@Cliente,@ProductoId,@Producto,@Fecha)", con);
        cmd.Parameters.Add("ClienteId", SqlDbType.NVarChar, 128 ).Value = data.ClienteId;
        cmd.Parameters.Add("Cliente", SqlDbType.NVarChar, 128).Value = data.ProductoId;
        cmd.Parameters.Add("ProductoId", SqlDbType.NVarChar, 128).Value = data.ProductoId;
        cmd.Parameters.Add("Producto", SqlDbType.NVarChar, 128).Value = data.Producto;
        cmd.Parameters.Add("Fecha", SqlDbType.NVarChar, 128).Value = data.Fecha;

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
        var cmd = new SqlCommand("UPDATE [Venta] SET [ClienteId] = @ClienteId, [Cliente] = @Cliente, [ProductoId] = @ProductoId, [Producto] = @Producto, [Fecha] = @Fecha,  WHERE [Id] = @Id", con);
        cmd.Parameters.Add("ClienteId", SqlDbType.UniqueIdentifier).Value = data.ClienteId;
        cmd.Parameters.Add("Cliente", SqlDbType.NVarChar, 128).Value = data.Cliente;
        cmd.Parameters.Add("ProductoId", SqlDbType.NVarChar, 128).Value = data.ProductoId;
        cmd.Parameters.Add("Producto", SqlDbType.NVarChar, 128).Value = data.Producto;
        cmd.Parameters.Add("Fecha", SqlDbType.NVarChar, 128).Value = data.Fecha;

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
