package com.benchmark;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.stream.Collectors;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;

class Person {
    int id;
    String name;
    int age;
    String city;
    String born;
    double height;
    double weight;

    public Person(int id, String name, int age, String city, String born, double height, double weight) {
        this.id = id;
        this.name = name;
        this.age = age;
        this.city = city;
        this.born = born;
        this.height = height;
        this.weight = weight;
    }
}

class Benchmark {
    private String path = "samples.json";

    public List<Person> loadPersonsFromJson() throws IOException {
        long startTime = System.currentTimeMillis();

        Gson gson = new Gson();
        List<Person> persons = gson.fromJson(new FileReader(path), new TypeToken<List<Person>>() {}.getType());

        long endTime = System.currentTimeMillis();
        System.out.println("File loading took " + (endTime - startTime) / 1000.0 + " seconds");

        return persons;
    }

    public void benchmarkStringOperations(List<Person> persons) {
        long startTime = System.currentTimeMillis();

        // Concatenation of names
        String concatenatedNames = persons.stream().map(person -> person.name).collect(Collectors.joining(" "));

        // Searching for a substring in city names
        long substringCount = persons.stream().filter(person -> person.city.contains("New")).count();

        // Reversing the names
        List<String> reversedNames = persons.stream().map(person -> new StringBuilder(person.name).reverse().toString()).collect(Collectors.toList());

        long endTime = System.currentTimeMillis();
        System.out.println("String operations took " + (endTime - startTime) / 1000.0 + " seconds");
    }

    public void benchmarkIntegerOperations(List<Person> persons) {
        long startTime = System.currentTimeMillis();

        // Summing the ages
        int totalAge = persons.stream().mapToInt(person -> person.age).sum();

        // Finding the maximum and minimum ages
        int maxAge = persons.stream().mapToInt(person -> person.age).max().orElseThrow();
        int minAge = persons.stream().mapToInt(person -> person.age).min().orElseThrow();

        // Counting the number of people within a certain age range
        long ageRangeCount = persons.stream().filter(person -> person.age >= 20 && person.age <= 30).count();

        long endTime = System.currentTimeMillis();
        System.out.println("Integer operations took " + (endTime - startTime) / 1000.0 + " seconds");
    }

    public void benchmarkFloatOperations(List<Person> persons) {
        long startTime = System.currentTimeMillis();

        // Calculating the average height and weight
        double totalHeight = persons.stream().mapToDouble(person -> person.height).sum();
        double totalWeight = persons.stream().mapToDouble(person -> person.weight).sum();
        double avgHeight = totalHeight / persons.size();
        double avgWeight = totalWeight / persons.size();

        // Finding the maximum and minimum height and weight
        double maxHeight = persons.stream().mapToDouble(person -> person.height).max().orElseThrow();
        double minHeight = persons.stream().mapToDouble(person -> person.height).min().orElseThrow();
        double maxWeight = persons.stream().mapToDouble(person -> person.weight).max().orElseThrow();
        double minWeight = persons.stream().mapToDouble(person -> person.weight).min().orElseThrow();

        // Scaling the height and weight by a factor
        List<Double> scaledHeights = persons.stream().map(person -> person.height * 1.1).collect(Collectors.toList());
        List<Double> scaledWeights = persons.stream().map(person -> person.weight * 1.1).collect(Collectors.toList());

        long endTime = System.currentTimeMillis();
        System.out.println("Float operations took " + (endTime - startTime) / 1000.0 + " seconds");
    }
}

public class Main {
    public static void main(String[] args) {
        try {
            Benchmark benchmark = new Benchmark();
            List<Person> persons = benchmark.loadPersonsFromJson();

            benchmark.benchmarkStringOperations(persons);
            benchmark.benchmarkIntegerOperations(persons);
            benchmark.benchmarkFloatOperations(persons);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}