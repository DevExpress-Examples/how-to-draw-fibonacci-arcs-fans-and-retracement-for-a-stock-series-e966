﻿Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraCharts

Namespace FibonacciIndicators
    Partial Public Class Form1
        Inherits Form

        Public Sub New()
            InitializeComponent()
        End Sub

        #Region "Initialization"
        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            ' Create a series.
            Dim series As New Series("Stock", ViewType.Stock)
            series.ArgumentScaleType = ScaleType.DateTime

            ' Populate the series with data.
            AddPoints(series)

            ' Add it to the chart.
            Me.chartControl1.Series.Add(series)

            ' Adjust the chart's appearance.
            Dim diagram As XYDiagram = CType(Me.chartControl1.Diagram, XYDiagram)
            diagram.AxisY.WholeRange.AlwaysShowZeroLevel = False
            diagram.AxisX.Label.Staggered = True
        End Sub
        #End Region

        #Region "Providing Data"
        Private Sub AddPoints(ByVal series As Series)
            series.Points.AddRange(New SeriesPoint() { _
                New SeriesPoint(New Date(2007, 12, 24), New Object() {110.55, 112, 111.3, 111.65}), _
                New SeriesPoint(New Date(2007, 12, 26), New Object() {110.9, 112.09, 110.9, 111.56}), _
                New SeriesPoint(New Date(2007, 12, 27), New Object() {109.49, 111.3, 110.53, 109.6}), _
                New SeriesPoint(New Date(2007, 12, 28), New Object() {109.108, 110.76, 110.76, 110.09}), _
                New SeriesPoint(New Date(2007, 12, 31), New Object() {107.26, 110, 109.51, 108.1}), _
                New SeriesPoint(New Date(2008, 1, 2), New Object() {104.17, 108.99, 108.99, 104.69}), _
                New SeriesPoint(New Date(2008, 1, 3), New Object() {103.98, 105.57, 104.83, 104.9}), _
                New SeriesPoint(New Date(2008, 1, 4), New Object() {100.48, 103.95, 103.95, 101.13}), _
                New SeriesPoint(New Date(2008, 1, 7), New Object() {99.03, 101, 100.25, 100.05}), _
                New SeriesPoint(New Date(2008, 1, 8), New Object() {97.17, 100.38, 100.05, 97.59}), _
                New SeriesPoint(New Date(2008, 1, 9), New Object() {97.16, 99.15, 97.76, 98.31}), _
                New SeriesPoint(New Date(2008, 1, 10), New Object() {97.15, 100.86, 97.39, 99.92}), _
                New SeriesPoint(New Date(2008, 1, 11), New Object() {97.09, 99.46, 99.2, 97.67}), _
                New SeriesPoint(New Date(2008, 1, 14), New Object() {101.33, 105.59, 105.01, 102.93}), _
                New SeriesPoint(New Date(2008, 1, 15), New Object() {101.23, 104.04, 102.03, 101.83}), _
                New SeriesPoint(New Date(2008, 1, 16), New Object() {100.14, 102.86, 100.14, 101.63}), _
                New SeriesPoint(New Date(2008, 1, 17), New Object() {100.05, 103.45, 102, 101.1}), _
                New SeriesPoint(New Date(2008, 1, 18), New Object() {102.5, 106.72, 106.72, 103.4}), _
                New SeriesPoint(New Date(2008, 1, 22), New Object() {98.55, 103.09, 98.55, 101.22}), _
                New SeriesPoint(New Date(2008, 1, 23), New Object() {98.5, 106.335, 99.63, 106.1}), _
                New SeriesPoint(New Date(2008, 1, 24), New Object() {104.68, 107.51, 106.38, 106.91}), _
                New SeriesPoint(New Date(2008, 1, 25), New Object() {104.1, 107.79, 107.79, 104.52}), _
                New SeriesPoint(New Date(2008, 1, 28), New Object() {103.83, 105.77, 104.44, 104.98}), _
                New SeriesPoint(New Date(2008, 1, 29), New Object() {104.6, 106.8, 105.5, 106.1}), _
                New SeriesPoint(New Date(2008, 1, 30), New Object() {104.855, 107.65, 105.85, 105.65}), _
                New SeriesPoint(New Date(2008, 1, 31), New Object() {103.7, 107.97, 104.21, 107.11}) _
            })
        End Sub
        #End Region

        #Region "Processing Button_Click"
        Private Sub btnArcs_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnArcs.Click
            AddIndicator(chartControl1.Series(0), FibonacciIndicatorKind.FibonacciArcs)
        End Sub

        Private Sub btnFans_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFans.Click
            AddIndicator(chartControl1.Series(0), FibonacciIndicatorKind.FibonacciFans)
        End Sub

        Private Sub btnRetracements_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRetracements.Click
            AddIndicator(chartControl1.Series(0), FibonacciIndicatorKind.FibonacciRetracement)
        End Sub
        #End Region

        Private Sub AddIndicator(ByVal ser As Series, ByVal kind As FibonacciIndicatorKind)
            ' Get a collection of indicators.
            Dim indicators As IndicatorCollection = CType(ser.View, StockSeriesView).Indicators

            ' Clear it.
            indicators.Clear()

            ' Add a new indicator to it.
            indicators.Add(CreateIndicator(kind, New Date(2007, 12, 27), New Date(2008, 1, 17), ValueLevel.High))
        End Sub

        Private Function CreateIndicator(ByVal kind As FibonacciIndicatorKind, ByVal arg1 As Date, ByVal arg2 As Date, ByVal level As ValueLevel) As FibonacciIndicator

            ' Create the Fibonacci Indicator of the specified kind.
            Dim fi As New FibonacciIndicator(kind)

            ' Specify its start and end points.
            fi.Point1.Argument = arg1
            fi.Point1.ValueLevel = level
            fi.Point2.Argument = arg2
            fi.Point2.ValueLevel = level

            ' Select the name.
            Select Case fi.Kind
                Case FibonacciIndicatorKind.FibonacciArcs
                        fi.Name = "Arcs"
                        Exit Select
                Case FibonacciIndicatorKind.FibonacciFans
                        fi.Name = "Fans"
                        Exit Select
                Case FibonacciIndicatorKind.FibonacciRetracement
                        fi.Name = "Retracement"
                        Exit Select
            End Select

            ' Make all its levels visible.
            fi.ShowLevel23_6 = True
            fi.ShowLevel76_4 = True
            fi.ShowLevel0 = True
            fi.ShowLevel100 = True
            fi.ShowAdditionalLevels = True

            ' Customize its appearance.
            fi.BaseLevelColor = Color.Green
            fi.BaseLevelLineStyle.DashStyle = DashStyle.Dash

            Return fi
        End Function
    End Class
End Namespace