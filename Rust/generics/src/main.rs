fn main() {
    let _condition = true;
    let a = if _condition {1} else { 2 };

    assert_eq!(a, 1);

    let mut count = 0f32;
    let result = loop {
        if count == 10.0 {
            break count / 10.0;
        }
        count += 1.0;
    };

    assert_eq!(result, 1.0);

    let mut matrix = [[1,2,3],
                                 [4,5,6]];

    for row in matrix.iter_mut() {
        for item in row.iter_mut() {
            *item *= 10;
        }
    }

    for row in matrix.iter() {
        for item in row.iter() {
            print!("{}\t", item);
        }
        print!("\n");
    }
}


