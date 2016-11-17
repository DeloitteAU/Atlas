using System;
using DeloitteDigital.Atlas.Extensions;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.SecurityModel;
using Sitecore.Workflows.Simple;

namespace DeloitteDigital.Atlas.Multisite.Workflow
{
    public class ApplyWorkflowAction
    {
        public void Process(WorkflowPipelineArgs args)
        {
            //this is the item being operated on
            var dataItem = args.DataItem;

            //this is the actual 'action' item in the tree
            var actionItem = args.ProcessorItem.InnerItem;


            // workflow id and state (if no settings found then delete triage workflow from item)
            var workflowId = string.Empty;
            var workflowState = string.Empty;

            // ENHANCEMENT - rather than configuring this in Sitecore, create a provider abstraction and allow this to be 
            // configured via a configuration file. 

            // find workflow from triage workflow config item
            var triageWorkflowConfigItemsField = "TriageWorkflowConfigItems"; // TODO - this should idally be made configurable. 
            var workflowConfigItems = actionItem.GetFieldValueAsItem(triageWorkflowConfigItemsField).GetChildren();
            foreach (Item item in workflowConfigItems)
            {
                if (
                    // TODO - this should ideally be made configurable 
                    !dataItem.Paths.FullPath.StartsWith(
                        item.GetFieldValue("StartPath"),
                        StringComparison.InvariantCultureIgnoreCase))
                {
                    continue;
                }
                // TODO - this should ideally be made configurable 
                var targetWorkflow = item.GetFieldValueAsItem("Workflow");
                if (targetWorkflow != null)
                {
                    workflowId = targetWorkflow.ID.ToString();
                    workflowState = targetWorkflow.GetFieldValue("Initial state"); // Sitecore field
                }
                break;
            }

            // apply target workflow
            using (new SecurityDisabler())
            {
                using (new EditContext(dataItem))
                {
                    dataItem.Fields[FieldIDs.Workflow].Value = workflowId;
                    dataItem.Fields[FieldIDs.WorkflowState].Value = workflowState;
                    dataItem.Editing.AcceptChanges();
                }
            }
        }
    }
}
