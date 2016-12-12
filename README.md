# Sample Azure Function App
Showing how to record data passed via HTTP trigger to Azure Table

`curl --retry 3 --connect-timeout 20 --max-time 30 -X POST -H "Content-Type: application/json" -d '{"subscriptionID":"123","deploymentName":"deployment1","product":"product1","vmName":"vm1","details":"details1"}' https://avfunction1.azurewebsites.net/api/AddDeployment?code=DzW0qCyzy4UgWacb9XYzy1zLS3dizGQqes7xyKRgiVX8aER5VW3fmA==`

![Azure Table](https://raw.githubusercontent.com/arsenvlad/azure-function-app1/master/images/storage.png)
