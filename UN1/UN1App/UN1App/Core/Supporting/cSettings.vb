Imports Plugin.Settings
Imports Plugin.Settings.Abstractions

Namespace UN1.Helpers
	Public Class cSettings
		Private Const SettingsKey As String = "Settings_Key"
		Private Shared ReadOnly SettingsDefault As String = String.Empty

		Public Shared ReadOnly Property AppSettings As ISettings
			Get
				Return CrossSettings.Current
			End Get
		End Property

			Public Shared Property GeneralSettings As String
				Get
					Return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault)
				End Get
			    Set(value As String)
					AppSettings.AddOrUpdateValue(SettingsKey, value)	
			    End Set
			End Property
	End Class
End Namespace