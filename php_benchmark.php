#!/usr/bin/php
<?php

ini_set('memory_limit', '2G');

class Person {
    public $id;
    public $name;
    public $age;
    public $city;
    public $born;
    public $height;
    public $weight;

    public function __construct($id, $name, $age, $city, $born, $height, $weight) {
        $this->id = $id;
        $this->name = $name;
        $this->age = $age;
        $this->city = $city;
        $this->born = DateTime::createFromFormat('d/m/Y', $born);
        $this->height = $height;
        $this->weight = $weight;
    }

    public static function fromArray($data) {
        return new self(
            $data['id'],
            $data['name'],
            $data['age'],
            $data['city'],
            $data['born'],
            $data['height'],
            $data['weight']
        );
    }
}

class Benchmark {
    private $path = 'samples.json';

    public function loadPersonsFromJson() {
        $start_time = microtime(true);

        $data = json_decode(file_get_contents($this->path), true);
        $persons = array_map(function($person) {
            return Person::fromArray($person);
        }, $data);

        $end_time = microtime(true);
        echo "File loading took " . ($end_time - $start_time) . " seconds\n";

        return $persons;
    }

    public function benchmarkIntegerOperations($persons) {
        $start_time = microtime(true);

        // Summing the ages
        $total_age = array_sum(array_map(function($person) {
            return $person->age;
        }, $persons));

        // Finding the maximum and minimum ages
        $ages = array_map(function($person) {
            return $person->age;
        }, $persons);
        $max_age = max($ages);
        $min_age = min($ages);

        // Counting the number of people within a certain age range
        $age_range_count = count(array_filter($persons, function($person) {
            return $person->age >= 20 && $person->age <= 30;
        }));

        $end_time = microtime(true);
        echo "Integer operations took " . ($end_time - $start_time) . " seconds\n";
    }

    public function benchmarkFloatOperations($persons) {
        $start_time = microtime(true);

        // Calculating the average height and weight
        $total_height = array_sum(array_map(function($person) {
            return $person->height;
        }, $persons));
        $total_weight = array_sum(array_map(function($person) {
            return $person->weight;
        }, $persons));
        $avg_height = $total_height / count($persons);
        $avg_weight = $total_weight / count($persons);

        // Finding the maximum and minimum height and weight
        $heights = array_map(function($person) {
            return $person->height;
        }, $persons);
        $weights = array_map(function($person) {
            return $person->weight;
        }, $persons);
        $max_height = max($heights);
        $min_height = min($heights);
        $max_weight = max($weights);
        $min_weight = min($weights);

        // Scaling the height and weight by a factor
        $scaled_heights = array_map(function($person) {
            return $person->height * 1.1;
        }, $persons);
        $scaled_weights = array_map(function($person) {
            return $person->weight * 1.1;
        }, $persons);

        $end_time = microtime(true);
        echo "Float operations took " . ($end_time - $start_time) . " seconds\n";
    }
    public function benchmarkStringOperations($persons) {
        $start_time = microtime(true);

        // Concatenation of names
        $concatenated_names = implode(" ", array_map(function($person) {
            return $person->name;
        }, $persons));

        // Searching for a substring in city names
        $substring_count = count(array_filter($persons, function($person) {
            return strpos($person->city, "New") !== false;
        }));

        // Reversing the names
        $reversed_names = array_map(function($person) {
            return strrev($person->name);
        }, $persons);

        $end_time = microtime(true);
        echo "String operations took " . ($end_time - $start_time) . " seconds\n";
    }
}

// Example usage
$benchmark = new Benchmark();
$persons = $benchmark->loadPersonsFromJson();
$benchmark->benchmarkStringOperations($persons);
$benchmark->benchmarkIntegerOperations($persons);
$benchmark->benchmarkFloatOperations($persons);

?>