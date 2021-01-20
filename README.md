# Distance measurement service

System requirements: `asp.net core 2.1`

## Steps to run

Open folder `src\DistanceMeasurement` in command prompt.\
And run following command:\
\
`SET ASPNETCORE_ENVIRONMENT=Development`\
\
Then run:\
\
`dotnet run --urls=http://localhost:5005/`

## API methods

`GET api/distance/{from}/{to}` - returns the distance between airports in miles.\
Where `from` and `to` IATA are the code of airport.\
For example, the distance between Amsterdam Airport Schiphol and Sheremetyevo International Airport:\
`http://localhost:5005/api/distance/AMS/SVO`\
Will return result in miles\
`1332.5557879530566`

