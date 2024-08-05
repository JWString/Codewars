namespace Solutions.SplitStrings
{
  public class SplitString
  {
    static IEnumerable<string> Split(string str)
    {
      for (int i = 0; i < str.Length; i += 2) {
        var substr = str.Substring(i, i + 2 > str.Length ? 1 : 2);
        if (substr.Length == 1) {
          substr += '_';
        }
        yield return substr;
      }
    }

    public static string[] Solution(string str)
    {
      return Split(str).ToArray();
    }
  }
}