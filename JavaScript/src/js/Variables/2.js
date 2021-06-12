// let age = 25;
//
// let age = 17;
//
// console.log(age);

// if(1>0){
//     let age = 88;
//     console.log(age);
// }
//
// console.log(age); // is not defined here with let


for(var i = 0; i < 10; i++){
    setTimeout(function () {
        console.log(i);
    }, 500)
}
//vs
for(let i = 0; i < 10; i++){
    setTimeout(function () {
        console.log(i);
    }, 500)
}
