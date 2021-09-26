using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class DataStorageTests
{
    [Test]
    public void PersistedDataStorageTests()
	{
        string teststring = "testString";
        bool testbool = true;
        int testint = 492;
        float testfloat = -44.678f;

        DataStorage.SavePersisted("string", teststring);
        DataStorage.SavePersisted("bool", testbool);
        DataStorage.SavePersisted("int", testint);
        DataStorage.SavePersisted("float", testfloat);

        string loadedstring = DataStorage.LoadPersisted("string", string.Empty);
        bool loadedbool = DataStorage.LoadPersisted("bool", false);
        int loadedint = DataStorage.LoadPersisted("int", 0);
        float loadedfloat = DataStorage.LoadPersisted("float", 0.0f);

        Assert.AreEqual(teststring, loadedstring);
        Assert.AreEqual(testbool, loadedbool);
        Assert.AreEqual(testint, loadedint);
        Assert.AreEqual(testfloat, loadedfloat);
    }

	[Test]
	public void NonePersistedDataStorageTests()
	{
        TestObject testobj = new TestObject();
        testobj.objstring = "anotherString";
        testobj.objbool = false;
        testobj.objint = 12554;
        testobj.objfloat = 5.66f;

        //support for 8 save files
        for (int i = 0; i < 8; i++)
        {
            DataStorage.SaveNonPersisted(testobj, i);

            TestObject loadedobj = DataStorage.LoadNonPersisted<TestObject>(i);

            Assert.AreEqual(testobj.objstring, loadedobj.objstring);
            Assert.AreEqual(testobj.objbool, loadedobj.objbool);
            Assert.AreEqual(testobj.objint, loadedobj.objint);
            Assert.AreEqual(testobj.objfloat, loadedobj.objfloat);
        }
    }
}

public struct TestObject
{
    public string objstring;
    public bool objbool;
    public int objint;
    public float objfloat;
}
