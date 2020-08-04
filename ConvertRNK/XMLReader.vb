Imports System.IO
Imports System.Xml

Public Class XMLReader
    Private configparam As Hashtable
    Private filename As String = String.Empty

    Private Function VerifyConfigParameterFile() As Boolean
        Dim filename As String = "config.xml"
        Dim fileConfigParam As System.IO.FileInfo = New FileInfo(Path.Combine(Directory.GetCurrentDirectory(), filename))

        If fileConfigParam.Exists Then

            Try
                Me.filename = Path.Combine(Directory.GetCurrentDirectory(), filename)
                Return True
            Catch ex As Exception
                Return False
            End Try
        Else
            Dim doc As XmlDocument = New XmlDocument()
            Dim xmlDeclaration As XmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", Nothing)
            Dim root As XmlElement = doc.DocumentElement
            doc.InsertBefore(xmlDeclaration, root)
            Dim element As XmlElement = doc.CreateElement(String.Empty, "CONFIGPARAM", doc.NamespaceURI)
            element.InnerText = String.Empty
            doc.AppendChild(element)
            Me.filename = fileConfigParam.FullName
            doc.Save(Me.filename)
            Return True
        End If
    End Function

    Public Function ReadConfigParameter() As Hashtable
        Try

            If Not VerifyConfigParameterFile() Then
                Return Nothing
            End If

            configparam = New Hashtable()
            Dim reader As XmlTextReader = New XmlTextReader(Me.filename)
            Dim key As String = String.Empty
            Dim value As String = String.Empty

            While reader.Read()

                Select Case reader.NodeType
                    Case XmlNodeType.Element
                        key = reader.Name
                    Case XmlNodeType.Text
                        value = reader.Value
                    Case XmlNodeType.EndElement

                        If Not String.IsNullOrEmpty(key.Trim()) Then
                            configparam.Add(key, value)
                            key = String.Empty
                            value = String.Empty
                        End If
                End Select
            End While

            reader.Close()
            Return configparam
        Catch ex As Exception
            MessageBox.Show(String.Format(Constants.XMLCannotRead, filename), "エラー", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            Return Nothing
        End Try
    End Function

    Public Function getValue(Of T)(ByVal Key As String, ByVal defaultValue As T) As T
        If configparam Is Nothing Then
            Return defaultValue
        End If

        Dim value = If(configparam.ContainsKey(Key), configparam(Key).ToString(), String.Empty)

        If String.IsNullOrEmpty(value) Then
            Return defaultValue
        End If

        Return CType(Convert.ChangeType(value, GetType(T)), T)
    End Function

    Public Function UpdateConfigParameter(ByVal param As Hashtable) As Boolean
        Try
            Dim xml As XmlDocument = New XmlDocument()
            xml.Load(Me.filename)

            For Each element As XmlNode In xml.SelectNodes("/CONFIGPARAM")

                For Each element1 As XmlNode In element.ChildNodes

                    If param.ContainsKey(element1.Name) Then
                        element1.InnerText = param(element1.Name).ToString()
                        param.Remove(element1.Name)
                    End If
                Next
            Next

            If param.Keys.Count > 0 Then

                For Each entry As DictionaryEntry In param
                    Dim newnode As XmlNode = xml.CreateNode(XmlNodeType.Element, entry.Key.ToString(), xml.NamespaceURI)
                    newnode.InnerText = entry.Value.ToString()
                    xml.SelectSingleNode("/CONFIGPARAM").AppendChild(newnode)
                Next
            End If

            xml.Save(Me.filename)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
