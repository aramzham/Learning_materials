import http from 'k6/http';
import {check, sleep} from 'k6';

export let options = {
    vus: 3,
    duration: "6s"
}

export default function() {
    const result = http.get('https://onlineboutique.dev/');

    check(result, {
        "Status is 200": (r) => r.status === 200,
        "Duration < 500ms": (r) => r.timings.duration < 500
    });

    sleep(1);
}