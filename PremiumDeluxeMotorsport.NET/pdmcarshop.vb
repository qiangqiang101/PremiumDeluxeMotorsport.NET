Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms
Imports GTA
Imports GTA.Native
Imports NativeUI
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Public Class pdmcarshop
    Inherits Script

    Private player As Player
    Private playerPed, simeon As Ped
    Private selectedVehicle As String
    Private vehPreview As Vehicle
    Private simeonDist As Single
    Private ModEnable As Boolean = False
    Private curRadius As Integer = 0
    Private PlayerCash As Integer
    Private vehiclePrice As Integer
    Private categoryName As String = Nothing
    Private ChangeCamera As Integer = 0

    Private mainMenu, modMenu, colorMenu, colorMenu2, plateMenu, confirmMenu As UIMenu
    Private motorMenu, compactMenu, coupeMenu, sedanMenu, sportMenu, classicMenu, exoticMenu, muscleMenu, offroadMenu, suvMenu, vanMenu, utilityMenu, armouredMenu As UIMenu

    Dim itemMotor As New UIMenuItem("Motorcycles")
    Dim itemCompact As New UIMenuItem("Compacts")
    Dim itemCoupe As New UIMenuItem("Coupes")
    Dim itemSedan As New UIMenuItem("Sedans")
    Dim itemSport As New UIMenuItem("Sports")
    Dim itemClassic As New UIMenuItem("Classics")
    Dim itemExotic As New UIMenuItem("Exotics")
    Dim itemMuscle As New UIMenuItem("Muscle")
    Dim itemOffRoad As New UIMenuItem("Off-Road")
    Dim itemSuv As New UIMenuItem("SUVs")
    Dim itemVan As New UIMenuItem("Vans")
    Dim itemUtility As New UIMenuItem("Utility")
    Dim itemArmoured As New UIMenuItem("Armoured")
    Dim itemMotorConfirm As New UIMenuItem("Proceed to Checkout")
    Dim itemCompactConfirm As New UIMenuItem("Proceed to Checkout")
    Dim itemCoupeConfirm As New UIMenuItem("Proceed to Checkout")
    Dim itemSedanConfirm As New UIMenuItem("Proceed to Checkout")
    Dim itemSportConfirm As New UIMenuItem("Proceed to Checkout")
    Dim itemClassicConfirm As New UIMenuItem("Proceed to Checkout")
    Dim itemExoticConfirm As New UIMenuItem("Proceed to Checkout")
    Dim itemMuscleConfirm As New UIMenuItem("Proceed to Checkout")
    Dim itemOffRoadConfirm As New UIMenuItem("Proceed to Checkout")
    Dim itemSuvConfirm As New UIMenuItem("Proceed to Checkout")
    Dim itemVanConfirm As New UIMenuItem("Proceed to Checkout")
    Dim itemUtilityConfirm As New UIMenuItem("Proceed to Checkout")
    Dim itemArmouredConfirm As New UIMenuItem("Proceed to Checkout")
    Dim itemColor As New UIMenuItem("Primary Color", "Transform vehicle appearance.")
    Dim itemColor2 As New UIMenuItem("Secondary Color", "Transform vehicle appearance.")
    Dim itemPlate As New UIMenuItem("Plate", "Customize license plate.")

    Dim btnRotLeft As New InstructionalButton(ReadIniValue(".\Scripts\PDMCarShop\config.ini", "OPTIONS", "RotLeftKey"), "Rotate Left")
    Dim btnRotRight As New InstructionalButton(ReadIniValue(".\Scripts\PDMCarShop\config.ini", "OPTIONS", "RotRightKey"), "Rotate Right")
    Dim btnOpenDoor As New InstructionalButton(ReadIniValue(".\Scripts\PDMCarShop\config.ini", "OPTIONS", "OpenDoorKey"), "Open Doors")
    Dim btnCloseDoor As New InstructionalButton(ReadIniValue(".\Scripts\PDMCarShop\config.ini", "OPTIONS", "CloseDoorKey"), "Close Doors")
    Dim btnChangeCam As New InstructionalButton(ReadIniValue(".\Scripts\PDMCarShop\config.ini", "OPTIONS", "ChangeCam"), "Change Camera")

    Private motorcycle As String = Application.StartupPath & "\scripts\PDMCarShop\motorcycle.ini"
    Private compact As String = Application.StartupPath & "\scripts\PDMCarShop\compact.ini"
    Private coupe As String = Application.StartupPath & "\scripts\PDMCarShop\coupe.ini"
    Private sedan As String = Application.StartupPath & "\scripts\PDMCarShop\sedan.ini"
    Private sport As String = Application.StartupPath & "\scripts\PDMCarShop\sport.ini"
    Private classic As String = Application.StartupPath & "\scripts\PDMCarShop\classic.ini"
    Private exotic As String = Application.StartupPath & "\scripts\PDMCarShop\exotic.ini"
    Private muscle As String = Application.StartupPath & "\scripts\PDMCarShop\muscle.ini"
    Private offroad As String = Application.StartupPath & "\scripts\PDMCarShop\offroad.ini"
    Private suv As String = Application.StartupPath & "\scripts\PDMCarShop\suv.ini"
    Private van As String = Application.StartupPath & "\scripts\PDMCarShop\van.ini"
    Private utility As String = Application.StartupPath & "\scripts\PDMCarShop\utility.ini"
    Private armoured As String = Application.StartupPath & "\scripts\PDMCarShop\armoured.ini"
    Private colour As String = Application.StartupPath & "\scripts\PDMCarShop\color.ini"
    Private plate As String = Application.StartupPath & "\scripts\PDMCarShop\plate.ini"
    Private parameters As String() = {"[name]", "[hash]", "[price]", "[price2]", "[model]"}
    Private paracolors As String() = {"[red]", "[green]", "[blue]", "[name]"}
    Private paraplates As String() = {"[index]", "[name]"}

    Private _menuPool As MenuPool

    Public Sub New()
        player = Game.Player
        playerPed = Game.Player.Character
        PlayerCash = playerPed.Money

        AddHandler Tick, AddressOf OnTick
        AddHandler KeyDown, AddressOf OnKeyDown
        _menuPool = New MenuPool()

        modMenu = New UIMenu("PDM Car Shop", "~b~VERSION: 2.1")
        _menuPool.Add(modMenu)
        modMenu.AddItem(New UIMenuItem("Enable", "Enable Mod"))
        modMenu.AddItem(New UIMenuItem("Disable", "Disable Mod"))
        modMenu.AddItem(New UIMenuItem("Key Settings", "Refresh Keys After you saving config.ini file."))
        modMenu.AddItem(New UIMenuItem("About", "About PDM Car Shop"))
        modMenu.RefreshIndex()

        mainMenu = New UIMenu("PDM Car Shop", "~b~CATEGORIES")
        _menuPool.Add(mainMenu)
        mainMenu.AddItem(itemMotor)
        mainMenu.AddItem(itemCompact)
        mainMenu.AddItem(itemCoupe)
        mainMenu.AddItem(itemSedan)
        mainMenu.AddItem(itemSport)
        mainMenu.AddItem(itemClassic)
        mainMenu.AddItem(itemExotic)
        mainMenu.AddItem(itemMuscle)
        mainMenu.AddItem(itemOffRoad)
        mainMenu.AddItem(itemSuv)
        mainMenu.AddItem(itemVan)
        mainMenu.AddItem(itemUtility)
        mainMenu.AddItem(itemArmoured)
        mainMenu.RefreshIndex()

        ReadMotorcycle()
        ReadCompact()
        ReadCoupe()
        ReadSedan()
        ReadSport()
        ReadClassic()
        ReadExotic()
        ReadMuscle()
        ReadOffroad()
        ReadSuv()
        ReadVan()
        ReadUtility()
        ReadArmoured()
        ReadConfirm()
        ReadColor()
        ReadColor2()
        ReadPlate()

        AddHandler mainMenu.OnMenuClose, AddressOf MenuCloseHandler

        AddHandler mainMenu.OnItemSelect, AddressOf ItemSelectHandler
        AddHandler motorMenu.OnItemSelect, AddressOf MotorItemSelectHandler
        AddHandler compactMenu.OnItemSelect, AddressOf CompactItemSelectHandler
        AddHandler coupeMenu.OnItemSelect, AddressOf CoupeItemSelectHandler
        AddHandler sedanMenu.OnItemSelect, AddressOf SedanItemSelectHandler
        AddHandler sportMenu.OnItemSelect, AddressOf SportItemSelectHandler
        AddHandler classicMenu.OnItemSelect, AddressOf ClassicItemSelectHandler
        AddHandler exoticMenu.OnItemSelect, AddressOf ExoticItemSelectHandler
        AddHandler muscleMenu.OnItemSelect, AddressOf MuscleItemSelectHandler
        AddHandler offroadMenu.OnItemSelect, AddressOf OffroadItemSelectHandler
        AddHandler suvMenu.OnItemSelect, AddressOf SuvItemSelectHandler
        AddHandler vanMenu.OnItemSelect, AddressOf VanItemSelectHandler
        AddHandler utilityMenu.OnItemSelect, AddressOf UtilityItemSelectHandler
        AddHandler armouredMenu.OnItemSelect, AddressOf ArmouredItemSelectHandler

        AddHandler confirmMenu.OnItemSelect, AddressOf ConfirmItemSelectHandler
        AddHandler modMenu.OnItemSelect, AddressOf ItemSelectHandler
        AddHandler colorMenu.OnItemSelect, AddressOf ColorItemSelectHandler
        AddHandler colorMenu2.OnItemSelect, AddressOf ColorItemSelectHandler2
        AddHandler plateMenu.OnItemSelect, AddressOf PlateItemSelectHandler

        AddHandler colorMenu.OnIndexChange, AddressOf ColorItemChangeHandler
        AddHandler colorMenu2.OnIndexChange, AddressOf ColorItemChangeHandler2
        AddHandler plateMenu.OnIndexChange, AddressOf PlateItemChangeHandler

        My.Settings.keyModEnable = [Enum].Parse(GetType(Keys), ReadIniValue(".\Scripts\PDMCarShop\config.ini", "OPTIONS", "ModEnableKey"), False)
        My.Settings.keyRotLeft = [Enum].Parse(GetType(Keys), ReadIniValue(".\Scripts\PDMCarShop\config.ini", "OPTIONS", "RotLeftKey"), False)
        My.Settings.keyRotRight = [Enum].Parse(GetType(Keys), ReadIniValue(".\Scripts\PDMCarShop\config.ini", "OPTIONS", "RotRightKey"), False)
        My.Settings.keyOpenDoor = [Enum].Parse(GetType(Keys), ReadIniValue(".\Scripts\PDMCarShop\config.ini", "OPTIONS", "OpenDoorKey"), False)
        My.Settings.keyCloseDoor = [Enum].Parse(GetType(Keys), ReadIniValue(".\Scripts\PDMCarShop\config.ini", "OPTIONS", "CloseDoorKey"), False)
        My.Settings.keyUse = [Enum].Parse(GetType(Keys), ReadIniValue(".\Scripts\PDMCarShop\config.ini", "OPTIONS", "UseKey"), False)
        My.Settings.keyChangeCam = [Enum].Parse(GetType(Keys), ReadIniValue(".\Scripts\PDMCarShop\config.ini", "OPTIONS", "ChangeCam"), False)
        My.Settings.Save()

        UI.DrawTexture(".\Scripts\PDMCarShop\purchase.png", 0, 0, 10, New Point(CInt(UI.WIDTH * 0.3), 100), New Size(600, 50), 0.0, Color.White)

        modMenu.MenuItems(1).SetRightBadge(UIMenuItem.BadgeStyle.Tick)
    End Sub

    Public Sub SpawnSimeon()
        Dim _random As Random = New Random
        simeon = World.CreatePed(PedHash.SiemonYetarian, New GTA.Math.Vector3(-40.3857F, -1108.79F, 25.4375F), 157.821F)
        simeon.AddBlip()
        simeon.Armor = 100
        simeon.Health = 500
        'simeon.Money = _random.Next(40000, 300000)
        simeon.Task.LookAt(playerPed)
        simeon.AlwaysKeepTask = True
        simeon.CurrentBlip.Sprite = BlipSprite.PersonalVehicleCar
        simeon.CurrentBlip.Color = BlipColor.Yellow
        simeon.CurrentBlip.IsShortRange = True
        simeon.FreezePosition = True
        'Dim ShopName As String = "Premium Deluxe Motorsport"
        GTA.Native.Function.Call(Hash.SET_BLIP_NAME_FROM_TEXT_FILE, simeon.CurrentBlip, "BLIP_FRIEND") 'VED_BLIPN
        'Native.Function.Call(Native.Hash.CREATE_CHECKPOINT, 24, -40.76268, -1110.27, 23.43815, -40.76268, -1110.27, 23.43815, 1, 255, 0, 0, 100, 0)
    End Sub

    Public Sub ReadMotorcycle()
        Dim format As New BTEFormatReader(motorcycle, parameters)
        Dim qty As Integer = format.Count - 1

        motorMenu = New UIMenu("PDM Car Shop", "~r~MOTORCYCLES")
        _menuPool.Add(motorMenu)
        motorMenu.AddInstructionalButton(btnRotLeft)
        motorMenu.AddInstructionalButton(btnRotRight)
        motorMenu.AddInstructionalButton(btnOpenDoor)
        motorMenu.AddInstructionalButton(btnCloseDoor)
        motorMenu.AddInstructionalButton(btnChangeCam)
        For i As Integer = 0 To format.Count - 1
            Dim item As New UIMenuItem(format(i)("name") & " $" & format(i)("price"))
            motorMenu.AddItem(item)
            With item
                .Model = format(i)("model")
                .Price = format(i)("price")
                .Car = format(i)("name")
            End With
        Next
        motorMenu.AddItem(itemMotorConfirm)
        motorMenu.RefreshIndex()
        mainMenu.BindMenuToItem(motorMenu, itemMotor)
    End Sub

    Public Sub ReadCompact()
        Dim format As New BTEFormatReader(compact, parameters)
        Dim qty As Integer = format.Count - 1

        compactMenu = New UIMenu("PDM Car Shop", "~r~COMPACTS")
        _menuPool.Add(compactMenu)
        compactMenu.AddInstructionalButton(btnRotLeft)
        compactMenu.AddInstructionalButton(btnRotRight)
        compactMenu.AddInstructionalButton(btnOpenDoor)
        compactMenu.AddInstructionalButton(btnCloseDoor)
        compactMenu.AddInstructionalButton(btnChangeCam)
        For i As Integer = 0 To format.Count - 1
            Dim item As New UIMenuItem(format(i)("name") & " $" & format(i)("price"))
            compactMenu.AddItem(item)
            With item
                .Model = format(i)("model")
                .Price = format(i)("price")
                .Car = format(i)("name")
            End With
        Next
        compactMenu.AddItem(itemCompactConfirm)
        compactMenu.RefreshIndex()
        mainMenu.BindMenuToItem(compactMenu, itemCompact)
    End Sub

    Public Sub ReadCoupe()
        Dim format As New BTEFormatReader(coupe, parameters)
        Dim qty As Integer = format.Count - 1

        coupeMenu = New UIMenu("PDM Car Shop", "~r~COUPES")
        _menuPool.Add(coupeMenu)
        coupeMenu.AddInstructionalButton(btnRotLeft)
        coupeMenu.AddInstructionalButton(btnRotRight)
        coupeMenu.AddInstructionalButton(btnOpenDoor)
        coupeMenu.AddInstructionalButton(btnCloseDoor)
        coupeMenu.AddInstructionalButton(btnChangeCam)
        For i As Integer = 0 To format.Count - 1
            Dim item As New UIMenuItem(format(i)("name") & " $" & format(i)("price"))
            coupeMenu.AddItem(item)
            With item
                .Model = format(i)("model")
                .Price = format(i)("price")
                .Car = format(i)("name")
            End With
        Next
        coupeMenu.AddItem(itemCoupeConfirm)
        coupeMenu.RefreshIndex()
        mainMenu.BindMenuToItem(coupeMenu, itemCoupe)
    End Sub

    Public Sub ReadSedan()
        Dim format As New BTEFormatReader(sedan, parameters)
        Dim qty As Integer = format.Count - 1

        sedanMenu = New UIMenu("PDM Car Shop", "~r~SEDANS")
        _menuPool.Add(sedanMenu)
        sedanMenu.AddInstructionalButton(btnRotLeft)
        sedanMenu.AddInstructionalButton(btnRotRight)
        sedanMenu.AddInstructionalButton(btnOpenDoor)
        sedanMenu.AddInstructionalButton(btnCloseDoor)
        sedanMenu.AddInstructionalButton(btnChangeCam)
        For i As Integer = 0 To format.Count - 1
            Dim item As New UIMenuItem(format(i)("name") & " $" & format(i)("price"))
            sedanMenu.AddItem(item)
            With item
                .Model = format(i)("model")
                .Price = format(i)("price")
                .Car = format(i)("name")
            End With
        Next
        sedanMenu.AddItem(itemSedanConfirm)
        sedanMenu.RefreshIndex()
        mainMenu.BindMenuToItem(sedanMenu, itemSedan)
    End Sub

    Public Sub ReadSport()
        Dim format As New BTEFormatReader(sport, parameters)
        Dim qty As Integer = format.Count - 1

        sportMenu = New UIMenu("PDM Car Shop", "~r~SPORTS")
        _menuPool.Add(sportMenu)
        sportMenu.AddInstructionalButton(btnRotLeft)
        sportMenu.AddInstructionalButton(btnRotRight)
        sportMenu.AddInstructionalButton(btnOpenDoor)
        sportMenu.AddInstructionalButton(btnCloseDoor)
        sportMenu.AddInstructionalButton(btnChangeCam)
        For i As Integer = 0 To format.Count - 1
            Dim item As New UIMenuItem(format(i)("name") & " $" & format(i)("price"))
            sportMenu.AddItem(item)
            With item
                .Model = format(i)("model")
                .Price = format(i)("price")
                .Car = format(i)("name")
            End With
        Next
        sportMenu.AddItem(itemSportConfirm)
        sportMenu.RefreshIndex()
        mainMenu.BindMenuToItem(sportMenu, itemSport)
    End Sub

    Public Sub ReadClassic()
        Dim format As New BTEFormatReader(classic, parameters)
        Dim qty As Integer = format.Count - 1

        classicMenu = New UIMenu("PDM Car Shop", "~r~CLASSICS")
        _menuPool.Add(classicMenu)
        classicMenu.AddInstructionalButton(btnRotLeft)
        classicMenu.AddInstructionalButton(btnRotRight)
        classicMenu.AddInstructionalButton(btnOpenDoor)
        classicMenu.AddInstructionalButton(btnCloseDoor)
        classicMenu.AddInstructionalButton(btnChangeCam)
        For i As Integer = 0 To format.Count - 1
            Dim item As New UIMenuItem(format(i)("name") & " $" & format(i)("price"))
            classicMenu.AddItem(item)
            With item
                .Model = format(i)("model")
                .Price = format(i)("price")
                .Car = format(i)("name")
            End With
        Next
        classicMenu.AddItem(itemClassicConfirm)
        classicMenu.RefreshIndex()
        mainMenu.BindMenuToItem(classicMenu, itemClassic)
    End Sub

    Public Sub ReadExotic()
        Dim format As New BTEFormatReader(exotic, parameters)
        Dim qty As Integer = format.Count - 1

        exoticMenu = New UIMenu("PDM Car Shop", "~r~EXOTICS")
        _menuPool.Add(exoticMenu)
        exoticMenu.AddInstructionalButton(btnRotLeft)
        exoticMenu.AddInstructionalButton(btnRotRight)
        exoticMenu.AddInstructionalButton(btnOpenDoor)
        exoticMenu.AddInstructionalButton(btnCloseDoor)
        exoticMenu.AddInstructionalButton(btnChangeCam)
        For i As Integer = 0 To format.Count - 1
            Dim item As New UIMenuItem(format(i)("name") & " $" & format(i)("price"))
            exoticMenu.AddItem(item)
            With item
                .Model = format(i)("model")
                .Price = format(i)("price")
                .Car = format(i)("name")
            End With
        Next
        exoticMenu.AddItem(itemExoticConfirm)
        exoticMenu.RefreshIndex()
        mainMenu.BindMenuToItem(exoticMenu, itemExotic)
    End Sub

    Public Sub ReadMuscle()
        Dim format As New BTEFormatReader(muscle, parameters)
        Dim qty As Integer = format.Count - 1

        muscleMenu = New UIMenu("PDM Car Shop", "~r~MUSCLES")
        _menuPool.Add(muscleMenu)
        muscleMenu.AddInstructionalButton(btnRotLeft)
        muscleMenu.AddInstructionalButton(btnRotRight)
        muscleMenu.AddInstructionalButton(btnOpenDoor)
        muscleMenu.AddInstructionalButton(btnCloseDoor)
        muscleMenu.AddInstructionalButton(btnChangeCam)
        For i As Integer = 0 To format.Count - 1
            Dim item As New UIMenuItem(format(i)("name") & " $" & format(i)("price"))
            muscleMenu.AddItem(item)
            With item
                .Model = format(i)("model")
                .Price = format(i)("price")
                .Car = format(i)("name")
            End With
        Next
        muscleMenu.AddItem(itemMuscleConfirm)
        muscleMenu.RefreshIndex()
        mainMenu.BindMenuToItem(muscleMenu, itemMuscle)
    End Sub

    Public Sub ReadOffroad()
        Dim format As New BTEFormatReader(offroad, parameters)
        Dim qty As Integer = format.Count - 1

        offroadMenu = New UIMenu("PDM Car Shop", "~r~OFF-ROAD")
        _menuPool.Add(offroadMenu)
        offroadMenu.AddInstructionalButton(btnRotLeft)
        offroadMenu.AddInstructionalButton(btnRotRight)
        offroadMenu.AddInstructionalButton(btnOpenDoor)
        offroadMenu.AddInstructionalButton(btnCloseDoor)
        offroadMenu.AddInstructionalButton(btnChangeCam)
        For i As Integer = 0 To format.Count - 1
            Dim item As New UIMenuItem(format(i)("name") & " $" & format(i)("price"))
            offroadMenu.AddItem(item)
            With item
                .Model = format(i)("model")
                .Price = format(i)("price")
                .Car = format(i)("name")
            End With
        Next
        offroadMenu.AddItem(itemOffRoadConfirm)
        offroadMenu.RefreshIndex()
        mainMenu.BindMenuToItem(offroadMenu, itemOffRoad)
    End Sub

    Public Sub ReadSuv()
        Dim format As New BTEFormatReader(suv, parameters)
        Dim qty As Integer = format.Count - 1

        suvMenu = New UIMenu("PDM Car Shop", "~r~SUVS")
        _menuPool.Add(suvMenu)
        suvMenu.AddInstructionalButton(btnRotLeft)
        suvMenu.AddInstructionalButton(btnRotRight)
        suvMenu.AddInstructionalButton(btnOpenDoor)
        suvMenu.AddInstructionalButton(btnCloseDoor)
        suvMenu.AddInstructionalButton(btnChangeCam)
        For i As Integer = 0 To format.Count - 1
            Dim item As New UIMenuItem(format(i)("name") & " $" & format(i)("price"))
            suvMenu.AddItem(item)
            With item
                .Model = format(i)("model")
                .Price = format(i)("price")
                .Car = format(i)("name")
            End With
        Next
        suvMenu.AddItem(itemSuvConfirm)
        suvMenu.RefreshIndex()
        mainMenu.BindMenuToItem(suvMenu, itemSuv)
    End Sub

    Public Sub ReadVan()
        Dim format As New BTEFormatReader(van, parameters)
        Dim qty As Integer = format.Count - 1

        vanMenu = New UIMenu("PDM Car Shop", "~r~VANS")
        _menuPool.Add(vanMenu)
        vanMenu.AddInstructionalButton(btnRotLeft)
        vanMenu.AddInstructionalButton(btnRotRight)
        vanMenu.AddInstructionalButton(btnOpenDoor)
        vanMenu.AddInstructionalButton(btnCloseDoor)
        vanMenu.AddInstructionalButton(btnChangeCam)
        For i As Integer = 0 To format.Count - 1
            Dim item As New UIMenuItem(format(i)("name") & " $" & format(i)("price"))
            vanMenu.AddItem(item)
            With item
                .Model = format(i)("model")
                .Price = format(i)("price")
                .Car = format(i)("name")
            End With
        Next
        vanMenu.AddItem(itemVanConfirm)
        vanMenu.RefreshIndex()
        mainMenu.BindMenuToItem(vanMenu, itemVan)
    End Sub

    Public Sub ReadUtility()
        Dim format As New BTEFormatReader(utility, parameters)
        Dim qty As Integer = format.Count - 1

        utilityMenu = New UIMenu("PDM Car Shop", "~r~UTILITIES")
        _menuPool.Add(utilityMenu)
        utilityMenu.AddInstructionalButton(btnRotLeft)
        utilityMenu.AddInstructionalButton(btnRotRight)
        utilityMenu.AddInstructionalButton(btnOpenDoor)
        utilityMenu.AddInstructionalButton(btnCloseDoor)
        utilityMenu.AddInstructionalButton(btnChangeCam)
        For i As Integer = 0 To format.Count - 1
            Dim item As New UIMenuItem(format(i)("name") & " $" & format(i)("price"))
            utilityMenu.AddItem(item)
            With item
                .Model = format(i)("model")
                .Price = format(i)("price")
                .Car = format(i)("name")
            End With
        Next
        utilityMenu.AddItem(itemUtilityConfirm)
        utilityMenu.RefreshIndex()
        mainMenu.BindMenuToItem(utilityMenu, itemUtility)
    End Sub

    Public Sub ReadArmoured()
        Dim format As New BTEFormatReader(armoured, parameters)
        Dim qty As Integer = format.Count - 1

        armouredMenu = New UIMenu("PDM Car Shop", "~r~ARMOURED")
        _menuPool.Add(armouredMenu)
        armouredMenu.AddInstructionalButton(btnRotLeft)
        armouredMenu.AddInstructionalButton(btnRotRight)
        armouredMenu.AddInstructionalButton(btnOpenDoor)
        armouredMenu.AddInstructionalButton(btnCloseDoor)
        armouredMenu.AddInstructionalButton(btnChangeCam)
        For i As Integer = 0 To format.Count - 1
            Dim item As New UIMenuItem(format(i)("name") & " $" & format(i)("price"))
            armouredMenu.AddItem(item)
            With item
                .Model = format(i)("model")
                .Price = format(i)("price")
                .Car = format(i)("name")
            End With
        Next
        armouredMenu.AddItem(itemArmouredConfirm)
        armouredMenu.RefreshIndex()
        mainMenu.BindMenuToItem(armouredMenu, itemArmoured)
    End Sub

    Public Sub ReadColor()
        Dim format As New BTEFormatReader(colour, paracolors)
        Dim qty As Integer = format.Count - 1

        colorMenu = New UIMenu("PDM Car Shop", "~r~PRIMARY COLOR")
        _menuPool.Add(colorMenu)
        colorMenu.AddInstructionalButton(btnRotLeft)
        colorMenu.AddInstructionalButton(btnRotRight)
        colorMenu.AddInstructionalButton(btnOpenDoor)
        colorMenu.AddInstructionalButton(btnCloseDoor)
        colorMenu.AddInstructionalButton(btnChangeCam)
        For i As Integer = 0 To format.Count - 1
            Dim item As New UIMenuItem(format(i)("name"))
            colorMenu.AddItem(item)
            With item
                .Price = format(i)("red")
                .Car = format(i)("green")
                .Model = format(i)("blue")
            End With
        Next
        colorMenu.RefreshIndex()
        confirmMenu.BindMenuToItem(colorMenu, itemColor)
    End Sub

    Public Sub ReadColor2()
        Dim format As New BTEFormatReader(colour, paracolors)
        Dim qty As Integer = format.Count - 1

        colorMenu2 = New UIMenu("PDM Car Shop", "~r~SECONDARY COLOR")
        _menuPool.Add(colorMenu2)
        colorMenu2.AddInstructionalButton(btnRotLeft)
        colorMenu2.AddInstructionalButton(btnRotRight)
        colorMenu2.AddInstructionalButton(btnOpenDoor)
        colorMenu2.AddInstructionalButton(btnCloseDoor)
        colorMenu2.AddInstructionalButton(btnChangeCam)
        For i As Integer = 0 To format.Count - 1
            Dim item As New UIMenuItem(format(i)("name"))
            colorMenu2.AddItem(item)
            With item
                .Price = format(i)("red")
                .Car = format(i)("green")
                .Model = format(i)("blue")
            End With
        Next
        colorMenu2.RefreshIndex()
        confirmMenu.BindMenuToItem(colorMenu2, itemColor2)
    End Sub

    Public Sub ReadPlate()
        Dim format As New BTEFormatReader(plate, paraplates)
        Dim qty As Integer = format.Count - 1

        plateMenu = New UIMenu("PDM Car Shop", "~r~PLATE")
        _menuPool.Add(plateMenu)
        plateMenu.AddInstructionalButton(btnRotLeft)
        plateMenu.AddInstructionalButton(btnRotRight)
        plateMenu.AddInstructionalButton(btnOpenDoor)
        plateMenu.AddInstructionalButton(btnCloseDoor)
        plateMenu.AddInstructionalButton(btnChangeCam)
        For i As Integer = 0 To format.Count - 1
            Dim item As New UIMenuItem(format(i)("name"))
            plateMenu.AddItem(item)
            With item
                .Car = format(i)("name")
                .Price = format(i)("index")
            End With
        Next
        plateMenu.RefreshIndex()
        confirmMenu.BindMenuToItem(plateMenu, itemPlate)
    End Sub

    Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, ByVal value As T) As T
        target = value
        Return value
    End Function

    Public Sub ReadConfirm()
        confirmMenu = New UIMenu("PDM Car Shop", "~r~PURCHASE ORDER")
        _menuPool.Add(confirmMenu)
        confirmMenu.AddInstructionalButton(btnRotLeft)
        confirmMenu.AddInstructionalButton(btnRotRight)
        confirmMenu.AddInstructionalButton(btnOpenDoor)
        confirmMenu.AddInstructionalButton(btnCloseDoor)
        confirmMenu.AddInstructionalButton(btnChangeCam)
        confirmMenu.AddItem(itemColor)
        confirmMenu.AddItem(itemColor2)
        confirmMenu.AddItem(itemPlate)
        confirmMenu.AddItem(New UIMenuItem("Plate Number", "Customize license plate number."))
        confirmMenu.AddItem(New UIMenuItem("Maximum Upgrade Vehicle", "Upgrade vehicle visual & performance."))
        confirmMenu.AddItem(New UIMenuItem("Open All Doors", "Open vehicle doors."))
        confirmMenu.AddItem(New UIMenuItem("Close All Doors", "Close vehicle doors."))
        confirmMenu.AddItem(New UIMenuItem("Confirm"))
        confirmMenu.RefreshIndex()
        motorMenu.BindMenuToItem(confirmMenu, itemMotorConfirm)
        compactMenu.BindMenuToItem(confirmMenu, itemCompactConfirm)
        coupeMenu.BindMenuToItem(confirmMenu, itemCoupeConfirm)
        sedanMenu.BindMenuToItem(confirmMenu, itemSedanConfirm)
        sportMenu.BindMenuToItem(confirmMenu, itemSportConfirm)
        classicMenu.BindMenuToItem(confirmMenu, itemClassicConfirm)
        muscleMenu.BindMenuToItem(confirmMenu, itemMuscleConfirm)
        exoticMenu.BindMenuToItem(confirmMenu, itemExoticConfirm)
        offroadMenu.BindMenuToItem(confirmMenu, itemOffRoadConfirm)
        suvMenu.BindMenuToItem(confirmMenu, itemSuvConfirm)
        vanMenu.BindMenuToItem(confirmMenu, itemVanConfirm)
        utilityMenu.BindMenuToItem(confirmMenu, itemUtilityConfirm)
        armouredMenu.BindMenuToItem(confirmMenu, itemArmouredConfirm)
    End Sub

    Public Sub ConfirmItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        'UI.Notify("You have selected: ~b~" + selectedItem.Text)
        If selectedItem.Text = "Confirm" Then
            If player.Money > vehiclePrice Then
                player.Money = (player.Money - vehiclePrice)
                confirmMenu.Visible = False
                World.RenderingCamera = Nothing
                vehPreview.IsDriveable = True
                Native.Function.Call(Hash.SET_VEHICLE_DOORS_SHUT, vehPreview, False)
                Native.Function.Call(Hash.TASK_WARP_PED_INTO_VEHICLE, playerPed, vehPreview, -1)
                vehPreview.MarkAsNoLongerNeeded()
                vehPreview = Nothing
                UI.DrawTexture(".\Scripts\PDMCarShop\purchase.png", 0, 0, 2000, New Point(CInt(UI.WIDTH * 0.3), 100), New Size(600, 50), 0.0, Color.White)
                Native.Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "PROPERTY_PURCHASE", "HUD_AWARDS", False)
            Else
                UI.Notify("You have insufficient funds to purchase this vehicle.", True)
            End If
        End If
        If selectedItem.Text = "Plate Number" Then
            Dim NumPlateText As String = Game.GetUserInput(vehPreview.NumberPlate, 9)
            If NumPlateText <> "" Then
                vehPreview.NumberPlate = NumPlateText
                If ChangeCamera = 0 Then
                    World.RenderingCamera = Nothing
                ElseIf ChangeCamera = 1 Then
                    World.RenderingCamera = World.CreateCamera(New GTA.Math.Vector3(-78.79827F, -1103.386F, 26.8126F), New GTA.Math.Vector3(Game.Player.Character.Rotation.X, Game.Player.Character.Rotation.Y, 253.0F), 10.0F)
                End If
            End If
        ElseIf selectedItem.Text = "Maximum Upgrade Vehicle" Then
            Native.Function.Call(Hash.SET_VEHICLE_MOD_KIT, vehPreview.Handle, 0)
            vehPreview.SetMod(VehicleMod.Suspension, 3, True)
            vehPreview.SetMod(VehicleMod.Engine, 3, True)
            vehPreview.SetMod(VehicleMod.Brakes, 2, True)
            vehPreview.SetMod(VehicleMod.Transmission, 3, True)
            vehPreview.SetMod(VehicleMod.Armor, 4, True)
            vehPreview.SetMod(VehicleWindowTint.Green, 6, True)
            vehPreview.SetMod(VehicleToggleMod.XenonHeadlights, 22, True)
            vehPreview.SetMod(VehicleToggleMod.Turbo, 18, True)
        ElseIf selectedItem.Text = "Open All Doors" Then
            vehPreview.OpenDoor(VehicleDoor.BackLeftDoor, False, False)
            vehPreview.OpenDoor(VehicleDoor.BackRightDoor, False, False)
            vehPreview.OpenDoor(VehicleDoor.FrontLeftDoor, False, False)
            vehPreview.OpenDoor(VehicleDoor.FrontRightDoor, False, False)
            vehPreview.OpenDoor(VehicleDoor.Hood, False, False)
            vehPreview.OpenDoor(VehicleDoor.Trunk, False, False)
            vehPreview.OpenDoor(VehicleDoor.Trunk2, False, False)
            Native.Function.Call(Hash.LOWER_CONVERTIBLE_ROOF, vehPreview, False)
        ElseIf selectedItem.Text = "Close All Doors" Then
            Native.Function.Call(Hash.SET_VEHICLE_DOORS_SHUT, vehPreview, False)
            Native.Function.Call(Hash.RAISE_CONVERTIBLE_ROOF, vehPreview, False)
        End If
    End Sub

    Public Sub ItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        If selectedItem.Text = "Enable" Then
            ModEnable = True
            UI.Notify("~r~Premium Deluxe Motorsport ~w~Mod Enabled.", True)
            SpawnSimeon()
            modMenu.Visible = False
            sender.MenuItems(0).SetRightBadge(UIMenuItem.BadgeStyle.Tick)
            sender.MenuItems(1).SetRightBadge(UIMenuItem.BadgeStyle.None)
        ElseIf selectedItem.Text = "Disable" Then
            ModEnable = False
            UI.Notify("~r~Premium Deluxe Motorsport ~w~Mod Disabled.", True)
            simeon.CurrentBlip.Remove()
            simeon.MarkAsNoLongerNeeded()
            simeon.Delete()
            modMenu.Visible = False
            sender.MenuItems(1).SetRightBadge(UIMenuItem.BadgeStyle.Tick)
            sender.MenuItems(0).SetRightBadge(UIMenuItem.BadgeStyle.None)
        ElseIf selectedItem.Text = "Key Settings" Then
            My.Settings.keyModEnable = [Enum].Parse(GetType(Keys), ReadIniValue(".\Scripts\PDMCarShop\config.ini", "OPTIONS", "ModEnableKey"), False)
            My.Settings.keyRotLeft = [Enum].Parse(GetType(Keys), ReadIniValue(".\Scripts\PDMCarShop\config.ini", "OPTIONS", "RotLeftKey"), False)
            My.Settings.keyRotRight = [Enum].Parse(GetType(Keys), ReadIniValue(".\Scripts\PDMCarShop\config.ini", "OPTIONS", "RotRightKey"), False)
            My.Settings.keyOpenDoor = [Enum].Parse(GetType(Keys), ReadIniValue(".\Scripts\PDMCarShop\config.ini", "OPTIONS", "OpenDoorKey"), False)
            My.Settings.keyCloseDoor = [Enum].Parse(GetType(Keys), ReadIniValue(".\Scripts\PDMCarShop\config.ini", "OPTIONS", "CloseDoorKey"), False)
            My.Settings.keyUse = [Enum].Parse(GetType(Keys), ReadIniValue(".\Scripts\PDMCarShop\config.ini", "OPTIONS", "UseKey"), False)
            My.Settings.keyChangeCam = [Enum].Parse(GetType(Keys), ReadIniValue(".\Scripts\PDMCarShop\config.ini", "OPTIONS", "ChangeCam"), False)
            My.Settings.Save()
            modMenu.Visible = False
            UI.Notify("Keys has been Saved.", True)
        ElseIf selectedItem.Text = "About" Then
            modMenu.Visible = False
            UI.Notify("Premium Deluxe Motorsport Car Shop Mod v2.1", True)
            UI.Notify("Release Date: 30 Jul 2015", True)
            UI.Notify("Mod Author: I'm Not MentaL", True)
            UI.Notify("Special Thanks: Rockstar Games, Alexander Blade, Crosire, Guad, EnergyStyle, LetsPlayOrDy,", True)
            UI.Notify("NewTheft, Calm, LCBuffalo, Gang1111, Matt_STS, frodzet, leftas & marhex", True)
        End If
    End Sub

    Public Sub ColorItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        vehPreview.CustomPrimaryColor = Color.FromArgb(255, selectedItem.Price, selectedItem.Car, selectedItem.Model)
        colorMenu.GoBack()
    End Sub

    Public Sub ColorItemSelectHandler2(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        vehPreview.CustomSecondaryColor = Color.FromArgb(255, selectedItem.Price, selectedItem.Car, selectedItem.Model)
        colorMenu2.GoBack()
    End Sub

    Private Sub PlateItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        Native.Function.Call(Hash.SET_VEHICLE_NUMBER_PLATE_TEXT_INDEX, vehPreview, selectedItem.Price)
        plateMenu.GoBack()
    End Sub

    Public Sub MotorItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        If selectedItem.Text = "Proceed to Checkout" Then
            'spawn nothing
        Else
            selectedVehicle = selectedItem.Car
            If vehPreview = Nothing Then
                vehPreview = World.CreateVehicle(selectedItem.Model, New GTA.Math.Vector3(-56.79958F, -1110.868F, 26.43581F), 6.122209F)
            Else
                vehPreview.Delete()
                vehPreview = World.CreateVehicle(selectedItem.Model, New GTA.Math.Vector3(-56.79958F, -1110.868F, 26.43581F), 6.122209F)
            End If
            vehPreview.Rotation = New GTA.Math.Vector3(0, 0, curRadius)
            vehPreview.IsDriveable = False
            vehPreview.DirtLevel = 0F
            vehiclePrice = selectedItem.Price
            categoryName = "Motorcycle"
        End If
    End Sub

    Public Sub ArmouredItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        If selectedItem.Text = "Proceed to Checkout" Then
            'spawn nothing
        Else
            selectedVehicle = selectedItem.Car
            If vehPreview = Nothing Then
                vehPreview = World.CreateVehicle(selectedItem.Model, New GTA.Math.Vector3(-56.79958F, -1110.868F, 26.43581F), 6.122209F)
            Else
                vehPreview.Delete()
                vehPreview = World.CreateVehicle(selectedItem.Model, New GTA.Math.Vector3(-56.79958F, -1110.868F, 26.43581F), 6.122209F)
            End If
            vehPreview.Rotation = New GTA.Math.Vector3(0, 0, curRadius)
            vehPreview.IsDriveable = False
            vehPreview.DirtLevel = 0F
            vehiclePrice = selectedItem.Price
            categoryName = "Armoured"
        End If
    End Sub

    Public Sub CompactItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        If selectedItem.Text = "Proceed to Checkout" Then
            'spawn nothing
        Else
            selectedVehicle = selectedItem.Car
            If vehPreview = Nothing Then
                vehPreview = World.CreateVehicle(selectedItem.Model, New GTA.Math.Vector3(-56.79958F, -1110.868F, 26.43581F), 6.122209F)
            Else
                vehPreview.Delete()
                vehPreview = World.CreateVehicle(selectedItem.Model, New GTA.Math.Vector3(-56.79958F, -1110.868F, 26.43581F), 6.122209F)
            End If
            vehPreview.Rotation = New GTA.Math.Vector3(0, 0, curRadius)
            vehPreview.IsDriveable = False
            vehPreview.DirtLevel = 0F
            vehiclePrice = selectedItem.Price
            categoryName = "Compact"
        End If
    End Sub

    Public Sub CoupeItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        If selectedItem.Text = "Proceed to Checkout" Then
            'spawn nothing
        Else
            selectedVehicle = selectedItem.Car
            If vehPreview = Nothing Then
                vehPreview = World.CreateVehicle(selectedItem.Model, New GTA.Math.Vector3(-56.79958F, -1110.868F, 26.43581F), 6.122209F)
            Else
                vehPreview.Delete()
                vehPreview = World.CreateVehicle(selectedItem.Model, New GTA.Math.Vector3(-56.79958F, -1110.868F, 26.43581F), 6.122209F)
            End If
            vehPreview.Rotation = New GTA.Math.Vector3(0, 0, curRadius)
            vehPreview.IsDriveable = False
            vehPreview.DirtLevel = 0F
            vehiclePrice = selectedItem.Price
            categoryName = "Coupe"
        End If
    End Sub

    Public Sub SedanItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        If selectedItem.Text = "Proceed to Checkout" Then
            'spawn nothing
        Else
            selectedVehicle = selectedItem.Car
            If vehPreview = Nothing Then
                vehPreview = World.CreateVehicle(selectedItem.Model, New GTA.Math.Vector3(-56.79958F, -1110.868F, 26.43581F), 6.122209F)
            Else
                vehPreview.Delete()
                vehPreview = World.CreateVehicle(selectedItem.Model, New GTA.Math.Vector3(-56.79958F, -1110.868F, 26.43581F), 6.122209F)
            End If
            vehPreview.Rotation = New GTA.Math.Vector3(0, 0, curRadius)
            vehPreview.IsDriveable = False
            vehPreview.DirtLevel = 0F
            vehiclePrice = selectedItem.Price
            categoryName = "Sedan"
        End If
    End Sub

    Public Sub SportItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        If selectedItem.Text = "Proceed to Checkout" Then
            'spawn nothing
        Else
            selectedVehicle = selectedItem.Car
            If vehPreview = Nothing Then
                vehPreview = World.CreateVehicle(selectedItem.Model, New GTA.Math.Vector3(-56.79958F, -1110.868F, 26.43581F), 6.122209F)
            Else
                vehPreview.Delete()
                vehPreview = World.CreateVehicle(selectedItem.Model, New GTA.Math.Vector3(-56.79958F, -1110.868F, 26.43581F), 6.122209F)
            End If
            vehPreview.Rotation = New GTA.Math.Vector3(0, 0, curRadius)
            vehPreview.IsDriveable = False
            vehPreview.DirtLevel = 0F
            vehiclePrice = selectedItem.Price
            categoryName = "Sport"
        End If
    End Sub

    Public Sub ClassicItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        If selectedItem.Text = "Proceed to Checkout" Then
            'spawn nothing
        Else
            selectedVehicle = selectedItem.Car
            If vehPreview = Nothing Then
                vehPreview = World.CreateVehicle(selectedItem.Model, New GTA.Math.Vector3(-56.79958F, -1110.868F, 26.43581F), 6.122209F)
            Else
                vehPreview.Delete()
                vehPreview = World.CreateVehicle(selectedItem.Model, New GTA.Math.Vector3(-56.79958F, -1110.868F, 26.43581F), 6.122209F)
            End If
            vehPreview.Rotation = New GTA.Math.Vector3(0, 0, curRadius)
            vehPreview.IsDriveable = False
            vehPreview.DirtLevel = 0F
            vehiclePrice = selectedItem.Price
            categoryName = "Classic"
        End If
    End Sub

    Public Sub ExoticItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        If selectedItem.Text = "Proceed to Checkout" Then
            'spawn nothing
        Else
            selectedVehicle = selectedItem.Car
            If vehPreview = Nothing Then
                vehPreview = World.CreateVehicle(selectedItem.Model, New GTA.Math.Vector3(-56.79958F, -1110.868F, 26.43581F), 6.122209F)
            Else
                vehPreview.Delete()
                vehPreview = World.CreateVehicle(selectedItem.Model, New GTA.Math.Vector3(-56.79958F, -1110.868F, 26.43581F), 6.122209F)
            End If
            vehPreview.Rotation = New GTA.Math.Vector3(0, 0, curRadius)
            vehPreview.IsDriveable = False
            vehPreview.DirtLevel = 0F
            vehiclePrice = selectedItem.Price
            categoryName = "Exotic"
        End If
    End Sub

    Public Sub MuscleItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        If selectedItem.Text = "Proceed to Checkout" Then
            'spawn nothing
        Else
            selectedVehicle = selectedItem.Car
            If vehPreview = Nothing Then
                vehPreview = World.CreateVehicle(selectedItem.Model, New GTA.Math.Vector3(-56.79958F, -1110.868F, 26.43581F), 6.122209F)
            Else
                vehPreview.Delete()
                vehPreview = World.CreateVehicle(selectedItem.Model, New GTA.Math.Vector3(-56.79958F, -1110.868F, 26.43581F), 6.122209F)
            End If
            vehPreview.Rotation = New GTA.Math.Vector3(0, 0, curRadius)
            vehPreview.IsDriveable = False
            vehPreview.DirtLevel = 0F
            vehiclePrice = selectedItem.Price
            categoryName = "Muscle"
        End If
    End Sub

    Public Sub OffroadItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        If selectedItem.Text = "Proceed to Checkout" Then
            'spawn nothing
        Else
            selectedVehicle = selectedItem.Car
            If vehPreview = Nothing Then
                vehPreview = World.CreateVehicle(selectedItem.Model, New GTA.Math.Vector3(-56.79958F, -1110.868F, 26.43581F), 6.122209F)
            Else
                vehPreview.Delete()
                vehPreview = World.CreateVehicle(selectedItem.Model, New GTA.Math.Vector3(-56.79958F, -1110.868F, 26.43581F), 6.122209F)
            End If
            vehPreview.Rotation = New GTA.Math.Vector3(0, 0, curRadius)
            vehPreview.IsDriveable = False
            vehPreview.DirtLevel = 0F
            vehiclePrice = selectedItem.Price
            categoryName = "Offroad"
        End If
    End Sub

    Public Sub SuvItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        If selectedItem.Text = "Proceed to Checkout" Then
            'spawn nothing
        Else
            selectedVehicle = selectedItem.Car
            If vehPreview = Nothing Then
                vehPreview = World.CreateVehicle(selectedItem.Model, New GTA.Math.Vector3(-56.79958F, -1110.868F, 26.43581F), 6.122209F)
            Else
                vehPreview.Delete()
                vehPreview = World.CreateVehicle(selectedItem.Model, New GTA.Math.Vector3(-56.79958F, -1110.868F, 26.43581F), 6.122209F)
            End If
            vehPreview.Rotation = New GTA.Math.Vector3(0, 0, curRadius)
            vehPreview.IsDriveable = False
            vehPreview.DirtLevel = 0F
            vehiclePrice = selectedItem.Price
            categoryName = "SUV"
        End If
    End Sub

    Public Sub VanItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        If selectedItem.Text = "Proceed to Checkout" Then
            'spawn nothing
        Else
            selectedVehicle = selectedItem.Car
            If vehPreview = Nothing Then
                vehPreview = World.CreateVehicle(selectedItem.Model, New GTA.Math.Vector3(-56.79958F, -1110.868F, 26.43581F), 6.122209F)
            Else
                vehPreview.Delete()
                vehPreview = World.CreateVehicle(selectedItem.Model, New GTA.Math.Vector3(-56.79958F, -1110.868F, 26.43581F), 6.122209F)
            End If
            vehPreview.Rotation = New GTA.Math.Vector3(0, 0, curRadius)
            vehPreview.IsDriveable = False
            vehPreview.DirtLevel = 0F
            vehiclePrice = selectedItem.Price
            categoryName = "Van"
        End If
    End Sub

    Public Sub UtilityItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        If selectedItem.Text = "Proceed to Checkout" Then
            'spawn nothing
        Else
            selectedVehicle = selectedItem.Car
            If vehPreview = Nothing Then
                vehPreview = World.CreateVehicle(selectedItem.Model, New GTA.Math.Vector3(-56.79958F, -1110.868F, 26.43581F), 6.122209F)
            Else
                vehPreview.Delete()
                vehPreview = World.CreateVehicle(selectedItem.Model, New GTA.Math.Vector3(-56.79958F, -1110.868F, 26.43581F), 6.122209F)
            End If
            vehPreview.Rotation = New GTA.Math.Vector3(0, 0, curRadius)
            vehPreview.IsDriveable = False
            vehPreview.DirtLevel = 0F
            vehiclePrice = selectedItem.Price
            categoryName = "Utility"
        End If
    End Sub

    Public Sub PlateItemChangeHandler(sender As UIMenu, index As Integer)
        Native.Function.Call(Hash.SET_VEHICLE_NUMBER_PLATE_TEXT_INDEX, vehPreview, sender.MenuItems(index).Price)
    End Sub

    Public Sub ColorItemChangeHandler(sender As UIMenu, index As Integer)
        vehPreview.CustomPrimaryColor = Color.FromArgb(255, sender.MenuItems(index).Price, sender.MenuItems(index).Car, sender.MenuItems(index).Model)
    End Sub

    Public Sub ColorItemChangeHandler2(sender As UIMenu, index As Integer)
        vehPreview.CustomSecondaryColor = Color.FromArgb(255, sender.MenuItems(index).Price, sender.MenuItems(index).Car, sender.MenuItems(index).Model)
    End Sub

    Public Sub MenuCloseHandler(sender As UIMenu)
        If selectedVehicle IsNot Nothing Then
            vehPreview.Delete()
        End If
        World.RenderingCamera = Nothing
    End Sub

    Public Sub OnTick(o As Object, e As EventArgs)
        _menuPool.ProcessMenus()

        If ModEnable = True Then
            simeonDist = GTA.World.GetDistance(Game.Player.Character.Position, simeon.Position)

            If simeon.IsDead Then
                simeon.CurrentBlip.Remove()
                ModEnable = False
                simeon.MarkAsNoLongerNeeded()
                simeon.Delete()
            End If

            If Not playerPed.IsInVehicle AndAlso Not playerPed.IsDead AndAlso simeonDist < 3.0F Then
                'mainMenu.Visible = True
                UI.Notify("Welcome to ~h~~r~Premium Deluxe Motorsport~h~~s~, Please press E to browse Vehicles.", True)
            End If
        End If
    End Sub

    Public Sub OnKeyDown(o As Object, e As KeyEventArgs)
        If e.KeyCode = My.Settings.keyUse AndAlso ModEnable = True AndAlso simeonDist < 3.0F Then
            'mainMenu.Visible = Not mainMenu.Visible
            mainMenu.Visible = True
            ChangeCamera = 1
            World.RenderingCamera = World.CreateCamera(New GTA.Math.Vector3(-78.79827F, -1103.386F, 26.8126F), New GTA.Math.Vector3(Game.Player.Character.Rotation.X, Game.Player.Character.Rotation.Y, 253.0F), 10.0F)
            Game.Player.Character.Position = New GTA.Math.Vector3(-43.79905F, -1116.247F, 25.43394F)
        End If

        If e.KeyCode = My.Settings.keyModEnable Then
            modMenu.Visible = Not mainMenu.Visible
        End If

        If e.KeyCode = My.Settings.keyRotLeft AndAlso ModEnable = True AndAlso simeonDist < 40.0F Then
            curRadius = curRadius - 3
            vehPreview.Rotation = New GTA.Math.Vector3(0, 0, curRadius)
        ElseIf e.KeyCode = My.Settings.keyRotRight AndAlso ModEnable = True AndAlso simeonDist < 40.0F Then
            curRadius = curRadius + 3
            vehPreview.Rotation = New GTA.Math.Vector3(0, 0, curRadius)
        ElseIf e.KeyCode = My.Settings.keyOpenDoor AndAlso ModEnable = True AndAlso simeonDist < 40.0F Then
            vehPreview.OpenDoor(VehicleDoor.BackLeftDoor, False, False)
            vehPreview.OpenDoor(VehicleDoor.BackRightDoor, False, False)
            vehPreview.OpenDoor(VehicleDoor.FrontLeftDoor, False, False)
            vehPreview.OpenDoor(VehicleDoor.FrontRightDoor, False, False)
            vehPreview.OpenDoor(VehicleDoor.Hood, False, False)
            vehPreview.OpenDoor(VehicleDoor.Trunk, False, False)
            vehPreview.OpenDoor(VehicleDoor.Trunk2, False, False)
            Native.Function.Call(Hash.LOWER_CONVERTIBLE_ROOF, vehPreview, False)
        ElseIf e.KeyCode = My.Settings.keyCloseDoor AndAlso ModEnable = True AndAlso simeonDist < 40.0F Then
            Native.Function.Call(Hash.SET_VEHICLE_DOORS_SHUT, vehPreview, False)
            Native.Function.Call(Hash.RAISE_CONVERTIBLE_ROOF, vehPreview, False)
        ElseIf e.KeyCode = My.Settings.keyChangeCam AndAlso ModEnable = True AndAlso simeonDist < 40.0F AndAlso ChangeCamera = 0 Then
            World.RenderingCamera = Nothing
            ChangeCamera = (ChangeCamera + 1)
        ElseIf e.KeyCode = My.Settings.keyChangeCam AndAlso ModEnable = True AndAlso simeonDist < 40.0F AndAlso ChangeCamera = 1 Then
            World.RenderingCamera = World.CreateCamera(New GTA.Math.Vector3(-78.79827F, -1103.386F, 26.8126F), New GTA.Math.Vector3(Game.Player.Character.Rotation.X, Game.Player.Character.Rotation.Y, 253.0F), 10.0F)
            ChangeCamera = (ChangeCamera - 1)
        End If
    End Sub

End Class
