// let xhr = new XMLHttpRequest();
//
// xhr.open("GET",'js/Promises/data.json');
// xhr.send();
// xhr.addEventListener('readystatechange',()=>{
//    if(xhr.readyState !==4) return;
//    if(xhr.status !== 200) throw new Error('trav');
//
//     console.log(xhr.responseText);
// });


const promise = new Promise((resolve, reject)=>{
    let xhr = new XMLHttpRequest();

    xhr.open("GET",'js/Promises/data.json');
    xhr.send();
    xhr.addEventListener('readystatechange',()=>{
        if(xhr.readyState !==4) return;
        if(xhr.status !== 200) throw new Error('trav');

        console.log(xhr.responseText);
        resolve(xhr.responseText);
    });
});

promise.then((text)=>console.log(text));
promise.catch(()=>console.log("Bad!"));