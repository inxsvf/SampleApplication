# Sample Application

## Main Dependencies

- .NET Core v5.0.0
- EntityFrameworkCore v5.0.1

## Starting Up and Usage

Open up the solution in Visual Studio, restore NuGet Packages, build and run the solution.

It may ask you for permission to install a temp certificate, accept. The server will run at https://localhost:5001 .

The solution uses [Swagger](https://swagger.io) (that is available through https://localhost:5001/swagger/index.html) so you can use it to view and try out the API. 

## API

With the project running, please go to [Project's Swagger](https://localhost:5001/swagger/index.html) for a better API description.

### Base url
```
https://localhost:5001
```

### Endpoints
#### List All People
```
GET /people
```

#### List Person
```
GET /people/{id}
```

#### Count All People
```
GET /people/totalcount
```

#### Create Person
```
POST /people
{
   "firstName": "Santa",
   "lastName": "Claus"
}
```

#### Update Personal Details
```
PUT /people
{
   "id": "1",
   "firstName": "Santa",
   "lastName": "Claus"
}
```

#### Delete Person
```
DELETE /people/{id}
```


