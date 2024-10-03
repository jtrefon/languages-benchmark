use serde::Deserialize;
use std::fs::File;
use std::io::BufReader;
use std::time::Instant;

#[derive(Deserialize)]
struct Person {
    id: i32,
    name: String,
    age: i32,
    city: String,
    born: String,
    height: f64,
    weight: f64,
}

fn load_persons_from_json(path: &str) -> Vec<Person> {
    let start_time = Instant::now();

    let file = File::open(path).expect("Unable to open file");
    let reader = BufReader::new(file);
    let persons: Vec<Person> = serde_json::from_reader(reader).expect("Unable to parse JSON");

    println!("File loading took {:.2?} seconds", start_time.elapsed());
    persons
}

fn benchmark_string_operations(persons: &[Person]) {
    let start_time = Instant::now();

    // Concatenation of names
    let concatenated_names: String = persons.iter().map(|p| p.name.clone() + " ").collect();

    // Searching for a substring in city names
    let substring_count = persons.iter().filter(|p| p.city.contains("New")).count();

    // Reversing the names
    let reversed_names: Vec<String> = persons.iter().map(|p| p.name.chars().rev().collect()).collect();

    println!("String operations took {:.2?} seconds", start_time.elapsed());
}

fn benchmark_integer_operations(persons: &[Person]) {
    let start_time = Instant::now();

    // Summing the ages
    let total_age: i32 = persons.iter().map(|p| p.age).sum();

    // Finding the maximum and minimum ages
    let max_age = persons.iter().map(|p| p.age).max().unwrap();
    let min_age = persons.iter().map(|p| p.age).min().unwrap();

    // Counting the number of people within a certain age range
    let age_range_count = persons.iter().filter(|p| p.age >= 20 && p.age <= 30).count();

    println!("Integer operations took {:.2?} seconds", start_time.elapsed());
}

fn benchmark_float_operations(persons: &[Person]) {
    let start_time = Instant::now();

    // Calculating the average height and weight
    let total_height: f64 = persons.iter().map(|p| p.height).sum();
    let total_weight: f64 = persons.iter().map(|p| p.weight).sum();
    let avg_height = total_height / persons.len() as f64;
    let avg_weight = total_weight / persons.len() as f64;

    // Finding the maximum and minimum height and weight
    let max_height = persons.iter().map(|p| p.height).max_by(|a, b| a.partial_cmp(b).unwrap()).unwrap();
    let min_height = persons.iter().map(|p| p.height).min_by(|a, b| a.partial_cmp(b).unwrap()).unwrap();
    let max_weight = persons.iter().map(|p| p.weight).max_by(|a, b| a.partial_cmp(b).unwrap()).unwrap();
    let min_weight = persons.iter().map(|p| p.weight).min_by(|a, b| a.partial_cmp(b).unwrap()).unwrap();

    // Scaling the height and weight by a factor
    let scaled_heights: Vec<f64> = persons.iter().map(|p| p.height * 1.1).collect();
    let scaled_weights: Vec<f64> = persons.iter().map(|p| p.weight * 1.1).collect();

    println!("Float operations took {:.2?} seconds", start_time.elapsed());
}

fn main() {
    let persons = load_persons_from_json("../samples.json");
    benchmark_string_operations(&persons);
    benchmark_integer_operations(&persons);
    benchmark_float_operations(&persons);
}
