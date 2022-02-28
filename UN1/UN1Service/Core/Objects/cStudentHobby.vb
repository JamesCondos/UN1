Public Class cStudentHobby
	Private sKey As String = String.Empty
	Private iID As Integer = 0
	Private sAspNetUsersID  As String = String.Empty
	Private iIsSelected As Integer = 0
	Private iLookupHobbyID As Integer = 0
	Private sLookupHobbyDescription  As String = String.Empty
	Private sDateTimeStamp As String = String.Empty
	Private iRecordStatus As Integer = 0

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

	Public Property AspNetUsersID () As String
		Get
			Return sAspNetUsersID 
		End Get
		Set(value As String)
			sAspNetUsersID  = value.Trim
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

	Public Property LookupHobbyDescription () As String
		Get
			Return sLookupHobbyDescription 
		End Get
		Set(value As String)
			sLookupHobbyDescription  = value.Trim
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
End Class
