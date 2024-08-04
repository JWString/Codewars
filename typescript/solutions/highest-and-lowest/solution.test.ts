import solution = require('./solution');
import { assert } from "chai";

describe("Example Tests", function () {
  it("Test 1", function () {
    assert.strictEqual(solution.Kata.highAndLow("8 3 -5 42 -1 0 0 -9 4 7 4 -4"), "42 -9");
  });
  it("Test 2", function () {
    assert.strictEqual(solution.Kata.highAndLow("1 2 3"), "3 1");
  });
});