function getName(name = 'Aram') {
    return name;
}

console.log(getName());
console.log(getName('Vanik'));

function getSum(a,b) {
    a= a|| 0;
    b=b||0; // ete getSum-@ kancheluc b-in arjeq chen talis, b-n darnum a undefined => false => b-n kvercni 0 arjeq@
    return a+b;
}

console.log(getSum());
// taza greladzevum karanq default arjeqner tanq

// let summa = (x,y)=>{
//     return x+y;
// };
let summa = (x,y)=>x+y;
console.log(summa(2, 3));

const numbers = [1,2,3,4,5];
// let res = numbers.reduce((result, item)=>{return result*item;});
let res = numbers.reduce((result, item)=>result*item);
setTimeout(()=>console.log('hi'),1000);
console.log(res);
// lambda-ner@ chunen arguments u this

const obj= {
    name:'Aram',
    getName: function() {
        var me = this; // nayev that a ogtagorcvum me-i poxaren
        function getFullName(){
            console.log(me);
            return me.name + ' Hayki';
        }
        return getFullName();
    }
};

console.log(obj.getName());

const obj2 = {
    name:'Arman',
    getName:function () {
        let getFullName = ()=>{
            console.log(this);
            // lambdan this chuni => kariq chka me-i kam that-i, this@ vercnelu a ira cnoxi object@
            return `${this.name} Hayki`;
        }
        return getFullName();
    }
};
console.log(obj2.getName());