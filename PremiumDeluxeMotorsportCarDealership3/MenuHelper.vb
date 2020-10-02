Imports System.Drawing
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Text
Imports System.Windows.Forms
Imports GTA
Imports GTA.Math
Imports GTA.Native
Imports INMNativeUI

Public Module MenuHelper

    Public MainMenu, ConfirmMenu, CustomiseMenu, VehicleMenu As UIMenu
    Public itemCat As UIMenuItem
    Public PriColorMenu, ClassicColorMenu, MetallicColorMenu, MetalColorMenu, MatteColorMenu, ChromeColorMenu, PeaColorMenu, CPriColorMenu As UIMenu
    Public ColorMenu, SecColorMenu, ClassicColorMenu2, MetallicColorMenu2, MetalColorMenu2, MatteColorMenu2, ChromeColorMenu2, CSecColorMenu, PlateMenu As UIMenu

    Public ItemCustomize, ItemConfirm, ItemColor, ItemClassicColor, ItemClassicColor2, ItemMetallicColor, ItemMetallicColor2, ItemMetalColor, ItemMetalColor2,
        ItemMatteColor, ItemMatteColor2, ItemChromeColor, ItemChromeColor2, ItemCPriColor, ItemCSecColor, ItemPriColor, ItemSecColor, ItemPeaColor, ItemPlate As UIMenuItem

    Public Parameters As String() = {"[name]", "[price]", "[model]", "[gxt]", "[make]"}

    Public _menuPool As MenuPool

    Public Sub CreateMenus()
        ItemCustomize = New UIMenuItem(GetLangEntry("BTN_CUSTOMIZE"))
        ItemConfirm = New UIMenuItem(Game.GetGXTEntry("ITEM_YES"))
        ItemColor = New UIMenuItem(Game.GetGXTEntry("IB_COLOR"), Game.GetGXTEntry("CMOD_MOD_6_D"))
        ItemClassicColor = New UIMenuItem(Game.GetGXTEntry("CMOD_COL1_1"), Game.GetGXTEntry("CMOD_MOD_6_D"))
        ItemClassicColor2 = New UIMenuItem(Game.GetGXTEntry("CMOD_COL1_1"), Game.GetGXTEntry("CMOD_MOD_6_D"))
        ItemMetallicColor = New UIMenuItem(Game.GetGXTEntry("CMOD_COL1_3"), Game.GetGXTEntry("CMOD_MOD_6_D"))
        ItemMetallicColor2 = New UIMenuItem(Game.GetGXTEntry("CMOD_COL1_3"), Game.GetGXTEntry("CMOD_MOD_6_D"))
        ItemMetalColor = New UIMenuItem(Game.GetGXTEntry("CMOD_COL1_4"), Game.GetGXTEntry("CMOD_MOD_6_D"))
        ItemMetalColor2 = New UIMenuItem(Game.GetGXTEntry("CMOD_COL1_4"), Game.GetGXTEntry("CMOD_MOD_6_D"))
        ItemMatteColor = New UIMenuItem(Game.GetGXTEntry("CMOD_COL1_5"), Game.GetGXTEntry("CMOD_MOD_6_D"))
        ItemMatteColor2 = New UIMenuItem(Game.GetGXTEntry("CMOD_COL1_5"), Game.GetGXTEntry("CMOD_MOD_6_D"))
        ItemChromeColor = New UIMenuItem(Game.GetGXTEntry("CMOD_COL1_0"), Game.GetGXTEntry("CMOD_MOD_6_D"))
        ItemChromeColor2 = New UIMenuItem(Game.GetGXTEntry("CMOD_COL1_0"), Game.GetGXTEntry("CMOD_MOD_6_D"))
        ItemCPriColor = New UIMenuItem(GetLangEntry("BTN_CUSTOM_PRIMARY"), Game.GetGXTEntry("CMOD_MOD_6_D"))
        ItemCSecColor = New UIMenuItem(GetLangEntry("BTN_CUSTOM_SECONDARY"), Game.GetGXTEntry("CMOD_MOD_6_D"))
        ItemPriColor = New UIMenuItem(Game.GetGXTEntry("CMOD_COL0_0"), Game.GetGXTEntry("CMOD_MOD_6_D"))
        ItemSecColor = New UIMenuItem(Game.GetGXTEntry("CMOD_COL0_1"), Game.GetGXTEntry("CMOD_MOD_6_D"))
        ItemPeaColor = New UIMenuItem(Game.GetGXTEntry("CMOD_COL1_6"), Game.GetGXTEntry("CMOD_MOD_6_D"))
        ItemPlate = New UIMenuItem(Game.GetGXTEntry("CMOD_MOD_PLA"), Game.GetGXTEntry("CMOD_MOD_6_D"))

        CreateCategoryMenu()
        CreateConfirmMenu()
        CreateCustomizeMenu()
        CreateColorCategory()
        PlateMenu = PlateMenu.NewUIMenu(Game.GetGXTEntry("CMOD_MOD_PLA"), CustomiseMenu, ItemPlate)
        CreatePrimaryColor()
        CreateSecondaryColor()
        CPriColorMenu = CPriColorMenu.NewUIMenu(GetLangEntry("BTN_CUSTOM_PRIMARY"), ColorMenu, ItemCPriColor)
        CSecColorMenu = CSecColorMenu.NewUIMenu(GetLangEntry("BTN_CUSTOM_SECONDARY"), ColorMenu, ItemCSecColor)
        ClassicColorMenu = ClassicColorMenu.NewUIMenu(Game.GetGXTEntry("CMOD_COL1_1"), PriColorMenu, ItemClassicColor)
        MetallicColorMenu = MetallicColorMenu.NewUIMenu(Game.GetGXTEntry("CMOD_COL1_3"), PriColorMenu, ItemMetallicColor)
        MetalColorMenu = MetalColorMenu.NewUIMenu(Game.GetGXTEntry("CMOD_COL1_4"), PriColorMenu, ItemMetalColor)
        MatteColorMenu = MatteColorMenu.NewUIMenu(Game.GetGXTEntry("CMOD_COL1_5"), PriColorMenu, ItemMatteColor)
        ChromeColorMenu = ChromeColorMenu.NewUIMenu(Game.GetGXTEntry("CMOD_COL1_0"), PriColorMenu, ItemChromeColor)
        PeaColorMenu = PeaColorMenu.NewUIMenu(Game.GetGXTEntry("CMOD_COL1_6"), PriColorMenu, ItemPeaColor)
        ClassicColorMenu2 = ClassicColorMenu2.NewUIMenu(Game.GetGXTEntry("CMOD_COL1_1"), SecColorMenu, ItemClassicColor2)
        MetallicColorMenu2 = MetallicColorMenu2.NewUIMenu(Game.GetGXTEntry("CMOD_COL1_3"), SecColorMenu, ItemMetallicColor2)
        MetalColorMenu2 = MetalColorMenu2.NewUIMenu(Game.GetGXTEntry("CMOD_COL1_4"), SecColorMenu, ItemMetalColor2)
        MatteColorMenu2 = MatteColorMenu2.NewUIMenu(Game.GetGXTEntry("CMOD_COL1_5"), SecColorMenu, ItemMatteColor2)
        ChromeColorMenu2 = ChromeColorMenu2.NewUIMenu(Game.GetGXTEntry("CMOD_COL1_0"), SecColorMenu, ItemChromeColor2)
    End Sub

    Public Sub CreateCategoryMenu()
        MainMenu = MainMenu.NewUIMenu(Game.GetGXTEntry("CMOD_MOD_T"), True)
        For Each file As String In IO.Directory.GetFiles(".\scripts\PremiumDeluxeMotorsport\Vehicles\", "*.ini")
            If IO.File.Exists(file) Then
                itemCat = New UIMenuItem(GetLangEntry(IO.Path.GetFileNameWithoutExtension(file)))
                With itemCat
                    Dim lc As Integer = IO.File.ReadAllLines(file).Length
                    .Tag = New Tuple(Of Integer, String)(lc, IO.Path.GetFileNameWithoutExtension(file))
                End With
                MainMenu.AddItem(itemCat)
            End If
        Next
        MainMenu.RefreshIndex()
        AddHandler MainMenu.OnItemSelect, AddressOf CategoryItemSelectHandler
        AddHandler MainMenu.OnMenuClose, AddressOf MenuCloseHandler
    End Sub

    Public Sub CreateConfirmMenu()
        ConfirmMenu = ConfirmMenu.NewUIMenu(GetLangEntry("PURCHASE_ORDER"), True)
        ConfirmMenu.AddItem(ItemCustomize)
        ConfirmMenu.AddItem(New UIMenuItem(GetLangEntry("BTN_TEST_DRIVE")))
        ConfirmMenu.AddItem(New UIMenuItem(Game.GetGXTEntry("ITEM_YES")))
        ConfirmMenu.RefreshIndex()
        AddHandler ConfirmMenu.OnMenuClose, AddressOf ConfirmCloseHandler
        AddHandler ConfirmMenu.OnItemSelect, AddressOf ItemSelectHandler
    End Sub

    Public Sub CreateCustomizeMenu()
        CustomiseMenu = CustomiseMenu.NewUIMenu(GetLangEntry("BTN_CUSTOMIZE").ToUpper, True)
        CustomiseMenu.AddItem(ItemColor)
        CustomiseMenu.AddItem(New UIMenuItem(Game.GetGXTEntry("PERSO_MOD_PER"), Game.GetGXTEntry("IE_MOD_OBJ4")))
        CustomiseMenu.AddItem(New UIMenuItem(GetLangEntry("BTN_PLATE_NUMBER_NAME"), Game.GetGXTEntry("IE_MOD_OBJ2")))
        CustomiseMenu.AddItem(ItemPlate)
        CustomiseMenu.RefreshIndex()
        ConfirmMenu.BindMenuToItem(CustomiseMenu, ItemCustomize)
        AddHandler CustomiseMenu.OnItemSelect, AddressOf ItemSelectHandler
    End Sub

    Public Sub CreateColorCategory()
        ColorMenu = ColorMenu.NewUIMenu(Game.GetGXTEntry("CMOD_COL1_T"), True)
        ColorMenu.AddItem(ItemPriColor)
        ColorMenu.AddItem(ItemSecColor)
        ColorMenu.AddItem(ItemCPriColor)
        ColorMenu.AddItem(ItemCSecColor)
        ColorMenu.RefreshIndex()
        CustomiseMenu.BindMenuToItem(ColorMenu, ItemColor)
    End Sub

    Public Sub CreatePrimaryColor()
        PriColorMenu = PriColorMenu.NewUIMenu(Game.GetGXTEntry("CMOD_COL2_T"), True)
        PriColorMenu.AddItem(ItemClassicColor)
        PriColorMenu.AddItem(ItemMetallicColor)
        PriColorMenu.AddItem(ItemMatteColor)
        PriColorMenu.AddItem(ItemMetalColor)
        PriColorMenu.AddItem(ItemChromeColor)
        PriColorMenu.AddItem(ItemPeaColor)
        PriColorMenu.RefreshIndex()
        ColorMenu.BindMenuToItem(PriColorMenu, ItemPriColor)
    End Sub

    Public Sub CreateSecondaryColor()
        SecColorMenu = SecColorMenu.NewUIMenu(Game.GetGXTEntry("CMOD_COL3_T"), True)
        SecColorMenu.AddItem(ItemClassicColor2)
        SecColorMenu.AddItem(ItemMetallicColor2)
        SecColorMenu.AddItem(ItemMatteColor2)
        SecColorMenu.AddItem(ItemMetalColor2)
        SecColorMenu.AddItem(ItemChromeColor2)
        SecColorMenu.RefreshIndex()
        ColorMenu.BindMenuToItem(SecColorMenu, ItemSecColor)
    End Sub

    <Extension>
    Public Function NewUIMenu(ByRef menu As UIMenu, title As String, showStats As Boolean) As UIMenu
        Try
            If optRemoveImg = 0 Then
                menu = New UIMenu("", title, showStats)
                menu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PDMCD4.shopui_title_pdm.png"))
            Else
                menu = New UIMenu("", title, New Point(0, -107)) With {.EnableStats = showStats}
                menu.SetBannerType(New UIResRectangle() With {.Color = Color.Transparent})
            End If

            menu.MouseEdgeEnabled = False
            menu.AddInstructionalButton(BtnRotLeft)
            menu.AddInstructionalButton(BtnRotRight)
            menu.AddInstructionalButton(BtnCamera)
            menu.AddInstructionalButton(BtnZoom)
            _menuPool.Add(menu)
        Catch ex As Exception
            logger.Log(ex.Message & " " & ex.StackTrace)
        End Try

        Return menu
    End Function

    <Extension>
    Public Function NewUIMenu(ByRef menu As UIMenu, title As String, ByRef parentMenu As UIMenu, ByRef parentItem As UIMenuItem) As UIMenu
        Try
            If optRemoveImg = 0 Then
                menu = New UIMenu("", title, True)
                menu.SetBannerType(Sprite.WriteFileFromResources(Assembly.GetExecutingAssembly, "PDMCD4.shopui_title_pdm.png"))
            Else
                menu = New UIMenu("", title, New Point(0, -107)) With {.EnableStats = True}
                menu.SetBannerType(New UIResRectangle() With {.Color = Color.Transparent})
            End If

            menu.MouseEdgeEnabled = False
            menu.AddInstructionalButton(BtnRotLeft)
            menu.AddInstructionalButton(BtnRotRight)
            menu.AddInstructionalButton(BtnCamera)
            menu.AddInstructionalButton(BtnZoom)
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

        Return menu
    End Function

    Public Sub MenuCloseHandler(sender As UIMenu)
        Try
            TaskScriptStatus = -1
            If SelectedVehicle IsNot Nothing Then
                SelectedVehicle = Nothing
                VehPreview.Delete()
            End If
            wsCamera.Stop()
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

    Public Sub ConfirmCloseHandler(sender As UIMenu)
        Try
            MainMenu.Visible = True
            CustomiseMenu.RefreshIndex()
            ConfirmMenu.RefreshIndex()
            MainMenu.RefreshIndex()
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Public Sub RefreshColorMenuFor(ByRef menu As UIMenu, ByRef item As UIMenuItem, ByRef colorList As List(Of VehicleColor), prisecpear As String)
        Try
            menu.MenuItems.Clear()
            For Each col As VehicleColor In colorList
                item = New UIMenuItem(GetLocalizedColorName(col))
                With item
                    .Tag = col
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

    Public Sub RefreshRGBColorMenuFor(ByRef menu As UIMenu, ByRef item As UIMenuItem, category As String)
        Try
            menu.MenuItems.Clear()
            Dim removeList As New List(Of String) From {"R", "G", "B", "A", "IsKnownColor", "IsEmpty", "IsNamedColor", "IsSystemColor", "Name", "Transparent"}
            Dim index As Integer = 0
            For Each col As Reflection.PropertyInfo In GetType(Drawing.Color).GetProperties()
                If Not removeList.Contains(col.Name) Then
                    item = New UIMenuItem(Trim(RegularExpressions.Regex.Replace(col.Name, "[A-Z]", " ${0}")))
                    With item
                        .Tag = Color.FromArgb(Color.FromName(col.Name).R, Color.FromName(col.Name).G, Color.FromName(col.Name).B)
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

    Public Sub RefreshEnumModMenuFor(ByRef menu As UIMenu, ByRef item As UIMenuItem, ByRef enumType As EnumTypes)
        Try
            menu.MenuItems.Clear()

            Dim enumArray As Array = Nothing
            Select Case enumType
                Case EnumTypes.NumberPlateType
                    enumArray = System.Enum.GetValues(GetType(NumberPlateType))
                    For Each enumItem As NumberPlateType In enumArray
                        item = New UIMenuItem(LocalizedLicensePlate(enumItem))
                        With item
                            .Tag = enumItem
                            If VehPreview.NumberPlateType = enumItem Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                        End With
                        menu.AddItem(item)
                    Next
                Case EnumTypes.VehicleWindowTint
                    enumArray = System.Enum.GetValues(GetType(VehicleWindowTint))
                    For Each enumItem As VehicleWindowTint In enumArray
                        item = New UIMenuItem(LocalizedWindowsTint(enumItem))
                        With item
                            .Tag = enumItem
                            If VehPreview.WindowTint = enumItem Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                        End With
                        menu.AddItem(item)
                    Next
                Case EnumTypes.VehicleColorTrim
                    enumArray = System.Enum.GetValues(GetType(VehicleColor))
                    For Each enumItem As VehicleColor In enumArray
                        item = New UIMenuItem(GetLocalizedColorName(enumItem))
                        With item
                            .Tag = enumItem
                            If VehPreview.TrimColor = enumItem Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                        End With
                        menu.AddItem(item)
                    Next
                Case EnumTypes.VehicleColorDashboard
                    enumArray = System.Enum.GetValues(GetType(VehicleColor))
                    For Each enumItem As VehicleColor In enumArray
                        item = New UIMenuItem(GetLocalizedColorName(enumItem))
                        With item
                            .Tag = enumItem
                            If VehPreview.DashboardColor = enumItem Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                        End With
                        menu.AddItem(item)
                    Next
                Case EnumTypes.VehicleColorRim
                    enumArray = System.Enum.GetValues(GetType(VehicleColor))
                    For Each enumItem As VehicleColor In enumArray
                        item = New UIMenuItem(GetLocalizedColorName(enumItem))
                        With item
                            .Tag = enumItem
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

    Public Sub VehicleSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        If selectedItem.Text = VehicleName Then 'If VehPreview.Exists() = True Then
            Dim t As Tuple(Of String, Integer, String, String) = selectedItem.Tag
            sender.Visible = False
            ConfirmMenu.Visible = True
            VehicleName = selectedItem.Text
            optLastVehMake = t.Item4
            ShowVehicleName = True
            RefreshRGBColorMenuFor(CPriColorMenu, New UIMenuItem("nothing"), "Primary")
            RefreshRGBColorMenuFor(CSecColorMenu, New UIMenuItem("nothing"), "Secondary")
            RefreshColorMenuFor(ClassicColorMenu, New UIMenuItem("nothing"), ClassicColor, "Primary")
            RefreshColorMenuFor(MetallicColorMenu, New UIMenuItem("nothing"), ClassicColor, "Primary")
            RefreshColorMenuFor(MetalColorMenu, New UIMenuItem("nothing"), MetalColor, "Primary")
            RefreshColorMenuFor(MatteColorMenu, New UIMenuItem("nothing"), MatteColor, "Primary")
            RefreshColorMenuFor(ChromeColorMenu, New UIMenuItem("nothing"), ChromeColor, "Primary")
            RefreshColorMenuFor(PeaColorMenu, New UIMenuItem("nothing"), PearlescentColor, "Pearlescent")
            RefreshColorMenuFor(ClassicColorMenu2, New UIMenuItem("nothing"), ClassicColor, "Secondary")
            RefreshColorMenuFor(MetallicColorMenu2, New UIMenuItem("nothing"), ClassicColor, "Secondary")
            RefreshColorMenuFor(MetalColorMenu2, New UIMenuItem("nothing"), MetalColor, "Secondary")
            RefreshColorMenuFor(MatteColorMenu2, New UIMenuItem("nothing"), MatteColor, "Secondary")
            RefreshColorMenuFor(ChromeColorMenu2, New UIMenuItem("nothing"), ChromeColor, "Secondary")
            RefreshEnumModMenuFor(PlateMenu, New UIMenuItem("nothing"), EnumTypes.NumberPlateType)
        Else
            Dim t As Tuple(Of String, Integer, String, String) = selectedItem.Tag
            SelectedVehicle = t.Item3
            If VehPreview = Nothing Then
                If Not selectedItem.Text.Contains("NULL") Then
                    If optFade = 1 Then
                        Game.FadeScreenOut(200)
                        Script.Wait(200)
                        VehPreview = CreateVehicle(t.Item1, VehPreviewPos, Radius)
                        Script.Wait(200)
                        Game.FadeScreenIn(200)
                    Else
                        VehPreview = CreateVehicle(t.Item1, VehPreviewPos, Radius)
                    End If
                End If
            Else
                VehPreview.Delete()
                If Not selectedItem.Text.Contains("NULL") Then
                    If optFade = 1 Then
                        Game.FadeScreenOut(200)
                        Script.Wait(200)
                        VehPreview = CreateVehicle(t.Item1, VehPreviewPos, Radius)
                        Script.Wait(200)
                        Game.FadeScreenIn(200)
                    Else
                        VehPreview = CreateVehicle(t.Item1, VehPreviewPos, Radius)
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
            optLastVehMake = t.Item4
            ShowVehicleName = True
            VehPreview.Heading = Radius
            VehPreview.IsDriveable = False
            VehPreview.LockStatus = VehicleLockStatus.CannotBeTriedToEnter
            VehPreview.DirtLevel = 0
            VehiclePrice = t.Item2
            optLastVehHash = VehPreview.Model.Hash
            optLastVehName = VehicleName
            config.SetValue(Of Integer)("SETTINGS", "LastVehHash", VehPreview.Model.Hash)
            config.SetValue(Of String)("SETTINGS", "LastVehName", VehicleName)
            config.Save()
        End If
    End Sub

    Public Sub VehicleChangeHandler(sender As UIMenu, index As Integer)
        Try
            Dim t As Tuple(Of String, Integer, String, String) = sender.MenuItems(index).Tag
            SelectedVehicle = t.Item3
            If VehPreview = Nothing Then
                If Not sender.MenuItems(index).Text.Contains("NULL") Then
                    If optFade = 1 Then
                        Game.FadeScreenOut(200)
                        Script.Wait(200)
                        VehPreview = CreateVehicle(t.Item1, VehPreviewPos, Radius)
                        Script.Wait(200)
                        Game.FadeScreenIn(200)
                    Else
                        VehPreview = CreateVehicle(t.Item1, VehPreviewPos, Radius)
                    End If
                End If
            Else
                VehPreview.Delete()
                If Not sender.MenuItems(index).Text.Contains("NULL") Then
                    If optFade = 1 Then
                        Game.FadeScreenOut(200)
                        Script.Wait(200)
                        VehPreview = CreateVehicle(t.Item1, VehPreviewPos, Radius)
                        Script.Wait(200)
                        Game.FadeScreenIn(200)
                    Else
                        VehPreview = CreateVehicle(t.Item1, VehPreviewPos, Radius)
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
            optLastVehMake = t.Item4
            ShowVehicleName = True
            VehPreview.Heading = Radius
            VehPreview.IsDriveable = False
            VehPreview.LockStatus = VehicleLockStatus.CannotBeTriedToEnter
            VehPreview.DirtLevel = 0
            VehiclePrice = t.Item2
            wsCamera.RepositionFor(VehPreview)
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

    Public Sub ItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        Try
            If selectedItem.Text = Game.GetGXTEntry("ITEM_YES") Then 'GetLangEntry("BTN_CONFIRM") Then
                If PlayerCash > VehiclePrice Then
                    Game.FadeScreenOut(200)
                    Script.Wait(200)
                    GP.Money = (PlayerCash - VehiclePrice)
                    ConfirmMenu.Visible = False
                    wsCamera.Stop()
                    DrawSpotLight = False
                    VehPreview.IsDriveable = True
                    VehPreview.LockStatus = VehicleLockStatus.Unlocked
                    Native.Function.Call(Hash.SET_VEHICLE_DOORS_SHUT, VehPreview, False)
                    VehPreview.Position = New Vector3(-56.79958, -1110.868, 26.43581)
                    Native.Function.Call(Hash.TASK_WARP_PED_INTO_VEHICLE, GPC, VehPreview, -1)
                    VehPreview.MarkAsNoLongerNeeded()
                    VehPreview = Nothing
                    HideHud = False
                    Script.Wait(200)
                    Game.FadeScreenIn(200)
                    Native.Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "PROPERTY_PURCHASE", "HUD_AWARDS", False)
                    BigMessageThread.MessageInstance.ShowWeaponPurchasedMessage("~y~" & GetLangEntry("VEHICLE_PURCHASED"), "~w~" & SelectedVehicle, Nothing)
                    SelectedVehicle = Nothing
                    VehicleName = Nothing
                    ShowVehicleName = False
                    TaskScriptStatus = -1
                Else
                    If Game.Player.Character.Name = "Franklin" Then
                        DisplayNotificationThisFrame(Game.GetGXTEntry("EMSTR_55"), "", Game.GetGXTEntry("PI_BIK_HX8"), "CHAR_BANK_FLEECA", True, IconType.RightJumpingArrow) 'GetLangEntry("FLEECA_BANK") GetLangEntry("INSUFFICIENT_FUNCS_BODY")
                    ElseIf Game.Player.Character.Name = "Trevor" Then
                        DisplayNotificationThisFrame(Game.GetGXTEntry("EMSTR_58"), "", Game.GetGXTEntry("PI_BIK_HX8"), "CHAR_BANK_BOL", True, IconType.RightJumpingArrow) 'GetLangEntry("BOL_BANK")
                    Else
                        DisplayNotificationThisFrame(Game.GetGXTEntry("EMSTR_52"), "", Game.GetGXTEntry("PI_BIK_HX8"), "CHAR_BANK_MAZE", True, IconType.RightJumpingArrow) 'GetLangEntry("MAZE_BANK")
                    End If
                End If
            ElseIf selectedItem.Text = GetLangEntry("BTN_TEST_DRIVE") Then
                Game.FadeScreenOut(200)
                Script.Wait(200)
                Native.Function.Call(Hash.TASK_WARP_PED_INTO_VEHICLE, GPC, VehPreview, -1)
                ConfirmMenu.Visible = False
                wsCamera.Stop()
                DrawSpotLight = False
                VehPreview.IsDriveable = True
                VehPreview.LockStatus = VehicleLockStatus.Unlocked
                Native.Function.Call(Hash.SET_VEHICLE_DOORS_SHUT, VehPreview, False)
                DisplayHelpTextThisFrame(GetLangEntry("HELP_TEST_DRIVE"))
                TestDrive = TestDrive + 1
                HideHud = False
                VehPreview.Position = New Vector3(-56.79958, -1110.868, 26.43581)
                Script.Wait(200)
                Game.FadeScreenIn(200)
                ShowVehicleName = False
            End If

            If selectedItem.Text = Game.GetGXTEntry("PERSO_MOD_PER") Then 'GetLangEntry("BTN_UPGRADE_NAME") Then
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
                Dim NumPlateText As String = Game.GetUserInput(VehPreview.NumberPlate, 9)
                If NumPlateText <> "" Then
                    VehPreview.NumberPlate = NumPlateText
                End If
            End If
        Catch ex As Exception
            logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Sub CategoryItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        Try
            Dim t As Tuple(Of Integer, String) = selectedItem.Tag
            CreateVehicleMenu($".\scripts\PremiumDeluxeMotorsport\Vehicles\{t.Item2}.ini", GetLangEntry(t.Item2))
            sender.Visible = Not sender.Visible
            VehicleMenu.Visible = Not VehicleMenu.Visible
            If t.Item1 > 10 Then
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

    Public Sub CreateVehicleMenu(File As String, Subtitle As String)
        Try
            Dim Format As New Reader(File, Parameters)
            VehicleMenu = VehicleMenu.NewUIMenu(Subtitle.ToUpper, True)
            For ii As Integer = 0 To Format.Count - 1
                Dim i As Integer = (Format.Count - 1) - ii
                Price = Format(i)("price")
                Dim item As New UIMenuItem($"{Game.GetGXTEntry(Format(i)("make"))} {Game.GetGXTEntry(Format(i)("gxt"))}")
                With item
                    If .Text.Contains("NULL") Then .Text = Game.GetGXTEntry(Format(i)("gxt"))
                    If .Text.Contains("NULL") Then .Text = Format(i)("name")
                    .SetRightLabel($"${Price.ToString("N0")}")
                    .Tag = New Tuple(Of String, Integer, String, String)(Format(i)("model"), Format(i)("price"), $"{Game.GetGXTEntry(Format(i)("make"))} {Game.GetGXTEntry(Format(i)("gxt"))}", Format(i)("make"))
                    Dim model As Model = New Model(Format(i)("model"))
                    If hiddenSave.GetValue(Of Integer)("VEHICLES", model.Hash, 0) = 0 Then
                        hiddenSave.SetValue(Of Integer)("VEHICLES", VehPreview.Model.Hash, 0)
                        .SetLeftBadge(UIMenuItem.BadgeStyle.Star)
                    End If
                End With
                Dim vmodel As Model = New Model(Format(i)("model"))
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

    Public Sub ModsMenuIndexChangedHandler(sender As UIMenu, index As Integer)
        Try
            If (sender Is ClassicColorMenu) Or (sender Is ChromeColorMenu) Or (sender Is MatteColorMenu) Or (sender Is MetalColorMenu) Then
                VehPreview.PrimaryColor = sender.MenuItems(index).Tag
            ElseIf sender Is MetallicColorMenu Then
                VehPreview.PrimaryColor = sender.MenuItems(index).Tag
                VehPreview.PearlescentColor = sender.MenuItems(index).Tag
            ElseIf sender Is PeaColorMenu Then
                VehPreview.PearlescentColor = sender.MenuItems(index).Tag
            ElseIf (sender Is ClassicColorMenu2) Or (sender Is ChromeColorMenu2) Or (sender Is MatteColorMenu2) Or (sender Is MetalColorMenu2) Then
                VehPreview.SecondaryColor = sender.MenuItems(index).Tag
            ElseIf sender Is MetallicColorMenu2 Then
                VehPreview.SecondaryColor = sender.MenuItems(index).Tag
                VehPreview.PearlescentColor = sender.MenuItems(index).Tag
            ElseIf sender Is CPriColorMenu Then
                VehPreview.CustomPrimaryColor = sender.MenuItems(index).Tag
            ElseIf sender Is CSecColorMenu Then
                VehPreview.CustomSecondaryColor = sender.MenuItems(index).Tag
            ElseIf sender Is PlateMenu Then
                VehPreview.NumberPlateType = sender.MenuItems(index).Tag
            End If

            If optRemoveColor = True Then
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

    Public Sub ModsMenuItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        Try
            For Each i As UIMenuItem In sender.MenuItems
                i.SetRightBadge(UIMenuItem.BadgeStyle.None)
            Next

            'Color
            If (sender Is ClassicColorMenu) Or (sender Is ChromeColorMenu) Or (sender Is MatteColorMenu) Or (sender Is MetalColorMenu) Then
                VehPreview.PrimaryColor = selectedItem.Tag
                selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                lastVehMemory.PrimaryColor = selectedItem.Tag
            ElseIf sender Is MetallicColorMenu Then
                VehPreview.PrimaryColor = selectedItem.Tag
                VehPreview.PearlescentColor = selectedItem.Tag
                selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                lastVehMemory.PrimaryColor = selectedItem.Tag
                lastVehMemory.PearlescentColor = selectedItem.Tag
            ElseIf sender Is PeaColorMenu Then
                VehPreview.PearlescentColor = selectedItem.Tag
                selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                lastVehMemory.PearlescentColor = selectedItem.Tag
            ElseIf (sender Is ClassicColorMenu2) Or (sender Is ChromeColorMenu2) Or (sender Is MatteColorMenu2) Or (sender Is MetalColorMenu2) Then
                VehPreview.SecondaryColor = selectedItem.Tag
                selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                lastVehMemory.SecondaryColor = selectedItem.Tag
            ElseIf sender Is MetallicColorMenu2 Then
                VehPreview.SecondaryColor = selectedItem.Tag
                VehPreview.PearlescentColor = selectedItem.Tag
                selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                lastVehMemory.SecondaryColor = selectedItem.Tag
                lastVehMemory.PearlescentColor = selectedItem.Tag
            ElseIf sender Is CPriColorMenu Then
                VehPreview.CustomPrimaryColor = selectedItem.Tag
                selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                lastVehMemory.CustomPrimaryColor = selectedItem.Tag
            ElseIf sender Is CSecColorMenu Then
                VehPreview.CustomSecondaryColor = selectedItem.Tag
                selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                lastVehMemory.CustomSecondaryColor = selectedItem.Tag
            ElseIf sender Is PlateMenu Then
                VehPreview.NumberPlateType = selectedItem.Tag
                selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                lastVehMemory.NumberPlate = selectedItem.Tag
            End If

            If optRemoveColor = True Then
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

    Public Sub ModsMenuCloseHandler(sender As UIMenu)
        Try
            VehPreview.PrimaryColor = lastVehMemory.PrimaryColor
            VehPreview.SecondaryColor = lastVehMemory.SecondaryColor
            VehPreview.PearlescentColor = lastVehMemory.PearlescentColor
            VehPreview.NumberPlateType = lastVehMemory.NumberPlate

            If optRemoveColor = True Then
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

End Module
