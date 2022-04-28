using System.Threading.Tasks;
using Pulumi;
using Pulumi.AzureNative.Resources;
using Pulumi.AzureNative.Web;
using Pulumi.AzureNative.Web.Inputs;

class MyStack : Stack
{
    public MyStack()
    {
        // Create an Azure Resource Group
        var resourceGroup = new ResourceGroup("pulumitest");


        var appServicePlan = new AppServicePlan("pulumiasp", new AppServicePlanArgs
        {
            ResourceGroupName = resourceGroup.Name,
            Kind = "App",
            Sku = new SkuDescriptionArgs
            {
                Tier = "Free",
                Name = "F1",
            },
        });

        var app = new WebApp("pulumiapp", new WebAppArgs
        {
            ResourceGroupName = resourceGroup.Name,
            ServerFarmId = appServicePlan.Id
        });

        this.ResourceGroupName = resourceGroup.Name;
        this.AppServiceName = app.Name;
    }

    [Output] public Output<string> ResourceGroupName { get; set; }
    [Output] public Output<string> AppServiceName { get; set; }
}
