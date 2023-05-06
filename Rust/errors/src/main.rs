use std::{fs, io};

fn main() {
    // make program panic
    // make_panic();

    let result = fs::read_to_string("some_file_name.txt");
    // let contents = fs::read_to_string("ome_file_name.txt").expect("check the name of the file, file was not found"); // gracefully recover from recoverable errors
    println!("result is: {:?}", result); // returns a result enum

    let contents = match result {
        Ok(message) => message,
        Err(error) => match error.kind() {
            io::ErrorKind::NotFound => String::from("file not found"),
            io::ErrorKind::PermissionDenied => String::from("not enough permissions"),
            _ => panic!("unhandled error: {:?}", error)
        }
    };

    println!("contents is: {:?}", contents);
    
    let read_and_combine_result = read_and_combine("file1.txt", "file2");
    match read_and_combine_result {
        Ok(s) => println!("result is...\n{}", s),
        Err(e) => println!("There was an error: {}", e)
    };
}

fn make_panic() {
    let countdown = [4, 3, 2, 1, 0];

    for i in countdown {
        println!("number is {}", i);

        let division = 1 / i;
    }
    // to ensure having good stack trace from the error set $ENV:RUST:BACKTRACE=1
}

fn read_and_combine(f1: &str, f2: &str) -> Result<String, io::Error> {
    let mut s1 = fs::read_to_string(f1)?; // this is the same as what is done with s2 below
    let s2 = match fs::read_to_string(f2) {
        Ok(s) => s,
        Err(e) => return Err(e)
    };
    s1.push('\n');
    s1.push_str(&s2);
    Ok(s1)
}