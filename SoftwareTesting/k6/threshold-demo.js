import http from "k6/http";
import { check, sleep } from "k6";

// thresholds give you more flexible control on the flow than checks
export const options = {
    thresholds: {
        http_req_blocked: [{
            threshold: 'max < 3000',
            abortOnFail: true
        }],
        http_req_duration: [{
            threshold: 'p(95) < 1000', // 1s
            abortOnFail: true
        }]
    }
}

export default function() {
    let result = http.get('https://test-api.k6.io/public/crocodiles/5/');

    check(result, {
        "Status is 200": (r) => r.status === 200,
        'is not 404': r => r.status !== 404
    });

    sleep(1);
}

// run $LASTEXITCODE in cmd to check, 99 means it failed, 0 = success