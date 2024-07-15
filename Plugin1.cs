using Microsoft.Xrm.Sdk;
using System;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using MyApps.Model;

namespace crmplugin
{
 public class MyPlugin : PluginBase
{
  // Constructor
  public MyPlugin(string unsecureConfiguration, string secureConfiguration)
       : base(typeof(MyPlugin))
  { }

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
      tracingService.Trace("Entity name {0}",entity.LogicalName);
               // entity.Attributes.Contains("")
             if (entity.LogicalName == "account" && upper(context.MessageName) == "UPDATE" && entity.Attributes.Contains("NAME")) 
              {
                    throw new InvalidPluginExecutionException("Cannot update name");
                }
              else if (entity.LogicalName == "account" && upper(context.MessageName) == "CREATE")
              {
                  tracingService.Trace("Account created suscessfuly");  
              }
              else
              { 
                return;
              }
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
