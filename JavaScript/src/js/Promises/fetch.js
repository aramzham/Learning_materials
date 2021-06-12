fetch('js/Promises/data.json')
    .then(response=>response.json()) // .json() returns a promise
    .then(response=> console.log(response))
    .catch(error=> console.log(error));

Promise.all([
    fetch('url'),
    fetch('url')
])
.then((data)=>console.log(data))
.catch((error)=>console.log(error));