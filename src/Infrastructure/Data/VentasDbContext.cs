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

        // ToDo
        try
        {
            // ToDo
            return data;
        }
        catch (Exception) { throw; }
        finally
        {
            // ToDo
        }
    }

    public Venta Details(Guid id)
    {
        var data = new Venta();

        // ToDo
        try
        {
            // ToDo
            return data;
        }
        catch (Exception) { throw; }
        finally
        {
            // ToDo
        }
    }

    public void Create(Venta data)
    {
        // ToDo

        try
        {
            // ToDo
        }
        catch (Exception) { throw; }
        finally
        {
            // ToDo
        }
    }

    public void Edit(Venta data)
    {
        // ToDo

        try
        {
            // ToDo
        }
        catch (Exception) { throw; }
        finally
        {
            // ToDo
        }
    }

    public void Delete(Guid id)
    {
        // ToDo

        try
        {
            // ToDo
        }
        catch (Exception) { throw; }
        finally
        {
            // ToDo
        }
    }
}
