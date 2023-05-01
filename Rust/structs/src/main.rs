fn main() {
    let mut vehicle = Shuttle {
        name: String::from("Mir"),
        crew_size: 7,
        propellant: 234948.1,
    };

    vehicle.name = String::from("Vostok");

    println!("name is {}", vehicle.name);

    println!("vehicle is {:?}", vehicle);

    let vehicle2 = Shuttle {
        name: String::from("Discovery"),
        ..vehicle // means that other fields not defined here should be taken from the other variable
    };

    println!("vehicle2 is {:?}", vehicle2);

    let mut vehicle3 = Shuttle {
        ..vehicle.clone()
    };

    println!("vehicle3 is {:?}", vehicle3);

    let vehicle3_name = vehicle3.get_name();
    println!("vehicle3 name = {}", vehicle3_name);

    println!("propellant is {}", vehicle3.propellant);
    vehicle3.add_fuel(1000.0);
    println!("propellant is {}", vehicle3.propellant);

    let vehicle4 = Shuttle::new("Endeavour");
    println!("vehicle4 = {:?}", vehicle4);

    let color = Color(10, 20, 30);
    println!("first color = {}", color.0);
    
    // RECTANGLE challenge
    let mut rect = Rectangle::new(1.2, 3.4);
    assert_eq!(rect.get_area(), 4.08);
    rect.scale(0.5);
    assert_eq!(rect.get_area(), 1.02);
    println!("Tests passed!");
}

#[derive(Debug)]
#[derive(Clone)]
struct Shuttle {
    name: String,
    crew_size: u8,
    propellant: f64,
}

impl Shuttle {
    fn get_name(&self) -> &str { // self is not mutable because we are retrieving data
        &self.name
    }

    fn add_fuel(&mut self, gallons: f64) {
        self.propellant += gallons;
    }

    // associated method
    fn new(name: &str) -> Shuttle {
        Shuttle {
            name: String::from(name),
            crew_size: 7,
            propellant: 0.0,
        }
    }
}

struct Color(u8, u8, u8); // tuple struct representing RGB

struct Rectangle {
    height: f64,
    width: f64
}

impl Rectangle {
    fn get_area(&self) -> f64 {
        &self.height * &self.width
    }
    
    fn scale(&mut self, multiplier: f64) {
        self.width *= multiplier;
        self.height *= multiplier;
    }
    
    fn new(width: f64, height: f64) -> Rectangle {
        Rectangle {
            width,
            height
        }
    }
}