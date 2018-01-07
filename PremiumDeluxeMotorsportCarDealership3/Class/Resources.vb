Imports System.Drawing
Imports System.Text
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

    Enum EnumTypes
        NumberPlateType
        VehicleColorPrimary
        VehicleColorSecondary
        VehicleColorTrim
        VehicleColorDashboard
        VehicleColorRim
        VehicleColorAccent
        vehicleColorPearlescent
        VehicleWindowTint
    End Enum

    Public Shared Function IsCustomWheels() As Boolean
        Return Native.Function.Call(Of Boolean)(Hash.GET_VEHICLE_MOD_VARIATION, PDM.VehPreview, VehicleMod.FrontWheels)
    End Function

    Public Shared Function GetInteriorID(interior As Vector3) As Integer
        Return Native.Function.Call(Of Integer)(Hash.GET_INTERIOR_AT_COORDS, interior.X, interior.Y, interior.Z)
    End Function

    Public Shared Function DoesGXTEntryExist(entry As String) As Boolean
        Return Native.Function.Call(Of Boolean)(Hash.DOES_TEXT_LABEL_EXIST, entry)
    End Function

    Public Shared Function GetLocalizedColorName(vehColor As VehicleColor) As String
        If Not Native.Function.Call(Of Boolean)(Hash.HAS_THIS_ADDITIONAL_TEXT_LOADED, "mod_mnu", 10) Then
            Native.Function.Call(Hash.CLEAR_ADDITIONAL_TEXT, 10, True)
            Native.Function.Call(Hash.REQUEST_ADDITIONAL_TEXT, "mod_mnu", 10)
        End If
        If _colorNames.ContainsKey(vehColor) Then
            If DoesGXTEntryExist(_colorNames(vehColor).Item1) Then
                Return Game.GetGXTEntry(_colorNames(vehColor).Item1)
            End If
            Return Trim(RegularExpressions.Regex.Replace(_colorNames(vehColor).Item2, "[A-Z]", " ${0}"))
        End If
        Throw New ArgumentException("Vehicle Color Is undefined", "Vehicle Color")
    End Function

    Public Shared ClassicColor As List(Of VehicleColor) = New List(Of VehicleColor) From {0, 147, 1, 11, 2, 3, 4, 5, 6, 7, 8, 9, 10, 27, 28, 29, 150, 30, 31, 32, 33, 34, 143, 35, 135, 137, 136, 36, 38, 138, 99, 90, 88, 89,
        91, 49, 50, 51, 52, 53, 54, 92, 141, 61, 62, 63, 64, 65, 66, 67, 68, 69, 73, 70, 74, 96, 101, 95, 94, 97, 103, 104, 98, 100, 102, 99, 105, 106, 71, 72, 142, 145, 107, 111, 112}
    Public Shared MatteColor As List(Of VehicleColor) = New List(Of VehicleColor) From {12, 13, 14, 131, 83, 82, 84, 149, 148, 39, 40, 41, 42, 55, 128, 151, 155, 152, 153, 154}
    Public Shared MetalColor As List(Of VehicleColor) = New List(Of VehicleColor) From {117, 118, 119, 158, 159, 160}
    Public Shared ChromeColor As List(Of VehicleColor) = New List(Of VehicleColor) From {120}
    Public Shared PearlescentColor As List(Of VehicleColor) = New List(Of VehicleColor) From {0, 147, 1, 11, 2, 3, 4, 5, 6, 7, 8, 9, 10, 27, 28, 29, 150, 30, 31, 32, 33, 34, 143, 35, 135, 137, 136, 36, 38, 138, 99, 90, 88, 89, 91, 49, 50, 51, 52, 53, 54, 92, 141, 61, 62, 63, 64, 65, 66, 67, 68, 69, 73, 70, 74, 96, 101, 95, 94, 97, 103, 104, 98, 100, 102, 99, 105, 106, 71, 72, 142, 145, 107, 111, 112, 117, 118, 119, 158, 159, 160}

    Private Shared ReadOnly _colorNames As New Dictionary(Of Integer, Tuple(Of String, String))(New Dictionary(Of Integer, Tuple(Of String, String))() From {
    {0, New Tuple(Of String, String)("BLACK", "MetallicBlack")},
    {1, New Tuple(Of String, String)("GRAPHITE", "MetallicGraphiteBlack")},
    {2, New Tuple(Of String, String)("BLACK_STEEL", "MetallicBlackSteel")},
    {3, New Tuple(Of String, String)("DARK_SILVER", "MetallicDarkSilver")},
    {4, New Tuple(Of String, String)("SILVER", "MetallicSilver")},
    {5, New Tuple(Of String, String)("BLUE_SILVER", "MetallicBlueSilver")},
    {6, New Tuple(Of String, String)("ROLLED_STEEL", "MetallicSteelGray")},
    {7, New Tuple(Of String, String)("SHADOW_SILVER", "MetallicShadowSilver")},
    {8, New Tuple(Of String, String)("STONE_SILVER", "MetallicStoneSilver")},
    {9, New Tuple(Of String, String)("MIDNIGHT_SILVER", "MetallicMidnightSilver")},
    {10, New Tuple(Of String, String)("CAST_IRON_SIL", "MetallicGunMetal")},
    {11, New Tuple(Of String, String)("ANTHR_BLACK", "MetallicAnthraciteGray")},
    {12, New Tuple(Of String, String)("BLACK", "MatteBlack")},
    {13, New Tuple(Of String, String)("GREY", "MatteGray")},
    {14, New Tuple(Of String, String)("LIGHT_GREY", "MatteLightGray")},
    {15, New Tuple(Of String, String)("BLACK", "UtilBlack")},
    {16, New Tuple(Of String, String)("BLACK", "UtilBlackPoly")},
    {17, New Tuple(Of String, String)("DARK_SILVER", "UtilDarksilver")},
    {18, New Tuple(Of String, String)("SILVER", "UtilSilver")},
    {19, New Tuple(Of String, String)("CAST_IRON_SIL", "UtilGunMetal")},
    {20, New Tuple(Of String, String)("SHADOW_SILVER", "UtilShadowSilver")},
    {21, New Tuple(Of String, String)("BLACK", "WornBlack")},
    {22, New Tuple(Of String, String)("GRAPHITE", "WornGraphite")},
    {23, New Tuple(Of String, String)("ROLLED_STEEL", "WornSilverGray")},
    {24, New Tuple(Of String, String)("SILVER", "WornSilver")},
    {25, New Tuple(Of String, String)("BLUE_SILVER", "WornBlueSilver")},
    {26, New Tuple(Of String, String)("SHADOW_SILVER", "WornShadowSilver")},
    {27, New Tuple(Of String, String)("RED", "MetallicRed")},
    {28, New Tuple(Of String, String)("TORINO_RED", "MetallicTorinoRed")},
    {29, New Tuple(Of String, String)("FORMULA_RED", "MetallicFormulaRed")},
    {30, New Tuple(Of String, String)("BLAZE_RED", "MetallicBlazeRed")},
    {31, New Tuple(Of String, String)("GRACE_RED", "MetallicGracefulRed")},
    {32, New Tuple(Of String, String)("GARNET_RED", "MetallicGarnetRed")},
    {33, New Tuple(Of String, String)("SUNSET_RED", "MetallicDesertRed")},
    {34, New Tuple(Of String, String)("CABERNET_RED", "MetallicCabernetRed")},
    {35, New Tuple(Of String, String)("CANDY_RED", "MetallicCandyRed")},
    {36, New Tuple(Of String, String)("SUNRISE_ORANGE", "MetallicSunriseOrange")},
    {37, New Tuple(Of String, String)("GOLD", "MetallicClassicGold")},
    {38, New Tuple(Of String, String)("ORANGE", "MetallicOrange")},
    {39, New Tuple(Of String, String)("RED", "MatteRed")},
    {40, New Tuple(Of String, String)("DARK_RED", "MatteDarkRed")},
    {41, New Tuple(Of String, String)("ORANGE", "MatteOrange")},
    {42, New Tuple(Of String, String)("YELLOW", "MatteYellow")},
    {43, New Tuple(Of String, String)("RED", "UtilRed")},
    {44, New Tuple(Of String, String)("NULL", "UtilBrightRed")},
    {45, New Tuple(Of String, String)("GARNET_RED", "UtilGarnetRed")},
    {46, New Tuple(Of String, String)("RED", "WornRed")},
    {47, New Tuple(Of String, String)("NULL", "WornGoldenRed")},
    {48, New Tuple(Of String, String)("DARK_RED", "WornDarkRed")},
    {49, New Tuple(Of String, String)("DARK_GREEN", "MetallicDarkGreen")},
    {50, New Tuple(Of String, String)("RACING_GREEN", "MetallicRacingGreen")},
    {51, New Tuple(Of String, String)("SEA_GREEN", "MetallicSeaGreen")},
    {52, New Tuple(Of String, String)("OLIVE_GREEN", "MetallicOliveGreen")},
    {53, New Tuple(Of String, String)("BRIGHT_GREEN", "MetallicGreen")},
    {54, New Tuple(Of String, String)("PETROL_GREEN", "MetallicGasolineBlueGreen")},
    {55, New Tuple(Of String, String)("LIME_GREEN", "MatteLimeGreen")},
    {56, New Tuple(Of String, String)("DARK_GREEN", "UtilDarkGreen")},
    {57, New Tuple(Of String, String)("GREEN", "UtilGreen")},
    {58, New Tuple(Of String, String)("DARK_GREEN", "WornDarkGreen")},
    {59, New Tuple(Of String, String)("GREEN", "WornGreen")},
    {60, New Tuple(Of String, String)("NULL", "WornSeaWash")},
    {61, New Tuple(Of String, String)("GALAXY_BLUE", "MetallicMidnightBlue")},
    {62, New Tuple(Of String, String)("DARK_BLUE", "MetallicDarkBlue")},
    {63, New Tuple(Of String, String)("SAXON_BLUE", "MetallicSaxonyBlue")},
    {64, New Tuple(Of String, String)("BLUE", "MetallicBlue")},
    {65, New Tuple(Of String, String)("MARINER_BLUE", "MetallicMarinerBlue")},
    {66, New Tuple(Of String, String)("HARBOR_BLUE", "MetallicHarborBlue")},
    {67, New Tuple(Of String, String)("DIAMOND_BLUE", "MetallicDiamondBlue")},
    {68, New Tuple(Of String, String)("SURF_BLUE", "MetallicSurfBlue")},
    {69, New Tuple(Of String, String)("NAUTICAL_BLUE", "MetallicNauticalBlue")},
    {70, New Tuple(Of String, String)("ULTRA_BLUE", "MetallicBrightBlue")},
    {71, New Tuple(Of String, String)("PURPLE", "MetallicPurpleBlue")},
    {72, New Tuple(Of String, String)("SPIN_PURPLE", "MetallicSpinnakerBlue")},
    {73, New Tuple(Of String, String)("RACING_BLUE", "MetallicUltraBlue")},
    {74, New Tuple(Of String, String)("LIGHT_BLUE", "MetallicLightBlue")},
    {75, New Tuple(Of String, String)("DARK_BLUE", "UtilDarkBlue")},
    {76, New Tuple(Of String, String)("MIDNIGHT_BLUE", "UtilMidnightBlue")},
    {77, New Tuple(Of String, String)("BLUE", "UtilBlue")},
    {78, New Tuple(Of String, String)("NULL", "UtilSeaFoamBlue")},
    {79, New Tuple(Of String, String)("LIGHT_BLUE", "UtilLightningBlue")},
    {80, New Tuple(Of String, String)("NULL", "UtilMauiBluePoly")},
    {81, New Tuple(Of String, String)("NULL", "UtilBrightBlue")},
    {82, New Tuple(Of String, String)("DARK_BLUE", "MatteDarkBlue")},
    {83, New Tuple(Of String, String)("BLUE", "MatteBlue")},
    {84, New Tuple(Of String, String)("MIDNIGHT_BLUE", "MatteMidnightBlue")},
    {85, New Tuple(Of String, String)("DARK_BLUE", "WornDarkBlue")},
    {86, New Tuple(Of String, String)("BLUE", "WornBlue")},
    {87, New Tuple(Of String, String)("LIGHT_BLUE", "WornLightBlue")},
    {88, New Tuple(Of String, String)("YELLOW", "MetallicTaxiYellow")},
    {89, New Tuple(Of String, String)("RACE_YELLOW", "MetallicRaceYellow")},
    {90, New Tuple(Of String, String)("BRONZE", "MetallicBronze")},
    {91, New Tuple(Of String, String)("FLUR_YELLOW", "MetallicYellowBird")},
    {92, New Tuple(Of String, String)("LIME_GREEN", "MetallicLime")},
    {93, New Tuple(Of String, String)("NULL", "MetallicChampagne")},
    {94, New Tuple(Of String, String)("UMBER_BROWN", "MetallicPuebloBeige")},
    {95, New Tuple(Of String, String)("CREEK_BROWN", "MetallicDarkIvory")},
    {96, New Tuple(Of String, String)("CHOCOLATE_BROWN", "MetallicChocoBrown")},
    {97, New Tuple(Of String, String)("MAPLE_BROWN", "MetallicGoldenBrown")},
    {98, New Tuple(Of String, String)("SADDLE_BROWN", "MetallicLightBrown")},
    {99, New Tuple(Of String, String)("STRAW_BROWN", "MetallicStrawBeige")},
    {100, New Tuple(Of String, String)("MOSS_BROWN", "MetallicMossBrown")},
    {101, New Tuple(Of String, String)("BISON_BROWN", "MetallicBistonBrown")},
    {102, New Tuple(Of String, String)("WOODBEECH_BROWN", "MetallicBeechwood")},
    {103, New Tuple(Of String, String)("NULL", "MetallicDarkBeechwood")},
    {104, New Tuple(Of String, String)("SIENNA_BROWN", "MetallicChocoOrange")},
    {105, New Tuple(Of String, String)("SANDY_BROWN", "MetallicBeachSand")},
    {106, New Tuple(Of String, String)("BLEECHED_BROWN", "MetallicSunBleechedSand")},
    {107, New Tuple(Of String, String)("CREAM", "MetallicCream")},
    {108, New Tuple(Of String, String)("BROWN", "UtilBrown")},
    {109, New Tuple(Of String, String)("NULL", "UtilMediumBrown")},
    {110, New Tuple(Of String, String)("NULL", "UtilLightBrown")},
    {111, New Tuple(Of String, String)("WHITE", "MetallicWhite")},
    {112, New Tuple(Of String, String)("FROST_WHITE", "MetallicFrostWhite")},
    {113, New Tuple(Of String, String)("NULL", "WornHoneyBeige")},
    {114, New Tuple(Of String, String)("BROWN", "WornBrown")},
    {115, New Tuple(Of String, String)("DARK_BROWN", "WornDarkBrown")},
    {116, New Tuple(Of String, String)("STRAW_BROWN", "WornStrawBeige")},
    {117, New Tuple(Of String, String)("BR_STEEL", "BrushedSteel")},
    {118, New Tuple(Of String, String)("BR BLACK_STEEL", "BrushedBlackSteel")},
    {119, New Tuple(Of String, String)("BR_ALUMINIUM", "BrushedAluminium")},
    {120, New Tuple(Of String, String)("CHROME", "Chrome")},
    {121, New Tuple(Of String, String)("NULL", "WornOffWhite")},
    {122, New Tuple(Of String, String)("NULL", "UtilOffWhite")},
    {123, New Tuple(Of String, String)("ORANGE", "WornOrange")},
    {124, New Tuple(Of String, String)("NULL", "WornLightOrange")},
    {125, New Tuple(Of String, String)("NULL", "MetallicSecuricorGreen")},
    {126, New Tuple(Of String, String)("YELLOW", "WornTaxiYellow")},
    {127, New Tuple(Of String, String)("NULL", "PoliceCarBlue")},
    {128, New Tuple(Of String, String)("GREEN", "MatteGreen")},
    {129, New Tuple(Of String, String)("BROWN", "MatteBrown")},
    {130, New Tuple(Of String, String)("NULL", "SteelBlue")},
    {131, New Tuple(Of String, String)("WHITE", "MatteWhite")},
    {132, New Tuple(Of String, String)("WHITE", "WornWhite")},
    {133, New Tuple(Of String, String)("OLIVE_GREEN", "WornOliveArmyGreen")},
    {134, New Tuple(Of String, String)("WHITE", "PureWhite")},
    {135, New Tuple(Of String, String)("HOT PINK", "HotPink")},
    {136, New Tuple(Of String, String)("SALMON_PINK", "Salmonpink")},
    {137, New Tuple(Of String, String)("PINK", "MetallicVermillionPink")},
    {138, New Tuple(Of String, String)("BRIGHT_ORANGE", "Orange")},
    {139, New Tuple(Of String, String)("GREEN", "Green")},
    {140, New Tuple(Of String, String)("BLUE", "Blue")},
    {141, New Tuple(Of String, String)("MIDNIGHT_BLUE", "MettalicBlackBlue")},
    {142, New Tuple(Of String, String)("MIGHT_PURPLE", "MetallicBlackPurple")},
    {143, New Tuple(Of String, String)("WINE_RED", "MetallicBlackRed")},
    {144, New Tuple(Of String, String)("NULL", "HunterGreen")},
    {145, New Tuple(Of String, String)("BRIGHT_PURPLE", "MetallicPurple")},
    {146, New Tuple(Of String, String)("MIGHT_PURPLE", "MetaillicVDarkBlue")},
    {147, New Tuple(Of String, String)("BLACK_GRAPHITE", "ModshopBlack1")},
    {148, New Tuple(Of String, String)("PURPLE", "MattePurple")},
    {149, New Tuple(Of String, String)("MIGHT_PURPLE", "MatteDarkPurple")},
    {150, New Tuple(Of String, String)("LAVA_RED", "MetallicLavaRed")},
    {151, New Tuple(Of String, String)("MATTE_FOR", "MatteForestGreen")},
    {152, New Tuple(Of String, String)("MATTE_OD", "MatteOliveDrab")},
    {153, New Tuple(Of String, String)("MATTE_DIRT", "MatteDesertBrown")},
    {154, New Tuple(Of String, String)("MATTE_DESERT", "MatteDesertTan")},
    {155, New Tuple(Of String, String)("MATTE_FOIL", "MatteFoliageGreen")},
    {156, New Tuple(Of String, String)("NULL", "DefaultAlloyColor")},
    {157, New Tuple(Of String, String)("NULL", "EpsilonBlue")},
    {158, New Tuple(Of String, String)("GOLD_P", "PureGold")},
    {159, New Tuple(Of String, String)("GOLD_S", "BrushedGold")},
    {160, New Tuple(Of String, String)("NULL", "SecretGold")}
    })

    Public Shared Function LocalizedWindowsTint(tint As GTA.VehicleWindowTint) As String
        Dim result As String = Nothing

        Select Case tint
            Case VehicleWindowTint.DarkSmoke
                result = Game.GetGXTEntry("CMOD_WIN_2")
                Exit Select
            Case VehicleWindowTint.Green
                result = Game.GetGXTEntry("GREEN")
                Exit Select
            Case VehicleWindowTint.LightSmoke
                result = Game.GetGXTEntry("CMOD_WIN_1")
                Exit Select
            Case VehicleWindowTint.Limo
                result = Game.GetGXTEntry("CMOD_WIN_3")
                Exit Select
            Case VehicleWindowTint.None
                result = Game.GetGXTEntry("CMOD_WIN_0")
                Exit Select
            Case VehicleWindowTint.PureBlack
                result = Game.GetGXTEntry("CMOD_WIN_5")
                Exit Select
            Case VehicleWindowTint.Stock
                result = Game.GetGXTEntry("CMOD_WIN_4")
                Exit Select
        End Select

        Return result
    End Function

    Public Shared Function LocalizedLicensePlate(plateType As GTA.NumberPlateType) As String
        Dim result As String = Nothing

        Select Case plateType
            Case NumberPlateType.BlueOnWhite1
                result = Game.GetGXTEntry("CMOD_PLA_0")
                Exit Select
            Case NumberPlateType.BlueOnWhite2
                result = Game.GetGXTEntry("CMOD_PLA_1")
                Exit Select
            Case NumberPlateType.BlueOnWhite3
                result = Game.GetGXTEntry("CMOD_PLA_2")
                Exit Select
            Case NumberPlateType.NorthYankton
                result = Game.GetGXTEntry("CMOD_MOD_GLD2")
                Exit Select
            Case NumberPlateType.YellowOnBlack
                result = Game.GetGXTEntry("CMOD_PLA_4")
                Exit Select
            Case NumberPlateType.YellowOnBlue
                result = Game.GetGXTEntry("CMOD_PLA_3")
                Exit Select
        End Select

        Return result
    End Function

    Public Shared Function GetClassDisplayName(vehicleClass As VehicleClass) As String
        Return Game.GetGXTEntry("VEH_CLASS_" + CInt(vehicleClass).ToString())
    End Function

    'Public Shared Function GetCarMakeNames(veh As Vehicle) As String
    '    Dim brand As String = Nothing

    '    Select Case veh.Model
    '        Case "ninef", "ninef2", "rocoto", "tailgate", "omnis"
    '            brand = "OBEY"
    '        Case "blista", "akuma", "double", "marquis", "blista2", "blista3", "enduro", "jester", "jester2", "thrust", "vindicator"
    '            brand = "DINKA"
    '        Case "asea", "asea2", "burrito", "burrito2", "burrito3", "burrito4", "burrito5", "gburrito", "granger", "premier", "rancherxl", "rancherxl2", "sabregt", "tornado", "tornado2", "tornado3", "tornado4", "voodoo2", "vigero", "mamba", "moonbeam", "moonbeam2", "rhapsody", "sabregt2", "stalion", "stalion2", "tampa", "tampa2", "tornado5", "tornado6", "voodoo", "gburrito2", "tampa3"
    '            brand = "DECLASSE"
    '        Case "asterope", "bjxl", "dilettante", "dilettante2", "futo", "intruder", "rebel", "rebel2", "sultan", "kuruma", "kuruma2", "sultanrs", "technical", "technical2", "technical3"
    '            brand = "KARIN"
    '        Case "barracks2", "biff", "bulldozer", "cutter", "dump", "forklift", "mixer", "mixer2", "brickade", "insurgent", "insurgent2", "apc", "insurgent", "nightshark"
    '            brand = "HVY"
    '        Case "blazer", "blazer2", "blazer3", "carbonrs", "dinghy", "dinghy2", "bf400", "blazer4", "blazer5", "chimera", "dinghy3", "dinghy4", "shotaro"
    '            brand = "NAGASAKI"
    '        Case "bison", "bison2", "bison3", "banshee", "buffalo", "buffalo2", "gresley", "dloader", "gauntlet", "rumpo", "rumpo2", "youga", "banshee2", "buffalo3", "gauntlet2", "paradise", "ratloader2", "rumpo3", "verlierer2", "youga2", "halftrack"
    '            brand = "BRAVADO"
    '        Case "baller", "baller2", "baller3", "baller4", "baller5", "baller6"
    '            brand = "GALLIVAN"
    '        Case "benson", "bobcatxl", "bullet", "hotknife", "dominator", "minivan", "peyote", "radi", "sadler", "sadler2", "sandking", "sandking2", "speedo", "speedo2", "stanier", "chino", "chino2", "contender", "dominator2", "fmj", "guardian", "minivan2", "monster", "slamvan", "slamvan2", "slamvan3", "trophytruck", "trophytruck2", "blade", "retinue"
    '            brand = "VAPID"
    '        Case "bfinjection", "dune", "surfer", "surfer2", "bifta", "raptor", "dune3"
    '            brand = "BF"
    '        Case "boxville3", "camper", "pony", "pony2", "stockade", "stockade3", "tiptruck", "boxville4"
    '            brand = "BRUTE"
    '        Case "bodhi2", "mesa", "mesa2", "mesa3", "crusader", "seminole", "kalahari"
    '            brand = "CANIS"
    '        Case "buccaneer", "cavalcade", "cavalcade2", "emperor", "emperor2", "emperor3", "manana", "primo", "washington", "alpha", "btype", "btype2", "btype3", "buccaneer2", "Lurcher", "primo2", "virgo"
    '            brand = "ALBANY"
    '        Case "carbonizzare", "cheetah", "stinger", "stingergt", "bestiagts", "brioso", "prototipo", "turismo2", "turismor", "cheetah2", "visione"
    '            brand = "GROTTI"
    '        Case "comet2", "comet3", "pfister811"
    '            brand = "PFISTER"
    '        Case "cogcabrio", "superd", "cog55", "cog552", "cognoscenti", "cognoscenti2", "huntley", "windsor", "windsor2"
    '            brand = "ENUS"
    '        Case "coquette", "coquette2", "coquette3"
    '            brand = "INVERTO"
    '        Case "dubsta", "dubsta2", "feltzer2", "schafter2", "schwarzer", "serrano", "surano", "dubsta3", "feltzer3", "glendale", "limo2", "panto", "schafter3", "schafter4", "schafter5", "schafter6", "xls", "xls2"
    '            brand = "BENEFAC"
    '        Case "flatbed", "packer", "pounder", "rallytruck", "wastelander"
    '            brand = "MTL"
    '        Case "fq2"
    '            brand = "FATHOM"
    '        Case "fusilade"
    '            brand = "SCHYSTER"
    '        Case "fugitive", "picador", "surge", "marshall"
    '            brand = "CHEVAL"
    '        Case "habanero", "sheava"
    '            brand = "EMPEROR"
    '        Case "hauler", "rubble", "phantom", "phantom", "hauler2", "phantom3"
    '            brand = "JOBUILT"
    '        Case "entityxf"
    '            brand = "OVERFLOD"
    '        Case "exemplar", "jb700", "rapidgt", "rapidgt2", "massacro", "massacro2", "seven70", "specter", "specter2", "vagner", "rapidgt3"
    '            brand = "DEWBAUCH"
    '        Case "elegy2", "elegy", "le7b"
    '            brand = "ANNIS"
    '        Case "f620", "Jackal", "lynx", "penetrator", "ardent", "xa21"
    '            brand = "OCELOT"
    '        Case "felon", "felon2", "casco", "furoregt", "pigalle", "toro", "toro2", "tropos"
    '            brand = "LAMPADA"
    '        Case "infernus", "monroe", "bati", "bati2", "ruffian", "faggio2", "vacca", "esskey", "faggio", "faggio3", "fcr", "fcr2", "infernus2", "osiris", "reaper", "speeder", "speeder2", "tempesta", "vortex", "zentorno", "oppressor", "torero"
    '            brand = "PEGASSI"
    '        Case "ingot", "warrener"
    '            brand = "VULCAR"
    '        Case "issi2"
    '            brand = "WEENY"
    '        Case "journey", "stratum"
    '            brand = "ZIRCONIU"
    '        Case "khamelion", "ruston"
    '            brand = "HIJAK"
    '        Case "landstalker", "regina", "stretch", "virgo2", "virgo3"
    '            brand = "DUNDREAR"
    '        Case "mule", "mule2", "penumbra", "sanchez", "sanchez2", "manchez", "mule3"
    '            brand = "MAIBATSU"
    '        Case "bagger", "daemon", "besra", "cliffhanger", "daemon2", "gargoyle", "nightblade", "ratbike", "sovereign", "wolfsbane", "zombiea", "zombieb"
    '            brand = "WESTERN"
    '        Case "pcj", "vader", "squalo", "jetmax", "tropic", "suntrap", "defiler", "hakuchou", "hakuchou2", "tropic2"
    '            brand = "SHITZU"
    '        Case "hexer", "avarus", "innovation", "sanctus"
    '            brand = "LCC"
    '        Case "nemesis", "diablous", "diablous2", "lectro"
    '            brand = "PRINCIPL"
    '        Case "shamal", "luxor", "luxor2", "miljet", "nimbus", "supervolito", "supervolito2", "swift", "swift2", "vestra", "volatus"
    '            brand = "BUCKING"
    '        Case "seashark", "seashark2", "seashark3"
    '            brand = "SPEEDOPH"
    '        Case "adder", "ztype", "nero", "nero2"
    '            brand = "TRUFFADE"
    '        Case "voltic", "brawler", "voltic2", "cyclone"
    '            brand = "COIL"
    '        Case "dukes", "dukes2", "nightshade", "ruiner2", "ruiner3", "ruiner", "phoenix"
    '            brand = "IMPONTE"
    '        Case "faction", "faction2", "faction3"
    '            brand = "WILLARD"
    '        Case "gp1", "italigtb", "italigtb2", "t20", "tyrus"
    '            brand = "PROGEN"
    '        Case "hydra"
    '            brand = "MAMMOTH"
    '        Case "sentinel", "sentinel2", "zion", "zion2", "oracle", "oracle2"
    '            brand = "UBERMACH"
    '        Case "trailersmall2"
    '            brand = "VOMFEUER"
    '        Case Else
    '            brand = "NULL"
    '    End Select
    '    Return brand
    'End Function

    Public Shared Sub AttachTo(entity1 As Entity, entity2 As Entity, boneindex As Integer, position As Vector3, rotation As Vector3)
        Native.Function.Call(Hash.ATTACH_ENTITY_TO_ENTITY, entity1.Handle, entity2.Handle, boneindex, position.X, position.Y, position.Z, rotation.X, rotation.Y, rotation.Z, False, False, True, False, 2, True)
    End Sub

    Public Shared Function GetVehAcceleration(veh As Vehicle) As Single
        Dim result As Single = (Native.Function.Call(Of Single)(Hash.GET_VEHICLE_ACCELERATION, veh) * 100) * 4.4
        If result >= 200 Then result = 200
        Return result
    End Function

    Public Shared Function GetVehTopSpeed(veh As Vehicle) As Single
        Dim result As Single = (((Native.Function.Call(Of Single)(Hash._0x53AF99BAA671CA47, veh) * 3600) / 1609.344) * 1.9)
        If result >= 200 Then result = 200
        Return result
    End Function

    Public Shared Function GetVehBraking(veh As Vehicle) As Single
        Dim result As Single = (Native.Function.Call(Of Single)(Hash.GET_VEHICLE_MAX_BRAKING, veh) * 70)
        If result >= 200 Then result = 200
        Return result
    End Function

    Public Shared Function GetVehTraction(veh As Vehicle) As Single
        Dim result As Single = Native.Function.Call(Of Single)(Hash.GET_VEHICLE_MAX_TRACTION, veh) * 6.5
        If result >= 200 Then result = 200
        Return result
    End Function
End Class
