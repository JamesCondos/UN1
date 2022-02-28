Module mGlobal
	Public goDataHelper_LookupHobby As Task(Of cDataHelper) = Nothing

	Public gsAccessToken As String = String.Empty
	Public goStudent As cStudent = Nothing

	Public Sub Global_Initialise()
		Preload_LookupHobby()
	End Sub

	Public Sub Preload_LookupHobby()
		Dim oWebAPI As New cWebAPI
		goDataHelper_LookupHobby = oWebAPI.GetAll_LookupHobby()
		oWebAPI = Nothing
	End Sub

	Public Sub Initialise_CurrentUserInformation()
		gsAccessToken = GetAccessToken()

		If gsAccessToken.Trim.Length = 0 Then 
			goStudent = Nothing
			Return 
		End If

		Dim oWebAPI As New cWebAPI
		Dim oDataHelperTask As cDataHelper = oWebAPI.Get_CurrentUserInfo(gsAccessToken)
		oWebAPI = Nothing

		If oDataHelperTask Is Nothing Then Return

		goStudent = New cStudent
		goStudent .AspNetUsersID = oDataHelperTask.AspNetUsersID
		goStudent .FullName = oDataHelperTask.FullName
		goStudent.NickName = oDataHelperTask.NickName
		goStudent.Biography = oDataHelperTask.Biography
		goStudent.University = oDataHelperTask.University
		goStudent.Course = oDataHelperTask.Course
		goStudent.StudyYear= oDataHelperTask.StudyYear
		goStudent.Location = oDataHelperTask.Location
		goStudent.FunFact = oDataHelperTask.FunFact

		goStudent.StudentBackground = oDataHelperTask.StudentBackground
		goStudent.StudentImage = oDataHelperTask.StudentImage

		goStudent.DateJoined = oDataHelperTask.DateJoined

		goStudent.Dictionary_StudentHobby = oDataHelperTask.Dictionary1
	End Sub


End Module
