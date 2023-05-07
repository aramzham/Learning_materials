use std::{env, fs, path};
use std::collections::HashMap;
use std::hash::Hash;

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

    // 2. count the number of time each word occurs
    let map = count_words(file_result.unwrap());
    
    // 3. print a message with the top 5 most common words and how many times they appeared
    let mut vec: Vec<_> = map.iter().collect();
    vec.sort_by_key(|k| k.1);
    vec.reverse();
    for element in vec.iter().take(5) {
        if element.0.len() >= 2 && map[element.0] >= 2 {
            println!("{} occured {} times", element.0, map[element.0]);
        }
    }
}

fn count_words(input: String) -> HashMap<String, i32> {
    let mut map = HashMap::new();
    let split = input.split_whitespace();
    for s in split {
        let lowercase_word = s.to_ascii_lowercase();
        if !map.contains_key(&lowercase_word) {
            map.insert(lowercase_word, 1);
        } else {
            map.insert(lowercase_word.clone(), *map.get(&lowercase_word).unwrap() + 1);
        }
    }
    
    map
    
    // // more elegant way to do the same function
    // let mut word_counts: HashMap<&str, u32> = HashMap::new();
    // for word in all_words.iter() {
    //     *word_counts.entry(word).or_insert(0) += 1;
    // }
}
