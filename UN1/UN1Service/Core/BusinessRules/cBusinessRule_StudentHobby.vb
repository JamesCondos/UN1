Public Class cBusinessRule_StudentHobby
	Public Function AddModify(ByRef oObject_In As cStudentHobby) As cStudentHobby
		Dim oDatabase As New cDatabase
		oDatabase.DBConnect(GetAppSetting(DEFAULT_DATABASE_CONNECTION_USED))

		Dim oFactory As New cFactory_StudentHobby
		Dim oObject_Out As cStudentHobby = oFactory.AddModify(oDatabase,oObject_In)
		oFactory = Nothing

		oDatabase.DBDisconnect() : oDatabase = Nothing

		Return  oObject_Out
	End Function

	Public Function GetViaAspNetUsersID(ByVal sAspNetUsersID As String) As cDataHelper
		Dim oDatabase As New cDatabase
		oDatabase.DBConnect(GetAppSetting(DEFAULT_DATABASE_CONNECTION_USED))

		Dim oFactory As New cFactory_StudentHobby
		Dim oDataHelper As cDataHelper = oFactory.GetViaAspNetUsersID(oDatabase,sAspNetUsersID)
		oFactory = Nothing

		oDatabase.DBDisconnect() : oDatabase = Nothing

		Return  oDataHelper
	End Function
End Class
