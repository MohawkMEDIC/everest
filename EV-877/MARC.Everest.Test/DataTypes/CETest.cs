using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Connectors;

namespace MARC.Everest.Test.DataTypes
{
    /// <summary>
    /// Summary description for CETest
    /// </summary>
    [TestClass]
    public class CETest
    {
        public CETest()
        {
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

        /// <summary>
        /// Ensure that validation succeeds (return TRUE)
        /// When the following values are being populated:
        ///     NullFlavor
        /// And, the following variables are nullified:
        ///     Code            : The initial code. The plain code symbol defined by the code system
        ///     CodeSystem      : The code system the code was picked from. The OID representing the system from which the code was drawn from
        ///     CodeSystemName  : The name of the code system 
        ///     DisplayName     : The display name for the code
        /// </summary> 
        [TestMethod]
        public void CENullFlavorTest()
        {
            CE<String> ce = new CE<String>();
            ce.Code = null;
            ce.CodeSystem = null;
            ce.CodeSystemName = null;
            ce.DisplayName = null;
            ce.NullFlavor = NullFlavor.NoInformation;
            Assert.IsTrue(ce.Validate());
        }

        /// <summary>
        /// Ensure that validation succeeds (return TRUE)
        /// When the following values are being populated:
        ///     Code            : The initial code. The plain code symbol defined by the code system
        /// And, the following variables are nullified:
        ///     CodeSystem      : The code system the code was picked from. The OID representing the system from which the code was drawn from
        ///     CodeSystemName  : The name of the code system 
        ///     DisplayName     : The display name for the code
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void CECodeTest()
        {
            CE<String> ce = new CE<String>();
            ce.Code = "284196006";
            ce.CodeSystem = null;
            ce.CodeSystemName = null;
            ce.DisplayName = null;
            ce.NullFlavor = null;
            Assert.IsTrue(ce.Validate());
        }

        /// <summary>
        /// Ensure that validation succeeds (return TRUE)
        /// When the following values are being populated:
        ///     Code            : The initial code. The plain code symbol defined by the code system
        ///     CodeSystem      : The code system the code was picked from. The OID representing the system from which the code was drawn from
        /// And, the following variables are nullified:
        ///     CodeSystemName  : The name of the code system 
        ///     DisplayName     : The display name for the code
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void CECodeCodeSystemTest()
        {
            CE<String> ce = new CE<String>();
            ce.Code = "284196006";
            ce.CodeSystem = "2.16.840.1.113883.6.96";
            ce.CodeSystemName = null;
            ce.DisplayName = null;
            ce.NullFlavor = null;
            Assert.IsTrue(ce.Validate());
        }

        /// <summary>
        /// Ensure that validation succeeds (return TRUE)
        /// When the following values are being populated:
        ///     Code            : The initial code. The plain code symbol defined by the code system
        ///     DisplayName     : The display name for the code
        /// And, the following variables are nullified:
        ///     CodeSystem      : The code system the code was picked from. The OID representing the system from which the code was drawn from
        ///     CodeSystemName  : The name of the code system 
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void CECodeDisplayNameTest()
        {
            CE<String> ce = new CE<String>();
            ce.Code = "284196006";
            ce.CodeSystem = null;
            ce.CodeSystemName = null;
            ce.DisplayName = "Burn of skin";
            ce.NullFlavor = null;
            Assert.IsTrue(ce.Validate());
        }

        /// <summary>
        /// Ensure that validation succeeds (return TRUE)
        /// When the following values are being populated:
        ///     Code            : The initial code. The plain code symbol defined by the code system
        ///     CodeSystem      : The code system the code was picked from. The OID representing the system from which the code was drawn from
        ///     CodeSystemName  : The name of the code system 
        /// And, the following variables are nullified:
        ///     DisplayName     : The display name for the code
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void CECodeCodeSystemCodeSystemNameTest()
        {
            CE<String> ce = new CE<String>();
            ce.Code = "284196006";
            ce.CodeSystem = "2.16.840.1.113883.6.96";
            ce.CodeSystemName = "SNOMED CT";
            ce.DisplayName = null;
            ce.NullFlavor = null;
            Assert.IsTrue(ce.Validate());
        }

        /// <summary>
        /// Ensure that validation succeeds (return TRUE)
        /// When the following values are being populated:
        ///     Code            : The initial code. The plain code symbol defined by the code system
        ///     CodeSystem      : The code system the code was picked from. The OID representing the system from which the code was drawn from
        ///     CodeSystemName  : The name of the code system 
        ///     DisplayName     : The display name for the code
        /// And, the following variables are nullified:
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void CECodeCodeSystemCodeSystemNameDisplayNameTest()
        {
            CE<String> ce = new CE<String>();
            ce.Code = "284196006";
            ce.CodeSystem = "2.16.840.1.113883.6.96";
            ce.CodeSystemName = "SNOMED CT";
            ce.DisplayName = "Burn of skin";
            ce.NullFlavor = null;
            Assert.IsTrue(ce.Validate());
        }

        /// <summary>
        /// Ensure that validation fails (return FALSE)
        /// When the following values are being populated:
        ///     Code            : The initial code. The plain code symbol defined by the code system
        ///     CodeSystem      : The code system the code was picked from. The OID representing the system from which the code was drawn from
        ///     CodeSystemName  : The name of the code system 
        ///     DisplayName     : The display name for the code
        ///     NullFlavor
        /// And, there are not variables being nullified:
        /// </summary> 
        [TestMethod]
        public void CECodeCodeSystemCodeSystemNameDisplayNameNullFlavorTest()
        {
            CE<String> ce = new CE<String>();
            ce.Code = "284196006";
            ce.CodeSystem = "2.16.840.1.113883.6.96";
            ce.CodeSystemName = "SNOMED CT";
            ce.DisplayName = "Burn of skin";
            ce.NullFlavor = NullFlavor.NoInformation;
            Assert.IsFalse(ce.Validate());
        }

        /// <summary>
        /// Ensure that validation fails (return FALSE)
        /// When the following values are being populated:
        ///     DisplayName     : The display name for the code
        /// And, the following variables are nullified:
        ///     Code            : The initial code. The plain code symbol defined by the code system
        ///     CodeSystem      : The code system the code was picked from. The OID representing the system from which the code was drawn from
        ///     CodeSystemName  : The name of the code system 
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void CEDisplayNameTest()
        {
            CE<String> ce = new CE<String>();
            ce.Code = null;
            ce.CodeSystem = null;
            ce.CodeSystemName = null;
            ce.DisplayName = "Burn of skin";
            ce.NullFlavor = null;
            Assert.IsFalse(ce.Validate());
        }

        /// <summary>
        /// Ensure that validation fails (return FALSE)
        /// When the following values are being populated:
        ///     CodeSystem      : The code system the code was picked from. The OID representing the system from which the code was drawn from
        /// And, the following variables are nullified:
        ///     Code            : The initial code. The plain code symbol defined by the code system
        ///     CodeSystemName  : The name of the code system 
        ///     DisplayName     : The display name for the code
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void CECodeSystemTest()
        {
            CE<String> ce = new CE<String>();
            ce.Code = null;
            ce.CodeSystem = "2.16.840.1.113883.6.96";
            ce.CodeSystemName = null;
            ce.DisplayName = null;
            ce.NullFlavor = null;
            Assert.IsFalse(ce.Validate());
        }

        /// <summary>
        /// Ensure that validation fails (return FALSE)
        /// When the following values are being populated:
        ///     CodeSystemName  : The name of the code system 
        /// And, the following variables are nullified:
        ///     Code            : The initial code. The plain code symbol defined by the code system
        ///     CodeSystem      : The code system the code was picked from. The OID representing the system from which the code was drawn from
        ///     DisplayName     : The display name for the code
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void CECodeSystemNameTest()
        {
            CE<String> ce = new CE<String>();
            ce.Code = null;
            ce.CodeSystem = null;
            ce.CodeSystemName = "SNOMED CT";
            ce.DisplayName = null;
            ce.NullFlavor = null;
            Assert.IsFalse(ce.Validate());
        }

        /// <summary>
        /// Ensure that validation fails (return FALSE)
        /// When the following values are being populated:
        ///     Code            : The initial code. The plain code symbol defined by the code system
        ///     CodeSystemName  : The name of the code system 
        /// And, the following variables are nullified:
        ///     CodeSystem      : The code system the code was picked from. The OID representing the system from which the code was drawn from
        ///     DisplayName     : The display name for the code
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void CECodeSystemNameCodeTest()
        {
            CE<String> ce = new CE<String>();
            ce.Code = "284196006";
            ce.CodeSystem = null;
            ce.CodeSystemName = "SNOMED CT";
            ce.DisplayName = null;
            ce.NullFlavor = null;
            Assert.IsFalse(ce.Validate());
        }

        /// <summary>
        /// Ensure that validation fails (return FALSE)
        /// When the following values are being populated:
        ///     NullFlavor
        ///     Translation     : new instance of SET CD being created with the following
        ///                       values being populated:
        ///     Code            : The initial code. The plain code symbol defined by the code system
        ///     CodeSystemName  : The name of the code system 
        ///     CodeSystem      : The code system the code was picked from. The OID representing the system from which the code was drawn from
        ///     DisplayName     : The display name for the code
        /// And, there is no variables being nullified:
        /// </summary> 
        [TestMethod]
        public void CENullFlavorTranslationTest()
        {
            CE<String> ce = new CE<String>();
            ce.NullFlavor = NullFlavor.NoInformation;
            ce.Translation = new SET<CD<string>>();
            ce.Translation.Add(
                new CD<string>()
                {
                    Code = "15376812",
                    CodeSystem = "2.16.840.1.113883.3.232.99.1",
                    CodeSystemName = "3M HDD",
                    DisplayName = "BurnOfSkinSCT",
                });

            Assert.IsFalse(ce.Validate());
        }

        /// <summary>
        /// Ensure that validation succeeds (return TRUE)
        /// When the following values are being populated:
        ///     Code            : The initial code
        ///     CodeSystemName  : The name of the code system 
        ///     CodeSystem      : The code system the code was picked from
        ///     DisplayName     : The display name for the code
        ///     Translation     : new instance of SET CD being created with the following
        ///                       values being populated:
        ///                       Code            : The initial code
        ///                       CodeSystemName  : The name of the code system 
        ///                       CodeSystem      : The code system the code was picked from
        ///                       DisplayName     : The display name for the code
        /// And, there following variables are being nullified:
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void CECodeCodeSystemCodeSystemNameDisplayNameTranslationTest()
        {
            CE<String> ce = new CE<String>();
            ce.Code = "284196006";
            ce.CodeSystem = "2.16.840.1.113883.6.96";
            ce.CodeSystemName = "SNOMED CT";
            ce.DisplayName = "Burn of skin";
            ce.NullFlavor = null;
            ce.Translation = new SET<CD<string>>();
            ce.Translation.Add(
                new CD<string>()
                {
                    Code = "15376812",
                    CodeSystem = "2.16.840.1.113883.3.232.99.1",
                    CodeSystemName = "3M HDD",
                    DisplayName = "BurnOfSkinSCT",
                });
            Assert.IsTrue(ce.Validate());
        }

        /// <summary>
        /// Ensure that validation succeeds (return TRUE)
        /// When the following values are being populated:
        ///     Code            : The initial code
        ///     CodeSystemName  : The name of the code system 
        ///     CodeSystem      : The code system the code was picked from
        ///     DisplayName     : The display name for the code
        ///     Translation     : new instances of SET CD being created with the following
        ///                       values being populated:
        ///                       Code            : The initial code
        ///                       CodeSystemName  : The name of the code system 
        ///                       CodeSystem      : The code system the code was picked from
        ///                       DisplayName     : The display name for the code
        ///                       values being nullified:
        ///                       NullFlavor
        /// And, there following variables are being nullified:
        ///     NullFlavor
        /// </summary>         
        [TestMethod]
        public void CETranslationTranslationTest()
        {
            CE<String> ce = new CE<String>();
            ce.Code = "284196006";
            ce.CodeSystem = "2.16.840.1.113883.6.96";
            ce.CodeSystemName = "SNOMED CT";
            ce.DisplayName = "Burn of skin";
            ce.NullFlavor = null;
            ce.Translation = new SET<CD<string>>();
            ce.Translation.Add(
                new CD<string>()
                {
                    Code = "15376812",
                    CodeSystem = "2.16.840.1.113883.3.232.99.1",
                    CodeSystemName = "3M HDD",
                    DisplayName = "BurnOfSkinSCT",
                    NullFlavor = null,
                    Translation = new SET<CD<string>>(
                        new CD<string>()
                        {
                            Code = "284196006",
                            CodeSystem = "2.16.840.1.113883.6.96",
                            CodeSystemName = "SNOMED CT",
                            DisplayName = "Burn of skin"
                        }, CD<String>.Comparator)
                });

            Assert.IsFalse(ce.Validate());
        }

        /// <summary>
        /// We are going to Parse a CE<String>(T) to CE
        /// </summary>
        [TestMethod]
        public void CEParseGenericCEtoCETest()
        {
            // CE<NullFlavor>.Code = Other, 1 Translation
            CE<NullFlavor> instance = new CE<NullFlavor>();
            instance.Code = NullFlavor.Other;
            instance.Translation = new SET<CD<NullFlavor>>();
            instance.Translation.Add(new CD<NullFlavor>(NullFlavor.Other, "2.16.840.1.113883.3.232.99.1"));
            Console.WriteLine(instance.Translation.Count()); // temporary, equal to 1

            // this is where we parse the instance
            CE<String> parsedCE = Util.FromWireFormat(instance, typeof(CE<String>)) as CE<String>;
            Console.WriteLine(parsedCE.Translation.Count()); // temporary, equal to 0

            // will be true if parsed correctly
            Assert.IsTrue("OTH" == parsedCE.Code.ToString());
            Assert.IsTrue(1 == parsedCE.Translation.Count()); // fails
        }
        
        /// <summary>
        /// We are casting CE<String>(T) to T
        /// </summary>
        [TestMethod]
        public void CECastCEGenericToTypeTest()
        {
                // Setting CE<NullFlavor>.Code = Other
                CE<NullFlavor> ceInstance = new CE<NullFlavor>();
                ceInstance.Code = NullFlavor.Other;
                
                // will be true if the cast was succesful
                Assert.IsTrue(NullFlavor.Other == (NullFlavor)ceInstance);
        }


        /// <summary>
        /// We are going to Cast T to CE<T>
        /// </summary>
        [TestMethod]
        public void CECastTypeToCEGenericTest()
        {
                // set NullFlavor.Other
                NullFlavor mode = NullFlavor.Other; 
                
                // create an CE<NullFlavor> instance
                CE<NullFlavor> ceInstance = new CE<NullFlavor>(); // instance is a CE<T> where T is a nullflavor

                // cast T to CE<T>
                ceInstance =  mode;
                
                // will be true if cast was successful
                Assert.IsTrue(ceInstance.Code == NullFlavor.Other);
        }
       
        /// <summary>
        /// checking to see if we can Cast CE<String> To CE<T>
        /// </summary>
        [TestMethod]
        public void CECastCEtoCEGenericTest()
        {
                // set CE.Code = “OTH”, 1 Translation
                CE<String> ceInstance = new CE<String>();
                ceInstance.Code = "OTH";
                ceInstance.Translation = new SET<CD<String>>();
                ceInstance.Translation.Add(new CD<String>()
                                                    {
                                                        Code = "15376812",
                                                        CodeSystem = "2.16.840.1.113883.3.232.99.1",
                                                        CodeSystemName = "3M HDD",
                                                        DisplayName = "BurnOfSkinSCT",
                                                        NullFlavor = null
                                                    });
                
                // create CE<NullFlavor> instance
                CE<NullFlavor> ceInstance2 = new CE<NullFlavor>();
                
                // cast from CE<String> to CE<T>
                ceInstance2 = Util.Convert<CE<NullFlavor>>(ceInstance);
                
                // these asserts will be true if the cast is done correctly
                Console.WriteLine(ceInstance2.Translation.Count);
                Assert.AreEqual(ceInstance2.Code ,NullFlavor.Other);
                // this will not ever be true because Translation.Count = 1 before the cast
                // and Translation.Count = 0 after the cast. This is a bug
                Assert.AreEqual(1, ceInstance2.Translation.Count); // fails
        }

        /// <summary>
        /// Using CE<String> as Interface
        /// </summary>
        [TestMethod]
        public void CEUseCEAsInterfaceTest()
        {
                // CE<NullFlavor>.Code = Other, 2 Translations
                CE<NullFlavor> ceInstance = new CE<NullFlavor>();
                ceInstance.Code = NullFlavor.Other;
                ceInstance.Translation = new SET<CD<NullFlavor>>(CD<NullFlavor>.Comparator);
                ceInstance.Translation.Add(new CD<NullFlavor>(NullFlavor.Other, "2.16.840.1.113883.3.232.99.1"));
                ceInstance.Translation.Add(new CD<NullFlavor>(NullFlavor.Other, "2.16.840.1.113883.3.232.99.2"));
                Console.WriteLine(ceInstance.Translation.Count());
              
                // Creating an instance of the implementing class and assigning that object to a 
                // reference to any of the interfaces it implements.
                ICodedEquivalents interfaceUse = ceInstance;

            // is true if using CE<String> as Interface is possible    
            Assert.IsTrue(interfaceUse.Translation.Count == 2); // passes

        }

    }
}
