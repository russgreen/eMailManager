'***************************************************************************
'
'         Copyright (c) Microsoft Corporation. All rights reserved.
'
'    This code sample is provided "AS IS" without warranty of any kind.
'
'***************************************************************************
Imports System.Reflection

'Imports Outlook = Microsoft.Office.Interop.Outlook

''' <summary>
''' An internal static helper class that provides access to
''' common Outlook item properties so that mail, appointment
''' and task items can be treated polymorphically.
''' </summary>
Friend NotInheritable Class OutlookItem
	Private Sub New()
	End Sub
	''' <summary>
	''' Returns the Subject of the given Outlook item.
	''' </summary>
	''' <param name="item">Outlook item</param>
	Friend Shared Function GetSubject(item As Object) As String
		' Use reflection to retrieve the item's Subject property.
		Dim subject As String = TryCast(item.[GetType]().InvokeMember("Subject", BindingFlags.[Public] Or BindingFlags.GetField Or BindingFlags.GetProperty, Nothing, item, New Object() {}, System.Globalization.CultureInfo.InvariantCulture), String)

		Return subject
    End Function

	''' <summary>
	''' Sets the FilterMatch user property on the given Outlook item.
	''' </summary>
	''' <param name="item">Outlook item</param>
	''' <param name="value">User property value</param>
	Friend Shared Sub SetFilterMatchProperty(item As Object, value As String)
		Dim prop As Outlook.UserProperty = OutlookItem.GetUserProperty(item, "FilterMatch")
		prop.Value = value
		OutlookItem.Save(item)
	End Sub

    ''' <summary>
    ''' Returns the specified user property object from the given Outlook item.
    ''' If the user property is not found within the item's UserProperty collection,
    ''' then it will be added.
    ''' </summary>
    ''' <param name="item">Outlook item</param>
    ''' <param name="propName">User Property name</param>
    ''' <returns>UserProperty object</returns>
	Private Shared Function GetUserProperty(item As Object, propName As String) As Outlook.UserProperty
		Dim prop As Outlook.UserProperty = Nothing

		' Use reflection to retrieve the item's User Property.
		prop = TryCast(item.[GetType]().InvokeMember("UserProperties", BindingFlags.[Public] Or BindingFlags.GetField Or BindingFlags.GetProperty, Nothing, item, New Object() {propName}, System.Globalization.CultureInfo.InvariantCulture), Outlook.UserProperty)

		' UserProperty not found so add it.
		If prop Is Nothing Then
			Dim userProps As Outlook.UserProperties = TryCast(item.[GetType]().InvokeMember("UserProperties", BindingFlags.[Public] Or BindingFlags.GetField Or BindingFlags.GetProperty, Nothing, item, New Object() {}, System.Globalization.CultureInfo.InvariantCulture), Outlook.UserProperties)

			prop = TryCast(item.[GetType]().InvokeMember("Add", BindingFlags.[Public] Or BindingFlags.InvokeMethod, Nothing, userProps, New Object() {propName, Outlook.OlUserPropertyType.olText, True, Outlook.OlUserPropertyType.olText}, System.Globalization.CultureInfo.InvariantCulture), Outlook.UserProperty)
		End If

		Return prop
	End Function

	''' <summary>
	''' Invokes the save method on the given Outlook item.
	''' </summary>
	''' <param name="item">Outlook item</param>
	Private Shared Sub Save(item As Object)
		' Use reflection to invoke the Save method.
		item.[GetType]().InvokeMember("Save", BindingFlags.[Public] Or BindingFlags.InvokeMethod, Nothing, item, New Object() {}, System.Globalization.CultureInfo.InvariantCulture)
	End Sub
End Class

