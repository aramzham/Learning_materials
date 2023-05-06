struct Waypoint { // the name needs to be in pascal case
    name: String,
    latitude: f64,
    longitude: f64
}

struct Segment { // this is the definition
    start: Waypoint,
    end: Waypoint
}

impl Segment { // this is the implementation
    fn new(start: Waypoint, end: Waypoint) -> Self { // constructor
        Self {
            start, // this is shorthand for start: start
            end
        }
    }

    fn distance(&self) -> f32 { // &self gives access to the fields of the associated struct
        const EARTH_RADIUS_IN_KILOMETERS: f64 = 6371.0;
        (EARTH_RADIUS_IN_KILOMETERS / (self.start.latitude + self.end.longitude)) as f32
    }
}

fn main() {
    let kcle = Waypoint { // all members of a struct must be initialized
        name: "KCLE".to_string(),
        latitude: 41.4075,
        longitude: -81.851111
    };

    let kslc = Waypoint {
        name: "KSLC".to_string(), // this will override the value brought from ..kcle
        ..kcle // this will bring all the values from the variable
    };

    let kcle_to_kslc = Segment::new(kcle, kslc);
    let distance = kcle_to_kslc.distance(); // we use dot notation when we have a method with &self parameter = instance method
    println!("{:.2}", distance);

    // traits
    let boeing = Boeing {
        required_crew: 4,
        range: 7370
    };

    let airbus = Airbus {
        required_crew: 7,
        range: 5280
    };

    let boeing_is_legal = boeing.is_legal(boeing.required_crew, 18, boeing.range, 2385);
    let airbus_is_legal = airbus.is_legal(airbus.required_crew, 3, boeing.range, 2200);
    println!("Is the Boeing flight legal? {}\nIs the Airbus flight legal? {}", boeing_is_legal, airbus_is_legal);
}

struct Boeing {
    required_crew: u8,
    range: u16
}

struct Airbus {
    required_crew: u8,
    range: u16
}

trait Flight {
    fn is_legal(&self, required_crew: u8, available_crew: u8, range: u16, distance: u16) -> bool;
}

impl Flight for Boeing {
    fn is_legal(&self, required_crew: u8, available_crew: u8, range: u16, distance: u16) -> bool {
        available_crew >= required_crew && range + 150 >= distance
    }
}

impl Flight for Airbus {
    fn is_legal(&self, required_crew: u8, available_crew: u8, range: u16, distance: u16) -> bool {
        available_crew >= required_crew && range + 280 >= distance
    }
}