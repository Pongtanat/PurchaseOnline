Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.data

Public Class RequisitionDAL
    Inherits SQLConnectionDAL

    Private connPurchase As SqlConnection
    Private Trans As SqlTransaction

    Sub New(ByVal connDB As String)
        connPurchase = GetSqlConnection(connDB)
    End Sub

    Public Function getExcel(ByVal FullPath As String) As DataTable
        Dim sbSql As New StringBuilder
        sbSql.Remove(0, sbSql.Length)
        sbSql.AppendLine(" SELECT ISNULL([Item Number],'') [Item Number],ISNULL(Detail,'') Detail")
        sbSql.AppendLine(" ,ISNULL([Qty Order],0) [Qty Order], ISNULL(Section,'') Section")
        'sbSql.AppendLine(" ,ISNULL([Date Require],'') [Date Require]")
        'sbSql.AppendLine(" ,CONVERT(CHAR(10),[Date Require],103) [Date Require]")
        sbSql.AppendLine(" ,CONVERT(CHAR(10),ISNULL([Date Require],''),103) [Date Require]")
        sbSql.AppendLine(" FROM OPENROWSET(")
        sbSql.AppendLine("          'Microsoft.ACE.OLEDB.12.0','Excel 12.0;HDR=YES;IMEX=1;")
        'sbSql.AppendLine("          'Microsoft.Jet.OLEDB.4.0','Excel 8.0;HDR=YES;IMEX=1;")
        sbSql.AppendLine("          Database=" & FullPath & ";',")
        sbSql.AppendLine("          'SELECT * FROM [Sheet1$]') AS tb_EXCEL")
        Dim cDBSQL As New Common.cDBSQL
        Dim dt As DataTable = cDBSQL.GetData(sbSql.ToString, "tbEmployeeData", connPurchase).Tables("tbEmployeeData")
        Return dt
    End Function

    Public Function getAllSubSectionBySection(ByVal strSection As String) As DataTable
        Dim sbSql As New StringBuilder
        'sbSql.AppendLine(" SELECT subsection.VALUE SubSection FROM DimensionFinancialTag section")
        'sbSql.AppendLine(" INNER JOIN DIMENSIONATTRIBUTEVALUE ON section.VALUE = DIMENSIONATTRIBUTEVALUE.GROUPDIMENSION")
        'sbSql.AppendLine(" INNER JOIN DimensionFinancialTag subsection ON DIMENSIONATTRIBUTEVALUE.ENTITYINSTANCE = subsection.RECID")
        'sbSql.AppendLine(" INNER JOIN DIMENSIONATTRIBUTE ON DIMENSIONATTRIBUTEVALUE.DIMENSIONATTRIBUTE = DIMENSIONATTRIBUTE.RECID")
        'sbSql.AppendLine(" WHERE section.DESCRIPTION = @Section")
        'sbSql.AppendLine(" AND DIMENSIONATTRIBUTE.NAME = 'D3_Subsection'")

        sbSql.AppendLine(" SELECT VALUE,DIMENSIONFINANCIALTAG.RECID FROM DIMENSIONFINANCIALTAG ")
        sbSql.AppendLine(" INNER JOIN DIMENSIONATTRIBUTEDIRCATEGORY ON DIMENSIONFINANCIALTAG.FINANCIALTAGCATEGORY=DIMENSIONATTRIBUTEDIRCATEGORY.DIRCATEGORY")
        sbSql.AppendLine(" INNER JOIN DIMENSIONATTRIBUTE ON DIMENSIONATTRIBUTE.RECID=DIMENSIONATTRIBUTEDIRCATEGORY.DIMENSIONATTRIBUTE")
        sbSql.AppendLine(" WHERE NAME='D3_Subsection' AND ECL_SHORTNAME=@Section")
        'sbSql.AppendLine(" GROUP BY VALUE ,DIMENSIONFINANCIALTAG.RECID")
        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@Section", strSection)
        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.GetData(sbSql.ToString, "tbSection", connPurchase, param).Tables("tbSection")
    End Function

    'Public Function getSubSectionValueID(ByVal strSubSection As String) As DataTable
    '    Dim sbSql As New StringBuilder
    '    sbSql.AppendLine(" SELECT section.VALUE ID, subsection.VALUE VALUE FROM DimensionFinancialTag section")
    '    sbSql.AppendLine(" INNER JOIN DIMENSIONATTRIBUTEVALUE ON section.VALUE = DIMENSIONATTRIBUTEVALUE.GROUPDIMENSION")
    '    sbSql.AppendLine(" INNER JOIN DimensionFinancialTag subsection ON DIMENSIONATTRIBUTEVALUE.ENTITYINSTANCE = subsection.RECID")
    '    sbSql.AppendLine(" INNER JOIN DIMENSIONATTRIBUTE ON DIMENSIONATTRIBUTEVALUE.DIMENSIONATTRIBUTE = DIMENSIONATTRIBUTE.RECID")
    '    sbSql.AppendLine(" WHERE subsection.VALUE = @SubSection")
    '    sbSql.AppendLine(" AND DIMENSIONATTRIBUTE.NAME = 'D3_Subsection'")
    '    Dim param As SqlParameterCollection = New SqlCommand().Parameters
    '    param.AddWithValue("@SubSection", strSubSection)
    '    Dim cDBSQL As New Common.cDBSQL
    '    Return cDBSQL.GetData(sbSql.ToString, "tbSubSection", connPurchase, param).Tables("tbSubSection")
    'End Function

    Public Function getFactoryIDValue(ByVal strFactory As String) As DataTable
        Dim sbSql As New StringBuilder
        sbSql.AppendLine(" SELECT DimensionAttributeValue.recid,DimensionFinancialTag.VALUE")
        sbSql.AppendLine(" FROM DimensionAttributeValue ")
        sbSql.AppendLine(" INNER JOIN DimensionFinancialTag ON DimensionAttributeValue.ENTITYINSTANCE = DimensionFinancialTag.RecId")
        sbSql.AppendLine(" INNER JOIN DimensionAttribute ON DIMENSIONATTRIBUTE = DimensionAttribute.RecId")
        sbSql.AppendLine(" WHERE DimensionAttribute.NAME = 'D1_Factory'")
        'sbSql.AppendLine(" AND DimensionFinancialTag.VALUE = @Factory")
        sbSql.AppendLine(" AND DimensionFinancialTag.ECL_SHORTNAME = @Factory")
        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@Factory", strFactory)
        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.GetData(sbSql.ToString, "tbFactory", connPurchase, param).Tables("tbFactory")
    End Function

    Public Function getSectionIDValue(ByVal strSection As String) As DataTable
        Dim sbSql As New StringBuilder
        sbSql.AppendLine(" SELECT DimensionAttributeValue.RecId,DimensionFinancialTag.VALUE")
        sbSql.AppendLine(" FROM DimensionAttributeValue")
        sbSql.AppendLine(" INNER JOIN DimensionFinancialTag ON DimensionAttributeValue.ENTITYINSTANCE=DimensionFinancialTag.RecId")
        sbSql.AppendLine(" INNER JOIN DimensionAttribute ON DIMENSIONATTRIBUTE=DimensionAttribute.RecId")
        sbSql.AppendLine(" WHERE DimensionAttribute.NAME='D2_Section'")
        'sbSql.AppendLine(" AND DimensionFinancialTag.[DESCRIPTION] = @Section") 'Information Technology' section from user login
        sbSql.AppendLine(" AND DimensionFinancialTag.ECL_SHORTNAME = @Section")
        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@Section", strSection)
        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.GetData(sbSql.ToString, "tbSection", connPurchase, param).Tables("tbSection")
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="strSection">User Section from PR_Online(ECL_SHORTNAME)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getSectionIDValueByUserSection(ByVal strSection As String) As DataTable
        Dim sbSql As New StringBuilder
        sbSql.AppendLine(" SELECT DimAttrVal2.RECID,DimAttrVal.GROUPDIMENSION VALUE")
        sbSql.AppendLine(" FROM DIMENSIONATTRIBUTE DimSubSec")
        sbSql.AppendLine(" INNER JOIN DIMENSIONATTRIBUTEDIRCATEGORY ON DimSubSec.RECID=DIMENSIONATTRIBUTEDIRCATEGORY.DIMENSIONATTRIBUTE")
        sbSql.AppendLine(" INNER JOIN DIMENSIONFINANCIALTAG SubSec ON DIMENSIONATTRIBUTEDIRCATEGORY.DIRCATEGORY=SubSec.FINANCIALTAGCATEGORY")
        sbSql.AppendLine(" INNER JOIN DIMENSIONATTRIBUTEVALUE DimAttrVal ON SubSec.RECID=DimAttrVal.ENTITYINSTANCE")
        sbSql.AppendLine(" INNER JOIN DIMENSIONFINANCIALTAG Sec ON DimAttrVal.GROUPDIMENSION=Sec.VALUE")
        sbSql.AppendLine(" INNER JOIN DIMENSIONATTRIBUTEVALUE DimAttrVal2 ON Sec.RECID=DimAttrVal2.ENTITYINSTANCE")
        sbSql.AppendLine(" INNER JOIN DIMENSIONATTRIBUTE DimSec ON DimAttrVal2.DIMENSIONATTRIBUTE=DimSec.RecID")
        sbSql.AppendLine(" WHERE DimSubSec.NAME='D3_Subsection'")
        sbSql.AppendLine(" AND DimSec.NAME='D2_Section'")
        sbSql.AppendLine(" AND SubSec.ECL_SHORTNAME=@Section")
        sbSql.AppendLine(" GROUP BY DimAttrVal2.RECID,DimAttrVal.GROUPDIMENSION")

        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@Section", strSection)
        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.GetData(sbSql.ToString, "tbSection", connPurchase, param).Tables("tbSection")
    End Function

    Public Function getSectionIDValueBySection_SubSection(ByVal strSection As String, ByVal strSubSection As String) As DataTable
        Dim sbSql As New StringBuilder
        sbSql.AppendLine(" SELECT DimAttrVal2.RECID,DimAttrVal.GROUPDIMENSION VALUE")
        sbSql.AppendLine(" FROM DIMENSIONATTRIBUTE DimSubSec")
        sbSql.AppendLine(" INNER JOIN DIMENSIONATTRIBUTEDIRCATEGORY ON DimSubSec.RECID=DIMENSIONATTRIBUTEDIRCATEGORY.DIMENSIONATTRIBUTE")
        sbSql.AppendLine(" INNER JOIN DIMENSIONFINANCIALTAG SubSec ON DIMENSIONATTRIBUTEDIRCATEGORY.DIRCATEGORY=SubSec.FINANCIALTAGCATEGORY")
        sbSql.AppendLine(" INNER JOIN DIMENSIONATTRIBUTEVALUE DimAttrVal ON SubSec.RECID=DimAttrVal.ENTITYINSTANCE")
        sbSql.AppendLine(" INNER JOIN DIMENSIONFINANCIALTAG Sec ON DimAttrVal.GROUPDIMENSION=Sec.VALUE")
        sbSql.AppendLine(" INNER JOIN DIMENSIONATTRIBUTEVALUE DimAttrVal2 ON Sec.RECID=DimAttrVal2.ENTITYINSTANCE")
        sbSql.AppendLine(" INNER JOIN DIMENSIONATTRIBUTE DimSec ON DimAttrVal2.DIMENSIONATTRIBUTE=DimSec.RecID")
        sbSql.AppendLine(" WHERE DimSubSec.NAME='D3_Subsection'")
        sbSql.AppendLine(" AND DimSec.NAME='D2_Section'")
        sbSql.AppendLine(" AND SubSec.ECL_SHORTNAME=@Section")
        sbSql.AppendLine(" AND SubSec.VALUE=@SubSection")
        sbSql.AppendLine(" GROUP BY DimAttrVal2.RECID,DimAttrVal.GROUPDIMENSION")

        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@Section", strSection)
        param.AddWithValue("@SubSection", strSubSection)
        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.GetData(sbSql.ToString, "tbSection", connPurchase, param).Tables("tbSection")
    End Function

    Public Function getSubSectionIDValue(ByVal strSubSection As String) As DataTable
        Dim sbSql As New StringBuilder
        sbSql.AppendLine(" SELECT DimensionAttributeValue.RecId,DimensionFinancialTag.VALUE")
        sbSql.AppendLine(" FROM DimensionAttributeValue")
        sbSql.AppendLine(" INNER JOIN DimensionFinancialTag ON DimensionAttributeValue.ENTITYINSTANCE=DimensionFinancialTag.RecId")
        sbSql.AppendLine(" INNER JOIN DimensionAttribute ON DIMENSIONATTRIBUTE=DimensionAttribute.RecId")
        sbSql.AppendLine(" WHERE DimensionAttribute.NAME='D3_SubSection'")
        sbSql.AppendLine(" AND DimensionFinancialTag.VALUE = @SubSection") '' subsection from section in item
        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@SubSection", strSubSection)
        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.GetData(sbSql.ToString, "tbSubSection", connPurchase, param).Tables("tbSubSection")
    End Function

    Public Function getRequisitionAX(ByVal RQNNUMBER As String) As DataTable
        Dim sbSql As New StringBuilder
        sbSql.AppendLine(" SELECT PURCHREQID AS RQNNUMBER ")
        sbSql.AppendLine(" , CONVERT(CHAR(10),CREATEDDATETIME,103) AS POSTDATE ")
        sbSql.AppendLine(" , PURCHREQNAME AS REQUESTBY ")
        sbSql.AppendLine(" , ECL_PRAMOUNT AS PRAMOUNT ")
        sbSql.AppendLine(" , CONVERT(CHAR(10),REQUIREDDATE,103) AS EXPARRIVALH ")
        sbSql.AppendLine(" , HOYA_REFERENCE AS REFERENCE ")
        sbSql.AppendLine(" FROM PURCHREQTABLE ")
        'sbSql.AppendLine("  INNER JOIN PURCHREQLINE ON PURCHREQTABLE.RECID=PURCHREQLINE.PURCHREQTABLE ")
        sbSql.AppendLine(" WHERE PURCHREQID = @RQNNUMBER")
        'sbSql.AppendLine(" ORDER BY PURCHREQLINE.LINENUM")
        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@RQNNUMBER", RQNNUMBER.ToUpper)
        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.GetData(sbSql.ToString, "tbRequisition", connPurchase, param).Tables("tbRequisition")
    End Function

    Public Function getRequisitionListAX(ByVal purchReqId As String) As DataTable
        Dim sbSql As New StringBuilder
        sbSql.AppendLine(" SELECT PURCHREQNAME")
        sbSql.AppendLine(" ,ITEMID, NAME, PURCHQTY")
        sbSql.AppendLine(" FROM PURCHREQTABLE ")
        sbSql.AppendLine("  INNER JOIN PURCHREQLINE ON PURCHREQTABLE.RECID=PURCHREQLINE.PURCHREQTABLE ")
        sbSql.AppendLine(" WHERE UPPER(LTRIM(RTRIM(PURCHREQID))) = @PURCHREQID")
        sbSql.AppendLine(" ORDER BY PURCHREQLINE.LINENUM")
        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@PURCHREQID", purchReqId.ToUpper)
        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.GetData(sbSql.ToString, "tbRequisition", connPurchase, param).Tables("tbRequisition")
    End Function

    'Public Function getItemByItemCodeAX(ByVal strItemCode As String) As DataTable
    '    Dim sbSql As New StringBuilder
    '    sbSql.AppendLine(" SELECT InventTable.PRODUCT, InventTable.ITEMID, EcoResProductTranslation.NAME, EcoResProductDimensionGroup.NAME AS ProductDimension ")
    '    sbSql.AppendLine(" FROM InventTable ")
    '    sbSql.AppendLine(" INNER JOIN EcoResProductTranslation ON InventTable.Product = EcoResProductTranslation.Product ")
    '    sbSql.AppendLine(" LEFT OUTER JOIN EcoResProductDimensionGroupProduct ON InventTable.Product=EcoResProductDimensionGroupProduct.Product")
    '    sbSql.AppendLine(" LEFT OUTER JOIN EcoResProductDimensionGroup ON EcoResProductDimensionGroupProduct.ProductDimensionGroup=EcoResProductDimensionGroup.RECID")
    '    sbSql.AppendLine(" LEFT OUTER JOIN InventItemPurchSetup ON InventItemPurchSetup.ItemID=InventTable.ItemID")
    '    sbSql.AppendLine(" WHERE LOWER(InventTable.ITEMID) = '" & strItemCode.ToLower & "' AND InventItemPurchSetup.STOPPED<>1")
    '    Dim cDBSQL As New Common.cDBSQL
    '    Return cDBSQL.GetData(sbSql.ToString, "tbItem", connPurchase).Tables("tbItem")
    'End Function

    Public Function getItemByItemCodeAX(ByVal strItemCode As String, ByVal strFilterItemCode As String, ByVal strDomain As String, ByVal boolList As Boolean) As DataTable
        Dim sbSql As New StringBuilder
        sbSql.AppendLine(" SELECT InventTable.PRODUCT, InventTable.ITEMID, EcoResProductTranslation.NAME, EcoResProductDimensionGroup.NAME AS ProductDimension ")
        sbSql.AppendLine(" FROM InventTable ")
        sbSql.AppendLine(" INNER JOIN ECORESPRODUCTCATEGORY ON INVENTTABLE.PRODUCT=ECORESPRODUCTCATEGORY.PRODUCT")
        sbSql.AppendLine(" INNER JOIN EcoResCategoryTranslation ON ECORESPRODUCTCATEGORY.CATEGORY=EcoResCategoryTranslation.CATEGORY")
        sbSql.AppendLine(" INNER JOIN EcoResProductTranslation ON InventTable.Product = EcoResProductTranslation.Product ")
        sbSql.AppendLine(" LEFT OUTER JOIN EcoResProductDimensionGroupProduct ON InventTable.Product=EcoResProductDimensionGroupProduct.Product")
        sbSql.AppendLine(" LEFT OUTER JOIN EcoResProductDimensionGroup ON EcoResProductDimensionGroupProduct.ProductDimensionGroup=EcoResProductDimensionGroup.RECID")
        sbSql.AppendLine(" LEFT OUTER JOIN InventItemPurchSetup ON InventItemPurchSetup.ItemID=InventTable.ItemID")
        sbSql.AppendLine(" WHERE LOWER(InventTable.ITEMID) LIKE '%" & strFilterItemCode.ToLower & "' AND InventItemPurchSetup.STOPPED<>1")
        sbSql.AppendLine(" AND EcoResCategoryTranslation.SearchText LIKE '%" & strDomain & "%'")
        If boolList Then
            strItemCode = strItemCode.ToLower.Replace(" ", "%").Replace("*", "%").Replace("-", "")
            sbSql.AppendLine(" AND LOWER(REPLACE(InventTable.ITEMID,'-','')) LIKE '%" & strItemCode & "%'")
        Else
            sbSql.AppendLine(" AND LOWER(InventTable.ITEMID) = '" & strItemCode.ToLower & "'")
        End If
        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.GetData(sbSql.ToString, "tbItem", connPurchase).Tables("tbItem")
    End Function

    Public Function getItemByItemCodeAX(ByVal strItemCode As String, ByVal strFilterItemCode As String, ByVal strDomain As String, ByVal count As Integer) As DataTable
        Dim sbSql As New StringBuilder
        strItemCode = strItemCode.ToLower.Replace(" ", "%").Replace("*", "%")
        sbSql.AppendLine(" SELECT TOP " & count & " InventTable.PRODUCT, InventTable.ITEMID, EcoResProductTranslation.NAME , EcoResProductDimensionGroup.NAME AS ProductDimension")
        sbSql.AppendLine(" FROM InventTable ")
        sbSql.AppendLine(" INNER JOIN ECORESPRODUCTCATEGORY ON INVENTTABLE.PRODUCT=ECORESPRODUCTCATEGORY.PRODUCT")
        sbSql.AppendLine(" INNER JOIN EcoResCategoryTranslation ON ECORESPRODUCTCATEGORY.CATEGORY=EcoResCategoryTranslation.CATEGORY")
        sbSql.AppendLine(" INNER JOIN EcoResProductTranslation ON InventTable.PRODUCT = EcoResProductTranslation.PRODUCT ")
        sbSql.AppendLine(" LEFT OUTER JOIN EcoResProductDimensionGroupProduct ON InventTable.Product=EcoResProductDimensionGroupProduct.Product")
        sbSql.AppendLine(" LEFT OUTER JOIN EcoResProductDimensionGroup ON EcoResProductDimensionGroupProduct.ProductDimensionGroup=EcoResProductDimensionGroup.RECID")
        sbSql.AppendLine(" LEFT OUTER JOIN InventItemPurchSetup ON InventItemPurchSetup.ItemID=InventTable.ItemID")
        'sbSql.AppendLine(" WHERE LOWER(ITEMID) LIKE '%" & strItemCode.ToLower & "%" & strFilterItemCode.ToLower & "'")
        sbSql.AppendLine(" WHERE LOWER(InventTable.ITEMID) LIKE '%" & strFilterItemCode.ToLower & "' AND InventItemPurchSetup.STOPPED<>1")
        sbSql.AppendLine(" AND EcoResCategoryTranslation.SearchText LIKE '%" & strDomain & "%'")
        sbSql.AppendLine(" AND ( LOWER(InventTable.ITEMID) LIKE '%" & strItemCode & "%'")
        sbSql.AppendLine(" OR LOWER(REPLACE(InventTable.ITEMID,'-','')) LIKE '%" & strItemCode.Replace("-", "") & "%')")
        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.GetData(sbSql.ToString, "tbItem", connPurchase).Tables("tbItem")
    End Function

    Public Function getItemByItemDescAX(ByVal strItemDesc As String, ByVal strFilterItemCode As String, ByVal strDomain As String) As DataTable
        Dim sbSql As New StringBuilder
        'sbSql.AppendLine(" SELECT ITEMID, NAME ")
        sbSql.AppendLine(" SELECT InventTable.PRODUCT, InventTable.ITEMID, EcoResProductTranslation.NAME , EcoResProductDimensionGroup.NAME AS ProductDimension")
        sbSql.AppendLine(" FROM INVENTTABLE ")
        sbSql.AppendLine(" INNER JOIN ECORESPRODUCTCATEGORY ON INVENTTABLE.PRODUCT=ECORESPRODUCTCATEGORY.PRODUCT")
        sbSql.AppendLine(" INNER JOIN EcoResCategoryTranslation ON ECORESPRODUCTCATEGORY.CATEGORY=EcoResCategoryTranslation.CATEGORY")
        sbSql.AppendLine(" INNER JOIN EcoResProductTranslation ON INVENTTABLE.PRODUCT = EcoResProductTranslation.PRODUCT ")
        sbSql.AppendLine(" LEFT OUTER JOIN EcoResProductDimensionGroupProduct ON InventTable.Product=EcoResProductDimensionGroupProduct.Product")
        sbSql.AppendLine(" LEFT OUTER JOIN EcoResProductDimensionGroup ON EcoResProductDimensionGroupProduct.ProductDimensionGroup=EcoResProductDimensionGroup.RECID")
        sbSql.AppendLine(" LEFT OUTER JOIN InventItemPurchSetup ON InventItemPurchSetup.ItemID=InventTable.ItemID")
        sbSql.AppendLine(" WHERE LOWER(EcoResProductTranslation.NAME) LIKE @NAME")
        sbSql.AppendLine(" AND LOWER(InventTable.ITEMID) LIKE '%" & strFilterItemCode.ToLower & "'")
        sbSql.AppendLine(" AND InventItemPurchSetup.STOPPED<>1")
        sbSql.AppendLine(" AND EcoResCategoryTranslation.SearchText LIKE '%" & strDomain & "%'")
        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@NAME", "%" & strItemDesc.ToLower.Replace(" ", "%") & "%")
        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.GetData(sbSql.ToString, "tbItem", connPurchase, param).Tables("tbItem")
    End Function

    Public Function getItemByItemDescAX(ByVal strItemDesc As String, ByVal strFilterItemCode As String, ByVal strDomain As String, ByVal count As Integer) As DataTable
        Dim sbSql As New StringBuilder
        sbSql.AppendLine(" SELECT TOP " & count & " InventTable.ITEMID, NAME ")
        sbSql.AppendLine(" FROM INVENTTABLE ")
        sbSql.AppendLine(" INNER JOIN ECORESPRODUCTCATEGORY ON INVENTTABLE.PRODUCT=ECORESPRODUCTCATEGORY.PRODUCT")
        sbSql.AppendLine(" INNER JOIN EcoResCategoryTranslation ON ECORESPRODUCTCATEGORY.CATEGORY=EcoResCategoryTranslation.CATEGORY")
        sbSql.AppendLine(" INNER JOIN EcoResProductTranslation ON INVENTTABLE.PRODUCT = EcoResProductTranslation.PRODUCT ")
        sbSql.AppendLine(" LEFT OUTER JOIN InventItemPurchSetup ON InventItemPurchSetup.ItemID=InventTable.ItemID")
        sbSql.AppendLine(" WHERE LOWER(NAME) LIKE @NAME")
        sbSql.AppendLine(" AND LOWER(InventTable.ITEMID) LIKE '%" & strFilterItemCode.ToLower & "'")
        sbSql.AppendLine(" AND InventItemPurchSetup.STOPPED<>1")
        sbSql.AppendLine(" AND EcoResCategoryTranslation.SearchText LIKE '%" & strDomain & "%'")
        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@NAME", "%" & strItemDesc.ToLower.Replace(" ", "%") & "%")
        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.GetData(sbSql.ToString, "tbItem", connPurchase, param).Tables("tbItem")
    End Function

    Public Function getSiteAX() As DataTable
        Dim sbSql As New StringBuilder
        sbSql.AppendLine(" SELECT DISTINCT INVENTSITEID ")
        sbSql.AppendLine(" FROM INVENTDIM ")
        sbSql.AppendLine(" WHERE INVENTSITEID<>''")
        sbSql.AppendLine(" ORDER BY INVENTSITEID")
        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.GetData(sbSql.ToString, "tbSite", connPurchase).Tables("tbSite")
    End Function

    Public Function getWHAX(ByVal strSite As String) As DataTable
        Dim sbSql As New StringBuilder
        sbSql.AppendLine(" SELECT DISTINCT INVENTLOCATIONID, INVENTDIMID ")
        sbSql.AppendLine(" FROM INVENTDIM ")
        sbSql.AppendLine(" WHERE INVENTSITEID=@INVENTSITEID")
        sbSql.AppendLine("  AND INVENTLOCATIONID<>'' ")
        sbSql.AppendLine("  AND INVENTSIZEID='' ")
        sbSql.AppendLine("  AND INVENTCOLORID='' ")
        sbSql.AppendLine(" ORDER BY INVENTLOCATIONID")
        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@INVENTSITEID", strSite)
        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.GetData(sbSql.ToString, "tbWH", connPurchase, param).Tables("tbWH")
    End Function

    'Public Function getSizeBySiteLocationAX(ByVal strSite As String, ByVal strLocation As String) As DataTable
    '    Dim sbSql As New StringBuilder
    '    sbSql.AppendLine(" SELECT 'Size:' + INVENTSIZEID + ' Color:' + INVENTCOLORID AS PRODUCTDIMENSION,INVENTDIMID ")
    '    sbSql.AppendLine(" FROM INVENTDIM ")
    '    sbSql.AppendLine(" WHERE INVENTSITEID=@INVENTSITEID")
    '    sbSql.AppendLine("  AND INVENTLOCATIONID=@INVENTLOCATIONID ")
    '    sbSql.AppendLine("  AND INVENTSIZEID<>'' ")
    '    sbSql.AppendLine(" ORDER BY INVENTSIZEID,INVENTCOLORID")
    '    Dim param As SqlParameterCollection = New SqlCommand().Parameters
    '    param.AddWithValue("@INVENTSITEID", strSite)
    '    param.AddWithValue("@INVENTLOCATIONID", strLocation)
    '    Dim cDBSQL As New Common.cDBSQL
    '    Return cDBSQL.GetData(sbSql.ToString, "tbSize", connPurchase, param).Tables("tbSize")
    'End Function

    Public Function getConfigByItemRecIdAX(ByVal ItemRecId As String) As DataTable
        Dim sbSql As New StringBuilder
        sbSql.AppendLine(" SELECT Name AS Config ")
        sbSql.AppendLine(" FROM ECORESPRODUCTMASTERCONFIGURATION ")
        sbSql.AppendLine("  INNER JOIN ECORESCONFIGURATION ON ECORESPRODUCTMASTERCONFIGURATION.CONFIGURATION = ECORESCONFIGURATION.RECID")
        sbSql.AppendLine(" WHERE CONFIGPRODUCTMASTER=@ItemRecId")
        sbSql.AppendLine(" ORDER BY Name")
        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@ItemRecId", ItemRecId)
        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.GetData(sbSql.ToString, "tbConfig", connPurchase, param).Tables("tbConfig")
    End Function

    Public Function getSizeByItemRecIdAX(ByVal ItemRecId As String) As DataTable
        Dim sbSql As New StringBuilder
        sbSql.AppendLine(" SELECT Name AS Size ")
        sbSql.AppendLine(" FROM ECORESPRODUCTMASTERSIZE ")
        sbSql.AppendLine("  INNER JOIN ECORESSIZE ON ECORESPRODUCTMASTERSIZE.SIZE_ = ECORESSIZE.RECID")
        sbSql.AppendLine(" WHERE SIZEPRODUCTMASTER=@ItemRecId")
        sbSql.AppendLine(" ORDER BY Name")
        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@ItemRecId", ItemRecId)
        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.GetData(sbSql.ToString, "tbSize", connPurchase, param).Tables("tbSize")
    End Function

    Public Function getColorByItemRecIdAX(ByVal ItemRecId As String) As DataTable
        Dim sbSql As New StringBuilder
        sbSql.AppendLine(" SELECT Name AS Color ")
        sbSql.AppendLine(" FROM ECORESPRODUCTMASTERCOLOR ")
        sbSql.AppendLine("  INNER JOIN ECORESCOLOR ON ECORESPRODUCTMASTERCOLOR.COLOR = ECORESCOLOR.RECID")
        sbSql.AppendLine(" WHERE COLORPRODUCTMASTER=@ItemRecId")
        sbSql.AppendLine(" ORDER BY Name")
        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@ItemRecId", ItemRecId)
        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.GetData(sbSql.ToString, "tbColor", connPurchase, param).Tables("tbColor")
    End Function

    Public Function getVendorByVendCodeAX(ByVal VendorCode As String, ByVal count As Integer) As DataTable
        Dim sbSql As New StringBuilder
        sbSql.AppendLine(" SELECT TOP " & count & " ACCOUNTNUM AS VENDORID ")
        sbSql.AppendLine(" , NAME AS VENDNAME")
        sbSql.AppendLine(" FROM VENDTABLE ")
        sbSql.AppendLine(" INNER JOIN DIRPARTYTABLE ON VENDTABLE.PARTY = DIRPARTYTABLE.RECID ")
        sbSql.AppendLine(" WHERE ")
        sbSql.AppendLine(" ACCOUNTNUM LIKE @ACCOUNTNUM")
        sbSql.AppendLine(" ORDER BY VENDORID")
        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@ACCOUNTNUM", "%" & VendorCode & "%")
        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.GetData(sbSql.ToString, "tbVendor", connPurchase, param).Tables("tbVendor")
    End Function

    Public Function getVendorByVendNameAX(ByVal VendorName As String, ByVal count As Integer) As DataTable
        Dim sbSql As New StringBuilder
        sbSql.AppendLine(" SELECT TOP " & count & " ACCOUNTNUM AS VENDORID ")
        sbSql.AppendLine(" , NAME AS VENDNAME")
        sbSql.AppendLine(" FROM VENDTABLE ")
        sbSql.AppendLine(" INNER JOIN DIRPARTYTABLE ON VENDTABLE.PARTY = DIRPARTYTABLE.RECID ")
        sbSql.AppendLine(" WHERE ")
        sbSql.AppendLine(" NAME LIKE @NAME")
        sbSql.AppendLine(" ORDER BY VENDORID")
        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@NAME", "%" & VendorName & "%")
        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.GetData(sbSql.ToString, "tbVendor", connPurchase, param).Tables("tbVendor")
    End Function

    Public Function getLastPriceByItemCodeAX(ByVal strItemID As String) As DataTable
        Dim sbSql As New StringBuilder
        sbSql.AppendLine(" SELECT PRICE ")
        sbSql.AppendLine(" , UNITID ")
        sbSql.AppendLine(" , CASE WHEN HOYA_CURRENCYCODE='' THEN 'THB' ELSE ISNULL(HOYA_CURRENCYCODE,'THB') END HOYA_CURRENCYCODE")
        sbSql.AppendLine(" , ISNULL(CURRENCY.CURRENCYCODEISO,'THB') CURRENCYCODEISO")
        'sbSql.AppendLine(" , SYMBOL ")
        sbSql.AppendLine(" FROM INVENTTABLEMODULE ")
        'sbSql.AppendLine("  LEFT OUTER JOIN UNITOFMEASURE ON INVENTTABLEMODULE.PURCHUNITOFMEASURE=UNITOFMEASURE.RECID")
        sbSql.AppendLine("  LEFT OUTER JOIN CURRENCY ON INVENTTABLEMODULE.HOYA_CurrencyCode=CURRENCY.CURRENCYCODE")
        sbSql.AppendLine(" WHERE ITEMID = @ITEMID")
        sbSql.AppendLine("  AND MODULETYPE=1")
        sbSql.AppendLine(" ORDER BY MODIFIEDDATETIME DESC")
        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@ITEMID", strItemID)
        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.GetData(sbSql.ToString, "tbItemPrice", connPurchase, param).Tables("tbItemPrice")
    End Function

    Public Function getWorkerByEmpCode(ByVal strEmpCode As String) As DataTable
        Dim sbSql As New StringBuilder
        sbSql.AppendLine(" SELECT HCMWorker.RecID, Name, LanguageID")
        sbSql.AppendLine(" FROM HCMWorker")
        sbSql.AppendLine(" LEFT OUTER JOIN DirPartyTable ON HCMWorker.Person=DIRPARTYTABLE.RecID")
        sbSql.AppendLine(" WHERE PersonnelNumber=@EmpCode")
        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@EmpCode", strEmpCode)
        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.GetData(sbSql.ToString, "tbWorker", connPurchase, param).Tables("tbWorker")
    End Function

    ''' <summary>
    ''' Find a DIMENSIONATTRIBUTEVALUESET in DIMENSIONATTRIBUTEVALUESETITEM by DISPLAYVALUE(VALUE of DIMENSIONFINANCIALTAG)
    ''' </summary>
    ''' <param name="strFacValue">Value of Factory</param>
    ''' <param name="strSecValue">Value of Section</param>
    ''' <param name="strSubSecValue">Value of SubSection</param>
    ''' <returns>DIMENSIONATTRIBUTEVALUESET</returns>
    ''' <remarks></remarks>
    Public Function getDimAttrValSetItemAX(ByVal strFacValue As String, _
                                           ByVal strSecValue As String, _
                                           ByVal strSubSecValue As String) As DataTable
        Dim sbSql As New StringBuilder
        'sbSql.AppendLine(" SELECT A.DIMENSIONATTRIBUTEVALUESET,A.DIMENSIONATTRIBUTEVALUE FAC,B.DIMENSIONATTRIBUTEVALUE SEC,C.DIMENSIONATTRIBUTEVALUE PRO")
        'sbSql.AppendLine(" FROM DIMENSIONATTRIBUTEVALUESETITEM A")
        'sbSql.AppendLine(" INNER JOIN DIMENSIONATTRIBUTEVALUESETITEM B ON A.DIMENSIONATTRIBUTEVALUESET=B.DIMENSIONATTRIBUTEVALUESET")
        'sbSql.AppendLine(" INNER JOIN DIMENSIONATTRIBUTEVALUESETITEM C ON A.DIMENSIONATTRIBUTEVALUESET=C.DIMENSIONATTRIBUTEVALUESET")
        'sbSql.AppendLine(" WHERE A.DIMENSIONATTRIBUTEVALUE=@FacId ")
        'sbSql.AppendLine("   AND B.DIMENSIONATTRIBUTEVALUE=@SecId ")
        'sbSql.AppendLine("   AND NOT(A.DIMENSIONATTRIBUTEVALUESET IN ")
        'sbSql.AppendLine("          (SELECT DIMENSIONATTRIBUTEVALUESET FROM DIMENSIONATTRIBUTEVALUESETITEM WHERE DIMENSIONATTRIBUTEVALUE IN ('" & strRltId & "') )")
        'sbSql.AppendLine("     )")
        'sbSql.AppendLine("   AND C.DIMENSIONATTRIBUTEVALUE=@ProId ")
        'Dim param As SqlParameterCollection = New SqlCommand().Parameters
        'param.AddWithValue("@FacId", strFacId)
        'param.AddWithValue("@SecId", strSecId)
        'param.AddWithValue("@ProId", strProId)
        'sbSql.AppendLine("SELECT B.DIMENSIONATTRIBUTEVALUESET")
        'sbSql.AppendLine("FROM         DIMENSIONATTRIBUTEVALUESETITEM A INNER JOIN DIMENSIONATTRIBUTEVALUESETITEM B ON A.DIMENSIONATTRIBUTEVALUESET=B.DIMENSIONATTRIBUTEVALUESET")
        'sbSql.AppendLine("WHERE     A.DIMENSIONATTRIBUTEVALUE=" & strSecId)
        'sbSql.AppendLine("GROUP BY B.DIMENSIONATTRIBUTEVALUESET")
        'sbSql.AppendLine("        HAVING(COUNT(B.DIMENSIONATTRIBUTEVALUE) = 2)")
        sbSql.AppendLine(" SELECT * FROM ")
        sbSql.AppendLine("  (SELECT DIMENSIONATTRIBUTEVALUESETITEM.* FROM DIMENSIONATTRIBUTE ")
        sbSql.AppendLine("      INNER JOIN DIMENSIONATTRIBUTEVALUE ")
        sbSql.AppendLine("          ON DIMENSIONATTRIBUTE.RECID = DIMENSIONATTRIBUTEVALUE.DIMENSIONATTRIBUTE")
        sbSql.AppendLine("      INNER JOIN DIMENSIONATTRIBUTEVALUESETITEM")
        sbSql.AppendLine("          ON DIMENSIONATTRIBUTEVALUE.RECID = DIMENSIONATTRIBUTEVALUESETITEM.DIMENSIONATTRIBUTEVALUE")
        sbSql.AppendLine("  WHERE NAME = 'D1_Factory') FACTORY")
        sbSql.AppendLine(" LEFT OUTER JOIN")
        sbSql.AppendLine("  (SELECT DIMENSIONATTRIBUTEVALUESETITEM.* FROM DIMENSIONATTRIBUTE ")
        sbSql.AppendLine("      INNER JOIN DIMENSIONATTRIBUTEVALUE ")
        sbSql.AppendLine("          ON DIMENSIONATTRIBUTE.RECID = DIMENSIONATTRIBUTEVALUE.DIMENSIONATTRIBUTE")
        sbSql.AppendLine("      INNER JOIN DIMENSIONATTRIBUTEVALUESETITEM")
        sbSql.AppendLine("          ON DIMENSIONATTRIBUTEVALUE.RECID = DIMENSIONATTRIBUTEVALUESETITEM.DIMENSIONATTRIBUTEVALUE")
        sbSql.AppendLine("  WHERE NAME = 'D2_Section') SECTION ON FACTORY.DIMENSIONATTRIBUTEVALUESET = SECTION.DIMENSIONATTRIBUTEVALUESET")
        sbSql.AppendLine(" LEFT OUTER JOIN")

        sbSql.AppendLine("  (SELECT DIMENSIONATTRIBUTEVALUESETITEM.* FROM DIMENSIONATTRIBUTE ")
        sbSql.AppendLine("      INNER JOIN DIMENSIONATTRIBUTEVALUE ")
        sbSql.AppendLine("          ON DIMENSIONATTRIBUTE.RECID = DIMENSIONATTRIBUTEVALUE.DIMENSIONATTRIBUTE")
        sbSql.AppendLine("      INNER JOIN DIMENSIONATTRIBUTEVALUESETITEM")
        sbSql.AppendLine("          ON DIMENSIONATTRIBUTEVALUE.RECID = DIMENSIONATTRIBUTEVALUESETITEM.DIMENSIONATTRIBUTEVALUE")
        sbSql.AppendLine("  WHERE NAME = 'D3_SubSection') SUBSECTION ON FACTORY.DIMENSIONATTRIBUTEVALUESET = SUBSECTION.DIMENSIONATTRIBUTEVALUESET")
        sbSql.AppendLine(" LEFT OUTER JOIN")

        sbSql.AppendLine("  (SELECT DIMENSIONATTRIBUTEVALUESETITEM.* FROM DIMENSIONATTRIBUTE ")
        sbSql.AppendLine("      INNER JOIN DIMENSIONATTRIBUTEVALUE ")
        sbSql.AppendLine("          ON DIMENSIONATTRIBUTE.RECID = DIMENSIONATTRIBUTEVALUE.DIMENSIONATTRIBUTE")
        sbSql.AppendLine("      INNER JOIN DIMENSIONATTRIBUTEVALUESETITEM")
        sbSql.AppendLine("          ON DIMENSIONATTRIBUTEVALUE.RECID = DIMENSIONATTRIBUTEVALUESETITEM.DIMENSIONATTRIBUTEVALUE")
        sbSql.AppendLine("  WHERE NAME = 'D4_Related') RELATED ON FACTORY.DIMENSIONATTRIBUTEVALUESET = RELATED.DIMENSIONATTRIBUTEVALUESET")
        sbSql.AppendLine(" LEFT OUTER JOIN")
        sbSql.AppendLine("  (SELECT DIMENSIONATTRIBUTEVALUESETITEM.* FROM DIMENSIONATTRIBUTE ")
        sbSql.AppendLine("      INNER JOIN DIMENSIONATTRIBUTEVALUE ")
        sbSql.AppendLine("          ON DIMENSIONATTRIBUTE.RECID = DIMENSIONATTRIBUTEVALUE.DIMENSIONATTRIBUTE")
        sbSql.AppendLine("      INNER JOIN DIMENSIONATTRIBUTEVALUESETITEM")
        sbSql.AppendLine("          ON DIMENSIONATTRIBUTEVALUE.RECID = DIMENSIONATTRIBUTEVALUESETITEM.DIMENSIONATTRIBUTEVALUE")
        sbSql.AppendLine("  WHERE NAME = 'D5_Project') PROJECT ON FACTORY.DIMENSIONATTRIBUTEVALUESET = PROJECT.DIMENSIONATTRIBUTEVALUESET")
        sbSql.AppendLine(" WHERE FACTORY.DISPLAYVALUE = @FacValue")
        sbSql.AppendLine("  AND SECTION.DISPLAYVALUE = @SecValue")
        sbSql.AppendLine("  AND SUBSECTION.DISPLAYVALUE = @SubSecValue")
        sbSql.AppendLine("  AND RELATED.DISPLAYVALUE = 'NN'") 'NN General Transaction
        sbSql.AppendLine("  AND PROJECT.DISPLAYVALUE IS NULL")
        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@FacValue", strFacValue)
        param.AddWithValue("@SecValue", strSecValue) 'section
        param.AddWithValue("@SubSecValue", strSubSecValue) 'subsection
        'param.AddWithValue("@RltId", strRltId)
        'param.AddWithValue("@ProId", strProId)
        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.GetData(sbSql.ToString, "tbDimAttrValSetItem", connPurchase, param).Tables("tbDimAttrValSetItem")
    End Function

    Public Function UpdateNextRQNNUMBER(ByVal NextRQN As String) As Common.cDBSQL.ExecuteReturn
        Dim sbSql As New StringBuilder
        sbSql.AppendLine(" UPDATE POOPT ")
        sbSql.AppendLine(" SET RQNBODYD=@RQNNUMBER ")
        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@RQNNUMBER", NextRQN)
        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.ExecuteData(sbSql.ToString, connPurchase, param, Trans)
    End Function

    Public Function UpdatePurchReqTable(ByVal RequestHOBJ As RequestHOBJ) As Common.cDBSQL.ExecuteReturn
        Dim sbSql As New StringBuilder
        sbSql.AppendLine(" UPDATE PURCHREQTABLE SET ")
        sbSql.AppendLine(" HOYA_REFERENCE = @REFERENCE ")
        sbSql.AppendLine(" , ECL_PRAMOUNT = @PRAMOUNT ")
        'sbSql.AppendLine(" , SUBMITTEDDATETIMETZID=37001 ")
        sbSql.AppendLine(" WHERE PURCHREQID = @PURCHREQID")
        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@PRAMOUNT", RequestHOBJ.axPRAmount)
        param.AddWithValue("@REFERENCE", RequestHOBJ.rqREFERENCE)
        param.AddWithValue("@PURCHREQID", RequestHOBJ.rqNUMBER)

        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.ExecuteData(sbSql.ToString, connPurchase, param, Trans)
    End Function

    Public Function UpdatePurchReqTable_RequiredDate(ByVal RequestHOBJ As RequestHOBJ) As Common.cDBSQL.ExecuteReturn
        Dim sbSql As New StringBuilder
        sbSql.AppendLine(" UPDATE PURCHREQTABLE SET ")
        sbSql.AppendLine(" REQUIREDDATE = @REQUIREDDATE ")
        sbSql.AppendLine(" WHERE PURCHREQID = @PURCHREQID")
        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@REQUIREDDATE", RequestHOBJ.rqEXPARRIVAL)
        param.AddWithValue("@PURCHREQID", RequestHOBJ.rqNUMBER)
        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.ExecuteData(sbSql.ToString, connPurchase, param, Trans)
    End Function

    Public Function UpdatePurchReqLineByLineNum(ByVal RequestLOBJ As RequestLOBJ, ByVal strHRecID As String) As Common.cDBSQL.ExecuteReturn
        Dim sbSql As New StringBuilder
        sbSql.AppendLine(" UPDATE PURCHREQLINE SET ")
        'sbSql.AppendLine(" REQUISITIONSTATUS = 10 ")
        sbSql.AppendLine(" ECL_REMARK = @REMARK ")
        sbSql.AppendLine(" WHERE PURCHREQTABLE=@RECID AND LINENUM = @LINENUM")
        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@REMARK", RequestLOBJ.rqCOMMENT)
        param.AddWithValue("@RECID", strHRecID)
        param.AddWithValue("@LINENUM", RequestLOBJ.rqLINENUM)
        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.ExecuteData(sbSql.ToString, connPurchase, param, Trans)
    End Function

    Public Function UpdatePurchReqLineByLRecID(ByVal RequestLOBJ As RequestLOBJ) As Common.cDBSQL.ExecuteReturn
        Dim sbSql As New StringBuilder
        sbSql.AppendLine(" UPDATE PURCHREQLINE ")
        sbSql.AppendLine(" SET ECL_REMARK = @REMARK ")
        sbSql.AppendLine(" , PURCHQTY = @PURCHQTY ")
        sbSql.AppendLine(" , REQUIREDDATE = @REQUIREDDATE ")
        sbSql.AppendLine(" WHERE RECID=@RECID")
        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@REMARK", RequestLOBJ.rqCOMMENT)
        param.AddWithValue("@RECID", RequestLOBJ.rqRECID)
        param.AddWithValue("@LINENUM", RequestLOBJ.rqLINENUM)
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
