Imports Xamarin.Forms

Public Class cForm_Edit_Profile
	Inherits ContentPage

	Private Const TITLE_PAGE As String = TITLE_FORM_EDITPROFILE
	Private Const COLOUR_BACKGROUND As String = "FFFFFF"

	Private oEntry_FullName As Entry = Nothing
	Private oEntry_NickName As Entry = Nothing
	Private oEntry_Biography As Entry = Nothing 
	Private oEntry_University As Entry = Nothing 
	Private oEntry_Course As Entry = Nothing 
	Private oEntry_StudyYear As Entry = Nothing 
	Private oEntry_Location As Entry = Nothing 
	Private oEntry_FunFact As Entry = Nothing 

	Public Sub New()
		Initialise()
	End Sub

	Private Sub Initialise()
		Initialise_Page()
		Initialise_Content()

		Preload_Data()
	End Sub

	Private Sub Initialise_Page()
		Title = TITLE_PAGE
		BackgroundColor = Color.FromHex(COLOUR_BACKGROUND)
	End Sub

	Private Sub Initialise_Content()
		Dim oLayout_Entry As New StackLayout With {.Spacing = 1,
			.VerticalOptions = LayoutOptions.StartAndExpand, .HorizontalOptions = LayoutOptions.StartAndExpand}

		oEntry_FullName = SetUiLabelEntryPair(oLayout_Entry, "Full Name", "")
		oEntry_NickName = SetUiLabelEntryPair(oLayout_Entry, "Nickname", "")
		oEntry_Biography = SetUiLabelEntryPair(oLayout_Entry, "Biography", "")

		oLayout_Entry.Children.Add (SetUISettingsNavigation("Hobbies", Navigation, New cForm_Edit_Hobbies))

		oEntry_University = SetUiLabelEntryPair(oLayout_Entry, "University", "")
		oEntry_Course = SetUiLabelEntryPair(oLayout_Entry, "Course", "")
		oEntry_StudyYear = SetUiLabelEntryPair(oLayout_Entry, "Study Year", "")
		oEntry_Location = SetUiLabelEntryPair(oLayout_Entry, "Location", "")
		oEntry_FunFact = SetUiLabelEntryPair(oLayout_Entry, "Fun Fact", "")

		Dim oScrollView As New ScrollView With {.Content = oLayout_Entry, .FlowDirection = FlowDirection.LeftToRight,
			.VerticalOptions = LayoutOptions.Start, .Margin = New Thickness(5, 0, 5, 0),
			.Orientation = ScrollOrientation.Vertical, .HorizontalScrollBarVisibility = False, .VerticalScrollBarVisibility = False}

		Dim oLayout As New StackLayout With {.Spacing = 1,
			.VerticalOptions = LayoutOptions.StartAndExpand, .HorizontalOptions = LayoutOptions.StartAndExpand}

		oLayout.Children.Add (SetUIToolBar_EditProfile("Edit Profile", Navigation))
		oLayout.Children.Add (oScrollView)

		Content = oLayout	
	End Sub

	Private Sub Preload_Data()
		Try
			oEntry_FullName.Text = goStudent.FullName
			oEntry_NickName .Text = goStudent.NickName
			oEntry_Biography.Text = goStudent.Biography
			oEntry_University.Text = goStudent.University
			oEntry_Course.Text = goStudent.Course
			oEntry_StudyYear.Text = goStudent.StudyYear
			oEntry_Location.Text = goStudent.Location
			oEntry_FunFact.Text = goStudent.FunFact

		Catch ex As Exception
			Console.WriteLine(ex.Message)
		End Try
	End Sub

	Private Function SetUIToolBar_EditProfile(Byval sToolbarTitle As String, Byval oNavigation As INavigation) As Grid
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

		Dim oConfirmButton = New Button With {.Text = "Confirm", .Margin = 0, .Padding = 0, .FontAttributes = FontAttributes.Bold, .FontSize = 18, .TextColor = Color.Black, .BackgroundColor = Color.Transparent}
		AddHandler oConfirmButton.Clicked, Async Sub(sender, e)
				oConfirmButton.IsEnabled = False 
				Await DoStudentAddModify()			
				oConfirmButton.IsEnabled = True
			End Sub

		oGrid.Children.Add (oConfirmButton, 2, 0)

		Return oGrid
	End Function

	Private Async Function DoStudentAddModify() As Task
		Dim sMessage As String = String.Empty
		Try
			Dim sFullName As String = VString(oEntry_FullName.Text)
			Dim sNickName As String = VString(oEntry_NickName.Text)
			Dim sBiography As String = VString(oEntry_Biography.Text)
			Dim sUniversity As String = VString(oEntry_University.Text)
			Dim sCourse As String = VString(oEntry_Course.Text)
			Dim sStudyYear As String = VString(oEntry_StudyYear.Text)
			Dim sLocation As String = VString(oEntry_Location.Text)
			Dim sFunFact As String = VString(oEntry_FunFact.Text)

			If ValidateStringEmpty(Me, "Full Name", sFullName) = False Then
				oEntry_FullName.Focus()
				Return
			End If

			Dim oStudent As New cStudent
			oStudent.ID = 0 'Not Used
			oStudent.AspNetUsersID = goStudent.AspNetUsersID
			oStudent.FullName = sFullName
			oStudent.NickName =sNickName
			oStudent.Biography = sBiography
			oStudent.University = sUniversity
			oStudent.Course = sCourse
			oStudent.StudyYear = sStudyYear
			oStudent.Location = sLocation
			oStudent.FunFact = sFunFact

			Dim oWebAPI As New cWebAPI
			DIm oDataHelper As cDataHelper = oWebAPI.AddModify_Student(Me, gsAccessToken, oStudent)
			oWebAPI = Nothing

			goStudent.ID = oDataHelper.ID
			goStudent.AspNetUsersID = oDataHelper.AspNetUsersID
			goStudent.FullName = oDataHelper.FullName
			goStudent.NickName = oDataHelper.NickName
			goStudent.Biography = oDataHelper.Biography
			goStudent.University = oDataHelper.University
			goStudent.Course = oDataHelper.Course
			goStudent.StudyYear = oDataHelper.StudyYear
			goStudent.Location = oDataHelper.Location
			goStudent.FunFact = oDataHelper.FunFact

			Await Navigation.PopModalAsync()
		Catch ex As Exception
			sMessage = ex.Message
		End Try

		If sMessage.Trim.Length > 0 Then
			Await DisplayAlert("Error", sMessage, "OK")
		End If
	End Function

End Class
