import http from "k6/http";
import { check, sleep } from "k6";

export const options = {
    scenarios: {
        per_vu_scenario: {
            executor: "per-vu-iterations", // 5*5=25 iterations
            vus: 5,
            iterations: 5,
            startTime: '3s'
        },
        shared_scenario: {
            executor: "shared-iterations", // the iterations will be shared among virtual users
            vus: 5, // 5/5=1 iterations per vu => 5 in total
            iterations: 5,
            startTime: '0s'
        },
        constant_scenario: {
            executor: "constant-vus", // will execute as much iterations as possible in a constant amount of time
            vus: 5,
            duration: '10s',
            startTime: '0s'
        },
        ramping_vus_scenario: {
            executor: "ramping-vus",
            startTime: '0s',
            stages: [{
                target: 5,
                duration: '14s'
            }]
        },
        constant_arrival_scenario: {
            executor: "constant-arrival-rate", // fixed number of iterations in specified time
            rate: 5,
            duration: '20s',
            preAllocatedVUs: 5,
            maxVUs: 10 // up to 10 VUs
        },
        ramping_arrival_scenario: {
            executor: 'ramping-arrival-rate', // variable time of iterations based on time
            startRate: 2,
            timeUnit: '1s',
            preAllocatedVUs: 2,
            maxVUs: 20,
            stages: [{
                target: 15,
                duration: '30s'
            }]
        },
        externally_controlled_scenario: {
            executor: 'externally-controlled', // use 'k6 status', 'k6 pause', 'k6 resume', 'k6 scale --vus=1' etc commands
            vus: 10,
            maxVUs: 30,
            duration: '2m'
        }
    }
}

export default function() {
    let result = http.get('https://test-api.k6.io/');

    check(result, {
        "Status is 200": (r) => r.status === 200
    });

    sleep(1);
}