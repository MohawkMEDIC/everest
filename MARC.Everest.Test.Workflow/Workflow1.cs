using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using MARC.Everest.Interfaces;
using MARC.Everest.RMIM.CA.R020403.Vocabulary;
using MARC.Everest.DataTypes;
using MARC.Everest.RMIM.CA.R020403.Interactions;

namespace MARC.Everest.Test.Workflow
{
    public sealed partial class Workflow1 : SequentialWorkflowActivity
    {
        public Workflow1()
        {
            InitializeComponent();
        }


        public IGraphable everestShapeResult = null;

        /// <summary>
        /// Output the code
        /// </summary>
        private void outputCode_ExecuteCode(object sender, EventArgs e)
        {
            var fmtr = new MARC.Everest.Formatters.XML.ITS1.Formatter();
            fmtr.GraphAides.Add(typeof(MARC.Everest.Formatters.XML.Datatypes.R1.Formatter));
            fmtr.ValidateConformance = false;
            fmtr.GraphObject(Console.OpenStandardOutput(), everestShapeResult);
        }

        public MARC.Everest.DataTypes.CV<MARC.Everest.RMIM.CA.R020403.Vocabulary.AdministrativeGender> administrativeGender = new MARC.Everest.DataTypes.CV<MARC.Everest.RMIM.CA.R020403.Vocabulary.AdministrativeGender>();

        /// <summary>
        /// Add parameters
        /// </summary>
        private void developParameters_ExecuteCode(object sender, EventArgs e)
        {
            MARC.Everest.RMIM.CA.R020403.Interactions.REPC_IN000076CA p = new MARC.Everest.RMIM.CA.R020403.Interactions.REPC_IN000076CA();
            administrativeGender = AdministrativeGender.Male;
            personName.Add(new MARC.Everest.DataTypes.PN(
                EntityNameUse.Search,
                new ENXP[] { 
                    new ENXP("Smith", EntityNamePartType.Family),
                    new ENXP("John", EntityNamePartType.Given)
                }
            ));
            requestor = new MARC.Everest.RMIM.CA.R020403.MCAI_MT700220CA.ResponsibleParty(
                new MARC.Everest.RMIM.CA.R020403.COCT_MT090102CA.AssignedEntity(
                    new SET<II>(new II("1.1.1.1.1", "1234")),
                    new MARC.Everest.RMIM.CA.R020403.COCT_MT090108CA.Person(
                        new PN(
                            new ENXP[] {
                                new ENXP("Nurse", EntityNamePartType.Family),
                                new ENXP("Nancy", EntityNamePartType.Given)
                            }
                        )
                    )
                )
            );
        }

        public System.Collections.Generic.List<MARC.Everest.DataTypes.PN> personName = new System.Collections.Generic.List<MARC.Everest.DataTypes.PN>();
        public MARC.Everest.RMIM.CA.R020403.MCAI_MT700220CA.ResponsibleParty requestor = new MARC.Everest.RMIM.CA.R020403.MCAI_MT700220CA.ResponsibleParty();
    }

}
