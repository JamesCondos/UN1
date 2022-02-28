Public Class cFactory_LookupHobby
	Private Sub Populate(ByRef oObject As cLookupHobby, ByRef oDataset As Data.DataSet, ByRef iCounter As Integer, ByRef iTableIndex As Integer)
		If oObject Is Nothing Then oObject = New cLookupHobby

		oObject.ID = CType(GetField(oDataset, iTableIndex, iCounter, "ID"), Integer)
		oObject.Key = oObject.ID.ToString

		oObject.LocationType = GetField(oDataset, iTableIndex, iCounter, "LocationType").ToString
		oObject.Description = GetField(oDataset, iTableIndex, iCounter, "Description").ToString
		oObject.DescriptionFull = GetField(oDataset, iTableIndex, iCounter, "DescriptionFull").ToString
		oObject.DateTimeStamp = Format(GetField(oDataset, iTableIndex, iCounter, "DateTimeStamp"), DEFAULT_DATETIMESTAMP_SQL)
		oObject.RecordStatus = CType(GetField(oDataset, iTableIndex, iCounter, "RecordStatus"), Integer)
	End Sub

	Public Function GetAll_LookupHobby(ByRef oDatabase As cDatabase) As cDataHelper
		Try
			Const TABLE_FIRST As Integer = 0

			Dim sSQL As String = String.Format("EXEC SP_LookupHobby_GetAll")

			Dim oObject_Out As cLookupHobby = Nothing

			Dim oDataset As Data.DataSet = Nothing
			GetDatasetMultiTables(sSQL, oDataset, oDatabase)
			Dim oDataHelper As New cDataHelper

			Dim iTableIndex As Integer = TABLE_FIRST
			Dim iMaxRecords_TableOne As Integer = oDataset.Tables(iTableIndex).Rows.Count
			If iMaxRecords_TableOne > 0 Then
				iMaxRecords_TableOne -= 1

				For iCounter As Integer = 0 To iMaxRecords_TableOne
					Populate(oObject_Out, oDataset, iCounter, iTableIndex)
					oDataHelper.Dictionary1.Add(oObject_Out.Key, oObject_Out)
					oObject_Out = Nothing
				Next
			End If

			oDataset = Nothing

			Return oDataHelper
		Catch ex As Exception
			Log(Debug_GetCurrentFunctionName() & " ~ " & ex.Message)
			Return Nothing
		End Try
	End Function


End Class
