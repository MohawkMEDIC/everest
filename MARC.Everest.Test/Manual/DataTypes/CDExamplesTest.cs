using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MARC.Everest.DataTypes;
using MARC.Everest.DataTypes.Interfaces;
using MARC.Everest.Connectors;

namespace MARC.Everest.Test.DataTypes.Manual
{
    /// <summary>
    /// Summary description for CDExamplesTest
    /// </summary>
    [TestClass]
    public class CDExamplesTest
    {
        public CDExamplesTest()
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

        /* Example 14 */

        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Code            : The initial code.
        ///     Qualifier       : Contains a list of concept qualifier instances used to further
        ///                       increase the specificity of the primary code mnemonic.
        ///     CodeSystem      : The code system the code was picked from. The OID representing the system from which the code was drawn from
        ///     CodeSystemName  : The name of the code system 
        ///     DisplayName     : The display name for the code
        ///     
        /// And the following values are Nullfied:
        ///     NullFlavor
        /// </summary>
        [TestMethod]
        public void CDExampleTest01()
        {
            // Primary Code
            var burnCode = new CD<string>("284196006", "2.16.840.1.113883.6.96")
            {
                DisplayName = "Burn of Skin",
                CodeSystemName = "SNOMED-CT",
                CodeSystemVersion = "2009"
            };

            // Severity
            var severityQualifier = new CR<string>(
                new CV<String>("24112005", "2.16.840.1.113883.96"){
                    DisplayName = "Severity"
                },
                new CD<String>("24484000", "2.16.840.1.113883.96"){
                    DisplayName = "Severe"
                }
                );

            // Finding Site
            var findingSiteQualifier = new CR<String>(
                new CV<String>("363698007", "2.16.840.1.113883.96")
                {
                    DisplayName = "Finding Site"
                },
                new CD<String>("24484000", "2.16.840.1.113883.96")
                {
                    DisplayName = "Skin between fourth and fifth toes"
                }
            );

            // Laterality
            var lateralityQualifier = new CR<String>(
                new CV<String>("272741003", "2.16.840.1.113883.96") { DisplayName = "Laterality" },
                new CD<String>("77771000", "2.16.840.1.113883.96") { DisplayName = "Left Side" }
                );

            // Laterality applies to the finding site
            findingSiteQualifier.Value.Qualifier = new LIST<CR<string>>(){
                lateralityQualifier };

            // Finding site and severity applies to primary code
            burnCode.Qualifier = new LIST<CR<string>>()
            {
                severityQualifier,
                findingSiteQualifier
            };

            burnCode.NullFlavor = null;

            Assert.IsTrue(burnCode.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Code            : The initial code.
        ///     CodeSystem      : The code system the code was picked from. The OID representing the system from which the code was drawn from
        ///     CodeSystemName  : The name of the code system 
        ///     DisplayName     : The display name for the code
        ///     
        /// And the following values are Nullified:
        ///     Qualifier       : Contains a list of concept qualifier instances used to further
        ///                       increase the specificity of the primary code mnemonic.
        ///     NullFlavor
        ///                       
        /// Testing CD when qualifier is null and code is not null.
        /// </summary>
        [TestMethod]
        public void CDExampleTest02()
        {
            // Primary Code
            var burnCode = new CD<string>("284196006", "2.16.840.1.113883.6.96")
            {
                DisplayName = "Burn of Skin",
                CodeSystemName = "SNOMED-CT",
                CodeSystemVersion = "2009"
            };

            // Finding site and severity applies to primary code
            burnCode.Qualifier = null;

            Assert.IsTrue(burnCode.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return FALSE.
        /// When the following values are not Nullified:
        ///     CodeSystem      : The code system the code was picked from. The OID representing the system from which the code was drawn from
        ///     CodeSystemName  : The name of the code system 
        ///     DisplayName     : The display name for the code
        ///     Qualifier       : Contains a list of concept qualifier instances used to further
        ///                       increase the specificity of the primary code mnemonic.
        /// And the following values are Nullified:
        ///     Code            : The initial code.
        ///                       
        /// Testing CD when qualifier is not null and code is null.
        /// </summary>
        [TestMethod]
        public void CDExampleTest03()
        {
            // Primary Code
            var burnCode = new CD<string>("2.16.840.1.113883.6.96")
            {
                Code = null,
                DisplayName = "Burn of Skin",
                CodeSystemName = "SNOMED-CT",
                CodeSystemVersion = "2009"
            };

            // Finding site and severity applies to primary code
            burnCode.Qualifier = null;

            Assert.IsFalse(burnCode.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Code            : The initial code.
        ///     Qualifier       : Contains a list of concept qualifier instances used to further
        ///                       increase the specificity of the primary code mnemonic.
        ///     CodeSystem      : The code system the code was picked from. The OID representing the system from which the code was drawn from
        ///     CodeSystemName  : The name of the code system 
        ///     DisplayName     : The display name for the code
        ///     
        /// And the following values are Nullfied:
        ///     NullFlavor
        ///     
        /// Testind CD where Qualifier property is a empty list.
        /// </summary>
        [TestMethod]
        public void CDExampleTest04()
        {
            // Primary Code
            var burnCode = new CD<string>("284196006", "2.16.840.1.113883.6.96")
            {
                DisplayName = "Burn of Skin",
                CodeSystemName = "SNOMED-CT",
                CodeSystemVersion = "2009"
            };

            // Severity
            var severityQualifier = new CR<string>(
                new CV<String>("24112005", "2.16.840.1.113883.96")
                {
                    DisplayName = "Severity"
                },
                new CD<String>("24484000", "2.16.840.1.113883.96")
                {
                    DisplayName = "Severe"
                }
                );

            // Finding Site
            var findingSiteQualifier = new CR<String>(
                new CV<String>("363698007", "2.16.840.1.113883.96")
                {
                    DisplayName = "Finding Site"
                },
                new CD<String>("24484000", "2.16.840.1.113883.96")
                {
                    DisplayName = "Skin between fourth and fifth toes"
                }
            );

            // Laterality
            var lateralityQualifier = new CR<String>(
                new CV<String>("272741003", "2.16.840.1.113883.96") { DisplayName = "Laterality" },
                new CD<String>("77771000", "2.16.840.1.113883.96") { DisplayName = "Left Side" }
                );

            // Laterality applies to the finding site
            findingSiteQualifier.Value.Qualifier = new LIST<CR<string>>(){
                lateralityQualifier };

            // Finding site and severity applies to primary code.

            // Qualifier is set to empty list.
            burnCode.Qualifier = new LIST<CR<string>>();

            burnCode.NullFlavor = null;

            Assert.IsTrue(burnCode.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Code            : The initial code.
        ///     Qualifier       : Contains a list of concept qualifier instances used to further
        ///                       increase the specificity of the primary code mnemonic.
        ///     CodeSystem      : The code system the code was picked from. The OID representing the system from which the code was drawn from
        ///     CodeSystemName  : The name of the code system 
        ///     DisplayName     : The display name for the code
        ///     
        /// And the following values are Nullfied:
        ///     NullFlavor
        ///     
        /// Testing CD with no severity qualifier.
        /// </summary>
        [TestMethod]
        public void CDExampleTest06()
        {
            // Primary Code
            var burnCode = new CD<string>("284196006", "2.16.840.1.113883.6.96")
            {
                DisplayName = "Burn of Skin",
                CodeSystemName = "SNOMED-CT",
                CodeSystemVersion = "2009"
            };

            // Finding Site
            var findingSiteQualifier = new CR<String>(
                new CV<String>("363698007", "2.16.840.1.113883.96")
                {
                    DisplayName = "Finding Site"
                },
                new CD<String>("24484000", "2.16.840.1.113883.96")
                {
                    DisplayName = "Skin between fourth and fifth toes"
                }
            );

            // Laterality
            var lateralityQualifier = new CR<String>(
                new CV<String>("272741003", "2.16.840.1.113883.96") { DisplayName = "Laterality" },
                new CD<String>("77771000", "2.16.840.1.113883.96") { DisplayName = "Left Side" }
                );

            // Laterality applies to the finding site
            findingSiteQualifier.Value.Qualifier = new LIST<CR<string>>(){
                lateralityQualifier };

            // Finding site and severity applies to primary code
            burnCode.Qualifier = new LIST<CR<string>>()
            {
                findingSiteQualifier
            };

            burnCode.NullFlavor = null;

            Assert.IsTrue(burnCode.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Code            : The initial code.
        ///     Qualifier       : Contains a list of concept qualifier instances used to further
        ///                       increase the specificity of the primary code mnemonic.
        ///     CodeSystem      : The code system the code was picked from. The OID representing the system from which the code was drawn from
        ///     CodeSystemName  : The name of the code system 
        ///     DisplayName     : The display name for the code
        ///     
        /// And the following values are Nullfied:
        ///     NullFlavor
        ///     
        /// Testing CD with no FindSiteQualifier.
        /// </summary>
        [TestMethod]
        public void CDExampleTest07()
        {
            // Primary Code
            var burnCode = new CD<string>("284196006", "2.16.840.1.113883.6.96")
            {
                DisplayName = "Burn of Skin",
                CodeSystemName = "SNOMED-CT",
                CodeSystemVersion = "2009"
            };

            // Severity
            var severityQualifier = new CR<string>(
                new CV<String>("24112005", "2.16.840.1.113883.96")
                {
                    DisplayName = "Severity"
                },
                new CD<String>("24484000", "2.16.840.1.113883.96")
                {
                    DisplayName = "Severe"
                }
                );

            // Finding site and severity applies to primary code
            burnCode.Qualifier = new LIST<CR<string>>()
            {
                severityQualifier
            };

            burnCode.NullFlavor = null;

            Assert.IsTrue(burnCode.Validate());
        }


        /// <summary>
        /// Testing Function Validate must return TRUE.
        /// When the following values are not Nullified:
        ///     Code            : The initial code.
        ///     Qualifier       : Contains a list of concept qualifier instances used to further
        ///                       increase the specificity of the primary code mnemonic.
        ///     CodeSystem      : The code system the code was picked from. The OID representing the system from which the code was drawn from
        ///     CodeSystemName  : The name of the code system 
        ///     DisplayName     : The display name for the code
        ///     
        /// And the following values are Nullfied:
        ///     NullFlavor
        ///     
        /// Testind CD given FindingSiteQualifier without Laterality Qualifier.
        /// </summary>
        [TestMethod]
        public void CDExampleTest08()
        {
            // Primary Code
            var burnCode = new CD<string>("284196006", "2.16.840.1.113883.6.96")
            {
                DisplayName = "Burn of Skin",
                CodeSystemName = "SNOMED-CT",
                CodeSystemVersion = "2009"
            };

            // Severity
            var severityQualifier = new CR<string>(
                new CV<String>("24112005", "2.16.840.1.113883.96")
                {
                    DisplayName = "Severity"
                },
                new CD<String>("24484000", "2.16.840.1.113883.96")
                {
                    DisplayName = "Severe"
                }
                );

            // Finding Site
            var findingSiteQualifier = new CR<String>(
                new CV<String>("363698007", "2.16.840.1.113883.96")
                {
                    DisplayName = "Finding Site"
                },
                new CD<String>("24484000", "2.16.840.1.113883.96")
                {
                    DisplayName = "Skin between fourth and fifth toes"
                }
            );

            // Laterality
            var lateralityQualifier = new CR<String>(
                new CV<String>("272741003", "2.16.840.1.113883.96") { DisplayName = "Laterality" },
                new CD<String>("77771000", "2.16.840.1.113883.96") { DisplayName = "Left Side" }
                );

            /*
             Laterality NOT applied to the finding site.
             findingSiteQualifier.Value.Qualifier = new LIST<CR<string>>(){
                lateralityQualifier };
            */

            // Finding site and severity applies to primary code
            burnCode.Qualifier = new LIST<CR<string>>()
            {
                severityQualifier,
                findingSiteQualifier
            };

            burnCode.NullFlavor = null;

            Assert.IsTrue(burnCode.Validate());
        }
    }
}
