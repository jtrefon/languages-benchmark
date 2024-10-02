#include <iostream>
#include <fstream>
#include <vector>
#include <string>
#include <sstream>
#include <chrono>
#include <json/json.h>

class Person {
public:
    int id;
    std::string name;
    int age;
    std::string city;
    std::tm born;
    double height;
    double weight;

    Person(int id, const std::string& name, int age, const std::string& city, const std::string& born, double height, double weight)
        : id(id), name(name), age(age), city(city), height(height), weight(weight) {
        std::istringstream ss(born);
        ss >> std::get_time(&this->born, "%d/%m/%Y");
    }

    static Person fromJson(const Json::Value& data) {
        return Person(
            data["id"].asInt(),
            data["name"].asString(),
            data["age"].asInt(),
            data["city"].asString(),
            data["born"].asString(),
            data["height"].asDouble(),
            data["weight"].asDouble()
        );
    }
};

class Benchmark {
private:
    std::string path = "samples.json";

public:
    std::vector<Person> loadPersonsFromJson() {
        auto start_time = std::chrono::high_resolution_clock::now();

        std::ifstream file(path);
        Json::Value data;
        file >> data;

        std::vector<Person> persons;
        for (const auto& personData : data) {
            persons.push_back(Person::fromJson(personData));
        }

        auto end_time = std::chrono::high_resolution_clock::now();
        std::chrono::duration<double> duration = end_time - start_time;
        std::cout << "File loading took " << duration.count() << " seconds" << std::endl;

        return persons;
    }

    void benchmarkStringOperations(const std::vector<Person>& persons) {
        auto start_time = std::chrono::high_resolution_clock::now();

        // Concatenation of names
        std::string concatenatedNames;
        for (const auto& person : persons) {
            concatenatedNames += person.name + " ";
        }

        // Searching for a substring in city names
        int substringCount = 0;
        for (const auto& person : persons) {
            if (person.city.find("New") != std::string::npos) {
                substringCount++;
            }
        }

        // Reversing the names
        std::vector<std::string> reversedNames;
        for (const auto& person : persons) {
            std::string reversedName = person.name;
            std::reverse(reversedName.begin(), reversedName.end());
            reversedNames.push_back(reversedName);
        }

        auto end_time = std::chrono::high_resolution_clock::now();
        std::chrono::duration<double> duration = end_time - start_time;
        std::cout << "String operations took " << duration.count() << " seconds" << std::endl;
    }

    void benchmarkIntegerOperations(const std::vector<Person>& persons) {
        auto start_time = std::chrono::high_resolution_clock::now();

        // Summing the ages
        int totalAge = 0;
        for (const auto& person : persons) {
            totalAge += person.age;
        }

        // Finding the maximum and minimum ages
        int maxAge = persons[0].age;
        int minAge = persons[0].age;
        for (const auto& person : persons) {
            if (person.age > maxAge) maxAge = person.age;
            if (person.age < minAge) minAge = person.age;
        }

        // Counting the number of people within a certain age range
        int ageRangeCount = 0;
        for (const auto& person : persons) {
            if (person.age >= 20 && person.age <= 30) {
                ageRangeCount++;
            }
        }

        auto end_time = std::chrono::high_resolution_clock::now();
        std::chrono::duration<double> duration = end_time - start_time;
        std::cout << "Integer operations took " << duration.count() << " seconds" << std::endl;
    }

    void benchmarkFloatOperations(const std::vector<Person>& persons) {
        auto start_time = std::chrono::high_resolution_clock::now();

        // Calculating the average height and weight
        double totalHeight = 0;
        double totalWeight = 0;
        for (const auto& person : persons) {
            totalHeight += person.height;
            totalWeight += person.weight;
        }
        double avgHeight = totalHeight / persons.size();
        double avgWeight = totalWeight / persons.size();

        // Finding the maximum and minimum height and weight
        double maxHeight = persons[0].height;
        double minHeight = persons[0].height;
        double maxWeight = persons[0].weight;
        double minWeight = persons[0].weight;
        for (const auto& person : persons) {
            if (person.height > maxHeight) maxHeight = person.height;
            if (person.height < minHeight) minHeight = person.height;
            if (person.weight > maxWeight) maxWeight = person.weight;
            if (person.weight < minWeight) minWeight = person.weight;
        }

        // Scaling the height and weight by a factor
        std::vector<double> scaledHeights;
        std::vector<double> scaledWeights;
        for (const auto& person : persons) {
            scaledHeights.push_back(person.height * 1.1);
            scaledWeights.push_back(person.weight * 1.1);
        }

        auto end_time = std::chrono::high_resolution_clock::now();
        std::chrono::duration<double> duration = end_time - start_time;
        std::cout << "Float operations took " << duration.count() << " seconds" << std::endl;
    }
};

int main() {
    Benchmark benchmark;
    std::vector<Person> persons = benchmark.loadPersonsFromJson();
    benchmark.benchmarkStringOperations(persons);
    benchmark.benchmarkIntegerOperations(persons);
    benchmark.benchmarkFloatOperations(persons);
    return 0;
}