Imports Xamarin.Forms

Module mConstants
	Friend Const WEBSITE_USE_LIVE As Boolean = True
	Friend Const LOGIN_USEMINE As Boolean = False

	Friend Const URL_DEVELOPMENT As String = "http://192.168.1.88:5000/"
	Friend Const URL_LIVE As String =  "http://un1.cksoft.com.au/"
	
	'System Functions ---------------------------------------------------------------------------
	Friend Const API_GETALL_LOOKUPHOBBY As String = "api/GetAll_LookupHobby"
	Friend Const API_ADDMODIFY_STUDENT As String = "api/StudentAddModify"
	Friend Const API_ADDMODIFY_STUDENTHOBBY As String = "api/StudentHobbyAddModify"
	Friend Const API_GET_STUDENTHOBBY As String = "api/StudentHobbyGet"

	Friend Const API_ADDMODIFY_STUDENTBACKGROUND As String = "api/StudentBackgroundAddModify"
	Friend Const API_ADDMODIFY_STUDENTIMAGE As String = "api/StudentImageAddModify"

	Friend Const API_ACCOUNT_CURRENTUSERINFO As String = "api/Account/CurrentUserInfo"
	Friend Const API_ACCOUNT_REGISTER As String = "api/Account/Register"
	Friend Const API_GETVALUES As String = "api/values"
	Friend Const API_ISAUTHORIZED As String = "api/IsAuthorized"
	Friend Const API_ACCOUNT_LOGIN As String = "Token"
	Friend Const API_ACCOUNT_CHANGEPASSWORD As String = "api/Account/ChangePassword"

	Friend Const API_TAG_BEARER As String = "Bearer"
	Friend Const API_TAG_ACCESSTOKEN As String = "access_token"
	Friend Const API_TAG_ERRORDESCRIPTION As String = "error_description"
	Friend Const API_TAG_MESSAGE As String = "Message"

	Public Const FONTSIZE_NORMAL As Integer = 16

	Public Const MENU_Clubs As String = "Clubs"
	Public Const MENU_Events As String = "Events"
	Public Const MENU_Friends As String = "Friends"
	Public Const MENU_News As String = "News"
	Public Const MENU_Profile As String = "Profile"

	Public Const TITLE_MENU_Clubs As String = "Clubs"
	Public Const TITLE_MENU_Events As String = "Events"
	Public Const TITLE_MENU_Friends As String = "Friends"
	Public Const TITLE_MENU_News As String = "News"
	Public Const TITLE_MENU_Profile As String = "Profile"

	Public const TITLE_NAV_BACK As String = "Back"
	Public const TITLE_NAV_SIGNUP As String = "Signup"

	Public const TITLE_FORM_EDITPROFILE As String = "Edit Profile"

    Friend Const DEFAULT_DATETIMESTAMP_SQL As String = "yyyy-MM-dd HH:mm:ss.fff"
	Friend Const DEFAULT_DATE_DISPLAY00 As String = "d MMM yyyy"
	Friend Const DEFAULT_DATETIMESTAMP_FILE As String = "yyyyMMddHHmm"
	Friend Const DEFAULT_DATE_JOIN00 As String = "MMM yyyy"

	Friend Const TAG_DOT As String = "•"

	Friend Const TAG_NA As String = "N/A"

	Friend Const VALUE_ZERO As Integer = 0
	Friend Const VALUE_ONE As Integer = 1

	Friend Const FOLDER_APP_IMAGES As String = "App_Images"
	Friend Const FOLDER_APP_DATA As String = "App_Data"


	Friend Const IMAGE_BACKGROUND As String = "BackgroundProfile.png"
	Friend Const IMAGE_PERSON As String = "ProfileDefault.png"

End Module
