Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports System.Text
Imports System.Windows.Forms
Imports GTA
Imports GTA.Math
Imports GTA.Native
Imports INMNativeUI

Public Module Helper

    Public VC_MOTORCYCLE, VC_COMPACT, VC_COUPE, VC_SEDAN, VC_SPORT, VC_CLASSIC, VC_SUPER, VC_MUSCLE, VC_OFF_ROAD, VC_SUV, VC_VAN As String
    Public VC_INDUSTRIAL, VC_BICYCLE, VC_BOAT, VC_HELI, VC_PLANE, VC_SERVICE, VC_EMERGENCY, VC_MILITARY, VC_COMMERCIAL, VC_UTILITY As String

    Public config As ScriptSettings = ScriptSettings.Load("scripts\PremiumDeluxeMotorsport\config.ini")
    Public hiddenSave As ScriptSettings = ScriptSettings.Load("scripts\PremiumDeluxeMotorsport\database.ini")
    Public optRemoveColor As Boolean = True
    Public optRemoveImg As Boolean = False
    Public optRandomColor As Boolean = True
    Public optFade As Boolean = True
    Public optLastVehHash As Integer = 0
    Public optLastVehName As String = Nothing
    Public optLastVehMake As String = Nothing
    Public optLogging As Boolean = True
    Public keyZoom As GTA.Control = GTA.Control.NextCamera
    Public keyDoor As GTA.Control = GTA.Control.ParachuteBrakeLeft
    Public keyRoof As GTA.Control = GTA.Control.VehicleRoof
    Public keyCamera As GTA.Control = GTA.Control.VehiclePushbikeSprint

    Public BtnRotLeft, BtnRotRight, BtnCamera, BtnZoom As InstructionalButton

    Public VehPreview As Vehicle
    Public lastVehMemory As Memory
    Public TaskScriptStatus As Integer = -1
    Public SelectedVehicle As String, PlayerCash, VehiclePrice As Integer, PdmBlip As Blip
    Public HideHud As Boolean = False, DrawSpotLight As Boolean = False, ShowVehicleName As Boolean = False
    Public Price As Decimal = 0, Radius As Integer = 120, TestDrive As Integer = 1, VehicleName As String = Nothing
    Public wsCamera As New WorkshopCamera
    Public PdmDoor, PlayerLastPos As Vector3, GPC, pdmPed As Ped, GP As Player
    Public PdmDoorDist As Single
    Public poly As Interior = New Interior(), testDeivePoly As Interior = New Interior()
    Public blipName As String = "NULL"

    Public VehPreviewPos As Vector3 = New Vector3(-44.45501, -1096.976, 26.42235)
    Public CameraPos As Vector3 = New Vector3(-47.45673, -1101.28, 27.54757)
    Public CameraRot As Vector3 = New Vector3(-18.12634, 0, -26.97177)
    Public PlayerHeading As Single = 250.6701

    Public Sub LoadSettings()
        optRemoveColor = config.GetValue(Of Boolean)("SETTINGS", "REMOVECOLOR", True)
        optRemoveImg = config.GetValue(Of Boolean)("SETTINGS", "REMOVESPRITE", False)
        optRandomColor = config.GetValue(Of Boolean)("SETTINGS", "RANDOMCOLOR", True)
        optFade = config.GetValue(Of Boolean)("SETTINGS", "FADEEFFECT", True)
        optLastVehHash = config.GetValue(Of Integer)("SETTINGS", "LASTVEHHASH", -2022483795)
        optLastVehName = config.GetValue(Of String)("SETTINGS", "LASTVEHNAME", "Pfister Comet Retro Custom")
        optLogging = config.GetValue(Of Boolean)("SETTINGS", "LOGGING", True)
        keyZoom = config.GetValue(Of GTA.Control)("CONTROLS", "ZOOM", GTA.Control.FrontendRt)
        keyDoor = config.GetValue(Of GTA.Control)("CONTROLS", "DOOR", GTA.Control.ParachuteBrakeLeft)
        keyRoof = config.GetValue(Of GTA.Control)("CONTROLS", "ROOF", GTA.Control.ParachuteBrakeRight)
        keyCamera = config.GetValue(Of GTA.Control)("CONTROLS", "CAMERA", GTA.Control.NextCamera)
    End Sub

    <Extension()>
    Public Function Name(ped As Ped) As String
        Select Case ped.Model
            Case PedHash.Franklin
                Return "Franklin" '"Franklin Clinton"
            Case PedHash.Michael
                Return "Michael" '"Michael De Santa"
            Case PedHash.Trevor
                Return "Trevor" '"Trevor Philips"
            Case Else
                Return Game.Player.Name
        End Select
    End Function

    Public Sub DisplayHelpTextThisFrame(ByVal [text] As String)
        Native.Function.Call(Hash._0x8509B634FBE7DA11, "STRING")
        Native.Function.Call(Hash._0x6C188BE134E074AA, [text])
        Native.Function.Call(Hash._0x238FFE5C7B0498A6, 0, 0, 1, -1)
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
    Public Sub DisplayNotificationThisFrame(Sender As String, Subject As String, Message As String, Icon As String, Flash As Boolean, Type As IconType)
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

    Public Function GetLangEntry(Lang As String) As String
        Dim result As String = ReadCfgValue(Lang, Application.StartupPath & "\scripts\PremiumDeluxeMotorsport\Languages\" & Game.Language.ToString & ".cfg")
        Dim real_result As String
        If result = Nothing Then
            real_result = "NULL"
        Else
            real_result = result
        End If
        Return real_result
    End Function

    Public Function CreateVehicle(VehicleModel As String, Position As Vector3, Optional Heading As Single = 0) As Vehicle
        Dim Result As Vehicle = Nothing
        Dim model = New Model(VehicleModel)
        model.Request(250)
        If model.IsInCdImage AndAlso model.IsValid Then
            While Not model.IsLoaded
                Script.Yield()
            End While
            Result = World.CreateVehicle(model, Position, Heading)
        End If
        model.MarkAsNoLongerNeeded()
        Return Result
    End Function

    Public Function CreateVehicleFromHash(VehicleHash As Integer, Position As Vector3, Optional Heading As Single = 0) As Vehicle
        Dim Result As Vehicle = Nothing
        Dim model = New Model(VehicleHash)
        model.Request(250)
        If model.IsInCdImage AndAlso model.IsValid Then
            While Not model.IsLoaded
                Script.Yield()
            End While
            Result = World.CreateVehicle(model, Position, Heading)
        End If
        model.MarkAsNoLongerNeeded()
        Return Result
    End Function

    Public Function CreateProp(PropModel As Integer, Position As Vector3, Rotation As Vector3) As Prop
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

    Public Sub ToggleIPL(iplToEnable As String, iplToDisable As String)
        If Native.Function.Call(Of Boolean)(Hash.IS_IPL_ACTIVE, New InputArgument() {iplToDisable}) Then
            Native.Function.Call(Hash.REMOVE_IPL, New InputArgument() {iplToDisable})
            Native.Function.Call(Hash.REQUEST_IPL, New InputArgument() {iplToEnable})
        Else
            Native.Function.Call(Hash.REQUEST_IPL, New InputArgument() {iplToEnable})
        End If
    End Sub

    Public Sub LoadMissingProps()
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

    Public Function IsCustomWheels() As Boolean
        Return Native.Function.Call(Of Boolean)(Hash.GET_VEHICLE_MOD_VARIATION, VehPreview, VehicleMod.FrontWheels)
    End Function

    Public Function GetInteriorID(interior As Vector3) As Integer
        Return Native.Function.Call(Of Integer)(Hash.GET_INTERIOR_AT_COORDS, interior.X, interior.Y, interior.Z)
    End Function

    Public Function DoesGXTEntryExist(entry As String) As Boolean
        Return Native.Function.Call(Of Boolean)(Hash.DOES_TEXT_LABEL_EXIST, entry)
    End Function

    Public Function GetLocalizedColorName(vehColor As VehicleColor) As String
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

    Public ClassicColor As List(Of VehicleColor) = New List(Of VehicleColor) From {0, 147, 1, 11, 2, 3, 4, 5, 6, 7, 8, 9, 10, 27, 28, 29, 150, 30, 31, 32, 33, 34, 143, 35, 135, 137, 136, 36, 38, 138, 99, 90, 88, 89,
        91, 49, 50, 51, 52, 53, 54, 92, 141, 61, 62, 63, 64, 65, 66, 67, 68, 69, 73, 70, 74, 96, 101, 95, 94, 97, 103, 104, 98, 100, 102, 99, 105, 106, 71, 72, 142, 145, 107, 111, 112}
    Public MatteColor As List(Of VehicleColor) = New List(Of VehicleColor) From {12, 13, 14, 131, 83, 82, 84, 149, 148, 39, 40, 41, 42, 55, 128, 151, 155, 152, 153, 154}
    Public MetalColor As List(Of VehicleColor) = New List(Of VehicleColor) From {117, 118, 119, 158, 159, 160}
    Public ChromeColor As List(Of VehicleColor) = New List(Of VehicleColor) From {120}
    Public PearlescentColor As List(Of VehicleColor) = New List(Of VehicleColor) From {0, 147, 1, 11, 2, 3, 4, 5, 6, 7, 8, 9, 10, 27, 28, 29, 150, 30, 31, 32, 33, 34, 143, 35, 135, 137, 136, 36, 38, 138, 99, 90, 88, 89, 91, 49, 50, 51, 52, 53, 54, 92, 141, 61, 62, 63, 64, 65, 66, 67, 68, 69, 73, 70, 74, 96, 101, 95, 94, 97, 103, 104, 98, 100, 102, 99, 105, 106, 71, 72, 142, 145, 107, 111, 112, 117, 118, 119, 158, 159, 160}

    Private ReadOnly _colorNames As New Dictionary(Of Integer, Tuple(Of String, String))(New Dictionary(Of Integer, Tuple(Of String, String))() From {
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

    Public Function LocalizedWindowsTint(tint As GTA.VehicleWindowTint) As String
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

    Public Function LocalizedLicensePlate(plateType As GTA.NumberPlateType) As String
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

    <Extension()>
    Public Function GetClassDisplayName(vehicle As Vehicle) As String
        Return Game.GetGXTEntry("VEH_CLASS_" + CInt(vehicle.ClassType).ToString())
    End Function

    Public Sub AttachTo(entity1 As Entity, entity2 As Entity, boneindex As Integer, position As Vector3, rotation As Vector3)
        Native.Function.Call(Hash.ATTACH_ENTITY_TO_ENTITY, entity1.Handle, entity2.Handle, boneindex, position.X, position.Y, position.Z, rotation.X, rotation.Y, rotation.Z, False, False, True, False, 2, True)
    End Sub

    Public Function GetVehAcceleration(veh As Vehicle) As Single
        Dim result As Single = (Native.Function.Call(Of Single)(Hash.GET_VEHICLE_ACCELERATION, veh) * 100) * 4.4
        If result >= 200 Then result = 200
        Return result
    End Function

    Public Function GetVehTopSpeed(veh As Vehicle) As Single
        Dim result As Single = (((Native.Function.Call(Of Single)(Hash._0x53AF99BAA671CA47, veh) * 3600) / 1609.344) * 1.9)
        If result >= 200 Then result = 200
        Return result
    End Function

    Public Function GetVehBraking(veh As Vehicle) As Single
        Dim result As Single = (Native.Function.Call(Of Single)(Hash.GET_VEHICLE_MAX_BRAKING, veh) * 70)
        If result >= 200 Then result = 200
        Return result
    End Function

    Public Function GetVehTraction(veh As Vehicle) As Single
        Dim result As Single = Native.Function.Call(Of Single)(Hash.GET_VEHICLE_MAX_TRACTION, veh) * 6.5
        If result >= 200 Then result = 200
        Return result
    End Function

    Public Function RequestAdditionTextFile(ByVal textname As String, ByVal Optional timeout As Integer = 1000) As Boolean
        If Not Native.Function.Call(Of Boolean)(Hash.HAS_THIS_ADDITIONAL_TEXT_LOADED, textname, 9) Then
            Native.Function.Call(Hash.CLEAR_ADDITIONAL_TEXT, 9, True)
            Native.Function.Call(Hash.REQUEST_ADDITIONAL_TEXT, textname, 9)
            Dim [end] As Integer = Game.GameTime + timeout

            If True Then
                While Game.GameTime < [end]
                    If Native.Function.Call(Of Boolean)(Hash.HAS_THIS_ADDITIONAL_TEXT_LOADED, textname, 9) Then Return True
                    Script.Yield()
                End While
                Return False
            End If
        End If

        Return True
    End Function

    'open shop_controller.ysc and search for "!= 999", the first global
    Public Enum GlobalValue
        b1_0_757_4 = &H271803
        b1_0_791_2 = &H272A34
        b1_0_877_1 = &H2750BD
        b1_0_944_2 = &H279476
        'b1_0_1011_1 = ?
        b1_0_1032_1 = 2593970
        b1_0_1103_2 = 2599337
        b1_0_1180_2 = 2606794
        'b1_0_1290_1 = ?
        b1_0_1365_1 = 4265719
        'b1_0_1493_0 = ?
        b1_0_1493_1 = 4266042
        b1_0_1604_1 = 4266905
        b1_0_1737_0 = 4267883
        b1_0_1868_0 = 4268190
        b1_0_2060_0 = 4268340
    End Enum

    Public Function GetGlobalValue() As GlobalValue
        Select Case Game.Version
            Case GameVersion.VER_1_0_757_4_NOSTEAM
                Return GlobalValue.b1_0_757_4
            Case GameVersion.VER_1_0_791_2_NOSTEAM, GameVersion.VER_1_0_791_2_STEAM
                Return GlobalValue.b1_0_791_2
            Case GameVersion.VER_1_0_877_1_NOSTEAM, GameVersion.VER_1_0_877_1_STEAM
                Return GlobalValue.b1_0_877_1
            Case GameVersion.VER_1_0_944_2_NOSTEAM, GameVersion.VER_1_0_944_2_STEAM
                Return GlobalValue.b1_0_944_2
            Case GameVersion.VER_1_0_1032_1_NOSTEAM, GameVersion.VER_1_0_1032_1_STEAM
                Return GlobalValue.b1_0_1032_1
            Case GameVersion.VER_1_0_1103_2_NOSTEAM, GameVersion.VER_1_0_1103_2_STEAM
                Return GlobalValue.b1_0_1103_2
            Case GameVersion.VER_1_0_1180_2_NOSTEAM, GameVersion.VER_1_0_1180_2_STEAM
                Return GlobalValue.b1_0_1180_2
            Case GameVersion.VER_1_0_1365_1_NOSTEAM, GameVersion.VER_1_0_1365_1_STEAM
                Return GlobalValue.b1_0_1365_1
            Case GameVersion.VER_1_0_1493_1_NOSTEAM, GameVersion.VER_1_0_1493_1_STEAM
                Return GlobalValue.b1_0_1493_1
            Case GameVersion.VER_1_0_1604_0_NOSTEAM, GameVersion.VER_1_0_1604_0_STEAM, GameVersion.VER_1_0_1604_1_NOSTEAM, GameVersion.VER_1_0_1604_1_STEAM
                Return GlobalValue.b1_0_1604_1
            Case GameVersion.VER_1_0_1737_0_NOSTEAM, GameVersion.VER_1_0_1737_0_STEAM, GameVersion.VER_1_0_1737_6_NOSTEAM, GameVersion.VER_1_0_1737_6_STEAM
                Return GlobalValue.b1_0_1737_0
            Case GameVersion.VER_1_0_1868_0_NOSTEAM, GameVersion.VER_1_0_1868_0_STEAM, 57, 58, 59 'GameVersion.VER_1_0_1868_1_STEAM, GameVersion.VER_1_0_1868_1_NOSTEAM, GameVersion.VER_1_0_1868_4_EGS
                Return GlobalValue.b1_0_1868_0
            Case 60, 61, 62, 63 'GameVersion.VER_1_0_2060_0_STEAM, GameVersion.VER_1_0_2060_0_NOSTEAM, GameVersion.VER_1_0_2060_1_STEAM, GameVersion.VER_1_0_2060_1_NOSTEAM
                Return GlobalValue.b1_0_2060_0
            Case Else
                Return GlobalValue.b1_0_2060_0
        End Select
    End Function

    Public Sub UpdateVehPreview()
        lastVehMemory = New Memory() With {
                .Aerials = VehPreview.GetMod(VehicleMod.Aerials),
                .Trim = VehPreview.GetMod(VehicleMod.Trim),
                .FrontBumper = VehPreview.GetMod(VehicleMod.FrontBumper),
                .RearBumper = VehPreview.GetMod(VehicleMod.RearBumper),
                .SideSkirt = VehPreview.GetMod(VehicleMod.SideSkirt),
                .ColumnShifterLevers = VehPreview.GetMod(VehicleMod.ColumnShifterLevers),
                .Dashboard = VehPreview.GetMod(VehicleMod.Dashboard),
                .DialDesign = VehPreview.GetMod(VehicleMod.DialDesign),
                .Ornaments = VehPreview.GetMod(VehicleMod.Ornaments),
                .Seats = VehPreview.GetMod(VehicleMod.Seats),
                .SteeringWheels = VehPreview.GetMod(VehicleMod.SteeringWheels),
                .TrimDesign = VehPreview.GetMod(VehicleMod.TrimDesign),
                .LightsColor = VehPreview.DashboardColor,
                .TrimColor = VehPreview.TrimColor,
                .WheelType = VehPreview.WheelType,
                .AirFilter = VehPreview.GetMod(VehicleMod.AirFilter),
                .EngineBlock = VehPreview.GetMod(VehicleMod.EngineBlock),
                .Struts = VehPreview.GetMod(VehicleMod.Struts),
                .NumberPlate = VehPreview.NumberPlateType,
                .PlateHolder = VehPreview.GetMod(VehicleMod.PlateHolder),
                .VanityPlates = VehPreview.GetMod(VehicleMod.VanityPlates),
                .Armor = VehPreview.GetMod(VehicleMod.Armor),
                .Brakes = VehPreview.GetMod(VehicleMod.Brakes),
                .Engine = VehPreview.GetMod(VehicleMod.Engine),
                .Transmission = VehPreview.GetMod(VehicleMod.Transmission),
                .BackNeon = VehPreview.IsNeonLightsOn(VehicleNeonLight.Back),
                .FrontNeon = VehPreview.IsNeonLightsOn(VehicleNeonLight.Front),
                .LeftNeon = VehPreview.IsNeonLightsOn(VehicleNeonLight.Left),
                .RightNeon = VehPreview.IsNeonLightsOn(VehicleNeonLight.Right),
                .BackWheels = VehPreview.GetMod(VehicleMod.BackWheels),
                .FrontWheels = VehPreview.GetMod(VehicleMod.FrontWheels),
                .Headlights = VehPreview.IsToggleModOn(VehicleToggleMod.XenonHeadlights),
                .WheelsVariation = IsCustomWheels(),
                .ArchCover = VehPreview.GetMod(VehicleMod.ArchCover),
                .Exhaust = VehPreview.GetMod(VehicleMod.Exhaust),
                .Fender = VehPreview.GetMod(VehicleMod.Fender),
                .RightFender = VehPreview.GetMod(VehicleMod.RightFender),
                .DoorSpeakers = VehPreview.GetMod(VehicleMod.DoorSpeakers),
                .Frame = VehPreview.GetMod(VehicleMod.Frame),
                .Grille = VehPreview.GetMod(VehicleMod.Grille),
                .Hood = VehPreview.GetMod(VehicleMod.Hood),
                .Horns = VehPreview.GetMod(VehicleMod.Horns),
                .Hydraulics = VehPreview.GetMod(VehicleMod.Hydraulics),
                .Livery = VehPreview.GetMod(VehicleMod.Livery),
                .Plaques = VehPreview.GetMod(VehicleMod.Plaques),
                .Roof = VehPreview.GetMod(VehicleMod.Roof),
                .Speakers = VehPreview.GetMod(VehicleMod.Speakers),
                .Spoilers = VehPreview.GetMod(VehicleMod.Spoilers),
                .Tank = VehPreview.GetMod(VehicleMod.Tank),
                .Trunk = VehPreview.GetMod(VehicleMod.Trunk),
                .Turbo = VehPreview.IsToggleModOn(VehicleToggleMod.Turbo),
                .Windows = VehPreview.GetMod(VehicleMod.Windows),
                .Tint = VehPreview.WindowTint,
                .PearlescentColor = VehPreview.PearlescentColor,
                .PrimaryColor = VehPreview.PrimaryColor,
                .RimColor = VehPreview.RimColor,
                .SecondaryColor = VehPreview.SecondaryColor,
                .TireSmokeColor = VehPreview.TireSmokeColor,
                .NeonLightsColor = VehPreview.NeonLightsColor,
                .PlateNumbers = VehPreview.NumberPlate,
                .CustomPrimaryColor = VehPreview.CustomPrimaryColor,
                .CustomSecondaryColor = VehPreview.CustomSecondaryColor,
                .IsPrimaryColorCustom = VehPreview.IsPrimaryColorCustom,
                .IsSecondaryColorCustom = VehPreview.IsSecondaryColorCustom,
                .Suspension = VehPreview.GetMod(VehicleMod.Suspension)}
    End Sub

    Public Sub SuspendKeys()
        Game.DisableControlThisFrame(0, GTA.Control.MoveUpDown)
        Game.DisableControlThisFrame(0, GTA.Control.MoveLeftRight)
        Game.DisableControlThisFrame(0, GTA.Control.MoveDown)
        Game.DisableControlThisFrame(0, GTA.Control.MoveDownOnly)
        Game.DisableControlThisFrame(0, GTA.Control.MoveLeft)
        Game.DisableControlThisFrame(0, GTA.Control.MoveLeftOnly)
        Game.DisableControlThisFrame(0, GTA.Control.MoveRight)
        Game.DisableControlThisFrame(0, GTA.Control.MoveRightOnly)
        Game.DisableControlThisFrame(0, GTA.Control.MoveUp)
        Game.DisableControlThisFrame(0, GTA.Control.MoveUpOnly)
        Game.DisableControlThisFrame(0, GTA.Control.Jump)
        Game.DisableControlThisFrame(0, GTA.Control.Cover)
        Game.DisableControlThisFrame(0, GTA.Control.Context)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleAccelerate)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleAim)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleAttack)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleAttack2)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleBrake)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleCinCam)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleDuck)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleExit)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleHeadlight)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleHorn)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleMoveLeftOnly)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleMoveRightOnly)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleMoveLeft)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleMoveRight)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleSubTurnLeftRight)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleSubTurnLeftOnly)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleSubTurnRightOnly)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleSubTurnHardLeft)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleSubTurnHardRight)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleMoveLeftRight)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleLookLeft)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleLookRight)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleHotwireLeft)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleHotwireRight)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleGunLeftRight)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleGunLeft)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleGunRight)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleCinematicLeftRight)
        Game.DisableControlThisFrame(0, GTA.Control.NextCamera)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleRocketBoost)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleJump)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleCarJump)
        Game.DisableControlThisFrame(0, GTA.Control.Jump)
        Game.DisableControlThisFrame(0, keyCamera)
        Game.DisableControlThisFrame(0, keyDoor)
        Game.DisableControlThisFrame(0, keyRoof)
        Game.DisableControlThisFrame(0, keyZoom)
    End Sub
End Module
