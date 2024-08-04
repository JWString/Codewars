//https://www.codewars.com/kata/55bf01e5a717a0d57e0000ec

export function persistence(num) {
  var count = 0;
  num = num.toString();
  while(num.length > 1) {
    count++;
    num = num.split('').reduce((res, n) => res * Number(n), 1).toString();
  }
  return count;
}