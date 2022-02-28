-- SELECT * FROM AspNetUsers
-- SELECT * FROM LookupHobby
IF EXISTS (SELECT * FROM sysobjects WHERE id=object_id('dbo.StudentHobby') and sysstat & 0xf = 3)
	DROP TABLE dbo.StudentHobby
GO
IF EXISTS (SELECT * FROM sysobjects WHERE id=object_id('dbo.Student') and sysstat & 0xf = 3)
	DROP TABLE dbo.Student
GO
CREATE TABLE dbo.Student
(
	-- NOTE: StudentBackground and StudentImage is handled separatedly
	ID INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Student PRIMARY KEY NONCLUSTERED,

	AspNetUsersID NVARCHAR(128) NOT NULL DEFAULT '',

	NickName VARCHAR(100) NOT NULL DEFAULT '',

	Biography NVARCHAR(200) NOT NULL DEFAULT '',
	University NVARCHAR(200) NOT NULL DEFAULT '',
	Course NVARCHAR(200) NOT NULL DEFAULT '',
	StudyYear NVARCHAR(100) NOT NULL DEFAULT '',
	Location NVARCHAR(200) NOT NULL DEFAULT '',
	FunFact NVARCHAR(200) NOT NULL DEFAULT '',

	StudentBackground NVARCHAR(200) NOT NULL DEFAULT '',
	StudentImage NVARCHAR(200) NOT NULL DEFAULT '',
	DateJoined DATETIME NOT NULL DEFAULT GETDATE(),

	DateTimeStamp DATETIME NOT NULL DEFAULT GETDATE(),
	RecordStatus INT NOT NULL DEFAULT 0, -- 0: Active, 1: Disabled, 2: Closed, 3: Archived
	TempKey INT NOT NULL DEFAULT 0
)
GO
CREATE INDEX IX_Student ON dbo.Student
(
	AspNetUsersID,
	NickName,Location,
	DateTimeStamp,RecordStatus,TempKey
)
GO
-----------------------------------------------------------------------------------------------------------
		IF EXISTS (SELECT * FROM sysobjects WHERE id=object_id('dbo.StudentHobby') and sysstat & 0xf = 3)
			DROP TABLE dbo.StudentHobby
		GO
		CREATE TABLE dbo.StudentHobby
		(
			ID INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_StudentHobby PRIMARY KEY NONCLUSTERED,

			AspNetUsersID NVARCHAR(128) NOT NULL DEFAULT '',

			IsSelected INT NOT NULL DEFAULT 0, -- 0: No, 1: Yes

			LookupHobbyID INT NOT NULL DEFAULT 0,
			LookupHobbyDescription NVARCHAR(200) NOT NULL DEFAULT '',

			DateTimeStamp DATETIME NOT NULL DEFAULT GETDATE(),
			RecordStatus INT NOT NULL DEFAULT 0, -- 0: Active, 1: Disabled, 2: Closed, 3: Archived
			TempKey INT NOT NULL DEFAULT 0
		)
		GO
		CREATE INDEX IX_StudentHobby ON dbo.StudentHobby
		(
			AspNetUsersID,
			LookupHobbyID, LookupHobbyDescription,
			DateTimeStamp,RecordStatus,TempKey
		)
		GO

-----------------------------------------------------------------------------------------------------------

IF EXISTS (SELECT * FROM sysobjects WHERE id=object_id('dbo.LookupHobby') and sysstat & 0xf = 3)
	DROP TABLE dbo.LookupHobby
GO
CREATE TABLE dbo.LookupHobby
(
	ID INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_LookupHobby PRIMARY KEY NONCLUSTERED,

	LocationType NVARCHAR(50) NOT NULL DEFAULT '', -- Indoor/Outdoor (Unused for now
	Description NVARCHAR(200) NOT NULL DEFAULT '',

	DateTimeStamp DATETIME NOT NULL DEFAULT GETDATE(),
	RecordStatus INT NOT NULL DEFAULT 0, -- 0: Active, 1: Disabled, 2: Closed, 3: Archived
	TempKey INT NOT NULL DEFAULT 0
)
GO
CREATE INDEX IX_LookupHobby ON dbo.LookupHobby
(
	LocationType, Description,
	DateTimeStamp,RecordStatus,TempKey
)
GO

-----------------------------------------------------------------------------------------------------------

IF EXISTS(SELECT * FROM SYS.OBJECTS WHERE type = 'P' AND name = 'SP_LookupHobby_GetAll')
	DROP PROCEDURE dbo.SP_LookupHobby_GetAll
GO
CREATE PROCEDURE dbo.SP_LookupHobby_GetAll
AS
BEGIN TRANSACTION
BEGIN
	SELECT
		LookupHobby.ID,
		LookupHobby.LocationType,
		LookupHobby.Description,
		(LookupHobby.Description + ' (' + LookupHobby.LocationType + ')') AS DescriptionFull,
		LookupHobby.DateTimeStamp,
		LookupHobby.RecordStatus
	FROM
		LookupHobby
	ORDER BY
		LookupHobby.Description ASC
END
COMMIT TRANSACTION
GO

-----------------------------------------------------------------------------------------------------------

IF EXISTS(SELECT * FROM SYS.OBJECTS WHERE type = 'P' AND name = 'SP_Student_AddModify')
	DROP PROCEDURE dbo.SP_Student_AddModify
GO
CREATE PROCEDURE dbo.SP_Student_AddModify
	@ID INT,
	@AspNetUsersID NVARCHAR(128),
	@FullName NVARCHAR(MAX),
	@NickName VARCHAR(100),
	@Biography NVARCHAR(200),
	@University NVARCHAR(200),
	@StudyYear NVARCHAR(100),
	@Course NVARCHAR(200),
	@Location NVARCHAR(200),
	@FunFact NVARCHAR(200),
	@DateJoined DATETIME,
	@DateTimeStamp DATETIME,
	@RecordStatus INT
AS
BEGIN TRANSACTION
DECLARE @DateTimeStamp_Out DATETIME = GETDATE()
DECLARE @TempKey INT = CAST((RAND() * 100000) AS Integer)

SET @DateJoined = @DateTimeStamp_Out

-- Update Student Table
BEGIN
	IF NOT EXISTS (SELECT ID FROM Student WHERE AspNetUsersID LIKE @AspNetUsersID)
	BEGIN
		INSERT INTO Student
			(AspNetUsersID, NickName, Biography, University, Course, StudyYear, Location, FunFact, DateJoined, DateTimeStamp, RecordStatus, TempKey)
			VALUES
			(@AspNetUsersID, @NickName, @Biography, @University, @Course, @StudyYear, @Location, @FunFact, @DateJoined, @DateTimeStamp_Out, @RecordStatus, @TempKey)

		-- Reset TempKey
		SELECT @ID = ID FROM Student WHERE TempKey = @TempKey
		UPDATE Student SET TempKey = 0 WHERE ID = @ID
	END
	ELSE
	BEGIN
		UPDATE Student
		SET
			NickName = @NickName,
			Biography = @Biography,
			University = @University,
			Course = @Course,
			StudyYear = @StudyYear,
			Location = @Location,
			FunFact = @FunFact,
			DateTimeStamp = @DateTimeStamp_Out,
			RecordStatus = @RecordStatus
		WHERE
			AspNetUsersID = @AspNetUsersID
	END
END

-- Update AspNetUsers Table
BEGIN
	UPDATE AspNetUsers
	SET
		AspNetUsers.FullName = @FullName
	WHERE
		AspNetUsers.Id LIKE @AspNetUsersID AND
		AspNetUsers.FullName NOT LIKE @FullName
END

BEGIN
	-- Return the record
	SELECT
		@ID AS ID,
		@FullName AS FullName,
		@AspNetUsersID AS AspNetUsersID,
		@NickName AS NickName,
		@Biography AS Biography,
		@University AS University,
		@Course AS Course,
		@StudyYear AS StudyYear,
		@Location AS Location,
		@FunFact AS FunFact,
		'' AS StudentBackground,
		'' AS StudentImage,
		@DateJoined AS DateJoined,
		@DateTimeStamp_Out AS DateTimeStamp,
		@RecordStatus AS RecordStatus
END
COMMIT TRANSACTION
GO

-----------------------------------------------------------------------------------------------------------

IF EXISTS(SELECT * FROM SYS.OBJECTS WHERE type = 'P' AND name = 'SP_Student_Get')
	DROP PROCEDURE dbo.SP_Student_Get
GO
CREATE PROCEDURE dbo.SP_Student_Get
	@AspNetUsersID NVARCHAR(128)
AS
BEGIN TRANSACTION
BEGIN
	SELECT
		'' AS FullName, -- Pass in blank to use the same factory function
		Student.*
	FROM
		Student
	WHERE
		Student.AspNetUsersID LIKE @AspNetUsersID
END
COMMIT TRANSACTION
GO

-----------------------------------------------------------------------------------------------------------

IF EXISTS(SELECT * FROM SYS.OBJECTS WHERE type = 'P' AND name = 'SP_StudentHobby_AddModify')
	DROP PROCEDURE dbo.SP_StudentHobby_AddModify
GO
CREATE PROCEDURE dbo.SP_StudentHobby_AddModify
	@ID INT,
	@AspNetUsersID NVARCHAR(128),
	@IsSelected INT,
	@LookupHobbyID INT,
	@LookupHobbyDescription NVARCHAR(200),
	@DateTimeStamp DATETIME,
	@RecordStatus INT
AS
BEGIN TRANSACTION
DECLARE @DateTimeStamp_Out DATETIME = GETDATE()
DECLARE @TempKey INT = CAST((RAND() * 100000) AS Integer)

BEGIN
	IF NOT EXISTS (SELECT ID FROM StudentHobby WHERE AspNetUsersID LIKE @AspNetUsersID AND LookupHobbyID = @LookupHobbyID)
	BEGIN
		INSERT INTO StudentHobby
			(AspNetUsersID, IsSelected, LookupHobbyID, LookupHobbyDescription, DateTimeStamp, RecordStatus, TempKey)
			VALUES
			(@AspNetUsersID, @IsSelected, @LookupHobbyID, @LookupHobbyDescription, @DateTimeStamp_Out, @RecordStatus, @TempKey)

		-- Reset TempKey
		SELECT @ID = ID FROM StudentHobby WHERE TempKey = @TempKey
		UPDATE StudentHobby SET TempKey = 0 WHERE ID = @ID
	END
	ELSE
	BEGIN
		UPDATE StudentHobby
		SET
			AspNetUsersID = @AspNetUsersID,
			IsSelected = @IsSelected,
			LookupHobbyID = @LookupHobbyID,
			LookupHobbyDescription = @LookupHobbyDescription,
			DateTimeStamp = @DateTimeStamp_Out,
			RecordStatus = @RecordStatus
		WHERE
			AspNetUsersID LIKE @AspNetUsersID AND LookupHobbyID = @LookupHobbyID
	END

	-- Return the record
	SELECT
		@ID AS ID,
		@AspNetUsersID AS AspNetUsersID,
		@IsSelected AS IsSelected,
		@LookupHobbyID AS LookupHobbyID,
		@LookupHobbyDescription AS LookupHobbyDescription,
		@DateTimeStamp_Out AS DateTimeStamp,
		@RecordStatus AS RecordStatus
END
COMMIT TRANSACTION
GO
-- ____________________________________________________________________________________________
IF EXISTS(SELECT * FROM SYS.OBJECTS WHERE type = 'P' AND name = 'SP_StudentHobby_Get')
	DROP PROCEDURE dbo.SP_StudentHobby_Get
GO
CREATE PROCEDURE dbo.SP_StudentHobby_Get
	@AspNetUsersID NVARCHAR(128)
AS
BEGIN TRANSACTION
BEGIN
	SELECT
		StudentHobby.*
	FROM
		StudentHobby
	WHERE
		StudentHobby.AspNetUsersID LIKE @AspNetUsersID
	ORDER BY
		StudentHobby.LookupHobbyDescription ASC
END
COMMIT TRANSACTION
GO
-- ____________________________________________________________________________________________
IF EXISTS(SELECT * FROM SYS.OBJECTS WHERE type = 'P' AND name = 'SP_StudentAndHobby_Get')
	DROP PROCEDURE dbo.SP_StudentAndHobby_Get
GO
CREATE PROCEDURE dbo.SP_StudentAndHobby_Get
	@AspNetUsersID NVARCHAR(128)
AS
BEGIN TRANSACTION
BEGIN
	EXEC SP_Student_Get @AspNetUsersID
END

BEGIN
	EXEC SP_StudentHobby_Get @AspNetUsersID	
END
COMMIT TRANSACTION
GO

-----------------------------------------------------------------------------------------------------------


IF EXISTS(SELECT * FROM SYS.OBJECTS WHERE type = 'P' AND name = 'SP_Student_StudentBackground_AddModify')
	DROP PROCEDURE dbo.SP_Student_StudentBackground_AddModify
GO
CREATE PROCEDURE dbo.SP_Student_StudentBackground_AddModify
	@AspNetUsersID NVARCHAR(128),
	@StudentBackground NVARCHAR(200)
AS
BEGIN TRANSACTION
DECLARE @ID INT = 0
DECLARE @RecordStatus INT = 0
DECLARE @DateTimeStamp_Out DATETIME = GETDATE()
DECLARE @TempKey INT = CAST((RAND() * 100000) AS Integer)
DECLARE @DateJoined DATETIME = @DateTimeStamp_Out

-- Update Student Table
BEGIN
	IF NOT EXISTS (SELECT ID FROM Student WHERE AspNetUsersID LIKE @AspNetUsersID)
	BEGIN
		INSERT INTO Student
			(AspNetUsersID, StudentBackground, DateJoined, DateTimeStamp, RecordStatus, TempKey)
			VALUES
			(@AspNetUsersID, @StudentBackground, @DateJoined, @DateTimeStamp_Out, @RecordStatus, @TempKey)

		-- Reset TempKey
		SELECT @ID = ID FROM Student WHERE TempKey = @TempKey
		UPDATE Student SET TempKey = 0 WHERE ID = @ID
	END
	ELSE
	BEGIN
		UPDATE Student
		SET
			StudentBackground = @StudentBackground,
			DateTimeStamp = @DateTimeStamp_Out,
			RecordStatus = @RecordStatus
		WHERE
			AspNetUsersID = @AspNetUsersID
	END
END

BEGIN
	-- Return the record
	SELECT
		@ID AS ID,
		@AspNetUsersID AS AspNetUsersID,
		@StudentBackground AS StudentBackground,
		'' AS StudentImage,
		@DateJoined AS DateJoined,
		@DateTimeStamp_Out AS DateTimeStamp,
		@RecordStatus AS RecordStatus
END
COMMIT TRANSACTION
GO

-----------------------------------------------------------------------------------------------------------

IF EXISTS(SELECT * FROM SYS.OBJECTS WHERE type = 'P' AND name = 'SP_Student_StudentImage_AddModify')
	DROP PROCEDURE dbo.SP_Student_StudentImage_AddModify
GO
CREATE PROCEDURE dbo.SP_Student_StudentImage_AddModify
	@AspNetUsersID NVARCHAR(128),
	@StudentImage NVARCHAR(200)
AS
BEGIN TRANSACTION
DECLARE @ID INT = 0
DECLARE @RecordStatus INT = 0
DECLARE @DateTimeStamp_Out DATETIME = GETDATE()
DECLARE @TempKey INT = CAST((RAND() * 100000) AS Integer)
DECLARE @DateJoined DATETIME = @DateTimeStamp_Out

-- Update Student Table
BEGIN
	IF NOT EXISTS (SELECT ID FROM Student WHERE AspNetUsersID LIKE @AspNetUsersID)
	BEGIN
		INSERT INTO Student
			(AspNetUsersID, StudentImage, DateJoined, DateTimeStamp, RecordStatus, TempKey)
			VALUES
			(@AspNetUsersID, @StudentImage, @DateJoined, @DateTimeStamp_Out, @RecordStatus, @TempKey)

		-- Reset TempKey
		SELECT @ID = ID FROM Student WHERE TempKey = @TempKey
		UPDATE Student SET TempKey = 0 WHERE ID = @ID
	END
	ELSE
	BEGIN
		UPDATE Student
		SET
			StudentImage = @StudentImage,
			DateTimeStamp = @DateTimeStamp_Out,
			RecordStatus = @RecordStatus
		WHERE
			AspNetUsersID = @AspNetUsersID
	END
END

BEGIN
	-- Return the record
	SELECT
		@ID AS ID,
		@AspNetUsersID AS AspNetUsersID,
		'' AS StudentBackground,
		@StudentImage AS StudentImage,
		@DateJoined AS DateJoined,
		@DateTimeStamp_Out AS DateTimeStamp,
		@RecordStatus AS RecordStatus
END
COMMIT TRANSACTION
GO