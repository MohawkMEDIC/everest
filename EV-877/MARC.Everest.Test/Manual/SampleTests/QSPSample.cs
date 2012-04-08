using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;
using MARC.Everest.RMIM.UV.NE2008.Interactions;
using System.Reflection;
using MARC.Everest.Attributes;

namespace MARC.Everest.Test.Manual.SampleTests
{
    /// <summary>
    /// Summary description for QSPSample
    /// </summary>
    [TestClass]
    public class QSPSample
    {
        public QSPSample()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion



        public void EverestManualExample116Test()
        {
            Type scanType = typeof(PRPA_IN201305UV02);

            foreach (var property in scanType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                Console.WriteLine("{0} = ", property.Name);

                var structureAtt = property.PropertyType.GetCustomAttributes(typeof(StructureAttribute), false);

                if (structureAtt.Length == 0)
                    Console.WriteLine("?");
                else
                {
                    StructureAttribute sa = structureAtt[0] as StructureAttribute;
                    Console.WriteLine(sa.StructureType.ToString());
                }
            }
        }

        /*
        [TestMethod]
        public void TPHTest01()
        {
            // create a periodic hull set expression of type SXPR
            SXPR<INT> setExpression = new SXPR<INT>(
                new SXCM<INT>(
                    new IVL(1, 10){
                    }
                ),

                
                IVL<INT> test = new IVL<INT>(1, 10)
                {
                    LowClosed = true,
                    HighClosed = true
                },
                new IVL<INT>(5, 8)
                {
                    LowClosed = true,
                    HighClosed = true,
                    Operator = SetOperator.PeriodicHull
                },
                new IVL<INT>(7, 8)
                {
                    LowClosed = true,
                    HighClosed = true,
                    Operator = SetOperator.Exclusive
                }
            );

            // translate period hull set expression to type QSET
            var setExpression2 = setExpression.TranslateToQSET();

            setExpression2.NullFlavor = null;
            Assert.IsTrue(setExpression2.Validate());

            // test if 1 is within the range of the QSP
            //var test = new IVL<INT>(1,);
            bool isInRange = setExpression[0].Contains(1);
            Assert.AreEqual(isInRange, true);
        
        } * */
    }
}
