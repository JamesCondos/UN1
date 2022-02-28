Public Class cBusinessRule_LookupHobby
	Public Function GetAll_LookupHobby() As cDataHelper
		Dim oDatabase As New cDatabase
		oDatabase.DBConnect(GetAppSetting(DEFAULT_DATABASE_CONNECTION_USED))

		Dim oFactory As New cFactory_LookupHobby
		Dim oDataHelper As cDataHelper = oFactory.GetAll_LookupHobby(oDatabase)
		oFactory = Nothing

		oDatabase.DBDisconnect() : oDatabase = Nothing
		Return oDataHelper
	End Function


End Class
