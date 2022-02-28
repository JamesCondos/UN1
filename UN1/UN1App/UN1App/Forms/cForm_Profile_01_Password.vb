Imports Xamarin.Forms

Public Class cForm_Profile_01_Password
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

		oLayout.Children.Add (SetUIToolBar("Change Password", Navigation))
		oLayout.Children.Add (SetUISpacer())
		
		Dim oOldPassword = SetUIPasswordEntry("Old Password")
		Dim oNewPassword = SetUIPasswordEntry("New Password")
		Dim oConfirmPassword = SetUIPasswordEntry("Confirm Password")

		If WEBSITE_USE_LIVE = False  Then
			oOldPassword.Text = "111111"
			oNewPassword.Text = "222222"
			oConfirmPassword.Text = "222222"
		End If

		Dim oButton_Confirm = SetUIButtonSystem("Confirm")
		AddHandler oButton_Confirm.Clicked, Async Sub(sender, e)
				oButton_Confirm.IsEnabled = False 

				Await DoChangePasswordAsync(oOldPassword, oNewPassword, oConfirmPassword)

				oButton_Confirm.IsEnabled = True
			End Sub

		oLayout.Children.Add (oOldPassword)
		oLayout.Children.Add (oNewPassword)
		oLayout.Children.Add (oConfirmPassword)
		oLayout.Children.Add (oButton_Confirm)

		Content = oLayout	
	End Sub

	Private Async Function DoChangePasswordAsync(Byval oEntry_OldPassword As Entry, Byval oEntry_NewPassword As Entry, Byval oEntry_ConfirmPassword As Entry) As Task
		Dim sOldPassword As String = oEntry_OldPassword.Text
		Dim sNewPassword As String = oEntry_NewPassword.Text
		Dim sConfirmPassword As String = oEntry_ConfirmPassword.Text

		'If ValidatePasswords(Me, sNewPassword, sConfirmPassword) = False Then
		'	Await DisplayAlert("Mismatched Passwords", "New and confirmation passwords do not match. Try again.", "OK")
		'	oEntry_NewPassword.Focus()
		'	Return
		'End If

		Dim oWebAPI As New cWebAPI
		DIm oDataHelper As cDataHelper = Await oWebAPI.ChangePassword(Me, GetAccessToken(), sOldPassword, sNewPassword, sConfirmPassword)
		oWebAPI = Nothing

		If oDataHelper.Result = False Then
			Await DisplayAlert("Alert!", oDataHelper.Message, "OK")
			Return
		End If

		SetAccessToken()
		Await DisplayAlert("Password Changed!", "Please login with your new password", "OK")

		Application.Current.MainPage = SetNavigationPage(New cForm_Main)
		Await Navigation.PopToRootAsync(True)
	End Function








End Class
