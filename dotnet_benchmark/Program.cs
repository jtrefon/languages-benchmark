using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Diagnostics;

public class Person
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required int Age { get; set; }
    public required string City { get; set; }
    public required DateTime Born { get; set; }
    public required double Height { get; set; }
    public required double Weight { get; set; }
}

public class CustomDateTimeConverter : JsonConverter<DateTime>
{
    private readonly string _format = "dd/MM/yyyy";
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String && DateTime.TryParseExact(reader.GetString(), _format, null, System.Globalization.DateTimeStyles.None, out var date))
        {
            return date;
        }
        throw new JsonException($"Unable to convert \"{reader.GetString()}\" to DateTime.");
    }
    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(_format));
    }
}

public class Benchmark
{
    private readonly string path = "../samples.json";

    public List<Person> LoadPersonsFromJson()
    {
        var startTime = Stopwatch.StartNew();

        string jsonData;
        try
        {
            jsonData = File.ReadAllText(path);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error reading file: {ex.Message}");
        }

        List<Person> persons;
        try
        {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new CustomDateTimeConverter() }
        };
        persons = JsonSerializer.Deserialize<List<Person>>(jsonData, options);
        }
        catch (JsonException ex)
        {
            throw new Exception($"Error deserializing JSON data: {ex.Message}");
        }

        startTime.Stop();
        Console.WriteLine($"File loading took {startTime.Elapsed.TotalSeconds} seconds");

        return persons;
    }

    public void BenchmarkStringOperations(List<Person> persons)
    {
        var startTime = Stopwatch.StartNew();

        // Concatenation of names
        var concatenatedNames = string.Join(" ", persons.ConvertAll(p => p.Name));

        // Searching for a substring in city names
        var substringCount = persons.FindAll(p => p.City.Contains("New")).Count;

        // Reversing the names
        var reversedNames = persons.ConvertAll(p => new string(p.Name.Reverse().ToArray()));

        startTime.Stop();
        Console.WriteLine($"String operations took {startTime.Elapsed.TotalSeconds} seconds");
    }

    public void BenchmarkIntegerOperations(List<Person> persons)
    {
        var startTime = Stopwatch.StartNew();

        // Summing the ages
        var totalAge = 0;
        persons.ForEach(p => totalAge += p.Age);

        // Finding the maximum and minimum ages
        var maxAge = int.MinValue;
        var minAge = int.MaxValue;
        persons.ForEach(p =>
        {
            if (p.Age > maxAge) maxAge = p.Age;
            if (p.Age < minAge) minAge = p.Age;
        });

        // Counting the number of people within a certain age range
        var ageRangeCount = persons.FindAll(p => p.Age >= 20 && p.Age <= 30).Count;

        startTime.Stop();
        Console.WriteLine($"Integer operations took {startTime.Elapsed.TotalSeconds} seconds");
    }

    public void BenchmarkFloatOperations(List<Person> persons)
    {
        var startTime = Stopwatch.StartNew();

        // Calculating the average height and weight
        var totalHeight = 0.0;
        var totalWeight = 0.0;
        persons.ForEach(p =>
        {
            totalHeight += p.Height;
            totalWeight += p.Weight;
        });
        var avgHeight = totalHeight / persons.Count;
        var avgWeight = totalWeight / persons.Count;

        // Finding the maximum and minimum height and weight
        var maxHeight = double.MinValue;
        var minHeight = double.MaxValue;
        var maxWeight = double.MinValue;
        var minWeight = double.MaxValue;
        persons.ForEach(p =>
        {
            if (p.Height > maxHeight) maxHeight = p.Height;
            if (p.Height < minHeight) minHeight = p.Height;
            if (p.Weight > maxWeight) maxWeight = p.Weight;
            if (p.Weight < minWeight) minWeight = p.Weight;
        });

        // Scaling the height and weight by a factor
        var scaledHeights = persons.ConvertAll(p => p.Height * 1.1);
        var scaledWeights = persons.ConvertAll(p => p.Weight * 1.1);

        startTime.Stop();
        Console.WriteLine($"Float operations took {startTime.Elapsed.TotalSeconds} seconds");
    }
}

class Program
{
    static void Main(string[] args)
    {
        var benchmark = new Benchmark();
        var persons = benchmark.LoadPersonsFromJson();
        benchmark.BenchmarkStringOperations(persons);
        benchmark.BenchmarkIntegerOperations(persons);
        benchmark.BenchmarkFloatOperations(persons);
    }
}