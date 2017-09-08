Imports System
Imports System.IO
Imports System.Xml
Imports System.Xml.XPath

Module GTAXMLReader
    ' Private Functions List..... This is were the magic happens =P
    Private Function check_xml_entry(ByVal xml_filename As String, ByVal xml_path As String, ByVal value_name As String) As String
        Dim return_value As String
        Try
            Dim xd As New XmlDocument()
            xd.Load(xml_filename)
            ' Find the node where the Person's attribute ID is 1 using its XPath.
            Dim nod As XmlNode = xd.SelectSingleNode(xml_path)
            If nod IsNot Nothing Then
                return_value = "True"
            Else
                return_value = "False"
            End If
            xd.Save(xml_filename)
        Catch ex As Exception
            return_value = ex.Message
        End Try
        Return return_value
    End Function
    Private Function Check_Att(ByVal xml_filename As String, ByVal xpath As String, ByVal value_name As String, ByVal att_name As String) As String
        Dim return_value As String
        Try
            Dim xd As New XmlDocument
            xd.Load(xml_filename)
            Dim nod As XmlNode = xd.SelectSingleNode(xpath & "/" & value_name & "[@" & att_name & "]")
            If nod IsNot Nothing Then
                return_value = "True"
            Else
                return_value = "False"
            End If
        Catch ex As Exception
            return_value = ex.Message
        End Try
        Return return_value
    End Function
    Private Function Out_xml_from_xml_path(ByVal xml_path As String, ByVal value_name As String, ByVal value As String, ByVal att_name As String, ByVal att_value As String) As String
        Dim return_value As String
        Dim a, b, c, d As String
        Dim x, y, z As Integer
        Dim master As String
        Dim buffer As String
        If String.IsNullOrEmpty(att_name) = False Then
            master = "<" & value_name & " " & att_name & "=" & Chr(34) & att_value & Chr(34) & ">" & value & "</" & value_name & ">"
        Else
            master = "<" & value_name & ">" & value & "</" & value_name & ">"
        End If
        a = xml_path.Trim("/")
        x = a.IndexOf("/")
        If x < 1 Then ' Is Root
            return_value = master
            GoTo 1
        End If
        b = a.Remove(0, x + 1)
        d = b
        Do
            x = d.LastIndexOf("/")
            If x < 1 Then ' Is Last Key
                master = "<" & d & ">" & master & "</" & d & ">"
                return_value = master
                Exit Do
            End If
            b = d.Remove(0, x + 1) ' thats without /
            c = d.Remove(0, x) ' thats with /
            master = "<" & b & ">" & master & "</" & b & ">"
            a = d.Replace(c, "")
            d = a
        Loop
1:
        Return master
    End Function
    Private Function Create_New_XML(ByVal xml_filename As String, ByVal xml_path As String, ByVal value_name As String, ByVal value As String, ByVal att_name As String, ByVal att_value As String) As String
        Dim return_value As String
        Try
            Dim settings As New XmlWriterSettings()
            settings.Indent = True
            settings.Encoding = System.Text.Encoding.UTF8
            Dim a, b, c, d As String
            Dim XmlWrt As XmlWriter = XmlWriter.Create(xml_filename, settings)
            With XmlWrt
                .WriteStartDocument()
                .WriteComment("XML Document Constructed on " & DateTime.Now.Date & "/" & DateTime.Now.Month & "/" & DateTime.Now.Year)
                .WriteComment("Basic XML File. Create with Code from Dool Cookies")
                .WriteComment("From www.CodeProject.com")
                a = xml_path.Trim("/")
                b = a & "/" & value_name
                For Each t As String In b.Split("/")
                    .WriteStartElement(t)
                Next
                If String.IsNullOrEmpty(att_name) = False Then
                    .WriteAttributeString(att_name, att_value)
                End If
                .WriteString(value)
                .WriteFullEndElement()
                .WriteEndDocument()
                .Close()
                return_value = True
            End With
        Catch ex As Exception
            return_value = ex.Message
        End Try
        Return return_value
    End Function
    Private Function add_to_xml(ByVal xml_filename As String, ByVal xml_path As String, ByVal value_name As String, ByVal value As String) As String
        Dim return_value As String
        Try
            Dim cr As String = Environment.NewLine
            Dim dool As String
            dool = Out_xml_from_xml_path(xml_path, value_name, value, Nothing, Nothing)
            Dim xd As New XmlDocument()
            xd.Load(xml_filename)
            Dim docFrag As XmlDocumentFragment = xd.CreateDocumentFragment()
            docFrag.InnerXml = dool
            Dim root As XmlNode = xd.DocumentElement
            root.AppendChild(docFrag)
            xd.Save(xml_filename)
            return_value = "True"
        Catch ex As Exception
            return_value = ex.Message
        End Try
        Return return_value
    End Function
    Private Function Edit_XML_Entry(ByVal xml_filename As String, ByVal xml_path As String, ByVal Value_Name As String, ByVal Value As String) As String
        Dim return_value As String
        Dim xd As New XmlDocument()
        xd.Load(xml_filename)
        Dim nod As XmlNode = xd.SelectSingleNode(xml_path & "/" & Value_Name)
        If nod IsNot Nothing Then
            nod.InnerXml = Value
            return_value = "True"
        Else
            return_value = "Dool_Cookies"
        End If
        xd.Save(xml_filename)
        Return return_value
    End Function
    Private Function add_xml_att(ByVal xml_filename As String, ByVal xml_path As String, ByVal value_name As String, ByVal att_name As String, ByVal att_value As String) As String
        Dim return_value As String
        Try
            Dim document As New Xml.XmlDocument
            document.Load(xml_filename)
            Dim nav As Xml.XPath.XPathNavigator = document.CreateNavigator
            nav = nav.SelectSingleNode(xml_path & "/" & value_name)
            nav.CreateAttribute(Nothing, att_name, Nothing, att_value)
            document.Save(xml_filename)
            return_value = "True"
        Catch ex As Exception
            return_value = ex.Message
        End Try
        Return return_value
    End Function
    Private Function update_att(ByVal xml_filename As String, ByVal xml_path As String, ByVal value_name As String, ByVal att_name As String, ByVal att_value As String) As String
        Dim return_value As String
        Dim xd As New XmlDocument()
        xd.Load(xml_filename)
        Dim nod As XmlNode = xd.SelectSingleNode(xml_path & "/" & value_name & "[@" & att_name & "]")
        If nod IsNot Nothing Then
            nod.Attributes.GetNamedItem(att_name).Value = att_value
            return_value = "True"
        Else
            MsgBox("Opps")
        End If
        xd.Save(xml_filename)
        Return return_value
    End Function
    Private Function Get_ATT(ByVal xml_Filename As String, ByVal xml_path As String, ByVal value_name As String, ByVal att_name As String) As String
        Dim return_value As String
        Try
            Dim a As String
            Dim xd As New XmlDocument
            xd.Load(xml_Filename)
            Dim nod As XmlNode = xd.SelectSingleNode(xml_path & "/" & value_name & "[@" & att_name & "]")
            If nod IsNot Nothing Then
                a = nod.Attributes.GetNamedItem(att_name).Value
                return_value = a
            Else
                return_value = Nothing
            End If
            xd.Save(xml_Filename)
        Catch ex As Exception
            return_value = ex.Message
        End Try
        Return return_value
    End Function
    Private Function Get_Val(ByVal xml_filame As String, ByVal xml_path As String, ByVal value_name As String) As String
        Dim return_value As String
        Try
            Dim a As String
            Dim xd As New XmlDocument
            xd.Load(xml_filame)
            Dim nod As XmlNode = xd.SelectSingleNode(xml_path & "/" & value_name)
            If nod IsNot Nothing Then
                a = nod.InnerXml
                return_value = a
            Else
                return_value = Nothing
            End If
            xd.Save(xml_filame)
        Catch ex As Exception
            return_value = ex.Message
        End Try
        Return return_value

    End Function
    Private Function delete_Element(ByVal xml_filename As String, ByVal xml_path As String, ByVal value_name As String) As String
        Dim return_value As String
        Try
            Dim document As New Xml.XmlDocument
            document.Load(xml_filename)
            Dim nav As Xml.XPath.XPathNavigator = document.CreateNavigator
            nav = nav.SelectSingleNode(xml_path & "/" & value_name)
            nav.DeleteSelf()
            document.Save(xml_filename)
            return_value = "True"
        Catch ex As Exception
            return_value = ex.Message
        End Try
        Return return_value
    End Function
    Private Function delete_tree(ByVal xml_filename As String, ByVal xml_path As String) As String
        Dim return_value As String
        Try
            Dim document As New Xml.XmlDocument
            document.Load(xml_filename)
            Dim nav As Xml.XPath.XPathNavigator = document.CreateNavigator
            nav = nav.SelectSingleNode(xml_path)
            nav.DeleteSelf()
            document.Save(xml_filename)
            return_value = "True"
        Catch ex As Exception
            return_value = ex.Message
        End Try
        Return return_value
    End Function
    Private Function create_tree(ByVal xml_filename As String, ByVal start_at As String, ByVal add_these As String) As String
        Dim return_value As String
        Dim a, b, c, d As String
        Try
            Dim document As New Xml.XmlDocument
            document.Load(xml_filename)
            Dim nav As Xml.XPath.XPathNavigator = document.CreateNavigator
            nav = nav.SelectSingleNode(start_at)
            a = add_these.Trim("/")
            b = start_at
            For Each t As String In a.Split("/")
                b = b & "/" & t
                nav.AppendChildElement(Nothing, t, Nothing, "")
                nav = nav.SelectSingleNode(b)
            Next
            document.Save(xml_filename)
            return_value = "True"
        Catch ex As Exception
            return_value = ex.Message
        End Try
        Return return_value
    End Function
    Private Function dool_cookies(ByVal xml_filename As String, ByVal xml_path As String, ByVal value_name As String, ByVal value As String) As String
        Dim return_value As String
        Try
            Dim dool As New XmlDocument
            dool.Load(xml_filename)
            Dim nav As Xml.XPath.XPathNavigator = dool.CreateNavigator
            nav = nav.SelectSingleNode(xml_path)
            nav.AppendChildElement(Nothing, value_name, Nothing, value)
            dool.Save(xml_filename)
            return_value = "True"
        Catch ex As Exception
            return_value = ex.Message
        End Try
        Return return_value
    End Function
    ' Public Functions List........
    Public Function Write_XML_Value(ByVal XML_Filename As String, ByVal XML_Path As String, ByVal Value_Name As String, ByVal Value As String) As String
        Dim return_value As String
        Dim a, b, c, d As String
        If File.Exists(XML_Filename) = False Then
            a = Create_New_XML(XML_Filename, XML_Path, Value_Name, Value, Nothing, Nothing)
            return_value = a
            GoTo 1
        End If
        a = check_xml_entry(XML_Filename, XML_Path, Value_Name) ' Check to see if entry exsists.



        If a.ToLower = "true" Then ' Yes we need to update the value
            b = Edit_XML_Entry(XML_Filename, XML_Path, Value_Name, Value)
            return_value = b
            If b.ToLower = "dool_cookies" Then
                c = dool_cookies(XML_Filename, XML_Path, Value_Name, Value)
                return_value = c
            End If


        Else ' No we need to make a new value
            b = add_to_xml(XML_Filename, XML_Path, Value_Name, Value)
            return_value = b
        End If
1:
        Return return_value
    End Function
    Public Function Write_XML_Attribute(ByVal XML_FileName As String, ByVal XML_Path As String, ByVal Value_Name As String, ByVal Attribute_Name As String, ByVal Attribute_Value As String) As String
        Dim return_value As String
        Dim a, b, c As String
        If File.Exists(XML_FileName) = False Then
            a = Create_New_XML(XML_FileName, XML_Path, Value_Name, Nothing, Attribute_Name, Attribute_Value)
            return_value = a
            GoTo 1
        End If
        a = Check_Att(XML_FileName, XML_Path, Value_Name, Attribute_Name)
        If a.ToLower = "true" Then ' Att does exsist, update
            a = update_att(XML_FileName, XML_Path, Value_Name, Attribute_Name, Attribute_Value)
            return_value = a
        Else ' create new one.
            a = add_xml_att(XML_FileName, XML_Path, Value_Name, Attribute_Name, Attribute_Value)
            return_value = a
        End If
1:
        Return return_value
    End Function
    Public Function Read_XML_Value(ByVal XML_Filename As String, ByVal XML_Path As String, ByVal Value_Name As String) As String
        Dim return_value As String
        Dim a As String
        If File.Exists(XML_Filename) = False Then
            return_value = "File Does Not Exist"
            GoTo 1
        End If
        a = Get_Val(XML_Filename, XML_Path, Value_Name)
        return_value = a
1:
        Return return_value
    End Function
    Public Function Read_XML_Attribute(ByVal XML_Filename As String, ByVal XML_Path As String, ByVal Value_Name As String, ByVal Attribute_Name As String) As String
        Dim return_value As String
        Dim a As String
        If File.Exists(XML_Filename) = False Then
            return_value = "File Does Not Exist"
            GoTo 1
        End If
        a = Get_ATT(XML_Filename, XML_Path, Value_Name, Attribute_Name)
        return_value = a
1:
        Return return_value
    End Function
    Public Function Remove_XML_Entry(ByVal XML_Filename As String, ByVal XML_Path As String, ByVal Value_Name As String) As String
        Dim return_value As String
        Dim a As String
        If File.Exists(XML_Filename) = False Then
            return_value = "File Does Not Exist"
            GoTo 1
        End If
        a = delete_Element(XML_Filename, XML_Path, Value_Name)
        return_value = a
1:
        Return return_value
    End Function
    Public Function Remove_From_Element(ByVal XML_Filename As String, ByVal XML_Path As String) As String
        Dim return_value As String
        Dim a As String
        If File.Exists(XML_Filename) = False Then
            return_value = "File Does Not Exist"
            GoTo 1
        End If
        a = delete_tree(XML_Filename, XML_Path)
        return_value = a
1:
        Return return_value
    End Function
    Public Function Create_XML_Tree(ByVal xml_filename As String, ByVal Create_at_xml_path As String, ByVal Extra_Tree_Elements As String) As String
        Dim return_value As String
        Dim a As String
        If File.Exists(xml_filename) = False Then
            return_value = "File Does Not Exist"
            GoTo 1
        End If
        a = create_tree(xml_filename, Create_at_xml_path, Extra_Tree_Elements)
        return_value = a
1:
        Return return_value
    End Function

End Module