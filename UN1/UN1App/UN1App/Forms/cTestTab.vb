Imports Xamarin.Forms
Imports Xamarin.Forms.PlatformConfiguration
Imports Xamarin.Forms.PlatformConfiguration.AndroidSpecific

Public Class cTestTab
	Inherits Xamarin.Forms.TabbedPage

	Public Sub New()
		Initialise
	End Sub

	Public Sub Initialise()
		Initialise_Page()
		Initialise_Content()
	End Sub

	Private Sub Initialise_Page()
		Title = "Test Tab"
		BackgroundColor = Color.FromHex("FFFFFF")
	End Sub

	Private Sub Initialise_Content()
		[On](Of Android).SetToolbarPlacement(ToolbarPlacement.Bottom)

		'UnselectedTabColor = Color.Black
		'SelectedTabColor = Color.Gray
		'BarBackgroundColor = Color.Black
		'BarTextColor = Color.white

		Children.Add(New ContentPage With {.Title = "Clubs", .BackgroundColor = Color.Red})
		Children.Add(New ContentPage With {.Title = "Events", .BackgroundColor = Color.Blue})
		Children.Add(New ContentPage With {.Title = "News", .BackgroundColor = Color.Orange})
		Children.Add(New ContentPage With {.Title = "Friends", .BackgroundColor = Color.Purple})
		Children.Add(New ContentPage With {.Title = "Profile", .BackgroundColor = Color.Pink})

		Children(0).IconImageSource = "MenuClubs.png"
		Children(1).IconImageSource = "MenuEvents.png"
		Children(2).IconImageSource = "MenuNews.png"
		Children(3).IconImageSource = "MenuFriends.png"
		Children(4).IconImageSource = "MenuProfile.png"


	End Sub

End Class
