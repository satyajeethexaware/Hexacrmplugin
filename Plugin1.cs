using Microsoft.Xrm.Sdk;
using System;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using MyApps.Model;

namespace crmplugin
{
    /// <summary>
    /// Plugin development guide: https://docs.microsoft.com/powerapps/developer/common-data-service/plug-ins
    /// Best practices and guidance: https://docs.microsoft.com/powerapps/developer/common-data-service/best-practices/business-logic/
    /// </summary>
    public class Plugin1 : PluginBase
    {
        public Plugin1(string unsecureConfiguration, string secureConfiguration)
            : base(typeof(Plugin1))
        {
            // TODO: Implement your custom configuration handling
            // https://docs.microsoft.com/powerapps/developer/common-data-service/register-plug-in#set-configuration-data
        }

        // Entry point for custom business logic execution
        protected override void ExecuteDataversePlugin(ILocalPluginContext localPluginContext)
        {
            if (localPluginContext == null)
            {
                throw new ArgumentNullException(nameof(localPluginContext));
            }

              var context        = localPluginContext.PluginExecutionContext;
              var serviceFactory = localPluginContext.OrgSvcFactory;
              var tracingService = localPluginContext.TracingService;
                         

             try
            {
              Entity entity = (Entity)context.InputParameters["Target"];
              if (entity.LogicalName != "account" )
              {
                return;
              }
              else if (entity.LogicalName == "account" && context.MessageName == "update") 
              {
                    tracingService.Trace("Account updated suscessfuly.");  
              }
              else if (entity.LogicalName == "account" && context.MessageName == "create")
              {
                  tracingService.Trace("Account created suscessfuly");  
              }
              else{ return;}
               //Account acct = context.InputParameters["Target"].ToEntity<Account>();
            // TODO Plug-in business logic goes here. You can access data in the context,
            // and make calls to the Organization web service using the Dataverse SDK.
            
            }
            catch (FaultException<OrganizationServiceFault> ex)
            {
             tracingService.Trace("An exception occurred in MyPlugin: {0}", ex.ToString());     
             throw new InvalidPluginExecutionException("The following error occurred in MyPlugin.", ex);
            }
            catch (Exception ex)
            {
                tracingService.Trace("MyPlugin: error: {0}", ex.ToString());
                throw;
            }
        }
    }
}
