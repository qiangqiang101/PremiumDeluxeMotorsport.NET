Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports GTA
Imports GTA.Native
Imports GTA.Math
Imports GTA.Game
Imports INMNativeUI
Imports System.Reflection
Imports PremiumDeluxeMotorsportCarDealership3.Resources

Public Class PremiumDeluxeMotorsport
    Inherits Script

    Public Shared playerHash, SelectedVehicle As String, PlayerCash, VehiclePrice As Integer, VehPreview As Vehicle, PdmBlip As Blip, _Camera As Camera
    Public Shared Price As Decimal = 0, Radius As Integer = 0, TestDrive As Integer = 1, VehicleName As String = Nothing, ChangeCamera As Integer = 0
    Public Shared HideHud As Boolean = False, DrawSpotLight As Boolean = False, VehicleSpin As Boolean = True, ShowVehicleName As Boolean = False
    Public Shared PdmDoor, testDriveVector, VehPreviewPos, CameraPos, CameraRot, PlayerPos As Vector3, GPC As Ped, GP As Player
    Public Shared CameraFOV, PlayerHeading, PdmDoorDist, TestDriveDist As Single

    Public Shared MainMenu, ConfirmMenu, CustomiseMenu, ConfirmMenu2 As UIMenu
    Public Shared MotorMenu, CompactMenu, CoupeMenu, SedanMenu, SportMenu, ClassicMenu, ExoticMenu, MuscleMenu, OffRoadMenu, SuvMenu, VanMenu, UtilMenu, ArmourMenu, LowRiderMenu, AddOnMenu As UIMenu
    Public Shared ColorCategoryMenu, PriColorMenu, ClassicColorMenu, MetallicColorMenu, PearlColorMenu, MetalColorMenu, MatteColorMenu, ChromeColorMenu, PeaColorMenu, CPriColorMenu As UIMenu
    Public Shared ColorMenu, ColorCategoryMenu2, SecColorMenu, ClassicColorMenu2, MetallicColorMenu2, PearlColorMenu2, MetalColorMenu2, MatteColorMenu2, ChromeColorMenu2, CSecColorMenu, PlateMenu As UIMenu

    Public Shared ItemMotor As New UIMenuItem(GetLangEntry("VC_MOTORCYCLE"))
    Public Shared ItemCompact As New UIMenuItem(GetLangEntry("VC_COMPACT"))
    Public Shared ItemCoupe As New UIMenuItem(GetLangEntry("VC_COUPE"))
    Public Shared ItemSedan As New UIMenuItem(GetLangEntry("VC_SEDAN"))
    Public Shared ItemSport As New UIMenuItem(GetLangEntry("VC_SPORT"))
    Public Shared ItemClassic As New UIMenuItem(GetLangEntry("VC_CLASSIC"))
    Public Shared ItemExotic As New UIMenuItem(GetLangEntry("VC_SUPER"))
    Public Shared ItemMuscle As New UIMenuItem(GetLangEntry("VC_MUSCLE"))
    Public Shared ItemOffRoad As New UIMenuItem(GetLangEntry("VC_OFF_ROAD"))
    Public Shared ItemSUV As New UIMenuItem(GetLangEntry("VC_SUV"))
    Public Shared ItemVan As New UIMenuItem(GetLangEntry("VC_VAN"))
    Public Shared ItemUtil As New UIMenuItem(GetLangEntry("VC_UTILITY"))
    Public Shared ItemArmoured As New UIMenuItem(GetLangEntry("VC_ARMOUR"))
    Public Shared ItemLowRider As New UIMenuItem(GetLangEntry("VC_LOWRIDER"))
    Public Shared ItemAddOn As New UIMenuItem(GetLangEntry("VC_ADD_ON"))
    Public Shared ItemCustomize As New UIMenuItem(GetLangEntry("BTN_CUSTOMIZE"))
    Public Shared ItenConfirm As New UIMenuItem(GetLangEntry("BTN_CONFIRM"))
    Public Shared ItemColor As New UIMenuItem(GetLangEntry("BTN_COLOR_NAME"), GetLangEntry("BTN_COLOR_DESC"))
    Public Shared ItemClassicColor As New UIMenuItem(GetLangEntry("BTN_CLASSIC_COLOR"), GetLangEntry("BTN_COLOR_DESC"))
    Public Shared ItemClassicColor2 As New UIMenuItem(GetLangEntry("BTN_CLASSIC_COLOR"), GetLangEntry("BTN_COLOR_DESC"))
    Public Shared ItemMetallicColor As New UIMenuItem(GetLangEntry("BTN_METALLIC_COLOR"), GetLangEntry("BTN_COLOR_DESC"))
    Public Shared ItemMetallicColor2 As New UIMenuItem(GetLangEntry("BTN_METALLIC_COLOR"), GetLangEntry("BTN_COLOR_DESC"))
    Public Shared ItemPearlColor As New UIMenuItem(GetLangEntry("BTN_PEARL_COLOR"), GetLangEntry("BTN_COLOR_DESC"))
    Public Shared ItemPearlColor2 As New UIMenuItem(GetLangEntry("BTN_PEARL_COLOR"), GetLangEntry("BTN_COLOR_DESC"))
    Public Shared ItemMetalColor As New UIMenuItem(GetLangEntry("BTN_METAL_COLOR"), GetLangEntry("BTN_COLOR_DESC"))
    Public Shared ItemMetalColor2 As New UIMenuItem(GetLangEntry("BTN_METAL_COLOR"), GetLangEntry("BTN_COLOR_DESC"))
    Public Shared ItemMatteColor As New UIMenuItem(GetLangEntry("BTN_MATTE_COLOR"), GetLangEntry("BTN_COLOR_DESC"))
    Public Shared ItemMatteColor2 As New UIMenuItem(GetLangEntry("BTN_MATTE_COLOR"), GetLangEntry("BTN_COLOR_DESC"))
    Public Shared ItemChromeColor As New UIMenuItem(GetLangEntry("BTN_CHROME_COLOR"), GetLangEntry("BTN_COLOR_DESC"))
    Public Shared ItemChromeColor2 As New UIMenuItem(GetLangEntry("BTN_CHROME_COLOR"), GetLangEntry("BTN_COLOR_DESC"))
    Public Shared ItemCPriColor As New UIMenuItem(GetLangEntry("BTN_CUSTOM_PRIMARY"), GetLangEntry("BTN_COLOR_DESC"))
    Public Shared ItemCSecColor As New UIMenuItem(GetLangEntry("BTN_CUSTOM_SECONDARY"), GetLangEntry("BTN_COLOR_DESC"))
    Public Shared ItemPriColor As New UIMenuItem(GetLangEntry("BTN_PRIMARY_COLOR"), GetLangEntry("BTN_COLOR_DESC"))
    Public Shared ItemSecColor As New UIMenuItem(GetLangEntry("BTN_SECONDARY_COLOR"), GetLangEntry("BTN_COLOR_DESC"))
    Public Shared ItemPeaColor As New UIMenuItem(GetLangEntry("BTN_PEARLESCENT_COLOR"), GetLangEntry("BTN_COLOR_DESC"))
    Public Shared ItemPlate As New UIMenuItem(GetLangEntry("BTN_PLATE_STYLE"), GetLangEntry("BTN_COLOR_DESC"))

    Public Shared BtnRotLeft As New InstructionalButton(GTA.Control.ParachuteBrakeLeft, GetLangEntry("BTN_ROTATE_LEFT"))
    Public Shared BtnRotRight As New InstructionalButton(GTA.Control.ParachuteBrakeRight, GetLangEntry("BTN_ROTATE_RIGHT"))
    Public Shared BtnOpenDoor As New InstructionalButton(GTA.Control.SelectWeaponUnarmed, GetLangEntry("BTN_OPEN_DOOR"))
    Public Shared BtnCloseDoor As New InstructionalButton(GTA.Control.SelectWeaponMelee, GetLangEntry("BTN_CLOSE_DOOR"))
    Public Shared BtnChangeCam As New InstructionalButton(GTA.Control.NextCamera, GetLangEntry("BTN_CHANGE_CAM"))

    Public Shared MotorcycleFile As String = Application.StartupPath & "\scripts\PremiumDeluxeMotorsport\Vehicles\motorcycle.ini"
    Public Shared CompactFile As String = Application.StartupPath & "\scripts\PremiumDeluxeMotorsport\Vehicles\compact.ini"
    Public Shared CoupeFile As String = Application.StartupPath & "\scripts\PremiumDeluxeMotorsport\Vehicles\coupe.ini"
    Public Shared SedanFile As String = Application.StartupPath & "\scripts\PremiumDeluxeMotorsport\Vehicles\sedan.ini"
    Public Shared SportFile As String = Application.StartupPath & "\scripts\PremiumDeluxeMotorsport\Vehicles\sport.ini"
    Public Shared ClassicFile As String = Application.StartupPath & "\scripts\PremiumDeluxeMotorsport\Vehicles\classic.ini"
    Public Shared ExoticFile As String = Application.StartupPath & "\scripts\PremiumDeluxeMotorsport\Vehicles\exotic.ini"
    Public Shared MuscleFile As String = Application.StartupPath & "\scripts\PremiumDeluxeMotorsport\Vehicles\muscle.ini"
    Public Shared OffRoadFile As String = Application.StartupPath & "\scripts\PremiumDeluxeMotorsport\Vehicles\offroad.ini"
    Public Shared SUVFile As String = Application.StartupPath & "\scripts\PremiumDeluxeMotorsport\Vehicles\suv.ini"
    Public Shared VanFile As String = Application.StartupPath & "\scripts\PremiumDeluxeMotorsport\Vehicles\van.ini"
    Public Shared UtilityFile As String = Application.StartupPath & "\scripts\PremiumDeluxeMotorsport\Vehicles\utility.ini"
    Public Shared ArmouredFile As String = Application.StartupPath & "\scripts\PremiumDeluxeMotorsport\Vehicles\armoured.ini"
    Public Shared LowriderFile As String = Application.StartupPath & "\scripts\PremiumDeluxeMotorsport\Vehicles\lowrider.ini"
    Public Shared AddOnFile As String = Application.StartupPath & "\scripts\PremiumDeluxeMotorsport\Vehicles\addon.ini"
    Public Shared Parameters As String() = {"[name]", "[price]", "[model]", "[gxt]", "[make]"}
    Public Shared CustomColour As String = Application.StartupPath & "\scripts\PremiumDeluxeMotorsport\Customization\custom_color.ini"
    Public Shared ClassicColor As String = Application.StartupPath & "\scripts\PremiumDeluxeMotorsport\Customization\classic_color.ini"
    Public Shared MetallicColor As String = Application.StartupPath & "\scripts\PremiumDeluxeMotorsport\Customization\metallic_color.ini"
    Public Shared PearlColor As String = Application.StartupPath & "\scripts\PremiumDeluxeMotorsport\Customization\pearl_color.ini"
    Public Shared MetalColor As String = Application.StartupPath & "\scripts\PremiumDeluxeMotorsport\Customization\metal_color.ini"
    Public Shared MatteColor As String = Application.StartupPath & "\scripts\PremiumDeluxeMotorsport\Customization\matte_color.ini"
    Public Shared ChromeColor As String = Application.StartupPath & "\scripts\PremiumDeluxeMotorsport\Customization\chrome_color.ini"
    Public Shared PearlescentColor As String = Application.StartupPath & "\scripts\PremiumDeluxeMotorsport\Customization\pearlescent_color.ini"
    Public Shared Plate As String = Application.StartupPath & "\scripts\PremiumDeluxeMotorsport\Customization\plate.ini"
    Public Shared ParaCColors As String() = {"[red]", "[green]", "[blue]", "[name]"}
    Public Shared ParaColors As String() = {"[code]", "[type]", "[name]"}
    Public Shared ParaPlates As String() = {"[index]", "[name]"}

    Public Shared _menuPool As MenuPool
    Public Shared TaskScriptStatus As Integer = -1
    Public Shared Rectangle = New UIResRectangle()

    Public Sub New()
        Try
            VC_MOTORCYCLE = GetLangEntry("VC_MOTORCYCLE")
            VC_COMPACT = GetLangEntry("VC_COMPACT")
            VC_COUPE = GetLangEntry("VC_COUPE")
            VC_SEDAN = GetLangEntry("VC_SEDAN")
            VC_SPORT = GetLangEntry("VC_SPORT")
            VC_CLASSIC = GetLangEntry("VC_CLASSIC")
            VC_SUPER = GetLangEntry("VC_SUPER")
            VC_MUSCLE = GetLangEntry("VC_MUSCLE")
            VC_OFF_ROAD = GetLangEntry("VC_OFF_ROAD")
            VC_SUV = GetLangEntry("VC_SUV")
            VC_VAN = GetLangEntry("VC_VAN")
            VC_UTILITY = GetLangEntry("VC_UTILITY")
            VC_INDUSTRIAL = GetLangEntry("VC_INDUSTRIAL")
            VC_BICYCLE = GetLangEntry("VC_BICYCLE")
            VC_BOAT = GetLangEntry("VC_BOAT")
            VC_HELI = GetLangEntry("VC_HELI")
            VC_PLANE = GetLangEntry("VC_PLANE")
            VC_SERVICE = GetLangEntry("VC_SERVICE")
            VC_EMERGENCY = GetLangEntry("VC_EMERGENCY")
            VC_MILITARY = GetLangEntry("VC_MILITARY")
            VC_COMMERCIAL = GetLangEntry("VC_COMMERCIAL")


            playerHash = Game.Player.Character.Model.GetHashCode().ToString
            GP = Game.Player
            GPC = Game.Player.Character
            Select Case playerHash
                Case "1885233650"
                    PlayerCash = 1999999999
                Case "-1667301416"
                    PlayerCash = 1999999999
                Case Else
                    PlayerCash = Game.Player.Money
            End Select

            AddHandler Tick, AddressOf OnTick

            Dim AddOnCars As String = ReadCfgValue("AddOnCars", ".\Scripts\PremiumDeluxeMotorsport\config.ini")
            Dim RemoveColor As String = ReadCfgValue("ClearColor", ".\Scripts\PremiumDeluxeMotorsport\config.ini")
            Dim Showroom As String = ReadCfgValue("ShowRoom", ".\Scripts\PremiumDeluxeMotorsport\config.ini")
            Dim RemoveImg As String = ReadCfgValue("RemoveImg", ".\Scripts\PremiumDeluxeMotorsport\config.ini")
            If AddOnCars = "True" Then My.Settings.AddOnCars = True Else My.Settings.AddOnCars = False
            If RemoveColor = "True" Then My.Settings.RemoveColor = True Else My.Settings.RemoveColor = False
            If Showroom = "True" Then My.Settings.ShowRoom = True Else My.Settings.ShowRoom = False
            If RemoveImg = "False" Then My.Settings.RemoveImg = True Else My.Settings.RemoveImg = False
            My.Settings.LastVehHash = ReadCfgValue("LastVehHash", ".\Scripts\PremiumDeluxeMotorsport\config.ini")
            My.Settings.LastVehName = ReadCfgValue("LastVehName", ".\Scripts\PremiumDeluxeMotorsport\config.ini")
            My.Settings.Save()

            _menuPool = New MenuPool()

            Rectangle.Color = Color.FromArgb(0, 0, 0, 0)
            ReadMainMenu()
            ReadVehicles(MotorcycleFile, MotorMenu, "~r~" & GetLangEntry("VC_MOTORCYCLE").ToUpper, ItemMotor)
            ReadVehicles(CompactFile, CompactMenu, "~r~" & GetLangEntry("VC_COMPACT").ToUpper, ItemCompact)
            ReadVehicles(CoupeFile, CoupeMenu, "~r~" & GetLangEntry("VC_COUPE").ToUpper, ItemCoupe)
            ReadVehicles(SedanFile, SedanMenu, "~r~" & GetLangEntry("VC_SEDAN").ToUpper, ItemSedan)
            ReadVehicles(SportFile, SportMenu, "~r~" & GetLangEntry("VC_SPORT").ToUpper, ItemSport)
            ReadVehicles(ClassicFile, ClassicMenu, "~r~" & GetLangEntry("VC_CLASSIC").ToUpper, ItemClassic)
            ReadVehicles(ExoticFile, ExoticMenu, "~r~" & GetLangEntry("VC_SUPER").ToUpper, ItemExotic)
            ReadVehicles(MuscleFile, MuscleMenu, "~r~" & GetLangEntry("VC_MUSCLE").ToUpper, ItemMuscle)
            ReadVehicles(OffRoadFile, OffRoadMenu, "~r~" & GetLangEntry("VC_OFF_ROAD").ToUpper, ItemOffRoad)
            ReadVehicles(SUVFile, SuvMenu, "~r~" & GetLangEntry("VC_SUV").ToUpper, ItemSUV)
            ReadVehicles(VanFile, VanMenu, "~r~" & GetLangEntry("VC_VAN").ToUpper, ItemVan)
            ReadVehicles(UtilityFile, UtilMenu, "~r~" & GetLangEntry("VC_UTILITY").ToUpper, ItemUtil)
            ReadVehicles(ArmouredFile, ArmourMenu, "~r~" & GetLangEntry("VC_ARMOUR").ToUpper, ItemArmoured)
            ReadVehicles(LowriderFile, LowRiderMenu, "~r~" & GetLangEntry("VC_LOWRIDER").ToUpper, ItemLowRider)
            If My.Settings.AddOnCars = True Then
                ReadVehicles2(AddOnFile, AddOnMenu, "~r~" & GetLangEntry("VC_ADD_ON").ToUpper, ItemAddOn)
            End If

            ReadConfirm()
            ReadCustomize()
            ReadColorCategory()
            ReadPlate()
            ReadColorPrimary()
            ReadColorSecondary()
            ReadCustomPrimaryColor()
            ReadCustomSecondaryColor()
            ReadColorsPrimary(ClassicColor, ClassicColorMenu, "~r~" & GetLangEntry("BTN_CLASSIC_COLOR"), ItemClassicColor)
            ReadColorsPrimary(MetallicColor, MetallicColorMenu, "~r~" & GetLangEntry("BTN_METALLIC_COLOR"), ItemMetallicColor)
            ReadColorsPrimary(PearlColor, PearlColorMenu, "~r~" & GetLangEntry("BTN_PEARL_COLOR"), ItemPearlColor)
            ReadColorsPrimary(MetalColor, MetalColorMenu, "~r~" & GetLangEntry("BTN_METAL_COLOR"), ItemMetalColor)
            ReadColorsPrimary(MatteColor, MatteColorMenu, "~r~" & GetLangEntry("BTN_MATTE_COLOR"), ItemMatteColor)
            ReadColorsPrimary(ChromeColor, ChromeColorMenu, "~r~" & GetLangEntry("BTN_CHROME_COLOR"), ItemChromeColor)
            ReadPearlescentColor()
            ReadColorsSecondary(ClassicColor, ClassicColorMenu2, "~r~" & GetLangEntry("BTN_CLASSIC_COLOR"), ItemClassicColor2)
            ReadColorsSecondary(MetallicColor, MetallicColorMenu2, "~r~" & GetLangEntry("BTN_METALLIC_COLOR"), ItemMetallicColor2)
            ReadColorsSecondary(PearlColor, PearlColorMenu2, "~r~" & GetLangEntry("BTN_PEARL_COLOR"), ItemPearlColor2)
            ReadColorsSecondary(MetalColor, MetalColorMenu2, "~r~" & GetLangEntry("BTN_METAL_COLOR"), ItemMetalColor2)
            ReadColorsSecondary(MatteColor, MatteColorMenu2, "~r~" & GetLangEntry("BTN_MATTE_COLOR"), ItemMatteColor2)
            ReadColorsSecondary(ChromeColor, ChromeColorMenu2, "~r~" & GetLangEntry("BTN_CHROME_COLOR"), ItemChromeColor2)

            If My.Settings.ShowRoom = False Then
                'Outside
                VehPreviewPos = New Vector3(-56.79958, -1110.868, 26.43581)
                CameraPos = New Vector3(-65.65164, -1107.566, 27.75188)
                CameraRot = New Vector3(-9.58597, 0, -104.5207)
                CameraFOV = 35.0
                PlayerPos = New Vector3(-38.40855, -1109.074, 26.43719)
                PlayerHeading = 250.6701
            Else
                'Inside
                VehPreviewPos = New Vector3(-44.45501, -1096.976, 26.42235)
                CameraPos = New Vector3(-47.45673, -1101.28, 27.54757)
                CameraRot = New Vector3(-18.12634, 0, -26.97177)
                CameraFOV = 50.0
                PlayerPos = New Vector3(-51.56961, -1102.29, 26.42234)
                PlayerHeading = 250.6701
                ToggleIPL("shr_int", "fakeint")
                LoadMissingProps()
            End If

            CreateEntrance()
        Catch ex As Exception
            logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateEntrance()
        PdmDoor = New Vector3(-40.3857, -1108.79, 25.4375)
        testDriveVector = New Vector3(66.55125, -1356.585, 29.08711)
        PdmBlip = World.CreateBlip(PdmDoor)
        PdmBlip.Sprite = BlipSprite.PersonalVehicleCar
        PdmBlip.Color = BlipColor.Red
        PdmBlip.IsShortRange = True
        PdmBlip.Name = GetLangEntry("PREMIUM_DELUXE_MOTORSPORT")
    End Sub

    Public Shared Sub ReadMainMenu()

        If My.Settings.RemoveImg = True Then
            MainMenu = New UIMenu("", "~r~" & GetLangEntry("CATEGORY"))
            MainMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PremiumDeluxeMotorsportCarDealership3.shopui_title_pdm.png"))
        Else
            MainMenu = New UIMenu("", "~r~" & GetLangEntry("CATEGORY"), New Point(0, -107))
            MainMenu.SetBannerType(Rectangle)
        End If
        _menuPool.Add(MainMenu)
        MainMenu.AddItem(ItemMotor)
        MainMenu.AddItem(ItemCompact)
        MainMenu.AddItem(ItemCoupe)
        MainMenu.AddItem(ItemSedan)
        MainMenu.AddItem(ItemSport)
        MainMenu.AddItem(ItemClassic)
        MainMenu.AddItem(ItemExotic)
        MainMenu.AddItem(ItemMuscle)
        MainMenu.AddItem(ItemOffRoad)
        MainMenu.AddItem(ItemSUV)
        MainMenu.AddItem(ItemVan)
        MainMenu.AddItem(ItemUtil)
        MainMenu.AddItem(ItemArmoured)
        MainMenu.AddItem(ItemLowRider)
        If My.Settings.AddOnCars = True Then
            MainMenu.AddItem(ItemAddOn)
        End If
        MainMenu.RefreshIndex()
        AddHandler MainMenu.OnMenuClose, AddressOf MenuCloseHandler
    End Sub

    Public Shared Sub ReadVehicles(File As String, MenuCategory As UIMenu, Subtitle As String, MenuItem As UIMenuItem)
        Dim Format As New Reader(File, Parameters)
        If My.Settings.RemoveImg = True Then
            MenuCategory = New UIMenu("", Subtitle)
            MenuCategory.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PremiumDeluxeMotorsportCarDealership3.shopui_title_pdm.png"))
        Else
            MenuCategory = New UIMenu("", Subtitle, New Point(0, -107))
            MenuCategory.SetBannerType(Rectangle)
        End If
        _menuPool.Add(MenuCategory)
        MenuCategory.AddInstructionalButton(BtnRotLeft)
        MenuCategory.AddInstructionalButton(BtnRotRight)
        MenuCategory.AddInstructionalButton(BtnOpenDoor)
        MenuCategory.AddInstructionalButton(BtnCloseDoor)
        MenuCategory.AddInstructionalButton(BtnChangeCam)
        For i As Integer = 0 To Format.Count - 1
            Price = Format(i)("price")
            Dim item As New UIMenuItem(GetGXTEntry(Format(i)("make")) & " " & GetGXTEntry(Format(i)("gxt")))
            MenuCategory.AddItem(item)
            With item
                .SetRightLabel("$" & Price.ToString("###,###"))
                .SubString1 = Format(i)("model")
                .SubInteger1 = Format(i)("price")
                .SubString2 = GetGXTEntry(Format(i)("make")) & " " & GetGXTEntry(Format(i)("gxt"))
            End With
        Next
        MenuCategory.RefreshIndex()
        MainMenu.BindMenuToItem(MenuCategory, MenuItem)
        AddHandler MenuCategory.OnItemSelect, AddressOf VehicleSelectHandler
        AddHandler MenuCategory.OnIndexChange, AddressOf VehicleChangeHandler
    End Sub

    Public Shared Sub ReadVehicles2(File As String, MenuCategory As UIMenu, Subtitle As String, MenuItem As UIMenuItem)
        Dim Format As New Reader(File, Parameters)
        If My.Settings.RemoveImg = True Then
            MenuCategory = New UIMenu("", Subtitle)
            MenuCategory.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PremiumDeluxeMotorsportCarDealership3.shopui_title_pdm.png"))
        Else
            MenuCategory = New UIMenu("", Subtitle, New Point(0, -107))
            MenuCategory.SetBannerType(Rectangle)
        End If
        _menuPool.Add(MenuCategory)
        MenuCategory.AddInstructionalButton(BtnRotLeft)
        MenuCategory.AddInstructionalButton(BtnRotRight)
        MenuCategory.AddInstructionalButton(BtnOpenDoor)
        MenuCategory.AddInstructionalButton(BtnCloseDoor)
        MenuCategory.AddInstructionalButton(BtnChangeCam)
        For i As Integer = 0 To Format.Count - 1
            Price = Format(i)("price")
            Dim item As New UIMenuItem(Format(i)("name"))
            MenuCategory.AddItem(item)
            With item
                .SetRightLabel("$" & Price.ToString("###,###"))
                .SubString1 = Format(i)("model")
                .SubInteger1 = Format(i)("price")
                .SubString2 = Format(i)("name")
            End With
        Next
        MenuCategory.RefreshIndex()
        MainMenu.BindMenuToItem(MenuCategory, MenuItem)
        AddHandler MenuCategory.OnItemSelect, AddressOf VehicleSelectHandler
        AddHandler MenuCategory.OnIndexChange, AddressOf VehicleChangeHandler
    End Sub

    Public Shared Sub ReadCustomize()
        If My.Settings.RemoveImg = True Then
            CustomiseMenu = New UIMenu("", "~r~" & GetLangEntry("BTN_CUSTOMIZE"))
            CustomiseMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PremiumDeluxeMotorsportCarDealership3.shopui_title_pdm.png"))
        Else
            CustomiseMenu = New UIMenu("", "~r~" & GetLangEntry("BTN_CUSTOMIZE"), New Point(0, -107))
            CustomiseMenu.SetBannerType(Rectangle)
        End If
        _menuPool.Add(CustomiseMenu)
        CustomiseMenu.AddInstructionalButton(BtnRotLeft)
        CustomiseMenu.AddInstructionalButton(BtnRotRight)
        CustomiseMenu.AddInstructionalButton(BtnOpenDoor)
        CustomiseMenu.AddInstructionalButton(BtnCloseDoor)
        CustomiseMenu.AddInstructionalButton(BtnChangeCam)
        CustomiseMenu.AddItem(ItemColor)
        CustomiseMenu.AddItem(New UIMenuItem(GetLangEntry("BTN_UPGRADE_NAME"), GetLangEntry("BTN_UPGRADE_DESC")))
        CustomiseMenu.AddItem(New UIMenuItem(GetLangEntry("BTN_PLATE_NUMBER_NAME"), GetLangEntry("BTN_PLATE_NUMBER_DESC")))
        CustomiseMenu.AddItem(ItemPlate)
        CustomiseMenu.RefreshIndex()
        ConfirmMenu.BindMenuToItem(CustomiseMenu, ItemCustomize)
        AddHandler CustomiseMenu.OnItemSelect, AddressOf ItemSelectHandler
    End Sub

    'Public Shared Sub ReadConfirm2()
    '    If My.Settings.RemoveImg = True Then
    '        ConfirmMenu2 = New UIMenu("", "~r~" & GetLangEntry("BTN_CONFIRM").ToUpper)
    '        ConfirmMenu2.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PremiumDeluxeMotorsportCarDealership3.shopui_title_pdm.png"))
    '    Else
    '        ConfirmMenu2 = New UIMenu("", "~r~" & GetLangEntry("BTN_CONFIRM").ToUpper, New Point(0, -107))
    '        ConfirmMenu2.SetBannerType(Rectangle)
    '    End If
    '    _menuPool.Add(ConfirmMenu2)
    '    CustomiseMenu.AddInstructionalButton(BtnRotLeft)
    '    CustomiseMenu.AddInstructionalButton(BtnRotRight)
    '    CustomiseMenu.AddInstructionalButton(BtnOpenDoor)
    '    CustomiseMenu.AddInstructionalButton(BtnCloseDoor)
    '    CustomiseMenu.AddInstructionalButton(BtnChangeCam)
    '    ConfirmMenu2.AddItem(New UIMenuItem("Drive"))
    '    ConfirmMenu2.AddItem(New UIMenuItem("Delivery to Garage", "Deliver your New Vehicle to your Apartment."))
    'End Sub

    Public Shared Sub ReadColorCategory()
        If My.Settings.RemoveImg = True Then
            ColorMenu = New UIMenu("", "~r~" & GetLangEntry("BTN_COLOR_NAME").ToUpper)
            ColorMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PremiumDeluxeMotorsportCarDealership3.shopui_title_pdm.png"))
        Else
            ColorMenu = New UIMenu("", "~r~" & GetLangEntry("BTN_COLOR_NAME").ToUpper, New Point(0, -107))
            ColorMenu.SetBannerType(Rectangle)
        End If
        _menuPool.Add(ColorMenu)
        ColorMenu.AddInstructionalButton(BtnRotLeft)
        ColorMenu.AddInstructionalButton(BtnRotRight)
        ColorMenu.AddInstructionalButton(BtnOpenDoor)
        ColorMenu.AddInstructionalButton(BtnCloseDoor)
        ColorMenu.AddInstructionalButton(BtnChangeCam)
        ColorMenu.AddItem(ItemPriColor)
        ColorMenu.AddItem(ItemSecColor)
        ColorMenu.AddItem(ItemCPriColor)
        ColorMenu.AddItem(ItemCSecColor)
        ColorMenu.RefreshIndex()
        CustomiseMenu.BindMenuToItem(ColorMenu, ItemColor)
    End Sub

    Public Shared Sub ReadCustomPrimaryColor()
        Dim Format As New Reader(CustomColour, ParaCColors)
        If My.Settings.RemoveImg = True Then
            CPriColorMenu = New UIMenu("", "~r~" & GetLangEntry("BTN_CUSTOM_PRIMARY").ToUpper)
            CPriColorMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PremiumDeluxeMotorsportCarDealership3.shopui_title_pdm.png"))
        Else
            CPriColorMenu = New UIMenu("", "~r~" & GetLangEntry("BTN_CUSTOM_PRIMARY").ToUpper, New Point(0, -107))
            CPriColorMenu.SetBannerType(Rectangle)
        End If
        _menuPool.Add(CPriColorMenu)
        CPriColorMenu.AddInstructionalButton(BtnRotLeft)
        CPriColorMenu.AddInstructionalButton(BtnRotRight)
        CPriColorMenu.AddInstructionalButton(BtnOpenDoor)
        CPriColorMenu.AddInstructionalButton(BtnCloseDoor)
        CPriColorMenu.AddInstructionalButton(BtnChangeCam)
        For i As Integer = 0 To Format.Count - 1
            Dim item As New UIMenuItem(Format(i)("name"))
            CPriColorMenu.AddItem(item)
            With item
                .SubInteger1 = Format(i)("red")
                .SubString2 = Format(i)("green")
                .SubString1 = Format(i)("blue")
            End With
        Next
        CPriColorMenu.RefreshIndex()
        ColorMenu.BindMenuToItem(CPriColorMenu, ItemCPriColor)
        AddHandler CPriColorMenu.OnItemSelect, AddressOf ColorSelectHandler
        AddHandler CPriColorMenu.OnIndexChange, AddressOf CPriColorChangeHandler
    End Sub

    Public Shared Sub ReadCustomSecondaryColor()
        Dim Format As New Reader(CustomColour, ParaCColors)
        If My.Settings.RemoveImg = True Then
            CSecColorMenu = New UIMenu("", "~r~" & GetLangEntry("BTN_CUSTOM_SECONDARY").ToUpper)
            CSecColorMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PremiumDeluxeMotorsportCarDealership3.shopui_title_pdm.png"))
        Else
            CSecColorMenu = New UIMenu("", "~r~" & GetLangEntry("BTN_CUSTOM_SECONDARY").ToUpper, New Point(0, -107))
            CSecColorMenu.SetBannerType(Rectangle)
        End If
        _menuPool.Add(CSecColorMenu)
        CSecColorMenu.AddInstructionalButton(BtnRotLeft)
        CSecColorMenu.AddInstructionalButton(BtnRotRight)
        CSecColorMenu.AddInstructionalButton(BtnOpenDoor)
        CSecColorMenu.AddInstructionalButton(BtnCloseDoor)
        CSecColorMenu.AddInstructionalButton(BtnChangeCam)
        For i As Integer = 0 To Format.Count - 1
            Dim item As New UIMenuItem(Format(i)("name"))
            CSecColorMenu.AddItem(item)
            With item
                .SubInteger1 = Format(i)("red")
                .SubString2 = Format(i)("green")
                .SubString1 = Format(i)("blue")
            End With
        Next
        CSecColorMenu.RefreshIndex()
        ColorMenu.BindMenuToItem(CSecColorMenu, ItemCSecColor)
        AddHandler CSecColorMenu.OnItemSelect, AddressOf ColorSelectHandler
        AddHandler CSecColorMenu.OnIndexChange, AddressOf CSecColorChangeHandler
    End Sub

    Public Shared Sub ReadColorPrimary()
        If My.Settings.RemoveImg = True Then
            PriColorMenu = New UIMenu("", "~r~" & GetLangEntry("BTN_PRIMARY_COLOR").ToUpper)
            PriColorMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PremiumDeluxeMotorsportCarDealership3.shopui_title_pdm.png"))
        Else
            PriColorMenu = New UIMenu("", "~r~" & GetLangEntry("BTN_PRIMARY_COLOR").ToUpper, New Point(0, -107))
            PriColorMenu.SetBannerType(Rectangle)
        End If
        _menuPool.Add(PriColorMenu)
        PriColorMenu.AddInstructionalButton(BtnRotLeft)
        PriColorMenu.AddInstructionalButton(BtnRotRight)
        PriColorMenu.AddInstructionalButton(BtnOpenDoor)
        PriColorMenu.AddInstructionalButton(BtnCloseDoor)
        PriColorMenu.AddInstructionalButton(BtnChangeCam)
        PriColorMenu.AddItem(ItemClassicColor)
        PriColorMenu.AddItem(ItemMetallicColor)
        PriColorMenu.AddItem(ItemPearlColor)
        PriColorMenu.AddItem(ItemMatteColor)
        PriColorMenu.AddItem(ItemMetalColor)
        PriColorMenu.AddItem(ItemChromeColor)
        PriColorMenu.AddItem(ItemPeaColor)
        PriColorMenu.RefreshIndex()
        ColorMenu.BindMenuToItem(PriColorMenu, ItemPriColor)
    End Sub

    Public Shared Sub ReadColorSecondary()
        If My.Settings.RemoveImg = True Then
            SecColorMenu = New UIMenu("", "~r~" & GetLangEntry("BTN_SECONDARY_COLOR").ToUpper)
            SecColorMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PremiumDeluxeMotorsportCarDealership3.shopui_title_pdm.png"))
        Else
            SecColorMenu = New UIMenu("", "~r~" & GetLangEntry("BTN_SECONDARY_COLOR").ToUpper, New Point(0, -107))
            SecColorMenu.SetBannerType(Rectangle)
        End If
        _menuPool.Add(SecColorMenu)
        SecColorMenu.AddInstructionalButton(BtnRotLeft)
        SecColorMenu.AddInstructionalButton(BtnRotRight)
        SecColorMenu.AddInstructionalButton(BtnOpenDoor)
        SecColorMenu.AddInstructionalButton(BtnCloseDoor)
        SecColorMenu.AddInstructionalButton(BtnChangeCam)
        SecColorMenu.AddItem(ItemClassicColor2)
        SecColorMenu.AddItem(ItemMetallicColor2)
        SecColorMenu.AddItem(ItemPearlColor2)
        SecColorMenu.AddItem(ItemMatteColor2)
        SecColorMenu.AddItem(ItemMetalColor2)
        SecColorMenu.AddItem(ItemChromeColor2)
        SecColorMenu.RefreshIndex()
        ColorMenu.BindMenuToItem(SecColorMenu, ItemSecColor)
    End Sub

    Public Shared Sub ReadColorsPrimary(File As String, MenuCategory As UIMenu, Subtitle As String, MenuItem As UIMenuItem)
        Dim Format As New Reader(File, ParaColors)
        If My.Settings.RemoveImg = True Then
            MenuCategory = New UIMenu("", Subtitle)
            MenuCategory.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PremiumDeluxeMotorsportCarDealership3.shopui_title_pdm.png"))
        Else
            MenuCategory = New UIMenu("", Subtitle, New Point(0, -107))
            MenuCategory.SetBannerType(Rectangle)
        End If
        _menuPool.Add(MenuCategory)
        MenuCategory.AddInstructionalButton(BtnRotLeft)
        MenuCategory.AddInstructionalButton(BtnRotRight)
        MenuCategory.AddInstructionalButton(BtnOpenDoor)
        MenuCategory.AddInstructionalButton(BtnCloseDoor)
        MenuCategory.AddInstructionalButton(BtnChangeCam)
        For i As Integer = 0 To Format.Count - 1
            Dim item As New UIMenuItem(Format(i)("name"))
            MenuCategory.AddItem(item)
            With item
                .SubInteger1 = Format(i)("code")
                .SubString2 = Format(i)("type")
            End With
        Next
        MenuCategory.RefreshIndex()
        PriColorMenu.BindMenuToItem(MenuCategory, MenuItem)
        AddHandler MenuCategory.OnItemSelect, AddressOf ColorSelectHandler
        AddHandler MenuCategory.OnIndexChange, AddressOf PriColorChangeHandler
    End Sub

    Public Shared Sub ReadColorsSecondary(File As String, MenuCategory As UIMenu, Subtitle As String, MenuItem As UIMenuItem)
        Dim Format As New Reader(File, ParaColors)
        If My.Settings.RemoveImg = True Then
            MenuCategory = New UIMenu("", Subtitle)
            MenuCategory.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PremiumDeluxeMotorsportCarDealership3.shopui_title_pdm.png"))
        Else
            MenuCategory = New UIMenu("", Subtitle, New Point(0, -107))
            MenuCategory.SetBannerType(Rectangle)
        End If
        _menuPool.Add(MenuCategory)
        MenuCategory.AddInstructionalButton(BtnRotLeft)
        MenuCategory.AddInstructionalButton(BtnRotRight)
        MenuCategory.AddInstructionalButton(BtnOpenDoor)
        MenuCategory.AddInstructionalButton(BtnCloseDoor)
        MenuCategory.AddInstructionalButton(BtnChangeCam)
        For i As Integer = 0 To Format.Count - 1
            Dim item As New UIMenuItem(Format(i)("name"))
            MenuCategory.AddItem(item)
            With item
                .SubInteger1 = Format(i)("code")
                .SubString2 = Format(i)("type")
            End With
        Next
        MenuCategory.RefreshIndex()
        SecColorMenu.BindMenuToItem(MenuCategory, MenuItem)
        AddHandler MenuCategory.OnItemSelect, AddressOf ColorSelectHandler
        AddHandler MenuCategory.OnIndexChange, AddressOf SecColorChangeHandler
    End Sub

    Public Shared Sub ReadPearlescentColor()
        Dim Format As New Reader(PearlescentColor, ParaColors)
        If My.Settings.RemoveImg = True Then
            PeaColorMenu = New UIMenu("", "~r~" & GetLangEntry("BTN_PEARLESCENT_COLOR").ToUpper)
            PeaColorMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PremiumDeluxeMotorsportCarDealership3.shopui_title_pdm.png"))
        Else
            PeaColorMenu = New UIMenu("", "~r~" & GetLangEntry("BTN_PEARLESCENT_COLOR").ToUpper, New Point(0, -107))
            PeaColorMenu.SetBannerType(Rectangle)
        End If
        _menuPool.Add(PeaColorMenu)
        PeaColorMenu.AddInstructionalButton(BtnRotLeft)
        PeaColorMenu.AddInstructionalButton(BtnRotRight)
        PeaColorMenu.AddInstructionalButton(BtnOpenDoor)
        PeaColorMenu.AddInstructionalButton(BtnCloseDoor)
        PeaColorMenu.AddInstructionalButton(BtnChangeCam)
        For i As Integer = 0 To Format.Count - 1
            Dim item As New UIMenuItem(Format(i)("name"))
            PeaColorMenu.AddItem(item)
            With item
                .SubInteger1 = Format(i)("code")
                .SubString2 = Format(i)("type")
            End With
        Next
        PeaColorMenu.RefreshIndex()
        PriColorMenu.BindMenuToItem(PeaColorMenu, ItemPeaColor)
        AddHandler PeaColorMenu.OnItemSelect, AddressOf ColorSelectHandler
        AddHandler PeaColorMenu.OnIndexChange, AddressOf PeaColorChangeHandler
    End Sub

    Public Shared Sub ReadPlate()
        Dim Format As New Reader(Plate, ParaPlates)
        If My.Settings.RemoveImg = True Then
            PlateMenu = New UIMenu("", "~r~" & GetLangEntry("BTN_PLATE_STYLE").ToUpper)
            PlateMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PremiumDeluxeMotorsportCarDealership3.shopui_title_pdm.png"))
        Else
            PlateMenu = New UIMenu("", "~r~" & GetLangEntry("BTN_PLATE_STYLE").ToUpper, New Point(0, -107))
            PlateMenu.SetBannerType(Rectangle)
        End If
        _menuPool.Add(PlateMenu)
        PlateMenu.AddInstructionalButton(BtnRotLeft)
        PlateMenu.AddInstructionalButton(BtnRotRight)
        PlateMenu.AddInstructionalButton(BtnOpenDoor)
        PlateMenu.AddInstructionalButton(BtnCloseDoor)
        PlateMenu.AddInstructionalButton(BtnChangeCam)
        For i As Integer = 0 To Format.Count - 1
            Dim item As New UIMenuItem(Format(i)("name"))
            PlateMenu.AddItem(item)
            With item
                .SubString2 = Format(i)("name")
                .SubInteger1 = Format(i)("index")
            End With
        Next
        PlateMenu.RefreshIndex()
        CustomiseMenu.BindMenuToItem(PlateMenu, ItemPlate)
        AddHandler PlateMenu.OnItemSelect, AddressOf ColorSelectHandler
        AddHandler PlateMenu.OnIndexChange, AddressOf PlateItemChangeHandler
    End Sub

    Public Shared Sub ReadConfirm()
        If My.Settings.RemoveImg = True Then
            ConfirmMenu = New UIMenu("", "~r~" & GetLangEntry("PURCHASE_ORDER"))
            ConfirmMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PremiumDeluxeMotorsportCarDealership3.shopui_title_pdm.png"))
        Else
            ConfirmMenu = New UIMenu("", "~r~" & GetLangEntry("PURCHASE_ORDER"), New Point(0, -107))
            ConfirmMenu.SetBannerType(Rectangle)
        End If
        _menuPool.Add(ConfirmMenu)
        ConfirmMenu.AddInstructionalButton(BtnRotLeft)
        ConfirmMenu.AddInstructionalButton(BtnRotRight)
        ConfirmMenu.AddInstructionalButton(BtnOpenDoor)
        ConfirmMenu.AddInstructionalButton(BtnCloseDoor)
        ConfirmMenu.AddInstructionalButton(BtnChangeCam)
        ConfirmMenu.AddItem(ItemCustomize)
        ConfirmMenu.AddItem(New UIMenuItem(GetLangEntry("BTN_TEST_DRIVE")))
        ConfirmMenu.AddItem(New UIMenuItem(GetLangEntry("BTN_CONFIRM")))
        ConfirmMenu.RefreshIndex()
        AddHandler ConfirmMenu.OnMenuClose, AddressOf ConfirmCloseHandler
        AddHandler ConfirmMenu.OnItemSelect, AddressOf ItemSelectHandler
    End Sub

    Public Shared Sub MenuCloseHandler(sender As UIMenu)
        Try
            WriteCfgValue("LastVehHash", My.Settings.LastVehHash, ".\Scripts\PremiumDeluxeMotorsport\config.ini")
            WriteCfgValue("LastVehName", My.Settings.LastVehName, ".\Scripts\PremiumDeluxeMotorsport\config.ini")
            TaskScriptStatus = -1
            If SelectedVehicle IsNot Nothing Then
                SelectedVehicle = Nothing
                VehPreview.Delete()
            End If
            World.DestroyAllCameras()
            World.RenderingCamera = Nothing
            DrawSpotLight = False
            HideHud = False
            VehicleName = Nothing
            ShowVehicleName = False
            ArmourMenu.RefreshIndex()
            ClassicMenu.RefreshIndex()
            CustomiseMenu.RefreshIndex()
            CompactMenu.RefreshIndex()
            ConfirmMenu.RefreshIndex()
            CoupeMenu.RefreshIndex()
            ExoticMenu.RefreshIndex()
            MainMenu.RefreshIndex()
            MotorMenu.RefreshIndex()
            MuscleMenu.RefreshIndex()
            OffRoadMenu.RefreshIndex()
            SedanMenu.RefreshIndex()
            SportMenu.RefreshIndex()
            SuvMenu.RefreshIndex()
            UtilMenu.RefreshIndex()
            VanMenu.RefreshIndex()
            LowRiderMenu.RefreshIndex()
            If My.Settings.AddOnCars = True Then
                AddOnMenu.RefreshIndex()
            End If
        Catch ex As Exception
            logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub ConfirmCloseHandler(sender As UIMenu)
        Try
            'SelectedVehicle = Nothing
            'VehPreview.Delete()
            'VehicleName = Nothing
            'ShowVehicleName = False
            MainMenu.Visible = True
            ArmourMenu.RefreshIndex()
            ClassicMenu.RefreshIndex()
            CustomiseMenu.RefreshIndex()
            CompactMenu.RefreshIndex()
            ConfirmMenu.RefreshIndex()
            CoupeMenu.RefreshIndex()
            ExoticMenu.RefreshIndex()
            MainMenu.RefreshIndex()
            MotorMenu.RefreshIndex()
            MuscleMenu.RefreshIndex()
            OffRoadMenu.RefreshIndex()
            SedanMenu.RefreshIndex()
            SportMenu.RefreshIndex()
            SuvMenu.RefreshIndex()
            UtilMenu.RefreshIndex()
            VanMenu.RefreshIndex()
            LowRiderMenu.RefreshIndex()
            If My.Settings.AddOnCars = True Then
                AddOnMenu.RefreshIndex()
            End If
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub VehicleSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        If selectedItem.Text = VehicleName Then 'If VehPreview.Exists() = True Then
            sender.Visible = False
            ConfirmMenu.Visible = True
            VehicleName = selectedItem.Text
            ShowVehicleName = True
        Else
            SelectedVehicle = selectedItem.SubString2
            If VehPreview = Nothing Then
                VehPreview = CreateVehicle(selectedItem.SubString1, VehPreviewPos, 6.122209)
            Else
                VehPreview.Delete()
                VehPreview = CreateVehicle(selectedItem.SubString1, VehPreviewPos, 6.122209)
            End If
            Dim r As Random = New Random
            VehPreview.PrimaryColor = r.Next(0, 160)
            VehPreview.SecondaryColor = r.Next(0, 160)
            VehPreview.PearlescentColor = r.Next(0, 160)
            VehicleName = sender.MenuItems(index).Text
            ShowVehicleName = True
            VehPreview.Rotation = New Vector3(0, 0, Radius)
            VehPreview.IsDriveable = False
            VehPreview.LockStatus = VehicleLockStatus.CannotBeTriedToEnter
            VehPreview.DirtLevel = 0
            VehiclePrice = selectedItem.SubInteger1
            '_Camera.PointAt(VehPreview, Vector3.Zero)
            My.Settings.LastVehHash = VehPreview.Model.Hash
            My.Settings.LastVehName = VehicleName
            My.Settings.Save()
        End If
    End Sub

    Public Shared Sub VehicleChangeHandler(sender As UIMenu, index As Integer)
        Try
            SelectedVehicle = sender.MenuItems(index).SubString2
            If VehPreview = Nothing Then
                VehPreview = CreateVehicle(sender.MenuItems(index).SubString1, VehPreviewPos, 6.122209)
            Else
                VehPreview.Delete()
                VehPreview = CreateVehicle(sender.MenuItems(index).SubString1, VehPreviewPos, 6.122209)
            End If
            Dim r As Random = New Random
            VehPreview.PrimaryColor = r.Next(0, 160)
            VehPreview.SecondaryColor = r.Next(0, 160)
            VehPreview.PearlescentColor = r.Next(0, 160)
            VehicleName = sender.MenuItems(index).Text
            ShowVehicleName = True
            VehPreview.Rotation = New Vector3(0, 0, Radius)
            VehPreview.IsDriveable = False
            VehPreview.LockStatus = VehicleLockStatus.CannotBeTriedToEnter
            VehPreview.DirtLevel = 0
            VehiclePrice = sender.MenuItems(index).SubInteger1
            '_Camera.PointAt(VehPreview, Vector3.Zero)
            My.Settings.LastVehHash = VehPreview.Model.Hash
            My.Settings.LastVehName = VehicleName
            My.Settings.Save()
        Catch ex As Exception
            logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub ColorSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        sender.GoBack()
        VehPreview.EngineRunning = False
    End Sub

    Public Shared Sub PriColorChangeHandler(sender As UIMenu, index As Integer)
        Try
            If My.Settings.RemoveColor = True Then
                Native.Function.Call(Hash.CLEAR_VEHICLE_CUSTOM_PRIMARY_COLOUR, VehPreview)
                Native.Function.Call(Hash.CLEAR_VEHICLE_CUSTOM_SECONDARY_COLOUR, VehPreview)
            End If
            If sender.MenuItems(index).SubString2 = "1" Then
                VehPreview.PrimaryColor = sender.MenuItems(index).SubInteger1
                VehPreview.PearlescentColor = sender.MenuItems(index).SubInteger1
            Else
                VehPreview.PrimaryColor = sender.MenuItems(index).SubInteger1
            End If
        Catch ex As Exception
            logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub SecColorChangeHandler(sender As UIMenu, index As Integer)
        Try
            If My.Settings.RemoveColor = True Then
                Native.Function.Call(Hash.CLEAR_VEHICLE_CUSTOM_PRIMARY_COLOUR, VehPreview)
                Native.Function.Call(Hash.CLEAR_VEHICLE_CUSTOM_SECONDARY_COLOUR, VehPreview)
            End If
            VehPreview.SecondaryColor = sender.MenuItems(index).SubInteger1
        Catch ex As Exception
            logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub PeaColorChangeHandler(sender As UIMenu, index As Integer)
        Try
            VehPreview.PearlescentColor = sender.MenuItems(index).SubInteger1
        Catch ex As Exception
            logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub PlateItemChangeHandler(sender As UIMenu, index As Integer)
        Try
            Native.Function.Call(Hash.SET_VEHICLE_NUMBER_PLATE_TEXT_INDEX, VehPreview, sender.MenuItems(index).SubInteger1)
        Catch ex As Exception
            logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CPriColorChangeHandler(sender As UIMenu, index As Integer)
        Try
            If My.Settings.RemoveColor = True Then
                Native.Function.Call(Hash.SET_VEHICLE_COLOURS, VehPreview, 0, 0)
            End If
            VehPreview.CustomPrimaryColor = Color.FromArgb(255, sender.MenuItems(index).SubInteger1, sender.MenuItems(index).SubString2, sender.MenuItems(index).SubString1)
        Catch ex As Exception
            logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CSecColorChangeHandler(sender As UIMenu, index As Integer)
        Try
            If My.Settings.RemoveColor = True Then
                Native.Function.Call(Hash.SET_VEHICLE_COLOURS, VehPreview, 0, 0)
            End If
            VehPreview.CustomSecondaryColor = Color.FromArgb(255, sender.MenuItems(index).SubInteger1, sender.MenuItems(index).SubString2, sender.MenuItems(index).SubString1)
        Catch ex As Exception
            logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    'Public Shared Sub NeonColorChangeHandler(sender As UIMenu, index As Integer)
    '    Try
    '        VehPreview.EngineRunning = True
    '        VehPreview.SetNeonLightsOn(VehicleNeonLight.Back, True)
    '        VehPreview.SetNeonLightsOn(VehicleNeonLight.Front, True)
    '        VehPreview.SetNeonLightsOn(VehicleNeonLight.Left, True)
    '        VehPreview.SetNeonLightsOn(VehicleNeonLight.Right, True)
    '        VehPreview.NeonLightsColor = Color.FromArgb(255, sender.MenuItems(index).SubInteger1, sender.MenuItems(index).SubString2, sender.MenuItems(index).SubString1)
    '    Catch ex As Exception
    '        logger.Log(ex.Message & " " & ex.StackTrace)
    '    End Try
    'End Sub

    'Public Shared Sub RimColorChangeHandler(sender As UIMenu, index As Integer)
    '    Try
    '        VehPreview.RimColor = sender.MenuItems(index).SubInteger1
    '    Catch ex As Exception
    '        logger.Log(ex.Message & " " & ex.StackTrace)
    '    End Try
    'End Sub

    'Public Shared Sub SmokeColorChangeHandler(sender As UIMenu, index As Integer)
    '    Try
    '        Native.Function.Call(Hash.SET_VEHICLE_MOD_KIT, VehPreview.Handle, 0)
    '        VehPreview.TireSmokeColor = Color.FromArgb(255, sender.MenuItems(index).SubInteger1, sender.MenuItems(index).SubString2, sender.MenuItems(index).SubString1)
    '        Native.Function.Call(Hash.SET_VEHICLE_TYRE_SMOKE_COLOR, VehPreview, sender.MenuItems(index).SubInteger1, sender.MenuItems(index).SubString2, sender.MenuItems(index).SubString1)
    '    Catch ex As Exception
    '        logger.Log(ex.Message & " " & ex.StackTrace)
    '    End Try
    'End Sub

    'Public Shared Sub ModChangeHandler(sender As UIMenu, index As Integer)
    '    Try
    '        Native.Function.Call(Hash.SET_VEHICLE_MOD_KIT, VehPreview.Handle, 0)
    '        VehPreview.SetMod(sender.MenuItems(index).SubInteger1, sender.MenuItems(index).SubString1, True)
    '        If sender.MenuItems(index).Text.Contains("HORN") Then
    '            VehPreview.SoundHorn(5000)
    '        End If
    '    Catch ex As Exception
    '        logger.Log(ex.Message & " " & ex.StackTrace)
    '    End Try
    'End Sub

    Public Shared Sub ItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        Try
            If selectedItem.Text = GetLangEntry("BTN_CONFIRM") Then
                If PlayerCash > VehiclePrice Then
                    FadeScreenOut(500)
                    Wait(&H3E8)
                    GP.Money = (PlayerCash - VehiclePrice)
                    ConfirmMenu.Visible = False
                    World.DestroyAllCameras()
                    World.RenderingCamera = Nothing
                    DrawSpotLight = False
                    VehPreview.IsDriveable = True
                    VehPreview.LockStatus = VehicleLockStatus.Unlocked
                    Native.Function.Call(Hash.SET_VEHICLE_DOORS_SHUT, VehPreview, False)
                    VehPreview.Position = New Vector3(-56.79958, -1110.868, 26.43581)
                    Native.Function.Call(Hash.TASK_WARP_PED_INTO_VEHICLE, GPC, VehPreview, -1)
                    VehPreview.MarkAsNoLongerNeeded()
                    VehPreview = Nothing
                    HideHud = False
                    Wait(500)
                    FadeScreenIn(500)
                    Native.Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "PROPERTY_PURCHASE", "HUD_AWARDS", False)
                    BigMessageThread.MessageInstance.ShowWeaponPurchasedMessage("~y~" & GetLangEntry("VEHICLE_PURCHASED"), "~w~" & SelectedVehicle, Nothing)
                    SelectedVehicle = Nothing
                    VehicleName = Nothing
                    ShowVehicleName = False
                    TaskScriptStatus = -1
                Else
                    If PlayerName = "Michael" Then
                        DisplayNotificationThisFrame(GetLangEntry("MAZE_BANK"), GetLangEntry("INSUFFICIENT_FUNDS_TITLE"), GetLangEntry("INSUFFICIENT_FUNCS_BODY"), "CHAR_BANK_MAZE", True, IconType.RightJumpingArrow)
                    ElseIf PlayerName = "Franklin" Then
                        DisplayNotificationThisFrame(GetLangEntry("FLEECA_BANK"), GetLangEntry("INSUFFICIENT_FUNDS_TITLE"), GetLangEntry("INSUFFICIENT_FUNCS_BODY"), "CHAR_BANK_FLEECA", True, IconType.RightJumpingArrow)
                    ElseIf PlayerName = "Trevor" Then
                        DisplayNotificationThisFrame(GetLangEntry("BOL_BANK"), GetLangEntry("INSUFFICIENT_FUNDS_TITLE"), GetLangEntry("INSUFFICIENT_FUNCS_BODY"), "CHAR_BANK_BOL", True, IconType.RightJumpingArrow)
                    Else
                        DisplayNotificationThisFrame(GetLangEntry("MAZE_BANK"), GetLangEntry("INSUFFICIENT_FUNDS_TITLE"), GetLangEntry("INSUFFICIENT_FUNCS_BODY"), "CHAR_BANK_MAZE", True, IconType.RightJumpingArrow)
                    End If
                End If
            ElseIf selectedItem.Text = GetLangEntry("BTN_TEST_DRIVE") Then
                FadeScreenOut(500)
                Wait(&H3E8)
                Native.Function.Call(Hash.TASK_WARP_PED_INTO_VEHICLE, GPC, VehPreview, -1)
                ConfirmMenu.Visible = False
                World.DestroyAllCameras()
                World.RenderingCamera = Nothing
                DrawSpotLight = False
                VehPreview.IsDriveable = True
                VehPreview.LockStatus = VehicleLockStatus.Unlocked
                Native.Function.Call(Hash.SET_VEHICLE_DOORS_SHUT, VehPreview, False)
                DisplayHelpTextThisFrame(GetLangEntry("HELP_TEST_DRIVE"))
                TestDrive = TestDrive + 1
                HideHud = False
                VehPreview.Position = New Vector3(-56.79958, -1110.868, 26.43581)
                Wait(500)
                FadeScreenIn(500)
                ShowVehicleName = False
            End If

            If selectedItem.Text = GetLangEntry("BTN_UPGRADE_NAME") Then
                Native.Function.Call(Hash.SET_VEHICLE_MOD_KIT, VehPreview.Handle, 0)
                VehPreview.SetMod(VehicleMod.Suspension, 3, True)
                VehPreview.SetMod(VehicleMod.Engine, 3, True)
                VehPreview.SetMod(VehicleMod.Brakes, 2, True)
                VehPreview.SetMod(VehicleMod.Transmission, 2, True)
                VehPreview.SetMod(VehicleMod.Armor, 4, True)
                VehPreview.ToggleMod(VehicleToggleMod.XenonHeadlights, True)
                VehPreview.ToggleMod(VehicleToggleMod.Turbo, True)
                Native.Function.Call(Hash.SET_VEHICLE_TYRES_CAN_BURST, VehPreview, False)
            ElseIf selectedItem.Text = GetLangEntry("BTN_PLATE_NUMBER_NAME") Then
                Dim NumPlateText As String = GetUserInput(VehPreview.NumberPlate, 9)
                If NumPlateText <> "" Then
                    VehPreview.NumberPlate = NumPlateText
                    If ChangeCamera = 0 Then
                        World.DestroyAllCameras()
                        World.RenderingCamera = Nothing
                        DrawSpotLight = False
                    ElseIf ChangeCamera = 1 Then
                        _Camera = World.CreateCamera(CameraPos, CameraRot, CameraFOV)
                        World.RenderingCamera = _Camera
                        _Camera.Shake(CameraShake.Hand, 0.4)
                    End If
                End If
            End If
        Catch ex As Exception
            logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub OnTick(o As Object, e As EventArgs)
        Try
            _menuPool.ProcessMenus()
        Catch ex As Exception
            logger.Log("Error Menu Pool " & ex.Message & " " & ex.StackTrace)
        End Try

        Try
            If MissionFlag Or GP.WantedLevel > 1 Then
                PdmBlip.Alpha = 0
            Else
                PdmBlip.Alpha = 255
            End If
        Catch ex As Exception
            logger.Log("Error Blip Visible " & ex.Message & " " & ex.StackTrace)
        End Try

        Try
            PdmDoorDist = World.GetDistance(GPC.Position, PdmDoor)
            TestDriveDist = World.GetDistance(GPC.Position, testDriveVector)
            playerHash = Game.Player.Character.Model.GetHashCode().ToString
            GP = Game.Player
            GPC = Game.Player.Character
            Select Case playerHash
                Case "1885233650"
                    PlayerCash = 1999999999
                Case "-1667301416"
                    PlayerCash = 1999999999
                Case Else
                    PlayerCash = Game.Player.Money
            End Select
        Catch ex As Exception
            logger.Log("Error Get Info " & ex.Message & " " & ex.StackTrace)
        End Try

        Try
            If Not GPC.IsInVehicle AndAlso Not GPC.IsDead AndAlso PdmDoorDist < 3.0 AndAlso GP.WantedLevel = 0 AndAlso TaskScriptStatus = -1 Then
                DisplayHelpTextThisFrame(GetLangEntry("HELP_ENTER_SHOP"))
            ElseIf Not GPC.IsInVehicle AndAlso Not GPC.IsDead AndAlso PdmDoorDist < 3.0 AndAlso GP.WantedLevel >= 1 Then
                Native.Function.Call(Hash.DISPLAY_HELP_TEXT_THIS_FRAME, New InputArgument() {"LOSE_WANTED", 0})
            End If

            If TestDrive = 3 AndAlso Not GPC.IsInVehicle Then
                FadeScreenOut(500)
                Wait(&H3E8)
                Dim penalty As Double = VehiclePrice / 99
                If VehPreview.HasBeenDamagedBy(GPC) Then
                    GP.Money = (PlayerCash - (VehiclePrice / 99))
                    DisplayHelpTextThisFrame("$" & Math.Round(penalty) & GetLangEntry("HELP_PENALTY"))
                    UI.Notify("$" & Math.Round(penalty) & GetLangEntry("HELP_PENALTY"))
                End If
                ConfirmMenu.Visible = True
                _Camera = World.CreateCamera(CameraPos, CameraRot, CameraFOV)
                World.RenderingCamera = _Camera
                _Camera.Shake(CameraShake.Hand, 0.4)
                VehPreview.IsDriveable = False
                VehPreview.LockStatus = VehicleLockStatus.CannotBeTriedToEnter
                GPC.Position = PlayerPos
                GPC.Heading = PlayerHeading
                VehPreview.Position = VehPreviewPos
                Native.Function.Call(Hash.SET_VEHICLE_DOORS_SHUT, VehPreview, False)
                Native.Function.Call(Hash.SET_VEHICLE_FIXED, VehPreview)
                TestDrive = 1
                HideHud = True
                Wait(500)
                FadeScreenIn(500)
                ShowVehicleName = True
            ElseIf TestDrive = 3 AndAlso TestDriveDist > 450.0 Then
                FadeScreenOut(500)
                Wait(&H3E8)
                Dim penalty As Double = VehiclePrice / 99
                If VehPreview.HasBeenDamagedBy(GPC) Then
                    GP.Money = (PlayerCash - (VehiclePrice / 99))
                    UI.Notify("$" & Math.Round(penalty) & GetLangEntry("HELP_PENALTY"))
                End If
                ConfirmMenu.Visible = True
                _Camera = World.CreateCamera(CameraPos, CameraRot, CameraFOV)
                World.RenderingCamera = _Camera
                _Camera.Shake(CameraShake.Hand, 0.4)
                VehPreview.IsDriveable = False
                VehPreview.LockStatus = VehicleLockStatus.CannotBeTriedToEnter
                GPC.Position = PlayerPos
                GPC.Heading = PlayerHeading
                VehPreview.Position = VehPreviewPos
                Native.Function.Call(Hash.SET_VEHICLE_DOORS_SHUT, VehPreview, False)
                Native.Function.Call(Hash.SET_VEHICLE_FIXED, VehPreview)
                TestDrive = 1
                HideHud = True
                Wait(500)
                FadeScreenIn(500)
                ShowVehicleName = True
            ElseIf TestDrive = 2 AndAlso GPC.IsInVehicle Then
                TestDrive = TestDrive + 1
            End If
        Catch ex As Exception
            logger.Log("Error Show Help " & ex.Message & " " & ex.StackTrace)
        End Try

        Try
            If HideHud Then
                Native.Function.Call(Hash.HIDE_HUD_AND_RADAR_THIS_FRAME)
            End If

            If DrawSpotLight = True Then
                World.DrawSpotLightWithShadow(VehPreviewPos + Vector3.WorldUp * 4 + Vector3.WorldNorth * 4, Vector3.WorldSouth + Vector3.WorldDown, Color.White, 30, 30, 100, 50, -1)
                World.DrawSpotLight(VehPreviewPos + Vector3.WorldUp * 4 + Vector3.WorldNorth * 4, Vector3.WorldSouth + Vector3.WorldDown, Color.White, 30, 30, 100, 50, -1)
            End If
        Catch ex As Exception
            logger.Log("Error Hud Spotlight " & ex.Message & " " & ex.StackTrace)
        End Try

        Try
            If IsControlJustPressed(0, GTA.Control.Context) AndAlso PdmDoorDist < 3.0F AndAlso Not GPC.IsInVehicle AndAlso GP.WantedLevel = 0 AndAlso TaskScriptStatus = -1 Then
                TaskScriptStatus = 0
                FadeScreenOut(500)
                Wait(&H3E8)
                MainMenu.Visible = True
                ChangeCamera = 1
                _Camera = World.CreateCamera(CameraPos, CameraRot, CameraFOV)
                World.RenderingCamera = _Camera
                _Camera.Shake(CameraShake.Hand, 0.4)
                GPC.Position = PlayerPos
                GPC.Heading = PlayerHeading
                HideHud = True

                SelectedVehicle = My.Settings.LastVehName
                If VehPreview = Nothing Then
                    VehPreview = CreateVehicleFromHash(My.Settings.LastVehHash, VehPreviewPos, 6.122209)
                Else
                    VehPreview.Delete()
                    VehPreview = CreateVehicleFromHash(My.Settings.LastVehHash, VehPreviewPos, 6.122209)
                End If
                VehicleName = SelectedVehicle
                ShowVehicleName = True
                VehPreview.Rotation = New Vector3(0, 0, Radius)
                VehPreview.LockStatus = VehicleLockStatus.CannotBeTriedToEnter
                VehPreview.DirtLevel = 0
                '_Camera.PointAt(VehPreview, Vector3.Zero)

                Wait(500)
                FadeScreenIn(500)
            End If

            If IsControlPressed(0, GTA.Control.ParachuteBrakeLeft) AndAlso PdmDoorDist < 40.0 AndAlso Not GPC.IsInVehicle AndAlso TaskScriptStatus = 0 Then
                VehicleSpin = False
                Radius = Radius + 2
                VehPreview.Rotation = New Vector3(0, 0, Radius)
            ElseIf IsControlPressed(0, GTA.Control.ParachuteBrakeRight) AndAlso PdmDoorDist < 40.0 AndAlso Not GPC.IsInVehicle AndAlso TaskScriptStatus = 0 Then
                VehicleSpin = False
                Radius = Radius - 2
                VehPreview.Rotation = New Vector3(0, 0, Radius)
            ElseIf IsControlJustReleased(0, GTA.Control.ParachuteBrakeLeft) AndAlso PdmDoorDist < 40.0 AndAlso Not GPC.IsInVehicle AndAlso TaskScriptStatus = 0 Then
                VehicleSpin = True
            ElseIf IsControlJustReleased(0, GTA.Control.ParachuteBrakeRight) AndAlso PdmDoorDist < 40.0 AndAlso Not GPC.IsInVehicle AndAlso TaskScriptStatus = 0 Then
                VehicleSpin = True
            ElseIf IsControlJustPressed(0, GTA.Control.SelectWeaponUnarmed) AndAlso PdmDoorDist < 40.0 AndAlso Not GPC.IsInVehicle AndAlso TaskScriptStatus = 0 Then
                VehPreview.OpenDoor(VehicleDoor.BackLeftDoor, False, False)
                VehPreview.OpenDoor(VehicleDoor.BackRightDoor, False, False)
                VehPreview.OpenDoor(VehicleDoor.FrontLeftDoor, False, False)
                VehPreview.OpenDoor(VehicleDoor.FrontRightDoor, False, False)
                VehPreview.OpenDoor(VehicleDoor.Hood, False, False)
                VehPreview.OpenDoor(VehicleDoor.Trunk, False, False)
                VehPreview.OpenDoor(VehicleDoor.Trunk2, False, False)
            ElseIf IsControlJustPressed(0, GTA.Control.SelectWeaponMelee) AndAlso PdmDoorDist < 40.0 AndAlso Not GPC.IsInVehicle AndAlso TaskScriptStatus = 0 Then
                Native.Function.Call(Hash.SET_VEHICLE_DOORS_SHUT, VehPreview, False)
            ElseIf IsControlJustPressed(0, GTA.Control.VehicleRoof) AndAlso PdmDoorDist < 40.0 AndAlso TaskScriptStatus = 0 Then
                If VehPreview.RoofState = VehicleRoofState.Closed Then
                    Native.Function.Call(Hash.LOWER_CONVERTIBLE_ROOF, VehPreview, False)
                Else
                    Native.Function.Call(Hash.RAISE_CONVERTIBLE_ROOF, VehPreview, False)
                End If
            End If

            If IsControlJustPressed(0, GTA.Control.NextCamera) AndAlso PdmDoorDist < 40.0 AndAlso ChangeCamera = 0 AndAlso Not GPC.IsInVehicle AndAlso TaskScriptStatus = 0 Then
                World.DestroyAllCameras()
                World.RenderingCamera = Nothing
                ChangeCamera = (ChangeCamera + 1)
            ElseIf IsControlJustPressed(0, GTA.Control.NextCamera) AndAlso PdmDoorDist < 40.0 AndAlso ChangeCamera = 1 AndAlso Not GPC.IsInVehicle AndAlso TaskScriptStatus = 0 Then
                _Camera = World.CreateCamera(CameraPos, CameraRot, CameraFOV)
                World.RenderingCamera = _Camera
                _Camera.Shake(CameraShake.Hand, 0.4)
                ChangeCamera = (ChangeCamera - 1)
            End If
        Catch ex As Exception
            logger.Log("Error keypress " & ex.Message & " " & ex.StackTrace)
        End Try

        Try
            If VehicleSpin = True AndAlso PdmDoorDist < 40.0 AndAlso Not GPC.IsInVehicle AndAlso Not VehPreview = Nothing AndAlso TaskScriptStatus = 0 Then
                Radius = Radius + 1
                VehPreview.Rotation = New Vector3(0, 0, Radius)
            End If

            If ShowVehicleName = True AndAlso Not VehicleName = Nothing AndAlso PdmDoorDist < 40.0 AndAlso TaskScriptStatus = 0 Then
                Select Case Game.Language.ToString
                    Case "Chinese", "Korean", "Japanese"
                        DrawText(VehicleName, New Point(0, 550), 2.0, Color.White, GTAFont.UIDefault, GTAFontAlign.Right, GTAFontStyleOptions.DropShadow)
                        DrawText(GetVehicleClass(VehPreview), New Point(0, 600), 2.0, Color.DodgerBlue, GTAFont.Script, GTAFontAlign.Right, GTAFontStyleOptions.DropShadow)
                    Case Else
                        DrawText(VehicleName, New Point(0, 550), 2.0, Color.White, GTAFont.Title, GTAFontAlign.Right, GTAFontStyleOptions.DropShadow)
                        DrawText(GetVehicleClass(VehPreview), New Point(0, 600), 2.0, Color.DodgerBlue, GTAFont.Script, GTAFontAlign.Right, GTAFontStyleOptions.DropShadow)
                End Select
            End If
        Catch ex As Exception
            logger.Log("Error Spin Car Name " & ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Protected Overrides Sub Dispose(A_0 As Boolean)
        If (A_0) Then
            Try
                PdmBlip.Remove()
            Catch ex As Exception
                logger.Log(ex.Message & ex.StackTrace)
            End Try
        End If
    End Sub
End Class
