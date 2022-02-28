Imports Xamarin.Forms

Public Class cForm_Main
	Inherits ContentPage

	Private Const TITLE_PAGE As String = "Main"
	Private Const COLOUR_BACKGROUND As String = "#000000"
	Private Const IMAGE_BACKGROUND As String =  "Main1.png"

	Public Sub New()
		Initialise()
	End Sub

	Private Sub Initialise()
		Initialise_Page()
		Initialise_Content()
		AuthenticatedRedirect(Me)
	End Sub

	Private Sub Initialise_Page()
		'Me.IsVisible = False

		Title = TITLE_PAGE
		BackgroundColor = Color.FromHex(COLOUR_BACKGROUND)
		BackgroundImageSource = IMAGE_BACKGROUND
	End Sub

	Private Sub Initialise_Content()
		Dim oStackLayout As New StackLayout

		Dim oImage As New Image With {.Source = "UN1Logo.png", 
			.WidthRequest = 50, .VerticalOptions = LayoutOptions.Start, .Margin = SetUITopBottomPadding()}

		Dim oButton_SignUp = SetUIButtonSystem("Sign Up")
		oButton_SignUp.VerticalOptions = LayoutOptions.EndAndExpand
		AddHandler oButton_SignUp.Clicked, Async Sub(sender, e)
				oButton_SignUp.IsEnabled = False 
				Await Navigation.PushModalAsync(SetNavigationPage(New cForm_SignUp))
				oButton_SignUp.IsEnabled = True
			End Sub

		Dim oButton_LogIn = SetUIButtonSystem("Login")
		oButton_LogIn.VerticalOptions = LayoutOptions.End
		AddHandler oButton_LogIn.Clicked, Async Sub(sender, e)
				oButton_LogIn.IsEnabled = False
				Await Navigation.PushModalAsync(SetNavigationPage(New cForm_Login))
				oButton_LogIn.IsEnabled = True
			End Sub		

		Dim sURL As String = String.Empty
		If WEBSITE_USE_LIVE = False  Then
			sURL = String.Format("{0}", URL_DEVELOPMENT)
		Else
			sURL = String.Format("{0}", URL_LIVE)
		End If
		Dim oLabel = SetUILabel(sURL)

		oStackLayout.Children.Add(oImage)

		oStackLayout.Children.Add(oLabel)

		oStackLayout.Children.Add(oButton_SignUp)
		oStackLayout.Children.Add(oButton_LogIn)
		oStackLayout.Children.Add(SetUIVerticalSpacing(String.Empty, 10))
		Content = oStackLayout
	End Sub



End Class
