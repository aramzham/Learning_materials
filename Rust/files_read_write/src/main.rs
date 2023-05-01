use std::io::prelude::*;
use std::{env, fs, io, path};

fn main() {
    let path_to_file = path::Path::new("temporary_file_for_test");
    // write
    fs::write(path_to_file, "hello from Rust\nmy name is Rusty");
    // append
    let mut file = fs::OpenOptions::new()
        .append(true)
        .open(path_to_file)
        .unwrap();
    file.write(b"\nnew line text here"); // make string as bytes

    let mut buffer = String::new();
    println!(
        "Would you like to delete the created file {:?}? Y/N",
        path_to_file.file_name().unwrap()
    );
    io::stdin().read_line(&mut buffer);
    if buffer == "Y" {
        fs::remove_file(path_to_file);
    }

    let arg1 = env::args().nth(1).unwrap();
    let arg2 = env::args().nth(2).unwrap();

    let people_file = path::Path::new(&arg1);
    let content = fs::read_to_string(people_file).unwrap();
    let contains_name = content.lines().any(|x| x == arg2);
    println!("Does file contain the specified name? {}", contains_name);
}