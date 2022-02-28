--------------------------------------------------------------------
-- EXEC sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'
-- EXEC sp_msforeachtable 'DROP TABLE ?'
/*
ALTER TABLE AspNetUsers NOCHECK CONSTRAINT all
ALTER TABLE AspNetRoles NOCHECK CONSTRAINT all
ALTER TABLE AspNetUserClaims NOCHECK CONSTRAINT all
ALTER TABLE AspNetUserRoles NOCHECK CONSTRAINT all
ALTER TABLE AspNetUserLogins NOCHECK CONSTRAINT all
DROP TABLE AspNetUserLogins
DROP TABLE AspNetUserRoles
DROP TABLE AspNetUserClaims
DROP TABLE AspNetRoles
DROP TABLE AspNetUsers
DROP TABLE __MigrationHistory
*/
--------------------------------------------------------------------

/*
SELECT * FROM AspNetUsers
SELECT * FROM AspNetRoles
SELECT * FROM AspNetUserRoles

SELECT * FROM AspNetUserClaims

SELECT * FROM AspNetUserLogins



TRUNCATE TABLE AspNetUserLogins
TRUNCATE TABLE AspNetUserRoles
TRUNCATE TABLE AspNetUsers


*/


