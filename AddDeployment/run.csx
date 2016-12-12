#r "Microsoft.WindowsAzure.Storage"

using System.Net;
using Microsoft.WindowsAzure.Storage.Table;

public static HttpResponseMessage Run(Deployment deployment, CloudTable outTable, TraceWriter log)
{
    // Updated via GitHub 12/11/2016 19:53 CST
    // curl -X POST -H "Content-Type: application/json" -d '{"subscriptionID":"123","deploymentName":"deployment1","product":"product1","vmName":"vm1","details":"details1"}' https://avfunction1.azurewebsites.net/api/AddDeployment?code=DzW0qCyzy4UgWacb9XYzy1zLS3dizGQqes7xyKRgiVX8aER5VW3fmA==

    if (string.IsNullOrEmpty(deployment.SubscriptionID))
    {
        return new HttpResponseMessage(HttpStatusCode.BadRequest)
        {
            Content = new StringContent("A non-empty subscriptionID must be specified.")
        };
    };

    if (string.IsNullOrEmpty(deployment.DeploymentName))
    {
        return new HttpResponseMessage(HttpStatusCode.BadRequest)
        {
            Content = new StringContent("A non-empty deploymentName must be specified.")
        };
    };

    deployment.PartitionKey = DateTime.UtcNow.ToString("yyyyMM");
    deployment.RowKey = Guid.NewGuid().ToString(); 
    
    log.Info($"RowKey={deployment.RowKey}, SubscriptionID={deployment.SubscriptionID}, DeploymentName={deployment.DeploymentName}, Product={deployment.Product}, VMName={deployment.VMName}");
    
    TableOperation updateOperation = TableOperation.InsertOrReplace(deployment);
    TableResult result = outTable.Execute(updateOperation);    

    if (result.HttpStatusCode >= 200 && result.HttpStatusCode <= 299)
    {
        return new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(deployment.RowKey)
        };
    }
    else return new HttpResponseMessage((HttpStatusCode)result.HttpStatusCode); 
}

public class Deployment : TableEntity
{
    public string SubscriptionID { get; set; }
    public string DeploymentName { get; set; }
    public string Product { get; set; }
    public string VMName { get; set; }
    public string Details {get; set; }
}
