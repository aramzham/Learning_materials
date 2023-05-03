use std::{any, fmt};
use std::cmp::Ordering;
use std::cmp::Ordering::Equal;
use std::fmt::{Display, Formatter, write};

fn main() {
    let hubble = Satellite {
        name: String::from("Hubble Telescope"),
        velocity: 4.64,
    };

    let iss = SpaceStation {
        name: String::from("International space station"),
        crew_size: 6,
        altitude: 254,
    };

    println!("hubble is {}", hubble.describe());
    println!("iss is {}", iss.describe());

    print_type(23);
    print_type(23.0);
    print_type("twenty three");
    print_type([32]);

    compare_and_print(1.0, 1);
    compare_and_print(1.0, 2);
    
    println!("output is {}", get_displayable(true));
    
    println!("{}", hubble);
    
    let gps = Satellite {
        name: String::from("GPS"),
        velocity: 22.3
    };

    println!("is hubble == gps? {}", hubble == gps);
    println!("is hubble > gps? {}", hubble > gps);
    println!("is hubble >= gps? {}", hubble >= gps);
    println!("is hubble < gps? {}", hubble < gps);
    println!("is hubble <= gps? {}", hubble <= gps);
}

fn get_displayable(choice: bool) -> impl fmt::Display {
    // error[E0308]: `if` and `else` have incompatible types
    if choice {
        31
    } else { 
        21 // "twenty one"
    }
}

// fn compare_and_print<T: fmt::Display + PartialEq + From<U>, U: fmt::Display + PartialEq + Copy>(a: T, b: U){
fn compare_and_print<T, U>(a: T, b: U)
    where T: fmt::Display + PartialEq + From<U>, // these are called trait bounds
          U: fmt::Display + PartialEq + Copy {
    if a == T::from(b) {
        println!("{} = {}", a, b);
    } else {
        println!("{} != {}", a, b);
    }
}

fn print_type<T: fmt::Debug>(item: T) {
    println!("{:?} is {}", item, any::type_name::<T>())
}

struct Satellite {
    name: String,
    velocity: f64, // miles per second
}

struct SpaceStation {
    name: String,
    crew_size: u8,
    altitude: u32, // miles
}

// // in case a default implementation is defined and it's not overridden in the struct => default impl will be called
// trait Description {
//     fn describe(&self) -> String{
//         String::from("this is an object flying in space")
//     }
// }

trait Description {
    fn describe(&self) -> String;
}

impl Description for Satellite {
    fn describe(&self) -> String {
        format!("the {} flying at {} miles per second!", self.name, self.velocity)
    }
}

impl Display for Satellite {
    fn fmt(&self, f: &mut Formatter<'_>) -> fmt::Result {
        write!(f, "name is {}, velo is {}", self.name, self.velocity)
    }
}

impl PartialEq<Self> for Satellite {
    fn eq(&self, other: &Self) -> bool {
        self.velocity == other.velocity
    }
}

impl PartialOrd for Satellite {
    fn partial_cmp(&self, other: &Self) -> Option<Ordering> {
        if self.velocity == other.velocity {
            Some(Ordering::Equal)
        } else if self.velocity > other.velocity {
            Some(Ordering::Greater)
        } else {
            Some(Ordering::Less)
        }
    }
}

impl Description for SpaceStation {
    fn describe(&self) -> String {
        format!("the {} flying {} miles high with {} crew members abroad", self.name, self.altitude, self.crew_size)
    }
}