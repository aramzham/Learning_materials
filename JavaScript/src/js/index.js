import calculator, {name as authorName, age} from './calculator';

//console.log('Hello');
// var a = 25;
// var b = 5;
//
// var sum =  calculator.sum(a,b);
// console.log(sum);
// console.log(authorName);
// console.log(age);

// import './SpreadRestFor/spread';

import MovieList from './components/movie-list';
import moviesService from './movies-service';

const input = document.querySelector('.search-input');
const movieList = document.querySelector('.movies');
const list = new MovieList();

input.addEventListener('input', e => {
    const searchText = e.target.value;
    if(!searchText) {
        list.clearList(movieList);
        return;
    }

    moviesService.getVideoByText(searchText)
        .then(result => {
            list.renderMovies(result);
            list.drawToDom(movieList);
        });
});