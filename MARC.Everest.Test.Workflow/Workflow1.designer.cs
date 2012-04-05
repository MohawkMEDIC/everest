using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Reflection;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

namespace MARC.Everest.Test.Workflow
{
    partial class Workflow1
    {
        #region Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCode]
        private void InitializeComponent()
        {
            this.CanModifyActivities = true;
            System.Workflow.ComponentModel.ActivityBind activitybind1 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding1 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind2 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding2 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind3 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding3 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind4 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding4 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind5 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding5 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            this.outputCode = new System.Workflow.Activities.CodeActivity();
            this.MessageConstructor = new MARC.Everest.Workflow.Actions.Constructor.MessageConstructorActivity();
            this.developParameters = new System.Workflow.Activities.CodeActivity();
            // 
            // outputCode
            // 
            this.outputCode.Name = "outputCode";
            this.outputCode.ExecuteCode += new System.EventHandler(this.outputCode_ExecuteCode);
            // 
            // MessageConstructor
            // 
            activitybind1.Name = "Workflow1";
            activitybind1.Path = "MessageConstructor_Title1";
            workflowparameterbinding1.ParameterName = "Title";
            workflowparameterbinding1.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            activitybind2.Name = "Workflow1";
            activitybind2.Path = "everestShapeResult";
            workflowparameterbinding2.ParameterName = "(Result)";
            workflowparameterbinding2.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            activitybind3.Name = "Workflow1";
            activitybind3.Path = "administrativeGender";
            workflowparameterbinding3.ParameterName = "AdministrativeGender";
            workflowparameterbinding3.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            activitybind4.Name = "Workflow1";
            activitybind4.Path = "personName";
            workflowparameterbinding4.ParameterName = "PersonName";
            workflowparameterbinding4.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
            activitybind5.Name = "Workflow1";
            activitybind5.Path = "requestor";
            workflowparameterbinding5.ParameterName = "Requestor";
            workflowparameterbinding5.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind5)));
            this.MessageConstructor.MessageParameters.Add(workflowparameterbinding1);
            this.MessageConstructor.MessageParameters.Add(workflowparameterbinding2);
            this.MessageConstructor.MessageParameters.Add(workflowparameterbinding3);
            this.MessageConstructor.MessageParameters.Add(workflowparameterbinding4);
            this.MessageConstructor.MessageParameters.Add(workflowparameterbinding5);
            this.MessageConstructor.MessageType = typeof(MARC.Everest.RMIM.CA.R020403.Interactions.PRPA_IN101103CA);
            this.MessageConstructor.Name = "MessageConstructor";
            // 
            // developParameters
            // 
            this.developParameters.Name = "developParameters";
            this.developParameters.ExecuteCode += new System.EventHandler(this.developParameters_ExecuteCode);
            // 
            // Workflow1
            // 
            this.Activities.Add(this.developParameters);
            this.Activities.Add(this.MessageConstructor);
            this.Activities.Add(this.outputCode);
            this.Name = "Workflow1";
            this.CanModifyActivities = false;

        }

        #endregion

        private CodeActivity developParameters;
        private CodeActivity outputCode;
        private MARC.Everest.Workflow.Actions.Constructor.MessageConstructorActivity MessageConstructor;












    }
}
