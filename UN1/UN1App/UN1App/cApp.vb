Imports Xamarin.Forms

Public Class cApp
	Inherits Application

    Public Sub New()
		Initialise()
		MainPage = SetNavigationPage(New cForm_Main)

		'MainPage = SetNavigationPage(New cForm_Menu_Profile)
	End Sub

	Public Sub Initialise()
		Global_Initialise()
	End Sub

End Class
