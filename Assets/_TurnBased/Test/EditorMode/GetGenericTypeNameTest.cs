using NUnit.Framework;

public class GetGenericTypeNameTest
{
    [Category("Other")]
    [Test]
    public void GivenGenericTThenNameofTEqualsGetTypeName(){
        string actual = new CustomClass<MyClassA>().GetTypeName();
        string expected = "MyClass";

        Assert.AreEqual(expected, actual);
    }

    public class MyClassA
    {}

    public class CustomClass<T> where T : new()
    {
        public string GetTypeName() => new T().GetType().Name;
    }

    public enum ClassName
    {
        MyClassA,
        MyClassB,
        MyClassC,
        MyClassD,
        MyClassE
    }
}
