Imports Microsoft.VisualBasic
Imports System.Data

Public Class LoginOBJ
    Private strUserName As String
    Private strUserGroup As String
    Private strDomainName As String
    Private boolAUTHENTICATED As Boolean
    Private strFullName As String
    Private strFirstName As String
    Private strLastName As String
    Private strSection As String
    Private strSections As String
    Private strPurchase As Boolean = False
    Private strOUGroup As String = ""
    Private strFactory As String
    Private strWorkPlace As String
    Private strMessage As String
    Private strEmail As String
    Private strTitle As String
    Private strCompany As String
    Private strACCPAC As String
    Private strPRPreparerEmpCode As String
    Private boolAllowSelectVendor As Boolean = False
    Private boolAllowSubmitPRApprove As Boolean = False
    Private boolAllowSubmitPRReceive As Boolean = False
    Private boolAllowChangeRequisition As Boolean = False
    Private boolAllowEditPermission As Boolean = False
    Private boolAllowAnotherSection As Boolean = False
    Private strHOYA_Site As String
    Private strHOYA_Factory As String
    Private strFilterItemCode As String
    Private _categorySearchText As String


    Public Sub ChkOBJ(ByVal dr As DataRow)
        'boolAUTHENTICATED = True
        strUserName = dr("uName").ToString
        strUserGroup = dr("uGroup").ToString
        strDomainName = dr("uDomain").ToString
        strFullName = dr("uFullName").ToString
        strFirstName = dr("uFirstName").ToString
        strLastName = dr("uLastName").ToString
        strSection = dr("uSection").ToString
        strFactory = dr("uFactory").ToString
        strCompany = dr("uCompany").ToString
        strTitle = dr("uTitle").ToString
        strHOYA_Site = dr("HOYA_Site").ToString
        strHOYA_Factory = dr("HOYA_Factory").ToString
        strFilterItemCode = dr("FilterItemCode").ToString
    End Sub

    Public Property UserName() As String
        Get
            Return strUserName
        End Get
        Set(ByVal value As String)
            strUserName = value
        End Set
    End Property

    Public Property UserGroup() As String
        Get
            Return strUserGroup
        End Get
        Set(ByVal value As String)
            strUserGroup = value
        End Set
    End Property

    Public Property DomainName() As String
        Get
            Return strDomainName
        End Get
        Set(ByVal value As String)
            strDomainName = value
        End Set
    End Property

    Public Property AUTHENTICATED() As Boolean
        Get
            Return boolAUTHENTICATED
        End Get
        Set(ByVal value As Boolean)
            boolAUTHENTICATED = value
        End Set
    End Property

    Public Property FullName() As String
        Get
            Return strFullName
        End Get
        Set(ByVal value As String)
            strFullName = value
        End Set
    End Property

    Public Property FirstName() As String
        Get
            Return strFirstName
        End Get
        Set(ByVal value As String)
            strFirstName = value
        End Set
    End Property

    Public Property LastName() As String
        Get
            Return strLastName
        End Get
        Set(ByVal value As String)
            strLastName = value
        End Set
    End Property

    Public Property Company() As String
        Get
            Return strCompany
        End Get
        Set(ByVal value As String)
            strCompany = value
        End Set
    End Property

    Public Property Title() As String
        Get
            Return strTitle
        End Get
        Set(ByVal value As String)
            strTitle = value
        End Set
    End Property

    Public Property Section() As String
        Get
            Return strSection
        End Get
        Set(ByVal value As String)
            strSection = value
        End Set
    End Property

    Public Property Sections() As String
        Get
            Return strSections
        End Get
        Set(ByVal value As String)
            strSections = value
        End Set
    End Property

    ''' <summary>
    ''' Group Domain contain %PR%
    ''' </summary>
    ''' <value>True/False</value>
    ''' <returns>Boolean</returns>
    ''' <remarks>Group Domain contain %PR%</remarks>
    Public Property PRGroup() As Boolean
        Get
            Return strPurchase
        End Get
        Set(ByVal value As Boolean)
            strPurchase = value
        End Set
    End Property

    Public Property OUGroup() As String
        Get
            Return strOUGroup
        End Get
        Set(ByVal value As String)
            strOUGroup = value
        End Set
    End Property

    Public Property Factory() As String
        Get
            Return strFactory
        End Get
        Set(ByVal value As String)
            strFactory = value
        End Set
    End Property

    Public Property WorkPlace() As String
        Get
            Return strWorkPlace
        End Get
        Set(ByVal value As String)
            strWorkPlace = value
        End Set
    End Property

    Public Property ACCPAC() As String
        Get
            Return strACCPAC
        End Get
        Set(ByVal value As String)
            strACCPAC = value
        End Set
    End Property

    Public Property PRPreparerEmpCode() As String
        Get
            Return strPRPreparerEmpCode
        End Get
        Set(ByVal value As String)
            strPRPreparerEmpCode = value
        End Set
    End Property

    Public Property AllowSubmitPRApprove() As Boolean
        Get
            Return boolAllowSubmitPRApprove
        End Get
        Set(ByVal value As Boolean)
            boolAllowSubmitPRApprove = value
        End Set
    End Property

    Public Property AllowSubmitPRReceive() As Boolean
        Get
            Return boolAllowSubmitPRReceive
        End Get
        Set(ByVal value As Boolean)
            boolAllowSubmitPRReceive = value
        End Set
    End Property

    Public Property AllowChangeRequisition() As Boolean
        Get
            Return boolAllowChangeRequisition
        End Get
        Set(ByVal value As Boolean)
            boolAllowChangeRequisition = value
        End Set
    End Property

    Public Property AllowSelectVendor() As Boolean
        Get
            Return boolAllowSelectVendor
        End Get
        Set(ByVal value As Boolean)
            boolAllowSelectVendor = value
        End Set
    End Property

    Public Property AllowEditPermission() As Boolean
        Get
            Return boolAllowEditPermission
        End Get
        Set(ByVal value As Boolean)
            boolAllowEditPermission = value
        End Set
    End Property

    Public Property AllowAnotherSection() As Boolean
        Get
            Return boolAllowAnotherSection
        End Get
        Set(ByVal value As Boolean)
            boolAllowAnotherSection = value
        End Set
    End Property

    Public Property Message() As String
        Get
            Return strMessage
        End Get
        Set(ByVal value As String)
            strMessage = value
        End Set
    End Property

    Public Property axHOYA_Site As String
        Get
            Return strHOYA_Site
        End Get
        Set(value As String)
            strHOYA_Site = value
        End Set
    End Property

    Public Property axHOYA_FactoryDIMENSIONFINANCIALTAG As String
        Get
            Return strHOYA_Factory
        End Get
        Set(value As String)
            strHOYA_Factory = value
        End Set
    End Property

    Public Property FilterItemCode As String
        Get
            Return strFilterItemCode
        End Get
        Set(value As String)
            strFilterItemCode = value
        End Set
    End Property

    Public Property CategorySearchText As String
        Get
            Return _categorySearchText
        End Get
        Set(value As String)
            _categorySearchText = value
        End Set
    End Property
End Class
