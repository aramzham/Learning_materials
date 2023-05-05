use std::fmt::{Display, Formatter};

fn main() {
    let my_shape = Shape::Rectangle(2.3, 5.3);
    println!("my_shape is {:?}", my_shape);

    match my_shape {
        Shape::Circle(r) => println!("this is a circle with radius {}", r),
        Shape::Rectangle(w, h) => println!("{} x {} rectangle", w, h),
        Shape::Triangle(a, b, c) => println!("triangel with sides {}, {}, {}", a, b, c)
    } // this is an expression => no ; needed

    let my_number = 1u8;
    let result = match my_number {
        0 => "zero", // expressions are evaluated sequentially => include wildcard pattern at the end
        1 => "one",
        2 => "two",
        3 => "three",
        _ => {
            println!("number didn't match: {}", my_number);
            "something else"
        } // default case, will execute when all other case fail
    };

    println!("result is {}", result);

    let perimeter = my_shape.get_perimeter();
    println!("pertimier = {}", perimeter);

    let countdown = [4, 3, 2, 1];
    let number = countdown.get(4);
    let number = number.unwrap_or(&0) + 1;
    println!("number is {}", number);

    let number_2 = Some(324);
    if let Some(324) = number_2 { // this is a shorthand notation of a case where in match statement we care about only Some branch
        println!("number 2 has value")
    }
    
    // challenge
    let address = Location::Unknown;
    address.display();
    println!("{}", address);
    let address = Location::Anonymous;
    address.display();
    println!("{}", address);
    let address = Location::Known(233.34234, -80.3241);
    address.display();
    println!("{}", address);
}

enum Location {
    Unknown,
    Anonymous,
    Known(f64, f64) // latitude, longitude
}

impl Location {
    fn display(&self) {
        match *self {
            Location::Anonymous => println!("this is an anonymous location"),
            Location::Unknown => println!("we don't know anything about the location"),
            Location::Known(lat, lon) => println!("latitude = {}, longitude = {}", lat, lon)
        }
    }
}

impl Display for Location {
    fn fmt(&self, f: &mut Formatter<'_>) -> std::fmt::Result {
        match *self {
            Location::Anonymous => write!(f, "this is an anonymous location"),
            Location::Unknown => write!(f, "we don't know anything about the location"),
            Location::Known(lat, lon) => write!(f, "latitude = {}, longitude = {}", lat, lon)
        }
    }
}

#[derive(Debug)]
enum Shape {
    // shape represents one of the possible types
    Circle(f64),
    Rectangle(f64, f64),
    Triangle(f64, f64, f64),
}

impl Shape {
    fn get_perimeter(&self) -> f64 {
        match *self {
            Shape::Circle(r) => r * 2.0 * std::f64::consts::PI,
            Shape::Rectangle(w, h) => 2.0 * (w + h),
            Shape::Triangle(a, b, c) => a + b + c
        }
    }
}