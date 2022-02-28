Public Class cStudentImages
	Private sKey As String = String.Empty
	Private iID As Integer = 0
	Private sAspNetUsersID As String = String.Empty
	Private sStudentBackground As String = String.Empty
	Private sStudentImage As String = String.Empty
	Private sDateJoined As String = String.Empty
	Private sDateTimeStamp As String = String.Empty
	Private iRecordStatus As Integer = 0

	Private sBase64String As String = String.Empty

	Public Property Key() As String
		Get
			Return sKey
		End Get
		Set(value As String)
			sKey = value.Trim
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

	Public Property AspNetUsersID() As String
		Get
			Return sAspNetUsersID
		End Get
		Set(value As String)
			sAspNetUsersID = value.Trim
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

	Public Property DateJoined() As String
		Get
			Return sDateJoined
		End Get
		Set(value As String)
			sDateJoined = value.Trim
		End Set
	End Property

	Public Property DateTimeStamp() As String
		Get
			Return sDateTimeStamp
		End Get
		Set(value As String)
			sDateTimeStamp = value.Trim
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

	Public Property Base64String() As String
		Get
			Return sBase64String
		End Get
		Set(value As String)
			sBase64String = value.Trim
		End Set
	End Property
End Class
