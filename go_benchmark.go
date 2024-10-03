package main

import (
    "encoding/json"
    "fmt"
    "io/ioutil"
    "os"
    "strings"
    "time"
)

type Person struct {
    ID     int     `json:"id"`
    Name   string  `json:"name"`
    Age    int     `json:"age"`
    City   string  `json:"city"`
    Born   string  `json:"born"`
    Height float64 `json:"height"`
    Weight float64 `json:"weight"`
}

func loadPersonsFromJson(path string) ([]Person, error) {
    startTime := time.Now()

    file, err := os.Open(path)
    if err != nil {
        return nil, err
    }
    defer file.Close()

    byteValue, _ := ioutil.ReadAll(file)

    var persons []Person
    json.Unmarshal(byteValue, &persons)

    fmt.Printf("File loading took %f seconds\n", time.Since(startTime).Seconds())
    return persons, nil
}

func benchmarkStringOperations(persons []Person) {
    startTime := time.Now()

    // Concatenation of names
    var concatenatedNames strings.Builder
    for _, person := range persons {
        concatenatedNames.WriteString(person.Name + " ")
    }

    // Searching for a substring in city names
    substringCount := 0
    for _, person := range persons {
        if strings.Contains(person.City, "New") {
            substringCount++
        }
    }

    // Reversing the names
    var reversedNames []string
    for _, person := range persons {
        reversedName := reverseString(person.Name)
        reversedNames = append(reversedNames, reversedName)
    }

    fmt.Printf("String operations took %f seconds\n", time.Since(startTime).Seconds())
}

func reverseString(s string) string {
    runes := []rune(s)
    for i, j := 0, len(runes)-1; i < j; i, j = i+1, j-1 {
        runes[i], runes[j] = runes[j], runes[i]
    }
    return string(runes)
}

func benchmarkIntegerOperations(persons []Person) {
    startTime := time.Now()

    // Summing the ages
    totalAge := 0
    for _, person := range persons {
        totalAge += person.Age
    }

    // Finding the maximum and minimum ages
    maxAge := persons[0].Age
    minAge := persons[0].Age
    for _, person := range persons {
        if person.Age > maxAge {
            maxAge = person.Age
        }
        if person.Age < minAge {
            minAge = person.Age
        }
    }

    // Counting the number of people within a certain age range
    ageRangeCount := 0
    for _, person := range persons {
        if person.Age >= 20 && person.Age <= 30 {
            ageRangeCount++
        }
    }

    fmt.Printf("Integer operations took %f seconds\n", time.Since(startTime).Seconds())
}

func benchmarkFloatOperations(persons []Person) {
    startTime := time.Now()

    // Calculating the average height and weight
    totalHeight := 0.0
    totalWeight := 0.0
    for _, person := range persons {
        totalHeight += person.Height
        totalWeight += person.Weight
    }
    _ = totalHeight / float64(len(persons))
    _ = totalWeight / float64(len(persons))

    // Finding the maximum and minimum height and weight
    maxHeight := persons[0].Height
    minHeight := persons[0].Height
    maxWeight := persons[0].Weight
    minWeight := persons[0].Weight
    for _, person := range persons {
        if person.Height > maxHeight {
            maxHeight = person.Height
        }
        if person.Height < minHeight {
            minHeight = person.Height
        }
        if person.Weight > maxWeight {
            maxWeight = person.Weight
        }
        if person.Weight < minWeight {
            minWeight = person.Weight
        }
    }

    // Scaling the height and weight by a factor
    var scaledHeights []float64
    var scaledWeights []float64
    for _, person := range persons {
        scaledHeights = append(scaledHeights, person.Height*1.1)
        scaledWeights = append(scaledWeights, person.Weight*1.1)
    }

    fmt.Printf("Float operations took %f seconds\n", time.Since(startTime).Seconds())
}

func main() {
    persons, err := loadPersonsFromJson("samples.json")
    if err != nil {
        fmt.Println("Error loading persons:", err)
        return
    }

    benchmarkStringOperations(persons)
    benchmarkIntegerOperations(persons)
    benchmarkFloatOperations(persons)
}