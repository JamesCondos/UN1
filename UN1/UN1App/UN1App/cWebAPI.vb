Imports System.Net.Http
Imports Newtonsoft.Json
Imports Xamarin.Forms

Public Class cWebAPI
	Private Async Function DoPostAsynchronousLargeData(Byval sAccessToken As String, Byval sURL As String, Byval oKeyValues As List(Of KeyValuePair(Of String, String))) As Task (Of String)
		Try
			Dim sJSON As String = JsonConvert.SerializeObject(oKeyValues)

			Dim oHTTPContent As HttpContent = New StringContent(sJSON)
			oHTTPContent.Headers.ContentType = New Headers.MediaTypeHeaderValue("application/json")

			Dim oHTTPClient As New HttpClient(New System.Net.Http.HttpClientHandler())
			Dim oResponse As HttpResponseMessage = Await oHTTPClient.PostAsync(New Uri(sURL), oHTTPContent).ConfigureAwait(False)
			'Dim oResponse = Await oHTTPClient.SendAsync(oRequest).ConfigureAwait(False)
			Dim sResponse As String = Await oResponse.Content.ReadAsStringAsync().ConfigureAwait(False)

			oHTTPClient.Dispose
			oHTTPClient = Nothing

			Return sResponse
		Catch ex As Exception
			Console.WriteLine(ex.Message)
			Return String.Empty					
		End Try
	End Function

	Private Async Function DoPostAsynchronous(Byval sAccessToken As String, Byval sURL As String, Byval oKeyValues As List(Of KeyValuePair(Of String, String))) As Task (Of String)
		Try
			Console.WriteLine(">>>>>>>>>> Calling Post Asynchronous")

			Dim oRequest = New HttpRequestMessage(HttpMethod.Post, sURL)
			If oKeyValues IsNot Nothing Then
				oRequest.Content = New FormUrlEncodedContent(oKeyValues)
			End If

			Dim oHTTPClient = New HttpClient(New System.Net.Http.HttpClientHandler())
			If sAccessToken.Trim.Length > 0 Then
				oHTTPClient.DefaultRequestHeaders.Authorization = New Headers.AuthenticationHeaderValue(API_TAG_BEARER, sAccessToken)
			End If

			Dim oResponse = Await oHTTPClient.SendAsync(oRequest).ConfigureAwait(False)
			Dim sResponse As String = Await oResponse.Content.ReadAsStringAsync().ConfigureAwait(False)

			oHTTPClient.Dispose
			oHTTPClient = Nothing

			oRequest.Dispose
			oRequest = Nothing

			Return sResponse
		Catch ex As Exception
			Console.WriteLine(ex.Message)
			Return String.Empty					
		End Try
	End Function

	Private Function DoPostSynchronous(Byval sAccessToken As String, Byval sURL As String, Byval oKeyValues As List(Of KeyValuePair(Of String, String))) As String
		Try
			Console.WriteLine(">>>>>>>>>> Calling Post Synchronous")

			Dim oRequest = New HttpRequestMessage(HttpMethod.Post, sURL)
			If oKeyValues IsNot Nothing Then
				oRequest.Content = New FormUrlEncodedContent(oKeyValues)
			End If

			Dim oHTTPClient = New HttpClient(New System.Net.Http.HttpClientHandler())
			If sAccessToken.Trim.Length > 0 Then
				oHTTPClient.DefaultRequestHeaders.Authorization = New Headers.AuthenticationHeaderValue(API_TAG_BEARER, sAccessToken)
			End If

			Dim oResponse = oHTTPClient.SendAsync(oRequest).Result
			Dim sResponse As String = oResponse.Content.ReadAsStringAsync().Result

			oHTTPClient.Dispose
			oHTTPClient = Nothing

			oRequest.Dispose
			oRequest = Nothing

			Return sResponse
		Catch ex As Exception
			Console.WriteLine(ex.Message)
			Return String.Empty					
		End Try
	End Function

	Public Async Function AddModify_StudentHobby(ByVal oPage As Page, ByVal sAccessToken As String, Byval oStudentHobby As cStudentHobby) As Task(Of cDataHelper)
		Dim sError As String = String.Empty
		Try
			Dim sURL As String = String.Empty
			If WEBSITE_USE_LIVE = False  Then
				sURL = String.Format("{0}{1}", URL_DEVELOPMENT, API_ADDMODIFY_STUDENTHOBBY)
			Else
				sURL = String.Format("{0}{1}", URL_LIVE, API_ADDMODIFY_STUDENTHOBBY)
			End If

			Dim oKeyValues As New List(Of KeyValuePair(Of String, String))

			oKeyValues.Add(New KeyValuePair(Of String, String)("ID", oStudentHobby.ID))
			oKeyValues.Add(New KeyValuePair(Of String, String)("AspNetUsersID", oStudentHobby.AspNetUsersID))
			oKeyValues.Add(New KeyValuePair(Of String, String)("IsSelected", oStudentHobby.IsSelected))
			oKeyValues.Add(New KeyValuePair(Of String, String)("LookupHobbyID", oStudentHobby.LookupHobbyID))
			oKeyValues.Add(New KeyValuePair(Of String, String)("LookupHobbyDescription", oStudentHobby.LookupHobbyDescription))

			' **********************************************************************************
			Dim sResponse As String = Await DoPostAsynchronous(sAccessToken, sURL, oKeyValues)
			' **********************************************************************************

			If sResponse.Trim.Length = 0 Then
				Return Nothing
			Else
				Dim oDataHelper As cDataHelper= JsonConvert.DeserializeObject(Of cDataHelper)(sResponse)

				Return oDataHelper
			End If

		Catch ex As Exception
			sError = ex.Message
			Console.WriteLine(sError)
			Return Nothing
		End Try
	End Function

	Public Function AddModify_Student(ByVal oPage As Page, ByVal sAccessToken As String, Byval oStudent As cStudent) As cDataHelper
		Dim sError As String = String.Empty
		Try
			Dim sURL As String = String.Empty
			If WEBSITE_USE_LIVE = False  Then
				sURL = String.Format("{0}{1}", URL_DEVELOPMENT, API_ADDMODIFY_STUDENT)
			Else
				sURL = String.Format("{0}{1}", URL_LIVE, API_ADDMODIFY_STUDENT)
			End If

			Dim oKeyValues As New List(Of KeyValuePair(Of String, String))

			oKeyValues.Add(New KeyValuePair(Of String, String)("ID", oStudent.ID))
			oKeyValues.Add(New KeyValuePair(Of String, String)("AspNetUsersID", oStudent.AspNetUsersID))
			oKeyValues.Add(New KeyValuePair(Of String, String)("FullName", oStudent.FullName))
			oKeyValues.Add(New KeyValuePair(Of String, String)("NickName", oStudent.NickName))
			oKeyValues.Add(New KeyValuePair(Of String, String)("Biography", oStudent.Biography))
			oKeyValues.Add(New KeyValuePair(Of String, String)("University", oStudent.University))
			oKeyValues.Add(New KeyValuePair(Of String, String)("Course", oStudent.Course))
			oKeyValues.Add(New KeyValuePair(Of String, String)("StudyYear", oStudent.StudyYear))
			oKeyValues.Add(New KeyValuePair(Of String, String)("Location", oStudent.Location))
			oKeyValues.Add(New KeyValuePair(Of String, String)("FunFact", oStudent.FunFact))

			' **********************************************************************************
			Dim sResponse As String = DoPostSynchronous(sAccessToken, sURL, oKeyValues)
			' **********************************************************************************

			If sResponse.Trim.Length = 0 Then
				Return Nothing
			Else
				Dim oDataHelper As cDataHelper= JsonConvert.DeserializeObject(Of cDataHelper)(sResponse)

				Return oDataHelper
			End If

		Catch ex As Exception
			sError = ex.Message
			Console.WriteLine(sError)
			Return Nothing
		End Try
	End Function

	Public Async Function AddModify_StudentBackground(ByVal oPage As Page, ByVal sAccessToken As String, Byval oStudentImages As cStudentImages) As Task(Of cDataHelper)
		Dim sError As String = String.Empty
		Try
			Dim sURL As String = String.Empty
			If WEBSITE_USE_LIVE = False  Then
				sURL = String.Format("{0}{1}", URL_DEVELOPMENT, API_ADDMODIFY_STUDENTBACKGROUND)
			Else
				sURL = String.Format("{0}{1}", URL_LIVE, API_ADDMODIFY_STUDENTBACKGROUND)
			End If

			Dim oKeyValues As New List(Of KeyValuePair(Of String, String))

			oKeyValues.Add(New KeyValuePair(Of String, String)("AspNetUsersID", oStudentImages.AspNetUsersID))
			oKeyValues.Add(New KeyValuePair(Of String, String)("Base64String", oStudentImages.Base64String))

			' **********************************************************************************
			'Dim sResponse As String = Await DoPostAsynchronous(sAccessToken, sURL, oKeyValues)
			Dim sResponse As String = Await DoPostAsynchronousLargeData(sAccessToken, sURL, oKeyValues)
			' **********************************************************************************

			If sResponse.Trim.Length = 0 Then
				Return Nothing
			Else
				Dim oDataHelper As cDataHelper= JsonConvert.DeserializeObject(Of cDataHelper)(sResponse)

				Return oDataHelper
			End If

		Catch ex As Exception
			sError = ex.Message
			Console.WriteLine(sError)
			Return Nothing
		End Try
	End Function

	Public Async Function AddModify_StudentImage(ByVal oPage As Page, ByVal sAccessToken As String, Byval oStudentImages As cStudentImages) As Task(Of cDataHelper)
		Dim sError As String = String.Empty
		Try
			Dim sURL As String = String.Empty
			If WEBSITE_USE_LIVE = False  Then
				sURL = String.Format("{0}{1}", URL_DEVELOPMENT, API_ADDMODIFY_STUDENTIMAGE)
			Else
				sURL = String.Format("{0}{1}", URL_LIVE, API_ADDMODIFY_STUDENTIMAGE)
			End If

			Dim oKeyValues As New List(Of KeyValuePair(Of String, String))

			oKeyValues.Add(New KeyValuePair(Of String, String)("AspNetUsersID", oStudentImages.AspNetUsersID))
			oKeyValues.Add(New KeyValuePair(Of String, String)("Base64String", oStudentImages.Base64String))

			' **********************************************************************************
			Dim sResponse As String = Await DoPostAsynchronous(sAccessToken, sURL, oKeyValues)
			' **********************************************************************************

			Console.WriteLine(sResponse)

			If sResponse.Trim.Length = 0 Then
				Return Nothing
			Else
				Dim oDataHelper As cDataHelper= JsonConvert.DeserializeObject(Of cDataHelper)(sResponse)

				Return oDataHelper
			End If

		Catch ex As Exception
			sError = ex.Message
			Console.WriteLine(sError)
			Return Nothing
		End Try
	End Function


	Public Function Get_CurrentUserInfo(ByVal sAccessToken As String) As cDataHelper
		Dim sError As String = String.Empty
		Try
			Dim sURL As String = String.Empty
			If WEBSITE_USE_LIVE = False  Then
				sURL = String.Format("{0}{1}", URL_DEVELOPMENT, API_ACCOUNT_CURRENTUSERINFO)
			Else
				sURL = String.Format("{0}{1}", URL_LIVE, API_ACCOUNT_CURRENTUSERINFO)
			End If

			' **********************************************************************************
			Dim sResponse As String = DoPostSynchronous(sAccessToken, sURL, Nothing)
			' **********************************************************************************

			If sResponse.Trim.Length = 0 Then
				Return Nothing
			Else
				Dim oDataHelper As cDataHelper= JsonConvert.DeserializeObject(Of cDataHelper)(sResponse)

				Return oDataHelper
			End If

		Catch ex As Exception
			sError = ex.Message
			Console.WriteLine(sError)
			Return Nothing
		End Try
	End Function
	
	Public Function Get_StudentHobbies(ByVal sAccessToken As String, Byval sAspNetUsersID As String) As cDataHelper
		Dim sError As String = String.Empty
		Try
			Dim sURL As String = String.Empty
			If WEBSITE_USE_LIVE = False  Then
				sURL = String.Format("{0}{1}", URL_DEVELOPMENT, API_GET_STUDENTHOBBY)
			Else
				sURL = String.Format("{0}{1}", URL_LIVE, API_GET_STUDENTHOBBY)
			End If

			Dim oKeyValues As New List(Of KeyValuePair(Of String, String))
			oKeyValues.Add(New KeyValuePair(Of String, String)("AspNetUsersID", sAspNetUsersID))

			' **********************************************************************************
			Dim sResponse As String = DoPostSynchronous(sAccessToken, sURL, oKeyValues)
			' **********************************************************************************

			If sResponse.Trim.Length = 0 Then
				Return Nothing
			Else
				Dim oDataHelper As cDataHelper= JsonConvert.DeserializeObject(Of cDataHelper)(sResponse)
				Return oDataHelper
			End If

		Catch ex As Exception
			sError = ex.Message
			Console.WriteLine(sError)
			Return Nothing
		End Try
	End Function

	Public Async Function GetAll_LookupHobby() As Task(Of cDataHelper)
		Dim sError As String = String.Empty
		Try
			Dim sURL As String = String.Empty
			If WEBSITE_USE_LIVE = False  Then
				sURL = String.Format("{0}{1}", URL_DEVELOPMENT, API_GETALL_LOOKUPHOBBY)
			Else
				sURL = String.Format("{0}{1}", URL_LIVE, API_GETALL_LOOKUPHOBBY)
			End If

			' **********************************************************************************
			Dim sResponse As String = Await DoPostAsynchronous(String.Empty, sURL, Nothing)
			' **********************************************************************************

			If sResponse.Trim.Length = 0 Then
				Return Nothing
			Else
				Dim oDataHelper As cDataHelper= JsonConvert.DeserializeObject(Of cDataHelper)(sResponse)
				Return oDataHelper
			End If

		Catch ex As Exception
			sError = ex.Message
			Return Nothing
		End Try
	End Function

	Public Function IsAuthorized(ByVal oPage As Page, ByVal sAccessToken As String) As Boolean
		Dim sError As String = String.Empty
		Try
			Dim sURL As String = String.Empty
			If WEBSITE_USE_LIVE = False  Then
				sURL = String.Format("{0}{1}", URL_DEVELOPMENT, API_ISAUTHORIZED)
			Else
				sURL = String.Format("{0}{1}", URL_LIVE, API_ISAUTHORIZED)
			End If

			' **********************************************************************************
			Dim sResponse As String = DoPostSynchronous(sAccessToken, sURL, Nothing)
			' **********************************************************************************

			Dim bResult As Boolean = CBool(sResponse)

			Return bResult
		Catch ex As Exception
			sError = ex.Message
			Console.WriteLine("********* {0}", sError)
			Return False
		End Try
	End Function

	Public Async Function ChangePassword(ByVal oPage As Page, Byval sAccessToken As String, ByVal sOldPassword As String, Byval sNewPassword As String, Byval sConfirmPassword As String) As Task(Of cDataHelper)
		Dim oDataHelper As New cDataHelper
		Try
			Dim sURL As String = String.Empty
			If WEBSITE_USE_LIVE = False  Then
				sURL = String.Format("{0}{1}", URL_DEVELOPMENT, API_ACCOUNT_CHANGEPASSWORD)
			Else
				sURL = String.Format("{0}{1}", URL_LIVE, API_ACCOUNT_CHANGEPASSWORD)
			End If

			Dim oKeyValues As New List(Of KeyValuePair(Of String, String))

			oKeyValues.Add(New KeyValuePair(Of String, String)("OldPassword", sOldPassword))
			oKeyValues.Add(New KeyValuePair(Of String, String)("NewPassword", sNewPassword))
			oKeyValues.Add(New KeyValuePair(Of String, String)("ConfirmPassword", sConfirmPassword))

			' **********************************************************************************
			Dim sResponse As String = Await DoPostAsynchronous(sAccessToken, sURL, oKeyValues)
			' **********************************************************************************

			'Dim oRequest = New HttpRequestMessage(HttpMethod.Post, sURL)
			'oRequest.Content = New FormUrlEncodedContent(oKeyValues)

			'Dim oHTTPClient = New HttpClient(New System.Net.Http.HttpClientHandler())
			'oHTTPClient.DefaultRequestHeaders.Authorization = New Headers.AuthenticationHeaderValue(API_TAG_BEARER, sAccessToken)

			'Dim oResponse = Await oHTTPClient.SendAsync(oRequest).ConfigureAwait(False)
			'Dim sResponse As String = Await oResponse.Content.ReadAsStringAsync().ConfigureAwait(False)

			If sResponse.Trim.Length = 0 Then
				oDataHelper.Result = True
			Else
				Dim oDictionary As Dictionary(Of String, Object) = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(sResponse)
				If oDictionary.ContainsKey(API_TAG_MESSAGE) = True Then
					Dim sMessage As String = oDictionary.Item(API_TAG_MESSAGE).ToString
					oDataHelper.Result = False
					oDataHelper.Message = sMessage
				Else
					oDataHelper.Result = True
				End If
			End If

		Catch ex As Exception
				oDataHelper.Result = False 
				oDataHelper.Message = ex.Message
		End Try

		Return oDataHelper
	End Function

	Public Async Function RegisterUserAsync(ByVal sFullName As String, ByVal sEmail As String, ByVal sPassword As String, ByVal sConfirmPassword As String) As Task(Of String)
		Dim oModel As Object = New RegisterBindingModel With {.FullName = sFullName, .Email = sEmail, .Password = sPassword, .ConfirmPassword = sConfirmPassword}
		Dim sJSON As String = JsonConvert.SerializeObject(oModel)

		Dim oHTTPContent As HttpContent = New StringContent(sJSON)
		oHTTPContent.Headers.ContentType = New Headers.MediaTypeHeaderValue("application/json")

		Dim sURL As String = String.Empty
		If WEBSITE_USE_LIVE = False  Then
			sURL = String.Format("{0}{1}", URL_DEVELOPMENT, API_ACCOUNT_REGISTER)
		Else
			sURL = String.Format("{0}{1}", URL_LIVE, API_ACCOUNT_REGISTER)
		End If

		Dim oHTTPClient As New HttpClient(New System.Net.Http.HttpClientHandler())
		Dim oResponse As HttpResponseMessage = Await oHTTPClient.PostAsync(New Uri(sURL), oHTTPContent)
		Dim oResult = oResponse.Content.ReadAsStringAsync().Result
		Dim sResponse As String = Await oResponse.Content.ReadAsStringAsync()

		If sResponse.Trim.Length = 0 Then
			Return sResponse
		End If

		Dim oDictionary As Dictionary(Of String, Object) = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(sResponse)
		If oDictionary.ContainsKey(API_TAG_MESSAGE) = True Then
			Dim sMessage As String = oDictionary.Item(API_TAG_MESSAGE).ToString
			Return sMessage
		End If

		Return sResponse
	End Function

	Public Async Function LoginUserAsync(ByVal oPage As Page, ByVal sEmail As String, ByVal sPassword As String) As Task(Of String)
		Dim sMessage As String = String.Empty
		Try
			Dim oKeyValues As New List(Of KeyValuePair(Of String, String))

			oKeyValues.Add(New KeyValuePair(Of String, String)("Username", sEmail))
			oKeyValues.Add(New KeyValuePair(Of String, String)("Password", sPassword))
			oKeyValues.Add(New KeyValuePair(Of String, String)("grant_type", "password"))

			Dim sURL As String = String.Empty
			If WEBSITE_USE_LIVE = False  Then
				sURL = String.Format("{0}{1}", URL_DEVELOPMENT, API_ACCOUNT_LOGIN)
			Else
				sURL = String.Format("{0}{1}", URL_LIVE, API_ACCOUNT_LOGIN)
			End If

			Dim oRequest = New HttpRequestMessage(HttpMethod.Post, sURL)
			oRequest.Content = New FormUrlEncodedContent(oKeyValues)

			Dim oHTTPClient = New HttpClient(New System.Net.Http.HttpClientHandler())
			Dim oResponse = Await oHTTPClient.SendAsync(oRequest).ConfigureAwait(False)
			Dim sResponse As String = Await oResponse.Content.ReadAsStringAsync().ConfigureAwait(False)

			'Test if the Object is JSON, or a message return string (Bad Response - wrong login)
			Try
				Dim oObject As Linq.JObject = JsonConvert.DeserializeObject(sResponse)

				If oObject.ContainsKey(API_TAG_ACCESSTOKEN) = True Then
					Dim sAccessToken As String = oObject.Value(Of String)(API_TAG_ACCESSTOKEN)

					UN1.Helpers.cSettings.AppSettings.AddOrUpdateValue(API_TAG_ACCESSTOKEN, sAccessToken)

					Return String.Empty
				Else
					Dim sErrorDescription As String = oObject.Value(Of String)(API_TAG_ERRORDESCRIPTION)
					Return sErrorDescription
				End If
		
			Catch ex As Exception
				Return sResponse
			End Try
		Catch ex As Exception
			sMessage = ex.Message
			Return sMessage
		End Try
	End Function

End Class
