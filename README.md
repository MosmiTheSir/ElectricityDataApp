# Project Description


**Create an application that retrieves public electricity data and stores aggregated data into a database. Expose a GET API endpoint where aggregated data can be retrieved.**

## Requirements:

- Utilize the datasets available at:
  - https://data.gov.lt/dataset/siame-duomenu-rinkinyje-pateikiami-atsitiktinai-parinktu-1000-buitiniu-vartotoju-automatizuotos-apskaitos-elektriniu-valandiniai-duomenys.
- Use an HTTP Client to download the datasets from the following URLs:
  - https://data.gov.lt/dataset/1975/download/10766/2022-05.csv
  - https://data.gov.lt/dataset/1975/download/10765/2022-04.csv
  - https://data.gov.lt/dataset/1975/download/10764/2022-03.csv
  - https://data.gov.lt/dataset/1975/download/10763/2022-02.csv
- Process the data for the last four months.
- Filter the data to include only apartment (Butas) records.
- Store the data into a database, grouping it by the Tinklas (Regionas) field, and apply the following aggregations:
    - Sum of P+ fields
    - Sum of P- fields
- Use Dapper or EF Core for communication with the database.
- Implement logging functionality.
- Write unit tests for the main flow.
- The application must run in a Docker container.
