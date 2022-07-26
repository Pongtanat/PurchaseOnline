Imports System.Data.SqlClient
Imports System.Data
Imports System.Text
Imports Microsoft.Dynamics
'Imports Microsoft.Dynamics.BusinessConnectorNet

Public Class RequisitionBLL

    Private connDB As String = Nothing
    Private _applicantAdapter As RequisitionDAL

    Sub New(ByVal DB As String)
        connDB = DB
    End Sub

    Protected ReadOnly Property Adapter() As RequisitionDAL
        Get
            If _applicantAdapter Is Nothing Then
                _applicantAdapter = New RequisitionDAL(connDB)
            End If

            Return _applicantAdapter
        End Get
    End Property

    Public Function getSiteAX() As DataTable
        Return Adapter.getSiteAX
    End Function

    Public Function getWHAX(ByVal strSite As String) As DataTable
        Return Adapter.getWHAX(strSite)
    End Function

    Public Function getConfigByItemRecIdAX(ByVal strItemRecId As String) As DataTable
        Return Adapter.getConfigByItemRecIdAX(strItemRecId)
    End Function

    Public Function getSizeByItemRecIdAX(ByVal strItemRecId As String) As DataTable
        Return Adapter.getSizeByItemRecIdAX(strItemRecId)
    End Function

    Public Function getColorByItemRecIdAX(ByVal strItemRecId As String) As DataTable
        Return Adapter.getColorByItemRecIdAX(strItemRecId)
    End Function

    Public Function getItemByItemCodeAX(ByVal strItemCode As String, ByVal strFilterItemCode As String, ByVal strDomain As String, Optional ByVal boolList As Boolean = True) As DataTable
        'Dim dt As New DataTable
        'Dim AX As New Microsoft.Dynamics.BusinessConnectorNet.Axapta
        'Try
        '    AX.Logon("", "", "", "")
        '    Dim AXRecord As Microsoft.Dynamics.BusinessConnectorNet.AxaptaRecord = AX.CreateAxaptaRecord("InventTable")
        '    AXRecord.ExecuteStmt("Select * from %1 where %1.ItemId == '" & strItemCode & "'")
        '    Dim dr As DataRow
        '    Dim colItemId As New DataColumn("ItemId", GetType(System.String))
        '    dt.Columns.Add(colItemId)
        '    Dim colItemName As New DataColumn("Name", GetType(System.String))
        '    dt.Columns.Add(colItemName)
        '    While AXRecord.Found
        '        dr = dt.NewRow
        '        dr("ItemId") = AXRecord.get_Field("ItemId").ToString()
        '        dr("Name") = AXRecord.get_Field("NameAlias").ToString()
        '        dt.Rows.Add(dr)
        '        AXRecord.Next()
        '    End While
        'Catch ex As Exception

        'End Try
        'Return dt
        Return Adapter.getItemByItemCodeAX(strItemCode, strFilterItemCode, strDomain, boolList)
    End Function

    Public Function getItemByItemCodeAX(ByVal strItemCode As String, ByVal strFilterItemCode As String, ByVal strDomain As String, ByVal count As Integer) As DataTable
        Return Adapter.getItemByItemCodeAX(strItemCode, strFilterItemCode, strDomain, count)
    End Function

    Public Function getItemByItemDescAX(ByVal strItemDesc As String, ByVal strFilterItemCode As String, ByVal strDomain As String) As DataTable
        Return Adapter.getItemByItemDescAX(strItemDesc, strFilterItemCode, strDomain)
    End Function

    Public Function getItemByItemDescAX(ByVal strItemDesc As String, ByVal strFilterItemCode As String, ByVal strDomain As String, ByVal count As Integer) As DataTable
        Return Adapter.getItemByItemDescAX(strItemDesc, strFilterItemCode, strDomain, count)
    End Function

    Public Function getAllSubSectionBySection(ByVal strSec As String) As DataTable
        Return Adapter.getAllSubSectionBySection(strSec)
    End Function

    Public Function getLastPriceByItemCodeAX(ByVal strItemID As String) As DataTable
        Return Adapter.getLastPriceByItemCodeAX(strItemID)
    End Function

    Public Function getVendorByVendorCodeAX(ByVal strItemCode As String, ByVal count As Integer) As DataTable
        Return Adapter.getVendorByVendCodeAX(strItemCode, count)
    End Function

    Public Function getVendorByVendorNameAX(ByVal strSeq As String, ByVal count As Integer) As DataTable
        Return Adapter.getVendorByVendNameAX(strSeq, count)
    End Function

    Public Function getRequisitionAX(ByVal RQNNUMBER As String) As DataTable
        Return Adapter.getRequisitionAX(RQNNUMBER)
    End Function

    Public Function getRequisitionListAX(ByVal RQNNUMBER As String) As DataTable
        Return Adapter.getRequisitionListAX(RQNNUMBER)
    End Function

    Private Function getWorkerByEmpCode(ByVal strEmpCode As String) As DataTable
        Return Adapter.getWorkerByEmpCode(strEmpCode)
    End Function

    Public Function NewRequisitionAX(ByVal RequestHOBJ As RequestHOBJ, ByVal dt As DataTable, ByVal DIMENSIONFINANCIALTAG_FACTORY As String) As String
        Dim AX As New Microsoft.Dynamics.BusinessConnectorNet.Axapta
        Dim AxPurchReqTable, AxPurchReqLines As Microsoft.Dynamics.BusinessConnectorNet.AxaptaObject
        Dim PurchReqTable, PurchReqLines, InventTable, InventDim As Microsoft.Dynamics.BusinessConnectorNet.AxaptaRecord 'NumberSequenceTable, 
        Dim strDefaultDimension, InventDimId As String
        Dim Found As Boolean = False
        Dim strMessage As String = ""
        'Dim strFormat, strSeq As String
        Dim strPurchReqId, strHRecID As String
        Try

            'AX.Logon("HOYA", "en-us", "TEST@HOAX237:2712", "TEST")
            If ConfigurationManager.AppSettings("SERVER") = "LIVE" Then
                Dim netCredential As New System.Net.NetworkCredential
                netCredential.Domain = "hoya" '"HO"
                netCredential.Password = "P@ssw0rd"
                netCredential.UserName = "hopt-axadmin" '"axuser"
                AX.LogonAs(netCredential.UserName, netCredential.Domain, netCredential, "", "", "", "")
                'AX.Logon("", "", "", "")
            Else
                AX.Logon("HOYA", "en-us", "TEST@HOAX237:2712", "TEST")
            End If


            Dim RequisitionDAL As New RequisitionDAL(connDB)

            Dim dtFactory As DataTable = RequisitionDAL.getFactoryIDValue(DIMENSIONFINANCIALTAG_FACTORY)
            If dtFactory.Rows.Count = 0 Then
                Throw New SystemException("Can not post purchase requisition. Factory not found in AX, please contact system admin.")
            End If

            Dim dtSection As DataTable = RequisitionDAL.getSectionIDValueByUserSection(RequestHOBJ.rqCOMMENT)
            If dtSection.Rows.Count = 0 Then
                Throw New SystemException("Can not post purchase requisition. Your section not found in AX, please contact system admin.")
            End If

            Dim dtWorker As DataTable = RequisitionDAL.getWorkerByEmpCode(RequestHOBJ.PRPreparerEmpCode)
            If dtWorker.Rows.Count = 0 Then
                Throw New SystemException("PR Worker does not assign.")
            End If

            Dim dtDimAttrValSetItem As DataTable
            Dim dtSubSection As DataTable

            'For Each dr As DataRow In dt.Rows
            '    dtSection = RequisitionDAL.getSectionIDValueBySection_SubSection(RequestHOBJ.rqCOMMENT, dr("MANITEMNO"))
            '    dtSubSection = RequisitionDAL.getSubSectionIDValue(dr("MANITEMNO"))

            '    If dtSubSection.Rows.Count > 0 Then
            '        dtDimAttrValSetItem = RequisitionDAL.getDimAttrValSetItemAX(dtFactory.Rows(0)("VALUE"), dtSection.Rows(0)("VALUE"), dr("MANITEMNO"))

            '        If dtDimAttrValSetItem.Rows.Count > 0 Then
            '            dr("MANITEMNO") = dtDimAttrValSetItem.Rows(0)("DIMENSIONATTRIBUTEVALUESET").ToString()
            '        Else
            '            dr("MANITEMNO") = AX.CallStaticClassMethod("HOYA_DimensionAttributeValueSetItem", "NewDimensionAttributeValueSet", _
            '                                                            dtFactory.Rows(0)("RECID"), _
            '                                                            dtFactory.Rows(0)("VALUE"), _
            '                                                            dtSection.Rows(0)("RECID"), _
            '                                                            dtSection.Rows(0)("VALUE"), _
            '                                                            dtSubSection.Rows(0)("RECID"), _
            '                                                            dtSubSection.Rows(0)("VALUE")).ToString
            '        End If
            '    Else
            '        Throw New SystemException("Your section not match with AX Financial Dimension, please contact system admin.")
            '    End If

            '    InventDim = AX.CreateAxaptaRecord("INVENTDIM")
            '    InventDim.ExecuteStmt("SELECT * FROM %1 WHERE %1.InventSiteId=='" & RequestHOBJ.axHOYA_SITE & "'" & _
            '                                             " && %1.InventLocationId=='" & RequestHOBJ.axHOYA_LOCATION & "'" & _
            '                                             " && %1.ConfigId=='" & dr("Config") & "'" & _
            '                                             " && %1.InventSizeId=='" & dr("Size") & "'" & _
            '                                             " && %1.InventColorId=='" & dr("Color") & "'")
            '    If InventDim.Found Then
            '        dr("INVENTDIMID") = InventDim.get_Field("InventDimId").ToString
            '    Else
            '        InventDim.set_Field("InventSiteId", RequestHOBJ.axHOYA_SITE)
            '        InventDim.set_Field("InventLocationId", RequestHOBJ.axHOYA_LOCATION)
            '        InventDim.set_Field("ConfigId", dr("Config"))
            '        InventDim.set_Field("InventSizeId", dr("Size"))
            '        InventDim.set_Field("InventColorId", dr("Color"))
            '        InventDim = AX.CallStaticRecordMethod("InventDim", "create", InventDim)
            '        dr("INVENTDIMID") = InventDim.get_Field("InventDimId")
            '    End If
            'Next
            '-----------------------------------------------------------------

            AX.TTSBegin()

            AxPurchReqTable = AX.CreateAxaptaObject("AxPurchReqTable")
            AxPurchReqTable.Call("parmPurchReqName", String.Format("{0}-{1}-{2}", RequestHOBJ.rqACCPAC, RequestHOBJ.rqCOMMENT, RequestHOBJ.rqREQUESTBY))
            AxPurchReqTable.Call("parmExternalSourceID", String.Format("PR{0}", Format(Now, "yyyyMMddHHmmss"))) 'RequestHOBJ.rqNUMBER
            AxPurchReqTable.Call("parmExternalSourceName", "PROnline")
            AxPurchReqTable.Call("parmRequiredDate", RequestHOBJ.rqEXPARRIVAL)
            '-----------------------------------------------------------------
            AxPurchReqTable.Call("parmSubmittedBy", "hopt-axadmin")
            AxPurchReqTable.Call("parmSubmittedDateTime", Now.AddHours(-7))
            AxPurchReqTable.Call("parmRequisitionStatus", "30") 'In review(10) , Approved(30)
            AxPurchReqTable.Call("parmStatusToBeSaved", "Approved")
            AxPurchReqTable.Call("parmOriginator", dtWorker.Rows(0)("RecID"))
            '-----------------------------------------------------------------
            AxPurchReqTable.Call("save")

            strPurchReqId = AxPurchReqTable.Call("parmPurchReqId").ToString()
            strHRecID = AxPurchReqTable.Call("parmRecId").ToString()
            RequestHOBJ.rqNUMBER = strPurchReqId
            PurchReqTable = AX.CallStaticRecordMethod("PurchReqTable", "findPurchReqId", strPurchReqId)

            PurchReqLines = AX.CreateAxaptaRecord("PurchReqLine")
            For Each dr As DataRow In dt.Rows
                InventTable = AX.CallStaticRecordMethod("InventTable", "find", dr("ItemId").ToString)
                PurchReqLines.Clear()
                PurchReqLines.InitValue()
                PurchReqLines.Call("initFromPurchReqTable", PurchReqTable)
                PurchReqLines.Call("initFromInventTable", InventTable)
                AxPurchReqLines = AX.CallStaticClassMethod("AxPurchReqLine", "newPurchReqLine", PurchReqLines)
                AxPurchReqLines.Call("parmPurchId", strPurchReqId)

                AxPurchReqLines.Call("parmCurrencyCode", dr("CURRENCY"))
                AxPurchReqLines.Call("parmPurchQty", CDbl(dr("OQORDERED").ToString))

                ''----------------------
                'AxPurchReqLines.Call("parmPurchPrice", 10.1)
                'AxPurchReqLines.Call("parmLineAmount", 3020)
                ''----------------------

                '-test----------------------------------------------------------------
                dtSection = RequisitionDAL.getSectionIDValueBySection_SubSection(RequestHOBJ.rqCOMMENT, dr("MANITEMNO"))
                dtSubSection = RequisitionDAL.getSubSectionIDValue(dr("MANITEMNO"))

                If dtSubSection.Rows.Count > 0 Then
                    dtDimAttrValSetItem = RequisitionDAL.getDimAttrValSetItemAX(dtFactory.Rows(0)("VALUE"), dtSection.Rows(0)("VALUE"), dr("MANITEMNO"))

                    If dtDimAttrValSetItem.Rows.Count > 0 Then
                        strDefaultDimension = dtDimAttrValSetItem.Rows(0)("DIMENSIONATTRIBUTEVALUESET").ToString()
                    Else
                        strDefaultDimension = AX.CallStaticClassMethod("HOYA_DimensionAttributeValueSetItem", "NewDimensionAttributeValueSet", _
                                                                        dtFactory.Rows(0)("RECID"), _
                                                                        dtFactory.Rows(0)("VALUE"), _
                                                                        dtSection.Rows(0)("RECID"), _
                                                                        dtSection.Rows(0)("VALUE"), _
                                                                        dtSubSection.Rows(0)("RECID"), _
                                                                        dtSubSection.Rows(0)("VALUE")).ToString
                    End If
                Else
                    Throw New SystemException("Your section not match with AX Financial Dimension, please contact system admin.")
                End If

                InventDim = AX.CreateAxaptaRecord("INVENTDIM")
                InventDim.ExecuteStmt("SELECT * FROM %1 WHERE %1.InventSiteId=='" & RequestHOBJ.axHOYA_SITE & "'" & _
                                                         " && %1.InventLocationId=='" & RequestHOBJ.axHOYA_LOCATION & "'" & _
                                                         " && %1.ConfigId=='" & dr("Config") & "'" & _
                                                         " && %1.InventSizeId=='" & dr("Size") & "'" & _
                                                         " && %1.InventColorId=='" & dr("Color") & "'")
                If InventDim.Found Then
                    InventDimId = InventDim.get_Field("InventDimId").ToString 'dr("INVENTDIMID")
                Else
                    InventDim.set_Field("InventSiteId", RequestHOBJ.axHOYA_SITE)
                    InventDim.set_Field("InventLocationId", RequestHOBJ.axHOYA_LOCATION)
                    InventDim.set_Field("ConfigId", dr("Config"))
                    InventDim.set_Field("InventSizeId", dr("Size"))
                    InventDim.set_Field("InventColorId", dr("Color"))
                    InventDim = AX.CallStaticRecordMethod("InventDim", "create", InventDim)
                    InventDimId = InventDim.get_Field("InventDimId") 'dr("INVENTDIMID")
                End If
                '-----------------------------------------------------------------
                AxPurchReqLines.Call("parmDefaultDimension", strDefaultDimension) 'dr("MANITEMNO")
                AxPurchReqLines.Call("parmInventDimId", InventDimId) 'dr("INVENTDIMID")
                '-----------------------------------------------------------------
                AxPurchReqLines.Call("parmPurchLineCreated", "0")
                AxPurchReqLines.Call("parmIsPreEncumbranceRequired", "2")
                AxPurchReqLines.Call("parmIsPurchaseOrderGenerationManual", "1")
                AxPurchReqLines.Call("parmRequisitionStatus", "30")
                '-----------------------------------------------------------------
                If dr("EXPARRIVAL") <> "" AndAlso dr("EXPARRIVAL") <> "0" Then
                    AxPurchReqLines.Call("parmRequiredDate", dr("EXPARRIVAL").Split("/")(2) & dr("EXPARRIVAL").Split("/")(1) & dr("EXPARRIVAL").Split("/")(0))
                End If
                '-----------------------------------------------------------------
                AxPurchReqLines.Call("parmRequisitioner", dtWorker.Rows(0)("RecID"))
                '-----------------------------------------------------------------

                AxPurchReqLines.Call("parmBuyingLegalEntity", "5637144826") 'Field:RecID Table:COMPANYINFO DataArea:HOYA
                AxPurchReqLines.Call("Save")
            Next
            'PurchReqTable.Call("performBudgetCheck")
            'PurchReqTable.Call("recordBudgetFundReservation")

            'AX.TTSAbort()
            AX.TTSCommit()

            Found = CType(AX.CallStaticRecordMethod("PurchReqTable", "exist", strPurchReqId), Boolean)
            If Found Then
                'Dim RequisitionDAL As New RequisitionDAL(connDB)
                RequisitionDAL.BeginTrans()
                Try
                    Dim ExecuteReturn As Common.cDBSQL.ExecuteReturn = RequisitionDAL.UpdatePurchReqTable(RequestHOBJ)
                    If ExecuteReturn.strErrorMessage <> "" Then
                        Throw New SystemException(ExecuteReturn.strErrorMessage)
                    End If
                    Dim RequestLOBJ As New RequestLOBJ
                    For Each dr As DataRow In dt.Rows
                        RequestLOBJ.rqCOMMENT = dr("ITEMCOMMENT")
                        RequestLOBJ.rqLINENUM = dt.Rows.IndexOf(dr) + 1
                        RequisitionDAL.UpdatePurchReqLineByLineNum(RequestLOBJ, strHRecID)
                    Next
                    strMessage = strPurchReqId

                    RequisitionDAL.CommitTrans()
                Catch ex As Exception
                    strMessage = "Error : " & ex.Message
                    RequisitionDAL.RollBackTrans()
                End Try
            End If
            '------------------------------------------------------------
            'Dim AXTest As New Microsoft.Dynamics.BusinessConnectorNet
            'Dim aaa As Object = strPurchReqId
            'AX.CallStaticClassMethod("HOYA_PurchaseOnline_ReserveBudgetFunds", "ReserveBudget", aaa)

            '------------------------------------------------------------
            'Dim PurchReqWorkFlow As Microsoft.Dynamics.BusinessConnectorNet.AxaptaObject
            'PurchReqWorkFlow = AX.CreateAxaptaObject("PurchReqWorkFlow")
            'PurchReqWorkFlow.Call("parmPurchReqTable", PurchReqTable)
            'PurchReqWorkFlow.Call("parmSubmit", True)
            'PurchReqWorkFlow.Call("main")
            '------------------------------------------------------------

            AX.Logoff()
        Catch ex As Exception
            strMessage = "Error : " & ex.Message
            AX.TTSAbort()
            AX.Logoff()
        End Try
        Return strMessage
    End Function

    'Public Function UpdateRequisition(ByVal RequestHOBJ As RequestHOBJ, ByVal dt As DataTable, ByVal RQNNUMBER As String) As String
    '    'Header
    '    Dim RequisitionDAL As New RequisitionDAL(connDB)
    '    Try

    '        RequisitionDAL.BeginTrans()

    '        'Dim dtLastHSequence As DataTable = getLastSequence()
    '        'Dim NewSeq As Integer = CType(dtLastHSequence.Rows(0)("RQNHSEQ").ToString, Integer) + 1
    '        'Dim LastRQN As String = dtLastHSequence.Rows(0)("RQNNUMBER").ToString.Trim
    '        'Dim NewRQN As String = ""
    '        'Dim LineSeq As Integer = dtLastHSequence.Rows(0)("NEXTLSEQ")
    '        'LastRQN = LastRQN.Substring(LastRQN.Length - 7, 7)
    '        'Dim yy As String = LastRQN.Substring(LastRQN.Length - 7, 2)
    '        'Dim mm As String = LastRQN.Substring(LastRQN.Length - 5, 2)
    '        'Dim nn As String = LastRQN.Substring(LastRQN.Length - 3, 3)
    '        'If yy = Format(Now, "yy") AndAlso mm = Format(Now, "MM") Then
    '        '    NewRQN = "RQH" & Format(Now, "yyMM") & Format(CInt(nn) + 1, "000")
    '        'Else
    '        '    NewRQN = "RQH" & Format(Now, "yyMM001")
    '        'End If
    '        'RequestHOBJ.rqNHSEQ = RQNNUMBER
    '        RequestHOBJ.rqAUDTDATE = Format(Now, "yyyyMMdd")
    '        RequestHOBJ.rqAUDTTIME = "9" & Format(Now, "HHmmss")
    '        RequestHOBJ.RQAUDTUSER = RequestHOBJ.RQAUDTUSER
    '        '---RequestHOBJ.rqNEXTLSEQ = RequestHOBJ.rqNEXTLSEQ
    '        RequestHOBJ.rqDTCOMPLETE = 0
    '        '---RequestHOBJ.rqPOSTDATE = RequestHOBJ.rqPOSTDATE
    '        RequestHOBJ.rqNUMBER = RQNNUMBER
    '        Dim ExecuteReturn As Common.cDBSQL.ExecuteReturn
    '        Dim strError As String = ""
    '        ExecuteReturn = RequisitionDAL.UpdateRQH(RequestHOBJ)
    '        If ExecuteReturn.intRowEffected = -1 Then
    '            strError = ExecuteReturn.strErrorMessage & ControlChars.NewLine
    '        End If
    '        ExecuteReturn = RequisitionDAL.UpdateRQH2(RequestHOBJ)
    '        If ExecuteReturn.intRowEffected = -1 Then
    '            strError += ExecuteReturn.strErrorMessage & ControlChars.NewLine
    '        End If

    '        'detail
    '        Dim RequestLOBJ As New RequestLOBJ

    '        'Dim dtLastLSequence As DataTable = getLastLSequence()
    '        RequestLOBJ.rqNHSEQ = RequestHOBJ.rqNHSEQ
    '        For i As Integer = 0 To dt.Rows.Count - 1
    '            'RequestLOBJ.rqNHSEQ = NewSeq
    '            RequestLOBJ.rqNLREV = i + 1
    '            RequestLOBJ.RQAUDTDATE = RequestHOBJ.rqAUDTDATE
    '            RequestLOBJ.RQAUDTTIME = RequestHOBJ.rqAUDTTIME
    '            RequestLOBJ.RQAUDTUSER = RequestHOBJ.rqAUDTUSER
    '            RequestLOBJ.RQAUDTORG = RequestHOBJ.rqAUDTORG
    '            'RequestLOBJ.rqNLSEQ += 1
    '            'RequestLOBJ.rqLINEORDER = 0
    '            'RequestLOBJ.rqNCSEQ = RequestLOBJ.rqNLSEQ + 1
    '            'RequestLOBJ.rqOEONUMBER = ""
    '            'If dt.Rows(i)("VDCODE") = "" Then
    '            '    RequestLOBJ.rqVDEXISTS = 0
    '            'Else
    '            '    RequestLOBJ.rqVDEXISTS = 1
    '            'End If
    '            'RequestLOBJ.rqVDCODE = dt.Rows(i)("VDCODEL")
    '            'RequestLOBJ.rqVDNAME = dt.Rows(i)("VDNAMEL")

    '            'RequestLOBJ.rqINDBTABLE = 1
    '            'RequestLOBJ.rqCOMPLETION = 1
    '            'RequestLOBJ.rqITEMEXISTS = 1
    '            'RequestLOBJ.rqLOCATION = dt.Rows(i)("LOCATION")
    '            'RequestLOBJ.rqITEMNO = dt.Rows(i)("ITEMNO")
    '            If dt.Rows(i)("ITEMCOMMENT") <> "" Then
    '                RequestLOBJ.rqITEMDESC = String.Format("{0}({1})", dt.Rows(i)("ITEMDESC"), _
    '                Left(dt.Rows(i)("ITEMCOMMENT").ToString, 60 - (dt.Rows(i)("ITEMDESC").ToString.Length + 2)))
    '            Else
    '                RequestLOBJ.rqITEMDESC = dt.Rows(i)("ITEMDESC")
    '            End If
    '            'RequestLOBJ.rqVENDITEMNO = ""
    '            'RequestLOBJ.rqHASCOMMENT = 0
    '            If dt.Rows(i)("INSTRUCTCOMMENT") <> "" Then
    '                RequestLOBJ.rqHASCOMMENT = 1
    '                'RequestLOBJ.rqCOMMENT = dt.Rows(i)("INSTRUCTCOMMENT").ToString.Replace("ß", "")
    '                RequestLOBJ.rqCOMMENT = dt.Rows(i)("INSTRUCTCOMMENT").ToString
    '                If dt.Rows(i)("CURRENCY") <> "" Then
    '                    RequestLOBJ.rqCOMMENT = dt.Rows(i)("INSTRUCTCOMMENT").ToString.Replace("(" & dt.Rows(i)("CURRENCY") & ")", "")
    '                End If
    '                If IsNumeric(RequestLOBJ.rqCOMMENT) Then
    '                    RequestLOBJ.rqCOMMENT = CDbl(RequestLOBJ.rqCOMMENT)
    '                Else
    '                    RequestLOBJ.rqCOMMENT = 0
    '                End If
    '            End If
    '            'RequestLOBJ.rqORDERUNIT = dt.Rows(i)("ORDERUNIT")
    '            'RequestLOBJ.rqORDERCONV = 1
    '            'RequestLOBJ.rqORDERDECML = 4
    '            'RequestLOBJ.rqSTOCKDECML = 4
    '            'RequestLOBJ.rqOQORDERED = dt.Rows(i)("OQORDERED")
    '            'RequestLOBJ.rqHASDROPSHI = 0
    '            'RequestLOBJ.rqDROPTYPE = 2
    '            'RequestLOBJ.rqSTOCKITEM = 1
    '            RequestLOBJ.rqMANITEMNO = dt.Rows(i)("MANITEMNO")
    '            RequestLOBJ.rqEXPARRIVAL = 0
    '            If dt.Rows(i)("EXPARRIVAL").ToString <> "" Then
    '                RequestLOBJ.rqEXPARRIVAL = dt.Rows(i)("EXPARRIVAL").ToString
    '            End If

    '            ExecuteReturn = RequisitionDAL.UpdateRQL(RequestLOBJ)
    '            If ExecuteReturn.intRowEffected = -1 Then
    '                strError += ExecuteReturn.strErrorMessage & ControlChars.NewLine
    '            End If

    '            ExecuteReturn = RequisitionDAL.UpdateRQC(RequestLOBJ)
    '            If ExecuteReturn.intRowEffected = -1 Then
    '                strError += ExecuteReturn.strErrorMessage & ControlChars.NewLine
    '            End If

    '        Next
    '        If strError.Length > 0 Then
    '            RequisitionDAL.RollBackTrans()
    '            Return strError
    '        Else
    '            RequisitionDAL.CommitTrans()
    '        End If
    '        Return RQNNUMBER


    '    Catch ex As Exception
    '        RequisitionDAL.RollBackTrans()
    '        Return ex.Message
    '    End Try
    '    'Adapter.UpdateRQL(RequestLOBJ, Sequence)
    'End Function

    Public Function UpdateRequisitionAX(ByVal RequestHOBJ As RequestHOBJ, ByVal dt As DataTable, ByVal RQNNUMBER As String) As String
        Dim AX As New Microsoft.Dynamics.BusinessConnectorNet.Axapta
        Dim axRecord As Microsoft.Dynamics.BusinessConnectorNet.AxaptaRecord
        Dim axRecordforUpdate As Microsoft.Dynamics.BusinessConnectorNet.AxaptaRecord
        Dim strMessage As String = ""
        Try
            AX.Logon("", "", "", "")
            AX.TTSBegin()
            '== Head ======================================
            'axRecordforUpdate = AX.CreateAxaptaRecord("PurchReqTable")
            'axRecordforUpdate.ExecuteStmt("SELECT FORUPDATE * FROM %1 WHERE %1.PURCHREQID=='" & RQNNUMBER & "'")
            'If axRecordforUpdate.Found AndAlso RequestHOBJ.rqEXPARRIVAL <> New Date(1900, 1, 1) Then
            '    axRecordforUpdate.set_Field("parmRequiredDate", RequestHOBJ.rqEXPARRIVAL)
            '    axRecordforUpdate.Update()
            'End If
            Dim RequisitionDAL As New RequisitionDAL(connDB)
            RequestHOBJ.rqNUMBER = RQNNUMBER
            axRecordforUpdate = AX.CreateAxaptaRecord("PurchReqTable")
            axRecordforUpdate.ExecuteStmt("SELECT FORUPDATE * FROM %1 WHERE %1.PURCHREQID==" & RequestHOBJ.rqNUMBER)
            If axRecordforUpdate.Found Then
                axRecordforUpdate.set_Field("REQUIREDDATE", RequestHOBJ.rqEXPARRIVAL)
                axRecordforUpdate.Update()
            End If
            'RequisitionDAL.UpdatePurchReqTable_RequiredDate(RequestHOBJ)

            '== Line ======================================
            axRecord = AX.CreateAxaptaRecord("PurchReqLine")
            axRecord.ExecuteStmt("SELECT * FROM %1 WHERE %1.PURCHID=='" & RQNNUMBER & "'")

            Dim dtAX As New DataTable
            Dim drAX As DataRow

            Dim colLineNum As New DataColumn("LineNum", GetType(System.String))
            dtAX.Columns.Add(colLineNum)

            Dim colRecID As New DataColumn("RecID", GetType(System.String))
            dtAX.Columns.Add(colRecID)

            Dim colPurchPrice As New DataColumn("PurchPrice", GetType(System.String))
            dtAX.Columns.Add(colPurchPrice)

            While axRecord.Found
                drAX = dtAX.NewRow
                drAX("RecID") = axRecord.get_Field("RecID").ToString()
                drAX("LineNum") = axRecord.get_Field("LineNum").ToString()
                drAX("PurchPrice") = axRecord.get_Field("PurchPrice").ToString()
                dtAX.Rows.Add(drAX)
                axRecord.Next()
            End While

            Dim Found As Boolean = False
            axRecordforUpdate = AX.CreateAxaptaRecord("PurchReqLine")
            For Each drAX In dtAX.Rows
                Found = False
                For Each dr As DataRow In dt.Rows
                    If drAX("LineNum") = dr("LineNum") Then
                        Found = True
                        'axRecord.ExecuteStmt("SELECT FORUPDATE * FROM %1 WHERE %1.PURCHID=='" & RQNNUMBER & "' && %1.LINENUM==" & drAX("LineNum"))
                        axRecordforUpdate.ExecuteStmt("SELECT FORUPDATE * FROM %1 WHERE %1.RECID==" & drAX("RecID"))
                        If axRecordforUpdate.Found Then
                            axRecordforUpdate.set_Field("ECL_REMARK", dr("ITEMCOMMENT"))
                            axRecordforUpdate.set_Field("PURCHQTY", CDbl(dr("OQORDERED")))
                            axRecordforUpdate.set_Field("LineAmount", CDbl(dr("OQORDERED")) * drAX("PurchPrice"))
                            If dr("EXPARRIVAL") <> "" AndAlso dr("EXPARRIVAL") <> "0" Then
                                axRecordforUpdate.set_Field("REQUIREDDATE", New Date(dr("EXPARRIVAL").Split("/")(2), dr("EXPARRIVAL").Split("/")(1), dr("EXPARRIVAL").Split("/")(0)))
                            End If
                            axRecordforUpdate.Update()
                        End If
                        Exit For
                    End If
                Next
                If Found = False Then
                    axRecordforUpdate.ExecuteStmt("SELECT FORUPDATE * FROM %1 WHERE %1.RECID==" & drAX("RecID"))
                    axRecordforUpdate.Delete()
                End If
            Next
            AX.TTSCommit()
            AX.Logoff()
        Catch ex As Exception
            strMessage = "Error : " & ex.Message
            AX.TTSAbort()
        End Try
        Return strMessage
    End Function

    Public Function getExcel(ByVal strFullPath As String) As DataTable
        Return Adapter.getExcel(strFullPath)
    End Function


    'Public Function ImportItems(ByVal strFile As String) As String
    '    Try
    '        Dim strSystemPath As String = HttpContext.Current.Request.PhysicalApplicationPath
    '        Dim FullPath As String = strSystemPath & "TempInput\" & strFile
    '        Dim xlsApp As New Excel.Application
    '        Dim xlsBook As Excel.Workbook = xlsApp.Workbooks.Open(FullPath, , True)
    '        Dim xlsSheet As Excel.Worksheet
    '        xlsApp.Visible = False
    '        'Dim EmployeeDAL As New EmployeeDAL
    '        Dim strOutPut As String = ""
    '        Adapter.DeleteResignedEmployee()
    '        For Each xlsSheet In xlsBook.Worksheets 'loop get excel sheet
    '            'EmployeeDAL.DeleteEmployeeBySheetName(IIf(xlsSheet.Name = "R&D-MM", "R&D/MM", xlsSheet.Name))
    '            Adapter.DeleteEmployeeBySheetName(Adapter.GetConvertExcelSheet(xlsSheet.Name))
    '            strOutPut += Adapter.UpdateEmployee(strFile, FullPath, xlsSheet.Name) & "<br>"
    '        Next
    '        xlsBook.Close(False)
    '        xlsApp.DisplayAlerts = True
    '        xlsApp.Workbooks.Close()
    '        xlsApp.Quit()
    '        xlsSheet = Nothing
    '        xlsBook = Nothing
    '        xlsApp = Nothing
    '        Return strOutPut

    '    Catch ex As Exception
    '        Return ex.Message
    '    End Try
    'End Function


End Class
