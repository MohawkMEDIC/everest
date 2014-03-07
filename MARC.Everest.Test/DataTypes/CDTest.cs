/**
 * Copyright 2008-2014 Mohawk College of Applied Arts and Technology
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); you 
 * may not use this file except in compliance with the License. You may 
 * obtain a copy of the License at 
 * 
 * http://www.apache.org/licenses/LICENSE-2.0 
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the 
 * License for the specific language governing permissions and limitations under 
 * the License.
 * 
 * User: fyfej
 * Date: 3-6-2013
 */
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;
using MARC.Everest.RMIM.CA.R020401;
using MARC.Everest.Connectors;

namespace MARC.Everest.Test.DataTypes
{
    /// <summary>
    /// Summary description for CDTest
    /// </summary>
    [TestClass]
    public class CDTest
    {
        public CDTest()
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
        /// And, the rest of the variables are Nullified:
        ///     Code            : The initial code. The plain code symbol defined by the code system
        ///     CodeSystem      : The code system the code was picked from. The OID representing the system from which the code was drawn from
        ///     CodeSystemName  : The name of the code system 
        ///     DisplayName     : The display name for the code
        /// </summary> 
        [TestMethod]
        public void CDNullFlavorTest()
        {
            CD<String> cd = new CD<String>();
            cd.Code = null;
            cd.CodeSystem = null;
            cd.CodeSystemName = null;
            cd.DisplayName = null;
            cd.NullFlavor = MARC.Everest.DataTypes.NullFlavor.NoInformation;
            Assert.IsTrue(cd.Validate());
        }

        /// <summary>
        /// Confirms that a CD is not equal to a String
        /// </summary>
        [TestMethod]
        public void CDTypeMismatchEqualityTest()
        {
            CD<NullFlavor> a = new CD<NullFlavor>(
                NullFlavor.NotApplicable, "1.2.3.4.5.6", "Dummy Code System", "2011"
                );
            Assert.IsFalse(a.Equals("12345"));
        }

        /// <summary>
        /// Confirms that two CDs with equal content are equal
        /// </summary>
        [TestMethod]
        public void CDContentSameEqualityTest()
        {
            CD<String> a = new CD<string>(
                "12345", "1.2.3.4.5.6", "Dummy Code System", "2011"
                ),
            b = new CD<String>(
                "12345","1.2.3.4.5.6", "Dummy Code System", "2011"
                );
            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(b.Equals(a));
        }

        /// <summary>
        /// Confirms that two CDs with different content aren't equal
        /// </summary>
        [TestMethod]
        public void CDContentDifferentEqualityTest()
        {
            CD<String> a = new CD<string>(
                "12345", "1.2.3.4.5.6", "Dummy Code System", "2011"
                ),
            b = new CD<String>(
                "12345", "1.2.3.4.5.7", "Dummy Code System", "2011"
                );
            Assert.IsFalse(a.Equals(b));
            b.Code = "12346";
            b.CodeSystem = "1.2.3.4.5";
            Assert.IsFalse(a.Equals(b));


        }

        /// <summary>
        /// Ensure that validation succeeds (return TRUE)
        /// When the following values are being populated:
        ///     Code            : The initial code. The plain code symbol defined by the code system
        /// And, the rest of the variables are Nullified:
        ///     CodeSystem      : The code system the code was picked from. The OID representing the system from which the code was drawn from
        ///     CodeSystemName  : The name of the code system 
        ///     DisplayName     : The display name for the code
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void CDCodeTest()
        {
            CD<String> cd = new CD<String>();
            cd.Code = "284196006";
            cd.CodeSystem = null;
            cd.CodeSystemName = null;
            cd.DisplayName = null;
            cd.NullFlavor = null;
            Assert.IsTrue(cd.Validate());
        }

        /// <summary>
        /// Ensure that validation succeeds (return TRUE)
        /// When the following values are being populated:
        ///     Code            : The initial code. The plain code symbol defined by the code system
        ///     CodeSystem      : The code system the code was picked from. The OID representing the system from which the code was drawn from
        /// And, the rest of the variables are Nullified:
        ///     CodeSystemName  : The name of the code system 
        ///     DisplayName     : The display name for the code
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void CDCodeCodeSystemTest()
        {
            CD<String> cd = new CD<String>();
            cd.Code = "284196006";
            cd.CodeSystem = "2.16.840.1.113883.6.96";
            cd.CodeSystemName = null;
            cd.DisplayName = null;
            cd.NullFlavor = null;
            Assert.IsTrue(cd.Validate());
        }

        /// <summary>
        /// Ensure that validation succeeds (return TRUE)
        /// When the following values are being populated:
        ///     Code            : The initial code. The plain code symbol defined by the code system
        ///     DisplayName     : The display name for the code
        /// And, the rest of the variables are Nullified:
        ///     CodeSystem      : The code system the code was picked from. The OID representing the system from which the code was drawn from
        ///     CodeSystemName  : The name of the code system 
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void CDCodeDisplayNameTest()
        {
            CD<String> cd = new CD<String>();
            cd.Code = "284196006";
            cd.CodeSystem = null;
            cd.CodeSystemName = null;
            cd.DisplayName = "Burn of skin";
            cd.NullFlavor = null;
            Assert.IsTrue(cd.Validate());
        }

        /// <summary>
        /// Ensure that validation succeeds (return TRUE)
        /// When the following values are being populated:
        ///     Code            : The initial code. The plain code symbol defined by the code system
        ///     CodeSystem      : The code system the code was picked from. The OID representing the system from which the code was drawn from
        ///     CodeSystemName  : The name of the code system 
        /// And, the rest of the variables are Nullified:
        ///     DisplayName     : The display name for the code
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void CDCodeCodeSystemCodeSystemNameTest()
        {
            CD<String> cd = new CD<String>();
            cd.Code = "284196006";
            cd.CodeSystem = "2.16.840.1.113883.6.96";
            cd.CodeSystemName = "SNOMED CT";
            cd.DisplayName = null;
            cd.NullFlavor = null;
            Assert.IsTrue(cd.Validate());
        }

        /// <summary>
        /// Ensure that validation succeeds (return TRUE)
        /// When the following values are being populated:
        ///     Code            : The initial code. The plain code symbol defined by the code system
        ///     CodeSystem      : The code system the code was picked from. The OID representing the system from which the code was drawn from
        ///     CodeSystemName  : The name of the code system 
        ///     DisplayName     : The display name for the code
        /// And, the rest of the variables are Nullified:
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void CDCodeCodeSystemCodeSystemNameDisplayNameTest()
        {
            CD<String> cd = new CD<String>();
            cd.Code = "284196006";
            cd.CodeSystem = "2.16.840.1.113883.6.96";
            cd.CodeSystemName = "SNOMED CT";
            cd.DisplayName = "Burn of skin";
            cd.NullFlavor = null;
            Assert.IsTrue(cd.Validate());
        }

        /// <summary>
        /// Ensure that validation fails (return FALSE)
        /// When the following values are being populated:
        ///     Code            : The initial code. The plain code symbol defined by the code system
        ///     CodeSystem      : The code system the code was picked from. The OID representing the system from which the code was drawn from
        ///     CodeSystemName  : The name of the code system 
        ///     DisplayName     : The display name for the code
        ///     NullFlavor
        /// And, the there are noNullified variables
        /// </summary> 
        [TestMethod]
        public void CDCodeCodeSystemCodeSystemNameDisplayNameNullFlavorTest()
        {
            CD<String> cd = new CD<String>();
            cd.Code = "284196006";
            cd.CodeSystem = "2.16.840.1.113883.6.96";
            cd.CodeSystemName = "SNOMED CT";
            cd.DisplayName = "Burn of skin";
            cd.NullFlavor = MARC.Everest.DataTypes.NullFlavor.NoInformation;
            Assert.IsFalse(cd.Validate());
        }

        /// <summary>
        /// Ensure that validation fails (return FALSE)
        /// When the following values are being populated:
        ///     Code            : The initial code. The plain code symbol defined by the code system
        ///     CodeSystem      : The code system the code was picked from. The OID representing the system from which the code was drawn from
        ///     CodeSystemName  : The name of the code system 
        ///     DisplayName     : The display name for the code
        /// And, the rest of the variables are Nullified:
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void CDDisplayNameTest()
        {
            CD<String> cd = new CD<String>();
            cd.Code = null;
            cd.CodeSystem = null;
            cd.CodeSystemName = null;
            cd.DisplayName = "Burn of skin";
            cd.NullFlavor = null;
            Assert.IsFalse(cd.Validate());
        }

        /// <summary>
        /// Ensure that validation fails (return FALSE)
        /// When the following values are being populated:
        ///     CodeSystem      : The code system the code was picked from. The OID representing the system from which the code was drawn from
        /// And, the rest of the variables are Nullified:
        ///     Code            : The initial code. The plain code symbol defined by the code system
        ///     CodeSystemName  : The name of the code system 
        ///     DisplayName     : The display name for the code
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void CDCodeSystemTest()
        {
            CD<String> cd = new CD<String>();
            cd.Code = null;
            cd.CodeSystem = "2.16.840.1.113883.6.96";
            cd.CodeSystemName = null;
            cd.DisplayName = null;
            cd.NullFlavor = null;
            Assert.IsFalse(cd.Validate());
        }

        /// <summary>
        /// Ensure that validation fails (return FALSE)
        /// When the following values are being populated:
        ///     CodeSystemName  : The name of the code system 
        /// And, the rest of the variables are Nullified:
        ///     Code            : The initial code. The plain code symbol defined by the code system
        ///     CodeSystem      : The code system the code was picked from. The OID representing the system from which the code was drawn from
        ///     DisplayName     : The display name for the code
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void CDCodeSystemNameTest()
        {
            CD<String> cd = new CD<String>();
            cd.Code = null;
            cd.CodeSystem = null;
            cd.CodeSystemName = "SNOMED CT";
            cd.DisplayName = null;
            cd.NullFlavor = null;
            Assert.IsFalse(cd.Validate());
        }

        /// <summary>
        /// Ensure that validation fails (return FALSE)
        /// When the following values are being populated:
        ///     Code            : The initial code. The plain code symbol defined by the code system
        ///     CodeSystemName  : The name of the code system 
        /// And, the rest of the variables are Nullified:
        ///     CodeSystem      : The code system the code was picked from. The OID representing the system from which the code was drawn from
        ///     DisplayName     : The display name for the code
        ///     NullFlavor
        /// </summary> 
        [TestMethod]
        public void CDCodeSystemNameCodeTest()
        {
            CD<String> cd = new CD<String>();
            cd.Code = "284196006";
            cd.CodeSystem = null;
            cd.CodeSystemName = "SNOMED CT";
            cd.DisplayName = null;
            cd.NullFlavor = null;
            Assert.IsFalse(cd.Validate());
        }

        /// <summary>
        /// Ensure that validation fails (return FALSE)
        /// </summary> 
        [TestMethod]
        public void CDNullFlavorTranslationTest()
        {
            CD<String> cd = new CD<String>();
            cd.NullFlavor = MARC.Everest.DataTypes.NullFlavor.NoInformation;
            cd.Translation = new SET<CD<string>>();
            cd.Translation.Add(
                new CD<string>()
                {
                    Code = "15376812",
                    CodeSystem = "2.16.840.1.113883.3.232.99.1",
                    CodeSystemName = "3M HDD",
                    DisplayName = "BurnOfSkinSCT",
                });

            Assert.IsFalse(cd.Validate());
        }

        /// <summary>
        /// Ensures that validation succeeds (return TRUE)
        /// </summary> 
        [TestMethod]
        public void CDCodeCodeSystemCodeSystemNameDisplayNameTranslationTest()
        {
            CD<String> cd = new CD<String>();
            cd.Code = "284196006";
            cd.CodeSystem = "2.16.840.1.113883.6.96";
            cd.CodeSystemName = "SNOMED CT";
            cd.DisplayName = "Burn of skin";
            cd.NullFlavor = null;
            cd.Translation = new SET<CD<string>>();
            cd.Translation.Add(
                new CD<string>()
                {
                    Code = "15376812",
                    CodeSystem = "2.16.840.1.113883.3.232.99.1",
                    CodeSystemName = "3M HDD",
                    DisplayName = "BurnOfSkinSCT",
                });
            Assert.IsTrue(cd.Validate());
        }

        /// <summary>
        /// Ensures that Comparator succeeds (return TRUE)
        /// </summary> 
        [TestMethod]
        public void CDTranslationTranslationTest()
        {
            CD<String> cd = new CD<String>();
            cd.Code = "284196006";
            cd.CodeSystem = "2.16.840.1.113883.6.96";
            cd.CodeSystemName = "SNOMED CT";
            cd.DisplayName = "Burn of skin";
            cd.NullFlavor = null;
            cd.Translation = new SET<CD<string>>();
            cd.Translation.Add(
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
        }


        /// <summary>
        /// Ensure that validation fails (returns FALSE)
        /// When Qualifier is being populated:
        /// </summary> 
        [TestMethod]
        public void CDQualifierTest()
        {
            CD<String> cd = new CD<String>();
            CR<String> severity = new CR<String>();
            severity.Name = new CD<string>();
            severity.Value = new CD<string>();
            cd.Qualifier = new LIST<CR<string>>();
            cd.Qualifier.Add(severity);
            severity.Name.Code = "246112005";
            severity.Name.CodeSystem = "2.16.840.1.113883.6.96";
            severity.Name.CodeSystemName = "SNOMED CT";
            severity.Name.DisplayName = "Severity";
            severity.Value.Code = "24484000";
            severity.Value.CodeSystem = "2.16.840.1.113883.6.96";
            severity.Value.CodeSystemName = "SNOMED CT";
            severity.Value.DisplayName = "Severe";
            CR<String> findingsite = new CR<String>();
            findingsite.Name = new CD<string>();
            findingsite.Value = new CD<string>();
            cd.Qualifier.Add(findingsite);
            findingsite.Name.Code = "363698007";
            findingsite.Name.CodeSystem = "2.16.840.1.113883.6.96";
            findingsite.Name.CodeSystemName = "SNOMED CT";
            findingsite.Name.DisplayName = "Finding site";
            findingsite.Value.Code = "113185004";
            findingsite.Value.CodeSystem = "2.16.840.1.113883.6.96";
            findingsite.Value.CodeSystemName = "SNOMED CT";
            findingsite.Value.DisplayName = "Skin between Fourth and Fifth Toes";
            CR<String> laterality = new CR<String>();
            laterality.Name = new CD<string>();
            laterality.Value = new CD<string>();
            findingsite.Value.Qualifier = new LIST<CR<string>>();
            findingsite.Value.Qualifier.Add(laterality);
            laterality.Name.Code = "272741003";
            laterality.Name.CodeSystem = "2.16.840.1.113883.6.96";
            laterality.Name.CodeSystemName = "SNOMED CT";
            laterality.Name.DisplayName = "Laterality";
            laterality.Value.Code = "7771000";
            laterality.Value.CodeSystem = "2.16.840.1.113883.6.96";
            laterality.Value.CodeSystemName = "SNOMED CT";
            laterality.Value.DisplayName = "Left";
            Assert.IsFalse(cd.Validate());
        }

        /// <summary>
        /// Ensure that validation succeds (returns TRUE)
        /// When Qualifier and Code are being populated
        /// </summary> 
        [TestMethod]
        public void CDQualifierCodeTest()
        {
            CD<String> cd = new CD<String>();
            cd.Code = "284196006";
            CR<String> severity = new CR<String>();
            severity.Name = new CD<string>();
            severity.Value = new CD<string>();
            cd.Qualifier = new LIST<CR<string>>();
            cd.Qualifier.Add(severity);
            severity.Name.Code = "246112005";
            severity.Name.CodeSystem = "2.16.840.1.113883.6.96";
            severity.Name.CodeSystemName = "SNOMED CT";
            severity.Name.DisplayName = "Severity";
            severity.Value.Code = "24484000";
            severity.Value.CodeSystem = "2.16.840.1.113883.6.96";
            severity.Value.CodeSystemName = "SNOMED CT";
            severity.Value.DisplayName = "Severe";
            CR<String> findingsite = new CR<String>();
            findingsite.Name = new CD<string>();
            findingsite.Value = new CD<string>();
            cd.Qualifier.Add(findingsite);
            findingsite.Name.Code = "363698007";
            findingsite.Name.CodeSystem = "2.16.840.1.113883.6.96";
            findingsite.Name.CodeSystemName = "SNOMED CT";
            findingsite.Name.DisplayName = "Finding site";
            findingsite.Value.Code = "113185004";
            findingsite.Value.CodeSystem = "2.16.840.1.113883.6.96";
            findingsite.Value.CodeSystemName = "SNOMED CT";
            findingsite.Value.DisplayName = "Skin between Fourth and Fifth Toes";
            CR<String> laterality = new CR<String>();
            laterality.Name = new CD<string>();
            laterality.Value = new CD<string>();
            findingsite.Value.Qualifier = new LIST<CR<string>>();
            findingsite.Value.Qualifier.Add(laterality);
            laterality.Name.Code = "272741003";
            laterality.Name.CodeSystem = "2.16.840.1.113883.6.96";
            laterality.Name.CodeSystemName = "SNOMED CT";
            laterality.Name.DisplayName = "Laterality";
            laterality.Value.Code = "7771000";
            laterality.Value.CodeSystem = "2.16.840.1.113883.6.96";
            laterality.Value.CodeSystemName = "SNOMED CT";
            laterality.Value.DisplayName = "Left";
            Assert.IsTrue(cd.Validate());
        }

        /// <summary>
        /// Ensure that validation fails (returns FALSE)
        /// When Qualifier, Code and NullFlavor are being populated
        /// </summary> 
        [TestMethod]
        public void CDQualifierCodeNullFlavorTest()
        {
            CD<String> cd = new CD<String>();
            cd.Code = "284196006";
            cd.NullFlavor = MARC.Everest.DataTypes.NullFlavor.NoInformation;
            CR<String> severity = new CR<String>();
            severity.Name = new CD<string>();
            severity.Value = new CD<string>();
            cd.Qualifier = new LIST<CR<string>>();
            cd.Qualifier.Add(severity);
            severity.Name.Code = "246112005";
            severity.Name.CodeSystem = "2.16.840.1.113883.6.96";
            severity.Name.CodeSystemName = "SNOMED CT";
            severity.Name.DisplayName = "Severity";
            severity.Value.Code = "24484000";
            severity.Value.CodeSystem = "2.16.840.1.113883.6.96";
            severity.Value.CodeSystemName = "SNOMED CT";
            severity.Value.DisplayName = "Severe";
            CR<String> findingsite = new CR<String>();
            findingsite.Name = new CD<string>();
            findingsite.Value = new CD<string>();
            cd.Qualifier.Add(findingsite);
            findingsite.Name.Code = "363698007";
            findingsite.Name.CodeSystem = "2.16.840.1.113883.6.96";
            findingsite.Name.CodeSystemName = "SNOMED CT";
            findingsite.Name.DisplayName = "Finding site";
            findingsite.Value.Code = "113185004";
            findingsite.Value.CodeSystem = "2.16.840.1.113883.6.96";
            findingsite.Value.CodeSystemName = "SNOMED CT";
            findingsite.Value.DisplayName = "Skin between Fourth and Fifth Toes";
            CR<String> laterality = new CR<String>();
            laterality.Name = new CD<string>();
            laterality.Value = new CD<string>();
            findingsite.Value.Qualifier = new LIST<CR<string>>();
            findingsite.Value.Qualifier.Add(laterality);
            laterality.Name.Code = "272741003";
            laterality.Name.CodeSystem = "2.16.840.1.113883.6.96";
            laterality.Name.CodeSystemName = "SNOMED CT";
            laterality.Name.DisplayName = "Laterality";
            laterality.Value.Code = "7771000";
            laterality.Value.CodeSystem = "2.16.840.1.113883.6.96";
            laterality.Value.CodeSystemName = "SNOMED CT";
            laterality.Value.DisplayName = "Left";
            Assert.IsFalse(cd.Validate());
        }

        /// <summary>
        /// Ensure that validation fails (returns FALSE)
        /// When Qualifier, Code and NullFlavor are being nullified
        /// </summary> 
        [TestMethod]
        public void CDQualifierCodeNullFlavorNullCheckTest()
        {
        CD<String> cd = new CD<String>();
        cd.Code = null;
        cd.NullFlavor = null;
        CR<String> warning = new CR<String>();
        warning.Name = new CD<string>();
        warning.Value = new CD<string>();
        warning.Value.Code = null;
        warning.Value.CodeSystem = null;
        warning.Name.Code = null;
        warning.Name.CodeSystem = null;
        cd.Qualifier = new LIST<CR<string>>();
        cd.Qualifier.Add(warning);
        cd.Validate();
        Assert.IsFalse(cd.Validate());
        }

        /// <summary>
        /// Ensure that validation fails (returns FALSE)
        /// When Qualifier, Code are being populated
        /// </summary> 
        [TestMethod]
        public void CDCodeQualifierTest()
        {
              CD<String> cd = new CD<String>();
              cd.Code = null;
              CR<String> severity = new CR<String>();
              severity.Name = new CD<string>();
              severity.Value = new CD<string>();
              severity.Name.Code = "23543256";
              severity.Name.CodeSystem = "13.21.432.45";
              severity.Value.Code = "2345235";
              severity.Value.CodeSystem = "23.132532.532";
              cd.Qualifier = new LIST<CR<string>>();
              cd.Qualifier.Add(severity);
              Assert.IsFalse(cd.Validate());
        }

        /// <summary>
        /// Tests the CD<String>class' ability to accept a string
        /// 
        /// Expected result: CD.Code = "TEST"
        /// </summary>
        [TestMethod]
        public void CDCastStringToCDTest()
        {
            CD<String> StringToCD = new CD<String>();

            // Cast a string to the CD.
            StringToCD = "TEST";

            // Test passes if the Code attribute of the CD<String>is the same as the string it was passed.
            Assert.AreEqual("TEST", StringToCD.Code.ToString());
        }

        /// <summary>
        /// Tests to see if we can Cast a CD<String>to a String
        ///  
        /// Expected Result: (String)CD<String>= "TEST"
        /// </summary>
        [TestMethod]
        public void CDCastCDtoStringTest()
        {
            CD<String>CDtoString = new CD<String>();
            CDtoString.Code = "TEST";

            // Test passes if the string representation of the CD<String>is "TEST"
            Assert.AreEqual("TEST", (string)CDtoString);
        }

        /// <summary>   
        /// Tests to see if we can Parse CD<String>(T) to a CD"/>
        /// 
        /// Expected Result: CD.Code = "NI"
        ///                  CD.Qualifier.Count = 1
        /// </summary>
        [TestMethod]
        public void CDParseCDofTTest()
        {
            CD<MARC.Everest.DataTypes.NullFlavor> parseCD= new CD<MARC.Everest.DataTypes.NullFlavor>();
            
            // Set the Code to NullFlavor.NoInformation, and add a qualifier and a translation.
            parseCD.Code = MARC.Everest.DataTypes.NullFlavor.NoInformation;
            parseCD.Qualifier = new LIST<CR<MARC.Everest.DataTypes.NullFlavor>>()
            {
                new CR<MARC.Everest.DataTypes.NullFlavor>() { 
                    Name = new CV<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.NotApplicable)
                }
            };
            parseCD.Translation = new SET<CD<MARC.Everest.DataTypes.NullFlavor>>(CD<MARC.Everest.DataTypes.NullFlavor>.Comparator)
            {
                new CD<MARC.Everest.DataTypes.NullFlavor>(MARC.Everest.DataTypes.NullFlavor.NotAsked)
            };

            // Parse the CD<NullFlavor> into a CD. 
            CD<String> result = Util.FromWireFormat(parseCD, typeof(CD<String> )) as CD<String>;
            
            // The test passes if the parsed CD<String>has a code of "NI", 1 qualifier and 1 Translation. 
            Assert.AreEqual("NI", result.Code.ToString());
            Assert.AreEqual(1, parseCD.Qualifier.Count);
            Assert.AreEqual(1, parseCD.Translation.Count);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void CDCastCDtoCDofTTest()
        {
            CD<String>castCD = new CD<String>();
            CD<MARC.Everest.DataTypes.NullFlavor> test = new CD<MARC.Everest.DataTypes.NullFlavor>();
            castCD.Code = "NI";

            // Cast CD<String>to CD<NullFlavor>. This should translate "NI" to NullFlavor.NoInformation.
            test = Util.Convert<CD<NullFlavor>>(castCD);

            // The test passes if the Code attribute of the CD<NullFlavor> is the same as NullFlavor.NoInformation.
            Assert.AreEqual(MARC.Everest.DataTypes.NullFlavor.NoInformation, (NullFlavor)test.Code);
        }

         
    }
}
