# Distance measurement service

System requirements: `asp.net core 2.1`

## Steps to run

Open folder `DistanceMeasurement\src\DistanceMeasurement` in command prompt.\
And run following command before starting\
\
`SET ASPNETCORE_ENVIRONMENT=Development`\
\
Then run\
\
`dotnet run --urls=http://localhost:5005/`\

# API methods

`GET api/distance/{from}/{to}` - return distance between airports in miles\
Where `from` and `to` IATA code of airport.
For example:\
`http://localhost:5005/api/distance/AMS/SVO`

