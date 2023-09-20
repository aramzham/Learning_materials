import http from 'k6/http';
import {check, sleep} from 'k6';

export let options = {
    thresholds: {
        "http_req_duration": [{
            threshold: "p(95)<200" // 95% of requests must be under 200ms response time
        }],
        "checks": [{
            threshold: "rate>0.7" // 70% of checks must pass
        }],
        "http_req_failed":["rate<0.1"]
    },
    stages: [
        {duration: '2s', target: 5},
        {duration: '3s', target: 10},
        {duration: '3s', target: 5},
        {duration: '2s', target: 1},
    ]
}

export default function() {
    let result = http.get('https://test-api.k6.io/public/crocodiles/');

    check(result, {
        "Status is 200": (r) => r.status == 200,
        "Duration < 500ms": (r) => r.timings.duration < 500
    });

    sleep(1);
}