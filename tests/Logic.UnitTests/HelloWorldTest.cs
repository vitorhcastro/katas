namespace Katas.Logic.UnitTests;

public class HelloWorldTest
{
    [Fact]
    public void Hello_ReturnsWorld() => Assert.Equal("World!", HelloWorld.Hello());
}