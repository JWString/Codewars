//https://www.codewars.com/kata/5af4119888214326b4000019/train/csharp

namespace Solutions.Amidakuji
{
  public class Banzai
  {
    public static int[] Amidakuji(string[] ar)
    {
      var positions = Enumerable
        .Range(0, ar[0].Length + 1)
        .ToArray();
      foreach (var row in ar) {
        for (int i = 0; i < row.Length; i++) {
          if (row[i] == '1') {
            (positions[i+1], positions[i]) = (positions[i], positions[i+1]);
          }
        }
      }
      return positions;
    }
  }
}