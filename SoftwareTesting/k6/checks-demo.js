import http from "k6/http";
import { check, sleep } from "k6";

export default function() {
    const res = http.get('https://reqres.in/api/users?page=1');

    // all the checks will be executed no matter they were passed or not
    // on the other hand if one threshold is not met => the execution will stop
    check(res, {
        'is status 200': r => r.status === 200,
        'is not 404': r => r.status !== 404,
        'has data': r => (JSON.parse(r.body)).data.length > 0,
        'body size is less than 1030': r => r.body.length <= 1030,
        'content check': r => r.body.includes('id'),
        // other checks here
    });

    console.log(res.body);

    sleep(1);
}