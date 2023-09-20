import http from "k6/http";
import { check } from "k6";

export let options = {
    batch: 5, // default 20
    batchPerHost: 5 // default 6
};

export default function(){
    let r1 = {
        method: "GET",
        url: "https://reqres.in/api/users?page=10"
    };
    let r2 = {
        method: "GET",
        url: "https://test-api.k6.io/public/crocodiles/"
    };

    let res = http.batch([r1, r2]);

    check(res[0], { 'reqres was 200': (r) => r.status === 200 });
    check(res[1], { 'test crocodiles was 200': (r) => r.status === 200 });

    let namedRequests = {
        'Boutique': "https://onlineboutique.dev/",
        'test api': {
            method: "GET",
            url: "https://test-api.k6.io/"
        }
    };

    let namedResponses = http.batch(namedRequests);
    console.log("Online boutique response " + namedResponses['Boutique'].status);
    console.log("test api response " + namedResponses['test api'].status);
}