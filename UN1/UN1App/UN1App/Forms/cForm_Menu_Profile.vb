Imports Newtonsoft.Json
Imports Xamarin.Forms

Public Class cForm_Menu_Profile
	Inherits ContentPage

	Private Const TITLE_PAGE As String = TITLE_MENU_Profile
	Private Const COLOUR_BACKGROUND As String = "FFFFFF"

	Private oImage_Background As Image = Nothing
	Private oImage_Person As Image = Nothing

	Private oLabel_FullName As Label = Nothing

	Private oLabel_Course As Label = Nothing
	Private oLabel_Separator_CourseUniversity As Label = Nothing
	Private oLabel_University As Label = Nothing

	Private oLabel_Location As Label = Nothing
	Private oLabel_Separator_LocationStudyYear As Label = Nothing
	Private oLabel_StudyYear As Label = Nothing

	Private oLabel_Biography As Label = Nothing

	Private oImage_LivesIn As Image = Nothing
	Private oLabel_LivesIn As Label = Nothing
	
	Private oImage_NickName As Image = Nothing
	Private oLabel_NickName As Label = Nothing

	Private oLabel_JoinedOn As Label = Nothing

	Private oStackLayout_Hobbies As StackLayout = Nothing

	Public Sub New()
		Initialise()
	End Sub

	Public Sub Initialise()
		Initialise_Page()
		Initialise_Content()
	End Sub

	Private Sub Initialise_Page()
		Title = TITLE_PAGE
		BackgroundColor = Color.FromHex(COLOUR_BACKGROUND)
	End Sub

	Private Sub Initialise_Content()
		Dim oStack_Top = New StackLayout With {.Spacing = 1,
			.VerticalOptions = LayoutOptions.Start, .HorizontalOptions = LayoutOptions.StartAndExpand}

		oStack_Top.Children.Add(SetUIImageAndSearchBar(goStudent.StudentImage, Navigation))

		Dim oLayout_Main As New StackLayout With {.Spacing = 1}
		oLayout_Main.Children.Add(oStack_Top)
		oLayout_Main.Children.Add(WrapLayoutIntoScrollView())

		Content = oLayout_Main
	End Sub

	Private Sub cForm_Menu_Profile_Appearing(sender As Object, e As EventArgs) Handles Me.Appearing
		Initialise_CurrentUserInformation()
		RefreshPageInformation()
	End Sub

	Private Sub RefreshPageInformation()
		oLabel_FullName.Text = goStudent.FullName

		RefreshPageInformation_StudentBackground()
		RefreshPageInformation_StudentImage()

		RefreshPageInformation_CourseUniversity()
		RefreshPageInformation_LocationStudyYear()
		RefreshPageInformation_Biography()
		RefreshPageInformation_LivesIn()
		RefreshPageInformation_NickName()
		
		oLabel_JoinedOn.Text = CreateJoinedOnString(goStudent.DateJoined)

		 UpdateUIProfileDetailsHobbies(goStudent.Dictionary_StudentHobby)
	End Sub

	Private Sub RefreshPageInformation_StudentBackground()
		If goStudent.StudentBackground.Trim.Length = 0 Then
			oImage_Background.Source = IMAGE_BACKGROUND
		Else
			Dim sURL As String = String.Empty
			If WEBSITE_USE_LIVE = True Then
				sURL = String.Format("{0}{1}/{2}/{3}", URL_LIVE, FOLDER_APP_IMAGES, goStudent.AspNetUsersID, goStudent.StudentBackground)
			Else
				sURL = String.Format("{0}{1}/{2}/{3}", URL_DEVELOPMENT, FOLDER_APP_IMAGES, goStudent.AspNetUsersID, goStudent.StudentBackground)
			End If

			oImage_Background.Source = ImageSource.FromUri(new Uri(sURL))
		End If
	End Sub
	
	Private Sub RefreshPageInformation_StudentImage()
		If goStudent.StudentImage.Trim.Length = 0 Then
			oImage_Person.Source = IMAGE_PERSON
		Else
			Dim sURL As String = String.Empty
			If WEBSITE_USE_LIVE = True Then
				sURL = String.Format("{0}{1}/{2}/{3}", URL_LIVE, FOLDER_APP_IMAGES, goStudent.AspNetUsersID, goStudent.StudentImage)
			Else
				sURL = String.Format("{0}{1}/{2}/{3}", URL_DEVELOPMENT, FOLDER_APP_IMAGES, goStudent.AspNetUsersID, goStudent.StudentImage)
			End If

			oImage_Person.Source = ImageSource.FromUri(new Uri(sURL))
		End If
	End Sub

	Private Sub RefreshPageInformation_NickName()
		If goStudent.NickName.Trim.Length = 0 Then
			oImage_NickName.IsVisible = False
			oLabel_NickName.IsVisible = False 
		Else
			oImage_NickName.IsVisible = True
			oLabel_NickName.IsVisible = True
			oLabel_NickName.Text = CreateNickNameString(goStudent.NickName)
		End If
	End Sub

	Private Sub RefreshPageInformation_LivesIn()
		If goStudent.Location.Trim.Length = 0 Then
			oImage_LivesIn.IsVisible = False 
			oLabel_LivesIn.IsVisible = False 
		Else
			oImage_LivesIn.IsVisible = True
			oLabel_LivesIn.IsVisible = True
			oLabel_LivesIn.Text = CreateLiveInString(goStudent.Location)
		End If
	End Sub

	Private Sub RefreshPageInformation_Biography()
		If goStudent.Biography.Trim.Length = 0 Then
			oLabel_Biography.Text = TAG_NA
		Else
			oLabel_Biography.Text = goStudent.Biography.Trim
		End If
	End Sub

	Private Sub RefreshPageInformation_LocationStudyYear()
		oLabel_Location.Text = goStudent.Location.Trim
		oLabel_StudyYear.Text = goStudent.StudyYear.Trim

		Select Case True
		Case goStudent.Location.Trim.Length > 0 And goStudent.StudyYear.Trim.Length > 0
			oLabel_Separator_LocationStudyYear.IsVisible = True
			oLabel_Location.IsVisible = True
			oLabel_StudyYear.IsVisible = True
		Case goStudent.Location.Trim.Length = 0 And goStudent.StudyYear.Trim.Length > 0
			oLabel_Separator_LocationStudyYear.IsVisible = False
			oLabel_Location.IsVisible = False
			oLabel_StudyYear.IsVisible = True		
		Case goStudent.Location.Trim.Length > 0 And goStudent.StudyYear.Trim.Length = 0
			oLabel_Separator_LocationStudyYear.IsVisible = False
			oLabel_Location.IsVisible = True
			oLabel_StudyYear.IsVisible = False
		Case Else
			oLabel_Separator_LocationStudyYear.IsVisible = False
			oLabel_Location.IsVisible = False
			oLabel_StudyYear.IsVisible = False
		End Select
	End Sub

	Private Sub RefreshPageInformation_CourseUniversity()
		oLabel_Course.Text = goStudent.Course.Trim
		oLabel_University.Text = goStudent.University.Trim

		Select Case True
		Case goStudent.Course.Trim.Length > 0 And goStudent.University.Trim.Length > 0
			oLabel_Separator_CourseUniversity.IsVisible = True
			oLabel_Course.IsVisible = True
			oLabel_University.IsVisible = True
		Case goStudent.Course.Trim.Length = 0 And goStudent.University.Trim.Length > 0
			oLabel_Separator_CourseUniversity.IsVisible = False
			oLabel_Course.IsVisible = False
			oLabel_University.IsVisible = True		
		Case goStudent.Course.Trim.Length > 0 And goStudent.University.Trim.Length = 0
			oLabel_Separator_CourseUniversity.IsVisible = False
			oLabel_Course.IsVisible = True
			oLabel_University.IsVisible = False
		Case Else
			oLabel_Separator_CourseUniversity.IsVisible = False
			oLabel_Course.IsVisible = False
			oLabel_University.IsVisible = False
		End Select
	End Sub

	Private Sub UpdateUIProfileDetailsHobbies(ByVal oDictionary_Hobbies As Dictionary(Of String, Object))
		oStackLayout_Hobbies.Children.Clear

		Dim iMaximumHobby As Integer = oDictionary_Hobbies.Count

		If iMaximumHobby = 0 Then Return

		For Each oKeyPair As KeyValuePair(Of String, Object) In oDictionary_Hobbies
			Dim oObject As Linq.JObject = oKeyPair.Value
			
			Dim iIsSelected As Integer = CInt(oObject("IsSelected"))
			Dim sLookupHobbyDescription As String = oObject("LookupHobbyDescription")

			If iIsSelected = VALUE_ONE Then
				Dim oLabel_Hobby = New Label With {.Text = sLookupHobbyDescription, .FontAttributes = FontAttributes.Bold, .TextColor = Color.Black}
				oStackLayout_Hobbies.Children.Add(oLabel_Hobby)

				Dim oLabel_Dot As New Label With {.Text = TAG_DOT, .FontAttributes = FontAttributes.Bold, .TextColor = Color.Black}
				oStackLayout_Hobbies.Children.Add(oLabel_Dot)
			End If
		Next

		If oStackLayout_Hobbies.Children.Count > 0 Then
			oStackLayout_Hobbies.Children.RemoveAt(oStackLayout_Hobbies.Children.Count - 1)
		End If

	End Sub

	Private Function SetUIProfileDetailsHobbies(ByVal oDictionary_Hobbies As Dictionary(Of String, Object)) As StackLayout
		Dim oLayout = New StackLayout With {.BackgroundColor = Color.Transparent, .Orientation = StackOrientation.Horizontal,
			.HorizontalOptions = LayoutOptions.CenterAndExpand}

		Dim iMaximumHobby As Integer = oDictionary_Hobbies.Count

		If iMaximumHobby = 0 Then Return oLayout

		For Each oKeyPair As KeyValuePair(Of String, Object) In oDictionary_Hobbies
			Dim oObject As Linq.JObject = oKeyPair.Value
			
			Dim iIsSelected As Integer = CInt(oObject("IsSelected"))
			Dim sLookupHobbyDescription As String = oObject("LookupHobbyDescription")

			If iIsSelected = VALUE_ONE Then
				Dim oLabel_Hobby = New Label With {.Text = sLookupHobbyDescription, .FontAttributes = FontAttributes.Bold, .TextColor = Color.Black}
				oLayout.Children.Add(oLabel_Hobby)

				Dim oLabel_Dot As New Label With {.Text = TAG_DOT, .FontAttributes = FontAttributes.Bold, .TextColor = Color.Black}
				oLayout.Children.Add(oLabel_Dot)
			End If
		Next

		If oLayout.Children.Count > 0 Then
			oLayout.Children.RemoveAt(oLayout.Children.Count - 1)
		End If

		Return oLayout
	End Function

	Private Function WrapLayoutIntoScrollView() As ScrollView
		oLabel_FullName = SetUIProfileName(goStudent.FullName)

		Dim oLayout As New StackLayout With {.Spacing = 1}

		oLayout.Children.Add(SetUIProfileBackgroundPersonImages())

		oLayout.Children.Add(oLabel_FullName) 'ok

		Dim oStackLayout_CourseUniversity = SetUIProfileDetailsCourseUniversity(goStudent.Course, goStudent.University)
		If oStackLayout_CourseUniversity IsNot Nothing Then
			oLayout.Children.Add(oStackLayout_CourseUniversity) 'ok
		End If
		
		oLayout.Children.Add(SetUIProfileDetailsLocationStudyYear(goStudent.Location, goStudent.StudyYear)) 'ok

		'******************************************************************
		oStackLayout_Hobbies = SetUIProfileDetailsHobbies(goStudent.Dictionary_StudentHobby) 'ok
		Dim oScrollView_Hobbies As New ScrollView With {.Content = oStackLayout_Hobbies, .FlowDirection = FlowDirection.LeftToRight,
			.VerticalOptions = LayoutOptions.CenterAndExpand, .Margin = New Thickness(5, 5, 5, 5),
			.Orientation = ScrollOrientation.Horizontal, .HorizontalScrollBarVisibility = True, .VerticalScrollBarVisibility = True}
		oLayout.Children.Add(oScrollView_Hobbies)
		'******************************************************************

		oLayout.Children.Add(SetUIProfileBorder())
		oLayout.Children.Add(SetUIProfileDetailsBiography(goStudent.Biography)) 'ok
		oLayout.Children.Add(SetUIProfileBorder())

		Dim oButton_EditProfile = SetUIButtonSystem("Edit Profile") 'ok
		AddHandler oButton_EditProfile.Clicked, Async Sub(sender, e)
				oButton_EditProfile.IsEnabled = False
				Await Navigation.PushModalAsync(SetNavigationPage(New cForm_Edit_Profile))
				oButton_EditProfile.IsEnabled = True
			End Sub
		oLayout.Children.Add(oButton_EditProfile)

		Dim oGrid_LivesIn = SetUIProfileLivesIn(goStudent.Location) 'ok
		oLayout.Children.Add(oGrid_LivesIn)

		Dim oGrid_NickName = SetUIProfileNickName(goStudent.NickName) 'ok
		oLayout.Children.Add(oGrid_NickName)

		Dim oGrid_JoinedOn = SetUIProfileJoinedOn(goStudent.DateJoined)
		oLayout.Children.Add(oGrid_JoinedOn)

		oLayout.Children.Add(SetUIProfileBorder())

		Dim oLabel_FriendsTitle = SetUILabel("Friends", TextAlignment.Start)
		oLabel_FriendsTitle.Margin = New Thickness(5, 0, 0, 0)
		oLayout.Children.Add(oLabel_FriendsTitle)

		Dim sFriendsCount As String = String.Format("{0} friends", 229.ToString)
		Dim oLabel_FriendsCount = SetUILabel(sFriendsCount, TextAlignment.Start)
		oLabel_FriendsCount.Margin = New Thickness(5, 0, 0, 0)
		oLayout.Children.Add(oLabel_FriendsCount)

		oLayout.Children.Add(SetUIFriendBlocks())

		Dim oButton_SeeAllFriends = SetUIButtonSystem("See All Friends")
		AddHandler oButton_SeeAllFriends.Clicked, Async Sub(sender, e)
				oButton_SeeAllFriends.IsEnabled = False
				Await DisplayAlert("All Friends", "Display a list of all friends", "OK")
				oButton_SeeAllFriends.IsEnabled = True
			End Sub
		oLayout.Children.Add(oButton_SeeAllFriends)

		Dim oSpacer = New Frame With {.HeightRequest = 5}
		oLayout.Children.Add(oSpacer)

		' ==============================================================================================

		Dim oScrollView As New ScrollView With {.Content = oLayout, .FlowDirection = FlowDirection.LeftToRight,
			.VerticalOptions = LayoutOptions.StartAndExpand, .Margin = New Thickness(5, 0, 5, 0),
			.Orientation = ScrollOrientation.Vertical, .HorizontalScrollBarVisibility = False, .VerticalScrollBarVisibility = False}

		Return oScrollView
	End Function

	Private Function SetUIFriendBlocks() As FlexLayout
		Dim oLayout = New FlexLayout With {.Direction = FlexDirection.Row, .Wrap = FlexWrap.Wrap, .JustifyContent = FlexJustify.SpaceEvenly}

		Dim oGrid00 = SetUIFriendBlock("BackgroundFriends.png", "ProfilePicture.png", "John Smith", "UniversityMelbourne.png", "University of Melbourne")
		oLayout.Children.Add(oGrid00)

		Dim oGrid01 = SetUIFriendBlock("BackgroundFriends.png", "ProfilePicture.png", "John Smith", "UniversityMelbourne.png", "University of Melbourne")
		oLayout.Children.Add(oGrid01)

		Dim oGrid02 = SetUIFriendBlock("BackgroundFriends.png", "ProfilePicture.png", "John Smith", "UniversityMelbourne.png", "University of Melbourne")
		oLayout.Children.Add(oGrid02)

		Dim oGrid03 = SetUIFriendBlock("BackgroundFriends.png", "ProfilePicture.png", "John Smith", "UniversityMelbourne.png", "University of Melbourne")
		oLayout.Children.Add(oGrid03)

		Dim oGrid04 = SetUIFriendBlock("BackgroundFriends.png", "ProfilePicture.png", "John Smith", "UniversityMelbourne.png", "University of Melbourne")
		oLayout.Children.Add(oGrid04)

		Dim oGrid05 = SetUIFriendBlock("BackgroundFriends.png", "ProfilePicture.png", "John Smith", "UniversityMelbourne.png", "University of Melbourne")
		oLayout.Children.Add(oGrid05)

		Return oLayout
	End Function

	Private Function SetUIProfileJoinedOn(ByVal sJoinedOn As String) As Grid
		Dim oGrid As New Grid With {.BackgroundColor = Color.FromHex("FFFFF"), .HorizontalOptions = LayoutOptions.StartAndExpand,
			.Padding = New Thickness(0, 5, 0, 5)}

		oGrid.RowDefinitions.Add(New RowDefinition With {.Height = New GridLength(1, GridUnitType.Star)})
		oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Auto)})
		oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Star)})

		Dim oImage = New Image With {.Source = "Calendar.png", .Aspect = Aspect.AspectFit, .HeightRequest = 50, .HorizontalOptions = LayoutOptions.Start}

		oGrid.Children.Add(oImage, 0, 0)

		oLabel_JoinedOn = New Label With {.Text = CreateJoinedOnString(sJoinedOn), .FontAttributes = FontAttributes.Bold, .FontSize = 18, .HorizontalOptions = LayoutOptions.StartAndExpand,
			.TextColor = Color.Black, .VerticalTextAlignment = TextAlignment.Center,
			.Margin = New Thickness(0, 0, 20, 0)}

		oGrid.Children.Add(oLabel_JoinedOn, 1, 0)

		Return oGrid
	End Function

	Private Function CreateLiveInString(Byval sLivesIn As String) As String
		Return String.Format("Lives in {0}", sLivesIn.Trim)
	End Function

	Private Function CreateNickNameString(Byval sNickName As String) As String
		Return String.Format("Nickname is {0}", sNickname.Trim)
	End Function

	Private Function CreateJoinedOnString(ByVal sJoinedOn As String) As String
		If sJoinedOn.Trim.Length = 0 Then
			sJoinedOn = ValidateDate_MMMMyyyyy(DateTime.Today.ToString)
		Else
			sJoinedOn = ValidateDate_MMMMyyyyy(sJoinedOn)
		End If
	
		Return String.Format("Joined on {0}", sJoinedOn.Trim)
	End Function

	Private Function SetUIProfileLivesIn(ByVal sLivesIn As String) As Grid
		If sLivesIn.Trim.Length = 0 Then sLivesIn = CreateLiveInString(TAG_NA)

		Dim oGrid As New Grid With {.BackgroundColor = Color.FromHex("FFFFF"), .HorizontalOptions = LayoutOptions.StartAndExpand,
			.Padding = New Thickness(0, 5, 0, 5)}

		oGrid.RowDefinitions.Add(New RowDefinition With {.Height = New GridLength(1, GridUnitType.Star)})
		oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Auto)})
		oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Star)})

		oImage_LivesIn = New Image With {.Source = "House.png", .Aspect = Aspect.AspectFit, .HeightRequest = 50, .HorizontalOptions = LayoutOptions.Start}
		oGrid.Children.Add(oImage_LivesIn, 0, 0)

		oLabel_LivesIn = New Label With {.Text = CreateLiveInString(sLivesIn), .FontAttributes = FontAttributes.Bold, .FontSize = 18, .HorizontalOptions = LayoutOptions.StartAndExpand,
			.TextColor = Color.Black, .VerticalTextAlignment = TextAlignment.Center,
			.Margin = New Thickness(0, 0, 20, 0)}

		oGrid.Children.Add(oLabel_LivesIn, 1, 0)

		Return oGrid
	End Function

	Private Function SetUIProfileNickName(ByVal sNickname As String) As Grid
		If sNickname.Trim.Length = 0 Then sNickname = CreateNickNameString(TAG_NA)

		Dim oGrid As New Grid With {.BackgroundColor = Color.FromHex("FFFFF"), .HorizontalOptions = LayoutOptions.StartAndExpand,
			.Padding = New Thickness(0, 5, 0, 5)}

		oGrid.RowDefinitions.Add(New RowDefinition With {.Height = New GridLength(1, GridUnitType.Star)})
		oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Auto)})
		oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Star)})

		oImage_NickName = New Image With {.Source = "Smiley.png", .Aspect = Aspect.AspectFit, .HeightRequest = 50, .HorizontalOptions = LayoutOptions.Start}
		oGrid.Children.Add(oImage_NickName, 0, 0)

		oLabel_NickName = New Label With {.Text = CreateNickNameString(sNickname), .FontAttributes = FontAttributes.Bold, .FontSize = 18, .HorizontalOptions = LayoutOptions.StartAndExpand,
			.TextColor = Color.Black, .VerticalTextAlignment = TextAlignment.Center,
			.Margin = New Thickness(0, 0, 20, 0)}

		oGrid.Children.Add(oLabel_NickName, 1, 0)

		Return oGrid
	End Function

	Private Function SetUIProfileDetailsBiography(ByVal sBiography As String) As StackLayout
		Dim oLabel_Title = New Label With {.Text = "Biography", .FontAttributes = FontAttributes.Bold, .TextColor = Color.Black, .HorizontalTextAlignment = TextAlignment.Center}
		oLabel_Biography = New Label With {.Text = sBiography, .FontAttributes = FontAttributes.None, .TextColor = Color.Black, .HorizontalTextAlignment = TextAlignment.Center}

		Dim oLayout = New StackLayout With {.BackgroundColor = Color.Transparent, .Orientation = StackOrientation.Vertical,
			.HorizontalOptions = LayoutOptions.CenterAndExpand, .Spacing = 1}

		oLayout.Children.Add(oLabel_Title)
		oLayout.Children.Add(oLabel_Biography)

		Return oLayout
	End Function

	Private Function SetUIProfileBorder() As Frame
		Dim oFrame = New Frame With {.Margin = 5, .Padding = New Thickness(0, 2, 0, 0), .BorderColor = Color.Black, .BackgroundColor = Color.Black, .HorizontalOptions = LayoutOptions.FillAndExpand}
		Return oFrame
	End Function

	Private Function SetUIProfileName(ByVal sFullname As String) As Label
		Dim oLabel = New Label With {.Text = sFullname, .FontSize = 18, .FontAttributes = FontAttributes.Bold, .TextColor = Color.Black,
			.VerticalOptions = LayoutOptions.Start, .HorizontalOptions = LayoutOptions.Center}
		Return oLabel
	End Function

	Private Function SetUIProfileDetailsCourseUniversity(ByVal sCourse As String, ByVal sUniversity As String) As StackLayout
		oLabel_Separator_CourseUniversity =  New Label With {.Text = TAG_DOT, .FontAttributes = FontAttributes.Bold, .TextColor = Color.Black}
		oLabel_Course = New Label With {.Text = sCourse, .FontAttributes = FontAttributes.Bold, .TextColor = Color.Black}
		oLabel_University = New Label With {.Text = sUniversity, .FontAttributes = FontAttributes.Bold, .TextColor = Color.Black}

		Dim oLayout = New StackLayout With {.BackgroundColor = Color.Transparent, .Orientation = StackOrientation.Horizontal,
			.HorizontalOptions = LayoutOptions.Center}

		oLayout.Children.Add(oLabel_Course)
		oLayout.Children.Add(oLabel_Separator_CourseUniversity)
		oLayout.Children.Add(oLabel_University)

		Return oLayout
	End Function

	Private Function SetUIProfileDetailsLocationStudyYear(ByVal sLocation As String, ByVal sStudyYear As String) As StackLayout
		oLabel_Separator_LocationStudyYear =  New Label With {.Text = TAG_DOT, .FontAttributes = FontAttributes.Bold, .TextColor = Color.Black}
		oLabel_Location = New Label With {.Text = sLocation, .FontAttributes = FontAttributes.Bold, .TextColor = Color.Black}
		oLabel_StudyYear = New Label With {.Text = sStudyYear, .FontAttributes = FontAttributes.Bold, .TextColor = Color.Black}

		Dim oLayout = New StackLayout With {.BackgroundColor = Color.Transparent, .Orientation = StackOrientation.Horizontal,
			.HorizontalOptions = LayoutOptions.Center}

		oLayout.Children.Add(oLabel_Location)
		oLayout.Children.Add(oLabel_Separator_LocationStudyYear)
		oLayout.Children.Add(oLabel_StudyYear)

		Return oLayout
	End Function

	Private Function SetUIProfileBackgroundPersonImages() As Grid
		Dim oGrid As New Grid With {.BackgroundColor = Color.FromHex(COLOUR_BACKGROUND), .VerticalOptions = LayoutOptions.Start,
			.Margin = New Thickness(5, 0, 5, 0)}

		oGrid.RowDefinitions.Add(New RowDefinition With {.Height = New GridLength(1, GridUnitType.Auto)})
		oGrid.RowDefinitions.Add(New RowDefinition With {.Height = New GridLength(1, GridUnitType.Auto)})

		oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Star)})

		oGrid.Children.Add(SetUIProfileBackgroundImage(IMAGE_BACKGROUND), 0, 0)
		oGrid.Children.Add(SetUIProfilePersonImage(IMAGE_PERSON), 0, 0)

		oGrid.Children.Add(SetUIProfileBackgroundEdit("Camera.png"), 0, 0)

		Return oGrid
	End Function

	Private Async Sub SetBackgroundImage()
		Dim sMessage As String = String.Empty
		Try
			Dim oCrossMedia = Plugin.Media.CrossMedia.Current

			Await oCrossMedia.Initialize

			If oCrossMedia.IsPickPhotoSupported = False Then
				Await DisplayAlert("Pick Photo Not Supported", "Not Supported", "OK")
				Return
			End If

			Dim oFile = Await oCrossMedia.PickPhotoAsync(New Plugin.Media.Abstractions.PickMediaOptions With {.PhotoSize = Plugin.Media.Abstractions.PhotoSize.Full, 
				.CompressionQuality = 80})

			If oFile Is Nothing Then Return

			Dim oByteArray As Byte() = System.IO.File.ReadAllBytes(oFile.Path)
			Dim oBitmap As Android.Graphics.Bitmap = Android.Graphics.BitmapFactory.DecodeByteArray(oByteArray, 0, oByteArray.Length)

			oImage_Background.Source = oFile.Path

			Dim sBase64 As String =  Convert.ToBase64String(oByteArray.ToArray)

			Dim oStudentImages As new cStudentImages
			oStudentImages.AspNetUsersID = goStudent.AspNetUsersID
			oStudentImages.Base64String = sBase64
			Dim oWebAPI As New cWebAPI
			DIm oDataHelper As cDataHelper = Await oWebAPI.AddModify_StudentBackground(Me, gsAccessToken, oStudentImages)
			'DIm oDataHelper As cDataHelper = Await oWebAPI.AddModify_StudentBackground(Me, gsAccessToken, oStudentImages, oByteArray)
			oWebAPI = Nothing

		Catch ex As Exception
			sMessage = ex.Message
		End Try

		If sMessage.Trim.Length > 0 Then
			Await DisplayAlert("Error", sMessage, "OK")
		End If
	End Sub

 	Private Function SetUIProfileBackgroundEdit(ByVal sImage As String) As ImageButton
		Dim oImageButton = New ImageButton With {.Source = sImage, .HeightRequest = 20, .WidthRequest = 25, .Margin = 5, .BackgroundColor = Color.Transparent, .Aspect = Aspect.Fill,
			.VerticalOptions = LayoutOptions.Start, .HorizontalOptions = LayoutOptions.End}
		AddHandler oImageButton.Clicked, Sub(sender, e)
				oImageButton.IsEnabled = False
				SetBackgroundImage()
				oImageButton.IsEnabled = True
			End Sub

		Return oImageButton
	End Function

	Private Async Sub SetPersonImage()
		Dim sMessage As String = String.Empty
		Try
			Dim oCrossMedia = Plugin.Media.CrossMedia.Current

			Await oCrossMedia.Initialize

			Dim oFile = Await oCrossMedia.TakePhotoAsync( New Plugin.Media.Abstractions.StoreCameraMediaOptions With {
				.PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
				.SaveToAlbum = True,
				.CompressionQuality = 80,
				.Name = "PersonImage.jpg",
				.Directory = "UN1"}
				)

			If oFile Is Nothing Then
				Return
			End If

			Dim oByteArray As Byte() = System.IO.File.ReadAllBytes(oFile.Path)
			Dim oBitmap As Android.Graphics.Bitmap = Android.Graphics.BitmapFactory.DecodeByteArray(oByteArray, 0, oByteArray.Length)

			oImage_Person.Source = oFile.Path

			Dim sBase64 As String =  Convert.ToBase64String(oByteArray.ToArray)
			Dim oStudentImages As new cStudentImages
			oStudentImages.AspNetUsersID = goStudent.AspNetUsersID
			oStudentImages.Base64String = sBase64
			Dim oWebAPI As New cWebAPI
			DIm oDataHelper As cDataHelper = Await oWebAPI.AddModify_StudentImage(Me, gsAccessToken, oStudentImages)
			oWebAPI = Nothing

		Catch ex As Exception
			sMessage = ex.Message
		End Try

		If sMessage.Trim.Length > 0 Then
			Await DisplayAlert("Error", sMessage, "OK")
		End If
	End Sub

	Private Function SetUIProfilePersonEdit(ByVal sImage As String) As ImageButton
		Dim oImageButton = New ImageButton With {.Source = sImage, .HeightRequest = 20, .WidthRequest = 25, .Margin = 5, .BackgroundColor = Color.Transparent, .Aspect = Aspect.Fill,
			.VerticalOptions = LayoutOptions.Start, .HorizontalOptions = LayoutOptions.End}
		AddHandler oImageButton.Clicked, Async Sub(sender, e)
			oImageButton.IsEnabled = False
			SetPersonImage()
			oImageButton.IsEnabled = True
		End Sub

		Return oImageButton
	End Function

	Private Function SetUIProfileBadgeEdit(ByVal sImage As String) As ImageButton
		Dim oImageButton = New ImageButton With {.Source = sImage, .HeightRequest = 20, .WidthRequest = 25, .Margin = 5, .BackgroundColor = Color.White, .Aspect = Aspect.Fill,
			.VerticalOptions = LayoutOptions.End, .HorizontalOptions = LayoutOptions.End}
		'AddHandler oImageButton.Clicked, Async Sub(sender, e)
		'									 oImageButton.IsEnabled = False
		'									 Await DisplayAlert("Person", "Select Person Image", "OK")
		'									 oImageButton.IsEnabled = True
		'								 End Sub

		Return oImageButton
	End Function

	Private Function SetUIProfileBackgroundImage(ByVal sImage As String) As StackLayout
		oImage_Background= New Image With {.Source = sImage, .Aspect = Aspect.Fill, .HeightRequest = 200}

		Dim oLayout = New StackLayout With {.VerticalOptions = LayoutOptions.Start}
		oLayout.Children.Add(oImage_Background)
 
		Return oLayout
	End Function

	Private Function SetUIProfilePersonImage(ByVal sImage As String) As Grid
		Dim oGrid As New Grid With {.BackgroundColor = Color.Transparent}

		oGrid.RowDefinitions.Add(New RowDefinition With {.Height = New GridLength(1, GridUnitType.Star)})
		oGrid.RowDefinitions.Add(New RowDefinition With {.Height = New GridLength(1, GridUnitType.Auto)})
		oGrid.RowDefinitions.Add(New RowDefinition With {.Height = New GridLength(1, GridUnitType.Star)})
		oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Star)})
		oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Auto)})
		oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Star)})

		Dim oFrame = New Frame With {.HeightRequest = 120, .Padding = 3, .BorderColor = Color.White}

		oImage_Person = New Image With {.Source = sImage, .Aspect = Aspect.AspectFill, .WidthRequest = 120}
		oFrame.Content = oImage_Person

		oGrid.Children.Add(oFrame, 1, 1)
		oGrid.Children.Add(SetUIProfilePersonEdit("Pen.png"), 1, 1)
		oGrid.Children.Add(SetUIProfileBadgeEdit("BadgePhD.png"), 1, 1)

		Return oGrid
	End Function
End Class
