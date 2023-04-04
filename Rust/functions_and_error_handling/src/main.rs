use std::fs::File;
use std::io::{Error, ErrorKind, Read};

fn main() {
    println!("{}", return_greater(1,2));

    let mut original = String::from("original value");
    println!("\nouter scope original: \t\"{}\"", original);

    {
        print_original(&original);
        change_original(&mut original);
        println!("inner scope original: \t\"{}\"", original);
    }


    // closure
    let name ="Duck Airlines";
    let write_message = |slogan:String|->String{
        String::from(format!("{}. {}", name, slogan))
    };

    let phrase = write_message(String::from("We hit the ground every time"));
    println!("{}", phrase);


    // error handling
    let filename = "C:\\Users\\Aram\\Desktop\\[Pluralsight Edward Curren] Rust Fundamentals [2022, ENG] [rutracker-6273290].torrent";
    match File::open(filename){
        Ok(file) =>{
            println!("{:#?}", file);
        }
        Err(error) => {
            match error.kind(){
                ErrorKind::NotFound => {
                    match File::create(filename){
                        Ok(_)=>{
                            println!("file created!");
                        }
                        Err(error) => {
                            println!("{:#?}", error);
                        }
                    }
                }
                _ => {
                    println!("{:#?}", error);
                }
            }
        }
    }

    let file_name = "C:\\Users\\Aram\\source\\repos\\Learning_materials\\Rust\\Fundamentals.txt";
    let file_data = read_file(file_name);
    match file_data {
        Ok(data) => {
            println!("{}", data);
        }
        Err(_) => {println!("something went wrong...")}
    }
}

fn return_greater(first:i32, second:i32) -> i32{
    if first > second {
        first // drop return keyword and drop the semicolon
    }
    else {
        second
    }
}

fn print_original(original: &String){
    println!("fn print_original: \t\"{}\"", original);
}

fn change_original(original: &mut String){
    let next = original;
    *next = String::from("next value");
    println!("fn change_original: \t\"{}\"", next);
}

fn read_file(filename: &str) -> Result<String, Error>{
    let mut file_handle = File::open(filename)?; // ? means that it will throw error if the function fails
    let mut file_data = String::new();
    let _ = file_handle.read_to_string(&mut file_data);
    Ok(file_data)
}
