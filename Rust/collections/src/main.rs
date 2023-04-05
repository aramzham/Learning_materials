use std::collections::{HashMap, VecDeque, HashSet};

fn main() {
    // vectors
    let mut flights:Vec<&str> = Vec::new(); // it doesn't really make sense to declare a vector immutable because it's main purpose is to add and remove items
    let vec_macro = vec![1,2,3,4]; // create a vector from array

    flights.push("to Paris");
    flights.push("to Yerevan");
    flights.push("to Vienna");

    for flight in flights.iter() {
        println!("{}", flight);
    }

    // there are 2 options to get an element from a collection
    // 1. indexer
    let yerevan_flight = flights[1]; // this is O(1) fast but there is a chance of panic if you go out of bounds
    println!("flight at index 1 is '{}'", yerevan_flight);
    // 2. get method
    let vienna_flight = flights.get(2); // this method will iterate through the collection and return an option (is a bit slower than the indexer but is safer)
    if let Some(flight_value) = vienna_flight {
        println!("{}", flight_value);
    }

    flights.insert(1, "from Yerevan"); // index of the element after which to insert
    flights.remove(1);

    for flight in flights.iter() {
        println!("{}", flight);
    }

    // vecDeque
    let mut flights_queue:VecDeque<&str> = VecDeque::new();

    flights_queue.push_front("Gyumri");
    flights_queue.push_back("Vanadzor");
    flights_queue.push_back("Meghri");
    flights_queue.push_front("Kapan");

    for flight in flights_queue.iter() {
        println!("{}", flight);
    }

    let exists = flights_queue.contains(&"Kapan"); // contains looks for an exact match
    println!("Kapan exists? - {}", exists);

    let length = flights_queue.len();
    println!("queue length = {}", length);
    flights_queue.clear();

    // hash map
    let mut map = HashMap::new();
    map.insert("Paris", ("7 euros", "Kebab"));
    map.insert("Yerevan", ("650 drams", "Zavik"));
    map.insert("Vienna", ("4.9 euros", "Wurst mit kase"));
    map.insert("Gyumri", ("1400 drams", "Qyalagyosh"));
    map.insert("Budapest", ("2000 florins", "Gulyash"));

    let city_name = "Gyumri";

    if !map.contains_key(city_name){
        map.insert("Gyumri", ("1400 drams", "Qyalagyosh"));
    } else {
        println!("you already have your city '{}' in the list", city_name);
    }

    // set
    let mut set = HashSet::new(); // it internally stores data as a map but behaves like a sequence though not ordered

    set.insert("a");
    set.insert("b");
    set.insert("a");

    println!("len() = {}", set.len());

    let contains = set.contains("c");
    if set.contains("c") {
        println!("set contains 'c'");
    }
}
