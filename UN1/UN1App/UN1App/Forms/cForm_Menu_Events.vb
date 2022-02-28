Imports Xamarin.Forms

Public Class cForm_Menu_Events
	Inherits ContentPage

	Private Const TITLE_PAGE As String = TITLE_MENU_Events
	Private Const COLOUR_BACKGROUND As String = "FFFFFF"

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
		Dim oLayout_Main As New StackLayout With {.Spacing = 1}

		Dim oLabel = SetUILabel("Events Page")
		oLabel.VerticalOptions = LayoutOptions.CenterAndExpand

		oLayout_Main.Children.Add (oLabel)

		Content = oLayout_Main	
	End Sub
End Class
