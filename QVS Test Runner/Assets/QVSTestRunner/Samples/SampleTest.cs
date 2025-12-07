using QVSRunner.Core.Discovery;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TestClass]
public class SampleTest 
{
    [Test]
    public void SimpleTest()
    {
        int sum = 2 + 2;

        if(sum!=4)
        {
            throw new ArgumentException("The sum is not equal to 4");
        }
    }
}
