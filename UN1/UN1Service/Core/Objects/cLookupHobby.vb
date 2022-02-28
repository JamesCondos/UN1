Public Class cLookupHobby
	Private sKey As String = String.Empty
	Private iID As Integer = 0
	Private sLocationType As String = String.Empty
	Private sDescription  As String = String.Empty
	Private sDescriptionFull As String = String.Empty
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

	Public Property LocationType() As String
		Get
			Return sLocationType
		End Get
		Set(value As String)
			sLocationType = value.Trim
		End Set
	End Property
	
	Public Property Description () As String
		Get
			Return sDescription 
		End Get
		Set(value As String)
			sDescription  = value.Trim
		End Set
	End Property

	Public Property DescriptionFull() As String
		Get
			Return sDescriptionFull
		End Get
		Set(value As String)
			sDescriptionFull = value.Trim
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
