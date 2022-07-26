Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.data

Public Class RequisitionHistoryDAL
    Inherits SQLConnectionDAL
    Dim connPurchase As SqlConnection '= GetSqlConnection(connDB)
    Dim connPR_Online As SqlConnection = GetSqlConnection("PR_Online")
    Private Trans As SqlTransaction

    Sub New(ByVal connDB As String)
        connPurchase = GetSqlConnection(connDB)
    End Sub

    Public Function getRequestHistory(ByVal ReportOBJ As ReportOBJ) As DataTable
        Dim sbSql As New StringBuilder
        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        sbSql.AppendLine(" SELECT LTRIM(RTRIM(RQNNUMBER)) AS RQNNUMBER ")
        sbSql.AppendLine(" , LTRIM(RTRIM(COMMENT)) AS COMMENT ")
        sbSql.AppendLine(" , LTRIM(RTRIM(ISCOMPLETE)) AS ISCOMPLETE ")
        sbSql.AppendLine(" , LTRIM(RTRIM(ISPRINTED)) AS ISPRINTED ")
        sbSql.AppendLine(" , LTRIM(RTRIM(DTCOMPLETE)) AS DTCOMPLETE ")
        sbSql.AppendLine(" , CONVERT(DATETIME,LTRIM(RTRIM(POSTDATE)),103) AS POSTDATE ")
        sbSql.AppendLine(" , LTRIM(RTRIM(VDCODE)) AS VDCODE ")
        sbSql.AppendLine(" , LTRIM(RTRIM(VDNAME)) AS VDNAME ")
        sbSql.AppendLine(" , LTRIM(RTRIM(OPTFLD2)) AS APV_DATE")
        sbSql.AppendLine(" , LTRIM(RTRIM(OPTFLD3)) AS APV_YEARMONTH")
        sbSql.AppendLine(" , LTRIM(RTRIM(OPTFLD5)) AS OPTFLD5")
        sbSql.AppendLine(" FROM PORQNH1 ")
        sbSql.AppendLine(" WHERE POSTDATE BETWEEN @DATE1 AND @DATE2")
        param.AddWithValue("@DATE1", Format(ReportOBJ.ReportDateFrom, "yyyyMMdd"))
        param.AddWithValue("@DATE2", Format(ReportOBJ.ReportDateTo, "yyyyMMdd"))

        If ReportOBJ.ReportRequestBy <> "" Then
            sbSql.AppendLine(" AND LOWER(LTRIM(RTRIM(REQUESTBY))) = @REQUESTBY")
            param.AddWithValue("@REQUESTBY", ReportOBJ.ReportRequestBy.ToLower)
        End If

        If ReportOBJ.ReportVendorName <> "" Then
            sbSql.AppendLine(" AND LOWER(LTRIM(RTRIM(VDNAME))) LIKE @VDNAME")
            param.AddWithValue("@VDNAME", "%" & ReportOBJ.ReportVendorName.ToLower & "%")
        End If

        If ReportOBJ.ReportRequisitionFrom <> "" AndAlso ReportOBJ.ReportRequisitionTo <> "" Then
            sbSql.AppendLine(" AND LOWER(LTRIM(RTRIM(RQNNUMBER))) BETWEEN @RQN1 AND @RQN2")
            param.AddWithValue("@RQN1", ReportOBJ.ReportRequisitionFrom.ToLower)
            param.AddWithValue("@RQN2", ReportOBJ.ReportRequisitionTo.ToLower & "Z")
        ElseIf ReportOBJ.ReportRequisitionFrom <> "" Then
            sbSql.AppendLine(" AND LOWER(LTRIM(RTRIM(RQNNUMBER))) LIKE @RQN1")
            param.AddWithValue("@RQN1", ReportOBJ.ReportRequisitionFrom.ToLower & "%")
        End If

        If ReportOBJ.ReportSection <> "" Then
            sbSql.AppendLine(" AND LOWER(LTRIM(RTRIM(COMMENT))) LIKE @COMMENT")
            param.AddWithValue("@COMMENT", ReportOBJ.ReportSection.ToLower)
        End If

        sbSql.AppendLine(" ORDER BY POSTDATE DESC, RQNNUMBER DESC ")
        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.GetData(sbSql.ToString, "tbItem", connPurchase, param).Tables("tbItem")
    End Function

    Public Function getRequestHistoryAX(ByVal ReportOBJ As ReportOBJ) As DataTable
        Dim sbSql As New StringBuilder
        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        sbSql.AppendLine(" SELECT PurchReqId ")
        sbSql.AppendLine(" , PurchReqName ")
        sbSql.AppendLine(" , DATEADD(hh,7,createdDateTime) AS createdDateTime ")
        sbSql.AppendLine(" , ECL_PRDOCUMENTAPPROVEDDATE ")
        sbSql.AppendLine(" , ECL_PRDOCUMENTRECEIPTDATE ")
        sbSql.AppendLine(" , DATEADD(hh,7,SubmittedDateTime) AS SubmittedDateTime ")
        sbSql.AppendLine(" , CASE ECL_PRAMOUNT WHEN 0 THEN 'L5000'")
        sbSql.AppendLine("                      WHEN 1 THEN 'M5000' END AS ECL_PRAMOUNT")
        sbSql.AppendLine(" , CASE RequisitionStatus WHEN 0 THEN 'Draft' ")
        sbSql.AppendLine("                         WHEN 10 THEN 'In review' ")
        sbSql.AppendLine("                         WHEN 20 THEN 'Rejected' ")
        sbSql.AppendLine("                         WHEN 30 THEN 'Registered' ") 'Approved
        sbSql.AppendLine("                         WHEN 40 THEN 'Canceled' ")
        sbSql.AppendLine("                         WHEN 50 THEN 'Closed' END AS RequisitionStatus")
        sbSql.AppendLine(" FROM PURCHREQTABLE ")
        sbSql.AppendLine(" WHERE createdDateTime BETWEEN @DATE1 AND @DATE2")
        param.AddWithValue("@DATE1", Format(ReportOBJ.ReportDateFrom, "yyyyMMdd 00:00:00"))
        param.AddWithValue("@DATE2", Format(ReportOBJ.ReportDateTo, "yyyyMMdd 23:59:59"))

        'If ReportOBJ.ReportRequestBy <> "" Then
        '    sbSql.AppendLine(" AND LOWER(LTRIM(RTRIM(REQUESTBY))) = @REQUESTBY")
        '    param.AddWithValue("@REQUESTBY", ReportOBJ.ReportRequestBy.ToLower)
        'End If
        'If ReportOBJ.ReportSection <> "" Then
        '    sbSql.AppendLine(" AND LOWER(LTRIM(RTRIM(COMMENT))) LIKE @COMMENT")
        '    param.AddWithValue("@COMMENT", ReportOBJ.ReportSection.ToLower)
        'End If
        If ReportOBJ.ReportSection <> "" AndAlso ReportOBJ.ReportRequestBy <> "" Then
            'AXRecord.ExecuteStmt("Select * from %1 where %1.PurchReqName == '" & String.Format("{0}-{1}", ReportOBJ.ReportSection, ReportOBJ.ReportRequestBy) & "'")
            sbSql.AppendLine(" AND PurchReqName = @PurchReqName")
            param.AddWithValue("@PurchReqName", String.Format("{0}-{1}-{2}", ReportOBJ.ReportSite, ReportOBJ.ReportSection, ReportOBJ.ReportRequestBy))
        ElseIf ReportOBJ.ReportSection <> "" AndAlso ReportOBJ.ReportRequestBy = "" Then
            'AXRecord.ExecuteStmt("Select * from %1 where %1.PurchReqName LIKE '" & String.Format("{0}-*", ReportOBJ.ReportSection) & "'")
            sbSql.AppendLine(" AND PurchReqName LIKE @PurchReqName")
            param.AddWithValue("@PurchReqName", String.Format("{0}-{1}-%", ReportOBJ.ReportSite, ReportOBJ.ReportSection))
        Else
            sbSql.AppendLine(" AND PurchReqName LIKE @PurchReqName")
            param.AddWithValue("@PurchReqName", String.Format("{0}-%", ReportOBJ.ReportSite))

        End If

        If ReportOBJ.ReportVendorName <> "" Then
            sbSql.AppendLine(" AND LOWER(LTRIM(RTRIM(VDNAME))) LIKE @VDNAME")
            param.AddWithValue("@VDNAME", "%" & ReportOBJ.ReportVendorName.ToLower & "%")
        End If

        If ReportOBJ.ReportRequisitionFrom <> "" AndAlso ReportOBJ.ReportRequisitionTo <> "" Then
            sbSql.AppendLine(" AND LOWER(LTRIM(RTRIM(PurchReqId))) BETWEEN @RQN1 AND @RQN2")
            param.AddWithValue("@RQN1", ReportOBJ.ReportRequisitionFrom.ToLower)
            param.AddWithValue("@RQN2", ReportOBJ.ReportRequisitionTo.ToLower & "Z")
        ElseIf ReportOBJ.ReportRequisitionFrom <> "" Then
            sbSql.AppendLine(" AND LOWER(LTRIM(RTRIM(PurchReqId))) LIKE @RQN1")
            param.AddWithValue("@RQN1", ReportOBJ.ReportRequisitionFrom.ToLower & "%")
        End If

        sbSql.AppendLine(" ORDER BY createdDateTime DESC ")
        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.GetData(sbSql.ToString, "tbPURCHREQTABLE", connPurchase, param).Tables("tbPURCHREQTABLE")
    End Function

    Public Function getRequestHistoryByUser(ByVal strUser As String) As DataTable
        Dim sbSql As New StringBuilder
        sbSql.AppendLine(" SELECT LTRIM(RTRIM(RQNNUMBER)) AS RQNNUMBER ")
        sbSql.AppendLine(" , LTRIM(RTRIM(ISCOMPLETE)) AS ISCOMPLETE ")
        sbSql.AppendLine(" , LTRIM(RTRIM(ISPRINTED)) AS ISPRINTED ")
        sbSql.AppendLine(" , LTRIM(RTRIM(DTCOMPLETE)) AS DTCOMPLETE ")
        sbSql.AppendLine(" , CONVERT(DATETIME,LTRIM(RTRIM(POSTDATE)),103) AS POSTDATE ")
        sbSql.AppendLine(" , LTRIM(RTRIM(VDCODE)) AS VDCODE ")
        sbSql.AppendLine(" , LTRIM(RTRIM(VDNAME)) AS VDNAME ")
        sbSql.AppendLine(" FROM PORQNH1 ")
        sbSql.AppendLine(" WHERE LOWER(LTRIM(RTRIM(REQUESTBY))) = @REQUESTBY")
        sbSql.AppendLine(" ORDER BY POSTDATE DESC ")
        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@REQUESTBY", strUser.ToLower)
        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.GetData(sbSql.ToString, "tbItem", connPurchase, param).Tables("tbItem")
    End Function

    Public Function getRequestHistoryListByUser(ByVal strUser As String) As DataTable
        Dim sbSql As New StringBuilder
        sbSql.AppendLine(" SELECT LTRIM(RTRIM(RQNNUMBER)) AS RQNNUMBER ")
        sbSql.AppendLine(" , LTRIM(RTRIM(ISCOMPLETE)) AS ISCOMPLETE ")
        sbSql.AppendLine(" , LTRIM(RTRIM(ISPRINTED)) AS ISPRINTED ")
        sbSql.AppendLine(" , LTRIM(RTRIM(PORQNH1.DTCOMPLETE)) AS DTCOMPLETE ")
        sbSql.AppendLine(" , LTRIM(RTRIM(POSTDATE)) AS POSTDATE ")
        sbSql.AppendLine(" , LTRIM(RTRIM(PORQNH1.VDCODE)) AS VDCODE ")
        sbSql.AppendLine(" , LTRIM(RTRIM(PORQNH1.VDNAME)) AS VDNAME ")
        sbSql.AppendLine(" FROM PORQNH1 ")
        sbSql.AppendLine("  INNER JOIN PORQNL ON PORQNH1.RQNHSEQ=PORQNL.RQNHSEQ")
        sbSql.AppendLine(" WHERE LOWER(LTRIM(RTRIM(REQUESTBY))) = @REQUESTBY")
        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@REQUESTBY", strUser.ToLower)
        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.GetData(sbSql.ToString, "tbItem", connPurchase, param).Tables("tbItem")
    End Function

    Public Function getRequestHistoryListByRequestNumber(ByVal RQNNUMBER As String) As DataTable
        Dim sbSql As New StringBuilder
        sbSql.AppendLine(" SELECT LTRIM(RTRIM(RQNNUMBER)) AS RQNNUMBER ")
        sbSql.AppendLine(" , LTRIM(RTRIM(OPTFLD4)) AS OPTFLD4 ")
        sbSql.AppendLine(" , LTRIM(RTRIM(DESCRIPTIO)) AS DESCRIPTIO ")
        sbSql.AppendLine(" , LTRIM(RTRIM(REFERENCE)) AS REFERENCE ")
        sbSql.AppendLine(" , LTRIM(RTRIM(PORQNH1.VDCODE)) AS VDCODE ")
        sbSql.AppendLine(" , LTRIM(RTRIM(PORQNH1.VDNAME)) AS VDNAME ")
        sbSql.AppendLine(" , LTRIM(RTRIM(PORQNL.ITEMNO)) AS ITEMNO ")
        sbSql.AppendLine(" , LTRIM(RTRIM(PORQNL.ITEMDESC)) AS ITEMDESC ")
        sbSql.AppendLine(" , LTRIM(RTRIM(PORQNL.OQORDERED)) AS OQORDERED ")
        sbSql.AppendLine(" , LTRIM(RTRIM(PORQNL.ORDERUNIT)) AS ORDERUNIT ")
        sbSql.AppendLine(" , LTRIM(RTRIM(PORQNL.VDCODE)) AS VDCODEL ")
        sbSql.AppendLine(" , LTRIM(RTRIM(PORQNL.VDNAME)) AS VDNAMEL ")
        'sbSql.AppendLine(" , CASE WHEN PORQNL.EXPARRIVAL=0 THEN '' ELSE ")
        'sbSql.AppendLine("      CONVERT(VARCHAR(10),PORQNL.EXPARRIVAL) END AS EXPARRIVAL ")
        sbSql.AppendLine(" , PORQNL.EXPARRIVAL ")
        sbSql.AppendLine(" , LTRIM(RTRIM(PORQNL.LOCATION)) AS LOCATION ")
        sbSql.AppendLine(" , LTRIM(RTRIM(PORQNL.MANITEMNO)) AS MANITEMNO ")
        sbSql.AppendLine(" , LTRIM(RTRIM(PORQNL.DTCOMPLETE)) AS DTCOMPLETEL ")
        sbSql.AppendLine(" , LTRIM(RTRIM(PORQNC.COMMENT)) AS INSTRUCTCOMMENT ")
        sbSql.AppendLine(" FROM PORQNH1 ")
        sbSql.AppendLine("  INNER JOIN PORQNL ON PORQNH1.RQNHSEQ=PORQNL.RQNHSEQ")
        sbSql.AppendLine("  LEFT OUTER JOIN PORQNC ON PORQNL.RQNHSEQ=PORQNC.RQNHSEQ")
        sbSql.AppendLine("      AND PORQNL.RQNCSEQ = PORQNC.RQNCSEQ")
        sbSql.AppendLine(" WHERE LOWER(LTRIM(RTRIM(RQNNUMBER))) = @RQNNUMBER")
        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@RQNNUMBER", RQNNUMBER.ToLower)
        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.GetData(sbSql.ToString, "tbRQHistL", connPurchase, param).Tables("tbRQHistL")
    End Function

    Public Function getRequestHistoryListByRequestNumberAX(ByVal RQNNUMBER As String) As DataTable
        Dim sbSql As New StringBuilder
        sbSql.AppendLine(" SELECT PURCHREQID, PURCHREQLINE.RECID, CONVERT(INTEGER,LINENUM) AS LINENUM, ECL_PRAMOUNT ")
        sbSql.AppendLine(" , PURCHREQLINE.ITEMID, INVENTTABLE.PRODUCT, PURCHREQLINE.INVENTDIMID, PURCHREQLINE.NAME, PURCHQTY, UNITOFMEASURE.SYMBOL AS UNIT, PURCHPRICE, PURCHQTY * PURCHPRICE AS PURCHAMOUNT ")
        sbSql.AppendLine(" , CASE WHEN INVENTTABLEMODULE.HOYA_CURRENCYCODE='' THEN 'THB' ELSE ISNULL(INVENTTABLEMODULE.HOYA_CURRENCYCODE,'THB') END CURRENCYCODE ")
        sbSql.AppendLine(" , ISNULL(CURRENCY.CURRENCYCODEISO,'THB') CURRENCYCODEISO")
        sbSql.AppendLine(" , CONVERT(CHAR(10),PURCHREQTABLE.REQUIREDDATE,103) AS REQUIREDDATEH")
        sbSql.AppendLine(" , CONVERT(CHAR(10),PURCHREQLINE.REQUIREDDATE,103) AS REQUIREDDATEL")
        sbSql.AppendLine(" , ECL_REMARK, Section.ECL_SHORTNAME ")
        sbSql.AppendLine(" , INVENTDIM.INVENTLOCATIONID, CONFIGID AS CONFIG, INVENTSIZEID AS SIZE, INVENTCOLORID AS COLOR ")
        sbSql.AppendLine(" , ECORESPRODUCTDIMENSIONGROUP.NAME AS PRODUCTDIMENSION")
        sbSql.AppendLine(" FROM PURCHREQTABLE ")
        sbSql.AppendLine("  INNER JOIN PURCHREQLINE ON PURCHREQTABLE.RECID=PURCHREQLINE.PURCHREQTABLE")
        sbSql.AppendLine("  INNER JOIN INVENTTABLE ON PURCHREQLINE.ITEMID=INVENTTABLE.ITEMID")
        sbSql.AppendLine("  INNER JOIN INVENTTABLEMODULE ON PURCHREQLINE.ITEMID=INVENTTABLEMODULE.ITEMID")
        sbSql.AppendLine("  LEFT OUTER JOIN CURRENCY ON INVENTTABLEMODULE.HOYA_CURRENCYCODE=CURRENCY.CURRENCYCODE")
        sbSql.AppendLine("  INNER JOIN INVENTDIM ON PURCHREQLINE.INVENTDIMID=INVENTDIM.INVENTDIMID")
        sbSql.AppendLine(" LEFT OUTER JOIN ECORESPRODUCTDIMENSIONGROUPPRODUCT ON INVENTTABLE.PRODUCT=ECORESPRODUCTDIMENSIONGROUPPRODUCT.PRODUCT")
        sbSql.AppendLine(" LEFT OUTER JOIN ECORESPRODUCTDIMENSIONGROUP ON ECORESPRODUCTDIMENSIONGROUPPRODUCT.PRODUCTDIMENSIONGROUP=ECORESPRODUCTDIMENSIONGROUP.RECID")
        sbSql.AppendLine("  LEFT OUTER JOIN UNITOFMEASURE ON PURCHREQLINE.PURCHUNITOFMEASURE=UNITOFMEASURE.RECID")
        sbSql.AppendLine("  LEFT OUTER JOIN (")
        sbSql.AppendLine("      SELECT DIMENSIONATTRIBUTEVALUESET,ECL_SHORTNAME FROM DIMENSIONATTRIBUTE")
        sbSql.AppendLine("      INNER JOIN DIMENSIONATTRIBUTEVALUE ON DIMENSIONATTRIBUTE.RECID = DIMENSIONATTRIBUTEVALUE.DIMENSIONATTRIBUTE")
        sbSql.AppendLine("      INNER JOIN DIMENSIONATTRIBUTEVALUESETITEM ON DIMENSIONATTRIBUTEVALUE.RECID = DIMENSIONATTRIBUTEVALUESETITEM.DIMENSIONATTRIBUTEVALUE")
        sbSql.AppendLine("      INNER JOIN DIMENSIONFINANCIALTAG ON DIMENSIONATTRIBUTEVALUESETITEM.DISPLAYVALUE=DIMENSIONFINANCIALTAG.VALUE")
        sbSql.AppendLine("      WHERE DIMENSIONATTRIBUTE.NAME = 'D2_Section'")
        sbSql.AppendLine("  ) Section ON PURCHREQLINE.DEFAULTDIMENSION=Section.DIMENSIONATTRIBUTEVALUESET")
        sbSql.AppendLine(" WHERE PURCHREQID = @RQNNUMBER AND MODULETYPE=1")
        sbSql.AppendLine(" ORDER BY LINENUM")
        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@RQNNUMBER", RQNNUMBER)
        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.GetData(sbSql.ToString, "tbRQHistL", connPurchase, param).Tables("tbRQHistL")
    End Function

    Public Function getRequestSequenceByRequestNumber(ByVal RQNNUMBER As String) As DataTable
        Dim sbSql As New StringBuilder
        sbSql.AppendLine(" SELECT RQNHSEQ ")
        sbSql.AppendLine(" FROM PORQNH1 ")
        sbSql.AppendLine(" WHERE LOWER(LTRIM(RTRIM(RQNNUMBER))) = @RQNNUMBER")
        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@RQNNUMBER", RQNNUMBER.ToLower)
        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.GetData(sbSql.ToString, "tbRQNHSEQ", connPurchase, param).Tables("tbRQNHSEQ")
    End Function
    'RP,HOPTRP,PORQN01.RPT,L5000
    Public Function getDocRevision(ByVal Factory As String, ByVal ACCPAC As String, ByVal ReportName As String, ByVal Condition As String) As DataTable
        Dim sbSql As New StringBuilder
        sbSql.AppendLine(" SELECT DocNo, Revision, EffectDate ")
        sbSql.AppendLine(" FROM ReportRevision ")
        sbSql.AppendLine(" WHERE Factory=@Factory")
        sbSql.AppendLine(" AND ACCPAC=@ACCPAC")
        sbSql.AppendLine(" AND ReportName=@ReportName")
        sbSql.AppendLine(" AND Condition=@Condition")
        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@Factory", Factory)
        param.AddWithValue("@ACCPAC", ACCPAC)
        param.AddWithValue("@ReportName", ReportName)
        param.AddWithValue("@Condition", Condition)
        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.GetData(sbSql.ToString, "tbDocRevision", connPR_Online, param).Tables("tbDocRevision")
    End Function

    Public Function getSection() As DataTable
        Dim sbSql As New StringBuilder
        sbSql.AppendLine(" SELECT DISTINCT COMMENT ")
        sbSql.AppendLine(" FROM PORQNH1 ")
        sbSql.AppendLine(" ORDER BY COMMENT")
        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.GetData(sbSql.ToString, "tbSec", connPurchase).Tables("tbSec")
    End Function

    Public Sub UpdateApproveDate(ByVal NewAPVYearMonth As String, ByVal NewAPVDate As String, ByVal RQNNUMBER As String)
        Dim sbSql As New StringBuilder
        sbSql.Remove(0, sbSql.Length)
        sbSql.AppendLine(" UPDATE PORQNH1 SET ")
        sbSql.AppendLine(" OPTFLD2=@OPTFLD2")
        sbSql.AppendLine(" ,OPTFLD3=@OPTFLD3")
        sbSql.AppendLine(" WHERE RQNNUMBER=@RQNNUMBER")

        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        'param.AddWithValue("@OPTDATE", CInt(NewApproveDate))
        param.AddWithValue("@OPTFLD2", NewAPVDate)
        param.AddWithValue("@OPTFLD3", NewAPVYearMonth)
        param.AddWithValue("@RQNNUMBER", RQNNUMBER)

        Dim cDBSQL As New Common.cDBSQL
        cDBSQL.ExecuteData(sbSql.ToString, connPurchase, param)
    End Sub

    Public Sub UpdateApproveDateAX(ByVal NewAPVDate As Date, ByVal purchReqId As String)
        Dim sbSql As New StringBuilder
        sbSql.Remove(0, sbSql.Length)
        sbSql.AppendLine(" UPDATE PURCHREQTABLE SET ")
        sbSql.AppendLine(" ECL_PRDOCUMENTAPPROVEDDATE=@ECL_PRDOCUMENTAPPROVEDDATE")
        sbSql.AppendLine(" WHERE PURCHREQID=@PURCHREQID")

        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@ECL_PRDOCUMENTAPPROVEDDATE", NewAPVDate)
        param.AddWithValue("@PURCHREQID", purchReqId)

        Dim cDBSQL As New Common.cDBSQL
        cDBSQL.ExecuteData(sbSql.ToString, connPurchase, param)
    End Sub

    Public Sub UpdatePRReceivedDate(ByVal NewPRReceivedDate As Integer, ByVal RQNNUMBER As String)
        Dim sbSql As New StringBuilder
        sbSql.Remove(0, sbSql.Length)
        sbSql.AppendLine(" UPDATE PORQNH1 SET ")
        sbSql.AppendLine(" OPTFLD5=@OPTFLD5")
        sbSql.AppendLine(" WHERE RQNNUMBER=@RQNNUMBER")

        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@OPTFLD5", NewPRReceivedDate)
        param.AddWithValue("@RQNNUMBER", RQNNUMBER)

        Dim cDBSQL As New Common.cDBSQL
        cDBSQL.ExecuteData(sbSql.ToString, connPurchase, param)
    End Sub

    Public Sub UpdatePRReceivedDateAX(ByVal NewPRReceivedDate As Date, ByVal purchReqId As String)
        Dim sbSql As New StringBuilder
        sbSql.Remove(0, sbSql.Length)
        sbSql.AppendLine(" UPDATE PURCHREQTABLE SET ")
        sbSql.AppendLine(" ECL_PRDOCUMENTRECEIPTDATE=@ECL_PRDOCUMENTRECEIPTDATE")
        sbSql.AppendLine(" WHERE PURCHREQID=@PURCHREQID")

        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@ECL_PRDOCUMENTRECEIPTDATE", NewPRReceivedDate)
        param.AddWithValue("@PURCHREQID", purchReqId)

        Dim cDBSQL As New Common.cDBSQL
        cDBSQL.ExecuteData(sbSql.ToString, connPurchase, param)
    End Sub

    Public Function DeleteRQH(ByVal RQNHSEQ As String) As Common.cDBSQL.ExecuteReturn
        Dim sbSql As New StringBuilder
        sbSql.Remove(0, sbSql.Length)
        sbSql.AppendLine(" DELETE PORQNH1 ")
        sbSql.AppendLine(" WHERE RQNHSEQ=@RQNHSEQ")

        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@RQNHSEQ", RQNHSEQ)

        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.ExecuteData(sbSql.ToString, connPurchase, param, Trans)
    End Function

    Public Function DeleteRQH2(ByVal RQNHSEQ As String) As Common.cDBSQL.ExecuteReturn
        Dim sbSql As New StringBuilder
        sbSql.Remove(0, sbSql.Length)
        sbSql.AppendLine(" DELETE PORQNH2 ")
        sbSql.AppendLine(" WHERE RQNHSEQ=@RQNHSEQ")

        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@RQNHSEQ", RQNHSEQ)

        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.ExecuteData(sbSql.ToString, connPurchase, param, Trans)
    End Function

    Public Function DeleteRQL(ByVal RQNHSEQ As String) As Common.cDBSQL.ExecuteReturn
        Dim sbSql As New StringBuilder
        sbSql.Remove(0, sbSql.Length)
        sbSql.AppendLine(" DELETE PORQNL ")
        sbSql.AppendLine(" WHERE RQNHSEQ=@RQNHSEQ")

        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@RQNHSEQ", RQNHSEQ)

        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.ExecuteData(sbSql.ToString, connPurchase, param, Trans)
    End Function

    Public Function DeleteRQC(ByVal RQNHSEQ As String) As Common.cDBSQL.ExecuteReturn
        Dim sbSql As New StringBuilder
        sbSql.Remove(0, sbSql.Length)
        sbSql.AppendLine(" DELETE PORQNC ")
        sbSql.AppendLine(" WHERE RQNHSEQ=@RQNHSEQ")

        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@RQNHSEQ", RQNHSEQ)

        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.ExecuteData(sbSql.ToString, connPurchase, param, Trans)
    End Function

    Public Sub BeginTrans()
        With connPurchase
            If .State = ConnectionState.Open Then .Close()
            .Open()
        End With
        Trans = connPurchase.BeginTransaction
    End Sub

    Public Sub CommitTrans()
        Trans.Commit()
        Trans = Nothing
        connPurchase.Close()
    End Sub

    Public Sub RollBackTrans()
        Trans.Rollback()
        Trans = Nothing
        connPurchase.Close()
    End Sub

End Class
