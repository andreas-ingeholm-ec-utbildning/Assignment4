using ContactLib.Models;

namespace ContactLib.Tests;

public class ContactListTest
{

    [Theory]
    [InlineData("test", "test", "test@gmail.com", "041545", "street 1")]
    [InlineData("test1", "testsson", "test1.testsson@gmail.com", "01444041545", "street 2")]
    [InlineData("test4234", "test", "test4234@gmail.com", "041545", "street 3")]
    [InlineData("test234234234", "test", "test234234234@gmail.com", "041545", "street 575")]
    [InlineData("testtest", "testtest", "testtest.testtest@gmail.com", "041545", "street 45545")]
    public void AddRemoveTest(string firstName, string lastName, string email, string phoneNumber, string address)
    {

        //Create list
        var list = ContactList.InMemory();
        Assert.NotNull(list);

        //Test .CreateContact(...)
        list.CreateContact(firstName, lastName, email, phoneNumber, address);
        Assert.True(list.Count == 1);

        //Test .Add(...)
        var testItem = new Contact() { FirstName = firstName, LastName = lastName, Email = email, PhoneNumber = phoneNumber, Address = address };
        list.Add(testItem);
        Assert.True(list.Count == 2);

        //Test items are correct
        Assert.Equal(list[0]?.ToTupleWithoutID(), (firstName, lastName, email, phoneNumber, address));
        Assert.Same(list[1], testItem);
        Assert.Contains(testItem, list);

        //Test .RemoveAt(...) and .Remove(...)
        list.RemoveAt(0);
        _ = list.Remove(testItem);
        Assert.Empty(list);

    }

    [Theory]
    [InlineData("testlist.json")]
    public void SaveLoadTest(string filename)
    {

        var list = ContactList.InMemory();
        Assert.NotNull(list);
        Assert.Null(list.Path);

        list.Save(filename);
        Assert.True(File.Exists(filename), "File exists:");

        list.Add(new());
        Assert.NotEmpty(list);

        var newList = ContactList.FromFile(filename);
        Assert.NotEmpty(newList);
        Assert.NotNull(newList[0]);
        Assert.Equal(list[0]?.ID, newList[0]?.ID);

        if (File.Exists(filename))
            File.Delete(filename);

    }

}