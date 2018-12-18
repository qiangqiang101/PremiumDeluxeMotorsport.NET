Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports GTA
Imports GTA.Native
Imports GTA.Math
Imports GTA.Game
Imports INMNativeUI
Imports System.Reflection
Imports PDMCD4.Resources
Imports PDMCD4.PDM
Imports System.Text

Public Class PDMMenu
    Inherits Script

    Public Shared MainMenu, ConfirmMenu, CustomiseMenu, VehicleMenu As UIMenu
    Public Shared itemCat As UIMenuItem
    Public Shared PriColorMenu, ClassicColorMenu, MetallicColorMenu, MetalColorMenu, MatteColorMenu, ChromeColorMenu, PeaColorMenu, CPriColorMenu As UIMenu
    Public Shared ColorMenu, SecColorMenu, ClassicColorMenu2, MetallicColorMenu2, MetalColorMenu2, MatteColorMenu2, ChromeColorMenu2, CSecColorMenu, PlateMenu As UIMenu

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

    Public Shared BtnRotLeft As New InstructionalButton(GTA.Control.ParachuteBrakeLeft, GetLangEntry("BTN_DOOR"))
    Public Shared BtnRotRight As New InstructionalButton(GTA.Control.VehicleRoof, GetLangEntry("BTN_ROOF"))
    Public Shared BtnCamera As New InstructionalButton(GTA.Control.VehiclePushbikeSprint, GetLangEntry("BTN_CHANGE_CAM"))
    Public Shared BtnZoom As New InstructionalButton(GTA.Control.VehicleSubAscend, GetLangEntry("BTN_ZOOM"))
    'Public Shared BtnStat As New InstructionalButton(GTA.Control.MultiplayerInfo, GetLangEntry("BTN_SHOW_STAT"))

    Public Shared Parameters As String() = {"[name]", "[price]", "[model]", "[gxt]", "[make]"}

    Public Shared _menuPool As MenuPool
    Public Shared Rectangle = New UIResRectangle()

    Public Shared config As ScriptSettings = ScriptSettings.Load("scripts\PremiumDeluxeMotorsport\config.ini")
    Public Shared hiddenSave As ScriptSettings = ScriptSettings.Load("scripts\PremiumDeluxeMotorsport\database.ini")
    Public Shared optAddOnCars As Integer = 0
    Public Shared optRemoveColor As Integer = 1
    Public Shared optRemoveImg As Integer = 0
    Public Shared optRandomColor As Integer = 1
    Public Shared optFade As Integer = 1
    Public Shared optLastVehHash As Integer = 0
    Public Shared optLastVehName As String = Nothing
    Public Shared optLastVehMake As String = Nothing
    Public Shared optLogging As Integer = 1

    Public Shared Sub LoadSettings()
        optRemoveColor = config.GetValue(Of Integer)("SETTINGS", "REMOVECOLOR", 1)
        optRemoveImg = config.GetValue(Of Integer)("SETTINGS", "REMOVESPRITE", 0)
        optRandomColor = config.GetValue(Of Integer)("SETTINGS", "RANDOMCOLOR", 1)
        optFade = config.GetValue(Of Integer)("SETTINGS", "FADEEFFECT", 1)
        optLastVehHash = config.GetValue(Of Integer)("SETTINGS", "LASTVEHHASH", -2022483795)
        optLastVehName = config.GetValue(Of String)("SETTINGS", "LASTVEHNAME", "Pfister Comet Retro Custom")
        optLogging = config.GetValue(Of Integer)("SETTINGS", "LOGGING", 1)
    End Sub

    Public Sub New()
        Try
            LoadSettings()

            _menuPool = New MenuPool()
            Rectangle.Color = Color.FromArgb(0, 0, 0, 0)

            CreateCategoryMenu()
            ReadConfirm()
            ReadCustomize()
            ReadColorCategory()
            CreateModMenuFor(CustomiseMenu, ItemPlate, PlateMenu, GetLangEntry("BTN_PLATE_STYLE"))
            ReadColorPrimary()
            ReadColorSecondary()
            CreateModMenuFor(ColorMenu, ItemCPriColor, CPriColorMenu, GetLangEntry("BTN_CUSTOM_PRIMARY"))
            CreateModMenuFor(ColorMenu, ItemCSecColor, CSecColorMenu, GetLangEntry("BTN_CUSTOM_SECONDARY"))
            CreateModMenuFor(PriColorMenu, ItemClassicColor, ClassicColorMenu, GetLangEntry("BTN_CLASSIC_COLOR"))
            CreateModMenuFor(PriColorMenu, ItemMetallicColor, MetallicColorMenu, GetLangEntry("BTN_METALLIC_COLOR"))
            CreateModMenuFor(PriColorMenu, ItemMetalColor, MetalColorMenu, GetLangEntry("BTN_METAL_COLOR"))
            CreateModMenuFor(PriColorMenu, ItemMatteColor, MatteColorMenu, GetLangEntry("BTN_MATTE_COLOR"))
            CreateModMenuFor(PriColorMenu, ItemChromeColor, ChromeColorMenu, GetLangEntry("BTN_CHROME_COLOR"))
            CreateModMenuFor(PriColorMenu, ItemPeaColor, PeaColorMenu, GetLangEntry("BTN_PEARLESCENT_COLOR"))
            CreateModMenuFor(SecColorMenu, ItemClassicColor2, ClassicColorMenu2, GetLangEntry("BTN_CLASSIC_COLOR"))
            CreateModMenuFor(SecColorMenu, ItemMetallicColor2, MetallicColorMenu2, GetLangEntry("BTN_METALLIC_COLOR"))
            CreateModMenuFor(SecColorMenu, ItemMetalColor2, MetalColorMenu2, GetLangEntry("BTN_METAL_COLOR"))
            CreateModMenuFor(SecColorMenu, ItemMatteColor2, MatteColorMenu2, GetLangEntry("BTN_MATTE_COLOR"))
            CreateModMenuFor(SecColorMenu, ItemChromeColor2, ChromeColorMenu2, GetLangEntry("BTN_CHROME_COLOR"))

            Native.Function.Call(Hash.REQUEST_SCRIPT_AUDIO_BANK, "VEHICLE_SHOP_HUD_1", False, -1)
            Native.Function.Call(Hash.REQUEST_SCRIPT_AUDIO_BANK, "VEHICLE_SHOP_HUD_2", False, -1)
        Catch ex As Exception
            logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateCategoryMenu()
        Try
            If optRemoveImg = 0 Then
                MainMenu = New UIMenu("", GetLangEntry("CATEGORY"), True)
                MainMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PDMCD4.shopui_title_pdm.png"))
            Else
                MainMenu = New UIMenu("", GetLangEntry("CATEGORY"), New Point(0, -107)) With {.EnableStats = True}
                MainMenu.SetBannerType(Rectangle)
            End If
            MainMenu.MouseEdgeEnabled = False
            _menuPool.Add(MainMenu)
            MainMenu.AddInstructionalButton(BtnRotLeft)
            MainMenu.AddInstructionalButton(BtnRotRight)
            MainMenu.AddInstructionalButton(BtnCamera)
            MainMenu.AddInstructionalButton(BtnZoom)
            'MainMenu.AddInstructionalButton(BtnStat)
            For Each file As String In IO.Directory.GetFiles(Application.StartupPath & "\scripts\PremiumDeluxeMotorsport\Vehicles\", "*.ini")
                If IO.File.Exists(file) Then
                    itemCat = New UIMenuItem(GetLangEntry(IO.Path.GetFileNameWithoutExtension(file)))
                    With itemCat
                        Dim lc As Integer = IO.File.ReadAllLines(file).Length
                        .SubInteger1 = lc
                        .SubString1 = IO.Path.GetFileNameWithoutExtension(file)
                    End With
                    MainMenu.AddItem(itemCat)
                End If
            Next
            MainMenu.RefreshIndex()
            AddHandler MainMenu.OnItemSelect, AddressOf CategoryItemSelectHandler
            AddHandler MainMenu.OnMenuClose, AddressOf MenuCloseHandler
        Catch ex As Exception
            logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CategoryItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        Try
            CreateVehicleMenu(Application.StartupPath & "\scripts\PremiumDeluxeMotorsport\Vehicles\" & selectedItem.SubString1 & ".ini", GetLangEntry(selectedItem.SubString1))
            sender.Visible = Not sender.Visible
            VehicleMenu.Visible = Not VehicleMenu.Visible
            If selectedItem.SubInteger1 > 10 Then
                VehicleMenu.GoDownOverflow()
                VehicleMenu.GoUpOverflow()
            Else
                VehicleMenu.GoUp()
                VehicleMenu.GoDown()
            End If

            VehicleMenu.RefreshIndex()
        Catch ex As Exception
            logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateVehicleMenu(File As String, Subtitle As String)
        Try
            Dim Format As New Reader(File, Parameters)

            If optRemoveImg = 0 Then
                VehicleMenu = New UIMenu("", Subtitle.ToUpper, True)
                VehicleMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PDMCD4.shopui_title_pdm.png"))
            Else
                VehicleMenu = New UIMenu("", Subtitle.ToUpper, New Point(0, -107)) With {.EnableStats = True}
                VehicleMenu.SetBannerType(Rectangle)
            End If

            VehicleMenu.MouseEdgeEnabled = False
            _menuPool.Add(VehicleMenu)
            VehicleMenu.AddInstructionalButton(BtnRotLeft)
            VehicleMenu.AddInstructionalButton(BtnRotRight)
            VehicleMenu.AddInstructionalButton(BtnCamera)
            VehicleMenu.AddInstructionalButton(BtnZoom)
            'VehicleMenu.AddInstructionalButton(BtnStat)

            For ii As Integer = 0 To Format.Count - 1
                Dim i As Integer = (Format.Count - 1) - ii
                Price = Format(i)("price")
                Dim item As New UIMenuItem(GetGXTEntry(Format(i)("make")) & " " & GetGXTEntry(Format(i)("gxt")))
                With item
                    If .Text.Contains("NULL") Then .Text = GetGXTEntry(Format(i)("gxt"))
                    If .Text.Contains("NULL") Then .Text = Format(i)("name")
                    .SetRightLabel("$" & Price.ToString("###,###"))
                    .SubString1 = Format(i)("model")
                    .SubInteger1 = Format(i)("price")
                    .SubString2 = GetGXTEntry(Format(i)("make")) & " " & GetGXTEntry(Format(i)("gxt"))
                    .SubString3 = Format(i)("make")
                    Dim model As Model = New Model(.SubString1)
                    If hiddenSave.GetValue(Of Integer)("VEHICLES", model.Hash, 0) = 0 Then
                        hiddenSave.SetValue(Of Integer)("VEHICLES", VehPreview.Model.Hash, 0)
                        .SetLeftBadge(UIMenuItem.BadgeStyle.Star)
                    End If
                End With
                Dim vmodel As Model = New Model(item.SubString1)
                If vmodel.IsInCdImage AndAlso vmodel.IsValid Then
                    VehicleMenu.AddItem(item)
                End If
            Next
            VehicleMenu.RefreshIndex()
            AddHandler VehicleMenu.OnItemSelect, AddressOf VehicleSelectHandler
            AddHandler VehicleMenu.OnIndexChange, AddressOf VehicleChangeHandler
            MainMenu.BindMenuToItem(VehicleMenu, itemCat)
        Catch ex As Exception
            logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub ReadCustomize()
        If optRemoveImg = 0 Then
            CustomiseMenu = New UIMenu("", GetLangEntry("BTN_CUSTOMIZE").ToUpper, True)
            CustomiseMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PDMCD4.shopui_title_pdm.png"))
        Else
            CustomiseMenu = New UIMenu("", GetLangEntry("BTN_CUSTOMIZE").ToUpper, New Point(0, -107)) With {.EnableStats = True}
            CustomiseMenu.SetBannerType(Rectangle)
        End If
        CustomiseMenu.MouseEdgeEnabled = False
        _menuPool.Add(CustomiseMenu)
        CustomiseMenu.AddInstructionalButton(BtnRotLeft)
        CustomiseMenu.AddInstructionalButton(BtnRotRight)
        CustomiseMenu.AddInstructionalButton(BtnCamera)
        CustomiseMenu.AddInstructionalButton(BtnZoom)
        CustomiseMenu.AddItem(ItemColor)
        CustomiseMenu.AddItem(New UIMenuItem(GetLangEntry("BTN_UPGRADE_NAME"), GetLangEntry("BTN_UPGRADE_DESC")))
        CustomiseMenu.AddItem(New UIMenuItem(GetLangEntry("BTN_PLATE_NUMBER_NAME"), GetLangEntry("BTN_PLATE_NUMBER_DESC")))
        CustomiseMenu.AddItem(ItemPlate)
        CustomiseMenu.RefreshIndex()
        ConfirmMenu.BindMenuToItem(CustomiseMenu, ItemCustomize)
        AddHandler CustomiseMenu.OnItemSelect, AddressOf ItemSelectHandler
    End Sub

    Public Shared Sub ReadColorCategory()
        If optRemoveImg = 0 Then
            ColorMenu = New UIMenu("", GetLangEntry("BTN_COLOR_NAME").ToUpper, True)
            ColorMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PDMCD4.shopui_title_pdm.png"))
        Else
            ColorMenu = New UIMenu("", GetLangEntry("BTN_COLOR_NAME").ToUpper, New Point(0, -107)) With {.EnableStats = True}
            ColorMenu.SetBannerType(Rectangle)
        End If
        ColorMenu.MouseEdgeEnabled = False
        _menuPool.Add(ColorMenu)
        ColorMenu.AddInstructionalButton(BtnRotLeft)
        ColorMenu.AddInstructionalButton(BtnRotRight)
        ColorMenu.AddInstructionalButton(BtnCamera)
        ColorMenu.AddInstructionalButton(BtnZoom)
        ColorMenu.AddItem(ItemPriColor)
        ColorMenu.AddItem(ItemSecColor)
        ColorMenu.AddItem(ItemCPriColor)
        ColorMenu.AddItem(ItemCSecColor)
        ColorMenu.RefreshIndex()
        CustomiseMenu.BindMenuToItem(ColorMenu, ItemColor)
    End Sub

    Public Shared Sub ReadColorPrimary()
        If optRemoveImg = 0 Then
            PriColorMenu = New UIMenu("", GetLangEntry("BTN_PRIMARY_COLOR").ToUpper, True)
            PriColorMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PDMCD4.shopui_title_pdm.png"))
        Else
            PriColorMenu = New UIMenu("", GetLangEntry("BTN_PRIMARY_COLOR").ToUpper, New Point(0, -107)) With {.EnableStats = True}
            PriColorMenu.SetBannerType(Rectangle)
        End If
        PriColorMenu.MouseEdgeEnabled = False
        _menuPool.Add(PriColorMenu)
        PriColorMenu.AddInstructionalButton(BtnRotLeft)
        PriColorMenu.AddInstructionalButton(BtnRotRight)
        PriColorMenu.AddInstructionalButton(BtnCamera)
        PriColorMenu.AddInstructionalButton(BtnZoom)
        PriColorMenu.AddItem(ItemClassicColor)
        PriColorMenu.AddItem(ItemMetallicColor)
        PriColorMenu.AddItem(ItemMatteColor)
        PriColorMenu.AddItem(ItemMetalColor)
        PriColorMenu.AddItem(ItemChromeColor)
        PriColorMenu.AddItem(ItemPeaColor)
        PriColorMenu.RefreshIndex()
        ColorMenu.BindMenuToItem(PriColorMenu, ItemPriColor)
    End Sub

    Public Shared Sub ReadColorSecondary()
        If optRemoveImg = 0 Then
            SecColorMenu = New UIMenu("", GetLangEntry("BTN_SECONDARY_COLOR").ToUpper, True)
            SecColorMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PDMCD4.shopui_title_pdm.png"))
        Else
            SecColorMenu = New UIMenu("", GetLangEntry("BTN_SECONDARY_COLOR").ToUpper, New Point(0, -107)) With {.EnableStats = True}
            SecColorMenu.SetBannerType(Rectangle)
        End If
        SecColorMenu.MouseEdgeEnabled = False
        _menuPool.Add(SecColorMenu)
        SecColorMenu.AddInstructionalButton(BtnRotLeft)
        SecColorMenu.AddInstructionalButton(BtnRotRight)
        SecColorMenu.AddInstructionalButton(BtnCamera)
        SecColorMenu.AddInstructionalButton(BtnZoom)
        SecColorMenu.AddItem(ItemClassicColor2)
        SecColorMenu.AddItem(ItemMetallicColor2)
        SecColorMenu.AddItem(ItemMatteColor2)
        SecColorMenu.AddItem(ItemMetalColor2)
        SecColorMenu.AddItem(ItemChromeColor2)
        SecColorMenu.RefreshIndex()
        ColorMenu.BindMenuToItem(SecColorMenu, ItemSecColor)
    End Sub

    Public Shared Sub ReadConfirm()
        If optRemoveImg = 0 Then
            ConfirmMenu = New UIMenu("", GetLangEntry("PURCHASE_ORDER"), True)
            ConfirmMenu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PDMCD4.shopui_title_pdm.png"))
        Else
            ConfirmMenu = New UIMenu("", GetLangEntry("PURCHASE_ORDER"), New Point(0, -107)) With {.EnableStats = True}
            ConfirmMenu.SetBannerType(Rectangle)
        End If
        ConfirmMenu.MouseEdgeEnabled = False
        _menuPool.Add(ConfirmMenu)
        ConfirmMenu.AddInstructionalButton(BtnRotLeft)
        ConfirmMenu.AddInstructionalButton(BtnRotRight)
        ConfirmMenu.AddInstructionalButton(BtnCamera)
        ConfirmMenu.AddInstructionalButton(BtnZoom)
        ConfirmMenu.AddItem(ItemCustomize)
        ConfirmMenu.AddItem(New UIMenuItem(GetLangEntry("BTN_TEST_DRIVE")))
        ConfirmMenu.AddItem(New UIMenuItem(GetLangEntry("BTN_CONFIRM")))
        ConfirmMenu.RefreshIndex()
        AddHandler ConfirmMenu.OnMenuClose, AddressOf ConfirmCloseHandler
        AddHandler ConfirmMenu.OnItemSelect, AddressOf ItemSelectHandler
    End Sub

    Public Shared Sub MenuCloseHandler(sender As UIMenu)
        Try
            TaskScriptStatus = -1
            If SelectedVehicle IsNot Nothing Then
                SelectedVehicle = Nothing
                VehPreview.Delete()
                'zTimer.Enabled = False
                'pdmStats.Dispose()
            End If
            PDM.camera.Stop()
            DrawSpotLight = False
            HideHud = False
            VehicleName = Nothing
            ShowVehicleName = False
            CustomiseMenu.RefreshIndex()
            ConfirmMenu.RefreshIndex()
            MainMenu.RefreshIndex()
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
            CustomiseMenu.RefreshIndex()
            ConfirmMenu.RefreshIndex()
            MainMenu.RefreshIndex()
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub VehicleSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        If selectedItem.Text = VehicleName Then 'If VehPreview.Exists() = True Then
            sender.Visible = False
            ConfirmMenu.Visible = True
            VehicleName = selectedItem.Text
            optLastVehMake = selectedItem.SubString3
            ShowVehicleName = True
            RefreshRGBColorMenuFor(CPriColorMenu, New UIMenuItem("nothing"), "Primary")
            RefreshRGBColorMenuFor(CSecColorMenu, New UIMenuItem("nothing"), "Secondary")
            RefreshColorMenuFor(ClassicColorMenu, New UIMenuItem("nothing"), Resources.ClassicColor, "Primary")
            RefreshColorMenuFor(MetallicColorMenu, New UIMenuItem("nothing"), Resources.ClassicColor, "Primary")
            RefreshColorMenuFor(MetalColorMenu, New UIMenuItem("nothing"), Resources.MetalColor, "Primary")
            RefreshColorMenuFor(MatteColorMenu, New UIMenuItem("nothing"), Resources.MatteColor, "Primary")
            RefreshColorMenuFor(ChromeColorMenu, New UIMenuItem("nothing"), Resources.ChromeColor, "Primary")
            RefreshColorMenuFor(PeaColorMenu, New UIMenuItem("nothing"), Resources.PearlescentColor, "Pearlescent")
            RefreshColorMenuFor(ClassicColorMenu2, New UIMenuItem("nothing"), Resources.ClassicColor, "Secondary")
            RefreshColorMenuFor(MetallicColorMenu2, New UIMenuItem("nothing"), Resources.ClassicColor, "Secondary")
            RefreshColorMenuFor(MetalColorMenu2, New UIMenuItem("nothing"), Resources.MetalColor, "Secondary")
            RefreshColorMenuFor(MatteColorMenu2, New UIMenuItem("nothing"), Resources.MatteColor, "Secondary")
            RefreshColorMenuFor(ChromeColorMenu2, New UIMenuItem("nothing"), Resources.ChromeColor, "Secondary")
            RefreshEnumModMenuFor(PlateMenu, New UIMenuItem("nothing"), EnumTypes.NumberPlateType)
        Else
            SelectedVehicle = selectedItem.SubString2
            If VehPreview = Nothing Then
                If Not selectedItem.Text.Contains("NULL") Then
                    If optFade = 1 Then
                        FadeScreenOut(200)
                        Wait(200)
                        VehPreview = CreateVehicle(selectedItem.SubString1, VehPreviewPos, Radius)
                        Wait(200)
                        FadeScreenIn(200)
                    Else
                        VehPreview = CreateVehicle(selectedItem.SubString1, VehPreviewPos, Radius)
                    End If
                End If
            Else
                VehPreview.Delete()
                'zTimer.Enabled = False
                'pdmStats.Dispose()
                If Not selectedItem.Text.Contains("NULL") Then
                    If optFade = 1 Then
                        FadeScreenOut(200)
                        Wait(200)
                        VehPreview = CreateVehicle(selectedItem.SubString1, VehPreviewPos, Radius)
                        Wait(200)
                        FadeScreenIn(200)
                    Else
                        VehPreview = CreateVehicle(selectedItem.SubString1, VehPreviewPos, Radius)
                    End If
                End If
            End If
            If optRandomColor = 1 Then
                Dim r As Random = New Random
                Dim psc As Integer = r.Next(0, 160)
                VehPreview.PrimaryColor = psc
                VehPreview.SecondaryColor = psc
                VehPreview.PearlescentColor = r.Next(0, 160)
                VehPreview.TrimColor = r.Next(0, 160)
                VehPreview.DashboardColor = r.Next(0, 160)
                VehPreview.RimColor = r.Next(0, 160)
            End If
            UpdateVehPreview()
            VehicleName = sender.MenuItems(index).Text
            optLastVehMake = sender.MenuItems(index).SubString3
            ShowVehicleName = True
            VehPreview.Heading = Radius 'New Vector3(0, 0, Radius)
            VehPreview.IsDriveable = False
            VehPreview.LockStatus = VehicleLockStatus.CannotBeTriedToEnter
            VehPreview.DirtLevel = 0
            VehiclePrice = selectedItem.SubInteger1
            '_Camera.PointAt(VehPreview, Vector3.Zero)
            optLastVehHash = VehPreview.Model.Hash
            optLastVehName = VehicleName
            config.SetValue(Of Integer)("SETTINGS", "LastVehHash", VehPreview.Model.Hash)
            config.SetValue(Of String)("SETTINGS", "LastVehName", VehicleName)
            config.Save()
        End If
    End Sub

    Public Shared Sub VehicleChangeHandler(sender As UIMenu, index As Integer)
        Try
            SelectedVehicle = sender.MenuItems(index).SubString2
            If VehPreview = Nothing Then
                If Not sender.MenuItems(index).Text.Contains("NULL") Then
                    If optFade = 1 Then
                        FadeScreenOut(200)
                        Wait(200)
                        VehPreview = CreateVehicle(sender.MenuItems(index).SubString1, VehPreviewPos, Radius)
                        Wait(200)
                        FadeScreenIn(200)
                    Else
                        VehPreview = CreateVehicle(sender.MenuItems(index).SubString1, VehPreviewPos, Radius)
                    End If
                End If
            Else
                VehPreview.Delete()
                'zTimer.Enabled = False
                'pdmStats.Dispose()
                If Not sender.MenuItems(index).Text.Contains("NULL") Then
                    If optFade = 1 Then
                        FadeScreenOut(200)
                        Wait(200)
                        VehPreview = CreateVehicle(sender.MenuItems(index).SubString1, VehPreviewPos, Radius)
                        Wait(200)
                        FadeScreenIn(200)
                    Else
                        VehPreview = CreateVehicle(sender.MenuItems(index).SubString1, VehPreviewPos, Radius)
                    End If
                End If
            End If
            If optRandomColor = 1 Then
                Dim r As Random = New Random
                Dim psc As Integer = r.Next(0, 160)
                VehPreview.PrimaryColor = psc
                VehPreview.SecondaryColor = psc
                VehPreview.PearlescentColor = r.Next(0, 160)
                VehPreview.TrimColor = r.Next(0, 160)
                VehPreview.DashboardColor = r.Next(0, 160)
                VehPreview.RimColor = r.Next(0, 160)
            End If
            UpdateVehPreview()
            VehicleName = sender.MenuItems(index).Text
            optLastVehMake = sender.MenuItems(index).SubString3
            ShowVehicleName = True
            VehPreview.Heading = Radius
            VehPreview.IsDriveable = False
            VehPreview.LockStatus = VehicleLockStatus.CannotBeTriedToEnter
            VehPreview.DirtLevel = 0
            VehiclePrice = sender.MenuItems(index).SubInteger1
            PDM.camera.RepositionFor(VehPreview)
            optLastVehHash = VehPreview.Model.Hash
            optLastVehName = VehicleName
            config.SetValue(Of Integer)("SETTINGS", "LastVehHash", VehPreview.Model.Hash)
            config.SetValue(Of String)("SETTINGS", "LastVehName", VehicleName)
            config.Save()

            If hiddenSave.GetValue(Of Integer)("VEHICLES", VehPreview.Model.Hash, 0) = 0 Then
                hiddenSave.SetValue(Of Integer)("VEHICLES", VehPreview.Model.Hash, 1)
                hiddenSave.Save()
            End If
        Catch ex As Exception
            logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub ItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        Try
            If selectedItem.Text = GetLangEntry("BTN_CONFIRM") Then
                If PlayerCash > VehiclePrice Then
                    FadeScreenOut(200)
                    Wait(200)
                    GP.Money = (PlayerCash - VehiclePrice)
                    ConfirmMenu.Visible = False
                    PDM.camera.Stop()
                    DrawSpotLight = False
                    VehPreview.IsDriveable = True
                    VehPreview.LockStatus = VehicleLockStatus.Unlocked
                    Native.Function.Call(Hash.SET_VEHICLE_DOORS_SHUT, VehPreview, False)
                    VehPreview.Position = New Vector3(-56.79958, -1110.868, 26.43581)
                    Native.Function.Call(Hash.TASK_WARP_PED_INTO_VEHICLE, GPC, VehPreview, -1)
                    VehPreview.MarkAsNoLongerNeeded()
                    VehPreview = Nothing
                    HideHud = False
                    Wait(200)
                    FadeScreenIn(200)
                    Native.Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "PROPERTY_PURCHASE", "HUD_AWARDS", False)
                    BigMessageThread.MessageInstance.ShowWeaponPurchasedMessage("~y~" & GetLangEntry("VEHICLE_PURCHASED"), "~w~" & SelectedVehicle, Nothing)
                    SelectedVehicle = Nothing
                    VehicleName = Nothing
                    ShowVehicleName = False
                    TaskScriptStatus = -1
                Else
                    If Game.Player.Character.Name = "Franklin" Then
                        DisplayNotificationThisFrame(GetLangEntry("FLEECA_BANK"), GetLangEntry("INSUFFICIENT_FUNDS_TITLE"), GetLangEntry("INSUFFICIENT_FUNCS_BODY"), "CHAR_BANK_FLEECA", True, IconType.RightJumpingArrow)
                    ElseIf Game.Player.Character.Name = "Trevor" Then
                        DisplayNotificationThisFrame(GetLangEntry("BOL_BANK"), GetLangEntry("INSUFFICIENT_FUNDS_TITLE"), GetLangEntry("INSUFFICIENT_FUNCS_BODY"), "CHAR_BANK_BOL", True, IconType.RightJumpingArrow)
                    Else
                        DisplayNotificationThisFrame(GetLangEntry("MAZE_BANK"), GetLangEntry("INSUFFICIENT_FUNDS_TITLE"), GetLangEntry("INSUFFICIENT_FUNCS_BODY"), "CHAR_BANK_MAZE", True, IconType.RightJumpingArrow)
                    End If
                End If
            ElseIf selectedItem.Text = GetLangEntry("BTN_TEST_DRIVE") Then
                FadeScreenOut(200)
                Wait(200)
                Native.Function.Call(Hash.TASK_WARP_PED_INTO_VEHICLE, GPC, VehPreview, -1)
                ConfirmMenu.Visible = False
                PDM.camera.Stop()
                DrawSpotLight = False
                VehPreview.IsDriveable = True
                VehPreview.LockStatus = VehicleLockStatus.Unlocked
                Native.Function.Call(Hash.SET_VEHICLE_DOORS_SHUT, VehPreview, False)
                DisplayHelpTextThisFrame(GetLangEntry("HELP_TEST_DRIVE"))
                TestDrive = TestDrive + 1
                HideHud = False
                VehPreview.Position = New Vector3(-56.79958, -1110.868, 26.43581)
                Wait(200)
                FadeScreenIn(200)
                ShowVehicleName = False
            End If

            If selectedItem.Text = GetLangEntry("BTN_UPGRADE_NAME") Then
                VehPreview.InstallModKit()
                VehPreview.SetMod(VehicleMod.Suspension, VehPreview.GetModCount(VehicleMod.Suspension) - 1, False)
                VehPreview.SetMod(VehicleMod.Engine, VehPreview.GetModCount(VehicleMod.Engine) - 1, False)
                VehPreview.SetMod(VehicleMod.Brakes, VehPreview.GetModCount(VehicleMod.Brakes) - 1, False)
                VehPreview.SetMod(VehicleMod.Transmission, VehPreview.GetModCount(VehicleMod.Transmission) - 1, False)
                VehPreview.SetMod(VehicleMod.Armor, VehPreview.GetModCount(VehicleMod.Armor) - 1, False)
                VehPreview.ToggleMod(VehicleToggleMod.XenonHeadlights, True)
                VehPreview.ToggleMod(VehicleToggleMod.Turbo, True)
                Native.Function.Call(Hash.SET_VEHICLE_TYRES_CAN_BURST, VehPreview, False)
                Native.Function.Call(Hash._START_SCREEN_EFFECT, "MP_corona_switch_supermod", 0, 1)
                Native.Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "Lowrider_Upgrade", "Lowrider_Super_Mod_Garage_Sounds", 1)
            ElseIf selectedItem.Text = GetLangEntry("BTN_PLATE_NUMBER_NAME") Then
                Dim NumPlateText As String = GetUserInput(VehPreview.NumberPlate, 9)
                If NumPlateText <> "" Then
                    VehPreview.NumberPlate = NumPlateText
                End If
            End If
        Catch ex As Exception
            logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub OnTick(o As Object, e As EventArgs) Handles Me.Tick
        Try
            _menuPool.ProcessMenus()
            _menuPool.UpdateStats(GetVehTopSpeed(VehPreview), GetVehAcceleration(VehPreview), GetVehBraking(VehPreview), GetVehTraction(VehPreview))

            If _menuPool.IsAnyMenuOpen Then
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
            End If
        Catch ex As Exception
            logger.Log("Error Menu Pool " & ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateModMenuFor(ByRef parentMenu As UIMenu, ByRef parentItem As UIMenuItem, ByRef menu As UIMenu, ByRef title As String)
        Try
            If optRemoveImg = 0 Then
                menu = New UIMenu("", title.ToUpper, True)
                menu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PDMCD4.shopui_title_pdm.png"))
            Else
                menu = New UIMenu("", title, New Point(0, -107)) With {.EnableStats = True}
                menu.SetBannerType(Rectangle)
            End If
            menu.MouseEdgeEnabled = False
            _menuPool.Add(menu)
            menu.AddItem(New UIMenuItem("Nothing"))
            menu.RefreshIndex()
            parentMenu.BindMenuToItem(menu, parentItem)
            AddHandler menu.OnMenuClose, AddressOf ModsMenuCloseHandler
            AddHandler menu.OnItemSelect, AddressOf ModsMenuItemSelectHandler
            AddHandler menu.OnIndexChange, AddressOf ModsMenuIndexChangedHandler
        Catch ex As Exception
            logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshColorMenuFor(ByRef menu As UIMenu, ByRef item As UIMenuItem, ByRef colorList As List(Of VehicleColor), prisecpear As String)
        Try
            menu.MenuItems.Clear()
            For Each col As VehicleColor In colorList
                item = New UIMenuItem(GetLocalizedColorName(col))
                With item
                    .SubInteger1 = col
                    If prisecpear = "Primary" Then
                        If VehPreview.PrimaryColor = col Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    ElseIf prisecpear = "Secondary" Then
                        If VehPreview.SecondaryColor = col Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    ElseIf prisecpear = "Pearlescent" Then
                        If VehPreview.PearlescentColor = col Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    End If
                End With
                menu.AddItem(item)
            Next
            menu.RefreshIndex()
        Catch ex As Exception
            logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshRGBColorMenuFor(ByRef menu As UIMenu, ByRef item As UIMenuItem, category As String)
        Try
            menu.MenuItems.Clear()
            Dim removeList As New List(Of String) From {"R", "G", "B", "A", "IsKnownColor", "IsEmpty", "IsNamedColor", "IsSystemColor", "Name", "Transparent"}
            Dim index As Integer = 0
            For Each col As Reflection.PropertyInfo In GetType(Drawing.Color).GetProperties()
                If Not removeList.Contains(col.Name) Then
                    item = New UIMenuItem(Trim(RegularExpressions.Regex.Replace(col.Name, "[A-Z]", " ${0}")))
                    With item
                        .SubInteger1 = Drawing.Color.FromName(col.Name).R
                        .SubInteger2 = Drawing.Color.FromName(col.Name).G
                        .SubInteger3 = Drawing.Color.FromName(col.Name).B
                        If category = "Primary" Then
                            If VehPreview.CustomPrimaryColor = Drawing.Color.FromName(col.Name) Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                        ElseIf category = "Secondary" Then
                            If VehPreview.CustomSecondaryColor = Drawing.Color.FromName(col.Name) Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                        End If
                    End With
                    menu.AddItem(item)
                End If
            Next

            menu.RefreshIndex()
        Catch ex As Exception
            logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshEnumModMenuFor(ByRef menu As UIMenu, ByRef item As UIMenuItem, ByRef enumType As EnumTypes)
        Try
            menu.MenuItems.Clear()

            Dim enumArray As Array = Nothing
            Select Case enumType
                Case EnumTypes.NumberPlateType
                    enumArray = System.Enum.GetValues(GetType(NumberPlateType))
                    For Each enumItem As NumberPlateType In enumArray
                        item = New UIMenuItem(LocalizedLicensePlate(enumItem))
                        With item
                            .SubInteger1 = enumItem
                            If VehPreview.NumberPlateType = enumItem Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                        End With
                        menu.AddItem(item)
                    Next
                Case EnumTypes.VehicleWindowTint
                    enumArray = System.Enum.GetValues(GetType(VehicleWindowTint))
                    For Each enumItem As VehicleWindowTint In enumArray
                        item = New UIMenuItem(LocalizedWindowsTint(enumItem))
                        With item
                            .SubInteger1 = enumItem
                            If VehPreview.WindowTint = enumItem Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                        End With
                        menu.AddItem(item)
                    Next
                Case EnumTypes.VehicleColorTrim
                    enumArray = System.Enum.GetValues(GetType(VehicleColor))
                    For Each enumItem As VehicleColor In enumArray
                        item = New UIMenuItem(GetLocalizedColorName(enumItem))
                        With item
                            .SubInteger1 = enumItem
                            If VehPreview.TrimColor = enumItem Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                        End With
                        menu.AddItem(item)
                    Next
                Case EnumTypes.VehicleColorDashboard
                    enumArray = System.Enum.GetValues(GetType(VehicleColor))
                    For Each enumItem As VehicleColor In enumArray
                        item = New UIMenuItem(GetLocalizedColorName(enumItem))
                        With item
                            .SubInteger1 = enumItem
                            If VehPreview.DashboardColor = enumItem Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                        End With
                        menu.AddItem(item)
                    Next
                Case EnumTypes.VehicleColorRim
                    enumArray = System.Enum.GetValues(GetType(VehicleColor))
                    For Each enumItem As VehicleColor In enumArray
                        item = New UIMenuItem(GetLocalizedColorName(enumItem))
                        With item
                            .SubInteger1 = enumItem
                            If VehPreview.RimColor = enumItem Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                        End With
                        menu.AddItem(item)
                    Next
            End Select
            menu.RefreshIndex()
        Catch ex As Exception
            logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub ModsMenuCloseHandler(sender As UIMenu)
        Try
            'Color
            'VehPreview.DashboardColor = lastVehMemory.LightsColor
            'VehPreview.TrimColor = lastVehMemory.TrimColor
            'VehPreview.RimColor = lastVehMemory.RimColor
            'VehPreview.NeonLightsColor = lastVehMemory.NeonLightsColor
            'VehPreview.TireSmokeColor = lastVehMemory.TireSmokeColor

            VehPreview.PrimaryColor = lastVehMemory.PrimaryColor
            VehPreview.SecondaryColor = lastVehMemory.SecondaryColor
            VehPreview.PearlescentColor = lastVehMemory.PearlescentColor
            VehPreview.NumberPlateType = lastVehMemory.NumberPlate

            If optRemoveColor = 1 Then
                If sender Is CPriColorMenu Then
                    VehPreview.PrimaryColor = VehicleColor.MetallicBlack
                ElseIf sender Is CSecColorMenu Then
                    VehPreview.SecondaryColor = VehicleColor.MetallicBlack
                ElseIf sender.ParentMenu Is PriColorMenu Then
                    VehPreview.ClearCustomPrimaryColor()
                ElseIf sender.ParentMenu Is SecColorMenu Then
                    VehPreview.ClearCustomSecondaryColor()
                End If
            End If

            If sender Is CPriColorMenu Then VehPreview.CustomPrimaryColor = lastVehMemory.CustomPrimaryColor
            If sender Is CSecColorMenu Then VehPreview.CustomSecondaryColor = lastVehMemory.CustomSecondaryColor
        Catch ex As Exception
            logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub ModsMenuItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        Try
            For Each i As UIMenuItem In sender.MenuItems
                i.SetRightBadge(UIMenuItem.BadgeStyle.None)
            Next

            'Color
            If (sender Is ClassicColorMenu) Or (sender Is ChromeColorMenu) Or (sender Is MatteColorMenu) Or (sender Is MetalColorMenu) Then
                VehPreview.PrimaryColor = selectedItem.SubInteger1
                selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                lastVehMemory.PrimaryColor = selectedItem.SubInteger1
            ElseIf sender Is MetallicColorMenu Then
                VehPreview.PrimaryColor = selectedItem.SubInteger1
                VehPreview.PearlescentColor = selectedItem.SubInteger1
                selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                lastVehMemory.PrimaryColor = selectedItem.SubInteger1
                lastVehMemory.PearlescentColor = selectedItem.SubInteger1
            ElseIf sender Is PeaColorMenu Then
                VehPreview.PearlescentColor = selectedItem.SubInteger1
                selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                lastVehMemory.PearlescentColor = selectedItem.SubInteger1
            ElseIf (sender Is ClassicColorMenu2) Or (sender Is ChromeColorMenu2) Or (sender Is MatteColorMenu2) Or (sender Is MetalColorMenu2) Then
                VehPreview.SecondaryColor = selectedItem.SubInteger1
                selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                lastVehMemory.SecondaryColor = selectedItem.SubInteger1
            ElseIf sender Is MetallicColorMenu2 Then
                VehPreview.SecondaryColor = selectedItem.SubInteger1
                VehPreview.PearlescentColor = selectedItem.SubInteger1
                selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                lastVehMemory.SecondaryColor = selectedItem.SubInteger1
                lastVehMemory.PearlescentColor = selectedItem.SubInteger1
            ElseIf sender Is CPriColorMenu Then
                VehPreview.CustomPrimaryColor = Color.FromArgb(selectedItem.SubInteger1, selectedItem.SubInteger2, selectedItem.SubInteger3)
                selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                lastVehMemory.CustomPrimaryColor = Color.FromArgb(selectedItem.SubInteger1, selectedItem.SubInteger2, selectedItem.SubInteger3)
            ElseIf sender Is CSecColorMenu Then
                VehPreview.CustomSecondaryColor = Color.FromArgb(selectedItem.SubInteger1, selectedItem.SubInteger2, selectedItem.SubInteger3)
                selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                lastVehMemory.CustomSecondaryColor = Color.FromArgb(selectedItem.SubInteger1, selectedItem.SubInteger2, selectedItem.SubInteger3)
            ElseIf sender Is PlateMenu Then
                VehPreview.NumberPlateType = selectedItem.SubInteger1
                selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                lastVehMemory.NumberPlate = selectedItem.SubInteger1
            End If

            If optRemoveColor = 1 Then
                If sender Is CPriColorMenu Then
                    VehPreview.PrimaryColor = VehicleColor.MetallicBlack
                ElseIf sender Is CSecColorMenu Then
                    VehPreview.SecondaryColor = VehicleColor.MetallicBlack
                ElseIf sender.ParentMenu Is PriColorMenu Then
                    VehPreview.ClearCustomPrimaryColor()
                ElseIf sender.ParentMenu Is SecColorMenu Then
                    VehPreview.ClearCustomSecondaryColor()
                End If
            End If
        Catch ex As Exception
            logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub ModsMenuIndexChangedHandler(sender As UIMenu, index As Integer)
        Try
            If (sender Is ClassicColorMenu) Or (sender Is ChromeColorMenu) Or (sender Is MatteColorMenu) Or (sender Is MetalColorMenu) Then
                VehPreview.PrimaryColor = sender.MenuItems(index).SubInteger1
            ElseIf sender Is MetallicColorMenu Then
                VehPreview.PrimaryColor = sender.MenuItems(index).SubInteger1
                VehPreview.PearlescentColor = sender.MenuItems(index).SubInteger1
            ElseIf sender Is PeaColorMenu Then
                VehPreview.PearlescentColor = sender.MenuItems(index).SubInteger1
            ElseIf (sender Is ClassicColorMenu2) Or (sender Is ChromeColorMenu2) Or (sender Is MatteColorMenu2) Or (sender Is MetalColorMenu2) Then
                VehPreview.SecondaryColor = sender.MenuItems(index).SubInteger1
            ElseIf sender Is MetallicColorMenu2 Then
                VehPreview.SecondaryColor = sender.MenuItems(index).SubInteger1
                VehPreview.PearlescentColor = sender.MenuItems(index).SubInteger1
            ElseIf sender Is CPriColorMenu Then
                VehPreview.CustomPrimaryColor = Color.FromArgb(sender.MenuItems(index).SubInteger1, sender.MenuItems(index).SubInteger2, sender.MenuItems(index).SubInteger3)
            ElseIf sender Is CSecColorMenu Then
                VehPreview.CustomSecondaryColor = Color.FromArgb(sender.MenuItems(index).SubInteger1, sender.MenuItems(index).SubInteger2, sender.MenuItems(index).SubInteger3)
            ElseIf sender Is PlateMenu Then
                VehPreview.NumberPlateType = sender.MenuItems(index).SubInteger1
            End If

            If optRemoveColor = 1 Then
                If sender Is CPriColorMenu Then
                    VehPreview.PrimaryColor = VehicleColor.MetallicBlack
                ElseIf sender Is CSecColorMenu Then
                    VehPreview.SecondaryColor = VehicleColor.MetallicBlack
                ElseIf sender.ParentMenu Is PriColorMenu Then
                    VehPreview.ClearCustomPrimaryColor()
                ElseIf sender.ParentMenu Is SecColorMenu Then
                    VehPreview.ClearCustomSecondaryColor()
                End If
            End If
        Catch ex As Exception
            logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub UpdateVehPreview()
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
End Class
