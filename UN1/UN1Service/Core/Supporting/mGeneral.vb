Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.Owin

Module mGeneral
	Friend Function Save_Image(Byref oStudentImages As cStudentImages) As String
		Dim iMaxSize As Integer = CInt(GetAppSetting("MAXSIZE_UPLOAD_IMAGE"))

		Try
			If oStudentImages.Base64String.Trim.Length = 0 Then Return String.Empty

			Dim sFilenameRandom As String = Replace(IO.Path.GetRandomFileName(), ".", "")
			sFilenameRandom = String.Format("{0}{1}", sFilenameRandom, EXTENSION_IMAGE_JPG)

			Dim sFolder As String = String.Format("~{0}/{1}", FOLDER_APP_IMAGES, oStudentImages.AspNetUsersID)

			If IO.Directory.Exists(System.Web.Hosting.HostingEnvironment.MapPath(sFolder)) = False Then
				Dim oDirectoryInfo As IO.DirectoryInfo = IO.Directory.CreateDirectory(System.Web.Hosting.HostingEnvironment.MapPath(sFolder))
			End If

			Dim sFolderFilename As String = System.Web.Hosting.HostingEnvironment.MapPath(String.Format("{0}/{1}", sFolder, sFilenameRandom))

			Dim oByteArray() As Byte = Convert.FromBase64String(oStudentImages.Base64String)

			Using oMemoryStream As New System.IO.MemoryStream
				Using oFileStream As New System.IO.FileStream(sFolderFilename, IO.FileMode.Create, IO.FileAccess.ReadWrite)
					oMemoryStream.Write(oByteArray, 0, oByteArray.Length)
					Dim oNewByteArray() As Byte = oMemoryStream.ToArray
					oFileStream.Write(oNewByteArray, 0, oNewByteArray.Length)
				End Using
			End Using

			Return sFilenameRandom.Trim
		Catch ex As Exception
			Log(Debug_GetCurrentFunctionName(), ex.Message)
		End Try

		Return String.Empty
	End Function
	
	Friend Function GetNewGUID() As String
		Return System.Guid.NewGuid().ToString()
	End Function

	Friend Function GetNewGUIDNoDash() As String
		Dim sGUID As String  = System.Guid.NewGuid().ToString()
		Return Replace(sGUID, "-", String.Empty)
	End Function

	Friend Function GetNewShortGUID() As String
		Return DateTime.Now.Ticks.ToString("x")
	End Function

	Friend Function GetAppSetting(ByVal sSetting As String) As String
		Try
			Return System.Configuration.ConfigurationManager.AppSettings(sSetting).ToString
		Catch ex As Exception
			Return String.Empty
		End Try
	End Function

	Friend Function VSpan(ByVal sString As String) As String
		Dim sSpan As String = Replace(sString, "&nbsp;", "")
		If sSpan Is Nothing Then Return String.Empty

		sSpan = System.Web.HttpUtility.HtmlDecode(sSpan)

		Return sSpan
	End Function

	Friend Function VString(ByVal sString As String) As String
		If sString Is Nothing Then
			Return String.Empty
		End If
		Return sString.Trim
	End Function

	Friend Function VInteger(ByVal sString As String) As Integer
		Dim dValue As Double = Val(sString)
		Dim iValue As Integer = CInt(dValue)
		Return iValue
	End Function

	Friend Function VBoolean(ByVal bValue As Boolean) As Integer
		If bValue = True Then
			Return 1
		Else
			Return 0
		End If
	End Function

	Friend Function VBitWise(ByVal lSum As Long, Byval lValue As Long) As Boolean
		If (lValue Or lSum)  = lSum Then
			Return True
		Else 
			Return False
		End If
	End Function

	Friend Function VProperCase(ByVal sString As String) As String
		Return StrConv(sString, VbStrConv.ProperCase)
	End Function

	Friend Function ValidateDoubleQuotesDisplay(ByVal sString As String) As String
		Return Replace(sString, """", "&quot;")
	End Function

	Friend Function VJavascript(ByVal sString As String) As String
		Dim sReplace As String = Replace(sString, "'", "\'")
		Return sReplace
	End Function

	Friend Function VSQL(ByVal sString As String) As String
		Return Replace(sString, "'", "''")
	End Function

	Friend Function GetField(ByRef oDataset As Data.DataSet, ByVal iTable As Integer, ByVal iCounter As Integer, ByVal sFieldName As String) As Object
		Try
			Return oDataset.Tables(iTable).Rows(iCounter).Item(sFieldName)
		Catch ex As Exception
			Dim sError As String = (Debug_GetCurrentFunctionName() & " ~ " & ex.Message & " :: " & sFieldName)
			Debug.Print("************* {0}", sError)
			Throw New System.Exception(sError)
		End Try
	End Function

	Friend Function GetDatasetMultiTables(ByVal sSQL As String, ByRef oDataset As Data.DataSet, ByRef oDatabase As cDatabase, Optional ByVal iCommandTimeOut As Integer = 240) As Boolean
		Dim oDatabaseConnection As SqlClient.SqlConnection = oDatabase.Connection
		Dim oDataAdapter As SqlClient.SqlDataAdapter
		Try
			Debug.Print (sSQL)

			oDataAdapter = New SqlClient.SqlDataAdapter(sSQL, oDatabaseConnection)
			oDataAdapter.SelectCommand.CommandTimeout = iCommandTimeOut
			oDataset = New Data.DataSet

			oDataAdapter.Fill(oDataset)

			oDataAdapter.Dispose() : oDataAdapter = Nothing
			Return True
		Catch ex As Exception
			Log(Debug_GetCurrentFunctionName(), ex.Message & " :: " & sSQL)
			Return False
		End Try
	End Function

	Friend Function AppPath() As String
		Return System.AppDomain.CurrentDomain.BaseDirectory()
	End Function

	Friend Function GetDataset(ByVal iTable As Integer, ByVal sSQL As String, ByRef oDataset As Data.DataSet, ByRef oDatabase As cDatabase, Optional ByVal iCommandTimeOut As Integer = 240) As Integer
		Dim oDatabaseConnection As SqlClient.SqlConnection = oDatabase.Connection
		Dim oDataAdapter As SqlClient.SqlDataAdapter
		Dim iMaxRecords As Integer = 0

		Try
			oDataAdapter = New SqlClient.SqlDataAdapter(sSQL, oDatabaseConnection)
			oDataAdapter.SelectCommand.CommandTimeout = iCommandTimeOut
			oDataset = New Data.DataSet
			oDataAdapter.Fill(oDataset)
			oDataAdapter.Dispose() : oDataAdapter = Nothing
			iMaxRecords = oDataset.Tables(iTable).Rows.Count - 1
			If iMaxRecords < 0 Then
				Return -1
			End If
		Catch ex As Exception
			Log(Debug_GetCurrentFunctionName(), ex.Message & " :: " & sSQL)

			Return -1
		End Try
		Return iMaxRecords
	End Function

	Friend Function Debug_GetCurrentFunctionName(Optional ByVal sException As String = "") As String
		Dim oStackFrame As System.Diagnostics.StackFrame = New System.Diagnostics.StackFrame(1, True)
		Dim sString As String = ""
		If sException.Trim.Length = 0 Then
			sString = oStackFrame.GetMethod.ReflectedType.Name & "." & oStackFrame.GetMethod.Name
		Else
			sString = oStackFrame.GetMethod.ReflectedType.Name & "." & oStackFrame.GetMethod.Name & " :: " & sException
		End If
		oStackFrame = Nothing
		Return sString
	End Function
	
	'Friend Function Save_Image(ByRef oFileUpload As FileUpload, ByRef oSystemUser As cSystemUser, ByRef oServer As HttpServerUtility) As String
	'	Const TYPE_IMAGE As String = "image/jpeg"

	'	Dim iMaxSize As Integer = CInt(GetAppSetting("MAXSIZE_UPLOAD_IMAGE"))

	'	Try
	'		If oFileUpload.HasFile = True Then
	'			If oFileUpload.PostedFile.ContentType = TYPE_IMAGE Then
	'				If oFileUpload.PostedFile.ContentLength < iMaxSize Then
	'					Dim sFilename As String = IO.Path.GetFileName(oFileUpload.FileName)
	'					Dim sFileExtension As String = IO.Path.GetExtension(sFilename)

	'					Dim sFilenameRandom As String = Replace(IO.Path.GetRandomFileName(), ".", "")
	'					sFilenameRandom = String.Format("{0}.{1}", sFilenameRandom, sFileExtension)

	'					Dim sFolder As String = String.Format("{0}/{1}", FOLDER_APP_IMAGES, oSystemUser.AspNetUserID)
	'					If IO.Directory.Exists(oServer.MapPath(sFolder)) = False Then
	'						Dim oDirectoryInfo As IO.DirectoryInfo = IO.Directory.CreateDirectory(oServer.MapPath(sFolder))
	'					End If
	'					Dim sFolderFilename As String = String.Format("{0}/{1}", sFolder, sFilenameRandom)
	'					oFileUpload.SaveAs(oServer.MapPath(sFolderFilename))

	'					Return sFilenameRandom.Trim
	'				End If
	'			End If
	'		End If
	'	Catch ex As Exception
	'		Log(Debug_GetCurrentFunctionName(), ex.Message)
	'	End Try

	'	Return String.Empty
	'End Function

	'Friend Function Save_Image(ByRef oInputFile As HtmlInputFile, ByRef oSystemUser As cSystemUser, ByRef oPage As Page) As String
	'	Dim iMaxSize As Integer = CInt(GetAppSetting("MAXSIZE_UPLOAD_IMAGE"))

	'	Try
	'		If oInputFile.PostedFile IsNot Nothing Then
	'			If oInputFile.PostedFile.ContentLength <= 0 Then Return String.Empty

	'			Dim sFilename As String = IO.Path.GetFileName(oInputFile.PostedFile.FileName)

	'			If oInputFile.PostedFile.ContentLength < iMaxSize Then
	'				Dim sFileExtension As String = IO.Path.GetExtension(sFilename)

	'				Dim sFilenameRandom As String = Replace(IO.Path.GetRandomFileName(), ".", "")
	'				sFilenameRandom = String.Format("{0}{1}", sFilenameRandom, sFileExtension)

	'				Dim sFolder As String = String.Format("{0}/{1}", FOLDER_APP_IMAGES, oSystemUser.AspNetUserID)
	'				If IO.Directory.Exists(oPage.Server.MapPath(sFolder)) = False Then
	'					Dim oDirectoryInfo As IO.DirectoryInfo = IO.Directory.CreateDirectory(oPage.Server.MapPath(sFolder))
	'				End If
	'				Dim sFolderFilename As String = String.Format("{0}/{1}", sFolder, sFilenameRandom)
	'				oInputFile.PostedFile.SaveAs(oPage.Server.MapPath(sFolderFilename))

	'				Return sFilenameRandom.Trim
	'			Else
	'				JS_MsgBox(oPage, String.Format("{0} is too large and cannot be uploaded.", sFilename))
	'				Return String.Empty
	'			End If
	'		Else
	'			JS_MsgBox(oPage, String.Format("Oops. Something went wrong with the upload."))
	'		End If

	'	Catch ex As Exception
	'		Log(Debug_GetCurrentFunctionName(), ex.Message)
	'	End Try

	'	Return String.Empty
	'End Function

	Friend Sub JS_MsgBox(ByRef oPage As Page, ByVal sMessage As String)
		Try
			Dim sPopup As String = String.Format("alert('{0}')", sMessage)
			oPage.ClientScript.RegisterClientScriptBlock(GetType(Page), "Alert", sPopup, True)
		Catch ex As Exception
			Log(Debug_GetCurrentFunctionName(), ex.Message)
		End Try
	End Sub

	Friend Function Literal_SelectVerticalMenu(ByRef oPage As Page, ByVal sParent As String, Optional ByVal sChild As String = "") As String
		Try
			Dim sTag_Child As String = "$('#{0}').addClass('activebrand');"
			Dim sTag_Parent As String = "$('#{0}').collapse('show');"

			Dim oJS As New System.Text.StringBuilder

			oJS.Append("<script type=""text/javascript"">")

			If sChild.Trim.Length = 0 Then
				oJS.Append(String.Format(sTag_Child, sParent))
			Else
				oJS.Append(String.Format(sTag_Child, sChild))
				oJS.Append(String.Format(sTag_Parent, sParent))
			End If

			oJS.Append("</script>")

			Dim sJS As String = oJS.ToString

			oJS = Nothing

			Return sJS
		Catch ex As Exception
			Log(Debug_GetCurrentFunctionName(), ex.Message)
		End Try

		Return String.Empty
	End Function

	Friend Sub JS_SelectVerticalMenu(ByRef oPage As Page, ByVal sParent As String, Optional ByVal sChild As String = "")
		Try
			Dim sKey As String = "VertMenuItem"
			Dim sTag_Child As String = "$('#{0}').addClass('activebrand');"
			Dim sTag_Parent As String = "$('#{0}').collapse('show');"

			Dim oJS As New System.Text.StringBuilder
			If sChild.Trim.Length = 0 Then
				oJS.Append(String.Format(sTag_Child, sParent))
			Else
				oJS.Append(String.Format(sTag_Child, sChild))
				oJS.Append(String.Format(sTag_Parent, sParent))
			End If

			Dim sJS As String = oJS.ToString

			Debug.Print(sJS)

			oJS = Nothing

			'ScriptManager.RegisterClientScriptBlock(oPage, GetType(Page), sKey, sJS, True)

			oPage.ClientScript.RegisterClientScriptBlock(GetType(Page), sKey, sJS, True)
		Catch ex As Exception
			Log(Debug_GetCurrentFunctionName(), ex.Message)
		End Try
	End Sub

	Friend Function ReadFromFile(ByVal sFolderFilename As String) As String
		Return System.IO.File.ReadAllText(sFolderFilename)
	End Function

	Friend Sub WriteToFile(ByVal sFolderFilename As String, ByVal sMessage As String)
		Const DEFAULT_FORMAT_DATE_DAY_TIME As String = "ddd dd-MMM-yyyy hh:mm:ss"

		Try
			Dim oWrite As System.IO.StreamWriter : oWrite = System.IO.File.AppendText(sFolderFilename)

			Dim sTimeStamp As String = Format$(CType(Now, DateTime), DEFAULT_FORMAT_DATE_DAY_TIME).ToUpper
			Dim sFullMessage As String = sTimeStamp & ": " & sMessage

			oWrite.WriteLine(sFullMessage)
			oWrite.Close()
			oWrite = Nothing
		Catch ex As Exception
			Debug.Print(ex.Message)
		End Try
	End Sub

	Friend Function ValidateDate(ByVal dDateTime As DateTime, ByVal sFormat As String) As String
		Dim sFormattedDate As String = String.Empty
		Try
			sFormattedDate = Format(dDateTime, sFormat)
		Catch ex As Exception
			sFormattedDate = String.Empty
		End Try
		Return sFormattedDate.Trim
	End Function

	Friend Function ValidateDate(ByVal sString As String, ByVal sFormat As String) As String
		Dim sFormattedDate As String = String.Empty
		sString = sString.Trim
		Try
			If IsDate(sString) = True Then
				sFormattedDate = Format(CDate(sString), sFormat)
			End If
		Catch ex As Exception
			sFormattedDate = String.Empty
		End Try
		Return sFormattedDate.Trim
	End Function

	Friend Function ValidateDate_Blank1900JAN01(ByVal dDate As DateTime, ByVal sFormat As String) As String
		Dim sFormattedDate As String = Format(dDate, sFormat)
		Try
			If IsDate(sFormattedDate) = True Then
				If DateDiff(DateInterval.Day, CDate(sFormattedDate), CDate(DEFAULT_NULL_DATE)) = 0 Then
					sFormattedDate = String.Empty
				Else
					sFormattedDate = Format(CDate(sFormattedDate), sFormat)
				End If
			End If
		Catch ex As Exception
			sFormattedDate = String.Empty
		End Try
		Return sFormattedDate.Trim
	End Function

	Friend Function ValidateDate_Blank1900JAN01SetTBA(ByVal sString As String, ByVal sFormat As String) As String
		Dim sFormattedDate As String = String.Empty
		sString = sString.Trim
		Try
			If IsDate(sString) = True Then
				If DateDiff(DateInterval.Day, CDate(sString), CDate(DEFAULT_NULL_DATE)) = 0 Then
					sFormattedDate = TAG_TBA
				Else
					sFormattedDate = Format(CDate(sString), sFormat)
				End If
			End If
		Catch ex As Exception
			sFormattedDate = TAG_TBA
		End Try
		Return sFormattedDate.Trim
	End Function


	Friend Function ValidateDate_Blank1900JAN01(ByVal sString As String, ByVal sFormat As String) As String
		Dim sFormattedDate As String = String.Empty
		sString = sString.Trim
		Try
			If IsDate(sString) = True Then
				If DateDiff(DateInterval.Day, CDate(sString), CDate(DEFAULT_NULL_DATE)) = 0 Then
					sFormattedDate = String.Empty
				Else
					sFormattedDate = Format(CDate(sString), sFormat)
				End If
			End If
		Catch ex As Exception
			sFormattedDate = String.Empty
		End Try
		Return sFormattedDate.Trim
	End Function

	Friend Function VDate_ShortDescriptive(ByVal sString As String) As String
		'Format of: 21st Mar 2021
		Try
			Dim sDay As String = GetDate_DaySuffix(CDate(sString).Day.ToString)
			Dim sMonth As String = CDate(sString).ToString("MMM")
			Dim sYear As String = CDate(sString).ToString("yyyy")

			Dim sDate As String = String.Format("{0} {1} {2}", sDay, sMonth, sYear)
			Return sDate
		Catch ex As Exception
			Log(Debug_GetCurrentFunctionName, ex.Message)
			Return sString
		End Try
	End Function

	Friend Function VDate_LongDescriptive(ByVal sString As String) As String
		'Format of: 21st March 2021
		Try
			Dim sDay As String = GetDate_DaySuffix(CDate(sString).Day.ToString)
			Dim sMonth As String = CDate(sString).ToString("MMMM")
			Dim sYear As String = CDate(sString).ToString("yyyy")

			Dim sDate As String = String.Format("{0} {1} {2}", sDay, sMonth, sYear)
			Return sDate
		Catch ex As Exception
			Log(Debug_GetCurrentFunctionName, ex.Message)
			Return sString
		End Try
	End Function

	Friend Function ValidateDate_BlankSet1900JAN01(ByVal sString As String, ByVal sFormat As String) As String
		Dim sFormattedDate As String = String.Empty
		sString = sString.Trim
		Try
			If sString.ToUpper = "NOW" Or sString.ToUpper = "TODAY" Then
				sFormattedDate = Format(Now, sFormat)
				Return sFormattedDate.Trim
			End If

			If IsDate(sString) = True Then
				If DateDiff(DateInterval.Day, CDate(sString), CDate(DEFAULT_NULL_DATE)) = 0 Then
					sFormattedDate = DEFAULT_NULL_DATE
				Else
					sFormattedDate = Format(CDate(sString), sFormat)
				End If
			End If
		Catch ex As Exception
			sFormattedDate = DEFAULT_NULL_DATE
		End Try
		Return sFormattedDate.Trim
	End Function

	Friend Function DisplayNumerals(ByVal dValue As Decimal, Optional ByVal sDecimalFormat As String = FORMAT_NUMERAL_DECIMALPOINTS) As String
		Return Format(dValue, "###,###,###,##0." & sDecimalFormat)
	End Function

	Friend Function DisplayIntegers(ByVal iValue As Integer) As String
		Return Format(iValue, "###,###,###,##0")
	End Function

	Friend Function GetDate_DaySuffix(ByVal sDay As String) As String
		If sDay.EndsWith("1") And sDay <> "11" Then
			sDay &= "st"
		ElseIf sDay.EndsWith("2") And sDay <> "12" Then
			sDay &= "nd"
		ElseIf sDay.EndsWith("3") And sDay <> "13" Then
			sDay &= "rd"
		Else
			sDay &= "th"
		End If

		Return sDay
	End Function

	Friend Function VDoubleSpacesOrMore(ByVal sString As String) As String
		Return Regex.Replace(sString, "\s{2,}", " ")
	End Function
End Module
