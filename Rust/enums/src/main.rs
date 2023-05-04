fn main() {
    let my_shape = Shape::Rectangle(2.3, 5.3);
    println!("my_shape is {:?}", my_shape);
    
    match my_shape {
        Shape::Circle(r) => println!("this is a circle with radius {}", r),
        Shape::Rectangle(w, h) => println!("{} x {} rectangle", w, h),
        Shape::Triangle(a,b,c) => println!("triangel with sides {}, {}, {}", a, b, c)
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
        }, // default case, will execute when all other case fail
    };
    
    println!("result is {}", result);
    
    let perimeter = my_shape.get_perimeter();
    println!("pertimier = {}", perimeter);
}

#[derive(Debug)]
enum Shape { // shape represents one of the possible types
    Circle(f64),
    Rectangle(f64, f64),
    Triangle(f64, f64, f64)
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