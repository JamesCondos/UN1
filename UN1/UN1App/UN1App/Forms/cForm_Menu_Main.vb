Imports Xamarin.Forms
Imports Xamarin.Forms.PlatformConfiguration
Imports Xamarin.Forms.PlatformConfiguration.AndroidSpecific

Public Class cForm_Menu_Main
	Inherits Xamarin.Forms.TabbedPage

	Private Const TITLE_PAGE As String = "UN1"
	Private Const COLOUR_BACKGROUND As String = "#000000"

	Public Sub New()
		Initialise
	End Sub

	Public Sub Initialise()
		Initialise_CurrentUserInformation()

		Initialise_Page()
		Initialise_Content()
	End Sub

	Private Sub Initialise_Page()
		Title = TITLE_PAGE
		BackgroundColor = Color.FromHex(COLOUR_BACKGROUND)
	End Sub

	Private Sub Initialise_Content()
		[On](Of Xamarin.Forms.PlatformConfiguration.Android).SetToolbarPlacement(ToolbarPlacement.Bottom)

		UnselectedTabColor = Color.White
		SelectedTabColor = Color.Gray
		BarTextColor = Color.white

		'======================================
		Children.Add(New cForm_Menu_Clubs)
		Children.Add(New cForm_Menu_Events)
		Children.Add(New cForm_Menu_Friends)
		Children.Add(New cForm_Menu_News)
		Children.Add(New cForm_Menu_Profile)

		Children(0).IconImageSource = "MenuClubs.png"
		Children(1).IconImageSource = "MenuEvents.png"
		Children(2).IconImageSource = "MenuFriends.png"
		Children(3).IconImageSource = "MenuNews.png"
		Children(4).IconImageSource = "MenuProfile.png"

		'	CurrentPage = Children(4)

	End Sub
End Class
