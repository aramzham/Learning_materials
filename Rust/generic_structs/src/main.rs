use std::{io, mem};
use std::ops::Add;

fn main(){
    let rectangle = Rectangle {
        width: 1u8,
        height: 2u8
    };
    
    println!("width = {}", rectangle.get_width());
    println!("perimeter = {}", rectangle.get_perimeter());
    
    // let mut user_input = String::new();
    // println!("enter first number");
    // io::stdin().read_line(&mut user_input);
    // let first_number: i32 = user_input.trim().parse::<i32>().unwrap();
    // println!("enter second number");
    // user_input = String::new();
    // io::stdin().read_line(&mut user_input);
    // let second_number: i32 = user_input.trim().parse::<i32>().unwrap();
    // println!("biggest number is {}", get_biggest(first_number, second_number));
    
    let vehicle = Shuttle {
        name: String::from("Atlantis"),
        crew_size: 7,
        propellant: 34852.9
    };
    
    println!("vehicle size on stack: {} bytes", mem::size_of_val(&vehicle));
    
    let boxed_vehicle = Box::new(vehicle);
    println!("boxed_vehicle size on stack: {}b", mem::size_of_val(&boxed_vehicle));
    println!("boxed_vehicle size on heap: {}b", mem::size_of_val(&*boxed_vehicle)); // * is needed to access the object that is referenced by &
    
    let unboxed_vehicle: Shuttle = *boxed_vehicle;
    println!("unboxed_vehicle size on stack: {}b", mem::size_of_val(&unboxed_vehicle));
    
    // challenge
    let one = Box::new(1);
    let two = Box::new(2);
    assert_eq!(*sum_boxes(one, two), 3);

    let pi = Box::new(3.14159);
    let e = Box::new(2.71828);
    assert_eq!(*sum_boxes(pi, e), 5.85987);

    let left = Box::new(29);
    let right = Box::new(2384);
    assert_eq!(equal_boxes(left, right), -1);

    let left = Box::new(45);
    let right = Box::new(12);
    assert_eq!(equal_boxes(left, right), 1);

    let left = Box::new(498439);
    let right = Box::new(498439);
    assert_eq!(equal_boxes(left, right), 0);
    
    println!("Tests passed!!");
}

fn sum_boxes<T: Add<Output = T>>(p1: Box<T>, p2: Box<T>) -> Box<T> {
    Box::new(*p1 + *p2)
}

fn equal_boxes<T: PartialOrd>(p1: Box<T>, p2: Box<T>) -> i32 {
    if *p1 > *p2 {
        1
    } else if *p1 == *p2 {
        0
    } else {
        -1
    }
}

struct Shuttle { // lives on the stack
    name: String, // lives on the heap
    crew_size: u8,
    propellant: f64
}

fn get_biggest<T: PartialOrd>(a: T, b: T) -> T {
    if a > b {
        a
    } else {
        b
    }
}

#[derive(Debug)]
struct Rectangle<T, U> {
    width: T,
    height: U
}

// if we don't specify <T, U> after impl then we assume that the implementation is
// for a concrete type of a Rectangle
impl<T, U> Rectangle<T, U> {
    fn get_width(&self) -> &T {
        &self.width
    }
}

impl Rectangle<u8, u8> {
    fn get_perimeter(&self) -> u8 {
        2 * (&self.width + &self.height)
    }
}