const user = new Map(); // map-er@ maqur objectner en, foreach-i jamanak property-neri hertakanutyun@ pahpanvum a
const func = () => {alert('hi')};
let obj = {name:'Vanik'};
var arr = [1,2,3];

user.set('name','Aram');
user.set(func,'my function');
user.set(obj,{value:7});
user.set(arr,[3,4,5]);

// console.log(user.get(func));
// console.log(user.get(obj));
// console.log(user.get(arr));
// key-i poisk@ arvum a ===-ov => es orinak@ chi ashxati
user.set(5,'ban');
// console.log(user.get('5')); // undefined
//
// console.log(user.size); // = length


for (let k of user.keys()){
 //   console.log(k);
}

for(let v of user.values()){
    // console.log(v);
}

for(let e of user.entries()){ // nuyn ban@ kstanayinq, vor greinq of user
 //   console.log(e);
}

var seroj = new Map([['country', 'Armenia'],['city','Yerevan']]);
console.log(seroj);

seroj.forEach((v,k,m)=>console.log(v));

seroj.delete('country');
console.log(seroj);

console.log(seroj.has('city'));
seroj.clear();
console.log(seroj);


// weakMap-@ hishoxutyan optimizaciayi hamar a, GC-n jnjum a object@, ete ira referenc@ miayn mnacel a weakMap-i mej, ete mnacac liner mapi mej, cher jnji
let weakMap = new WeakMap(); // weakMap-i key-n object a
weakMap.set({someValue: 'any'},'anything');
// weakMap-n uni set, delete, get, has
// weakMap-@ chuni clear, foreach, size