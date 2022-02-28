Imports Xamarin.Forms

Public Class cForm_Login
	Inherits ContentPage

	Private Const TITLE_PAGE As String = "Login"
	Private Const COLOUR_BACKGROUND As String = "#000000"
	Private Const IMAGE_BACKGROUND As String =  "Login2.png"

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
		Dim oStackLayout As New StackLayout

		Dim oTopLogoGrid As Grid = SetUITopLogoGrid()

		'====================================

		Dim oFrame_StackLayout_SignUp = CreateUIFrame_Login()
		Dim oFrame_SignUp = New Frame With {.Margin = New Thickness (10), .VerticalOptions = LayoutOptions.CenterAndExpand}
		oFrame_SignUp.Content = oFrame_StackLayout_SignUp

		'====================================

		Dim oFrame_TermsAndConditions = New Frame With { .VerticalOptions = LayoutOptions.End, .BackgroundColor = Color.Black, .Opacity = 0.6,
			.HorizontalOptions = LayoutOptions.FillAndExpand,
			.Padding = SetUITopBottomPadding()}
		oFrame_TermsAndConditions.Content = CreateUIFrame_TermsAndConditions()

		'====================================

		oStackLayout.Children.Add(oTopLogoGrid)
		oStackLayout.Children.Add(oFrame_SignUp)
		oStackLayout.Children.Add(oFrame_TermsAndConditions)

		Content = oStackLayout
	End Sub

	Private Function CreateUIFrame_Login() As StackLayout
		Dim oFrame_StackLayout_SignUp = New StackLayout With {.Spacing = 0}

		Dim oLabel_Login = SetUILabel("Login")
		Dim oEntry_UniversityEmail = SetUITextEntry("University Email")
		Dim oEntry_Password = SetUIPasswordEntry("Your Password")

		'REMOVE THIS IN LIVE!!
		If LOGIN_USEMINE = True  Then
			oEntry_UniversityEmail.Text = "chowmein@cheeken.com"
			oEntry_Password.Text = "111111"
		End If

		Dim oButton_Login = SetUIButtonSystem("Login")
		AddHandler oButton_Login.Clicked, Async Sub(sender, e)
				oButton_Login.IsEnabled = False 
				Await DoLoginAsync(oEntry_UniversityEmail, oEntry_Password)
				oButton_Login.IsEnabled = True
			End Sub

		oFrame_StackLayout_SignUp.Children.Add(oLabel_Login)
		oFrame_StackLayout_SignUp.Children.Add(oEntry_UniversityEmail)
		oFrame_StackLayout_SignUp.Children.Add(oEntry_Password)
		oFrame_StackLayout_SignUp.Children.Add(oButton_Login)

		Return oFrame_StackLayout_SignUp
	End Function

	Private Async Function DoLoginAsync(Byval oEntry_UniversityEmail As Entry, Byval oEntry_Password As Entry) As Task
		Dim sMessage As String = String.Empty
		Try
			Dim sUniversityEmail As String = oEntry_UniversityEmail.Text.Trim
			Dim sPassword As String = oEntry_Password.Text

			If ValidateEmail(Me, sUniversityEmail) = False Then
				oEntry_UniversityEmail.Focus()
				Return
			End If

			If ValidatePassword(Me, sPassword) = False Then
				oEntry_Password.Focus()
				Return
			End If

			Dim oWebAPI As New cWebAPI
			DIm sResultAsync = Await oWebAPI.LoginUserAsync(Me, sUniversityEmail, sPassword)
			oWebAPI = Nothing

			If sResultAsync.Trim.Length > 0 Then
				Await DisplayAlert("Result", sResultAsync, "OK")
				Return
			End If

			Application.Current.MainPage = SetNavigationPage(New cForm_Menu_Main)
			Await Navigation.PopToRootAsync(True)
		Catch ex As Exception
			sMessage = ex.Message
		End Try

		If sMessage.Trim.Length > 0 Then
			Await DisplayAlert("Error", sMessage, "OK")
		End If
	End Function

	Private Function CreateUIFrame_TermsAndConditions() As StackLayout
		Dim oFrame_StackLayout_TermsAndConditions = New StackLayout With {.Spacing = 0}

		Dim oLabel_LoggingAgree = New Label With {.Text = "By logging in you agree to the",
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
                DisplayAlert ("T&C", "Display Terms and Conditions","OK")
            End Sub)
        oLabel_TermsAndConditions.GestureRecognizers.Add (oTap)

		oFrame_StackLayout_TermsAndConditions.Children.Add(oLabel_LoggingAgree)
		oFrame_StackLayout_TermsAndConditions.Children.Add(oLabel_TermsAndConditions)

		Return oFrame_StackLayout_TermsAndConditions
	End Function
End Class
