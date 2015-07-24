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
    Private playerPed As Ped
    Private simeon As Ped

    Private mainMenu As UIMenu
    Private motorMenu As UIMenu
    Private compactMenu As UIMenu
    Private coupeMenu As UIMenu
    Private sedanMenu As UIMenu
    Private sportMenu As UIMenu
    Private classicMenu As UIMenu
    Private exoticMenu As UIMenu
    Private muscleMenu As UIMenu
    Private offroadMenu As UIMenu
    Private suvMenu As UIMenu
    Private vanMenu As UIMenu
    Private utilityMenu As UIMenu
    Private armouredMenu As UIMenu

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
    Private parameters As String() = {"[name]", "[hash]", "[price]", "[price2]", "[model]"}

    Private _menuPool As MenuPool

    Public Sub ReadMotorcycle()
        Dim format As New BTEFormatReader(motorcycle, parameters)
        Dim qty As Integer = format.Count - 1

        motorMenu = New UIMenu("PDM Car Shop", "~r~MOTORCYCLES")
        _menuPool.Add(motorMenu)
        For i As Integer = 0 To format.Count - 1
            Dim item As New UIMenuItem(format(i)("name") & " $" & format(i)("price"))
            motorMenu.AddItem(item)
            With item
                .Hash = format(i)("hash")
                .Model = format(i)("model")
                .Price = format(i)("price")
            End With
        Next
        motorMenu.RefreshIndex()
        mainMenu.BindMenuToItem(motorMenu, itemMotor)
    End Sub

    Public Sub ReadCompact()
        Dim format As New BTEFormatReader(compact, parameters)
        Dim qty As Integer = format.Count - 1

        compactMenu = New UIMenu("PDM Car Shop", "~r~COMPACTS")
        _menuPool.Add(compactMenu)
        For i As Integer = 0 To format.Count - 1
            Dim item As New UIMenuItem(format(i)("name") & " $" & format(i)("price"))
            compactMenu.AddItem(item)
            With item
                .Hash = format(i)("hash")
                .Model = format(i)("model")
                .Price = format(i)("price")
            End With
        Next
        compactMenu.RefreshIndex()
        mainMenu.BindMenuToItem(compactMenu, itemCompact)
    End Sub

    Public Sub ReadCoupe()
        Dim format As New BTEFormatReader(coupe, parameters)
        Dim qty As Integer = format.Count - 1

        coupeMenu = New UIMenu("PDM Car Shop", "~r~COUPES")
        _menuPool.Add(coupeMenu)
        For i As Integer = 0 To format.Count - 1
            Dim item As New UIMenuItem(format(i)("name") & " $" & format(i)("price"))
            coupeMenu.AddItem(item)
            With item
                .Hash = format(i)("hash")
                .Model = format(i)("model")
                .Price = format(i)("price")
            End With
        Next
        coupeMenu.RefreshIndex()
        mainMenu.BindMenuToItem(coupeMenu, itemCoupe)
    End Sub

    Public Sub ReadSedan()
        Dim format As New BTEFormatReader(sedan, parameters)
        Dim qty As Integer = format.Count - 1

        sedanMenu = New UIMenu("PDM Car Shop", "~r~SEDANS")
        _menuPool.Add(sedanMenu)
        For i As Integer = 0 To format.Count - 1
            Dim item As New UIMenuItem(format(i)("name") & " $" & format(i)("price"))
            sedanMenu.AddItem(item)
            With item
                .Hash = format(i)("hash")
                .Model = format(i)("model")
                .Price = format(i)("price")
            End With
        Next
        sedanMenu.RefreshIndex()
        mainMenu.BindMenuToItem(sedanMenu, itemSedan)
    End Sub

    Public Sub ReadSport()
        Dim format As New BTEFormatReader(sport, parameters)
        Dim qty As Integer = format.Count - 1

        sportMenu = New UIMenu("PDM Car Shop", "~r~SPORTS")
        _menuPool.Add(sportMenu)
        For i As Integer = 0 To format.Count - 1
            Dim item As New UIMenuItem(format(i)("name") & " $" & format(i)("price"))
            sportMenu.AddItem(item)
            With item
                .Hash = format(i)("hash")
                .Model = format(i)("model")
                .Price = format(i)("price")
            End With
        Next
        sportMenu.RefreshIndex()
        mainMenu.BindMenuToItem(sportMenu, itemSport)
    End Sub

    Public Sub ReadClassic()
        Dim format As New BTEFormatReader(classic, parameters)
        Dim qty As Integer = format.Count - 1

        classicMenu = New UIMenu("PDM Car Shop", "~r~CLASSICS")
        _menuPool.Add(classicMenu)
        For i As Integer = 0 To format.Count - 1
            Dim item As New UIMenuItem(format(i)("name") & " $" & format(i)("price"))
            classicMenu.AddItem(item)
            With item
                .Hash = format(i)("hash")
                .Model = format(i)("model")
                .Price = format(i)("price")
            End With
        Next
        classicMenu.RefreshIndex()
        mainMenu.BindMenuToItem(classicMenu, itemClassic)
    End Sub

    Public Sub ReadExotic()
        Dim format As New BTEFormatReader(exotic, parameters)
        Dim qty As Integer = format.Count - 1

        exoticMenu = New UIMenu("PDM Car Shop", "~r~EXOTICS")
        _menuPool.Add(exoticMenu)
        For i As Integer = 0 To format.Count - 1
            Dim item As New UIMenuItem(format(i)("name") & " $" & format(i)("price"))
            exoticMenu.AddItem(item)
            With item
                .Hash = format(i)("hash")
                .Model = format(i)("model")
                .Price = format(i)("price")
            End With
        Next
        exoticMenu.RefreshIndex()
        mainMenu.BindMenuToItem(exoticMenu, itemExotic)
    End Sub

    Public Sub ReadMuscle()
        Dim format As New BTEFormatReader(muscle, parameters)
        Dim qty As Integer = format.Count - 1

        muscleMenu = New UIMenu("PDM Car Shop", "~r~MUSCLES")
        _menuPool.Add(muscleMenu)
        For i As Integer = 0 To format.Count - 1
            Dim item As New UIMenuItem(format(i)("name") & " $" & format(i)("price"))
            muscleMenu.AddItem(item)
            With item
                .Hash = format(i)("hash")
                .Model = format(i)("model")
                .Price = format(i)("price")
            End With
        Next
        muscleMenu.RefreshIndex()
        mainMenu.BindMenuToItem(muscleMenu, itemMuscle)
    End Sub

    Public Sub ReadOffroad()
        Dim format As New BTEFormatReader(offroad, parameters)
        Dim qty As Integer = format.Count - 1

        offroadMenu = New UIMenu("PDM Car Shop", "~r~OFF-ROAD")
        _menuPool.Add(offroadMenu)
        For i As Integer = 0 To format.Count - 1
            Dim item As New UIMenuItem(format(i)("name") & " $" & format(i)("price"))
            offroadMenu.AddItem(item)
            With item
                .Hash = format(i)("hash")
                .Model = format(i)("model")
                .Price = format(i)("price")
            End With
        Next
        offroadMenu.RefreshIndex()
        mainMenu.BindMenuToItem(offroadMenu, itemOffRoad)
    End Sub

    Public Sub ReadSuv()
        Dim format As New BTEFormatReader(suv, parameters)
        Dim qty As Integer = format.Count - 1

        suvMenu = New UIMenu("PDM Car Shop", "~r~SUVS")
        _menuPool.Add(suvMenu)
        For i As Integer = 0 To format.Count - 1
            Dim item As New UIMenuItem(format(i)("name") & " $" & format(i)("price"))
            suvMenu.AddItem(item)
            With item
                .Hash = format(i)("hash")
                .Model = format(i)("model")
                .Price = format(i)("price")
            End With
        Next
        suvMenu.RefreshIndex()
        mainMenu.BindMenuToItem(suvMenu, itemSuv)
    End Sub

    Public Sub ReadVan()
        Dim format As New BTEFormatReader(van, parameters)
        Dim qty As Integer = format.Count - 1

        vanMenu = New UIMenu("PDM Car Shop", "~r~VANS")
        _menuPool.Add(vanMenu)
        For i As Integer = 0 To format.Count - 1
            Dim item As New UIMenuItem(format(i)("name") & " $" & format(i)("price"))
            vanMenu.AddItem(item)
            With item
                .Hash = format(i)("hash")
                .Model = format(i)("model")
                .Price = format(i)("price")
            End With
        Next
        vanMenu.RefreshIndex()
        mainMenu.BindMenuToItem(vanMenu, itemVan)
    End Sub

    Public Sub ReadUtility()
        Dim format As New BTEFormatReader(utility, parameters)
        Dim qty As Integer = format.Count - 1

        utilityMenu = New UIMenu("PDM Car Shop", "~r~UTILITIES")
        _menuPool.Add(utilityMenu)
        For i As Integer = 0 To format.Count - 1
            Dim item As New UIMenuItem(format(i)("name") & " $" & format(i)("price"))
            utilityMenu.AddItem(item)
            With item
                .Hash = format(i)("hash")
                .Model = format(i)("model")
                .Price = format(i)("price")
            End With
        Next
        utilityMenu.RefreshIndex()
        mainMenu.BindMenuToItem(utilityMenu, itemUtility)
    End Sub

    Public Sub ReadArmoured()
        Dim format As New BTEFormatReader(armoured, parameters)
        _menuPool.Add(armouredMenu)
        Dim qty As Integer = format.Count - 1

        armouredMenu = New UIMenu("PDM Car Shop", "~r~ARMOURED")
        For i As Integer = 0 To format.Count - 1
            Dim item As New UIMenuItem(format(i)("name") & " $" & format(i)("price"))
            armouredMenu.AddItem(item)
            With item
                .Hash = format(i)("hash")
                .Model = format(i)("model")
                .Price = format(i)("price")
            End With
        Next
        armouredMenu.RefreshIndex()
        mainMenu.BindMenuToItem(armouredMenu, itemArmoured)
    End Sub

    Public Sub New()
        AddHandler Tick, AddressOf OnTick
        AddHandler KeyDown, AddressOf OnKeyDown
        _menuPool = New MenuPool()

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

        AddHandler motorMenu.OnIndexChange, AddressOf CategoryItemChange
        AddHandler compactMenu.OnIndexChange, AddressOf CategoryItemChange
        AddHandler coupeMenu.OnIndexChange, AddressOf CategoryItemChange
        AddHandler sedanMenu.OnIndexChange, AddressOf CategoryItemChange
        AddHandler sportMenu.OnIndexChange, AddressOf CategoryItemChange
        AddHandler classicMenu.OnIndexChange, AddressOf CategoryItemChange
        AddHandler exoticMenu.OnIndexChange, AddressOf CategoryItemChange
        AddHandler muscleMenu.OnIndexChange, AddressOf CategoryItemChange
        AddHandler offroadMenu.OnIndexChange, AddressOf CategoryItemChange
        AddHandler suvMenu.OnIndexChange, AddressOf CategoryItemChange
        AddHandler vanMenu.OnIndexChange, AddressOf CategoryItemChange
        AddHandler utilityMenu.OnIndexChange, AddressOf CategoryItemChange
        AddHandler armouredMenu.OnIndexChange, AddressOf CategoryItemChange
    End Sub

    Public Sub ItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        UI.Notify("You have selected: ~b~" + selectedItem.Text)
    End Sub

    Public Sub CategoryItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        Dim nl = Environment.NewLine
        UI.Notify("Name: ~b~" & selectedItem.Text & nl & "~s~Model: ~r~" & selectedItem.Model & nl & "~s~Hash: ~y~" & selectedItem.Hash & nl & "~s~Price: ~g~" & selectedItem.Price)
    End Sub

    Public Sub CategoryItemChange(sender As UIMenu, index As Integer)
        Dim nl = Environment.NewLine
        UI.ShowSubtitle("Name: ~b~" & sender.MenuItems(index).Text & nl & "~s~Model: ~r~" & sender.MenuItems(index).Model & nl & "~s~Hash: ~y~" & sender.MenuItems(index).Hash & nl & "~s~Price: ~g~" & sender.MenuItems(index).Price)
    End Sub

    Public Sub OnTick(o As Object, e As EventArgs)
        _menuPool.ProcessMenus()
    End Sub

    Public Sub OnKeyDown(o As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.F9 Then
            mainMenu.Visible = Not mainMenu.Visible
        End If
    End Sub
End Class
