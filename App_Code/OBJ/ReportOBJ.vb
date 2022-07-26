Imports Microsoft.VisualBasic
Imports System.Data

Public Class ReportOBJ
    Private RptRQN1 As String = ""
    Private RptRQN2 As String = "ZZZZZZZZZZZZZZZZ"
    Private RptFactory As String = ""
    Private RptSection As String = ""
    Private RptReqBy As String = ""
    Private RptVend As String = ""
    Private RptDate1 As Date = New Date(Year(Now) - 1, 1, 1)
    Private RptDate2 As Date = Now
    Private RptSite As String = ""

    Public Sub Report()

    End Sub

    Public Sub Report(ByVal dr As DataRow)
        'RptRQN1 = dr("").ToString
        'RptRQN2 = dr("").ToString
        'RptFactory = dr("").ToString
        'RptSection = dr("").ToString
        'RptReqBy = dr("").ToString
        'RptDate1 = dr("").ToString
        'RptDate2 = dr("").ToString
    End Sub

    Public Property ReportRequisitionFrom() As String
        Get
            Return RptRQN1
        End Get
        Set(ByVal value As String)
            RptRQN1 = value
        End Set
    End Property

    Public Property ReportRequisitionTo() As String
        Get
            Return RptRQN2
        End Get
        Set(ByVal value As String)
            RptRQN2 = value
        End Set
    End Property

    Public Property ReportFactory() As String
        Get
            Return RptFactory
        End Get
        Set(ByVal value As String)
            RptFactory = value
        End Set
    End Property

    Public Property ReportSection() As String
        Get
            Return RptSection
        End Get
        Set(ByVal value As String)
            RptSection = value
        End Set
    End Property

    Public Property ReportRequestBy() As String
        Get
            Return RptReqBy
        End Get
        Set(ByVal value As String)
            RptReqBy = value
        End Set
    End Property

    Public Property ReportVendorName() As String
        Get
            Return RptVend
        End Get
        Set(ByVal value As String)
            RptVend = value
        End Set
    End Property

    Public Property ReportDateFrom() As Date
        Get
            Return RptDate1
        End Get
        Set(ByVal value As Date)
            RptDate1 = value
        End Set
    End Property

    Public Property ReportDateTo() As Date
        Get
            Return RptDate2
        End Get
        Set(ByVal value As Date)
            RptDate2 = value
        End Set
    End Property

    Public Property ReportSite() As String
        Get
            Return RptSite
        End Get
        Set(ByVal value As String)
            RptSite = value
        End Set
    End Property

End Class
