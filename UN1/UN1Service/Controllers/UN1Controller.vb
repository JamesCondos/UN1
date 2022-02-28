Imports System.Net
Imports System.Net.Http
Imports System.Security.Claims
Imports System.Web.Http
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework

<Authorize>
Public Class UN1Controller
	Inherits ApiController

	<System.Web.Http.HttpPost>
	<Route("api/IsAuthorized")>
	Public Function IsAuthorized() As Boolean
		Return True
	End Function

	<OverrideAuthorization>
	<System.Web.Http.HttpPost>
	<Route("api/GetAll_LookupHobby")>
	Public Function GetAll_LookupHobby() As cDataHelper
		Dim oBusinessRule_LookupHobby As New cBusinessRule_LookupHobby
		Dim oDataHelper As cDataHelper = oBusinessRule_LookupHobby.GetAll_LookupHobby
		oBusinessRule_LookupHobby = Nothing

		Return oDataHelper
	End Function

	<System.Web.Http.HttpPost>
    <Route("api/StudentAddModify")>
    Public Function StudentAddModify(Byval oObject_In As cStudent) As cDataHelper
		Dim oDataHelper As New cDataHelper

		If oObject_In Is Nothing Then
			oDataHelper.Message = "Object_In Is Nothing"
			Return oDataHelper
		End If

		Dim oBusinessRule_Student As New cBusinessRule_Student
		Dim oObject_Out As cStudent = oBusinessRule_Student.AddModify(oObject_In)
		oBusinessRule_Student = Nothing

		oDataHelper.Result = True

		oDataHelper.ID = oObject_Out.ID.ToString
		oDataHelper.AspNetUsersID = oObject_Out.AspNetUsersID
		oDataHelper.FullName = oObject_Out.FullName
		oDataHelper.NickName = oObject_Out.NickName
		oDataHelper.Biography = oObject_Out.Biography
		oDataHelper.University = oObject_Out.University
		oDataHelper.Course = oObject_Out.Course
		oDataHelper.StudyYear = oObject_Out.StudyYear
		oDataHelper.Location = oObject_Out.Location
		oDataHelper.FunFact = oObject_Out.FunFact

		Return oDataHelper
    End Function

	<System.Web.Http.HttpPost>
    <Route("api/StudentHobbyAddModify")>
    Public Function StudentHobbyAddModify(Byval oObject_In As cStudentHobby) As cDataHelper
		Dim oDataHelper As New cDataHelper

		If oObject_In Is Nothing Then
			oDataHelper.Message = "Object_In Is Nothing"
			Return oDataHelper
		End If

		Dim oBusinessRule_StudentHobby As New cBusinessRule_StudentHobby
		Dim oObject_Out As cStudentHobby = oBusinessRule_StudentHobby.AddModify(oObject_In)
		oBusinessRule_StudentHobby = Nothing

		oDataHelper.Result = True

		oDataHelper.ID = oObject_Out.ID.ToString
		oDataHelper.AspNetUsersID = oObject_Out.AspNetUsersID
		oDataHelper.IsSelected = oObject_Out.IsSelected
		oDataHelper.LookupHobbyID = oObject_Out.LookupHobbyID
		oDataHelper.LookupHobbyDescription  = oObject_Out.LookupHobbyDescription

		Return oDataHelper
    End Function

	<System.Web.Http.HttpPost>
    <Route("api/StudentHobbyGet")>
    Public Function StudentHobbyGet(Byval oObject_In As cStudentHobby) As cDataHelper
		Dim oDataHelper As New cDataHelper

		Dim sAspNetUsersID As String = oObject_In.AspNetUsersID

		If sAspNetUsersID.Trim.Length = 0 Then
			oDataHelper.Message = "No User Hobbies Found."
			Return oDataHelper
		End If

		Dim oBusinessRule_StudentHobby As New cBusinessRule_StudentHobby
		Dim oDataHelper_Out As cDataHelper = oBusinessRule_StudentHobby.GetViaAspNetUsersID(sAspNetUsersID)
		oBusinessRule_StudentHobby = Nothing

		oDataHelper_Out.Result = True
		oDataHelper_Out.AspNetUsersID = sAspNetUsersID

		Return oDataHelper_Out
	End Function

	<System.Web.Http.HttpPost>
    <Route("api/StudentBackgroundAddModify")>
    Public Function StudentBackgroundAddModify(Byval oObject_In As cStudentImages) As cDataHelper
		Dim oDataHelper As New cDataHelper

		Try
			If oObject_In Is Nothing Then
				oDataHelper.Message = "Object_In Is Nothing"
				Return oDataHelper
			End If

			oObject_In.StudentBackground = Save_Image(oObject_In)

			Dim oBusinessRule_Student As New cBusinessRule_Student
			Dim oObject_Out As cStudentImages = oBusinessRule_Student.AddModify_StudentBackground(oObject_In)
			oBusinessRule_Student = Nothing

			oDataHelper.Result = True

			oDataHelper.ID = oObject_Out.ID.ToString
			oDataHelper.AspNetUsersID = oObject_Out.AspNetUsersID
			oDataHelper.StudentBackground = oObject_Out.StudentBackground

		Catch ex As Exception
			Log(Debug_GetCurrentFunctionName(), ex.Message)
			oDataHelper.Result = False 
			oDataHelper.Message = ex.Message
		End Try

		Return oDataHelper
    End Function

	<System.Web.Http.HttpPost>
    <Route("api/StudentImageAddModify")>
    Public Function StudentImageAddModify(Byval oObject_In As cStudentImages) As cDataHelper
		Dim oDataHelper As New cDataHelper
		Try
			If oObject_In Is Nothing Then
				oDataHelper.Message = "Object_In Is Nothing"
				Return oDataHelper
			End If

			oObject_In.StudentImage = Save_Image(oObject_In)

			Dim oBusinessRule_Student As New cBusinessRule_Student
			Dim oObject_Out As cStudentImages = oBusinessRule_Student.AddModify_StudentImage(oObject_In)
			oBusinessRule_Student = Nothing

			oDataHelper.Result = True

			oDataHelper.ID = oObject_Out.ID.ToString
			oDataHelper.AspNetUsersID = oObject_Out.AspNetUsersID
			oDataHelper.StudentImage = oObject_Out.StudentImage

		Catch ex As Exception
			Log(Debug_GetCurrentFunctionName(), ex.Message)
			oDataHelper.Result = False 
			oDataHelper.Message = ex.Message
		End Try

		Return oDataHelper
    End Function

End Class