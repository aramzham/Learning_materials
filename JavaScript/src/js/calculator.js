var name = 'Anton';
var age = 16;

var calculator = {
  sum: function (a,b) {
      return a+b;
  },
  dif: function (a,b) {
      return a-b;
  }
};

export default calculator;
export {name};
export {age};