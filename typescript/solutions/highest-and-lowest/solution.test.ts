import solution = require('./solution');

describe("Example Tests", function () {
  it("Test 1", function () {
    expect(solution.Kata.highAndLow("8 3 -5 42 -1 0 0 -9 4 7 4 -4")).toBe("42 -9");
  });
  it("Test 2", function () {
    expect(solution.Kata.highAndLow("1 2 3")).toBe("3 1");
  });
});