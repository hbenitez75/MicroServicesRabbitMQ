##Api Reporting

#The main goal of this project is to provide a GraphQl-based interface  that offers an easy and flexible method to query the data.
#This project takes advantage of HotChocolate for working with GraphQL. Hot Chocolate is an open-source .NET GraphQL platform that adheres to the most current GraphQL specifications. It serves as a wrapper around the GraphQL library, making building a full-fledged GraphQL server easier.

#Framework:
	-Net6.0

#Used Packages:
	-HotChocolate.AspNetCore 12.12.1
	-HotChocolate.AspNetCore.Authorization 12.12.1
	-HotChocolate.AspNetCore.Playground 10.5.5
	-Microsoft.AspNetCore.Authentication.JwtBearer 6.0.7 
	-Microsoft.EntityFrameworkCore.InMemory 6.0.7
	-Serilog 2.11.0

#Project Structure
	-Properties: Launch Settings
	-Data: Models, repositories and database context 
	-GrapQL: Resolvers, Types, Mutations and Queries
	-Messaging: RabbitMQ Configuration and Listeners
	-Services: All the project services
	-Appsettings.json
	-Program.cs 



