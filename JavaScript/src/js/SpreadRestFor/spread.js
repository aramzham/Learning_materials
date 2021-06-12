//import getsum from './rest';

const firstUser = {
    name:'Dima',
    age:28,
    width: 23
};

const data = {
  color:'yellow',
  goal:[],
  width:40
};

const result = {
    name:'Dima',
    age:28,
    width: 500,
    color:'yellow',
    goal:[],
};

//const resultObj = Object.assign(firstUser,data);
const resultObj = {
    // bacazatum
    ...firstUser,
    ...data,
    newValue: 'nor arjeq',
    another: 'nor hajn'
};

// console.log(resultObj);

const firstArray = [1,23,3];
const secondArray = [2,4,6];

//const resultArray = firstArray.concat(secondArray);
const resultArray = [
    ...firstArray,
    ...secondArray,
    'something'
];
console.log(resultArray);
