import http from 'k6/http';
import {check, sleep} from 'k6';

export let options = {
    vus: 5,
    duration: "5s",
    thresholds: {
        "http_req_duration": [{
            threshold: "p(95)<200" // 95% of requests must be under 200ms response time
        }],
        "checks": [{
            threshold: "rate>0.7" // 70% of checks must pass
        }]
    }
}

export default function() {
    let result = http.get('https://reqres.in/api/users?page=2');

    check(result, {
        "Status is 200": (r) => r.status === 200,
        "Duration < 500ms": (r) => r.timings.duration < 500
    });

    sleep(1);
}