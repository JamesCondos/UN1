Imports System.Web.Hosting

Module mLog
    Friend Sub Log(Byval sMessage As String)
        Debug.Print(sMessage)
    End Sub

    Friend Sub Log(Byval sSourceModule As String, Byval sMessage As String)
        Const FILENAME_LOG As String = "SystemLog.txt"

        Dim sString As String = String.Format("{0} = {1}", sSourceModule, sMessage)
        Debug.Print (sString)

        Dim sFolderFilename As String = String.Format("{0}/{1}", AppPath(), FILENAME_LOG)
        sFolderFilename = HostingEnvironment.MapPath(String.Format("{0}/{1}",  FOLDER_APP_DATA, FILENAME_LOG))

        If IO.File.Exists(sFolderFilename) = False Then		
            Dim oStreamWriter As IO.StreamWriter =  IO.File.CreateText(sFolderFilename)
			oStreamWriter.Close: oStreamWriter = Nothing
        End If

        WriteToFile(sFolderFilename, sString)
	End Sub
End Module
