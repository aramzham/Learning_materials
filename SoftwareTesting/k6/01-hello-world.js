import http from 'k6/http';
import {sleep} from 'k6';

export default function () {
    // if you want to use other port than 6565 which is set by default
    // use k6 run {file_name} --address=:15645 (some other available port number)
    let result = http.get('https://test-api.k6.io/');

    sleep(1);
}