// function getSum() {
//     // arguments is a pseudoarray, a collection
//     let newArray  = Array.prototype.slice.call(arguments,0);
//     // newArray.push('new value');
//     //
//     // console.log(newArray);
//
//     return newArray.reduce(function (res,item) {
//         return res+item;
//     });
// }

// ...args-ov arguments-@ darnum a massiv liarjeq
function getSum(a, b, ...args) {
    // console.log(args);
    // args.push('new value');
    // console.log(args);
    let sum = 0;
    for(let i = 0; i<args.length;i++){
        sum+=args[i];
    }
    return sum+a+b;
}

console.log(getSum(1, 2, 3,100,10000));

// / function getSum() {
//     console.log(arguments);
// }
//
// console.log(getSum(1, 2,'aram'));

export default getSum;