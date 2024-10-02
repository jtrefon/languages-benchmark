import json
from datetime import datetime
import time
from typing import List


class Person:
    def __init__(self, id, name, age, city, born, height, weight):
        self.id = id
        self.name = name
        self.age = age
        self.city = city
        self.born = datetime.strptime(born, "%d/%m/%Y")
        self.height = height
        self.weight = weight

    def to_dict(self):
        return {
            "id": self.id,
            "name": self.name,
            "age": self.age,
            "city": self.city,
            "born": self.born.strftime("%d/%m/%Y"),
            "height": self.height,
            "weight": self.weight
        }

    @classmethod
    def from_dict(cls, data):
        return cls(
            id=data["id"],
            name=data["name"],
            age=data["age"],
            city=data["city"],
            born=data["born"],
            height=data["height"],
            weight=data["weight"]
        )


class Benchmark:
    def __init__(self):
        self.path = 'samples.json'
    
    def load_persons_from_json(self) -> List[Person]:
        start_time = time.time()
        
        with open(self.path, 'r') as file:
            data = json.load(file)
            persons = [Person.from_dict(person) for person in data]
        
        end_time = time.time()
        print(f"File loading took {end_time - start_time:.6f} seconds")
        
        return persons

    def benchmark_string_operations(self, persons: List[Person]):
        start_time = time.time()
        
        # Concatenation of names
        concatenated_names = " ".join([person.name for person in persons])
        
        # Searching for a substring in city names
        substring_count = sum("New" in person.city for person in persons)
        
        # Reversing the names
        reversed_names = [person.name[::-1] for person in persons]
        
        end_time = time.time()
        print(f"String operations took {end_time - start_time:.6f} seconds")

    def benchmark_integer_operations(self, persons: List[Person]):
        start_time = time.time()
        
        # Summing the ages
        total_age = sum(person.age for person in persons)
        
        # Finding the maximum and minimum ages
        max_age = max(person.age for person in persons)
        min_age = min(person.age for person in persons)
        
        # Counting the number of people within a certain age range
        age_range_count = sum(20 <= person.age <= 30 for person in persons)
        
        end_time = time.time()
        print(f"Integer operations took {end_time - start_time:.6f} seconds")

    def benchmark_float_operations(self, persons: List[Person]):
        start_time = time.time()
        
        # Calculating the average height and weight
        avg_height = sum(person.height for person in persons) / len(persons)
        avg_weight = sum(person.weight for person in persons) / len(persons)
        
        # Finding the maximum and minimum height and weight
        max_height = max(person.height for person in persons)
        min_height = min(person.height for person in persons)
        max_weight = max(person.weight for person in persons)
        min_weight = min(person.weight for person in persons)
        
        # Scaling the height and weight by a factor
        scaled_heights = [person.height * 1.1 for person in persons]
        scaled_weights = [person.weight * 1.1 for person in persons]
        
        end_time = time.time()
        print(f"Float operations took {end_time - start_time:.6f} seconds")
    

if __name__ == "__main__":
    benchmark = Benchmark()
    persons = benchmark.load_persons_from_json()
    
    benchmark.benchmark_string_operations(persons)
    benchmark.benchmark_integer_operations(persons)
    benchmark.benchmark_float_operations(persons)