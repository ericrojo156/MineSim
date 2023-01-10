# MineSim

A simple mining simulation implemented by microservices in .Net framework

1) Build the .Net projects separately in DiamondMine, RubyMine, and Operations folders, which are the three microservices.
2) Copy Gateway.json to the Hexagon Gateway service folder (ensure Hexagon Gateway service is installed and running)
3) In DiamondMine, RubyMine, and Operations folders, run the command "dotnet run" to run each microservice.
4) Once all the microservices are running, use Postman to make the following requests:
* GET http://localhost:5006/api/min/rubymine, which will get a status summary of the Ruby Mine
* GET http://localhost:5007/api/min/diamondmine, which will get a status summary of the Diamond Mine
* POST http://localhost:5006/api/min/rubymine, which will excavate from the Ruby Mine (at the expense of consuming some mining equipment condition at the site)
* POST http://localhost:5007/api/min/diamondmine, which will excavate from the Diamond Mine (at the expense of consuming some mining equipment condition at the site)
* GET http://localhost:5008/api/min/operations, which will obtain a list of summaries from both mines (Ruby and Diamond)
