## Checks
All the checks will be executed no matter they were passed or not.
On the other hand if one threshold is not met => the execution will stop.

## Thresholds
Thresholds give you more flexible control on the flow than checks.
Run $LASTEXITCODE in cmd to check, **99** means it failed, **0** = success.

## Executors

- shared-iterations
- per-vu-iterations
- constant-vus
- ramping-vus
- constant-arrival-rate
- ramping-arrival-rate
- externally-controlled

## Scenarios

In scenarios you can specify sequential or parallel execution.

## Metrics

There are __4__ types of metrics
- counter, gauge, rate and trend
``` javascript
import { Counter, Gauge, Rate, Trend } from "k6/metrics'
const myMetric = new(<metric_type>);
myMetric.add(<value>);
```