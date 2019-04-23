# CompanyAPI
.NetCore basic Web app that performs CRUD operations for a company data using MongoDB as a storage client

# Functional Specifications:
CompanyAPI is built on .Net Core. Given the following supported actions:

1. Creates a Company record specifying the Name, Stock Ticker, Exchange, Isin, and optionally a website url. Isin is Unique and the first two characters of an ISIN must be letters / non numeric.

2. Retrieve an existing Company by Id

3. Retrieve a Company by ISIN

4. Retrieve a collection of all Companies

5. Update an existing Comp

# How it Works:
Project is built with the following technologies, in order to run the API live these must be configured first
- Visual Studio 2017
- .Net core v2.1
- MongoDB v2.8.0

### MongoDB installations:

- Configure MongoDB
  If using Windows, MongoDB is installed at C:\Program Files\MongoDB by default. Add C:\Program Files\MongoDB\Server\<version_number>\bin to the Path environment variable. This change enables MongoDB access from anywhere on your development machine.
  
- Create a MongoDB database
  Open mongo shell and run the following command, Mongo would run on default port 27017
  ```
  mongod --dbpath <data_directory_path>
  ```
  Open another shell and type the folowing command to start playing with mongo database:
  ```
  mongo
  ```
- Define a MongoDB collection and schema
  Select your database first as:
  ```
  use CompanyStoreDb
  ```
  Then create a collection (Entity or Table) to store *Company* type of objects:
  ```
  db.createCollection('Companies')
  ```
  Enlist the available tables in your schema as:
  ```
  show collecctions
  ```
  You must see a response like
  ```
  Companies
  ```
- Perform MongoDB CRUD operations from a web API
  Insert few records into the table *companies* by running the following command:
  ```JSON
  db.Companies.insertMany([
  {'Isin':'AB12345','Name':'Orange Inc','StockTicker':'Org','Exchange':'Computers','Website':'www.orange.org'},
  {'Isin':'AB123467','Name':'Glass Lewis','StockTicker':'GLewis','Exchange':'Computers'}
  ])
  ```
  If success, there would be areponse as below:
  ```JSON
  {
        "acknowledged" : true,
        "insertedIds" : [
                ObjectId("5cbaea74ace51b4c9ef6b04b"),
                ObjectId("5cbaea74ace51b4c9ef6b04c")
        ]
  }
  ```
  Run the command below to see the recently added records to the database:
  ```
  db.Companies.find({}).pretty()
  ```

# API Invocations via PostMan Client:
As you run the application in IIS Express or Google Chrome it would be running on a port that is made available by OS: for-example port 44368 was made available for me by my OS at the time of development.
1. GET:
  Request: http://localhost:44368/api/companies

  Response:
  ```
  [
      {
          "id": "5cbaea74ace51b4c9ef6b04b",
          "isin": "AB12345",
          "name": "Orange Inc",
          "stockTicker": "Org",
          "exchange": "Computers",
          "website": "www.orange.org"
      },
      {
          "id": "5cbaea74ace51b4c9ef6b04c",
          "isin": "AB123467",
          "name": "Glass Lewis",
          "stockTicker": "GLewis",
          "exchange": "Computers"
      },
      {
          "id": "5cbd131b8e2b8954cc69592d",
          "isin": "AB123435",
          "name": "Honda",
          "stockTicker": "CG",
          "exchange": "Bike",
          "website": "www.honda.org"
      }
  ]
  ```
  
2. GET by Id:
  
  Request: https://localhost:44368/api/Companies/GetCompanyById?Id=5cbaea74ace51b4c9ef6b04c

  Response:
  ```
  {
    "id": "5cbaea74ace51b4c9ef6b04c",
    "isin": "AB123467",
    "name": "Glass Lewis",
    "stockTicker": "GLewis",
    "exchange": "Computers"
  }
  ```
    
3. GET by Isin:
  
  Request: https://localhost:44368/api/Companies/GetCompanyByIsin?isin=AB12345

  Response: 
  ```
  {
    "id": "5cbaea74ace51b4c9ef6b04b",
    "isin": "AB12345",
    "name": "Orange Inc",
    "stockTicker": "Org",
    "exchange": "Computers",
    "website": "www.orange.org"
  }
  ```
    
4. POST:
  
  Request: https://localhost:44368/api/companies/
  and pass the body as a JSON object in a POST raw request body:
  
  ```
  {
      "Isin": "AB123",
      "Name": "Renault Inc",
      "StockTicker": "Ren",
      "Exchange": "Auto mobiles",
      "Website": "www.renault.ie"
  }
  ```

5. PUT:
  
  Request: https://localhost:44368/api/companies/
  and pass the body as a JSON object in a PUT raw request body:
  
  ```
  {
      "Isin": "AB123435",
      "Name": "Honda",
      "StockTicker": "CG",
      "Exchange": "Bike and cars",
      "Website": "www.honda.org"
  }
  ```
6. DELETE
  
  Request: https://localhost:44368/api/Companies?Isin=AB123435

  Response: Accepted or Not Found
