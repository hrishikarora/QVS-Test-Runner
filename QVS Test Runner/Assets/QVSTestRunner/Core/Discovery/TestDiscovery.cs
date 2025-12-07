using System;
using System.Collections.Generic;
using System.Reflection;

namespace QVSRunner.Core.Discovery
{
    public static class TestDiscovery
    {
        /// <summary>
        /// This function returns all the test that was discovered by reflection
        /// </summary>
        /// <returns></returns>
        public static List<DiscoveredTest> Discover()
        {
            var results = new List<DiscoveredTest>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies(); //Gets the assemblies
            foreach(var assembly in assemblies)
            {
                foreach(var type in assembly.GetTypes())
                {
                    var testClassAtribute = type.GetCustomAttribute<TestClassAttribute>(); //Checks if its of [TestClass] attribute
                    if (testClassAtribute == null)
                        continue; 
                    
                    foreach(var method in type.GetMethods())
                    {
                        var testAttribut = method.GetCustomAttribute<TestAttribute>(); //Filters methods with [Test] attribute method
                        if(testAttribut == null)
                            continue;

                        results.Add(new DiscoveredTest
                        {
                            ClassType = type,
                            Method = method
                        });
                    }
                }
            }
            return results;
        }
    }
}
