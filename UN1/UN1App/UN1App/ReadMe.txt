480 x 800

To Create APK File:
- Go to .Android Project
- Set Release -> Deploy

ALWAYS REMOVE ROSLYN!!!!
https://galdin.dev/blog/removing-roslyn-from-asp-net-4-5-2-project-template/

https://github.com/ufinixtutorials/Xamarin-Camera/blob/master/XamarinCamera/MainActivity.cs

https://stackoverflow.com/questions/37398962/how-to-remove-a-modal-page-from-navigationstack-in-xamarin-forms


AccountController.vb is the main class

The media plugin for the camera
https://github.com/jamesmontemagno/MediaPlugin
https://github.com/jamesmontemagno/MediaPlugin


		BIO = 100 Characters Max


		Scroll Lock Friends under Profile Page


After some tests, I have found whats cause the problem:
if I remove 
[XamlCompilation(XamlCompilationOptions.Compile)]
from all views inside pcl project, I can see local variables.


as workaroung i use this directive:

#if DEBUG
#else
    [XamlCompilation(XamlCompilationOptions.Compile)]
#endif

so when I compile for release, I can still use xamlcompilation, but during debug I can see local variables!


public async Task<WeatherObservation[]> GetWeatherObservationsAsync () {

        var client = new System.Net.Http.HttpClient ();

        client.BaseAddress = new Uri("http://api.geonames.org/");

        var response = await client.GetAsync("weatherJSON?north=44.1&south=-9.9&east=-22.4&west=55.2&username=bertt&maxRows=20");

        var earthquakesJson = response.Content.ReadAsStringAsync().Result;

        var rootobject = JsonConvert.DeserializeObject<RootObjectWO>(earthquakesJson);

        return rootobject.weatherObservations;

    }




	Unable to evaluate the expression.













	Public Async Function RegisterUserAsync(Byval sFullName As String, ByVal sEmail As String, ByVal sPassword As String, ByVal sConfirmPassword As String) As Task(Of String)
		Dim oModel As Object = New RegisterBindingModel With {.FullName = sFullName, .Email = sEmail, .Password = sPassword, .ConfirmPassword = sConfirmPassword}

		Dim sJSON As String = JsonConvert.SerializeObject(oModel)

		Dim oHTTPContent As HttpContent = New StringContent(sJSON)
		oHTTPContent.Headers.ContentType = New Headers.MediaTypeHeaderValue("application/json")

		Dim sURL As String = String.Empty

		If Debugger.IsAttached = True Then
			sURL = String.Format("{0}{1}", URL_DEVELOPMENT, API_DEVELOPMENT_ACCOUNT_REGISTER)
		Else
			sURL = String.Format("{0}{1}", URL_LIVE, API_LIVE_ACCOUNT_REGISTER)
		End If

		Dim oHTTPClient As New HttpClient(new System.Net.Http.HttpClientHandler())
		Dim oResponse As HttpResponseMessage = Await oHTTPClient.PostAsync(New Uri(sURL), oHTTPContent)


		Dim oResult = oResponse.Content.ReadAsStringAsync().Result

		'Dim oRootObject = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(oResult)
		'Dim sResponse As String = String.Empty

		Dim sResponse As String = Await oResponse.Content.ReadAsStringAsync()

		'Dim oDataHelper As cDataHelper = JsonConvert.DeserializeObject(Of cDataHelper)(sResponse)

		Dim oDictionary As Dictionary(Of String, Object) = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(sResponse)


		If oDictionary.ContainsKey("Message") = True Then
			Dim sMessage As String = oDictionary.Item("Message").ToString
			Return sMessage
		End If

		'For Each oKP As KeyValuePair(Of String, Object) in oDictionary
		'	Dim sName = oKP.Key
		'	Dim oObject = oKP.Value

			


		'	Console.WriteLine(">>> [{0}] ==  {1}", sName, oObject)
		'	Console.WriteLine("")

		'Next



		'Dim oObject As KeyValuePair(Of String, Object) = oDictionary.Item("ModelState")
		'Dim sModelState As String = oObject.Value

		'Console.WriteLine("**************** " & oDataHelper.Dictionary_Error.Count.ToString)


		Return sResponse
	End Function










[HttpPost]
[AllowAnonymous]
[Route("login")]
public async Task<IHttpActionResult> LoginUser(UserAccountBindingModel model)
{
    if (model == null)
    {
        return this.BadRequest("Invalid user data");
    }
 
    // Invoke the "token" OWIN service to perform the login (POST /api/token)
    var testServer = TestServer.Create<Startup>();
    var requestParams = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("grant_type", "password"),
        new KeyValuePair<string, string>("username", model.Username),
        new KeyValuePair<string, string>("password", model.Password)
    };
    var requestParamsFormUrlEncoded = new FormUrlEncodedContent(requestParams);
    var tokenServiceResponse = await testServer.HttpClient.PostAsync(
        "/api/Token", requestParamsFormUrlEncoded);
 
    return this.ResponseMessage(tokenServiceResponse);
}