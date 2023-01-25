using ContactLib.Utility;

namespace ContactLib.Tests;

public class FileUtilityTest
{

    class TestClass
    {
        public bool TestBool { get; set; }
        public string? TestString { get; set; }
        public int? TestInt { get; set; }
        public (bool, string?) ToTuple() => (TestBool, TestString);
    }

    [Theory]
    [InlineData("test1.json", false, "test345")]
    [InlineData("test2.json", true, "test4")]
    [InlineData("test42.json", false, "test")]
    [InlineData("2test.json", true, "test244")]
    public void SaveAndLoadTest(string filename, bool testBool, string testString)
    {

        //Create test object
        var test = new TestClass() { TestBool = testBool, TestString = testString, TestInt = Random.Shared.Next() };


        //Test .Save(...)
        FileUtility.Save(filename, test);
        Assert.True(File.Exists(filename), "File exists:");


        //Test .Load(...)
        var deserializationResult = FileUtility.Load<TestClass>(filename, out var deserializedTest);

        Assert.True(deserializationResult, ".Load(...) return value:");
        Assert.NotNull(deserializedTest);
        Assert.Equal(test?.ToTuple(), deserializedTest?.ToTuple());


        //Cleanup
        if (File.Exists(filename))
            File.Delete(filename);

    }

}
