using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml;
using WeatherApp.Common.Models;

namespace WeatherApp.Common;

public class XMLInputParser<T> : IInputParser<T>
{
    public T? Parse(string input)
    {
        try
        {
            var document = XDocument.Parse(input);

            var serializer = new XmlSerializer(typeof(T));

            using var reader = document.CreateReader();

            var data = (T?)serializer.Deserialize(reader);

            return data;

        }
        catch (XmlException ex)
        {
            Console.WriteLine($"Error while parsing XML: {ex.Message}");
            return default;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine($"Something Went Wrong");
            return default;
        }
    }
}
