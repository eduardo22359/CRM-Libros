using System.Data;
using System.Data.SqlClient;

using Domain;

namespace Infrastructure;
public class ClientesDbContext
{
    private readonly string _connectionString;
    public ClientesDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<Cliente> List()
    {
        var data = new List<Cliente>();

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

    public Cliente Details(Guid id)
    {
        var data = new Cliente();

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

    public void Create(Cliente data)
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

    public void Edit(Cliente data)
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