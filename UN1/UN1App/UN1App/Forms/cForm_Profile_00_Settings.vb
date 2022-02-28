Imports Xamarin.Forms

Public Class cForm_Profile_00_Settings
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
		Dim oLayout As New StackLayout With {.Spacing = 1}

		oLayout.Children.Add (SetUIToolBar("Settings", Navigation))
		
		oLayout.Children.Add (SetUISpacer())

		oLayout.Children.Add (SetUISettingsNavigation("Change Password", Navigation, New cForm_Profile_01_Password))
		oLayout.Children.Add (SetUiSettingsNavigation("Notifications", Navigation, New cForm_Profile_02_Notifications))

		Dim sVersion As String = GetVersion()

		oLayout.Children.Add (CreateUiSettingsAppVersion("App Version", sVersion))
		oLayout.Children.Add (CreateUiSettingsDeleteAccount("Delete Account"))
		
		oLayout.Children.Add (SetUISpacer())
		oLayout.Children.Add (CreateUiSettingsLogout("Log Out"))

		Content = oLayout	
	End Sub

	Private Function CreateUiSettingsDeleteAccount(Byval sText As String) As Grid
		Dim oGrid As New Grid With {.BackgroundColor = Color.FromHex("FFFFFF"), .HorizontalOptions = LayoutOptions.StartAndExpand,
			.Padding = New Thickness(0,10,0,10)}

		oGrid.RowDefinitions.Add(New RowDefinition With {.Height = New GridLength(1, GridUnitType.Star)})
			oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Star)})
			oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Auto)})

		oGrid.Children.Add (New Label With {.Text =sText, .FontAttributes = FontAttributes.Bold, .FontSize = 18, .VerticalOptions = LayoutOptions.Center, 
			.TextColor = Color.Black,
			.Margin = New Thickness(20,0,0,0)}, 0, 0)

		Dim oNextButton = New ImageButton With {.Source = "Next.png", .HeightRequest = 10, .BackgroundColor = Color.Transparent, .WidthRequest = 30, .Padding = 5,
			.Margin = New Thickness(0, 0, 20, 0)}
		AddHandler oNextButton.Clicked, Async Sub(sender, e)
				oNextButton.IsEnabled = False 

				Dim bResult As Boolean= Await DisplayAlert("Delete Account", "Are you sure you want to delete your account?", "Yes", "No")
				Select Case bResult
					Case True
						Await DisplayAlert ("Account Deleted", "Your account have been permanently deleted", "Ok")
						Application.Current.MainPage = SetNavigationPage(New cForm_Main)
						Await Navigation.PopToRootAsync(True)
					Case False
						'Do nothing
				End Select

				oNextButton.IsEnabled = True
			End Sub

		oGrid.Children.Add(oNextButton, 1, 0)

		Return oGrid
	End Function

	Private Function CreateUiSettingsAppVersion(Byval sText As String, Byval sValue As String) As Grid
		Dim oGrid As New Grid With {.BackgroundColor = Color.FromHex("FFFFFF"), .HorizontalOptions = LayoutOptions.StartAndExpand,
			.Padding = New Thickness(0,10,0,10)}

		oGrid.RowDefinitions.Add(New RowDefinition With {.Height = New GridLength(1, GridUnitType.Star)})
			oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Star)})
			oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Auto)})

		oGrid.Children.Add (New Label With {.Text =sText, .FontAttributes = FontAttributes.Bold, .FontSize = 18, .VerticalOptions = LayoutOptions.Center, 
			.TextColor = Color.Black,
			.Margin = New Thickness(20,0,0,0)}, 0, 0)

		oGrid.Children.Add (New Label With {.Text =sValue, .FontAttributes = FontAttributes.Bold, .FontSize = 18, .HorizontalOptions = LayoutOptions.End,
			.TextColor = Color.Gray,
			.Margin = New Thickness(0,0,20,0)}, 1, 0)

		Return oGrid
	End Function

	Private Function CreateUiSettingsLogout(Byval sText As String ) As Grid
		Dim oGrid As New Grid With {.BackgroundColor = Color.FromHex("FFFFFF"), .HorizontalOptions = LayoutOptions.StartAndExpand,
			.Padding = New Thickness(0,10,0,10)}

		oGrid.RowDefinitions.Add(New RowDefinition With {.Height = New GridLength(1, GridUnitType.Star)})
			oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Star)})
			oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Auto)})

		oGrid.Children.Add (New Label With {.Text =sText, .FontAttributes = FontAttributes.Bold, .FontSize = 18, .VerticalOptions = LayoutOptions.Center, 
			.TextColor = Color.Red,
			.Margin = New Thickness(20,0,0,0)}, 0, 0)

		Dim oLogOutButton = New ImageButton With {.Source = "Logout.png", .HeightRequest = 10, .BackgroundColor = Color.Transparent, .WidthRequest = 30, .Padding = 5,
			.Margin = New Thickness(0, 0, 20, 0)}
		AddHandler oLogOutButton.Clicked, Async Sub(sender, e)
				oLogOutButton.IsEnabled = False 

				Dim bResult As Boolean= Await DisplayAlert("Log Out", "Are you sure you want to Log Out?", "Yes", "No")
				Select Case bResult
					Case True
						LogOut(Me)
						'Application.Current.MainPage = SetNavigationPage(New cForm_Main)
						'Await Navigation.PopToRootAsync(True)
					Case False
						'Do nothing
				End Select

				oLogOutButton.IsEnabled = True
			End Sub


		oGrid.Children.Add(oLogOutButton, 1, 0)

		Return oGrid
	End Function

End Class
