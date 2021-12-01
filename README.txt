Musicalog Notes
---------------

> The projects are .Net 6, created in Visual Studio 2022.

> There is a backup of the database in the 'Additional' folder, the database has a built-in administrator 'MusicalogUser', password: 'password'.

> You'll need to change the connection strings in the application to point to your SQL server, I was using SQL-VM1.

> I added a web front-end project to demonstrate the use of the API, however this is not exhaustative and since it's not in scope I decided to leave it incomplete but available.

> If you 'run' aat the solution level both the API and Web projects should start in the correctt order.

> The endpoints can be tested using the built-in swagger web interface

> I did not make provisions for Albums which might be collaborations betyween multiple artists, although the was considered, instead a new Artist should be created. To implement this I would have had to normalise the resuilting manmy-to-many relationship with and additional table.

> Deleting an Artist with cascade-delete all associate Albums, this does not apply in reverse.

> Although the database generation model was essentially created when I imported the schema, this was created DB-first.

> On the Get() endpoints for Albums and Artists there are two parameters...

The first allows you to specifiy whether or not the entire relational object-graph should be retrieved, or, if this isn't required the query can the optimised by setting the to false.

The second is a keypair string dictionary which can be used to specify filters, in this casse we're only using a combination of Album Title and Artist Name, however this allows for additional filters to be added in the future.

NOTE: The dictionary is sent in Json format as the request body of the GET request, this is not entirely compliant with the recommendations in RFC 2616 section 4.3 (https://www.rfc-editor.org/rfc/rfc2616#section-4.3).

> Extensive use has been made of the async/await pattern where appropriate, also generics to reduce the code-base and the dependency-injection model for instantiation.

>The various abstration layers are as folllows:

	> Database
	> Repository / Model
	> Service
	> Controller (API)
	> Client

Thanks you.