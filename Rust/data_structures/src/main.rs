use std::collections::HashMap;

fn main() {
    // vectors
    let mut astronauts: Vec<String> = Vec::new();
    astronauts.push(String::from("Shepard")); // Alan Shepard
    astronauts.push(String::from("Grissom")); // Gus Grissom
    astronauts.push(String::from("Glenn")); // John Glenn
    println!("astronauts is {:?}", astronauts);
    
    let last = astronauts.pop();
    println!("last is {:?}", last);
    
    // let third = &astronauts[2];
    let third = astronauts.get(2);
    println!("third is {:?}", third);
    
    // hash maps
    let mut missions_flown = HashMap::new(); // missions flown as of 1.1.21
    missions_flown.insert("Hadfield", 3); // Chris Hadfield
    missions_flown.insert("Hurley", 3); // Doug Hurley
    missions_flown.insert("Barron", 0); // Kayla Barron
    println!("missions_flown is {:?}", missions_flown);

    missions_flown.insert("Barron", 1); // overwrite
    missions_flown.entry("Stone").or_insert(2);
    let kayla = missions_flown.entry("Barron").or_insert(0);
    *kayla += 1;
    let barron_missions = missions_flown.get("Barron");
    println!("barron_missions is {:?}", barron_missions);
}
