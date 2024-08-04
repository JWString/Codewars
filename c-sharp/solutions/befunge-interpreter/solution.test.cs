using NUnit.Framework;
namespace Solutions.BefungeInterpreter
{
  public class BefungeInterpreterBasicTests
  {
      [Test]
      public void Tests()
      {
          Assert.AreEqual(
                  "123456789",
                  new BefungeInterpreter().Interpret(">987v>.v\nv456<  :\n>321 ^ _@"));
      }
  }
}