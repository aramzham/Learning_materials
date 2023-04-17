fn main() {
    let test1 = "We need more space.";
    assert_eq!(trim_spaces(test1), "We need more space.");
    
    let test2 = "   There's space in front";
    assert_eq!(trim_spaces(&test2), "There's space in front");
    
    let test3 = "There's space to the rear. ";
    assert_eq!(trim_spaces(&test3[..]), "There's space to the rear.");

    let test4 = "   Surrounded by space...     ";
    assert_eq!(trim_spaces(&test4[..]), "Surrounded by space...");
    
    println!("tests passed!!");
}

fn trim_spaces(input: &str) -> &str {
    let mut first_letter_index = 0;
    let mut last_letter_index = input.len() - 1;

    for (index, letter) in input.chars().enumerate() {
        if letter != ' ' {
            first_letter_index = index;
            break;
        }
    }

    for (index, letter) in input.chars().rev().enumerate() {
        if letter != ' ' {
            last_letter_index = input.len() - index;
            break;
        }
    }

    &input[first_letter_index..last_letter_index]
}