Imports System.Drawing
Imports System.Windows.Forms
Imports GTA
Imports GTA.Math
Imports GTA.Native

Public Class Resources

    Public Shared PlayerHash, PlayerName As String
    Public Shared VC_MOTORCYCLE, VC_COMPACT, VC_COUPE, VC_SEDAN, VC_SPORT, VC_CLASSIC, VC_SUPER, VC_MUSCLE, VC_OFF_ROAD, VC_SUV, VC_VAN As String
    Public Shared VC_INDUSTRIAL, VC_BICYCLE, VC_BOAT, VC_HELI, VC_PLANE, VC_SERVICE, VC_EMERGENCY, VC_MILITARY, VC_COMMERCIAL, VC_UTILITY As String

    Public Sub New()
        PlayerHash = Game.Player.Character.Model.Hash.ToString
        If PlayerHash = "225514697" Then
            PlayerName = "Michael"
        ElseIf PlayerHash = "-1692214353" Then
            PlayerName = "Franklin"
        ElseIf PlayerHash = "-1686040670" Then
            PlayerName = "Trevor"
        End If
    End Sub

    Public Shared Sub DisplayHelpTextThisFrame(ByVal [text] As String)
        Dim arguments As InputArgument() = New InputArgument() {"STRING"}
        Native.Function.Call(Hash._0x8509B634FBE7DA11, arguments)
        Dim argumentArray2 As InputArgument() = New InputArgument() {[text]}
        Native.Function.Call(Hash._0x6C188BE134E074AA, argumentArray2)
        Dim argumentArray3 As InputArgument() = New InputArgument() {0, 0, 1, -1}
        Native.Function.Call(Hash._0x238FFE5C7B0498A6, argumentArray3)
    End Sub

    Public Shared Sub SetBlipName(ByVal BlipString As String, ByVal BlipName As Blip)
        Dim arguments As InputArgument() = New InputArgument() {"STRING"}
        Native.Function.Call(Hash._0xF9113A30DE5C6670, arguments)
        Native.Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, BlipString)
        Native.Function.Call(Hash._0xBC38B49BCB83BC9B, BlipName)
    End Sub

    Public Enum IconType
        ChatBox = 1
        Email = 2
        AddFriendRequest = 3
        Nothing4 = 4
        Nothing5 = 5
        Nothing6 = 6
        RightJumpingArrow = 7
        RPIcon = 8
        DollarSignIcon = 9
    End Enum

    ''' <summary>
    ''' CHAR_DEFAULT : Default profile pic
    ''' CHAR_FACEBOOK: Facebook
    ''' CHAR_SOCIAL_CLUB: Social Club Star
    ''' CHAR_CARSITE2: Super Auto San Andreas Car Site
    ''' CHAR_BOATSITE: Boat Site Anchor
    ''' CHAR_BANK_MAZE: Maze Bank Logo
    ''' CHAR_BANK_FLEECA: Fleeca Bank
    ''' CHAR_BANK_BOL: Bank Bell Icon
    ''' CHAR_MINOTAUR: Minotaur Icon
    ''' CHAR_EPSILON: Epsilon E
    ''' CHAR_MILSITE: Warstock W
    ''' CHAR_CARSITE: Legendary Motorsports Icon
    ''' CHAR_DR_FRIEDLANDER: Dr Freidlander Face
    ''' CHAR_BIKESITE: P&M Logo
    ''' CHAR_LIFEINVADER: Liveinvader
    ''' CHAR_PLANESITE: Plane Site E
    ''' CHAR_MICHAEL: Michael's Face
    ''' CHAR_FRANKLIN: Franklin's Face
    ''' CHAR_TREVOR: Trevor's Face
    ''' CHAR_SIMEON: Simeon's Face
    ''' CHAR_RON: Ron's Face
    ''' CHAR_JIMMY: Jimmy's Face
    ''' CHAR_LESTER: Lester's Shadowed Face
    ''' CHAR_DAVE: Dave Norton 's Face
    ''' CHAR_LAMAR: Chop's Face
    ''' CHAR_DEVIN: Devin Weston 's Face
    ''' CHAR_AMANDA: Amanda's Face
    ''' CHAR_TRACEY: Tracey's Face
    ''' CHAR_STRETCH: Stretch's Face
    ''' CHAR_WADE: Wade's Face
    ''' CHAR_MARTIN: Martin Madrazo 's Face
    ''' CHAR_ACTING_UP: Acting Icon
    ''' </summary>
    Public Shared Sub DisplayNotificationThisFrame(Sender As String, Subject As String, Message As String, Icon As String, Flash As Boolean, Type As IconType)
        Native.Function.Call(Hash._SET_NOTIFICATION_TEXT_ENTRY, "STRING")
        Native.Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, Message)
        Native.Function.Call(Hash._SET_NOTIFICATION_MESSAGE, Icon, Icon, Flash, Type, Sender, Subject)
        Native.Function.Call(Hash._DRAW_NOTIFICATION, False, True)
    End Sub

    Public Enum ModType
        Spoiler = 0
        FBumper = 1
        RBumper = 2
        SSkirt = 3
        Exhaust = 4
        Frame = 5
        Grille = 6
        Hood = 7
        Fender = 8
        RFender = 9
        Roof = 10
        Engine = 11
        Brakes = 12
        Transmission = 13
        Horns = 14
        Suspension = 15
        Armor = 16
        FWheels = 23
        BWheels = 24
        PlateHolder = 25
        TrimDesign = 27
        Ornaments = 28
        DialDesign = 30
        Steering = 33
        Shifter = 34
        Plaques = 35
        Hydraulics = 38
    End Enum

    Public Enum GTAFont
        ' Fields
        Pricedown = 7
        Script = 1
        Symbols = 3
        Symbols2 = 5
        Title = 4
        Title2 = 6
        TitleWSymbols = 2
        UIDefault = 0
    End Enum

    Public Enum GTAFontAlign
        ' Fields
        Center = 1
        Left = 0
        Right = 2
    End Enum

    Public Enum GTAFontStyleOptions
        ' Fields
        DropShadow = 1
        None = 0
        Outline = 2
    End Enum

    Public Shared Sub DrawText(ByVal [Text] As String, ByVal Position As PointF, ByVal Scale As Single, ByVal color As Color, ByVal Font As GTAFont, ByVal Alignment As GTAFontAlign, ByVal Options As GTAFontStyleOptions)
        Dim arguments As InputArgument() = New InputArgument() {Font}
        Native.Function.Call(Hash._0x66E0276CC5F6B9DA, arguments)
        Dim argumentArray2 As InputArgument() = New InputArgument() {1.0!, Scale}
        Native.Function.Call(Hash._0x07C837F9A01C34C9, argumentArray2)
        Dim argumentArray3 As InputArgument() = New InputArgument() {color.R, color.G, color.B, color.A}
        Native.Function.Call(Hash._0xBE6B23FFA53FB442, argumentArray3)
        If Options.HasFlag(GTAFontStyleOptions.DropShadow) Then
            Native.Function.Call(Hash._0x1CA3E9EAC9D93E5E, New InputArgument(0 - 1) {})
        End If
        If Options.HasFlag(GTAFontStyleOptions.Outline) Then
            Native.Function.Call(Hash._0x2513DFB0FB8400FE, New InputArgument(0 - 1) {})
        End If
        If Alignment.HasFlag(GTAFontAlign.Center) Then
            Dim argumentArray4 As InputArgument() = New InputArgument() {1}
            Native.Function.Call(Hash._0xC02F4DBFB51D988B, argumentArray4)
        ElseIf Alignment.HasFlag(GTAFontAlign.Right) Then
            Dim argumentArray5 As InputArgument() = New InputArgument() {1}
            Native.Function.Call(Hash._0x6B3C4650BC8BEE47, argumentArray5)
        End If
        Dim argumentArray6 As InputArgument() = New InputArgument() {"jamyfafi"}
        Native.Function.Call(Hash._0x25FBB336DF1804CB, argumentArray6)
        PushBigString([Text])
        Dim argumentArray7 As InputArgument() = New InputArgument() {(Position.X / 1280.0!), (Position.Y / 720.0!)}
        Native.Function.Call(Hash._0xCD015E5BB0D96A57, argumentArray7)
    End Sub

    Public Shared Sub PushBigString(ByVal [Text] As String)
        Dim strArray As String() = SplitStringEveryNth([Text], &H63)
        Dim i As Integer
        For i = 0 To strArray.Length - 1
            Dim arguments As InputArgument() = New InputArgument() {[Text].Substring((i * &H63), strArray(i).Length)}
            Native.Function.Call(Hash._0x6C188BE134E074AA, arguments)
        Next i
    End Sub

    Private Shared Function SplitStringEveryNth(ByVal [text] As String, ByVal Nth As Integer) As String()
        Dim list As New List(Of String)
        Dim item As String = ""
        Dim num As Integer = 0
        Dim i As Integer
        For i = 0 To [text].Length - 1
            item = (item & [text].Chars(i).ToString)
            num += 1
            If ((i <> 0) AndAlso (num = Nth)) Then
                list.Add(item)
                item = ""
                num = 0
            End If
        Next i
        If (item <> "") Then
            list.Add(item)
        End If
        Return list.ToArray
    End Function

    Public Shared Function GetLangEntry(Lang As String) As String
        Dim result As String = ReadCfgValue(Lang, Application.StartupPath & "\scripts\PremiumDeluxeMotorsport\Languages\" & Game.Language.ToString & ".cfg")
        Dim real_result As String
        If result = Nothing Then
            real_result = "NULL"
        Else
            real_result = result
        End If
        Return real_result
    End Function

    Public Shared Function CreateVehicle(VehicleModel As String, Position As Vector3, Optional Heading As Single = 0) As Vehicle
        Dim Result As Vehicle = Nothing
        Dim model = New Model(VehicleModel)
        model.Request(250)
        If model.IsInCdImage AndAlso model.IsValid Then
            While Not model.IsLoaded
                Script.Wait(50)
            End While
            Result = World.CreateVehicle(model, Position, Heading)
        End If
        model.MarkAsNoLongerNeeded()
        Return Result
    End Function

    Public Shared Function CreateVehicleFromHash(VehicleHash As Integer, Position As Vector3, Optional Heading As Single = 0) As Vehicle
        Dim Result As Vehicle = Nothing
        Dim model = New Model(VehicleHash)
        model.Request(250)
        If model.IsInCdImage AndAlso model.IsValid Then
            While Not model.IsLoaded
                Script.Wait(50)
            End While
            Result = World.CreateVehicle(model, Position, Heading)
        End If
        model.MarkAsNoLongerNeeded()
        Return Result
    End Function

    Public Shared Function CreateProp(PropModel As Integer, Position As Vector3, Rotation As Vector3) As Prop
        Dim Result As Prop = Nothing
        Dim model = New Model(PropModel)
        model.Request(250)
        If model.IsInCdImage AndAlso model.IsValid Then
            While Not model.IsLoaded
                Script.Wait(50)
            End While
            Result = World.CreateProp(model, Position, Rotation, False, False)
        End If
        model.MarkAsNoLongerNeeded()
        Return Result
    End Function

    Public Shared Sub ToggleIPL(iplToEnable As String, iplToDisable As String)
        If Native.Function.Call(Of Boolean)(Hash.IS_IPL_ACTIVE, New InputArgument() {iplToDisable}) Then
            Native.Function.Call(Hash.REMOVE_IPL, New InputArgument() {iplToDisable})
            Native.Function.Call(Hash.REQUEST_IPL, New InputArgument() {iplToEnable})
        Else
            Native.Function.Call(Hash.REQUEST_IPL, New InputArgument() {iplToEnable})
        End If
    End Sub

    Public Shared Sub LoadMissingProps()
        Dim v_2 As Integer = Native.Function.Call(Of Integer)(Hash.GET_INTERIOR_AT_COORDS, -59.793598175048828, -1098.7840576171875, 27.2612) 'Showroom
        Native.Function.Call(Hash._ENABLE_INTERIOR_PROP, v_2, "csr_beforeMission")
        Native.Function.Call(Hash.REFRESH_INTERIOR, v_2)
    End Sub

    Public Shared Function GetVehicleClass(Vehicle As Vehicle) As String
        Dim i As Integer = Native.Function.Call(Of Integer)(Hash.GET_VEHICLE_CLASS, Vehicle)
        Dim Result As String = Nothing
        Select Case i
            Case 0
                Result = VC_COMPACT
            Case 1
                Result = VC_SEDAN
            Case 2
                Result = VC_SUV
            Case 3
                Result = VC_COUPE
            Case 4
                Result = VC_MUSCLE
            Case 5
                Result = VC_CLASSIC
            Case 6
                Result = VC_SPORT
            Case 7
                Result = VC_SUPER
            Case 8
                Result = VC_MOTORCYCLE
            Case 9
                Result = VC_OFF_ROAD
            Case 10
                Result = VC_INDUSTRIAL
            Case 11
                Result = VC_UTILITY
            Case 12
                Result = VC_VAN
            Case 13
                Result = VC_BICYCLE
            Case 14
                Result = VC_BOAT
            Case 15
                Result = VC_HELI
            Case 16
                Result = VC_PLANE
            Case 17
                Result = VC_SERVICE
            Case 18
                Result = VC_EMERGENCY
            Case 19
                Result = VC_MILITARY
            Case 20
                Result = VC_COMMERCIAL
        End Select
        Return Result
    End Function
End Class
