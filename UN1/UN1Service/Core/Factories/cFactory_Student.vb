Public Class cFactory_Student
	Private Sub Populate(ByRef oObject As cStudentImages, ByRef oDataset As Data.DataSet, ByRef iCounter As Integer, ByRef iTableIndex As Integer)
		If oObject Is Nothing Then oObject = New cStudentImages

		oObject.ID = CType(GetField(oDataset, iTableIndex, iCounter, "ID"), Integer)
		oObject.Key = oObject.ID.ToString

		oObject.AspNetUsersID = GetField(oDataset, iTableIndex, iCounter, "AspNetUsersID").ToString
		oObject.StudentBackground = GetField(oDataset, iTableIndex, iCounter, "StudentBackground").ToString
		oObject.StudentImage = GetField(oDataset, iTableIndex, iCounter, "StudentImage").ToString
		oObject.DateJoined = Format(GetField(oDataset, iTableIndex, iCounter, "DateJoined"), DEFAULT_DATE_DISPLAY00)
		oObject.DateTimeStamp = Format(GetField(oDataset, iTableIndex, iCounter, "DateTimeStamp"), DEFAULT_DATETIMESTAMP_SQL)
		oObject.RecordStatus = CType(GetField(oDataset, iTableIndex, iCounter, "RecordStatus"), Integer)
	End Sub

	Private Sub Populate(ByRef oObject As cStudent, ByRef oDataset As Data.DataSet, ByRef iCounter As Integer, ByRef iTableIndex As Integer)
		If oObject Is Nothing Then oObject = New cStudent

		If oDataset.Tables(iTableIndex).Rows.Count = 0 Then Return

		oObject.ID = CType(GetField(oDataset, iTableIndex, iCounter, "ID"), Integer)
		oObject.Key = oObject.ID.ToString

		oObject.AspNetUsersID = GetField(oDataset, iTableIndex, iCounter, "AspNetUsersID").ToString
		oObject.FullName = GetField(oDataset, iTableIndex, iCounter, "FullName").ToString
		oObject.NickName = GetField(oDataset, iTableIndex, iCounter, "NickName").ToString
		oObject.Biography = GetField(oDataset, iTableIndex, iCounter, "Biography").ToString
		oObject.University = GetField(oDataset, iTableIndex, iCounter, "University").ToString
		oObject.Course = GetField(oDataset, iTableIndex, iCounter, "Course").ToString
		oObject.StudyYear = GetField(oDataset, iTableIndex, iCounter, "StudyYear").ToString
		oObject.Location = GetField(oDataset, iTableIndex, iCounter, "Location").ToString
		oObject.FunFact = GetField(oDataset, iTableIndex, iCounter, "FunFact").ToString

		oObject.StudentBackground = GetField(oDataset, iTableIndex, iCounter, "StudentBackground").ToString
		oObject.StudentImage = GetField(oDataset, iTableIndex, iCounter, "StudentImage").ToString

		oObject.DateJoined = Format(GetField(oDataset, iTableIndex, iCounter, "DateJoined"), DEFAULT_DATE_DISPLAY00)
		oObject.DateTimeStamp = Format(GetField(oDataset, iTableIndex, iCounter, "DateTimeStamp"), DEFAULT_DATETIMESTAMP_SQL)
		oObject.RecordStatus = CType(GetField(oDataset, iTableIndex, iCounter, "RecordStatus"), Integer)
	End Sub

	Private Sub Populate(ByRef oObject As cStudentHobby, ByRef oDataset As Data.DataSet, ByRef iCounter As Integer, ByRef iTableIndex As Integer)
		If oObject Is Nothing Then oObject = New cStudentHobby

		If oDataset.Tables(iTableIndex).Rows.Count = 0 Then Return

		oObject.ID = CType(GetField(oDataset, iTableIndex, iCounter, "ID"), Integer)

		oObject.AspNetUsersID = GetField(oDataset, iTableIndex, iCounter, "AspNetUsersID").ToString
		oObject.IsSelected = CType(GetField(oDataset, iTableIndex, iCounter, "IsSelected"), Integer)

		oObject.LookupHobbyID = CType(GetField(oDataset, iTableIndex, iCounter, "LookupHobbyID"), Integer)
		oObject.Key = oObject.LookupHobbyID.ToString

		oObject.LookupHobbyDescription = GetField(oDataset, iTableIndex, iCounter, "LookupHobbyDescription").ToString
		oObject.DateTimeStamp = Format(GetField(oDataset, iTableIndex, iCounter, "DateTimeStamp"), DEFAULT_DATETIMESTAMP_SQL)
		oObject.RecordStatus = CType(GetField(oDataset, iTableIndex, iCounter, "RecordStatus"), Integer)
	End Sub

	Public Function AddModify(ByRef oDatabase As cDatabase, ByRef oObject_In As cStudent) As cStudent
		Const TABLE_FIRST As Integer = 0

		Dim sSQL as String = "EXEC SP_Student_AddModify " &
			oObject_In.ID.ToString & "," &
			"'" & VSQL(oObject_In.AspNetUsersID ) & "'," &
			"'" & VSQL(oObject_In.FullName) & "'," &
			"'" & VSQL(oObject_In.NickName) & "'," &
			"'" & VSQL(oObject_In.Biography ) & "'," &
			"'" & VSQL(oObject_In.University ) & "'," &
			"'" & VSQL(oObject_In.Course ) & "'," &
			"'" & VSQL(oObject_In.StudyYear ) & "'," &
			"'" & VSQL(oObject_In.Location ) & "'," &
			"'" & VSQL(oObject_In.FunFact ) & "'," &
			"'" & VSQL(oObject_In.DateJoined) & "'," &
			"'" & VSQL(oObject_In.DateTimeStamp) & "'," &
			oObject_In.RecordStatus.ToString

		Dim oObject_Out As cStudent = Nothing
		Dim iCounter As Integer = 0

		Dim oDataset As Data.DataSet = Nothing
		GetDatasetMultiTables(sSQL, oDataset, oDatabase)
		Populate(oObject_Out, oDataset, iCounter, TABLE_FIRST)

		Return oObject_Out
	End Function

	Public Function GetViaAspNetUsersID(ByRef oDatabase As cDatabase, ByVal sAspNetUsersID As String) As cStudent
		Const TABLE_FIRST As Integer = 0

		Dim sSQL As String = String.Format("EXEC SP_Student_Get '{0}'", VSQL(sAspNetUsersID))

		Dim oObject_Out As cStudent = Nothing
		Dim iCounter As Integer = 0

		Dim oDataset As Data.DataSet = Nothing
		GetDatasetMultiTables(sSQL, oDataset, oDatabase)
		Populate(oObject_Out, oDataset, iCounter, TABLE_FIRST)

		Return oObject_Out
	End Function

	Public Function GetStudentAndHobby_ViaAspNetUsersID(ByRef oDatabase As cDatabase, ByVal sAspNetUsersID As String) As cStudent
		Const TABLE_FIRST As Integer = 0
		Const TABLE_SECOND AS Integer = 1

		Dim sSQL As String = String.Format("EXEC SP_StudentAndHobby_Get '{0}'", sAspNetUsersID)

		Debug.Print (sSQL)

		Dim oObject_Student As cStudent = Nothing
		Dim oObject_StudentHobby As cStudentHobby = Nothing
		Dim iCounter_Student As Integer = 0

		Dim oDataset As Data.DataSet = Nothing
		GetDatasetMultiTables(sSQL, oDataset, oDatabase)
		Populate(oObject_Student, oDataset, iCounter_Student, TABLE_FIRST)

		Dim iTableIndex As Integer = TABLE_SECOND
		Dim iMaxRecords_TableTwo As Integer = oDataset.Tables(iTableIndex).Rows.Count
		If iMaxRecords_TableTwo > 0 Then
			iMaxRecords_TableTwo -= 1

			For iCounter As Integer = 0 To iMaxRecords_TableTwo
				Populate(oObject_StudentHobby, oDataset, iCounter, iTableIndex)
				oObject_Student.Dictionary_StudentHobby.Add(oObject_StudentHobby.Key, oObject_StudentHobby)
				oObject_StudentHobby = Nothing
			Next
		End If

		oDataset = Nothing

		Return oObject_Student
	End Function

	Public Function AddModify_StudentBackground(ByRef oDatabase As cDatabase, ByRef oObject_In As cStudentImages) As cStudentImages
		Const TABLE_FIRST As Integer = 0

		Dim sSQL as String = "EXEC SP_Student_StudentBackground_AddModify " &
			"'" & VSQL(oObject_In.AspNetUsersID) & "'," &
			"'" & VSQL(oObject_In.StudentBackground) & "'"

		Dim oObject_Out As cStudentImages = Nothing
		Dim iCounter As Integer = 0

		Dim oDataset As Data.DataSet = Nothing
		GetDatasetMultiTables(sSQL, oDataset, oDatabase)
		Populate(oObject_Out, oDataset, iCounter, TABLE_FIRST)

		Return oObject_Out
	End Function

	Public Function AddModify_StudentImage(ByRef oDatabase As cDatabase, ByRef oObject_In As cStudentImages) As cStudentImages
		Const TABLE_FIRST As Integer = 0

		Dim sSQL as String = "EXEC SP_Student_StudentImage_AddModify " &
			"'" & VSQL(oObject_In.AspNetUsersID) & "'," &
			"'" & VSQL(oObject_In.StudentImage) & "'"

		Dim oObject_Out As cStudentImages = Nothing
		Dim iCounter As Integer = 0

		Dim oDataset As Data.DataSet = Nothing
		GetDatasetMultiTables(sSQL, oDataset, oDatabase)
		Populate(oObject_Out, oDataset, iCounter, TABLE_FIRST)

		Return oObject_Out
	End Function
End Class
