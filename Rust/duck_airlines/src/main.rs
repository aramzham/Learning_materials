#![allow(unused_variables)]

fn main() {
    let unused_variable: u32 = 9; // will emit warning if not #![allow(unused_variables)]
    let arr: [f32; 2] = [1.0,2.1];
    let arr1 = [0.0; 20];
    let tuple = ("string here", 1.2, 3);
    println!("tuple contains a string: {}, a float: {} and an int: {}", tuple.0, tuple.1, tuple.2);
    // a better way is to deconstruct the tuple into variables
    let (string_value, float_value, int_value) = tuple;
    println!("deconstructed tuple contains a string: {}, a float: {} and an int: {}", string_value, float_value, int_value);

    // strings
    let string_slice = "string slice"; // &str
    let string = string_slice.to_string(); // String

    let converted_into_String = String::from("this is a &str");
    let address_of_String = &converted_into_String; // here we deal with the address of the variable and not its value, it's just a pointer
    let converted_into_slice = converted_into_String.as_str();

    // concatenation
    let duck = "Duck";
    let airlines = "Airlines";
    let duck_airlines = [duck, " ", airlines].concat();
    let formatted_duck_airlines = format!("{} {}", duck, airlines);
    println!("{}", duck_airlines);
    println!("{}", formatted_duck_airlines);

    let mut slogan = String::new();
    slogan.push_str("We hit the ground");
    slogan.push(' ');
    slogan += "every time";
    println!("{}", slogan);

    // unused variable
    let unused_variable = 0_i8; // if you comment this #![allow(unused_variables)] the compiler will emit a warning
    let _unused_variable = 1; // if you prepend _ to the unused variable the compiler will not complain about it

    // casting
    let float = 12_f32;
    let int = 10_u8;
    // let division = float / int; // no implementation for `f32 / u8`
    let division = float / int as f32; // needs explicit casting
    println!("{}", division);

    // exponents
    let squared = i32::pow(8,2);
    let float_integer = f32::powi(6.5, 3);
    let float_float = f32::powf(6.5, 3.14); // heavy computation
    println!("integer: {}", squared);
    println!("float to int: {}", float_integer);
    println!("float to float: {}", float_float);

    // enums
    println!("NDB: {}", NavigationAids::NDB as u8);
    println!("VOR: {}", NavigationAids::VOR as u8);
    println!("VORDME: {}", NavigationAids::VORDME as u8);

    // option
    const POSITION:usize = 15;
    let phrase = String::from("Duck Airlines");
    let letter = phrase.chars().nth(POSITION);
    let value = match letter {
        Some(c) => c.to_string(),
        None => String::from("no value")
    };
    println!("{}th value = {}", POSITION, value);

    // match
    let ndb_freq:u16 = 384;
    match ndb_freq { // like switch expression in C#
        200..=500 => println!("good frequency"),
        _ => println!("cannot catch this frequency")
    }
    match ndb_freq {
        ndb_freq if ndb_freq >= 200 && ndb_freq <= 500 => {
            println!("caught with if")
        },
        _ => {
            println!("in default branch")
        }
    }

    // if let
    let animal = "Duck";
    if let animal = "Duck" {
        println!("this is a duck");
    }

    // loop
    let mut counter = 0;
    loop {
        println!("{}", counter);
        counter += 1;
        if counter == 10{
            break;
        }
    }

    // for
    println!("printing from for in exclusive range loop");
    for i in 1..11 { // in 1..11 <- is not included => we'll have from 1 to 10
        println!("{}", i);
    }

    println!("printing from for in inclusive range loop");
    for i in 1..=10 { // this time 10 is inclusive
        println!("{}", i);
    }

    let colors = ["red", "blue", "orange"];
    for color in colors.iter() {
        println!("{}", color);
    }

    println!("Execution finished!");
}

enum NavigationAids {
    NDB,
    VOR,
    VORDME
}