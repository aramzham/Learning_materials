use std::io;

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
    
    println!("number + 1 = {}", input_number + 1);
}
