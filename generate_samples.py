import json
import random
import sys
from faker import Faker

fake = Faker()

def generate_sample(id):
    return {
        "id": id,
        "name": fake.name(),
        "age": random.randint(18, 80),
        "city": fake.city(),
        "born": fake.date_of_birth().strftime("%d/%m/%Y"),
        "height": round(random.uniform(1.5, 2.0), 5),
        "weight": round(random.uniform(50, 100), 3)
    }
def main():
    if len(sys.argv) != 2:
        print("Usage: python generate_samples.py <number_of_samples>")
        sys.exit(1)

    try:
        num_samples = int(sys.argv[1])
    except ValueError:
        print("Please provide a valid integer for the number of samples.")
        sys.exit(1)

    samples = [generate_sample(i) for i in range(1, num_samples + 1)]

    with open('samples.json', 'w') as f:
        json.dump(samples, f, indent=4)

if __name__ == "__main__":
    main()