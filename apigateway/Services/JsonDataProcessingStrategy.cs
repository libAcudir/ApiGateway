using Common.DTO;
using Common.Interfaces;
using Domain;
using System.Text.Json;

public class JsonDataProcessingStrategy : IDataProcessingStrategy
{
    public Task<StandardResponse> DataAsync(RequestParameters parameters)
    {
        throw new NotImplementedException();
    }

    public void ProcessData(Data data)
    {
        // Lógica para procesar datos en formato JSON en futuro 
        Console.WriteLine("Processing JSON data: " + data.Content);

        // Ejemplo de deserialización
        var jsonData = JsonSerializer.Deserialize<dynamic>(data.Content);
        Console.WriteLine("Deserialized JSON: " + jsonData);
    }
}
