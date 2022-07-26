Imports Microsoft.VisualBasic
Imports System.Data

Public Class RulesOBJ
    Private boolEditEmployee As Boolean
    Private boolReadEmployee As Boolean
    Private boolReport As Boolean
    Private boolEditGroup As Boolean
    Private boolEditUser As Boolean
    Private boolMailSetting As Boolean

    Public Sub RulesOBJ(ByVal dr As DataRow)
        boolEditEmployee = dr("rEditEmployee").ToString
        boolReadEmployee = dr("rReadEmployee").ToString
        boolReport = dr("rReport").ToString
        boolEditGroup = dr("rEditGroup").ToString
        boolEditUser = dr("rEditUser").ToString
        boolMailSetting = dr("rEditMail").ToString
    End Sub

    Public Property EditEmployee() As Boolean
        Get
            Return boolEditEmployee
        End Get
        Set(ByVal value As Boolean)
            boolEditEmployee = value
        End Set
    End Property

    Public Property ReadEmployee() As Boolean
        Get
            Return boolReadEmployee
        End Get
        Set(ByVal value As Boolean)
            boolReadEmployee = value
        End Set
    End Property

    Public Property Report() As Boolean
        Get
            Return boolReport
        End Get
        Set(ByVal value As Boolean)
            boolReport = value
        End Set
    End Property

    Public Property EditGroup() As Boolean
        Get
            Return boolEditGroup
        End Get
        Set(ByVal value As Boolean)
            boolEditGroup = value
        End Set
    End Property

    Public Property EditUser() As Boolean
        Get
            Return boolEditUser
        End Get
        Set(ByVal value As Boolean)
            boolEditUser = value
        End Set
    End Property

    Public Property MailSetting() As Boolean
        Get
            Return boolMailSetting
        End Get
        Set(ByVal value As Boolean)
            boolMailSetting = value
        End Set
    End Property
End Class
