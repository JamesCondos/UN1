Imports Xamarin.Forms

Public Class cForm_Menu_Clubs
	Inherits ContentPage

	Private Const TITLE_PAGE As String = MENU_Clubs
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
		Dim oStack_Top = New StackLayout With {.Spacing = 1,  .BackgroundColor = Color.red,
			.VerticalOptions = LayoutOptions.Start, .HorizontalOptions = LayoutOptions.StartAndExpand}

		oStack_Top.Children.Add(SetUIImageAndSearchBar(goStudent.StudentImage, Navigation))

		Dim oLayout_Main As New StackLayout With {.Spacing = 1}
		oLayout_Main.Children.Add (oStack_Top)
		oLayout_Main.Children.Add (CreateUIClubFunctions())

		Content = oLayout_Main
	End Sub

	Private Function CreateImageInView(ByVal sImageSource As String) As ImageButton
		Dim oObject As New ImageButton With {.Source = sImageSource, 
			.BackgroundColor = Color.Black, .Aspect = Aspect.AspectFill,
			.Margin = 0, .Padding = 0
			}

		Return oObject
	End Function

	Private Function CreateIconInView(ByVal sImageSource As String, optional byval iScale As Double = 0.5) As StackLayout

		Dim oImage As New Image With {.Source = sImageSource, .BackgroundColor = Color.Black, .Aspect = Aspect.AspectFit,
			.HeightRequest = 140, .Margin = 0, .Scale = iScale
			}

		Dim oLabel As New Label With {.Text = "Test", .BackgroundColor = Color.Black, .TextColor = Color.White}

		Dim oLayout_Image = New StackLayout With {.Spacing = 0, .Margin = 0, .Padding = 0,  .VerticalOptions = LayoutOptions.Start}
		oLayout_Image.Children.Add (oImage)

		Dim oLayout_Label = New StackLayout With {.Spacing = 0, .Margin = 0, .Padding = 0,  .VerticalOptions = LayoutOptions.Start, .TranslationY = -20}
		oLayout_Image.Children.Add (oLabel)


		Dim oLayout = New StackLayout With {.Spacing = 0, .Margin = 0, .Padding = 0,  .VerticalOptions = LayoutOptions.Center}
		oLayout.Children.Add (oLayout_Image)
		oLayout.Children.Add (oLayout_Label)

		Return oLayout
	End Function
	   
	Private Function CreateUIClubFunctions() As ScrollView
		Dim oGrid As New Grid With {.BackgroundColor = Color.FromHex(COLOUR_BACKGROUND)}

		oGrid.RowDefinitions.Add(New RowDefinition With {.Height = New GridLength(1, GridUnitType.Star)})
		oGrid.RowDefinitions.Add(New RowDefinition With {.Height = New GridLength(1, GridUnitType.Star)})
		oGrid.RowDefinitions.Add(New RowDefinition With {.Height = New GridLength(1, GridUnitType.Star)})
		oGrid.RowDefinitions.Add(New RowDefinition With {.Height = New GridLength(1, GridUnitType.Star)})

			oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Star)})
			oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Star)})

		oGrid.Children.Add (CreateImageInView("ClubFoodDrinks.png"), 0, 0)
			oGrid.Children.Add (SetIconClubs("ClubFoodDrinksIcon.png", "Food and Drinks"), 0, 0)
			
		oGrid.Children.Add (CreateImageInView("ClubCommunityService.png"), 1, 0)
			oGrid.Children.Add (SetIconClubs("ClubCommunityServiceIcon.png", "Community Service"), 1, 0)

		oGrid.Children.Add (CreateImageInView("ClubCultureLanguage.png"), 0, 1)
			oGrid.Children.Add (SetIconClubs("ClubCultureLanguageIcon.png", "Culture and Language"), 0, 1)

		oGrid.Children.Add (CreateImageInView("ClubPoliticsActivism.png"), 1, 1)
			oGrid.Children.Add (SetIconClubs("ClubPoliticsActivismIcon.png", "Politics and Activism"), 1, 1)

		oGrid.Children.Add (CreateImageInView("ClubTravelOutdoorActivities.png"), 0, 2)
			oGrid.Children.Add (SetIconClubs("ClubTravelOutdoorActivitiesIcon.png", "Travel and Outdoors"), 0, 2)

		oGrid.Children.Add (CreateImageInView("ClubReligion.png"), 1, 2)
			oGrid.Children.Add (SetIconClubs("ClubReligionIcon.png", "Religion"), 1, 2)

		oGrid.Children.Add (CreateImageInView("ClubMusicDance.png"), 0, 3)
			oGrid.Children.Add (SetIconClubs("ClubMusicDanceIcon.png", "Music and Dance"), 0, 3)

		oGrid.Children.Add (CreateImageInView("ClubSportsGames.png"), 1, 3)
			oGrid.Children.Add (SetIconClubs("ClubSportsGamesIcon.png", "Sports and games"), 1, 3)

		Dim oScrollView As New ScrollView With {.Content = oGrid, .FlowDirection = FlowDirection.LeftToRight, 
			.VerticalOptions = LayoutOptions.StartAndExpand, .Margin = New Thickness(5, 0, 5, 0), 
			.Orientation = ScrollOrientation.Vertical, .HorizontalScrollBarVisibility = False, .VerticalScrollBarVisibility = False}

		oGrid = Nothing

		Return oScrollView
	End Function


	'Private Function CreateUIClubFunctionsX() As ScrollView
	'	Dim oGrid As New Grid With {.BackgroundColor = Color.FromHex(COLOUR_BACKGROUND),
	'		.ColumnSpacing = 1, .RowSpacing = 1}

	'	oGrid.RowDefinitions.Add(New RowDefinition With {.Height = New GridLength(1, GridUnitType.Star)})
	'	oGrid.RowDefinitions.Add(New RowDefinition With {.Height = New GridLength(1, GridUnitType.Star)})
	'	oGrid.RowDefinitions.Add(New RowDefinition With {.Height = New GridLength(1, GridUnitType.Star)})

	'	oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Star)})
	'	oGrid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(1, GridUnitType.Star)})


	'	oGrid.Children.Add (CreateImageInView("ClubFoodDrinks1.png"), 0, 0)
	'	oGrid.Children.Add (CreateImageInView("ClubCommunityService2.png"), 1, 0)

	'	oGrid.Children.Add (CreateImageInView("ClubCultureLanguage3.png"), 0, 1)
	'	oGrid.Children.Add (CreateImageInView("ClubPoliticsActivism4.png"), 1, 1)

	'	oGrid.Children.Add (CreateImageInView("ClubTravelOutdoorActivities5.png"), 0, 2)
	'	oGrid.Children.Add (CreateImageInView("ClubReligion6.png"), 1, 2)

	'	oGrid.Children.Add (CreateImageInView("ClubMusicDance7.png"), 0, 3)
	'	oGrid.Children.Add (CreateImageInView("ClubSportsGames8.png"), 1, 3)
		

	'	Dim oScrollView As New ScrollView With {.Content = oGrid, .FlowDirection = FlowDirection.LeftToRight, .VerticalOptions = LayoutOptions.Start, 
	'		.Orientation = ScrollOrientation.Vertical, .HorizontalScrollBarVisibility = False, .VerticalScrollBarVisibility = False}

	'	oGrid = Nothing

	'	Return oScrollView
	'End Function

	Private Function GetAdvertisements() As ScrollView
		Dim oGrid As New Grid
		oGrid.RowDefinitions.Add (New RowDefinition With {.Height = New GridLength(1, GridUnitType.Star)})

		Dim bAlternateBackground As Boolean = True
		For iCounter As Integer = 0 To 5

			bAlternateBackground  = Not bAlternateBackground 

			Dim oStackLayoutAdvertisement As New StackLayout
			Dim oLabel As New Label

			Select Case bAlternateBackground 
			    Case True
					oStackLayoutAdvertisement.BackgroundColor = Color.Blue
					oLabel.TextColor = Color.White
				Case False
					oStackLayoutAdvertisement.BackgroundColor = Color.Red
					oLabel.TextColor = Color.Green
			End Select

			oLabel.Text = String.Format("Number {0}", iCounter.ToString)
			oLabel.VerticalOptions = LayoutOptions.FillAndExpand
			oLabel.VerticalTextAlignment = TextAlignment.Center
			oLabel.HorizontalTextAlignment = TextAlignment.Center

			oStackLayoutAdvertisement.VerticalOptions  = LayoutOptions.FillAndExpand
			oStackLayoutAdvertisement.HorizontalOptions = LayoutOptions.FillAndExpand
			oStackLayoutAdvertisement.Children.Add (oLabel)

				oGrid.ColumnDefinitions.Add (New ColumnDefinition With {.Width = New GridLength(200)})
				oGrid.Children.Add (oStackLayoutAdvertisement, iCounter, 0)

			oLabel = Nothing
			oStackLayoutAdvertisement = Nothing
			
		Next

		Dim oScrollView As New ScrollView With {.Content = oGrid, .FlowDirection = FlowDirection.LeftToRight, .Orientation = ScrollOrientation.Horizontal, .HorizontalScrollBarVisibility = False, .VerticalScrollBarVisibility = False}

		oGrid = Nothing

		Return oScrollView
	End Function


End Class
