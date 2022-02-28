Public Class cDataHelper
	Implements IDisposable 

	Private oDictionary_Error As Dictionary(Of String, Object)	= Nothing

	Private oDictionary1 As Dictionary(Of String, Object) = Nothing
	Private oDictionary2 As Dictionary(Of String, Object) = Nothing
	Private oDictionary3 As Dictionary(Of String, Object) = Nothing

	Private bResult As Boolean = False
	Private sMessage As String = String.Empty

	Private sID As String = String.Empty
	Private sAspNetUsersID As String = String.Empty
	Private sFullName As String = String.Empty
	Private sNickName As String = String.Empty
	Private sBiography As String = String.Empty
	Private sUniversity As String = String.Empty
	Private sCourse As String = String.Empty
	Private sStudyYear As String = String.Empty
	Private sLocation As String = String.Empty
	Private sFunFact As String = String.Empty
	Private sDateJoined As String = String.Empty

	'Specific to Student Hobby - Shares with as Student
	Private iIsSelected As Integer = 0
	Private iLookupHobbyID As Integer = 0
	Private sLookupHobbyDescription  As String = String.Empty

	Private sStudentImage As String = String.Empty
	Private sStudentBackground As String = String.Empty
	
	Public Sub New()
		oDictionary_Error = New Dictionary(Of String, Object)
		oDictionary1 = New Dictionary(Of String, Object)
		oDictionary2 = New Dictionary(Of String, Object)
		oDictionary3 = New Dictionary(Of String, Object)
	End Sub

	Public Property Dictionary_Error() As Dictionary(Of String, Object)
		Get
			Return oDictionary_Error
		End Get
	    Set(value As Dictionary(Of String, Object))
			oDictionary_Error = value
	    End Set
	End Property

	Public Property Dictionary1() As Dictionary(Of String, Object)
		Get
			Return oDictionary1
		End Get
		Set(value As Dictionary(Of String, Object))
			oDictionary1 = value
		End Set
	End Property

	Public Property Dictionary2() As Dictionary(Of String, Object)
		Get
			Return oDictionary2
		End Get
		Set(value As Dictionary(Of String, Object))
			oDictionary2 = value
		End Set
	End Property

	Public Property Dictionary3() As Dictionary(Of String, Object)
		Get
			Return oDictionary3
		End Get
		Set(value As Dictionary(Of String, Object))
			oDictionary3 = value
		End Set
	End Property

	Public Property Result() As Boolean
		Get
			Return bResult
		End Get
		Set(value As Boolean)
			bResult = value
		End Set
	End Property

	Public Property Message() As String
		Get
			Return sMessage
		End Get
		Set(value As String)
			sMessage = value.Trim
		End Set
	End Property

	Public Property ID() As String
		Get
			Return sID
		End Get
		Set(value As String)
			sID = value.Trim
		End Set
	End Property

	Public Property AspNetUsersID() As String
		Get
			Return sAspNetUsersID
		End Get
		Set(value As String)
			sAspNetUsersID = value.Trim
		End Set
	End Property

	Public Property FullName() As String
		Get
			Return sFullName
		End Get
		Set(value As String)
			sFullName = value.Trim
		End Set
	End Property

	Public Property NickName() As String
		Get
			Return sNickName
		End Get
		Set(value As String)
			sNickName = value.Trim
		End Set
	End Property

	Public Property Biography() As String
		Get
			Return sBiography
		End Get
		Set(value As String)
			sBiography = value.Trim
		End Set
	End Property

	Public Property University() As String
		Get
			Return sUniversity
		End Get
		Set(value As String)
			sUniversity = value.Trim
		End Set
	End Property

	Public Property Course() As String
		Get
			Return sCourse
		End Get
		Set(value As String)
			sCourse = value.Trim
		End Set
	End Property

	Public Property StudyYear() As String
		Get
			Return sStudyYear
		End Get
		Set(value As String)
			sStudyYear = value.Trim
		End Set
	End Property

	Public Property Location() As String
		Get
			Return sLocation
		End Get
		Set(value As String)
			sLocation = value.Trim
		End Set
	End Property

	Public Property FunFact() As String
		Get
			Return sFunFact
		End Get
		Set(value As String)
			sFunFact = value.Trim
		End Set
	End Property

	Public Property DateJoined() As String
		Get
			Return sDateJoined
		End Get
		Set(value As String)
			sDateJoined = value.Trim
		End Set
	End Property

	Public Property IsSelected() As Integer
		Get
			Return iIsSelected
		End Get
		Set(value As Integer)
			iIsSelected = value
		End Set
	End Property

	Public Property LookupHobbyID() As Integer
		Get
			Return iLookupHobbyID
		End Get
		Set(value As Integer)
			iLookupHobbyID = value
		End Set
	End Property

	Public Property LookupHobbyDescription() As String
		Get
			Return sLookupHobbyDescription
		End Get
		Set(value As String)
			sLookupHobbyDescription = value.Trim
		End Set
	End Property

	Public Property StudentImage() As String
		Get
			Return sStudentImage
		End Get
		Set(value As String)
			sStudentImage = value.Trim
		End Set
	End Property

	Public Property StudentBackground() As String
		Get
			Return sStudentBackground
		End Get
		Set(value As String)
			sStudentBackground = value.Trim
		End Set
	End Property

#Region "IDisposable Support"
	Private disposedValue As Boolean ' To detect redundant calls

	' IDisposable
	Protected Overridable Sub Dispose(disposing As Boolean)
		If Not disposedValue Then
			If disposing Then
				oDictionary_Error.Clear
				oDictionary1.Clear()
				oDictionary2.Clear()
				oDictionary3.Clear()
			End If

			oDictionary_Error = Nothing
			oDictionary1 = Nothing
			oDictionary2 = Nothing
			oDictionary3 = Nothing
		End If
		disposedValue = True
	End Sub
	Public Sub Dispose() Implements IDisposable.Dispose
		Dispose(True)
	End Sub
#End Region

End Class
