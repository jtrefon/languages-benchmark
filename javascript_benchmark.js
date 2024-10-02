const fs = require('fs');

class Person {
  constructor(id, name, age, city, born, height, weight) {
    this.id = id;
    this.name = name;
    this.age = age;
    this.city = city;
    this.born = new Date(born.split('/').reverse().join('-'));
    this.height = height;
    this.weight = weight;
  }

  static fromObject(data) {
    return new Person(
      data.id,
      data.name,
      data.age,
      data.city,
      data.born,
      data.height,
      data.weight
    );
  }
}

class Benchmark {
  constructor() {
    this.path = 'samples.json';
  }

  loadPersonsFromJson() {
    const startTime = Date.now();

    const data = JSON.parse(fs.readFileSync(this.path, 'utf8'));
    const persons = data.map(person => Person.fromObject(person));

    const endTime = Date.now();
    console.log(`File loading took ${(endTime - startTime) / 1000} seconds`);

    return persons;
  }

  benchmarkStringOperations(persons) {
    const startTime = Date.now();

    // Concatenation of names
    const concatenatedNames = persons.map(person => person.name).join(' ');

    // Searching for a substring in city names
    const substringCount = persons.filter(person => person.city.includes('New')).length;

    // Reversing the names
    const reversedNames = persons.map(person => person.name.split('').reverse().join(''));

    const endTime = Date.now();
    console.log(`String operations took ${(endTime - startTime) / 1000} seconds`);
  }

  benchmarkIntegerOperations(persons) {
    const startTime = Date.now();

    // Summing the ages
    const totalAge = persons.reduce((sum, person) => sum + person.age, 0);

    // Finding the maximum and minimum ages
    const ages = persons.map(person => person.age);
    const maxAge = ages.reduce((max, age) => (age > max ? age : max), ages[0]);
    const minAge = ages.reduce((min, age) => (age < min ? age : min), ages[0]);

    // Counting the number of people within a certain age range
    const ageRangeCount = persons.filter(person => person.age >= 20 && person.age <= 30).length;

    const endTime = Date.now();
    console.log(`Integer operations took ${(endTime - startTime) / 1000} seconds`);
  }

  benchmarkFloatOperations(persons) {
    const startTime = Date.now();

    // Calculating the average height and weight
    const totalHeight = persons.reduce((sum, person) => sum + person.height, 0);
    const totalWeight = persons.reduce((sum, person) => sum + person.weight, 0);
    const avgHeight = totalHeight / persons.length;
    const avgWeight = totalWeight / persons.length;

    // Finding the maximum and minimum height and weight
    const heights = persons.map(person => person.height);
    const weights = persons.map(person => person.weight);
    const maxHeight = heights.reduce((max, height) => (height > max ? height : max), heights[0]);
    const minHeight = heights.reduce((min, height) => (height < min ? height : min), heights[0]);
    const maxWeight = weights.reduce((max, weight) => (weight > max ? weight : max), weights[0]);
    const minWeight = weights.reduce((min, weight) => (weight < min ? weight : min), weights[0]);

    // Scaling the height and weight by a factor
    const scaledHeights = heights.map(height => height * 1.1);
    const scaledWeights = weights.map(weight => weight * 1.1);

    const endTime = Date.now();
    console.log(`Float operations took ${(endTime - startTime) / 1000} seconds`);
  }
}

// Example usage
const benchmark = new Benchmark();
const persons = benchmark.loadPersonsFromJson();

benchmark.benchmarkStringOperations(persons);
benchmark.benchmarkIntegerOperations(persons);
benchmark.benchmarkFloatOperations(persons);