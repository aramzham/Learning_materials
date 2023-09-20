import { sleep, check } from "k6";
import { Counter, Gauge, Rate, Trend } from "k6/metrics";
import http from "k6/http";

const customTrend = new Trend("custom_duration");
const customCounter = new Trend("custom_counter");
const customGauge = new Trend("custom_gauge");
const customRate = new Trend("custom_rate");

export const options = {
    scenarios: {
        constant_scenario: {
            executor: "constant-vus",
            vus: 1,
            duration: '5s',
            startTime: '0s'
        }
    }
};

export default function() {
    const res = http.get('https://reqres.in/api/users?page=2');

    check(res, {
        'is status 200': r => r.status === 200
    });

    // custom trend
    console.log('Response time (ms) was ' + String(res.timings.duration));
    customTrend.add(res.timings.duration);

    // counter (sum of all values -> each iteration)
    customCounter.add(1);
    customCounter.add(2);
    customCounter.add(3);

    // rate -> 50 / 50 here :)
    customRate.add(1); // 1 for passed
    customRate.add(true); // same
    customRate.add(false); // fail
    customRate.add(0); // same

    // gauge displays last value, along with min and max
    customGauge.add(1);
    customGauge.add(5);
    customGauge.add(10);

    sleep(1);
};