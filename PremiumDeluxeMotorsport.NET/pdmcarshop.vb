Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports GTA
Imports GTA.Native
Imports GTA.Math
Imports PDMCarShopGUI
Imports System.Reflection

Public Class pdmcarshop
    Inherits Script

    Private player As Player
    Private playerPed As Ped
    Private simeon As Vector3
    Private testDriveVector As Vector3
    Private simeonBlip As Blip
    Private selectedVehicle As String
    Private vehPreview As Vehicle
    Private simeonDist As Single
    Private testdriveDist As Single
    Private ModEnable As Boolean = False
    Private curRadius As Integer = 0
    Private PlayerCash As Integer
    Private vehiclePrice As Integer
    Private ChangeCamera As Integer = 0
    Private vehPreviewPosition As Vector3
    Private cameraPosition As Vector3
    Private playerPosition As Vector3
    Private Price As Decimal = 0
    Private testDrive As Integer = 1
    Private hideHud As Boolean = False

    Private mainMenu, modMenu, colorMenu, colorMenu2, plateMenu, confirmMenu, colorMenu3, colorMenu4, colorMenu5, rimColorMenu, neonColorMenu, smokeColorMenu As UIMenu
    Private motorMenu, compactMenu, coupeMenu, sedanMenu, sportMenu, classicMenu, exoticMenu, muscleMenu, offroadMenu, suvMenu, vanMenu, utilityMenu, armouredMenu, lowriderMenu As UIMenu

    Dim itemMotor As New UIMenuItem("Motorcycles")
    Dim itemCompact As New UIMenuItem("Compacts")
    Dim itemCoupe As New UIMenuItem("Coupes")
    Dim itemSedan As New UIMenuItem("Sedans")
    Dim itemSport As New UIMenuItem("Sports")
    Dim itemClassic As New UIMenuItem("Classics")
    Dim itemExotic As New UIMenuItem("Exotics")
    Dim itemMuscle As New UIMenuItem("Muscles")
    Dim itemOffRoad As New UIMenuItem("Off-Road")
    Dim itemSuv As New UIMenuItem("SUVs")
    Dim itemVan As New UIMenuItem("Vans")
    Dim itemUtility As New UIMenuItem("Utilities")
    Dim itemArmoured As New UIMenuItem("Armoured")
    Dim itemLowrider As New UIMenuItem("Lowriders")
    Dim itemColor As New UIMenuItem("Custom Primary Color", "Transform vehicle appearance.")
    Dim itemColor2 As New UIMenuItem("Custom Secondary Color", "Transform vehicle appearance.")
    Dim itemColor3 As New UIMenuItem("Primary Color", "Transform vehicle appearance.")
    Dim itemColor4 As New UIMenuItem("Secondary Color", "Transform vehicle appearance.")
    Dim itemColor5 As New UIMenuItem("Pearlescent Color", "Transform vehicle appearance.")
    Dim itemRimColor As New UIMenuItem("Rim Color", "Transform wheels appearance.")
    Dim itemNeonColor As New UIMenuItem("Neon Color", "Transform vehicle appearance.")
    Dim itemSmokeColor As New UIMenuItem("Tyre Smoke Color", "Transform tyre smoke appearance.")
    Dim itemPlate As New UIMenuItem("Plate", "Customize license plate.")

    Dim btnRotLeft As New InstructionalButton(GTA.Control.ParachuteBrakeLeft, "Rotate Left")
    Dim btnRotRight As New InstructionalButton(GTA.Control.ParachuteBrakeRight, "Rotate Right")
    Dim btnOpenDoor As New InstructionalButton(GTA.Control.SelectWeaponUnarmed, "Open Doors")
    Dim btnCloseDoor As New InstructionalButton(GTA.Control.SelectWeaponMelee, "Close Doors")
    Dim btnChangeCam As New InstructionalButton(GTA.Control.NextCamera, "Change Camera")
    Dim btnConfirm As New InstructionalButton(GTA.Control.Jump, "Checkout")

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
    Private lowrider As String = Application.StartupPath & "\scripts\PDMCarShop\lowrider.ini"
    Private colour As String = Application.StartupPath & "\scripts\PDMCarShop\color.ini"
    Private colour2 As String = Application.StartupPath & "\scripts\PDMCarShop\color2.ini"
    Private plate As String = Application.StartupPath & "\scripts\PDMCarShop\plate.ini"
    Private parameters As String() = {"[name]", "[hash]", "[price]", "[price2]", "[model]"}
    Private paracolors As String() = {"[red]", "[green]", "[blue]", "[name]"}
    Private paracolors2 As String() = {"[code]", "[type]", "[name]"}
    Private paraplates As String() = {"[index]", "[name]"}

    Private _scaleform As Scaleform
    Private _displayTimer As Timer
    Private _fadeTimer As Timer
    Private _menuPool As MenuPool

    Private spoiler, fbumper, rbumper, sskirt, frame, grille, hood, fender, rfender, roof, exhaust, suspension, engine, brakes, transms, armor, fWheels, bWheels As Integer

    Public Sub New()
        Try
            player = Game.Player
            playerPed = Game.Player.Character
            PlayerCash = playerPed.Money

            AddHandler Tick, AddressOf OnTick
            AddHandler KeyDown, AddressOf OnKeyDown

            _menuPool = New MenuPool()

            modMenu = New UIMenu("", "~b~VERSION: " & My.Application.Info.Version.ToString, New Point(0, 0))
            modMenu.SetMenuWidthOffset(11)
            modMenu.MouseEdgeEnabled = False
            modMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PDMCarShopMod.shopui_title_pdm.png"))
            _menuPool.Add(modMenu)
            modMenu.AddItem(New UIMenuItem("Enable Mod", "Enable Premium Deluxe Motorsport Car Shop Mod."))
            modMenu.AddItem(New UIMenuItem("Disable Mod", "Disable Premium Deluxe Motorsport Car Shop Mod."))
            modMenu.AddItem(New UIMenuItem("Enable Clear Color", "Color will clear when change Custom Color."))
            modMenu.AddItem(New UIMenuItem("Disable Clear Color", "Color will not clear when change Custom Color."))
            modMenu.AddItem(New UIMenuItem("Showroom", "Use the showroom."))
            modMenu.AddItem(New UIMenuItem("Car Park", "Use the car park."))
            modMenu.AddItem(New UIMenuItem("Refresh Settings", "Refresh Keys & Settings After you saving config.ini file."))
            modMenu.AddItem(New UIMenuItem("About", "About PDM Car Shop"))
            modMenu.RefreshIndex()

            mainMenu = New UIMenu("", "~b~CATEGORIES", New Point(0, 0))
            mainMenu.SetMenuWidthOffset(11)
            mainMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PDMCarShopMod.shopui_title_pdm.png"))
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
            mainMenu.AddItem(itemLowrider)
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
            ReadLowrider()
            ReadConfirm()
            ReadColor()
            ReadColor2()
            ReadColor3()
            ReadColor4()
            ReadColor5()
            ReadRimColor()
            ReadNeonColor()
            ReadSmokeColor()
            ReadPlate()


            AddHandler mainMenu.OnMenuClose, AddressOf MenuCloseHandler
            AddHandler confirmMenu.OnMenuClose, AddressOf ConfirmCloseHandler

            AddHandler mainMenu.OnItemSelect, AddressOf ItemSelectHandler
            AddHandler motorMenu.OnItemSelect, AddressOf CategoryItemSelectHandler
            AddHandler compactMenu.OnItemSelect, AddressOf CategoryItemSelectHandler
            AddHandler coupeMenu.OnItemSelect, AddressOf CategoryItemSelectHandler
            AddHandler sedanMenu.OnItemSelect, AddressOf CategoryItemSelectHandler
            AddHandler sportMenu.OnItemSelect, AddressOf CategoryItemSelectHandler
            AddHandler classicMenu.OnItemSelect, AddressOf CategoryItemSelectHandler
            AddHandler exoticMenu.OnItemSelect, AddressOf CategoryItemSelectHandler
            AddHandler muscleMenu.OnItemSelect, AddressOf CategoryItemSelectHandler
            AddHandler offroadMenu.OnItemSelect, AddressOf CategoryItemSelectHandler
            AddHandler suvMenu.OnItemSelect, AddressOf CategoryItemSelectHandler
            AddHandler vanMenu.OnItemSelect, AddressOf CategoryItemSelectHandler
            AddHandler utilityMenu.OnItemSelect, AddressOf CategoryItemSelectHandler
            AddHandler armouredMenu.OnItemSelect, AddressOf CategoryItemSelectHandler
            AddHandler lowriderMenu.OnItemSelect, AddressOf CategoryItemSelectHandler

            AddHandler confirmMenu.OnItemSelect, AddressOf ConfirmItemSelectHandler
            AddHandler modMenu.OnItemSelect, AddressOf ItemSelectHandler
            AddHandler colorMenu.OnItemSelect, AddressOf ColorItemSelectHandler
            AddHandler colorMenu2.OnItemSelect, AddressOf ColorItemSelectHandler2
            AddHandler colorMenu3.OnItemSelect, AddressOf ColorItemSelectHandler3
            AddHandler colorMenu4.OnItemSelect, AddressOf ColorItemSelectHandler4
            AddHandler colorMenu5.OnItemSelect, AddressOf ColorItemSelectHandler5
            AddHandler rimColorMenu.OnItemSelect, AddressOf RimColorItemSelectHandler
            AddHandler neonColorMenu.OnItemSelect, AddressOf NeonColorItemSelectHandler
            AddHandler smokeColorMenu.OnItemSelect, AddressOf SmokeColorItemSelectHandler
            AddHandler plateMenu.OnItemSelect, AddressOf PlateItemSelectHandler

            AddHandler colorMenu.OnIndexChange, AddressOf ColorItemChangeHandler
            AddHandler colorMenu2.OnIndexChange, AddressOf ColorItemChangeHandler2
            AddHandler colorMenu3.OnIndexChange, AddressOf ColorItemChangeHandler3
            AddHandler colorMenu4.OnIndexChange, AddressOf ColorItemChangeHandler4
            AddHandler colorMenu5.OnIndexChange, AddressOf ColorItemChangeHandler5
            AddHandler rimColorMenu.OnIndexChange, AddressOf RimColorItemChangeHandler
            AddHandler neonColorMenu.OnIndexChange, AddressOf NeonColorItemChangeHandler
            AddHandler smokeColorMenu.OnIndexChange, AddressOf SmokeColorItemChangeHandler
            AddHandler plateMenu.OnIndexChange, AddressOf PlateItemChangeHandler

            My.Settings.keyModEnable = [Enum].Parse(GetType(Keys), ReadIniValue(".\Scripts\PDMCarShop\config.ini", "OPTIONS", "ModEnableKey"), False)
            My.Settings.showRoom = ReadIniValue(".\Scripts\PDMCarShop\config.ini", "OPTIONS", "Showroom")
            My.Settings.removeColor = ReadIniValue(".\Scripts\PDMCarShop\config.ini", "OPTIONS", "ClearColor")
            My.Settings.Save()

            _displayTimer = New Timer(2200)
            _fadeTimer = New Timer(2000)
            _scaleform = New Scaleform([Function].[Call](Of Integer)(Hash.REQUEST_SCALEFORM_MOVIE, "MP_BIG_MESSAGE_FREEMODE"))

            If ReadIniValue(".\Scripts\PDMCarShop\config.ini", "OPTIONS", "DefaultEnable") = True Then
                ModEnable = True
                SpawnSimeon()
                modMenu.MenuItems(0).SetRightBadge(UIMenuItem.BadgeStyle.Tick)
                modMenu.MenuItems(1).SetRightBadge(UIMenuItem.BadgeStyle.None)
            Else
                modMenu.MenuItems(1).SetRightBadge(UIMenuItem.BadgeStyle.Tick)
            End If

            If ReadIniValue(".\Scripts\PDMCarShop\config.ini", "OPTIONS", "ClearColor") = True Then
                modMenu.MenuItems(2).SetRightBadge(UIMenuItem.BadgeStyle.Tick)
                modMenu.MenuItems(3).SetRightBadge(UIMenuItem.BadgeStyle.None)
            Else
                modMenu.MenuItems(3).SetRightBadge(UIMenuItem.BadgeStyle.Tick)
                modMenu.MenuItems(2).SetRightBadge(UIMenuItem.BadgeStyle.None)
            End If

            'If ReadIniValue(".\Scripts\PDMCarShop\config.ini", "OPTIONS", "Showroom") = False Then
            If My.Settings.showRoom = False Then
                'Outside
                vehPreviewPosition = New Vector3(-56.79958F, -1110.868F, 26.43581F)
                cameraPosition = New Vector3(-78.79827F, -1103.386F, 26.8126F)
                playerPosition = New Vector3(-43.79905F, -1116.247F, 25.43394F)
                modMenu.MenuItems(5).SetRightBadge(UIMenuItem.BadgeStyle.Tick)
                modMenu.MenuItems(4).SetRightBadge(UIMenuItem.BadgeStyle.None)
            Else
                'Inside
                vehPreviewPosition = New Vector3(-44.142F, -1098.996F, 26.422F)
                cameraPosition = New Vector3(-59.76299F, -1093.913F, 26.622F)
                playerPosition = New Vector3(-54.16683F, -1088.698F, 25.42233F)
                modMenu.MenuItems(4).SetRightBadge(UIMenuItem.BadgeStyle.Tick)
                modMenu.MenuItems(5).SetRightBadge(UIMenuItem.BadgeStyle.None)
            End If
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub SpawnSimeon()
        Try
            simeon = New Vector3(-40.3857F, -1108.79F, 25.4375F)
            testDriveVector = New Vector3(66.55125F, -1356.585F, 29.08711F)
            simeonBlip = World.CreateBlip(simeon)
            simeonBlip.Sprite = BlipSprite.PersonalVehicleCar
            simeonBlip.Color = BlipColor.Red
            simeonBlip.IsShortRange = True
            SetBlipName("Premium Deluxe Motorsport", simeonBlip)
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub ReadMotorcycle()
        Try
            Dim format As New BTEFormatReader(motorcycle, parameters)
            Dim qty As Integer = format.Count - 1

            motorMenu = New UIMenu("", "~r~MOTORCYCLES", New Point(0, 0))
            motorMenu.SetMenuWidthOffset(11)
            motorMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PDMCarShopMod.shopui_title_pdm.png"))
            _menuPool.Add(motorMenu)
            motorMenu.AddInstructionalButton(btnConfirm)
            motorMenu.AddInstructionalButton(btnRotLeft)
            motorMenu.AddInstructionalButton(btnRotRight)
            motorMenu.AddInstructionalButton(btnOpenDoor)
            motorMenu.AddInstructionalButton(btnCloseDoor)
            motorMenu.AddInstructionalButton(btnChangeCam)
            For i As Integer = 0 To format.Count - 1
                Price = format(i)("price")
                Dim item As New UIMenuItem(format(i)("name"))
                motorMenu.AddItem(item)
                With item
                    .SetRightLabel("$" & Price.ToString("N"))
                    .Model = format(i)("model")
                    .Price = format(i)("price")
                    .Car = format(i)("name")
                End With
            Next
            motorMenu.RefreshIndex()
            mainMenu.BindMenuToItem(motorMenu, itemMotor)
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub ReadCompact()
        Try
            Dim format As New BTEFormatReader(compact, parameters)
            Dim qty As Integer = format.Count - 1

            compactMenu = New UIMenu("", "~r~COMPACTS", New Point(0, 0))
            compactMenu.SetMenuWidthOffset(11)
            compactMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PDMCarShopMod.shopui_title_pdm.png"))
            _menuPool.Add(compactMenu)
            compactMenu.AddInstructionalButton(btnConfirm)
            compactMenu.AddInstructionalButton(btnRotLeft)
            compactMenu.AddInstructionalButton(btnRotRight)
            compactMenu.AddInstructionalButton(btnOpenDoor)
            compactMenu.AddInstructionalButton(btnCloseDoor)
            compactMenu.AddInstructionalButton(btnChangeCam)
            For i As Integer = 0 To format.Count - 1
                Price = format(i)("price")
                Dim item As New UIMenuItem(format(i)("name"))
                compactMenu.AddItem(item)
                With item
                    .SetRightLabel("$" & Price.ToString("N"))
                    .Model = format(i)("model")
                    .Price = format(i)("price")
                    .Car = format(i)("name")
                End With
            Next
            compactMenu.RefreshIndex()
            mainMenu.BindMenuToItem(compactMenu, itemCompact)
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub ReadCoupe()
        Try
            Dim format As New BTEFormatReader(coupe, parameters)
            Dim qty As Integer = format.Count - 1

            coupeMenu = New UIMenu("", "~r~COUPES", New Point(0, 0))
            coupeMenu.SetMenuWidthOffset(11)
            coupeMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PDMCarShopMod.shopui_title_pdm.png"))
            _menuPool.Add(coupeMenu)
            coupeMenu.AddInstructionalButton(btnConfirm)
            coupeMenu.AddInstructionalButton(btnRotLeft)
            coupeMenu.AddInstructionalButton(btnRotRight)
            coupeMenu.AddInstructionalButton(btnOpenDoor)
            coupeMenu.AddInstructionalButton(btnCloseDoor)
            coupeMenu.AddInstructionalButton(btnChangeCam)
            For i As Integer = 0 To format.Count - 1
                Price = format(i)("price")
                Dim item As New UIMenuItem(format(i)("name"))
                coupeMenu.AddItem(item)
                With item
                    .SetRightLabel("$" & Price.ToString("N"))
                    .Model = format(i)("model")
                    .Price = format(i)("price")
                    .Car = format(i)("name")
                End With
            Next
            coupeMenu.RefreshIndex()
            mainMenu.BindMenuToItem(coupeMenu, itemCoupe)
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub ReadSedan()
        Try
            Dim format As New BTEFormatReader(sedan, parameters)
            Dim qty As Integer = format.Count - 1

            sedanMenu = New UIMenu("", "~r~SEDANS", New Point(0, 0))
            sedanMenu.SetMenuWidthOffset(11)
            sedanMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PDMCarShopMod.shopui_title_pdm.png"))
            _menuPool.Add(sedanMenu)
            sedanMenu.AddInstructionalButton(btnConfirm)
            sedanMenu.AddInstructionalButton(btnRotLeft)
            sedanMenu.AddInstructionalButton(btnRotRight)
            sedanMenu.AddInstructionalButton(btnOpenDoor)
            sedanMenu.AddInstructionalButton(btnCloseDoor)
            sedanMenu.AddInstructionalButton(btnChangeCam)
            For i As Integer = 0 To format.Count - 1
                Price = format(i)("price")
                Dim item As New UIMenuItem(format(i)("name"))
                sedanMenu.AddItem(item)
                With item
                    .SetRightLabel("$" & Price.ToString("N"))
                    .Model = format(i)("model")
                    .Price = format(i)("price")
                    .Car = format(i)("name")
                End With
            Next
            sedanMenu.RefreshIndex()
            mainMenu.BindMenuToItem(sedanMenu, itemSedan)
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub ReadSport()
        Try
            Dim format As New BTEFormatReader(sport, parameters)
            Dim qty As Integer = format.Count - 1

            sportMenu = New UIMenu("", "~r~SPORTS", New Point(0, 0))
            sportMenu.SetMenuWidthOffset(11)
            sportMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PDMCarShopMod.shopui_title_pdm.png"))
            _menuPool.Add(sportMenu)
            sportMenu.AddInstructionalButton(btnConfirm)
            sportMenu.AddInstructionalButton(btnRotLeft)
            sportMenu.AddInstructionalButton(btnRotRight)
            sportMenu.AddInstructionalButton(btnOpenDoor)
            sportMenu.AddInstructionalButton(btnCloseDoor)
            sportMenu.AddInstructionalButton(btnChangeCam)
            For i As Integer = 0 To format.Count - 1
                Price = format(i)("price")
                Dim item As New UIMenuItem(format(i)("name"))
                sportMenu.AddItem(item)
                With item
                    .SetRightLabel("$" & Price.ToString("N"))
                    .Model = format(i)("model")
                    .Price = format(i)("price")
                    .Car = format(i)("name")
                End With
            Next
            sportMenu.RefreshIndex()
            mainMenu.BindMenuToItem(sportMenu, itemSport)
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub ReadClassic()
        Try
            Dim format As New BTEFormatReader(classic, parameters)
            Dim qty As Integer = format.Count - 1

            classicMenu = New UIMenu("", "~r~CLASSICS", New Point(0, 0))
            classicMenu.SetMenuWidthOffset(11)
            classicMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PDMCarShopMod.shopui_title_pdm.png"))
            _menuPool.Add(classicMenu)
            classicMenu.AddInstructionalButton(btnConfirm)
            classicMenu.AddInstructionalButton(btnRotLeft)
            classicMenu.AddInstructionalButton(btnRotRight)
            classicMenu.AddInstructionalButton(btnOpenDoor)
            classicMenu.AddInstructionalButton(btnCloseDoor)
            classicMenu.AddInstructionalButton(btnChangeCam)
            For i As Integer = 0 To format.Count - 1
                Price = format(i)("price")
                Dim item As New UIMenuItem(format(i)("name"))
                classicMenu.AddItem(item)
                With item
                    .SetRightLabel("$" & Price.ToString("N"))
                    .Model = format(i)("model")
                    .Price = format(i)("price")
                    .Car = format(i)("name")
                End With
            Next
            classicMenu.RefreshIndex()
            mainMenu.BindMenuToItem(classicMenu, itemClassic)
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub ReadExotic()
        Try
            Dim format As New BTEFormatReader(exotic, parameters)
            Dim qty As Integer = format.Count - 1

            exoticMenu = New UIMenu("", "~r~EXOTICS", New Point(0, 0))
            exoticMenu.SetMenuWidthOffset(11)
            exoticMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PDMCarShopMod.shopui_title_pdm.png"))
            _menuPool.Add(exoticMenu)
            exoticMenu.AddInstructionalButton(btnConfirm)
            exoticMenu.AddInstructionalButton(btnRotLeft)
            exoticMenu.AddInstructionalButton(btnRotRight)
            exoticMenu.AddInstructionalButton(btnOpenDoor)
            exoticMenu.AddInstructionalButton(btnCloseDoor)
            exoticMenu.AddInstructionalButton(btnChangeCam)
            For i As Integer = 0 To format.Count - 1
                Price = format(i)("price")
                Dim item As New UIMenuItem(format(i)("name"))
                exoticMenu.AddItem(item)
                With item
                    .SetRightLabel("$" & Price.ToString("N"))
                    .Model = format(i)("model")
                    .Price = format(i)("price")
                    .Car = format(i)("name")
                End With
            Next
            exoticMenu.RefreshIndex()
            mainMenu.BindMenuToItem(exoticMenu, itemExotic)
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub ReadMuscle()
        Try
            Dim format As New BTEFormatReader(muscle, parameters)
            Dim qty As Integer = format.Count - 1

            muscleMenu = New UIMenu("", "~r~MUSCLES", New Point(0, 0))
            muscleMenu.SetMenuWidthOffset(11)
            muscleMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PDMCarShopMod.shopui_title_pdm.png"))
            _menuPool.Add(muscleMenu)
            muscleMenu.AddInstructionalButton(btnConfirm)
            muscleMenu.AddInstructionalButton(btnRotLeft)
            muscleMenu.AddInstructionalButton(btnRotRight)
            muscleMenu.AddInstructionalButton(btnOpenDoor)
            muscleMenu.AddInstructionalButton(btnCloseDoor)
            muscleMenu.AddInstructionalButton(btnChangeCam)
            For i As Integer = 0 To format.Count - 1
                Price = format(i)("price")
                Dim item As New UIMenuItem(format(i)("name"))
                muscleMenu.AddItem(item)
                With item
                    .SetRightLabel("$" & Price.ToString("N"))
                    .Model = format(i)("model")
                    .Price = format(i)("price")
                    .Car = format(i)("name")
                End With
            Next
            muscleMenu.RefreshIndex()
            mainMenu.BindMenuToItem(muscleMenu, itemMuscle)
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub ReadOffroad()
        Try
            Dim format As New BTEFormatReader(offroad, parameters)
            Dim qty As Integer = format.Count - 1

            offroadMenu = New UIMenu("", "~r~OFF-ROAD", New Point(0, 0))
            offroadMenu.SetMenuWidthOffset(11)
            offroadMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PDMCarShopMod.shopui_title_pdm.png"))
            _menuPool.Add(offroadMenu)
            offroadMenu.AddInstructionalButton(btnConfirm)
            offroadMenu.AddInstructionalButton(btnRotLeft)
            offroadMenu.AddInstructionalButton(btnRotRight)
            offroadMenu.AddInstructionalButton(btnOpenDoor)
            offroadMenu.AddInstructionalButton(btnCloseDoor)
            offroadMenu.AddInstructionalButton(btnChangeCam)
            For i As Integer = 0 To format.Count - 1
                Price = format(i)("price")
                Dim item As New UIMenuItem(format(i)("name"))
                offroadMenu.AddItem(item)
                With item
                    .SetRightLabel("$" & Price.ToString("N"))
                    .Model = format(i)("model")
                    .Price = format(i)("price")
                    .Car = format(i)("name")
                End With
            Next
            offroadMenu.RefreshIndex()
            mainMenu.BindMenuToItem(offroadMenu, itemOffRoad)
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub ReadSuv()
        Try
            Dim format As New BTEFormatReader(suv, parameters)
            Dim qty As Integer = format.Count - 1

            suvMenu = New UIMenu("", "~r~SUVS", New Point(0, 0))
            suvMenu.SetMenuWidthOffset(11)
            suvMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PDMCarShopMod.shopui_title_pdm.png"))
            _menuPool.Add(suvMenu)
            suvMenu.AddInstructionalButton(btnConfirm)
            suvMenu.AddInstructionalButton(btnRotLeft)
            suvMenu.AddInstructionalButton(btnRotRight)
            suvMenu.AddInstructionalButton(btnOpenDoor)
            suvMenu.AddInstructionalButton(btnCloseDoor)
            suvMenu.AddInstructionalButton(btnChangeCam)
            For i As Integer = 0 To format.Count - 1
                Price = format(i)("price")
                Dim item As New UIMenuItem(format(i)("name"))
                suvMenu.AddItem(item)
                With item
                    .SetRightLabel("$" & Price.ToString("N"))
                    .Model = format(i)("model")
                    .Price = format(i)("price")
                    .Car = format(i)("name")
                End With
            Next
            suvMenu.RefreshIndex()
            mainMenu.BindMenuToItem(suvMenu, itemSuv)
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub ReadVan()
        Try
            Dim format As New BTEFormatReader(van, parameters)
            Dim qty As Integer = format.Count - 1

            vanMenu = New UIMenu("", "~r~VANS", New Point(0, 0))
            vanMenu.SetMenuWidthOffset(11)
            vanMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PDMCarShopMod.shopui_title_pdm.png"))
            _menuPool.Add(vanMenu)
            vanMenu.AddInstructionalButton(btnConfirm)
            vanMenu.AddInstructionalButton(btnRotLeft)
            vanMenu.AddInstructionalButton(btnRotRight)
            vanMenu.AddInstructionalButton(btnOpenDoor)
            vanMenu.AddInstructionalButton(btnCloseDoor)
            vanMenu.AddInstructionalButton(btnChangeCam)
            For i As Integer = 0 To format.Count - 1
                Price = format(i)("price")
                Dim item As New UIMenuItem(format(i)("name"))
                vanMenu.AddItem(item)
                With item
                    .SetRightLabel("$" & Price.ToString("N"))
                    .Model = format(i)("model")
                    .Price = format(i)("price")
                    .Car = format(i)("name")
                End With
            Next
            vanMenu.RefreshIndex()
            mainMenu.BindMenuToItem(vanMenu, itemVan)
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub ReadUtility()
        Try
            Dim format As New BTEFormatReader(utility, parameters)
            Dim qty As Integer = format.Count - 1

            utilityMenu = New UIMenu("", "~r~UTILITIES", New Point(0, 0))
            utilityMenu.SetMenuWidthOffset(11)
            utilityMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PDMCarShopMod.shopui_title_pdm.png"))
            _menuPool.Add(utilityMenu)
            utilityMenu.AddInstructionalButton(btnConfirm)
            utilityMenu.AddInstructionalButton(btnRotLeft)
            utilityMenu.AddInstructionalButton(btnRotRight)
            utilityMenu.AddInstructionalButton(btnOpenDoor)
            utilityMenu.AddInstructionalButton(btnCloseDoor)
            utilityMenu.AddInstructionalButton(btnChangeCam)
            For i As Integer = 0 To format.Count - 1
                Price = format(i)("price")
                Dim item As New UIMenuItem(format(i)("name"))
                utilityMenu.AddItem(item)
                With item
                    .SetRightLabel("$" & Price.ToString("N"))
                    .Model = format(i)("model")
                    .Price = format(i)("price")
                    .Car = format(i)("name")
                End With
            Next
            utilityMenu.RefreshIndex()
            mainMenu.BindMenuToItem(utilityMenu, itemUtility)
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub ReadArmoured()
        Try
            Dim format As New BTEFormatReader(armoured, parameters)
            Dim qty As Integer = format.Count - 1

            armouredMenu = New UIMenu("", "~r~ARMOURED", New Point(0, 0))
            armouredMenu.SetMenuWidthOffset(11)
            armouredMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PDMCarShopMod.shopui_title_pdm.png"))
            _menuPool.Add(armouredMenu)
            armouredMenu.AddInstructionalButton(btnConfirm)
            armouredMenu.AddInstructionalButton(btnRotLeft)
            armouredMenu.AddInstructionalButton(btnRotRight)
            armouredMenu.AddInstructionalButton(btnOpenDoor)
            armouredMenu.AddInstructionalButton(btnCloseDoor)
            armouredMenu.AddInstructionalButton(btnChangeCam)
            For i As Integer = 0 To format.Count - 1
                Price = format(i)("price")
                Dim item As New UIMenuItem(format(i)("name"))
                armouredMenu.AddItem(item)
                With item
                    .SetRightLabel("$" & Price.ToString("N"))
                    .Model = format(i)("model")
                    .Price = format(i)("price")
                    .Car = format(i)("name")
                End With
            Next
            armouredMenu.RefreshIndex()
            mainMenu.BindMenuToItem(armouredMenu, itemArmoured)
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub ReadLowrider()
        Try
            Dim format As New BTEFormatReader(lowrider, parameters)
            Dim qty As Integer = format.Count - 1

            lowriderMenu = New UIMenu("", "~r~LOWRIDERS", New Point(0, 0))
            lowriderMenu.SetMenuWidthOffset(11)
            lowriderMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PDMCarShopMod.shopui_title_pdm.png"))
            _menuPool.Add(lowriderMenu)
            lowriderMenu.AddInstructionalButton(btnConfirm)
            lowriderMenu.AddInstructionalButton(btnRotLeft)
            lowriderMenu.AddInstructionalButton(btnRotRight)
            lowriderMenu.AddInstructionalButton(btnOpenDoor)
            lowriderMenu.AddInstructionalButton(btnCloseDoor)
            lowriderMenu.AddInstructionalButton(btnChangeCam)
            For i As Integer = 0 To format.Count - 1
                Price = format(i)("price")
                Dim item As New UIMenuItem(format(i)("name"))
                lowriderMenu.AddItem(item)
                With item
                    .SetRightLabel("$" & Price.ToString("N"))
                    .Model = format(i)("model")
                    .Price = format(i)("price")
                    .Car = format(i)("name")
                End With
            Next
            lowriderMenu.RefreshIndex()
            mainMenu.BindMenuToItem(lowriderMenu, itemLowrider)
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub ReadColor()
        Try
            Dim format As New BTEFormatReader(colour, paracolors)
            Dim qty As Integer = format.Count - 1

            colorMenu = New UIMenu("", "~r~CUSTOM PRIMARY COLOR", New Point(0, 0))
            colorMenu.SetMenuWidthOffset(11)
            colorMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PDMCarShopMod.shopui_title_pdm.png"))
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
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub ReadColor2()
        Try
            Dim format As New BTEFormatReader(colour, paracolors)
            Dim qty As Integer = format.Count - 1

            colorMenu2 = New UIMenu("", "~r~CUSTOM SECONDARY COLOR", New Point(0, 0))
            colorMenu2.SetMenuWidthOffset(11)
            colorMenu2.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PDMCarShopMod.shopui_title_pdm.png"))
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
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub ReadColor3()
        Try
            Dim format As New BTEFormatReader(colour2, paracolors2)
            Dim qty As Integer = format.Count - 1

            colorMenu3 = New UIMenu("", "~r~PRIMARY COLOR", New Point(0, 0))
            colorMenu3.SetMenuWidthOffset(11)
            colorMenu3.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PDMCarShopMod.shopui_title_pdm.png"))
            _menuPool.Add(colorMenu3)
            colorMenu3.AddInstructionalButton(btnRotLeft)
            colorMenu3.AddInstructionalButton(btnRotRight)
            colorMenu3.AddInstructionalButton(btnOpenDoor)
            colorMenu3.AddInstructionalButton(btnCloseDoor)
            colorMenu3.AddInstructionalButton(btnChangeCam)
            For i As Integer = 0 To format.Count - 1
                Dim item As New UIMenuItem(format(i)("name"))
                colorMenu3.AddItem(item)
                With item
                    .Price = format(i)("code")
                    .Car = format(i)("type")
                End With
            Next
            colorMenu3.RefreshIndex()
            confirmMenu.BindMenuToItem(colorMenu3, itemColor3)
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub ReadColor4()
        Try
            Dim format As New BTEFormatReader(colour2, paracolors2)
            Dim qty As Integer = format.Count - 1

            colorMenu4 = New UIMenu("", "~r~SECONDARY COLOR", New Point(0, 0))
            colorMenu4.SetMenuWidthOffset(11)
            colorMenu4.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PDMCarShopMod.shopui_title_pdm.png"))
            _menuPool.Add(colorMenu4)
            colorMenu4.AddInstructionalButton(btnRotLeft)
            colorMenu4.AddInstructionalButton(btnRotRight)
            colorMenu4.AddInstructionalButton(btnOpenDoor)
            colorMenu4.AddInstructionalButton(btnCloseDoor)
            colorMenu4.AddInstructionalButton(btnChangeCam)
            For i As Integer = 0 To format.Count - 1
                Dim item As New UIMenuItem(format(i)("name"))
                colorMenu4.AddItem(item)
                With item
                    .Price = format(i)("code")
                    .Car = format(i)("type")
                End With
            Next
            colorMenu4.RefreshIndex()
            confirmMenu.BindMenuToItem(colorMenu4, itemColor4)
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub ReadColor5()
        Try
            Dim format As New BTEFormatReader(colour2, paracolors2)
            Dim qty As Integer = format.Count - 1

            colorMenu5 = New UIMenu("", "~r~PEARLESCENT COLOR", New Point(0, 0))
            colorMenu5.SetMenuWidthOffset(11)
            colorMenu5.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PDMCarShopMod.shopui_title_pdm.png"))
            _menuPool.Add(colorMenu5)
            colorMenu5.AddInstructionalButton(btnRotLeft)
            colorMenu5.AddInstructionalButton(btnRotRight)
            colorMenu5.AddInstructionalButton(btnOpenDoor)
            colorMenu5.AddInstructionalButton(btnCloseDoor)
            colorMenu5.AddInstructionalButton(btnChangeCam)
            For i As Integer = 0 To format.Count - 1
                Dim item As New UIMenuItem(format(i)("name"))
                colorMenu5.AddItem(item)
                With item
                    .Price = format(i)("code")
                    .Car = format(i)("type")
                End With
            Next
            colorMenu5.RefreshIndex()
            confirmMenu.BindMenuToItem(colorMenu5, itemColor5)
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub ReadRimColor()
        Try
            Dim format As New BTEFormatReader(colour2, paracolors2)
            Dim qty As Integer = format.Count - 1

            rimColorMenu = New UIMenu("", "~r~RIM COLOR", New Point(0, 0))
            rimColorMenu.SetMenuWidthOffset(11)
            rimColorMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PDMCarShopMod.shopui_title_pdm.png"))
            _menuPool.Add(rimColorMenu)
            rimColorMenu.AddInstructionalButton(btnRotLeft)
            rimColorMenu.AddInstructionalButton(btnRotRight)
            rimColorMenu.AddInstructionalButton(btnOpenDoor)
            rimColorMenu.AddInstructionalButton(btnCloseDoor)
            rimColorMenu.AddInstructionalButton(btnChangeCam)
            For i As Integer = 0 To format.Count - 1
                Dim item As New UIMenuItem(format(i)("name"))
                rimColorMenu.AddItem(item)
                With item
                    .Price = format(i)("code")
                    .Car = format(i)("type")
                End With
            Next
            rimColorMenu.RefreshIndex()
            confirmMenu.BindMenuToItem(rimColorMenu, itemRimColor)
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub ReadNeonColor()
        Try
            Dim format As New BTEFormatReader(colour, paracolors)
            Dim qty As Integer = format.Count - 1

            neonColorMenu = New UIMenu("", "~r~NEON COLOR", New Point(0, 0))
            neonColorMenu.SetMenuWidthOffset(11)
            neonColorMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PDMCarShopMod.shopui_title_pdm.png"))
            _menuPool.Add(neonColorMenu)
            neonColorMenu.AddInstructionalButton(btnRotLeft)
            neonColorMenu.AddInstructionalButton(btnRotRight)
            neonColorMenu.AddInstructionalButton(btnOpenDoor)
            neonColorMenu.AddInstructionalButton(btnCloseDoor)
            neonColorMenu.AddInstructionalButton(btnChangeCam)
            For i As Integer = 0 To format.Count - 1
                Dim item As New UIMenuItem(format(i)("name"))
                neonColorMenu.AddItem(item)
                With item
                    .Price = format(i)("red")
                    .Car = format(i)("green")
                    .Model = format(i)("blue")
                End With
            Next
            neonColorMenu.RefreshIndex()
            confirmMenu.BindMenuToItem(neonColorMenu, itemNeonColor)
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub ReadSmokeColor()
        Try
            Dim format As New BTEFormatReader(colour, paracolors)
            Dim qty As Integer = format.Count - 1

            smokeColorMenu = New UIMenu("", "~r~TYRE SMOKE COLOR", New Point(0, 0))
            smokeColorMenu.SetMenuWidthOffset(11)
            smokeColorMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PDMCarShopMod.shopui_title_pdm.png"))
            _menuPool.Add(smokeColorMenu)
            smokeColorMenu.AddInstructionalButton(btnRotLeft)
            smokeColorMenu.AddInstructionalButton(btnRotRight)
            smokeColorMenu.AddInstructionalButton(btnOpenDoor)
            smokeColorMenu.AddInstructionalButton(btnCloseDoor)
            smokeColorMenu.AddInstructionalButton(btnChangeCam)
            For i As Integer = 0 To format.Count - 1
                Dim item As New UIMenuItem(format(i)("name"))
                smokeColorMenu.AddItem(item)
                With item
                    .Price = format(i)("red")
                    .Car = format(i)("green")
                    .Model = format(i)("blue")
                End With
            Next
            smokeColorMenu.RefreshIndex()
            confirmMenu.BindMenuToItem(smokeColorMenu, itemSmokeColor)
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub ReadPlate()
        Try
            Dim format As New BTEFormatReader(plate, paraplates)
            Dim qty As Integer = format.Count - 1

            plateMenu = New UIMenu("", "~r~PLATE", New Point(0, 0))
            plateMenu.SetMenuWidthOffset(11)
            plateMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PDMCarShopMod.shopui_title_pdm.png"))
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
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub ReadConfirm()
        Try
            confirmMenu = New UIMenu("", "~r~PURCHASE ORDER", New Point(0, 0))
            confirmMenu.SetMenuWidthOffset(11)
            confirmMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PDMCarShopMod.shopui_title_pdm.png"))
            _menuPool.Add(confirmMenu)
            confirmMenu.AddInstructionalButton(btnRotLeft)
            confirmMenu.AddInstructionalButton(btnRotRight)
            confirmMenu.AddInstructionalButton(btnOpenDoor)
            confirmMenu.AddInstructionalButton(btnCloseDoor)
            confirmMenu.AddInstructionalButton(btnChangeCam)
            confirmMenu.AddItem(itemColor3)
            confirmMenu.AddItem(itemColor4)
            confirmMenu.AddItem(itemColor5)
            confirmMenu.AddItem(itemColor)
            confirmMenu.AddItem(itemColor2)
            confirmMenu.AddItem(itemRimColor)
            confirmMenu.AddItem(itemNeonColor)
            confirmMenu.AddItem(New UIMenuItem("Upgrade Vehicle", "Select Twice for Maximum upgrade vehicle visual & performance."))
            confirmMenu.AddItem(itemPlate)
            confirmMenu.AddItem(New UIMenuItem("Plate Number", "Customize license plate number."))
            confirmMenu.AddItem(New UIMenuItem("Test Drive"))
            confirmMenu.AddItem(New UIMenuItem("Confirm"))
            confirmMenu.RefreshIndex()
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Private Sub DisplayHelpTextThisFrame(ByVal [text] As String)
        Dim arguments As InputArgument() = New InputArgument() {"STRING"}
        Native.Function.Call(Hash._0x8509B634FBE7DA11, arguments)
        Dim argumentArray2 As InputArgument() = New InputArgument() {[text]}
        Native.Function.Call(Hash._0x6C188BE134E074AA, argumentArray2)
        Dim argumentArray3 As InputArgument() = New InputArgument() {0, 0, 1, -1}
        Native.Function.Call(Hash._0x238FFE5C7B0498A6, argumentArray3)
    End Sub

    Private Sub SetBlipName(ByVal BlipString As String, ByVal BlipName As Blip)
        Dim arguments As InputArgument() = New InputArgument() {"STRING"}
        Native.Function.Call(Hash._0xF9113A30DE5C6670, arguments)
        Native.Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, BlipString)
        Native.Function.Call(Hash._0xBC38B49BCB83BC9B, BlipName)
    End Sub

    Public Function getNumVehMod(_vehicle As Vehicle, _modType As Integer) As Integer
        If Native.Function.Call(Of Integer)(Hash.GET_NUM_VEHICLE_MODS, _vehicle, _modType) > 1 Then
            Return Native.Function.Call(Of Integer)(Hash.GET_NUM_VEHICLE_MODS, _vehicle, _modType) - 1
        End If
        Return 0
    End Function

    Public Sub ConfirmItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        Try
            If selectedItem.Text = "Confirm" Then
                If PlayerCash > vehiclePrice Then
                    Game.FadeScreenOut(500)
                    Script.Wait(&H3E8)
                    player.Money = (PlayerCash - vehiclePrice)
                    confirmMenu.Visible = False
                    World.DestroyAllCameras()
                    World.RenderingCamera = Nothing
                    vehPreview.IsDriveable = True
                    Native.Function.Call(Hash.SET_VEHICLE_DOORS_SHUT, vehPreview, False)
                    Native.Function.Call(Hash.TASK_WARP_PED_INTO_VEHICLE, playerPed, vehPreview, -1)
                    vehPreview.MarkAsNoLongerNeeded()
                    vehPreview = Nothing
                    hideHud = False
                    Script.Wait(500)
                    Game.FadeScreenIn(500)
                    Native.Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "PROPERTY_PURCHASE", "HUD_AWARDS", False)
                    _scaleform.CallFunction("SHOW_MISSION_PASSED_MESSAGE", String.Format("Vehicle Purchased" & vbLf & "~w~{0}", selectedVehicle), "", 100, True, 0, True)
                    _displayTimer.Start()
                    selectedVehicle = Nothing
                Else
                    UI.Notify("You have insufficient funds to purchase this vehicle.", True)
                End If
            ElseIf selectedItem.Text = "Test Drive" Then
                Game.FadeScreenOut(500)
                Script.Wait(&H3E8)
                Native.Function.Call(Hash.TASK_WARP_PED_INTO_VEHICLE, playerPed, vehPreview, -1)
                confirmMenu.Visible = False
                World.DestroyAllCameras()
                World.RenderingCamera = Nothing
                vehPreview.IsDriveable = True
                Native.Function.Call(Hash.SET_VEHICLE_DOORS_SHUT, vehPreview, False)
                DisplayHelpTextThisFrame("To quit Test Drive, Leave this vehicle.")
                testDrive = testDrive + 1
                hideHud = False
                vehPreview.Position = New Vector3(-56.79958F, -1110.868F, 26.43581F)
                Script.Wait(500)
                Game.FadeScreenIn(500)
            End If
            If selectedItem.Text = "Plate Number" Then
                Dim NumPlateText As String = Game.GetUserInput(vehPreview.NumberPlate, 9)
                If NumPlateText <> "" Then
                    vehPreview.NumberPlate = NumPlateText
                    If ChangeCamera = 0 Then
                        World.DestroyAllCameras()
                        World.RenderingCamera = Nothing
                    ElseIf ChangeCamera = 1 Then
                        World.RenderingCamera = World.CreateCamera(cameraPosition, New Vector3(Game.Player.Character.Rotation.X, Game.Player.Character.Rotation.Y, 253.0F), 10.0F)
                    End If
                End If
            End If
            If selectedItem.Text = "Upgrade Vehicle" Then
                spoiler = getNumVehMod(vehPreview, 0)
                fbumper = getNumVehMod(vehPreview, 1)
                rbumper = getNumVehMod(vehPreview, 2)
                sskirt = getNumVehMod(vehPreview, 3)
                frame = getNumVehMod(vehPreview, 5)
                grille = getNumVehMod(vehPreview, 6)
                hood = getNumVehMod(vehPreview, 7)
                fender = getNumVehMod(vehPreview, 8)
                rfender = getNumVehMod(vehPreview, 9)
                roof = getNumVehMod(vehPreview, 10)
                exhaust = getNumVehMod(vehPreview, 4)
                suspension = getNumVehMod(vehPreview, 15)
                engine = getNumVehMod(vehPreview, 11)
                brakes = getNumVehMod(vehPreview, 12)
                transms = getNumVehMod(vehPreview, 13)
                armor = getNumVehMod(vehPreview, 16)
                fWheels = getNumVehMod(vehPreview, 23)
                bWheels = getNumVehMod(vehPreview, 24)

                Native.Function.Call(Hash.SET_VEHICLE_MOD_KIT, vehPreview.Handle, 0)
                Dim motorList As Model() = {"akuma", "bagger", "bati", "bati2", "carbonrs", "daemon", "double", "faggio2", "hexer", "nemesis", "pcj", "ruffian", "sanchez", "sanchez2", "vader", "thrust", "sovereign", "innovation", "hakuchou", "enduro", "lectro", "vindicator"}
                If Not motorList.Contains(vehPreview.Model) Then
                    vehPreview.WheelType = VehicleWheelType.HighEnd
                End If
                vehPreview.WindowTint = VehicleWindowTint.Limo
                vehPreview.SetMod(VehicleMod.Spoilers, spoiler, True)
                vehPreview.SetMod(VehicleMod.FrontBumper, fbumper, True)
                vehPreview.SetMod(VehicleMod.RearBumper, rbumper, True)
                vehPreview.SetMod(VehicleMod.SideSkirt, sskirt, True)
                vehPreview.SetMod(VehicleMod.Frame, frame, True)
                vehPreview.SetMod(VehicleMod.Grille, grille, True)
                vehPreview.SetMod(VehicleMod.Hood, hood, True)
                vehPreview.SetMod(VehicleMod.Fender, fender, True)
                vehPreview.SetMod(VehicleMod.RightFender, rfender, True)
                vehPreview.SetMod(VehicleMod.Roof, roof, True)
                vehPreview.SetMod(VehicleMod.Exhaust, exhaust, True)
                vehPreview.SetMod(VehicleMod.FrontWheels, fWheels, True)
                vehPreview.SetMod(VehicleMod.BackWheels, bWheels, True)
                vehPreview.SetMod(VehicleMod.Suspension, suspension, True)
                vehPreview.SetMod(VehicleMod.Engine, engine, True)
                vehPreview.SetMod(VehicleMod.Brakes, brakes, True)
                vehPreview.SetMod(VehicleMod.Transmission, transms, True)
                vehPreview.SetMod(VehicleMod.Armor, armor, True)
                vehPreview.SetMod(VehicleToggleMod.XenonHeadlights, 22, True)
                vehPreview.ToggleMod(VehicleToggleMod.XenonHeadlights, True)
                vehPreview.ToggleMod(VehicleToggleMod.Turbo, True)
                vehPreview.SetMod(VehicleToggleMod.Turbo, 18, True)
            End If
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub ItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        Try
            If selectedItem.Text = "Enable Mod" Then
                ModEnable = True
                DisplayHelpTextThisFrame("~r~Premium Deluxe Motorsport ~w~Mod Enabled.")
                SpawnSimeon()
                modMenu.Visible = False
                sender.MenuItems(0).SetRightBadge(UIMenuItem.BadgeStyle.Tick)
                sender.MenuItems(1).SetRightBadge(UIMenuItem.BadgeStyle.None)
            ElseIf selectedItem.Text = "Disable Mod" Then
                ModEnable = False
                DisplayHelpTextThisFrame("~r~Premium Deluxe Motorsport ~w~Mod Disabled.")
                simeonBlip.Remove()
                modMenu.Visible = False
                sender.MenuItems(1).SetRightBadge(UIMenuItem.BadgeStyle.Tick)
                sender.MenuItems(0).SetRightBadge(UIMenuItem.BadgeStyle.None)
            ElseIf selectedItem.Text = "Enable Clear Color" Then
                My.Settings.removeColor = True
                My.Settings.Save()
                DisplayHelpTextThisFrame("~r~Clear Color ~w~Enabled.")
                sender.MenuItems(2).SetRightBadge(UIMenuItem.BadgeStyle.Tick)
                sender.MenuItems(3).SetRightBadge(UIMenuItem.BadgeStyle.None)
            ElseIf selectedItem.Text = "Disable Clear Color" Then
                My.Settings.removeColor = False
                My.Settings.Save()
                DisplayHelpTextThisFrame("~r~Clear Color ~w~Disable.")
                sender.MenuItems(3).SetRightBadge(UIMenuItem.BadgeStyle.Tick)
                sender.MenuItems(2).SetRightBadge(UIMenuItem.BadgeStyle.None)
            ElseIf selectedItem.Text = "Showroom" Then
                My.Settings.showRoom = True
                My.Settings.Save()
                DisplayHelpTextThisFrame("~r~Showroom ~w~Selected.")
                sender.MenuItems(4).SetRightBadge(UIMenuItem.BadgeStyle.Tick)
                sender.MenuItems(5).SetRightBadge(UIMenuItem.BadgeStyle.None)
            ElseIf selectedItem.Text = "Car Park" Then
                My.Settings.showRoom = False
                My.Settings.Save()
                DisplayHelpTextThisFrame("~r~Car Park ~w~Selected.")
                sender.MenuItems(5).SetRightBadge(UIMenuItem.BadgeStyle.Tick)
                sender.MenuItems(4).SetRightBadge(UIMenuItem.BadgeStyle.None)
            ElseIf selectedItem.Text = "Refresh Settings" Then
                My.Settings.keyModEnable = [Enum].Parse(GetType(Keys), ReadIniValue(".\Scripts\PDMCarShop\config.ini", "OPTIONS", "ModEnableKey"), False)
                My.Settings.showRoom = ReadIniValue(".\Scripts\PDMCarShop\config.ini", "OPTIONS", "Showroom")
                My.Settings.removeColor = ReadIniValue(".\Scripts\PDMCarShop\config.ini", "OPTIONS", "ClearColor")
                My.Settings.Save()
                modMenu.Visible = False
                DisplayHelpTextThisFrame("Keys has been Saved.")
            ElseIf selectedItem.Text = "About" Then
                modMenu.Visible = False
                UI.Notify("Premium Deluxe Motorsport Car Shop Mod v" & My.Application.Info.Version.ToString, True)
                UI.Notify("Release Date: 26 Oct 2015", True)
                UI.Notify("Mod Author: I'm Not MentaL", True)
                UI.Notify("Special Thanks: Rockstar Games, Alexander Blade, Crosire, Guad, EnergyStyle, LetsPlayOrDy,", True)
                UI.Notify("Calm, LCBuffalo, Gang1111, Matt_STS, frodzet, leftas, marhex & CamxxCore", True)
            End If
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub ColorItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        Try
            If My.Settings.removeColor = True Then
                Native.Function.Call(Hash.SET_VEHICLE_COLOURS, vehPreview, 0, 0)
            End If
            vehPreview.CustomPrimaryColor = Color.FromArgb(255, selectedItem.Price, selectedItem.Car, selectedItem.Model)
            colorMenu.GoBack()
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub ColorItemSelectHandler2(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        Try
            If My.Settings.removeColor = True Then
                Native.Function.Call(Hash.SET_VEHICLE_COLOURS, vehPreview, 0, 0)
            End If
            vehPreview.CustomSecondaryColor = Color.FromArgb(255, selectedItem.Price, selectedItem.Car, selectedItem.Model)
            colorMenu2.GoBack()
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub ColorItemSelectHandler3(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        Try
            If My.Settings.removeColor = True Then
                Native.Function.Call(Hash.CLEAR_VEHICLE_CUSTOM_PRIMARY_COLOUR, vehPreview)
                Native.Function.Call(Hash.CLEAR_VEHICLE_CUSTOM_SECONDARY_COLOUR, vehPreview)
            End If
            If selectedItem.Car = 1 Then
                vehPreview.PrimaryColor = selectedItem.Price
                vehPreview.PearlescentColor = selectedItem.Price + 1
            Else
                vehPreview.PrimaryColor = selectedItem.Price
            End If
            colorMenu3.GoBack()
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub ColorItemSelectHandler4(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        Try
            If My.Settings.removeColor = True Then
                Native.Function.Call(Hash.CLEAR_VEHICLE_CUSTOM_PRIMARY_COLOUR, vehPreview)
                Native.Function.Call(Hash.CLEAR_VEHICLE_CUSTOM_SECONDARY_COLOUR, vehPreview)
            End If
            If selectedItem.Car = 1 Then
                vehPreview.SecondaryColor = selectedItem.Price
                vehPreview.PearlescentColor = selectedItem.Price + 1
            Else
                vehPreview.SecondaryColor = selectedItem.Price
            End If
            colorMenu4.GoBack()
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub ColorItemSelectHandler5(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        Try
            vehPreview.PearlescentColor = selectedItem.Price
            'Native.Function.Call(Hash.SET_VEHICLE_MOD_COLOR_1, vehPreview, selectedItem.Car, selectedItem.Price)
            colorMenu5.GoBack()
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub RimColorItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        Try
            vehPreview.RimColor = selectedItem.Price
            rimColorMenu.GoBack()
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub NeonColorItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        Try
            vehPreview.SetNeonLightsOn(VehicleNeonLight.Back, True)
            vehPreview.SetNeonLightsOn(VehicleNeonLight.Front, True)
            vehPreview.SetNeonLightsOn(VehicleNeonLight.Left, True)
            vehPreview.SetNeonLightsOn(VehicleNeonLight.Right, True)
            vehPreview.NeonLightsColor = Color.FromArgb(255, selectedItem.Price, selectedItem.Car, selectedItem.Model)
            neonColorMenu.GoBack()
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub SmokeColorItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        Try
            Native.Function.Call(Hash.SET_VEHICLE_TYRE_SMOKE_COLOR, vehPreview, sender.MenuItems(index).Price, sender.MenuItems(index).Car, sender.MenuItems(index).Model)
            'vehPreview.TireSmokeColor = Color.FromArgb(255, selectedItem.Price, selectedItem.Car, selectedItem.Model)
            smokeColorMenu.GoBack()
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Private Sub PlateItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        Try
            Native.Function.Call(Hash.SET_VEHICLE_NUMBER_PLATE_TEXT_INDEX, vehPreview, selectedItem.Price)
            plateMenu.GoBack()
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Private Sub SpoilerItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        Try
            vehPreview.SetMod(VehicleMod.Spoilers, selectedItem.Price, True)
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub CategoryItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        Try
            selectedVehicle = selectedItem.Car
            If vehPreview = Nothing Then
                vehPreview = World.CreateVehicle(selectedItem.Model, vehPreviewPosition, 6.122209F)
            Else
                vehPreview.Delete()
                vehPreview = World.CreateVehicle(selectedItem.Model, vehPreviewPosition, 6.122209F)
            End If
            vehPreview.Rotation = New Vector3(0, 0, curRadius)
            vehPreview.IsDriveable = False
            vehPreview.DirtLevel = 0F
            vehiclePrice = selectedItem.Price
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub PlateItemChangeHandler(sender As UIMenu, index As Integer)
        Try
            Native.Function.Call(Hash.SET_VEHICLE_NUMBER_PLATE_TEXT_INDEX, vehPreview, sender.MenuItems(index).Price)
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub ColorItemChangeHandler(sender As UIMenu, index As Integer)
        Try
            If My.Settings.removeColor = True Then
                Native.Function.Call(Hash.SET_VEHICLE_COLOURS, vehPreview, 0, 0)
            End If
            vehPreview.CustomPrimaryColor = Color.FromArgb(255, sender.MenuItems(index).Price, sender.MenuItems(index).Car, sender.MenuItems(index).Model)
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub ColorItemChangeHandler2(sender As UIMenu, index As Integer)
        Try
            If My.Settings.removeColor = True Then
                Native.Function.Call(Hash.SET_VEHICLE_COLOURS, vehPreview, 0, 0)
            End If
            vehPreview.CustomSecondaryColor = Color.FromArgb(255, sender.MenuItems(index).Price, sender.MenuItems(index).Car, sender.MenuItems(index).Model)
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub ColorItemChangeHandler3(sender As UIMenu, index As Integer)
        Try
            If My.Settings.removeColor = True Then
                Native.Function.Call(Hash.CLEAR_VEHICLE_CUSTOM_PRIMARY_COLOUR, vehPreview)
                Native.Function.Call(Hash.CLEAR_VEHICLE_CUSTOM_SECONDARY_COLOUR, vehPreview)
            End If
            vehPreview.PrimaryColor = sender.MenuItems(index).Price
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub ColorItemChangeHandler4(sender As UIMenu, index As Integer)
        Try
            If My.Settings.removeColor = True Then
                Native.Function.Call(Hash.CLEAR_VEHICLE_CUSTOM_PRIMARY_COLOUR, vehPreview)
                Native.Function.Call(Hash.CLEAR_VEHICLE_CUSTOM_SECONDARY_COLOUR, vehPreview)
            End If
            vehPreview.SecondaryColor = sender.MenuItems(index).Price
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub ColorItemChangeHandler5(sender As UIMenu, index As Integer)
        Try
            vehPreview.PearlescentColor = sender.MenuItems(index).Price
            'Native.Function.Call(Hash.SET_VEHICLE_MOD_COLOR_1, vehPreview, sender.MenuItems(index).Car, sender.MenuItems(index).Price)
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub RimColorItemChangeHandler(sender As UIMenu, index As Integer)
        Try
            vehPreview.RimColor = sender.MenuItems(index).Price
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub NeonColorItemChangeHandler(sender As UIMenu, index As Integer)
        Try
            vehPreview.SetNeonLightsOn(VehicleNeonLight.Back, True)
            vehPreview.SetNeonLightsOn(VehicleNeonLight.Front, True)
            vehPreview.SetNeonLightsOn(VehicleNeonLight.Left, True)
            vehPreview.SetNeonLightsOn(VehicleNeonLight.Right, True)
            vehPreview.NeonLightsColor = Color.FromArgb(255, sender.MenuItems(index).Price, sender.MenuItems(index).Car, sender.MenuItems(index).Model)
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub SmokeColorItemChangeHandler(sender As UIMenu, index As Integer)
        Try
            Native.Function.Call(Hash.SET_VEHICLE_TYRE_SMOKE_COLOR, vehPreview, sender.MenuItems(index).Price, sender.MenuItems(index).Car, sender.MenuItems(index).Model)
            'vehPreview.TireSmokeColor = Color.FromArgb(255, sender.MenuItems(index).Price, sender.MenuItems(index).Car, sender.MenuItems(index).Model)
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub MenuCloseHandler(sender As UIMenu)
        Try
            If selectedVehicle IsNot Nothing Then
                selectedVehicle = Nothing
                vehPreview.Delete()
            End If
            World.DestroyAllCameras()
            World.RenderingCamera = Nothing
            hideHud = False
            armouredMenu.RefreshIndex()
            classicMenu.RefreshIndex()
            colorMenu.RefreshIndex()
            colorMenu2.RefreshIndex()
            colorMenu3.RefreshIndex()
            colorMenu4.RefreshIndex()
            colorMenu5.RefreshIndex()
            rimColorMenu.RefreshIndex()
            neonColorMenu.RefreshIndex()
            smokeColorMenu.RefreshIndex()
            compactMenu.RefreshIndex()
            confirmMenu.RefreshIndex()
            coupeMenu.RefreshIndex()
            exoticMenu.RefreshIndex()
            mainMenu.RefreshIndex()
            modMenu.RefreshIndex()
            motorMenu.RefreshIndex()
            muscleMenu.RefreshIndex()
            offroadMenu.RefreshIndex()
            plateMenu.RefreshIndex()
            sedanMenu.RefreshIndex()
            sportMenu.RefreshIndex()
            suvMenu.RefreshIndex()
            utilityMenu.RefreshIndex()
            vanMenu.RefreshIndex()
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub ConfirmCloseHandler(sender As UIMenu)
        Try
            selectedVehicle = Nothing
            vehPreview.Delete()
            mainMenu.Visible = True
            armouredMenu.RefreshIndex()
            classicMenu.RefreshIndex()
            colorMenu.RefreshIndex()
            colorMenu2.RefreshIndex()
            colorMenu3.RefreshIndex()
            colorMenu4.RefreshIndex()
            colorMenu5.RefreshIndex()
            rimColorMenu.RefreshIndex()
            neonColorMenu.RefreshIndex()
            smokeColorMenu.RefreshIndex()
            compactMenu.RefreshIndex()
            confirmMenu.RefreshIndex()
            coupeMenu.RefreshIndex()
            exoticMenu.RefreshIndex()
            mainMenu.RefreshIndex()
            modMenu.RefreshIndex()
            motorMenu.RefreshIndex()
            muscleMenu.RefreshIndex()
            offroadMenu.RefreshIndex()
            plateMenu.RefreshIndex()
            sedanMenu.RefreshIndex()
            sportMenu.RefreshIndex()
            suvMenu.RefreshIndex()
            utilityMenu.RefreshIndex()
            vanMenu.RefreshIndex()
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub OnTick(o As Object, e As EventArgs)
        Try
            _menuPool.ProcessMenus()

            If ModEnable = True Then
                simeonDist = GTA.World.GetDistance(Game.Player.Character.Position, simeon)
                testdriveDist = GTA.World.GetDistance(Game.Player.Character.Position, testDriveVector)
                player = Game.Player
                playerPed = Game.Player.Character
                PlayerCash = player.Money

                If Not playerPed.IsInVehicle AndAlso Not playerPed.IsDead AndAlso simeonDist < 3.0F AndAlso player.WantedLevel = 0 Then
                    DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to enter Premium Deluxe Motorsport.")
                ElseIf Not playerPed.IsInVehicle AndAlso Not playerPed.IsDead AndAlso simeonDist < 3.0F AndAlso player.WantedLevel >= 1 Then
                    Native.Function.Call(Hash.DISPLAY_HELP_TEXT_THIS_FRAME, New InputArgument() {"LOSE_WANTED", 0})
                End If
            End If

            If ModEnable = True And Not vehPreview = Nothing Then
                spoiler = getNumVehMod(vehPreview, 0)
                fbumper = getNumVehMod(vehPreview, 1)
                rbumper = getNumVehMod(vehPreview, 2)
                sskirt = getNumVehMod(vehPreview, 3)
                frame = getNumVehMod(vehPreview, 5)
                grille = getNumVehMod(vehPreview, 6)
                hood = getNumVehMod(vehPreview, 7)
                fender = getNumVehMod(vehPreview, 8)
                rfender = getNumVehMod(vehPreview, 9)
                roof = getNumVehMod(vehPreview, 10)
                exhaust = getNumVehMod(vehPreview, 4)
                suspension = getNumVehMod(vehPreview, 15)
                engine = getNumVehMod(vehPreview, 11)
                brakes = getNumVehMod(vehPreview, 12)
                transms = getNumVehMod(vehPreview, 13)
                armor = getNumVehMod(vehPreview, 16)
                fWheels = getNumVehMod(vehPreview, 23)
                bWheels = getNumVehMod(vehPreview, 24)
            End If

            If ModEnable = True AndAlso testDrive = 3 AndAlso Not playerPed.IsInVehicle Then
                Game.FadeScreenOut(500)
                Script.Wait(&H3E8)
                Dim penalty As Double = vehiclePrice / 99
                If vehPreview.HasBeenDamagedBy(playerPed) Then
                    player.Money = (PlayerCash - (vehiclePrice / 99))
                    UI.Notify("$" & Math.Round(penalty) & " has been charge for fixing the vehicle.")
                End If
                confirmMenu.Visible = True
                World.RenderingCamera = World.CreateCamera(cameraPosition, New Vector3(Game.Player.Character.Rotation.X, Game.Player.Character.Rotation.Y, 253.0F), 10.0F)
                vehPreview.IsDriveable = False
                Game.Player.Character.Position = playerPosition
                vehPreview.Position = vehPreviewPosition
                Native.Function.Call(Hash.SET_VEHICLE_DOORS_SHUT, vehPreview, False)
                Native.Function.Call(Hash.SET_VEHICLE_FIXED, vehPreview)
                testDrive = 1
                hideHud = True
                Script.Wait(500)
                Game.FadeScreenIn(500)
            ElseIf ModEnable = True AndAlso testDrive = 3 AndAlso testdriveDist > 450.0F Then
                Game.FadeScreenOut(500)
                Script.Wait(&H3E8)
                Dim penalty As Double = vehiclePrice / 99
                If vehPreview.HasBeenDamagedBy(playerPed) Then
                    player.Money = (PlayerCash - (vehiclePrice / 99))
                    UI.Notify("$" & Math.Round(penalty) & " has been charge for fixing the vehicle.")
                End If
                confirmMenu.Visible = True
                World.RenderingCamera = World.CreateCamera(cameraPosition, New Vector3(Game.Player.Character.Rotation.X, Game.Player.Character.Rotation.Y, 253.0F), 10.0F)
                vehPreview.IsDriveable = False
                Game.Player.Character.Position = playerPosition
                vehPreview.Position = vehPreviewPosition
                Native.Function.Call(Hash.SET_VEHICLE_DOORS_SHUT, vehPreview, False)
                Native.Function.Call(Hash.SET_VEHICLE_FIXED, vehPreview)
                testDrive = 1
                hideHud = True
                Script.Wait(500)
                Game.FadeScreenIn(500)
            ElseIf ModEnable = True AndAlso testDrive = 2 AndAlso playerPed.IsInVehicle Then
                testDrive = testDrive + 1
            End If

            If hideHud Then
                Native.Function.Call(Hash.HIDE_HUD_AND_RADAR_THIS_FRAME)
            End If

            If My.Settings.showRoom = False Then
                'Outside
                vehPreviewPosition = New Vector3(-56.79958F, -1110.868F, 26.43581F)
                cameraPosition = New Vector3(-78.79827F, -1103.386F, 26.8126F)
                playerPosition = New Vector3(-43.79905F, -1116.247F, 25.43394F)
            Else
                'Inside
                Native.Function.Call(Hash.CLEAR_AREA_OF_VEHICLES, -44.142F, -1098.996F, 26.422F, 50, True, False, False, False, False)
                vehPreviewPosition = New Vector3(-44.142F, -1098.996F, 26.422F)
                cameraPosition = New Vector3(-59.76299F, -1093.913F, 26.622F)
                playerPosition = New Vector3(-54.16683F, -1088.698F, 25.42233F)
            End If

            If _displayTimer.Enabled Then
                _scaleform.Render2D()

                If Game.GameTime > _displayTimer.Waiter Then
                    _scaleform.CallFunction("TRANSITION_OUT")
                    _displayTimer.Enabled = False
                    _fadeTimer.Start()
                End If
            End If

            If _fadeTimer.Enabled Then
                _scaleform.Render2D()

                If Game.GameTime > _fadeTimer.Waiter Then
                    _fadeTimer.Enabled = False
                End If
            End If
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub OnKeyDown(o As Object, e As KeyEventArgs)
        Try
            If Game.IsControlJustPressed(0, GTA.Control.Talk) AndAlso ModEnable = True AndAlso simeonDist < 3.0F AndAlso Not playerPed.IsInVehicle AndAlso player.WantedLevel = 0 Then
                'mainMenu.Visible = Not mainMenu.Visible
                Game.FadeScreenOut(500)
                Script.Wait(&H3E8)
                mainMenu.Visible = True
                ChangeCamera = 1
                World.RenderingCamera = World.CreateCamera(cameraPosition, New Vector3(Game.Player.Character.Rotation.X, Game.Player.Character.Rotation.Y, 253.0F), 10.0F)
                Game.Player.Character.Position = playerPosition
                hideHud = True
                Script.Wait(500)
                Game.FadeScreenIn(500)
            End If

            If Game.IsControlJustPressed(0, GTA.Control.Jump) AndAlso ModEnable = True AndAlso simeonDist < 40.0F AndAlso Not playerPed.IsInVehicle AndAlso Not selectedVehicle = Nothing Then
                If armouredMenu.Visible = True Or classicMenu.Visible = True Or compactMenu.Visible = True Or coupeMenu.Visible = True Or exoticMenu.Visible = True Or
                    motorMenu.Visible = True Or muscleMenu.Visible = True Or offroadMenu.Visible = True Or sedanMenu.Visible = True Or sportMenu.Visible = True Or
                    suvMenu.Visible = True Or utilityMenu.Visible = True Or vanMenu.Visible = True Or lowriderMenu.Visible = True Then
                    confirmMenu.Visible = True
                    armouredMenu.Visible = False
                    classicMenu.Visible = False
                    compactMenu.Visible = False
                    coupeMenu.Visible = False
                    exoticMenu.Visible = False
                    motorMenu.Visible = False
                    muscleMenu.Visible = False
                    offroadMenu.Visible = False
                    sedanMenu.Visible = False
                    sportMenu.Visible = False
                    suvMenu.Visible = False
                    utilityMenu.Visible = False
                    vanMenu.Visible = False
                    lowriderMenu.Visible = False
                End If
            End If

            If e.KeyCode = My.Settings.keyModEnable Then
                modMenu.Visible = Not mainMenu.Visible
                World.DestroyAllCameras()
                World.RenderingCamera = Nothing
            ElseIf e.KeyCode = Keys.NumPad1 Then
                Dim spoiler As Integer = getNumVehMod(vehPreview, 0)
                UI.Notify(spoiler)
            End If

            If Game.IsControlPressed(0, GTA.Control.ParachuteBrakeLeft) AndAlso ModEnable = True AndAlso simeonDist < 40.0F AndAlso Not playerPed.IsInVehicle Then
                curRadius = curRadius + 2
                vehPreview.Rotation = New Vector3(0, 0, curRadius)
            ElseIf Game.IsControlPressed(0, GTA.Control.ParachuteBrakeRight) AndAlso ModEnable = True AndAlso simeonDist < 40.0F AndAlso Not playerPed.IsInVehicle Then
                curRadius = curRadius - 2
                vehPreview.Rotation = New Vector3(0, 0, curRadius)
            ElseIf Game.IsControlJustPressed(0, GTA.Control.SelectWeaponUnarmed) AndAlso ModEnable = True AndAlso simeonDist < 40.0F AndAlso Not playerPed.IsInVehicle Then
                vehPreview.OpenDoor(VehicleDoor.BackLeftDoor, False, False)
                vehPreview.OpenDoor(VehicleDoor.BackRightDoor, False, False)
                vehPreview.OpenDoor(VehicleDoor.FrontLeftDoor, False, False)
                vehPreview.OpenDoor(VehicleDoor.FrontRightDoor, False, False)
                vehPreview.OpenDoor(VehicleDoor.Hood, False, False)
                vehPreview.OpenDoor(VehicleDoor.Trunk, False, False)
                vehPreview.OpenDoor(VehicleDoor.Trunk2, False, False)
            ElseIf Game.IsControlJustPressed(0, GTA.Control.SelectWeaponMelee) AndAlso ModEnable = True AndAlso simeonDist < 40.0F AndAlso Not playerPed.IsInVehicle Then
                Native.Function.Call(Hash.SET_VEHICLE_DOORS_SHUT, vehPreview, False)
            ElseIf Game.IsControlJustPressed(0, GTA.Control.VehicleRoof) AndAlso ModEnable = True AndAlso simeonDist < 40.0F Then
                If vehPreview.RoofState = VehicleRoofState.Closed Then
                    Native.Function.Call(Hash.LOWER_CONVERTIBLE_ROOF, vehPreview, False)
                Else
                    Native.Function.Call(Hash.RAISE_CONVERTIBLE_ROOF, vehPreview, False)
                End If
            End If

            If Game.IsControlJustPressed(0, GTA.Control.NextCamera) AndAlso ModEnable = True AndAlso simeonDist < 40.0F AndAlso ChangeCamera = 0 AndAlso Not playerPed.IsInVehicle Then
                World.DestroyAllCameras()
                World.RenderingCamera = Nothing
                ChangeCamera = (ChangeCamera + 1)
            ElseIf Game.IsControlJustPressed(0, GTA.Control.NextCamera) AndAlso ModEnable = True AndAlso simeonDist < 40.0F AndAlso ChangeCamera = 1 AndAlso Not playerPed.IsInVehicle Then
                World.RenderingCamera = World.CreateCamera(cameraPosition, New Vector3(Game.Player.Character.Rotation.X, Game.Player.Character.Rotation.Y, 253.0F), 10.0F)
                ChangeCamera = (ChangeCamera - 1)
            End If
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Protected Overrides Sub Dispose(A_0 As Boolean)
        If (A_0) Then
            Try
                simeonBlip.Remove()
            Catch ex As Exception
                logger.Log(ex.Message & ex.StackTrace)
            End Try
        End If
    End Sub
End Class