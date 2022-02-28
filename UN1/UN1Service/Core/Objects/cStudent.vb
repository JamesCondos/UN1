Public Class cStudent
	Implements IDisposable 

	Private sKey As String = String.Empty
	Private iID As Integer = 0
	Private sAspNetUsersID  As String = String.Empty
	Private sFullName As String = String.Empty
	Private sNickName As String = String.Empty
	Private sBiography  As String = String.Empty
	Private sUniversity  As String = String.Empty
	Private sCourse  As String = String.Empty
	Private sStudyYear  As String = String.Empty
	Private sLocation  As String = String.Empty
	Private sFunFact  As String = String.Empty
	Private sDateJoined As String = String.Empty
	Private sDateTimeStamp As String = String.Empty
	Private iRecordStatus As Integer = 0

	Private sStudentBackground As String = String.Empty
	Private sStudentImage As String = String.Empty

	Private oDictionary_StudentHobby As Dictionary(Of String, Object) = Nothing

	Public Sub New()
		oDictionary_StudentHobby = New Dictionary(Of String, Object)
	End Sub

	Public Property Key() As String
		Get
			Return sKey
		End Get
		Set(value As String)
			sKey = VString(value)
		End Set
	End Property

	Public Property ID() As Integer
		Get
			Return iID
		End Get
		Set(value As Integer)
			iID = value
		End Set
	End Property

	Public Property AspNetUsersID () As String
		Get
			Return sAspNetUsersID 
		End Get
		Set(value As String)
			sAspNetUsersID  = VString(value)
		End Set
	End Property

	Public Property FullName() As String
		Get
			Return sFullName
		End Get
		Set(value As String)
			sFullName = VString(value)
		End Set
	End Property

	Public Property NickName() As String
		Get
			Return sNickName
		End Get
		Set(value As String)
			sNickName = VString(value)
		End Set
	End Property

	Public Property Biography () As String
		Get
			Return sBiography 
		End Get
		Set(value As String)
			sBiography  = VString(value)
		End Set
	End Property

	Public Property University () As String
		Get
			Return sUniversity 
		End Get
		Set(value As String)
			sUniversity  = VString(value)
		End Set
	End Property

	Public Property Course () As String
		Get
			Return sCourse 
		End Get
		Set(value As String)
			sCourse  = VString(value)
		End Set
	End Property

	Public Property StudyYear () As String
		Get
			Return sStudyYear 
		End Get
		Set(value As String)
			sStudyYear  = VString(value)
		End Set
	End Property

	Public Property Location () As String
		Get
			Return sLocation 
		End Get
		Set(value As String)
			sLocation  = VString(value)
		End Set
	End Property

	Public Property FunFact () As String
		Get
			Return sFunFact 
		End Get
		Set(value As String)
			sFunFact  = VString(value)
		End Set
	End Property

	Public Property DateJoined() As String
		Get
			Return sDateJoined
		End Get
		Set(value As String)
			sDateJoined = VString(value)
		End Set
	End Property

	Public Property DateTimeStamp() As String
		Get
			Return sDateTimeStamp
		End Get
		Set(value As String)
			sDateTimeStamp = VString(value)
		End Set
	End Property

	Public Property RecordStatus() As Integer
		Get
			Return iRecordStatus
		End Get
		Set(value As Integer)
			iRecordStatus = value
		End Set
	End Property

	Public Property Dictionary_StudentHobby() As Dictionary(Of String, Object)
		Get
			Return oDictionary_StudentHobby
		End Get
		Set(value As Dictionary(Of String, Object))
			oDictionary_StudentHobby = value
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

	Public Property StudentImage() As String
		Get
			Return sStudentImage
		End Get
		Set(value As String)
			sStudentImage = value.Trim
		End Set
	End Property

#Region "IDisposable Support"
	Private disposedValue As Boolean ' To detect redundant calls

	' IDisposable
	Protected Overridable Sub Dispose(disposing As Boolean)
		If Not disposedValue Then
			If disposing Then
				oDictionary_StudentHobby.Clear
			End If
			oDictionary_StudentHobby = Nothing
		End If
		disposedValue = True
	End Sub
	Public Sub Dispose() Implements IDisposable.Dispose
		Dispose(True)
	End Sub
#End Region
End Class
