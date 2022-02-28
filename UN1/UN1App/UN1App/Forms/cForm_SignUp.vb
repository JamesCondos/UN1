Imports System.Text.RegularExpressions
Imports Xamarin.Forms

Public Class cForm_SignUp
	Inherits ContentPage

	Private Const TITLE_PAGE As String = TITLE_NAV_SIGNUP
	Private Const COLOUR_BACKGROUND As String = "#000000"
	Private Const IMAGE_BACKGROUND As String = "SignUp480x800.png"

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
		BackgroundImageSource = IMAGE_BACKGROUND
	End Sub

	Private Sub Initialise_Content()
		Dim oTopLogoGrid As Grid = SetUITopLogoGrid()

		Dim oFrame_StackLayout_SignUp = CreateUIFrame_SignUp()
		Dim oFrame_SignUp = New Frame With {.Margin = New Thickness(10), .VerticalOptions = LayoutOptions.CenterAndExpand}
		oFrame_SignUp.Content = oFrame_StackLayout_SignUp

		Dim oFrame_TermsAndConditions = New Frame With {.VerticalOptions = LayoutOptions.End, .BackgroundColor = Color.Black, .Opacity = 0.6,
			.HorizontalOptions = LayoutOptions.FillAndExpand,
			.Padding = SetUITopBottomPadding()}
		oFrame_TermsAndConditions.Content = CreateUIFrame_TermsAndConditions()

		'====================================

		Dim oStackLayout As New StackLayout
		oStackLayout.Children.Add(oTopLogoGrid)
		oStackLayout.Children.Add(oFrame_SignUp)
		oStackLayout.Children.Add(oFrame_TermsAndConditions)

		Content = oStackLayout
	End Sub

	Private Function CreateUIFrame_SignUp() As StackLayout
		Dim oFrame_StackLayout_SignUp = New StackLayout With {.Spacing = 0}

		Dim oLabel_CreateYourAccount = SetUILabel("Create Your Account")
		Dim oEntry_FullName = SetUITextEntry("Full Name")
		Dim oEntry_UniversityEmail = SetUITextEntry("University Email")
		Dim oEntry_Password = SetUIPasswordEntry("Your Password")
		Dim oEntry_ConfirmPassword = SetUIPasswordEntry("Confirm Password")

		If WEBSITE_USE_LIVE = False  Then
			oEntry_FullName.Text = "Fatty Man"
			oEntry_UniversityEmail.Text = "chowmein@cheeken.com"
			oEntry_Password.Text = "111111"
			oEntry_ConfirmPassword.Text = oEntry_Password.Text
		End If

		Dim oButton_SignUp = SetUIButtonSystem("Sign Up")
		AddHandler oButton_SignUp.Clicked, Async Sub(sender, e)
				oButton_SignUp.IsEnabled = False

				Await DoSignUpAsync(oEntry_FullName, oEntry_UniversityEmail, oEntry_Password, oEntry_ConfirmPassword)

				oButton_SignUp.IsEnabled = True
			End Sub

		oFrame_StackLayout_SignUp.Children.Add(oLabel_CreateYourAccount)
		oFrame_StackLayout_SignUp.Children.Add(oEntry_FullName)
		oFrame_StackLayout_SignUp.Children.Add(oEntry_UniversityEmail)
		oFrame_StackLayout_SignUp.Children.Add(oEntry_Password)
		oFrame_StackLayout_SignUp.Children.Add(oEntry_ConfirmPassword)
		oFrame_StackLayout_SignUp.Children.Add(oButton_SignUp)

		Return oFrame_StackLayout_SignUp
	End Function
	
	Private Async Function DoSignUpAsync(Byval oEntry_FullName As Entry, Byval oEntry_UniversityEmail As Entry,
		Byval oEntry_Password As Entry, Byval oEntry_ConfirmPassword As Entry) As Task

		Dim sFullName As String = oEntry_FullName.Text.Trim
		Dim sUniversityEmail As String = oEntry_UniversityEmail.Text.Trim
		Dim sPassword As String = oEntry_Password.Text
		Dim sConfirmPassword As String = oEntry_ConfirmPassword.Text

		If ValidateFullName(sFullName) = False Then
			oEntry_FullName.Focus
			Return
		End If

		If ValidateEmail(Me, sUniversityEmail) = False Then
			oEntry_UniversityEmail.Focus()
			Return
		End If

		If ValidatePasswords(Me, sPassword, sConfirmPassword) = False Then
			oEntry_Password.Focus()
			Return
		End If


		Dim oWebAPI As New cWebAPI
		DIm sResultAsync = Await oWebAPI.RegisterUserAsync(sFullName, sUniversityEmail, sPassword, sConfirmPassword)
		oWebAPI = Nothing

		If sResultAsync.Trim.Length > 0 Then
			Await DisplayAlert("Result", sResultAsync, "OK")
			Return
		End If

		Application.Current.MainPage = SetNavigationPage(New cForm_Menu_Main)
		Await Navigation.PopToRootAsync(True)

	End Function

	Private Function ValidateFullName(Byval sFullName As String) As Boolean
		If sFullName.Trim.Length = 0 Then
			DisplayAlert("Full Name", "Invalid Full Name.", "OK")
			Return False
		Else
			Return True
		End If
	End Function

	Private Function CreateUIFrame_TermsAndConditions() As StackLayout
		Dim oFrame_StackLayout_TermsAndConditions = New StackLayout With {.Spacing = 0}

		Dim oLabel_SigningAgree = New Label With {.Text = "By signing up you agree to the",
			.TextColor = Color.FromHex("#FFFFFF"),
			.FontSize = FONTSIZE_NORMAL,
			.HorizontalTextAlignment = TextAlignment.Center
			}

		Dim oLabel_TermsAndConditions = New Label With {.Text = "terms and conditions",
			.TextColor = Color.FromHex("#FF0000"),
			.TextDecorations = TextDecorations.Underline,
			.FontSize = FONTSIZE_NORMAL,
			.HorizontalTextAlignment = TextAlignment.Center
			}
		Dim oTap As New TapGestureRecognizer
		oTap.CommandParameter = Nothing
		oTap.Command = New Command(Sub()
									   DisplayAlert("T&C", "Display Terms and Conditions", "OK")
								   End Sub)
		oLabel_TermsAndConditions.GestureRecognizers.Add(oTap)

		oFrame_StackLayout_TermsAndConditions.Children.Add(oLabel_SigningAgree)
		oFrame_StackLayout_TermsAndConditions.Children.Add(oLabel_TermsAndConditions)

		Return oFrame_StackLayout_TermsAndConditions
	End Function
End Class
