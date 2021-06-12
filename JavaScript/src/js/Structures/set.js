var arr = [1,1,1]; // normal a
var food = new Set(); // chka krknutyun

food.add('Pan');
food.add('Te');
food.add('Frutas');
food.add('Frutas');
food.add('Frutas'); // mi hatn a toxum

food.delete('Te');
console.log(food.has('Te'));
//food.clear();

console.log(food);

food.forEach(x=>console.log(x));

for(let item of food){
    console.log(item);
}

// jnjel krknutyunner@

const data = [1,2,1,2,2];
var s = new Set(data);

console.log(s);
console.log(s.size);

const weakSet = new WeakSet();
weakSet.add({name:'Aram'}); // menak object-ner