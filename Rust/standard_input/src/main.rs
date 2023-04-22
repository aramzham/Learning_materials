use std::io;
use rand::prelude::*;

fn main() {
    let mut buffer = String::new();
    let mut parse_value;
    let input_number = loop {
        println!("enter a number");
        io::stdin().read_line(&mut buffer);
        println!("buffer is {}", buffer);
        parse_value = buffer.trim().parse::<i32>();
        match parse_value {
            Ok(v) => {
                break v;
            }
            Err(e) => {
                println!("{}", e);
                buffer = String::new();
            }
        }
    };

    let random_number = random::<i32>();
    println!("random 1 = {}", random_number);
    let random_number = thread_rng().gen_range(1..11); // from 1 to 10
    println!("random 2 = {}", random_number);
    println!("number + randoms = {}", input_number + random_number);
}
