# ImageUploaderAPI

This a .NET 6 project.

##  Setup

.NET 6 version, SQL Server.

##  Details

1)  The project consists of one Controller i.e. ImageController with methods POST & GET Methods to save Image & Image Caption.

2)  The database being used is SQL Server DB.

3)  Database is being data migration. A migration is already generated; use command 'update-database' in Package Manager Console to push migration.

4)  Image and Image Caption is being saved in database as indicated in test requirements.

5)  Image is also being saved in local repository to indicate second methodology of sending required Image URIs to front-end mirroring the concept of file server instead

6)  For task at hand byte array along with caption fetched from database is sent to front-end for image gallery display purpose.

# End User change case;

  End user has to change local Database connection in config file i.e. appsettings.json
