import http from "k6/http";

export default function () {
    let requests = http.batch([
        ["GET", "https://reqres.in/api/users/2"],
        ["GET", 'https://reqres.in/api/users?page=2']
    ]);

    for (let i = 0; i < requests.length; i++) {
        console.log(requests[i].status);        
    }
}