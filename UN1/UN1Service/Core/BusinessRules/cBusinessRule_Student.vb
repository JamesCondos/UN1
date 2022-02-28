Public Class cBusinessRule_Student
	Public Function AddModify(ByRef oObject_In As cStudent) As cStudent
		Dim oDatabase As New cDatabase
		oDatabase.DBConnect(GetAppSetting(DEFAULT_DATABASE_CONNECTION_USED))

		Dim oFactory As New cFactory_Student
		Dim oObject_Out As cStudent = oFactory.AddModify(oDatabase,oObject_In)
		oFactory = Nothing

		oDatabase.DBDisconnect() : oDatabase = Nothing

		Return  oObject_Out
	End Function

	Public Function GetViaAspNetUsersID(ByVal sAspNetUsersID As String) As cStudent
		Dim oDatabase As New cDatabase
		oDatabase.DBConnect(GetAppSetting(DEFAULT_DATABASE_CONNECTION_USED))

		Dim oFactory As New cFactory_Student
		Dim oObject_Out As cStudent = oFactory.GetViaAspNetUsersID(oDatabase,sAspNetUsersID)
		oFactory = Nothing

		oDatabase.DBDisconnect() : oDatabase = Nothing

		Return  oObject_Out
	End Function

	Public Function GetStudentAndHobby_ViaAspNetUsersID(ByVal sAspNetUsersID As String) As cStudent
		Dim oDatabase As New cDatabase
		oDatabase.DBConnect(GetAppSetting(DEFAULT_DATABASE_CONNECTION_USED))

		Dim oFactory As New cFactory_Student
		Dim oObject_Out As cStudent = oFactory.GetStudentAndHobby_ViaAspNetUsersID(oDatabase,sAspNetUsersID)
		oFactory = Nothing

		oDatabase.DBDisconnect() : oDatabase = Nothing

		Return  oObject_Out
	End Function
	
	Public Function AddModify_StudentBackground(ByRef oObject_In As cStudentImages) As cStudentImages
		Dim oDatabase As New cDatabase
		oDatabase.DBConnect(GetAppSetting(DEFAULT_DATABASE_CONNECTION_USED))

		Dim oFactory As New cFactory_Student
		Dim oObject_Out As cStudentImages = oFactory.AddModify_StudentBackground(oDatabase,oObject_In)
		oFactory = Nothing

		oDatabase.DBDisconnect() : oDatabase = Nothing

		Return  oObject_Out
	End Function

	Public Function AddModify_StudentImage(ByRef oObject_In As cStudentImages) As cStudentImages
		Dim oDatabase As New cDatabase
		oDatabase.DBConnect(GetAppSetting(DEFAULT_DATABASE_CONNECTION_USED))

		Dim oFactory As New cFactory_Student
		Dim oObject_Out As cStudentImages = oFactory.AddModify_StudentImage(oDatabase,oObject_In)
		oFactory = Nothing

		oDatabase.DBDisconnect() : oDatabase = Nothing

		Return  oObject_Out
	End Function
End Class
