import http from 'k6/http';
import {sleep} from 'k6';

// vu is for VIRTUAL USERS
export let options = {
    discardResponseBodies: true,
    scenarios: {
        Scenario_GetCommunications: {
            exec: 'FunctionForThisScenario',
            executor: 'ramping-vus',
            startTime: '0s',
            startVUs: 1,
            stages: [{duration: '5s', target: 5}]
        },
        Scenario_GetSubmissions: {
            exec: 'FunctionGetSubmissions',
            executor: 'ramping-vus',
            startTime: '3s',
            startVUs: 5,
            stages: [{duration: '2s', target: 5}]
        }
    }
}

export function FunctionForThisScenario(params) {

    let result = http.get('https://test-api.k6.io/public/crocodiles/1/');

    sleep(1);
}

export function FunctionGetSubmissions(params) {

    let result = http.get('https://test-api.k6.io/public/crocodiles/2/');

    sleep(1);
}