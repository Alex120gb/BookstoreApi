# BookstoreApi
This is the API project of the bookstore project.

To use it you have to consider a few things:
For either case, either local or with the docker-compose file that I will provide within this repository, after running the compose file (pretty much needed for the SQL Server) you will also need to create the database - I have added in the project an sql script which not only contains the creation of the database, but of course also the tables AND some data to use!
Also before running the docker you will have to download my two repositories from docker (first do this!)
1) Api: docker pull alexisk120/bookapi
2) WebApp: docker pull alexisk120/bookwebapp

Location for docker-compose and sql script:
1) Sql script location: Database_setup_script in the main direcotry of BookstoreApi project
2) Docker-compose.yml location: Docker_compose_file in the main direcotry of BookstoreApi project

Once you downloaded/pulled the two docker repositoires, open up a command window and go to where you have stored in your machine the docker-compose file (yes you can move the file somewhere more convinient for you) - once you are in the directory with the docker-compose file just run this command: docker-compose up
With this the compose container will be created - after that initialize the database SQL Server and you should be good to go!

Note: Remember to use dbForge database managing tool for the SQL Server to use the scrpit:
![image](https://github.com/Alex120gb/BookstoreApi/assets/93439743/45ca098e-11f4-4270-adf7-e71bdb875741)

The password in the image is as follows - B@@k2toR3S3rVer - this is also what the docker-compose file initilazes!

-------------------------------------------------------------------------------------------------------------------------------------------

If you want to use this locally, you still need to create the database, again use the docker-compose file, and stop the other two containers (webapp and api - not really needed) and leave the SQL Server container running. Again you will have to fill up the database with the database, its tables and data.
Remember to change the connection string in the startup to the one I provided in comments so that your local api can communicate with the SQL Server created with the use of docker!

Here is again the connection string: Server=127.0.0.1,1433;Database=Bookstore;User=sa;Password=B@@k2toR3S3rVer;TrustServerCertificate=true;

-------------------------------------------------------------------------------------------------------------------------------------------

A few more things regarding the api:
Once you set it up as I mentioned above, one thing that you can do is after clicking on the link that the docker provides for the api like seen here in the picture below:
![image](https://github.com/Alex120gb/BookstoreApi/assets/93439743/3e9a2ac9-3b9d-4692-a22f-de4b4589e1a4)
try the /health endpoint to see if the api is healthy or not!

Here is an example: 

![image](https://github.com/Alex120gb/BookstoreApi/assets/93439743/324a242c-9153-4927-9931-99ee7053149e)

After the the health check - or if you didn't which is fine write on the url after the localhost:8080 - type in the /swagger so that the swagger interface appears - once you open that interface to use the api you first need to generate a token to authenticate! To generate a token, simply use the login endpoint (if you have no account then of course
first create one and use that to login) and after successfuly logging in a token will be generated in the response body - here is an example in the image: 
![image](https://github.com/Alex120gb/BookstoreApi/assets/93439743/3fbfd43c-0657-4685-807d-ac3962abd836)

Once you generate a token, copy it and you have to do two things - first authorize youself here 
![image](https://github.com/Alex120gb/BookstoreApi/assets/93439743/2e85d035-9bc9-4d55-9c2c-be416efa43c4)
by pasting the token in the value input box. Then once you authorize your self, you will notice a header input box in every  books api endpoint much like this:
![image](https://github.com/Alex120gb/BookstoreApi/assets/93439743/3fea3877-b579-41d6-97b6-c13387c48300)
you will also need to past the token in this header so that the api can validate with its own authorized token (the one with which you authorized yourself) BUT PLEASE DO NOT DELETE THE - Bearer - WORD IN THE HEADER INPUT - just press space once and paste the token after the Bearer - after that feel free to use the endpoint!

