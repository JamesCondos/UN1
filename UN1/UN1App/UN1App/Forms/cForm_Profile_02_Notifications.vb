Imports Xamarin.Forms

Public Class cForm_Profile_02_Notifications
	Inherits ContentPage

	Private Const TITLE_PAGE As String = "Back"
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

		oLayout.Children.Add (SetUIToolBar("Notifications", Navigation))
		oLayout.Children.Add (SetUISpacer())

		Dim sKey_EmailNotification As String = String.Empty
		Dim oSwitch_EmailNotification As Switch = SetUISwitch(sKey_EmailNotification, False)
		Dim oGrid_EmailNotification = SetUiSwitch(oSwitch_EmailNotification, "Email Notifications")

		Dim sKey_AppNotifications As String = String.Empty
		Dim oSwitch_AppNotifications As Switch = SetUISwitch(sKey_AppNotifications, False)
		Dim oGrid_AppNotifications = SetUiSwitch(oSwitch_AppNotifications, "App Notifications")

		oLayout.Children.Add (oGrid_EmailNotification)
		oLayout.Children.Add (oGrid_AppNotifications)

		Content = oLayout	
	End Sub
End Class
