Imports Microsoft.VisualBasic
Imports System.Data

Public Class RequestHOBJ
    Private NHSEQ As String
    Private AUDTDATE As Integer
    Private AUDTTIME As Integer
    Private AUDTUSER As String
    Private AUDTORG As String
    Private NEXTLSEQ As String
    Private LINES As Integer
    Private LINESCMPL As Integer
    Private LINESORDER As Integer
    Private ISPRINTED As Integer
    Private ISCOMPLETE As Integer
    Private DTCOMPLETE As Integer
    Private POSTDATE As Integer
    Private REQDATE As Integer
    Private NUMBER As String
    Private VDCODE As String
    Private VDEXISTS As Integer
    Private VDNAME As String
    Private ONHOLD As Integer
    Private ORDEREDON As Integer
    Private EXPARRIVAL As Date = New Date(1900, 1, 1)
    Private EXPIRATION As Integer
    Private DESCRIPTION As String = ""
    Private REFERENCE As String
    Private COMMENT As String
    Private OPTFLD1 As String = ""
    Private OPTFLD2 As String = ""
    Private OPTFLD3 As String = ""
    Private OPTFLD4 As String = ""
    Private OPTFLD5 As String = ""
    Private OPTFLD6 As String = ""
    Private OPTDATE As Integer
    Private OQORDERED As Integer
    Private REQUESTBY As String
    Private DOCSOURCE As String
    Private STCODE As String
    Private STDESC As String
    Private PRAMOUNT As Integer = 0
    Private HOYA_SITE As String
    Private HOYA_LOCATION As String
    Private ACCPAC As String
    Private strPRPreparerEmpCode As String

    Public Sub Request()

    End Sub

    Public Sub Request(ByVal dr As DataRow)
        NHSEQ = dr("NHSEQ").ToString
        AUDTDATE = dr("AUDTDATE").ToString
        AUDTTIME = dr("AUDTTIME").ToString
        AUDTUSER = dr("AUDTUSER").ToString
        AUDTORG = dr("AUDTORG").ToString
        NEXTLSEQ = dr("NEXTLSEQ").ToString
        LINES = dr("LINES").ToString
        LINESCMPL = dr("LINESCMPL").ToString
        LINESORDER = dr("LINESORDER").ToString
        ISPRINTED = dr("ISPRINTED").ToString
        ISCOMPLETE = dr("ISCOMPLETE").ToString
        DTCOMPLETE = dr("DTCOMPLETE").ToString
        POSTDATE = dr("POSTDATE").ToString
        REQDATE = dr("REQDATE").ToString
        NUMBER = dr("NUMBER").ToString
        VDCODE = dr("VDCODE").ToString
        VDEXISTS = dr("VDEXISTS").ToString
        VDNAME = dr("VDNAME").ToString
        ONHOLD = dr("ONHOLD").ToString
        ORDEREDON = dr("ORDEREDON").ToString
        EXPARRIVAL = dr("EXPARRIVAL").ToString
        EXPIRATION = dr("EXPIRATION").ToString
        DESCRIPTION = dr("DESCRIPTIO").ToString
        REFERENCE = dr("REFERENCE").ToString
        COMMENT = dr("COMMENT").ToString
        OPTFLD1 = dr("OPTFLD1").ToString
        OPTFLD2 = dr("OPTFLD2").ToString
        OPTFLD3 = dr("OPTFLD3").ToString
        OPTFLD4 = dr("OPTFLD4").ToString
        OPTFLD5 = dr("OPTFLD5").ToString
        OPTFLD6 = dr("OPTFLD6").ToString
        OPTDATE = dr("OPTDATE").ToString
        OQORDERED = dr("OQORDERED").ToString
        REQUESTBY = dr("REQUESTBY").ToString
        DOCSOURCE = dr("DOCSOURCE").ToString
        PRAMOUNT = CInt(dr("PRAMOUNT").ToString)
        HOYA_SITE = dr("HOYA_SITE").ToString
        HOYA_LOCATION = dr("HOYA_LOCATION").ToString
        ACCPAC = dr("ACCPAC").ToString
    End Sub

    Public Property rqNHSEQ() As Integer
        Get
            Return NHSEQ
        End Get
        Set(ByVal value As Integer)
            NHSEQ = value
        End Set
    End Property

    Public Property rqAUDTDATE() As Integer
        Get
            Return AUDTDATE
        End Get
        Set(ByVal value As Integer)
            AUDTDATE = value
        End Set
    End Property

    Public Property rqAUDTTIME() As Integer
        Get
            Return AUDTTIME
        End Get
        Set(ByVal value As Integer)
            AUDTTIME = value
        End Set
    End Property

    Public Property rqAUDTUSER() As String
        Get
            Return AUDTUSER
        End Get
        Set(ByVal value As String)
            AUDTUSER = value
        End Set
    End Property

    Public Property rqAUDTORG() As String
        Get
            Return AUDTORG
        End Get
        Set(ByVal value As String)
            AUDTORG = value
        End Set
    End Property

    Public Property rqNEXTLSEQ() As Integer
        Get
            Return NEXTLSEQ
        End Get
        Set(ByVal value As Integer)
            NEXTLSEQ = value
        End Set
    End Property

    Public Property rqLINES() As Integer
        Get
            Return LINES
        End Get
        Set(ByVal value As Integer)
            LINES = value
        End Set
    End Property

    Public Property rqLINESCMPL() As Integer
        Get
            Return LINESCMPL
        End Get
        Set(ByVal value As Integer)
            LINESCMPL = value
        End Set
    End Property

    Public Property rqLINESORDER() As Integer
        Get
            Return LINESORDER
        End Get
        Set(ByVal value As Integer)
            LINESORDER = value
        End Set
    End Property

    Public Property rqISPRINTED() As Integer
        Get
            Return ISPRINTED
        End Get
        Set(ByVal value As Integer)
            ISPRINTED = value
        End Set
    End Property

    Public Property rqISCOMPLETE() As Integer
        Get
            Return ISCOMPLETE
        End Get
        Set(ByVal value As Integer)
            ISCOMPLETE = value
        End Set
    End Property

    Public Property rqDTCOMPLETE() As Integer
        Get
            Return DTCOMPLETE
        End Get
        Set(ByVal value As Integer)
            DTCOMPLETE = value
        End Set
    End Property

    Public Property rqPOSTDATE() As Integer
        Get
            Return POSTDATE
        End Get
        Set(ByVal value As Integer)
            POSTDATE = value
        End Set
    End Property

    Public Property rqDATE() As Integer
        Get
            Return REQDATE
        End Get
        Set(ByVal value As Integer)
            REQDATE = value
        End Set
    End Property

    Public Property rqNUMBER() As String
        Get
            Return NUMBER
        End Get
        Set(ByVal value As String)
            NUMBER = value
        End Set
    End Property

    Public Property rqVDCODE() As String
        Get
            Return VDCODE
        End Get
        Set(ByVal value As String)
            VDCODE = value
        End Set
    End Property

    Public Property rqVDEXISTS() As Integer
        Get
            Return VDEXISTS
        End Get
        Set(ByVal value As Integer)
            VDEXISTS = value
        End Set
    End Property

    Public Property rqVDNAME() As String
        Get
            Return VDNAME
        End Get
        Set(ByVal value As String)
            VDNAME = value
        End Set
    End Property

    Public Property rqONHOLD() As Integer
        Get
            Return ONHOLD
        End Get
        Set(ByVal value As Integer)
            ONHOLD = value
        End Set
    End Property

    Public Property rqORDEREDON() As Integer
        Get
            Return ORDEREDON
        End Get
        Set(ByVal value As Integer)
            ORDEREDON = value
        End Set
    End Property

    Public Property rqEXPARRIVAL() As Date
        Get
            Return EXPARRIVAL
        End Get
        Set(ByVal value As Date)
            EXPARRIVAL = value
        End Set
    End Property

    Public Property rqEXPIRATION() As Integer
        Get
            Return EXPIRATION
        End Get
        Set(ByVal value As Integer)
            EXPIRATION = value
        End Set
    End Property

    Public Property rqDESCRIPTION() As String
        Get
            Return DESCRIPTION
        End Get
        Set(ByVal value As String)
            DESCRIPTION = value
        End Set
    End Property

    Public Property rqREFERENCE() As String
        Get
            Return REFERENCE
        End Get
        Set(ByVal value As String)
            REFERENCE = value
        End Set
    End Property

    Public Property rqCOMMENT() As String
        Get
            Return COMMENT
        End Get
        Set(ByVal value As String)
            COMMENT = value
        End Set
    End Property

    Public Property rqOPTFLD1() As String
        Get
            Return OPTFLD1
        End Get
        Set(ByVal value As String)
            OPTFLD1 = value
        End Set
    End Property

    Public Property rqOPTFLD2() As String
        Get
            Return OPTFLD2
        End Get
        Set(ByVal value As String)
            OPTFLD2 = value
        End Set
    End Property

    Public Property rqOPTFLD3() As String
        Get
            Return OPTFLD3
        End Get
        Set(ByVal value As String)
            OPTFLD3 = value
        End Set
    End Property

    Public Property rqOPTFLD4() As String
        Get
            Return OPTFLD4
        End Get
        Set(ByVal value As String)
            OPTFLD4 = value
        End Set
    End Property

    Public Property rqOPTFLD5() As String
        Get
            Return OPTFLD5
        End Get
        Set(ByVal value As String)
            OPTFLD5 = value
        End Set
    End Property

    Public Property rqOPTFLD6() As String
        Get
            Return OPTFLD6
        End Get
        Set(ByVal value As String)
            OPTFLD6 = value
        End Set
    End Property

    Public Property rqOPTDATE() As Integer
        Get
            Return OPTDATE
        End Get
        Set(ByVal value As Integer)
            OPTDATE = value
        End Set
    End Property

    Public Property rqOQORDERED() As Double
        Get
            Return OQORDERED
        End Get
        Set(ByVal value As Double)
            OQORDERED = value
        End Set
    End Property

    Public Property rqREQUESTBY() As String
        Get
            Return REQUESTBY
        End Get
        Set(ByVal value As String)
            REQUESTBY = value
        End Set
    End Property

    Public Property rqDOCSOURCE() As Integer
        Get
            Return DOCSOURCE
        End Get
        Set(ByVal value As Integer)
            DOCSOURCE = value
        End Set
    End Property

    Public Property rqSTCODE() As String
        Get
            Return STCODE
        End Get
        Set(ByVal value As String)
            STCODE = value
        End Set
    End Property

    Public Property rqSTDESC() As String
        Get
            Return STDESC
        End Get
        Set(ByVal value As String)
            STDESC = value
        End Set
    End Property

    Public Property axPRAmount() As Integer
        Get
            Return PRAMOUNT
        End Get
        Set(value As Integer)
            PRAMOUNT = value
        End Set
    End Property

    Public Property axHOYA_SITE As String
        Get
            Return HOYA_SITE
        End Get
        Set(value As String)
            HOYA_SITE = value
        End Set
    End Property

    Public Property axHOYA_LOCATION As String
        Get
            Return HOYA_LOCATION
        End Get
        Set(value As String)
            HOYA_LOCATION = value
        End Set
    End Property

    Public Property rqACCPAC As String
        Get
            Return ACCPAC
        End Get
        Set(value As String)
            ACCPAC = value
        End Set
    End Property

    Public Property PRPreparerEmpCode As String
        Get
            Return strPRPreparerEmpCode
        End Get
        Set(value As String)
            strPRPreparerEmpCode = value
        End Set
    End Property
End Class
