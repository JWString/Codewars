namespace Solutions.SplitStrings
{
  public class SplitString
  {
    static IEnumerable<string> EnumerateString(string str) {
      var estr = str.AsEnumerable();
      var len = str.Length;
      if ((str.Length & 1) == 1) {
        estr = estr.Append('_');
        len += 1;
      }
      for (int i = 0; i < len; i += 2) {
        yield return string.Concat(estr.Take(2));
        estr = estr.Skip(2);
      }
    }
    public static string[] Solution(string str)
    {
      return EnumerateString(str).ToArray();
    }
  }
}