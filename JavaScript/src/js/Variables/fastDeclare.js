let options = {
    name: 'Aram',
    age:28,
    width: 200
};

// function render(option) {
//     // var {name} = option;
//     // var {age} = option;
//     // var {width} =option;
//
//     var {name,age,width} = option;
//
//     console.log(name, age, width);
// }

function render({name, age, width}) {
    console.log(name, age, width);
}

render(options);

const data = [1,3,4];

/*const price = data[0];
const count = data[1];
const weight = data[2];*/

const [price, count, weight] = data;

console.log(price,count, weight);

const sergey = {
  name:'Sergey',
  age:28,
  sound:500
};

const {name: anotherName, age, sound: jacuzzi} =  sergey;
console.log(anotherName, age, jacuzzi);