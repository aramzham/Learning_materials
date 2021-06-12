function getSum() {
    // forOf-@ iterable objectneri vrov a frum, arguments-n el a iterable, chnayac, vor array chi
    // string-i vrov el a frum
    for(let value of arguments){
        console.log(value);
    }
}

console.log(getSum(1, 23, 3));