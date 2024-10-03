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
- C++

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

## Running Benchmarks

### Java

To run the Java benchmark, execute the following command:
```sh
mvn install
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

### C++
```sh
brew install jsoncpp
g++ -std=c++11 -o cpp_benchmark cpp_benchmark.cpp -I/usr/local/include -L/usr/local/lib -ljsoncpp
./cpp_benchmark
```

### Go
```sh
go run go_benchmark.go
```

### Generating Samples
```sh
python generate_samples.py 1000000
```
