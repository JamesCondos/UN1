Imports System.Text.RegularExpressions
Imports Xamarin.Forms

Module mGeneral
	Friend Function ValidateDate_MMMMyyyyy(ByVal dDate As DateTime) As String
		Dim sMonth As String = dDate.ToString("MMMM")
		Dim sYear As String = dDate.Year.ToString

		Dim sString As String = String.Format("{0} {1}", sMonth, sYear)
		Return sString
	End Function

	Friend Function ValidateDate_MMMMyyyyy(ByVal sDate As String) As String
		Dim dDate As DateTime = CDate(sDate)

		Dim sMonth As String = dDate.ToString("MMMM")
		Dim sYear As String = dDate.Year.ToString

		Dim sString As String = String.Format("{0} {1}", sMonth, sYear)
		Return sString
	End Function

	Friend Function SetUIVerticalSpacing(ByVal sColourHex As String, Optional ByVal iThickness As Integer = 10) As Frame
		Dim oSpacer As New Frame With {.Padding = New Thickness(iThickness)}

		If sColourHex.Trim.Length > 0 Then
			oSpacer.BackgroundColor = Color.FromHex(sColourHex)
			oSpacer.BorderColor = Color.FromHex(sColourHex)
		Else
			oSpacer.Opacity = 0
		End If

		Return oSpacer
	End Function

	Friend Function SetUILeftRightMargin(Optional ByVal iThickness As Integer = 10) As Thickness
		Dim oThickness As New Thickness With {.Left = iThickness, .Right = iThickness}
		Return oThickness
	End Function

	Friend Function SetUITopBottomPadding(Optional ByVal iThickness As Integer = 5) As Thickness
		Dim oThickness As New Thickness With {.Top = iThickness, .Bottom = iThickness}
		Return oThickness
	End Function

	Friend Function SetUIButtonSystem(ByVal sText As String) As Button
		Dim oButton = New Button With {.Text = sText, .TextColor = Color.White, .Margin = SetUILeftRightMargin(),
			.FontSize = FONTSIZE_NORMAL,
			.Padding = New Thickness(0),
			.BackgroundColor = Color.Black, .BorderWidth = 1, .BorderColor = Color.Black}

		Return oButton
	End Function

	Friend Function SetUILabel(ByVal sText As String, ByVal oTextAlignment As TextAlignment, Optional ByVal sColourHex As String = "#000000") As Label
		Dim oLabel = New Label With {.Text = sText,
			.FontSize = FONTSIZE_NORMAL,
			.HorizontalTextAlignment = oTextAlignment, .TextColor = Color.FromHex(sColourHex)}

		Return oLabel
	End Function

	Friend Function SetUILabel(ByVal sText As String, Optional ByVal sColourHex As String = "#000000") As Label
		Dim oLabel = New Label With {.Text = sText,
			.FontSize = FONTSIZE_NORMAL,
			.HorizontalTextAlignment = TextAlignment.Center, .TextColor = Color.FromHex(sColourHex)}

		Return oLabel
	End Function

	Friend Function SetUISwitch(Byval sID As String, ByVal bToggled As Boolean) As Switch
		Dim oSwitch = New Switch With {.IsToggled = bToggled, .StyleId = sID}
		Return oSwitch
	End Function

	Friend Function SetUiSwitch(Byval oSwitch As Switch, ByVal sText As String) As Grid
		Dim oGrid As New Grid With {.BackgroundColor = Color.FromHex("FFFFFF"), .HorizontalOptions = LayoutOptions.StartAndExpand,
			.Padding = New Thickness(0, 10, 0, 10)}

		oGrid.RowDefinitions.Add(New RowDefinition With {.Height = New GridLength(1, GridUnitType.Star)})
		oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Star)})
		oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Auto)})

		oGrid.Children.Add(New Label With {.Text = sText, .FontAttributes = FontAttributes.Bold, .FontSize = 14, .VerticalOptions = LayoutOptions.Center,
			.TextColor = Color.Black, 
			.Margin = New Thickness(20, 0, 0, 0)}, 0, 0)

		oGrid.Children.Add(oSwitch)

		Return oGrid
	End Function

	'Friend Function SetUITextEditor(Byval sPlaceholder As String,Optional Byval sPlaceholderColourHex As String = "#EEEEFF", Optional Byval sTextColourHex As String = "#000000") As Editor
	'	Dim oEditor = New Editor With {.Text = String.Empty, .Placeholder = sPlaceholder, .PlaceholderColor = Color.FromHex(sPlaceholder), 
	'		.Fontsize = FONTSIZE_NORMAL,
	'		.TextColor = Color.FromHex(sTextColourHex), .Margin = SetUILeftRightMargin()}

	'	Return oEditor
	'End Function

	Friend Function SetUITextEntry(ByVal sPlaceholder As String, Optional ByVal sPlaceholderColourHex As String = "#EEEEFF", Optional ByVal sTextColourHex As String = "#000000") As Entry
		Dim oEntry = New Entry With {.Text = String.Empty, .Placeholder = sPlaceholder, .PlaceholderColor = Color.FromHex(sPlaceholder),
			.FontSize = FONTSIZE_NORMAL,
			.TextColor = Color.FromHex(sTextColourHex), .Margin = SetUILeftRightMargin()}

		Return oEntry
	End Function

	Friend Function SetUIPasswordEntry(ByVal sPlaceholder As String, Optional ByVal sPlaceholderColourHex As String = "#EEEEFF", Optional ByVal sTextColourHex As String = "#000000") As Entry
		Dim oEntry = New Entry With {.Text = String.Empty, .Placeholder = sPlaceholder, .PlaceholderColor = Color.FromHex(sPlaceholder),
			.FontSize = FONTSIZE_NORMAL,
			.TextColor = Color.FromHex(sTextColourHex), .Margin = SetUILeftRightMargin(), .IsPassword = True}

		Return oEntry
	End Function

	Friend Function SetUITopLogoGrid() As Grid
		Dim oGrid = New Grid With {.VerticalOptions = LayoutOptions.Start}
		oGrid.RowDefinitions.Add(New RowDefinition With {.Height = New GridLength(50)})
		oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Star)})
		Dim oImage As New Image With {.Source = "UN1LogoTopBottomPad.png", .BackgroundColor = Color.Black, .Opacity = 0.6}
		oGrid.Children.Add(oImage, 0, 0)

		Return oGrid
	End Function

	Friend Function GetIcon(ByVal sIcon As String, ByVal sText As String, ByVal sTextColorHex As String) As StackLayout
		Dim oStackLayout As New StackLayout
		oStackLayout.Spacing = 0
		oStackLayout.Margin = New Thickness(0, 1, 0, 0)
		oStackLayout.VerticalOptions = LayoutOptions.Center

		Dim oImage As New Image With {.Source = sIcon}
		oImage.Scale = 0.9

		Dim oLabel As New Label
		oLabel.Text = sText
		oLabel.FontSize = 10
		oLabel.FontAttributes = FontAttributes.Bold
		oLabel.TextColor = Color.FromHex(sTextColorHex)
		oLabel.HorizontalTextAlignment = TextAlignment.Center

		oStackLayout.Children.Add(oImage)
		oStackLayout.Children.Add(oLabel)

		Return oStackLayout
	End Function

	Friend Function SetIconClubs(ByVal sIcon As String, ByVal sText As String) As StackLayout
		Dim oStackLayout As New StackLayout
		oStackLayout.Spacing = 0
		oStackLayout.VerticalOptions = LayoutOptions.CenterAndExpand
		oStackLayout.Scale = 0.3

		Dim oImage As New Image With {.Source = sIcon}
		oImage.Margin = New Thickness(0, 0, 0, 30)

		Dim oLabel As New Label
		oLabel.Text = sText
		oLabel.FontSize = 12
		oLabel.FontAttributes = FontAttributes.None
		oLabel.TextColor = Color.White
		oLabel.HorizontalTextAlignment = TextAlignment.Center
		oLabel.Scale = 4.0

		oStackLayout.Children.Add(oImage)
		oStackLayout.Children.Add(oLabel)

		Return oStackLayout
	End Function

	Friend Function GetVersion() As String
		Dim sVersion As String = GetType(cApp).Assembly.GetName.Version.ToString
		Return sVersion
	End Function

	Friend Function GetBuild() As String
		Dim sBuild As String = GetType(cApp).Assembly.GetName.Version.Build.ToString
		Return sBuild
	End Function

	Friend Function SetUISpacer() As BoxView
		Dim oBox As New BoxView With {.Color = Color.Transparent,
			.VerticalOptions = LayoutOptions.End,
			.HorizontalOptions = LayoutOptions.FillAndExpand,
			.HeightRequest = 50
		}
		Return oBox
	End Function

	Friend Function SetUIToolBar(ByVal sToolbarTitle As String, ByVal oNavigation As INavigation) As Grid
		Dim oGrid As New Grid With {.BackgroundColor = Color.FromHex("FFFFFF"), .Padding = New Thickness(0, 10, 0, 10)}

		oGrid.RowDefinitions.Add(New RowDefinition With {.Height = New GridLength(1, GridUnitType.Star)})
		oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Auto)})
		oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Auto)})
		oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Star)})

		Dim oBackButton = New Button With {.Text = "< Back", .Margin = 0, .Padding = 0, .FontAttributes = FontAttributes.Bold, .FontSize = 18, .TextColor = Color.Black, .BackgroundColor = Color.Transparent}
		AddHandler oBackButton.Clicked, Async Sub(sender, e)
											oBackButton.IsEnabled = False
											Await oNavigation.PopModalAsync()
											oBackButton.IsEnabled = True
										End Sub

		oGrid.Children.Add(oBackButton, 1, 0)

		oGrid.Children.Add(New Label With {.Text = sToolbarTitle, .FontAttributes = FontAttributes.Bold, .FontSize = 18, .TextColor = Color.Black, .VerticalOptions = LayoutOptions.Center,
			.HorizontalOptions = LayoutOptions.End, .Margin = New Thickness(0, 0, 20, 0)}, 2, 0)

		Return oGrid
	End Function

	Friend Function SetUiSettingsNavigation(ByVal sText As String, ByVal oNavigation As INavigation, ByVal oContentPage As ContentPage) As Grid
		Dim oGrid As New Grid With {.BackgroundColor = Color.FromHex("FFFFFF"), .HorizontalOptions = LayoutOptions.StartAndExpand,
			.Padding = New Thickness(0, 15, 0, 15)}

		oGrid.RowDefinitions.Add(New RowDefinition With {.Height = New GridLength(1, GridUnitType.Star)})
		oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Star)})
		oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Auto)})

		oGrid.Children.Add(New Label With {.Text = sText, .FontAttributes = FontAttributes.Bold, .FontSize = 18, .VerticalOptions = LayoutOptions.Center,
			.TextColor = Color.Black,
			.Margin = New Thickness(20, 0, 0, 0)}, 0, 0)

		Dim oNextButton = New ImageButton With {.Source = "Next.png", .HeightRequest = 10, .BackgroundColor = Color.Transparent, .WidthRequest = 30, .Padding = 5,
			.Margin = New Thickness(0, 0, 20, 0)}
		AddHandler oNextButton.Clicked, Async Sub(sender, e)
											oNextButton.IsEnabled = False
											Await oNavigation.PushModalAsync(SetNavigationPage(oContentPage))
											oNextButton.IsEnabled = True
										End Sub

		oGrid.Children.Add(oNextButton, 1, 0)

		Return oGrid
	End Function

	Friend Function SetNavigationPage(ByVal oContentPage As ContentPage) As NavigationPage
		Dim oNavPage = New NavigationPage(oContentPage)
		NavigationPage.SetHasNavigationBar(oNavPage.CurrentPage, False)
		Return oNavPage
	End Function

	Friend Function SetNavigationPage(ByVal oTabbedPage As TabbedPage) As NavigationPage
		Dim oNavPage = New NavigationPage(oTabbedPage)
		NavigationPage.SetHasNavigationBar(oNavPage.CurrentPage, False)
		Return oNavPage
	End Function

	Friend Function SetUIImageAndSearchBar(ByVal sImage As String, ByVal oNavigation As INavigation) As Grid

		If sImage.Trim.Length = 0 Then
			sImage = IMAGE_PERSON
		Else
			Dim sURL As String = String.Empty
			If WEBSITE_USE_LIVE = True Then
				sURL = String.Format("{0}{1}/{2}/{3}", URL_LIVE, FOLDER_APP_IMAGES, goStudent.AspNetUsersID, sImage)
			Else
				sURL = String.Format("{0}{1}/{2}/{3}", URL_DEVELOPMENT, FOLDER_APP_IMAGES, goStudent.AspNetUsersID, sImage)
			End If
			sImage = sURL
		End If

		Dim oImage_Profile = CreateProfileImage(sImage)
		Dim oSearch = New SearchBar With {.Placeholder = "Search...", .Margin = 0, .BackgroundColor = Color.White}
		Dim oImage_Cog = CreateSettingsButton("Cog.png", oNavigation)

		Dim oGrid As New Grid With {.BackgroundColor = Color.White, .HorizontalOptions = LayoutOptions.Start,'.MinimumHeightRequest = 60,
			.Margin = New Thickness(0, 0, 0, 0)}

		oGrid.RowDefinitions.Add(New RowDefinition With {.Height = New GridLength(1, GridUnitType.Auto)})
		oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Auto)})
		oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Star)})
		oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Auto)})

		oGrid.Children.Add(oImage_Profile, 0, 0)
		oGrid.Children.Add(oSearch, 1, 0)
		oGrid.Children.Add(oImage_Cog, 2, 0)

		Return oGrid
	End Function

	Private Function CreateSettingsButton(ByVal sImage As String, ByVal oNavigation As INavigation) As ImageButton
		Dim oButton = New ImageButton With {.Source = sImage,
			.BackgroundColor = Color.Transparent, .Aspect = Aspect.Fill,
			.HeightRequest = 50, .WidthRequest = 60
			}
		AddHandler oButton.Clicked, Async Sub(sender, e)
										oButton.IsEnabled = False
										Await oNavigation.PushModalAsync(SetNavigationPage(New cForm_Profile_00_Settings))
										oButton.IsEnabled = True
									End Sub

		Return oButton
	End Function

	Private Function CreateProfileImage(ByVal sImage As String) As Image
		Dim oImage = New Image With {.Source = sImage, .Aspect = Aspect.AspectFill,
			.HeightRequest = 50, .WidthRequest = 50, .Margin = New Thickness(5, 5, 0, 0)}
		Return oImage
	End Function

	Friend Function SetUIFriendBlock(ByVal sImageBackground As String, ByVal sImagePerson As String, ByVal sFullName As String, ByVal sUniversityLogo As String, ByVal sUniverisity As String) As Grid
		Dim oGrid As New Grid With {.BackgroundColor = Color.FromHex("FFFFFF"), .VerticalOptions = LayoutOptions.Start, .WidthRequest = 110,
			.Margin = New Thickness(5, 0, 5, 10)}

		oGrid.RowDefinitions.Add(New RowDefinition With {.Height = New GridLength(1, GridUnitType.Auto)})

		oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Star)})

		oGrid.Children.Add(SetUIProfileBackgroundImage(sImageBackground), 0, 0)
		oGrid.Children.Add(SetUIProfilePersonImage(sImagePerson), 0, 0)

		Dim oLabel_FullName = New Label With {.Text = sFullName, .VerticalOptions = LayoutOptions.Center, .Margin = New Thickness(0, 50, 0, 0),
			.TextColor = Color.White,
			.HorizontalTextAlignment = TextAlignment.Center}

		Dim oImage_University = SetUIProfileUniversityImage("UniversityMelbourne.png")

		Dim oLabel_University = New Label With {.Text = sUniverisity, .VerticalOptions = LayoutOptions.Center,
			.TextColor = Color.White,
			.HorizontalTextAlignment = TextAlignment.Center}


		Dim oLayout_Details = New StackLayout With {.Margin = 1, .VerticalOptions = LayoutOptions.Center}
		oLayout_Details.Children.Add(oLabel_FullName)
		oLayout_Details.Children.Add(oImage_University)
		oLayout_Details.Children.Add(oLabel_University)


		oGrid.Children.Add(oLayout_Details, 0, 0)


		Return oGrid
	End Function

	Private Function SetUIProfileBackgroundImage(ByVal sImage As String) As StackLayout
		Dim oImage_Background = New Image With {.Source = sImage, .Aspect = Aspect.AspectFill, .HeightRequest = 200}

		Dim oLayout = New StackLayout With {.VerticalOptions = LayoutOptions.Start}
		oLayout.Children.Add(oImage_Background)

		Return oLayout
	End Function

	Private Function SetUIProfilePersonImage(ByVal sImage As String) As Grid
		Dim oGrid As New Grid With {.BackgroundColor = Color.Transparent, .VerticalOptions = LayoutOptions.Start, .Margin = New Thickness(0, 10, 0, 10)}

		oGrid.RowDefinitions.Add(New RowDefinition With {.Height = New GridLength(1, GridUnitType.Auto)})

		oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Star)})
		oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Auto)})
		oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Star)})

		Dim oFrame = New Frame With {.HeightRequest = 50, .Padding = 3, .BorderColor = Color.White}

		Dim oImage_Person = New Image With {.Source = sImage, .Aspect = Aspect.AspectFit}
		oFrame.Content = oImage_Person

		oGrid.Children.Add(oFrame, 1, 0)

		Return oGrid
	End Function

	Private Function SetUIProfileUniversityImage(ByVal sImage As String) As Grid
		Dim oGrid As New Grid With {.BackgroundColor = Color.Transparent, .VerticalOptions = LayoutOptions.Start, .Margin = New Thickness(0, 0, 0, 0)}

		oGrid.RowDefinitions.Add(New RowDefinition With {.Height = New GridLength(1, GridUnitType.Auto)})

		oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Star)})
		oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Auto)})
		oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Star)})

		Dim oFrame = New Frame With {.HeightRequest = 30, .Padding = 0, .BorderColor = Color.White, .Margin = 0, .CornerRadius = 0}

		Dim oImage = New Image With {.Source = sImage, .Aspect = Aspect.AspectFill}
		oFrame.Content = oImage

		oGrid.Children.Add(oFrame, 1, 0)

		Return oGrid
	End Function

	Friend Function SetUiLabelEntryPair(ByRef oLayout As StackLayout, ByVal sLabel As String, ByVal sPlaceholder As String) As Entry
		Dim oGrid As New Grid With {.BackgroundColor = Color.FromHex("FFFFFF"), .HorizontalOptions = LayoutOptions.StartAndExpand,
			.Padding = New Thickness(0, 5, 0, 5)}

		oGrid.RowDefinitions.Add(New RowDefinition With {.Height = New GridLength(1, GridUnitType.Star)})
		'oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(100)})
		oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Star)})
		oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Star)})

		oGrid.Children.Add(New Label With {.Text = sLabel, .FontAttributes = FontAttributes.Bold, .FontSize = 18, .VerticalOptions = LayoutOptions.Center,
			.TextColor = Color.Black,
			.Margin = New Thickness(20, 0, 0, 0)}, 0, 0)

		Dim oEntry = SetUITextEntry(sPlaceholder)
		oGrid.Children.Add(oEntry, 1, 0)

		oLayout.Children.Add(oGrid)

		oGrid = Nothing

		Return oEntry
	End Function

	Friend Function ValidateEmail(Byval oPage As Page, ByVal sEmail As String) As Boolean
		Dim sEmailPattern As String = "^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$"

		If Regex.IsMatch(sEmail, sEmailPattern) = True Then
			Return True
		Else
			oPage.DisplayAlert("Email", "Invalid Email Format.", "OK")
			Return False
		End If
	End Function

	Friend Function VString(Byval sString As String) As String
		'Validate String to ensure its not nothing
		If sString Is Nothing Then
			Return String.Empty
		End If

		Return sString.Trim
	End Function

	Friend Function ValidateStringEmpty(Byval oPage As Page, Byval sStringName As String, Byval sString As String) As Boolean
		If sString.Trim.Length = 0 Then 
			oPage.DisplayAlert(sStringName, String.Format("{0} Cannot Be Blank.", sStringName), "OK")
			Return False 
		End If

		Return True
	End Function

	Friend Function ValidatePasswords(Byval oPage As Page, Byval sPassword As String, Byval sConfirmPassword As String) As Boolean
		If sPassword.Trim.Length = 0 Then 
			oPage.DisplayAlert("Password", "Invalid Password.", "OK")
			Return False 
		End If

		If sConfirmPassword.Trim.Length = 0 Then 
			oPage.DisplayAlert("Confirmation Password", "Invalid Confirmation Password.", "OK")
			Return False 
		End If

		If String.Compare(sPassword, sConfirmPassword, False) <> 0 Then
			oPage.DisplayAlert("Passwords", "Passwords Do Not Match.", "OK")
			Return False
		End If
		
		Return True
	End Function

	Friend Function ValidatePassword(Byval oPage As Page, Byval sPassword As String) As Boolean
		If sPassword.Trim.Length = 0 Then 
			oPage.DisplayAlert("Password", "Invalid Password.", "OK")
			Return False 
		End If

		Return True
	End Function

	Friend Async Sub AuthenticatedRedirect(Byval oPage As Page)
		Dim sAccessToken As String = GetAccessToken()

		Dim oWebAPI As New cWebAPI
		DIm bResult As Boolean = oWebAPI.IsAuthorized(oPage, sAccessToken)
		oWebAPI = Nothing

		If bResult = True Then
			Application.Current.MainPage = SetNavigationPage(New cForm_Menu_Main)
			Await oPage.Navigation.PopToRootAsync(True)
		Else
			oPage.IsVisible = True
		End If

	End Sub

	Friend Async Sub LogOut(Byval oPage As Page)
		SetAccessToken()
		Application.Current.MainPage = SetNavigationPage(New cForm_Main)
		Await oPage.Navigation.PopToRootAsync(True)
	End Sub

	Friend Function GetAccessToken() As String
		Return UN1.Helpers.cSettings.AppSettings.GetValueOrDefault(API_TAG_ACCESSTOKEN, String.Empty)
	End Function

	Friend Sub SetAccessToken(Optional Byval sAccessToken As String = "")
		UN1.Helpers.cSettings.AppSettings.AddOrUpdateValue(API_TAG_ACCESSTOKEN, sAccessToken)
	End Sub










	'Friend Function SetUiLabelEditorPair(Byref oLayout As StackLayout, Byval sLabel As String, Byval sPlaceholder As String) As Editor
	'	Dim oGrid As New Grid With {.BackgroundColor = Color.FromHex("FFFFFF"), .HorizontalOptions = LayoutOptions.StartAndExpand,
	'		.Padding = New Thickness(0,10,0,10)}

	'	oGrid.RowDefinitions.Add(New RowDefinition With {.Height = New GridLength(1, GridUnitType.Star)})
	'		oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(130)})
	'		oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Star)})

	'	oGrid.Children.Add (New Label With {.Text =sLabel, .FontAttributes = FontAttributes.Bold, .FontSize = 18, .VerticalOptions = LayoutOptions.Center, 
	'		.TextColor = Color.Black,
	'		.Margin = New Thickness(20,0,0,0)}, 0, 0)

	'	Dim oEditor = SetUITextEditor(sPlaceholder)
	'	oGrid.Children.Add (oEditor, 1, 0)

	'	oLayout.Children.Add (oGrid)

	'	oGrid = Nothing

	'	Return oEditor
	'End Function

End Module
