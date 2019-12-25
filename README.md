# Operations Software Solution

### Summary
The Operations Software Solution implements a [Domain Driven Design (DDD)](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/ddd-oriented-microservice) pattern to help solve enterprise level problems. 

#### Data/Infrastructure Layer
**Summary:** Uses Entity Framework Core to implement the [Repository design pattern](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design) and persist data.
* Entities
* Repositories
* Data Migrations

#### Domain Model Layer
**Summary:** Responsible for representing concepts of the business, information about the business situation, and business rules.
* Business Logic
* Models


#### Application Layer
**Summary:** Front end applications the user interacts with to delegate collaborations of domain objects to the Domain Layer.
* Administration
* Store
* Portfolio
