fn main() {
    let test1 = "We need more space.";
    assert_eq!(trim_spaces(test1), "We need more space.");

    let test2 = "   There's space in front";
    assert_eq!(trim_spaces(&test2), "There's space in front");

    let test3 = "There's space to the rear. ";
    assert_eq!(trim_spaces(&test3[..]), "There's space to the rear.");

    let test4 = "   Surrounded by space...     ";
    assert_eq!(trim_spaces(&test4[..]), "Surrounded by space...");

    let test5 = "\n\r starting with whitespaces";
    assert_eq!(trim_spaces(test5), "starting with whitespaces");
    
    let test6 = "\t   whitespaces everywhere \t\n\r   ";
    assert_eq!(trim_spaces(test6), "whitespaces everywhere");
    
    println!("tests passed!!");
}

fn trim_spaces(input: &str) -> &str {
    let mut first_letter_index = 0;
    let mut last_letter_index = input.len() - 1;
    let whitespaces = [' ', '\t', '\n', '\r'];

    for (index, letter) in input.chars().enumerate() {
        if whitespaces.iter().all(|&char| char != letter) {
            first_letter_index = index;
            break;
        }
    }

    for (index, letter) in input.chars().rev().enumerate() {
        if whitespaces.iter().all(|&char| char != letter) {
            last_letter_index = input.len() - index;
            break;
        }
    }

    &input[first_letter_index..last_letter_index]
}