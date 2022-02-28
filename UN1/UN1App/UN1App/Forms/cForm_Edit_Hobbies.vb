Imports Newtonsoft.Json
Imports Xamarin.Forms

Public Class cForm_Edit_Hobbies
	Inherits ContentPage

	Private Const TITLE_PAGE As String = TITLE_NAV_BACK
	Private Const COLOUR_BACKGROUND As String = "ECEBEB"

	Public Sub New()
		Initialise()
	End Sub

	Private Sub Initialise()
		Initialise_Page()
		Initialise_Content()
	End Sub

	Private Sub Initialise_Page()
		Title = TITLE_PAGE
		BackgroundColor = Color.FromHex(COLOUR_BACKGROUND)
	End Sub

	Private Sub Initialise_Content()
		Dim oLayout As New StackLayout With {.Spacing = 1,
			.VerticalOptions = LayoutOptions.StartAndExpand, .HorizontalOptions = LayoutOptions.StartAndExpand}

		Dim oLayout_LookupHobby = Initialise_LookupHobby()
		Dim oScrollView As New ScrollView With {.Content = oLayout_LookupHobby, .FlowDirection = FlowDirection.LeftToRight,
			.VerticalOptions = LayoutOptions.Start, .Margin = New Thickness(5, 0, 5, 0),
			.Orientation = ScrollOrientation.Vertical, .HorizontalScrollBarVisibility = False, .VerticalScrollBarVisibility = False}

		oLayout.Children.Add (SetUIToolBar_EditHobbies("Hobbies", Navigation))
		oLayout.Children.Add (oScrollView)

		Content = oLayout
	End Sub

	Private Function Initialise_LookupHobby() As StackLayout
		Dim oLayout As New StackLayout With {.Spacing = 1,
			.VerticalOptions = LayoutOptions.StartAndExpand, .HorizontalOptions = LayoutOptions.StartAndExpand}

		Try
			If goDataHelper_LookupHobby Is Nothing Then
				Preload_LookupHobby()
			End If

			Console.WriteLine("**** ID: {0}", goStudent.AspNetUsersID)

			Dim oWebAPI As New cWebAPI
			Dim oDataHelper_StuidentHobby  = oWebAPI.Get_StudentHobbies(gsAccessToken, goStudent.AspNetUsersID)
			oWebAPI = Nothing
			Dim oDictionary_StudentHobby As Dictionary(Of String, Object) = oDataHelper_StuidentHobby.Dictionary1

			Dim oDictionary As Dictionary(Of String, Object) = goDataHelper_LookupHobby.Result.Dictionary1
			For Each oKeyPair As KeyValuePair(Of String, Object) In oDictionary
				Dim oObject As Linq.JObject = oKeyPair.Value
			
				Dim sKey As String = oObject("Key")
				Dim sDescriptionFull As String = oObject("DescriptionFull")

				Dim bToggled As Boolean = False
				If oDictionary_StudentHobby.ContainsKey(sKey) = True Then
					Dim oObjectStudentHobby As Linq.JObject = oDictionary_StudentHobby.Item(sKey)

					Dim iIsSelected As Integer = CInt(oObjectStudentHobby("IsSelected"))

					If iIsSelected = VALUE_ONE Then bToggled = True
				End If

				Dim oSwitch_LookupHobby As Switch = SetUISwitch(sKey, bToggled)
				AddHandler oSwitch_LookupHobby.Toggled, Async Sub(sender, e)
						oSwitch_LookupHobby.IsEnabled = False 
						Await DoStudentHobbyAddModify(oSwitch_LookupHobby, sKey, sDescriptionFull)
						oSwitch_LookupHobby.IsEnabled = True
				    End Sub

				Dim oGrid_LookupHobby = SetUiSwitch(oSwitch_LookupHobby, sDescriptionFull)
				oLayout.Children.Add(oGrid_LookupHobby)
			Next
		Catch ex As Exception
			Console.WriteLine("********* {0}", ex.Message)
			Return Nothing
		End Try

		Return oLayout
	End Function

	Private Async Function DoStudentHobbyAddModify(Byval oSwitch_LookupHobby As Switch, Byval sKey As String, Byval sDescriptionFull As String) As Task
		Dim sMessage As String = String.Empty
		Try
			Dim oStudentHobby As New cStudentHobby
			oStudentHobby.ID = 0 'Not Used
			oStudentHobby.AspNetUsersID = goStudent.AspNetUsersID

			Select Case oSwitch_LookupHobby.IsToggled
			    Case True
					oStudentHobby.IsSelected = VALUE_ONE
				Case False
					oStudentHobby.IsSelected = VALUE_ZERO
			End Select

			oStudentHobby.LookupHobbyID = sKey
			oStudentHobby.LookupHobbyDescription = sDescriptionFull

			Dim oWebAPI As New cWebAPI
			DIm oDataHelper As cDataHelper = Await oWebAPI.AddModify_StudentHobby(Me, gsAccessToken, oStudentHobby)
			oWebAPI = Nothing
		Catch ex As Exception
			sMessage = ex.Message
		End Try

		If sMessage.Trim.Length > 0 Then
			Await DisplayAlert("Error", sMessage, "OK")
		End If
	End Function

	Private Function SetUIToolBar_EditHobbies(Byval sToolbarTitle As String, Byval oNavigation As INavigation) As Grid
		Dim oGrid As New Grid With {.BackgroundColor = Color.FromHex("FFFFFF"), .Padding = New Thickness(0,10,0,10)}

		oGrid.RowDefinitions.Add(New RowDefinition With {.Height = New GridLength(30, GridUnitType.Absolute)})
			oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Star)})
			oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Star)})
			oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Star)})

		Dim oBackButton = New Button With {.Text = "< Back", .Margin = 0, .Padding = 0, .FontAttributes = FontAttributes.Bold, .FontSize = 18, .TextColor = Color.Black, .BackgroundColor = Color.Transparent}
		AddHandler oBackButton.Clicked, Async Sub(sender, e)
				oBackButton.IsEnabled = False 
				Await oNavigation.PopModalAsync()
				oBackButton.IsEnabled = True
			End Sub

		oGrid.Children.Add (oBackButton, 0, 0)

		oGrid.Children.Add (New Label With {.Text = sToolbarTitle, .FontAttributes = FontAttributes.Bold, .FontSize = 18, .TextColor = Color.Black, .VerticalOptions = LayoutOptions.Center,
			.HorizontalOptions = LayoutOptions.Center, .Margin = New Thickness(0,0,20,0)}, 1, 0)

		'Dim oConfirmButton = New Button With {.Text = "Confirm", .Margin = 0, .Padding = 0, .FontAttributes = FontAttributes.Bold, .FontSize = 18, .TextColor = Color.Black, .BackgroundColor = Color.Transparent}
		'AddHandler oConfirmButton.Clicked, Async Sub(sender, e)
		'		oConfirmButton.IsEnabled = False 
		'		Await DisplayAlert("Confirm", "Confirm Changes", "OK")
		'		Await oNavigation.PopModalAsync()
		'		oConfirmButton.IsEnabled = True
		'	End Sub

		'oGrid.Children.Add (oConfirmButton, 2, 0)

		Return oGrid
	End Function
End Class
