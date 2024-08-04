//https://www.codewars.com/kata/51ba717bb08c1cd60f00002f

namespace Solutions.RangeExtraction
{
  public class RangeExtraction
  {

    class Range {
      readonly int _first;
      int _last;

      public Range(int first) {
        _first = first;
        _last = first;
      }

      public bool TryAdd(int n) {
        if (n - _last == 1) {
          _last = n;
          return true;
        } else {
          return false;
        }
      }

      public override string ToString()
      {
        if (_first == _last) {
          return _first.ToString();
        } else {
          return _first.ToString() + (_last - _first == 1 ? ',' : '-') + _last.ToString();
        }
      }
    }

    public static string Extract(int[] args)
    {
      var ranges = new Queue<Range>();
      ranges.Enqueue(new Range(args[0]));
      foreach (var n in args.Skip(1)) {
        var prev = ranges.Last();
        if (!prev.TryAdd(n)) {
          ranges.Enqueue(new Range(n));
        }
      }
      return string.Join(',', ranges.Select(r => r.ToString()));
    }
  }  
}
