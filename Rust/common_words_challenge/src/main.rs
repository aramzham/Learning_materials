use std::{env, fs, path};
use std::collections::HashMap;

fn main() {
    // 1. read in a text file
    // 1a. accept path from command-line argument
    let mut args = env::args();
    if args.len() == 1 {
        println!("please specify the file name");
        return;
    }

    let filename = args.nth(1).unwrap();
    let file_path = path::Path::new(&filename);
    println!("{:?}", file_path);
    
    let file_result = fs::read_to_string(file_path);
    if let Err(e) = file_result {
        println!("{:?}", e);
        return;
    }
    
    let map = count_words(file_result.unwrap()); // 2. count the number of time each word occurs
    
    // 3. print a message with the most common words and how many times they appeared
    for key in map.keys() {
        if key.len() >= 2 && map[key] >= 2 {
            println!("{} occured {} times", key, map[key]);
        }
    }
}

fn count_words(input: String) -> HashMap<String, i32> {
    let mut map = HashMap::new();
    let split = input.split_whitespace();
    // TODO: ignore capitalization
    for s in split {
        let lowercase_word = s.to_ascii_lowercase();
        if !map.contains_key(&lowercase_word) {
            map.insert(lowercase_word, 1);
        } else {
            map.insert(lowercase_word.clone(), *map.get(&lowercase_word).unwrap() + 1);
        }
    }
    
    map
}
