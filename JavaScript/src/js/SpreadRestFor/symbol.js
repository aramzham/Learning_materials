// symbol@ primitiv tip a inti, stringi pes

let id = Symbol();

const user = {
    name: 'Dima',
    age:19,
    color: 'yellow',
    [id]:'my immutable id'
};

console.log(user);

for(let key in user){
    // symbol()-@ chi masnakcum iteration-in
    console.log(user[key]);
}

// esi id nor dasht ksarqi
//user.id = 'new id';
console.log(user[id]);

// const first = Symbol('aram');
// const second = Symbol('aram');

const first = Symbol.for('aram');
const second = Symbol.for('aram');

console.log(first===second); // talu a false, qani vor symbol-ner@ unikal en, Symbol.for-ov kta true
console.log(Symbol.keyFor(second));