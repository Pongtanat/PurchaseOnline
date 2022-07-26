Imports Microsoft.VisualBasic
Imports System.Data

Public Class RequestLOBJ
    Private NHSEQ As Integer
    Private NLREV As Integer
    Private AUDTDATE As Integer
    Private AUDTTIME As Integer
    Private AUDTUSER As String
    Private AUDTORG As String
    Private NLSEQ As Integer
    Private LINEORDER As Integer
    Private NCSEQ As Integer
    Private OEONUMBER As String
    Private VDEXISTS As Integer
    Private VDCODE As String
    Private VDNAME As String
    Private INDBTABLE As Integer
    Private COMPLETION As Integer
    Private DTCOMPLETE As Integer
    Private ITEMEXISTS As Integer
    Private ITEMNO As String
    Private LOCATION As String
    Private ITEMDESC As String
    Private EXPARRIVAL As Integer
    Private VENDITEMNO As String
    Private HASCOMMENT As Integer = 0
    Private COMMENT As String = ""
    Private ORDERUNIT As String
    Private ORDERCONV As Double
    Private ORDERDECML As Integer
    Private STOCKDECML As Integer
    Private OQORDERED As Double
    Private HASDROPSHI As Integer
    Private DROPTYPE As Integer
    Private IDCUST As String = ""
    Private IDCUSTSHPT As String = ""
    Private DLOCATION As String = ""
    Private DESC As String = ""
    Private ADDRESS1 As String = ""
    Private ADDRESS2 As String = ""
    Private ADDRESS3 As String = ""
    Private ADDRESS4 As String = ""
    Private CITY As String = ""
    Private STATE As String = ""
    Private ZIP As String = ""
    Private COUNTRY As String = ""
    Private PHONE As String = ""
    Private FAX As String = ""
    Private CONTACT As String = ""
    Private STOCKITEM As Integer = 1
    Private EMAIL As String = ""
    Private PHONEC As String = ""
    Private FAXC As String = ""
    Private EMAILC As String = ""
    Private MANITEMNO As String = ""
    Private LINENUM As Integer
    Private RECID As Integer

    Public Sub Request()

    End Sub

    Public Sub Request(ByVal dr As DataRow)
        NHSEQ = dr("RQNHSEQ").ToString
        NLREV = dr("RQNLREV").ToString
        AUDTDATE = dr("AUDTDATE").ToString
        AUDTTIME = dr("AUDTTIME").ToString
        AUDTUSER = dr("AUDTUSER").ToString
        AUDTORG = dr("AUDTORG").ToString
        NLSEQ = dr("RQNLSEQ").ToString
        LINEORDER = dr("LINEORDER").ToString
        NCSEQ = dr("RQNCSEQ").ToString
        OEONUMBER = dr("OEONUMBER").ToString
        VDEXISTS = dr("VDEXISTS").ToString
        VDCODE = dr("VDCODE").ToString
        VDNAME = dr("VDNAME").ToString
        INDBTABLE = dr("INDBTABLE").ToString
        COMPLETION = dr("COMPLETION").ToString
        DTCOMPLETE = dr("DTCOMPLETE").ToString
        ITEMEXISTS = dr("ITEMEXISTS").ToString
        ITEMNO = dr("ITEMNO").ToString
        LOCATION = dr("LOCATION").ToString
        ITEMDESC = dr("ITEMDESC").ToString
        EXPARRIVAL = dr("EXPARRIVAL").ToString
        VENDITEMNO = dr("VENDITEMNO").ToString
        HASCOMMENT = dr("HASCOMMENT").ToString
        ORDERUNIT = dr("ORDERUNIT").ToString
        ORDERCONV = dr("ORDERCONV").ToString
        ORDERDECML = dr("ORDERDECML").ToString
        STOCKDECML = dr("STOCKDECML").ToString
        OQORDERED = dr("OQORDERED").ToString
        HASDROPSHI = dr("HASDROPSHI").ToString
        DROPTYPE = dr("DROPTYPE").ToString
        IDCUST = dr("IDCUST").ToString
        IDCUSTSHPT = dr("IDCUSTSHPT").ToString
        DLOCATION = dr("DLOCATION").ToString
        DESC = dr("DESC").ToString
        ADDRESS1 = dr("ADDRESS1").ToString
        ADDRESS2 = dr("ADDRESS2").ToString
        ADDRESS3 = dr("ADDRESS3").ToString
        ADDRESS4 = dr("ADDRESS4").ToString
        CITY = dr("CITY").ToString
        STATE = dr("STATE").ToString
        ZIP = dr("ZIP").ToString
        COUNTRY = dr("COUNTRY").ToString
        PHONE = dr("PHONE").ToString
        FAX = dr("FAX").ToString
        CONTACT = dr("CONTACT").ToString
        STOCKITEM = dr("STOCKITEM").ToString
        EMAIL = dr("EMAIL").ToString
        PHONEC = dr("PHONEC").ToString
        FAXC = dr("FAXC").ToString
        EMAILC = dr("EMAILC").ToString
        MANITEMNO = dr("MANITEMNO").ToString
        LINENUM = dr("LINENUM")
        RECID = dr("RECID")
    End Sub

    Public Property rqRECID() As Integer
        Get
            Return RECID
        End Get
        Set(ByVal value As Integer)
            RECID = value
        End Set
    End Property

    Public Property rqLINENUM() As Integer
        Get
            Return LINENUM
        End Get
        Set(ByVal value As Integer)
            LINENUM = value
        End Set
    End Property

    Public Property rqNHSEQ() As Integer
        Get
            Return NHSEQ
        End Get
        Set(ByVal value As Integer)
            NHSEQ = value
        End Set
    End Property

    Public Property rqNLREV() As Integer
        Get
            Return NLREV
        End Get
        Set(ByVal value As Integer)
            NLREV = value
        End Set
    End Property

    Public Property RQAUDTDATE() As Integer
        Get
            Return AUDTDATE
        End Get
        Set(ByVal value As Integer)
            AUDTDATE = value
        End Set
    End Property

    Public Property RQAUDTTIME() As Integer
        Get
            Return AUDTTIME
        End Get
        Set(ByVal value As Integer)
            AUDTTIME = value
        End Set
    End Property

    Public Property RQAUDTUSER() As String
        Get
            Return AUDTUSER
        End Get
        Set(ByVal value As String)
            AUDTUSER = value
        End Set
    End Property

    Public Property RQAUDTORG() As String
        Get
            Return AUDTORG
        End Get
        Set(ByVal value As String)
            AUDTORG = value
        End Set
    End Property

    Public Property rqNLSEQ() As Integer
        Get
            Return NLSEQ
        End Get
        Set(ByVal value As Integer)
            NLSEQ = value
        End Set
    End Property

    Public Property rqLINEORDER() As Integer
        Get
            Return LINEORDER
        End Get
        Set(ByVal value As Integer)
            LINEORDER = value
        End Set
    End Property

    Public Property rqNCSEQ() As Integer
        Get
            Return NCSEQ
        End Get
        Set(ByVal value As Integer)
            NCSEQ = value
        End Set
    End Property

    Public Property rqOEONUMBER() As String
        Get
            Return OEONUMBER
        End Get
        Set(ByVal value As String)
            OEONUMBER = value
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

    Public Property rqVDCODE() As String
        Get
            Return VDCODE
        End Get
        Set(ByVal value As String)
            VDCODE = value
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

    Public Property rqINDBTABLE() As Integer
        Get
            Return INDBTABLE
        End Get
        Set(ByVal value As Integer)
            INDBTABLE = value
        End Set
    End Property

    Public Property rqCOMPLETION() As Integer
        Get
            Return COMPLETION
        End Get
        Set(ByVal value As Integer)
            COMPLETION = value
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

    Public Property rqITEMEXISTS() As Integer
        Get
            Return ITEMEXISTS
        End Get
        Set(ByVal value As Integer)
            ITEMEXISTS = value
        End Set
    End Property

    Public Property rqITEMNO() As String
        Get
            Return ITEMNO
        End Get
        Set(ByVal value As String)
            ITEMNO = value
        End Set
    End Property

    Public Property rqLOCATION() As String
        Get
            Return LOCATION
        End Get
        Set(ByVal value As String)
            LOCATION = value
        End Set
    End Property

    Public Property rqITEMDESC() As String
        Get
            Return ITEMDESC
        End Get
        Set(ByVal value As String)
            ITEMDESC = value
        End Set
    End Property

    Public Property rqEXPARRIVAL() As Integer
        Get
            Return EXPARRIVAL
        End Get
        Set(ByVal value As Integer)
            EXPARRIVAL = value
        End Set
    End Property

    Public Property rqVENDITEMNO() As String
        Get
            Return VENDITEMNO
        End Get
        Set(ByVal value As String)
            VENDITEMNO = value
        End Set
    End Property

    Public Property rqHASCOMMENT() As Integer
        Get
            Return HASCOMMENT
        End Get
        Set(ByVal value As Integer)
            HASCOMMENT = value
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

    Public Property rqORDERUNIT() As String
        Get
            Return ORDERUNIT
        End Get
        Set(ByVal value As String)
            ORDERUNIT = value
        End Set
    End Property

    Public Property rqORDERCONV() As Double
        Get
            Return ORDERCONV
        End Get
        Set(ByVal value As Double)
            ORDERCONV = value
        End Set
    End Property

    Public Property rqORDERDECML() As Integer
        Get
            Return ORDERDECML
        End Get
        Set(ByVal value As Integer)
            ORDERDECML = value
        End Set
    End Property

    Public Property rqSTOCKDECML() As Integer
        Get
            Return STOCKDECML
        End Get
        Set(ByVal value As Integer)
            STOCKDECML = value
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

    Public Property rqHASDROPSHI() As Integer
        Get
            Return HASDROPSHI
        End Get
        Set(ByVal value As Integer)
            HASDROPSHI = value
        End Set
    End Property

    Public Property rqDROPTYPE() As Integer
        Get
            Return DROPTYPE
        End Get
        Set(ByVal value As Integer)
            DROPTYPE = value
        End Set
    End Property

    Public Property rqIDCUST() As String
        Get
            Return IDCUST
        End Get
        Set(ByVal value As String)
            IDCUST = value
        End Set
    End Property

    Public Property rqIDCUSTSHPT() As String
        Get
            Return IDCUSTSHPT
        End Get
        Set(ByVal value As String)
            IDCUSTSHPT = value
        End Set
    End Property

    Public Property rqDLOCATION() As String
        Get
            Return DLOCATION
        End Get
        Set(ByVal value As String)
            DLOCATION = value
        End Set
    End Property

    Public Property rqDESC() As String
        Get
            Return DESC
        End Get
        Set(ByVal value As String)
            DESC = value
        End Set
    End Property

    Public Property rqADDRESS1() As String
        Get
            Return ADDRESS1
        End Get
        Set(ByVal value As String)
            ADDRESS1 = value
        End Set
    End Property

    Public Property rqADDRESS2() As String
        Get
            Return ADDRESS2
        End Get
        Set(ByVal value As String)
            ADDRESS2 = value
        End Set
    End Property

    Public Property rqADDRESS3() As String
        Get
            Return ADDRESS3
        End Get
        Set(ByVal value As String)
            ADDRESS3 = value
        End Set
    End Property

    Public Property rqADDRESS4() As String
        Get
            Return ADDRESS4
        End Get
        Set(ByVal value As String)
            ADDRESS4 = value
        End Set
    End Property

    Public Property rqCITY() As String
        Get
            Return CITY
        End Get
        Set(ByVal value As String)
            CITY = value
        End Set
    End Property

    Public Property rqSTATE() As String
        Get
            Return STATE
        End Get
        Set(ByVal value As String)
            STATE = value
        End Set
    End Property

    Public Property rqZIP() As String
        Get
            Return ZIP
        End Get
        Set(ByVal value As String)
            ZIP = value
        End Set
    End Property

    Public Property rqCOUNTRY() As String
        Get
            Return COUNTRY
        End Get
        Set(ByVal value As String)
            COUNTRY = value
        End Set
    End Property

    Public Property rqPHONE() As String
        Get
            Return PHONE
        End Get
        Set(ByVal value As String)
            PHONE = value
        End Set
    End Property

    Public Property rqFAX() As String
        Get
            Return FAX
        End Get
        Set(ByVal value As String)
            FAX = value
        End Set
    End Property

    Public Property rqCONTACT() As String
        Get
            Return CONTACT
        End Get
        Set(ByVal value As String)
            CONTACT = value
        End Set
    End Property

    Public Property rqSTOCKITEM() As Integer
        Get
            Return STOCKITEM
        End Get
        Set(ByVal value As Integer)
            STOCKITEM = value
        End Set
    End Property

    Public Property rqEMAIL() As String
        Get
            Return EMAIL
        End Get
        Set(ByVal value As String)
            EMAIL = value
        End Set
    End Property

    Public Property rqPHONEC() As String
        Get
            Return PHONEC
        End Get
        Set(ByVal value As String)
            PHONEC = value
        End Set
    End Property

    Public Property rqFAXC() As String
        Get
            Return FAXC
        End Get
        Set(ByVal value As String)
            FAXC = value
        End Set
    End Property

    Public Property rqEMAILC() As String
        Get
            Return EMAILC
        End Get
        Set(ByVal value As String)
            EMAILC = value
        End Set
    End Property

    Public Property rqMANITEMNO() As String
        Get
            Return MANITEMNO
        End Get
        Set(ByVal value As String)
            MANITEMNO = value
        End Set
    End Property

End Class
