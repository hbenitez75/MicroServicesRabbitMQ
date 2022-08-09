##Api Billable Transaction

#The main goal of this project is to provide an interface in order to manage all system transactions, every transaction generated is sended to another system that will process it.
#This project  uses a simple object mapper framework (Dapper)  that provides a high performance data access system built.

#Framework:
	-Net6.0

#Used Packages:
	-Dapper 2.0.123
	-Microsoft.AspNetCore.Authentication.JwtBearer 6.0.7
	-Microsoft.Data.Sqlite 6.0.7
	-Microsoft.Extensions.DependencyInjection 6.0.0
	-RabbitMQ.Client 6.4.0

#Project Structure
	-Properties: Launch Settings
	-Controllers: Transaction controllers
	-Messaging: RabbitMQ Configuration and Messaging Clases
	-Transaction Manager: Transaction Classes and Data
	-Appsettings.json
	-Program.cs 



