require 'json'
require 'time'

class Person
  attr_accessor :id, :name, :age, :city, :born, :height, :weight

  def initialize(id, name, age, city, born, height, weight)
    @id = id
    @name = name
    @age = age
    @city = city
    @born = Time.strptime(born, '%d/%m/%Y')
    @height = height
    @weight = weight
  end

  def self.from_hash(data)
    new(data['id'], data['name'], data['age'], data['city'], data['born'], data['height'], data['weight'])
  end
end

class Benchmark
  def initialize
    @path = 'samples.json'
  end

  def load_persons_from_json
    start_time = Time.now

    data = JSON.parse(File.read(@path))
    persons = data.map { |person| Person.from_hash(person) }

    end_time = Time.now
    puts "File loading took #{end_time - start_time} seconds"

    persons
  end

  def benchmark_string_operations(persons)
    start_time = Time.now

    # Concatenation of names
    concatenated_names = persons.map(&:name).join(' ')

    # Searching for a substring in city names
    substring_count = persons.count { |person| person.city.include?('New') }

    # Reversing the names
    reversed_names = persons.map { |person| person.name.reverse }

    end_time = Time.now
    puts "String operations took #{end_time - start_time} seconds"
  end

  def benchmark_integer_operations(persons)
    start_time = Time.now

    # Summing the ages
    total_age = persons.sum(&:age)

    # Finding the maximum and minimum ages
    ages = persons.map(&:age)
    max_age = ages.max
    min_age = ages.min

    # Counting the number of people within a certain age range
    age_range_count = persons.count { |person| person.age >= 20 && person.age <= 30 }

    end_time = Time.now
    puts "Integer operations took #{end_time - start_time} seconds"
  end

  def benchmark_float_operations(persons)
    start_time = Time.now

    # Calculating the average height and weight
    total_height = persons.sum(&:height)
    total_weight = persons.sum(&:weight)
    avg_height = total_height / persons.size
    avg_weight = total_weight / persons.size

    # Finding the maximum and minimum height and weight
    heights = persons.map(&:height)
    weights = persons.map(&:weight)
    max_height = heights.max
    min_height = heights.min
    max_weight = weights.max
    min_weight = weights.min

    # Scaling the height and weight by a factor
    scaled_heights = heights.map { |height| height * 1.1 }
    scaled_weights = weights.map { |weight| weight * 1.1 }

    end_time = Time.now
    puts "Float operations took #{end_time - start_time} seconds"
  end
end

# Example usage
benchmark = Benchmark.new
persons = benchmark.load_persons_from_json

benchmark.benchmark_string_operations(persons)
benchmark.benchmark_integer_operations(persons)
benchmark.benchmark_float_operations(persons)