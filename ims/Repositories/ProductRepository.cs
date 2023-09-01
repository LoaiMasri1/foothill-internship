using ims.Models;
using Microsoft.Data.SqlClient;

namespace ims.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly string _connectionString;

    public ProductRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public bool Create(Product product)
    {
        var query = "INSERT INTO products (name, price, quantity) VALUES (@name, @price, @quantity)";

        bool result = Excuter(query, command =>
        {
            command.Parameters.AddWithValue("@name", product.Name);
            command.Parameters.AddWithValue("@price", product.Price);
            command.Parameters.AddWithValue("@quantity", product.Quantity);
        });

        return result;
    }

    public bool Delete(string productName)
    {
        var query = "DELETE FROM products WHERE name = @name";

        bool result = Excuter(query, command =>
        {
            command.Parameters.AddWithValue("@name", productName);
        });

        return result;
    }

    public Product? Get(string productName)
    {
        var query = $"SELECT * FROM products WHERE name = @name";

        var result = Excuter<Product>(query, command =>
        {
            command.Parameters.AddWithValue("@name", productName);
        });

        return result.FirstOrDefault();
    }

    public ICollection<Product> GetAll()
    {
        var query = "SELECT * FROM products";

        var result = Excuter<Product>(query);

        return result;
    }

    public bool IsExists(string productName)
    {
        var query = "SELECT * FROM products WHERE name = @name";

        var results = Excuter<Product>(query, command =>
        {
            command.Parameters.AddWithValue("@name", productName);
        });

        return results.Any();
    }

    public bool Update(Product product)
    {
        var query = "UPDATE products SET name = @name, price = @price, quantity = @quantity WHERE name = @name";

        bool result = Excuter(query, command =>
        {
            command.Parameters.AddWithValue("@name", product.Name);
            command.Parameters.AddWithValue("@price", product.Price);
            command.Parameters.AddWithValue("@quantity", product.Quantity);
        });

        return result;
    }

    private static T ReadFromReader<T>(SqlDataReader reader)
    {
        var properties = typeof(T).GetProperties();
        var instance = Activator.CreateInstance<T>();
        foreach (var property in properties)
        {
            var value = reader[property.Name];
            if (value == DBNull.Value)
            {
                property.SetValue(instance, null);
            }
            else
            {
                property.SetValue(instance, value);
            }
        }

        return instance;
    }

    private bool Excuter(
    string query,
    Action<SqlCommand>? addParameters = null)
    {
        using var connection = new SqlConnection(_connectionString);

        using var command = new SqlCommand(query, connection);
        addParameters?.Invoke(command);

        connection.Open();
        var result = command.ExecuteNonQuery();
        connection.Close();

        return result == 1;
    }

    private ICollection<T> Excuter<T>(
        string query,
        Action<SqlCommand>? addParameters = null)
    {

        using var connection = new SqlConnection(_connectionString);

        using var command = new SqlCommand(query, connection);
        addParameters?.Invoke(command);

        connection.Open();
        var reader = command.ExecuteReader();
        var list = new List<T>();
        while (reader.Read())
        {
            list.Add(ReadFromReader<T>(reader));
        }

        connection.Close();

        return list;
    }
}
