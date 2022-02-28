Public Class cFactory_StudentHobby
	Private Sub Populate(ByRef oObject As cStudentHobby, ByRef oDataset As Data.DataSet, ByRef iCounter As Integer, ByRef iTableIndex As Integer)
		If oObject Is Nothing Then oObject = New cStudentHobby

		oObject.ID = CType(GetField(oDataset, iTableIndex, iCounter, "ID"), Integer)

		oObject.AspNetUsersID = GetField(oDataset, iTableIndex, iCounter, "AspNetUsersID").ToString
		oObject.IsSelected = CType(GetField(oDataset, iTableIndex, iCounter, "IsSelected"), Integer)

		oObject.LookupHobbyID = CType(GetField(oDataset, iTableIndex, iCounter, "LookupHobbyID"), Integer)
		oObject.Key = oObject.LookupHobbyID.ToString

		oObject.LookupHobbyDescription = GetField(oDataset, iTableIndex, iCounter, "LookupHobbyDescription").ToString
		oObject.DateTimeStamp = Format(GetField(oDataset, iTableIndex, iCounter, "DateTimeStamp"), DEFAULT_DATETIMESTAMP_SQL)
		oObject.RecordStatus = CType(GetField(oDataset, iTableIndex, iCounter, "RecordStatus"), Integer)
	End Sub

	Public Function AddModify(ByRef oDatabase As cDatabase, ByRef oObject_In As cStudentHobby) As cStudentHobby
		Const TABLE_FIRST As Integer = 0

		Dim sSQL as String = "EXEC SP_StudentHobby_AddModify " &
			oObject_In.ID.ToString & "," &
			"'" & VSQL(oObject_In.AspNetUsersID ) & "'," &
			oObject_In.IsSelected.ToString & "," &
			oObject_In.LookupHobbyID.ToString & "," &
			"'" & VSQL(oObject_In.LookupHobbyDescription ) & "'," &
			"'" & VSQL(oObject_In.DateTimeStamp) & "'," &
			oObject_In.RecordStatus.ToString

		Dim oObject_Out As cStudentHobby = Nothing
		Dim iCounter As Integer = 0

		Dim oDataset As Data.DataSet = Nothing
		GetDatasetMultiTables(sSQL, oDataset, oDatabase)
		Populate(oObject_Out, oDataset, iCounter, TABLE_FIRST)

		Return oObject_Out
	End Function

	Public Function GetViaAspNetUsersID(ByRef oDatabase As cDatabase, ByVal sAspNetUsersID As String) As cDataHelper
		Const TABLE_FIRST As Integer = 0

		Dim sSQL As String = String.Format("EXEC SP_StudentHobby_Get '{0}'", sAspNetUsersID)

		Dim oObject_StudentHobby As cStudentHobby = Nothing

		Dim oDataset As Data.DataSet = Nothing
		GetDatasetMultiTables(sSQL, oDataset, oDatabase)
		Dim oDataHelper As New cDataHelper

		Dim iTableIndex As Integer = TABLE_FIRST
		Dim iMaxRecords_TableOne As Integer = oDataset.Tables(iTableIndex).Rows.Count
		If iMaxRecords_TableOne > 0 Then
			iMaxRecords_TableOne -= 1

			For iCounter As Integer = 0 To iMaxRecords_TableOne
				Populate(oObject_StudentHobby, oDataset, iCounter, iTableIndex)
				oDataHelper.Dictionary1.Add(oObject_StudentHobby.Key, oObject_StudentHobby)
				oObject_StudentHobby = Nothing
			Next
		End If

		oDataset = Nothing

		Return oDataHelper
	End Function

End Class
