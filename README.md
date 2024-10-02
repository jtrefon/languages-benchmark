# Benchmark Project

This project is designed to benchmark various operations in different programming languages. It includes benchmarks for JavaScript, PHP, Python, and Ruby.

## Project Structure

## Prerequisites

- Java 8 or higher
- Maven
- Python 3.12
- Node.js
- PHP
- Ruby

## Setup

1. **Clone the repository**:
    ```sh
    git clone <repository-url>
    cd performance-test
    ```

2. **Set up the Python virtual environment**:
    ```sh
    python3 -m venv venv
    source venv/bin/activate
    pip install -r requirements.txt
    ```

3. **Install Maven dependencies**:
    ```sh
    mvn install
    ```

## Running Benchmarks

### Java

To run the Java benchmark, execute the following command:
```sh
mvn compile
mvn exec:java -Dexec.mainClass="com.benchmark.Main"
```

### python
```sh
python python_benchmark.py
```

### JavaScript
```sh
node javascript_benchmark.js
```

### PHP
```sh
php php_benchmark.php
```

### Ruby
```sh
ruby ruby_benchmark.rb
```

### Generating Samples
```sh
python generate_samples.py 1000000
```