Imports System.Data.SqlClient

Public Class cDatabase
	Private oConnection As SqlConnection = Nothing

	Public ReadOnly Property Connection() As SqlConnection
		Get
			Return oConnection
		End Get
	End Property

	Public Sub DBConnect(ByVal sConnectionString As String)
		Try
			sConnectionString = ConfigurationManager.ConnectionStrings(sConnectionString).ToString
			oConnection = New SqlConnection : oConnection.ConnectionString = sConnectionString : oConnection.Open()
		Catch ex As Exception
			oConnection = Nothing
			Log(Debug_GetCurrentFunctionName() & " ~ " & ex.Message)
		End Try
	End Sub

	Public Sub DBConnect(ByVal sServer As String, ByVal sDatabase As String, ByVal sUsername As String, ByVal sPassword As String)
		Try
			Dim sString As String = "Data Source=" & sServer & ";Initial Catalog=" & sDatabase & ";User ID=" & sUsername & ";Password=" & sPassword
			oConnection = New SqlConnection : oConnection.ConnectionString = sString : oConnection.Open()
		Catch ex As Exception
			oConnection = Nothing
			Log(Debug_GetCurrentFunctionName() & " ~ " & ex.Message)
		End Try
	End Sub

	Public Sub DBDisconnect()
		If oConnection Is Nothing Then Exit Sub
		oConnection.Close() : oConnection = Nothing
	End Sub

	Public Function ExecuteSQL(ByVal sSQL As String) As Boolean
		Dim oCommand As SqlClient.SqlCommand
		Dim oDataReader As SqlClient.SqlDataReader

		If oConnection Is Nothing Then Return False

		Try
			oCommand = New SqlClient.SqlCommand(sSQL, oConnection)
			oDataReader = oCommand.ExecuteReader(CommandBehavior.SingleResult)

			oDataReader.Close()

			Return True
		Catch ex As Exception
			Log(Debug_GetCurrentFunctionName() & " ~ " & ex.Message)
			Log("SQLError: " & sSQL)

			oCommand = Nothing

			Return False
		End Try
	End Function
End Class
