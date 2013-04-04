Imports System.ComponentModel
Imports System.Text


Imports System.Windows.Forms.Integration
Imports MyControls

Namespace WFHost
   Partial Public Class Form1
      Inherits Form

      ' >>> [[ DFPHD :: 2013-04-03 ]] PERFORMANCE IMPROVEMENT
      ' <<< use this static/shared method to improve performance
      ' <<< static/shared determination of IsDebugEnabled()
      ' <<< compiled by the JIT *ONCE*, perhaps even optimized out
      Public Shared logger As log4net.ILog

      Private ctrlHost As ElementHost

      ' >>> [[ DFPHD :: 2013-04-03 ]] NOT SURE WHY THIS TYPE/NAMESPACE IS NOT RECOGNIZED
      ' <<< the type is MyControls.CustomControl1
      ' <<< this is exactly the type defiend in the MyControls DLL/Library
      Private wpfAddressCtrl As MyControls.CustomControl1

      Private initFontWeight As System.Windows.FontWeight
      Private initFontSize As Double
      Private initFontStyle As System.Windows.FontStyle
      Private initBackBrush As System.Windows.Media.SolidColorBrush
      Private initForeBrush As System.Windows.Media.SolidColorBrush
      Private initFontFamily As System.Windows.Media.FontFamily

      Public Sub New()
         InitializeComponent()
      End Sub

      Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

         Try
            ' Get logger named in config file
            logger = log4net.LogManager.GetLogger("WFHostLogger")
            logger.Info("Method Form1_load() - Method Entry")
         Catch ex As Exception
            logger.Error("Method Form1_load() - EXCEPTION THROWN!")
            logger.Error("Exception Message: " + ex.Message)
         Finally
            logger.Info("Method Form1_load() - Finally")
         End Try

         logger.Info("Method Form1_load() - NEW WPF ElementHost ...")
         ctrlHost = New ElementHost()
         ctrlHost.Dock = DockStyle.Fill

         logger.Info("Method Form1_load() - Panel1 . Controls . Add( ElementHost ) ...")
         panel1.Controls.Add(ctrlHost)

         logger.Info("Method Form1_load() - NEW WPF CONTROL (MyControls . CustomControl1 ...")
         wpfAddressCtrl = New MyControls.CustomControl1()

         logger.Info("Method Form1_load() - WPF Control :: InitializeComponent() ...")
         wpfAddressCtrl.InitializeComponent()

         logger.Info("Method Form1_load() - WPF Control :: control host . Child = WPF Control ...")
         ctrlHost.Child = wpfAddressCtrl

         logger.Info("Method Form1_load() - WPF Control :: AddHandlers ONBUTTONCLICK ...")
         AddHandler wpfAddressCtrl.OnButtonClick, AddressOf avAddressCtrl_OnButtonClick

         logger.Info("Method Form1_load() - WPF Control :: AddHandlers LOADED ...")
         AddHandler wpfAddressCtrl.Loaded, AddressOf avAddressCtrl_Loaded

      End Sub

      Private Sub avAddressCtrl_Loaded(ByVal sender As Object, ByVal e As EventArgs)
         Dim mName As String = System.Reflection.MethodInfo.GetCurrentMethod().Name
         logger.Debug("Method(" + mName + ") - Method Entry ...")
         initBackBrush = CType(wpfAddressCtrl.MyControl_Background, System.Windows.Media.SolidColorBrush)
         initForeBrush = wpfAddressCtrl.MyControl_Foreground
         initFontFamily = wpfAddressCtrl.MyControl_FontFamily
         initFontSize = wpfAddressCtrl.MyControl_FontSize
         initFontWeight = wpfAddressCtrl.MyControl_FontWeight
         initFontStyle = wpfAddressCtrl.MyControl_FontStyle
         logger.Info("Method(" + mName + ") - Method Exit")
      End Sub

      Private Sub avAddressCtrl_OnButtonClick(ByVal sender As Object, ByVal args As MyControls.MyControlEventArgs)
         Dim mName As String = System.Reflection.MethodInfo.GetCurrentMethod().Name
         logger.Debug("Method(" + mName + ") - Method Entry ...")
         If args.IsOK Then
            logger.Debug("ARGS.ISOK ...")
            logger.Debug("ARGS.MyName = [" + args.MyName + "]")
            logger.Debug("ARGS.MyStreetAddress = [" + args.MyStreetAddress + "]")
            logger.Debug("ARGS.MyCity = [" + args.MyCity + "]")
            logger.Debug("ARGS.MyState = [" + args.MyState + "]")
            logger.Debug("ARGS.MyZip = [" + args.MyZip + "]")
            lblAddress.Text = "Street Address: " & args.MyStreetAddress
            lblCity.Text = "City: " & args.MyCity
            lblName.Text = "Name: " & args.MyName
            lblState.Text = "State: " & args.MyState
            lblZip.Text = "Zip: " & args.MyZip
         Else
            logger.Debug("ARGS.CANCEL ...")
            lblAddress.Text = "Street Address: "
            lblCity.Text = "City: "
            lblName.Text = "Name: "
            lblState.Text = "State: "
            lblZip.Text = "Zip: "
         End If
         logger.Info("Method(" + mName + ") - Method Exit")
      End Sub

      Private Sub radioBackgroundOriginal_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles radioBackgroundOriginal.CheckedChanged
         Dim mName As String = System.Reflection.MethodInfo.GetCurrentMethod().Name
         logger.Debug("Method(" + mName + ") - Method Entry ...")
         wpfAddressCtrl.MyControl_Background = initBackBrush
         logger.Info("Method(" + mName + ") - Method Exit")
      End Sub

      Private Sub radioBackgroundLightGreen_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles radioBackgroundLightGreen.CheckedChanged
         Dim mName As String = System.Reflection.MethodInfo.GetCurrentMethod().Name
         logger.Debug("Method(" + mName + ") - Method Entry ...")
         wpfAddressCtrl.MyControl_Background = New System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.LightGreen)
         logger.Info("Method(" + mName + ") - Method Exit")
      End Sub

      Private Sub radioBackgroundLightSalmon_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles radioBackgroundLightSalmon.CheckedChanged
         Dim mName As String = System.Reflection.MethodInfo.GetCurrentMethod().Name
         logger.Debug("Method(" + mName + ") - Method Entry ...")
         wpfAddressCtrl.MyControl_Background = New System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.LightSalmon)
         logger.Info("Method(" + mName + ") - Method Exit")
      End Sub

      Private Sub radioForegroundOriginal_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles radioForegroundOriginal.CheckedChanged
         Dim mName As String = System.Reflection.MethodInfo.GetCurrentMethod().Name
         logger.Debug("Method(" + mName + ") - Method Entry ...")
         wpfAddressCtrl.MyControl_Foreground = initForeBrush
         logger.Info("Method(" + mName + ") - Method Exit")
      End Sub

      Private Sub radioForegroundRed_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles radioForegroundRed.CheckedChanged
         Dim mName As String = System.Reflection.MethodInfo.GetCurrentMethod().Name
         logger.Debug("Method(" + mName + ") - Method Entry ...")
         wpfAddressCtrl.MyControl_Foreground = New System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red)
         logger.Info("Method(" + mName + ") - Method Exit")
      End Sub

      Private Sub radioForegroundYellow_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles radioForegroundYellow.CheckedChanged
         Dim mName As String = System.Reflection.MethodInfo.GetCurrentMethod().Name
         logger.Debug("Method(" + mName + ") - Method Entry ...")
         wpfAddressCtrl.MyControl_Foreground = New System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Yellow)
         logger.Info("Method(" + mName + ") - Method Exit")
      End Sub

      Private Sub radioFamilyOriginal_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles radioFamilyOriginal.CheckedChanged
         Dim mName As String = System.Reflection.MethodInfo.GetCurrentMethod().Name
         logger.Debug("Method(" + mName + ") - Method Entry ...")
         wpfAddressCtrl.MyControl_FontFamily = initFontFamily
         logger.Info("Method(" + mName + ") - Method Exit")
      End Sub

      Private Sub radioFamilyTimes_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles radioFamilyTimes.CheckedChanged
         Dim mName As String = System.Reflection.MethodInfo.GetCurrentMethod().Name
         logger.Debug("Method(" + mName + ") - Method Entry ...")
         wpfAddressCtrl.MyControl_FontFamily = New System.Windows.Media.FontFamily("Times New Roman")
         logger.Info("Method(" + mName + ") - Method Exit")
      End Sub

      Private Sub radioFamilyWingDings_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles radioFamilyWingDings.CheckedChanged
         Dim mName As String = System.Reflection.MethodInfo.GetCurrentMethod().Name
         logger.Debug("Method(" + mName + ") - Method Entry ...")
         wpfAddressCtrl.MyControl_FontFamily = New System.Windows.Media.FontFamily("WingDings")
         logger.Info("Method(" + mName + ") - Method Exit")
      End Sub

      Private Sub radioSizeOriginal_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles radioSizeOriginal.CheckedChanged
         Dim mName As String = System.Reflection.MethodInfo.GetCurrentMethod().Name
         logger.Debug("Method(" + mName + ") - Method Entry ...")
         wpfAddressCtrl.MyControl_FontSize = initFontSize
         logger.Info("Method(" + mName + ") - Method Exit")
      End Sub

      Private Sub radioSizeTen_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles radioSizeTen.CheckedChanged
         Dim mName As String = System.Reflection.MethodInfo.GetCurrentMethod().Name
         logger.Debug("Method(" + mName + ") - Method Entry ...")
         wpfAddressCtrl.MyControl_FontSize = 10
         logger.Info("Method(" + mName + ") - Method Exit")
      End Sub

      Private Sub radioSizeTwelve_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles radioSizeTwelve.CheckedChanged
         Dim mName As String = System.Reflection.MethodInfo.GetCurrentMethod().Name
         logger.Debug("Method(" + mName + ") - Method Entry ...")
         wpfAddressCtrl.MyControl_FontSize = 12
         logger.Info("Method(" + mName + ") - Method Exit")
      End Sub

      Private Sub radioStyleOriginal_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles radioStyleOriginal.CheckedChanged
         Dim mName As String = System.Reflection.MethodInfo.GetCurrentMethod().Name
         logger.Debug("Method(" + mName + ") - Method Entry ...")
         wpfAddressCtrl.MyControl_FontStyle = initFontStyle
         logger.Info("Method(" + mName + ") - Method Exit")
      End Sub

      Private Sub radioStyleItalic_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles radioStyleItalic.CheckedChanged
         Dim mName As String = System.Reflection.MethodInfo.GetCurrentMethod().Name
         logger.Debug("Method(" + mName + ") - Method Entry ...")
         wpfAddressCtrl.MyControl_FontStyle = System.Windows.FontStyles.Italic
         logger.Info("Method(" + mName + ") - Method Exit")
      End Sub

      Private Sub radioWeightOriginal_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles radioWeightOriginal.CheckedChanged
         Dim mName As String = System.Reflection.MethodInfo.GetCurrentMethod().Name
         logger.Debug("Method(" + mName + ") - Method Entry ...")
         wpfAddressCtrl.MyControl_FontWeight = initFontWeight
         logger.Info("Method(" + mName + ") - Method Exit")
      End Sub

      Private Sub radioWeightBold_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles radioWeightBold.CheckedChanged
         Dim mName As String = System.Reflection.MethodInfo.GetCurrentMethod().Name
         logger.Debug("Method(" + mName + ") - Method Entry ...")
         wpfAddressCtrl.MyControl_FontWeight = System.Windows.FontWeights.Bold
         logger.Info("Method(" + mName + ") - Method Exit")
      End Sub

   End Class
End Namespace
