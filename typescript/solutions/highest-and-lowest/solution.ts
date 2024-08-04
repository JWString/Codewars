//https://www.codewars.com/kata/554b4ac871d6813a03000035

export class Kata {
  static highAndLow(numbers) {
    var arr = numbers.split(' ');
    return `${Math.max(...arr)} ${Math.min(...arr)}`;
  }
}