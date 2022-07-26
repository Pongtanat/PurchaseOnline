Imports Microsoft.VisualBasic
Imports System.DirectoryServices
Imports System.Data

Public Class LoginBLL
    Private _applicantAdapter As LoginDAL = Nothing

    Protected ReadOnly Property Adapter() As LoginDAL
        Get
            If _applicantAdapter Is Nothing Then
                _applicantAdapter = New LoginDAL
            End If

            Return _applicantAdapter
        End Get
    End Property

    Public Function Login(ByVal DomainName As String, _
                               ByVal UserName As String, _
                               ByVal Password As String, _
                               ByVal strSite As String) As LoginOBJ
        Dim LoginOBJ As New LoginOBJ
        LoginOBJ = Login_DirectoryServices(DomainName, UserName, Password)
        LoginOBJ = Login_Database(LoginOBJ, strSite)
        Select Case strSite
            'Case "HOPTHO" : LoginOBJ.axHOYA_Site = "HO" : LoginOBJ.axHOYA_FactoryDIMENSIONFINANCIALTAG = "HO_ A816"
            'Case "HPTPOL", "HOPT", "HPTMAT" : LoginOBJ.axHOYA_Site = "PO" : LoginOBJ.axHOYA_FactoryDIMENSIONFINANCIALTAG = "PO_ A811"
            'Case "HPTNPL", "HOPTNP", "HMATNP" : LoginOBJ.axHOYA_Site = "PO3" : LoginOBJ.axHOYA_FactoryDIMENSIONFINANCIALTAG = "P3_ A815"
            'Case "HPTRPL", "HOPTRP" : LoginOBJ.axHOYA_Site = "RP" : LoginOBJ.axHOYA_FactoryDIMENSIONFINANCIALTAG = "RP_ A813"
            'Case "HPTMOL", "HOPTMO" : LoginOBJ.axHOYA_Site = "GMO" : LoginOBJ.axHOYA_FactoryDIMENSIONFINANCIALTAG = "MO_ A812"
            Case "HOPTHO" : LoginOBJ.axHOYA_Site = "HO" : LoginOBJ.axHOYA_FactoryDIMENSIONFINANCIALTAG = "HO" : LoginOBJ.CategorySearchText = "HO"
            Case "HPTPOL", "HOPT", "HPTMAT" : LoginOBJ.axHOYA_Site = "PO" : LoginOBJ.axHOYA_FactoryDIMENSIONFINANCIALTAG = "PO" : LoginOBJ.CategorySearchText = "PO"
            Case "HPTNPL", "HOPTNP", "HMATNP" : LoginOBJ.axHOYA_Site = "PO3" : LoginOBJ.axHOYA_FactoryDIMENSIONFINANCIALTAG = "P3" : LoginOBJ.CategorySearchText = "PO3"
            Case "HPTRPL", "HOPTRP" : LoginOBJ.axHOYA_Site = "RP" : LoginOBJ.axHOYA_FactoryDIMENSIONFINANCIALTAG = "RP" : LoginOBJ.CategorySearchText = "RP"
                'Case "HPTMOL", "HOPTMO" : LoginOBJ.axHOYA_Site = "MO" : LoginOBJ.axHOYA_FactoryDIMENSIONFINANCIALTAG = "MO"
            Case "HPTMOL", "HOPTMO" : LoginOBJ.axHOYA_Site = "GMO" : LoginOBJ.axHOYA_FactoryDIMENSIONFINANCIALTAG = "GMO" : LoginOBJ.CategorySearchText = "MO"
        End Select
        Return LoginOBJ
    End Function

    Protected Function Login_DirectoryServices(ByVal DomainName As String, _
                                                ByVal UserName As String, _
                                                ByVal Password As String) As LoginOBJ
        Dim LoginOBJ As New LoginOBJ
        Try
            'Lightweight Directory Access Protocol
            Dim entry As New DirectoryEntry()
            'entry.Path = String.Format("LDAP://{0}.HOPT.COM", DomainName)
            entry.Path = String.Format("LDAP://{0}", DomainName)
            ' "LDAP://" & ddlDomain.SelectedValue & ".HOPT.COM"
            entry.Username = String.Format("{0}\{1}", DomainName, UserName)
            entry.Password = Password

            'Dim nativeObject As Object = entry.NativeObject

            Dim search As New DirectorySearcher(entry)
            Dim result As SearchResult
            'search.Filter = "(SAMAccountName=" + txbName.Text + ")"
            'search.Filter = String.Format("(SAMAccountName={0})", UserName)
            If DomainName = "hoya" Then
                search.Filter = String.Format("(&(objectCategory=Person)(objectClass=user)(company=HOPT)(SAMAccountName={0}))", UserName)
            Else
                search.Filter = String.Format("(SAMAccountName={0})", UserName)
            End If
            'search.PropertiesToLoad.Add("cn")
            With search
                .PropertiesToLoad.Add("description")
                .PropertiesToLoad.Add("displayname")
                .PropertiesToLoad.Add("samaccountname")
                .PropertiesToLoad.Add("mail")
                .PropertiesToLoad.Add("firstname")
                .PropertiesToLoad.Add("lastname")
                .PropertiesToLoad.Add("title")
                .PropertiesToLoad.Add("department")
                .PropertiesToLoad.Add("company")
                .PropertiesToLoad.Add("memberOf")
                .PropertiesToLoad.Add("physicalDeliveryOfficeName")
                .Sort.Direction = SortDirection.Ascending
            End With

            entry.Close()

            Dim strb As New StringBuilder

            result = search.FindOne()
            For Each str As String In result.Properties.PropertyNames
                strb.AppendLine(String.Format("{0} : {1}<br>", str, result.Properties(str)(0).ToString))
            Next


            If result Is Nothing Then
                LoginOBJ.AUTHENTICATED = False
                LoginOBJ.Message = "Please check user/password"
            Else
                LoginOBJ.AUTHENTICATED = True
                LoginOBJ.Message = strb.ToString '"logon completed"
                LoginOBJ.UserName = result.Properties("samaccountname")(0).ToString
                LoginOBJ.DomainName = DomainName
                'LoginOBJ.Section = result.Properties("memberOf")(0).ToString
                'If result.Properties("description").Count > 0 Then
                '    LoginOBJ.Section = result.Properties("description")(0).ToString
                'End If
                If result.Properties("memberOf").Count > 0 Then
                    '======================================================================
                    'Only for HO
                    '----------------------------------------------------------------------
                    'Dim OUGroup As String = ""
                    'For i As Integer = 0 To result.Properties("memberOf").Count - 1
                    '    OUGroup = result.Properties("memberOf")(i).ToString.ToUpper
                    '    If OUGroup.Split(",")(0).Split("=")(0) = "CN" Then
                    '        If OUGroup.Split(",")(0).Split("=")(1).IndexOf("PR") >= 0 Then
                    '            LoginOBJ.PRGroup = True
                    '        End If
                    '    End If
                    'Next
                    '----------------------------------------------------------------------
                    'Apply for all factory
                    '----------------------------------------------------------------------
                    Dim OUGroup As String = ""
                    Dim arrOUGroup() As String
                    For i As Integer = 0 To result.Properties("memberOf").Count - 1
                        OUGroup = result.Properties("memberOf")(i).ToString.ToUpper
                        arrOUGroup = OUGroup.Split(",")(0).Split("=")
                        If arrOUGroup(0) = "CN" Then
                            LoginOBJ.OUGroup += "," & arrOUGroup(1)
                            'If arrOUGroup(1).IndexOf("HO") >= 0 Then
                            '    LoginOBJ.Factory = "HO"
                            'ElseIf arrOUGroup(1).IndexOf("SLR") >= 0 Then
                            '    LoginOBJ.Factory = "SLR"
                            'ElseIf arrOUGroup(1).IndexOf("PO") >= 0 Then
                            '    LoginOBJ.Factory = "PO"
                            'ElseIf arrOUGroup(1).IndexOf("RP") >= 0 Then
                            '    LoginOBJ.Factory = "RP"
                            'ElseIf arrOUGroup(1).IndexOf("MO") >= 0 Then
                            '    LoginOBJ.Factory = "GMO"
                            'End If
                            'If arrOUGroup(1).IndexOf("PR") >= 0 Then
                            '    Select Case arrOUGroup(1)
                            '        Case "HOPR" : LoginOBJ.Factory = "HO" : LoginOBJ.PRGroup = True
                            '        Case "POPR" : LoginOBJ.Factory = "PO" : LoginOBJ.PRGroup = True
                            '        Case "RPPR" : LoginOBJ.Factory = "RP" : LoginOBJ.PRGroup = True
                            '        Case "SLRPR" : LoginOBJ.Factory = "SLR" : LoginOBJ.PRGroup = True
                            '        Case "PR MO" : LoginOBJ.Factory = "GMO" : LoginOBJ.PRGroup = True
                            '    End Select
                            'End If
                        End If
                    Next
                    'LoginOBJ.OUGroup = LoginOBJ.OUGroup.Substring(0, LoginOBJ.OUGroup.Length - 1)

                    '======================================================================
                    'Else
                    '    LoginOBJ.Section = result.Properties("description")(0).ToString
                End If
                If result.Properties("displayname").Count > 0 Then
                    LoginOBJ.FullName = result.Properties("displayname")(0).ToString
                Else
                    LoginOBJ.FullName = LoginOBJ.DomainName & "\" & LoginOBJ.UserName
                End If
                If result.Properties("firstname").Count > 0 Then
                    LoginOBJ.FirstName = result.Properties("firstname")(0).ToString
                End If
                If result.Properties("lastname").Count > 0 Then
                    LoginOBJ.LastName = result.Properties("lastname")(0).ToString
                End If
                If result.Properties("description").Count > 0 Then
                    LoginOBJ.Section = result.Properties("physicalDeliveryOfficeName")(0).ToString
                End If

            End If


        Catch dex As DirectoryServicesCOMException
            LoginOBJ.Message = dex.Message
        Catch ex As Exception
            LoginOBJ.Message = ex.Message
        End Try
        Return LoginOBJ
    End Function

    Protected Function Login_Database(ByVal LoginOBJ As LoginOBJ, ByVal strSite As String) As LoginOBJ
        If LoginOBJ.AUTHENTICATED = True Then
            LoginOBJ.ACCPAC = strSite
            Dim dtLogin As DataTable = Adapter.Login(LoginOBJ)
            If dtLogin.Rows.Count > 0 Then
                Adapter.AccessLog(LoginOBJ)
                LoginOBJ.AllowSelectVendor = dtLogin.Rows(0)("AllowSelectVendor")
                LoginOBJ.AllowSubmitPRApprove = dtLogin.Rows(0)("AllowSubmitPRApprove")
                LoginOBJ.AllowSubmitPRReceive = dtLogin.Rows(0)("AllowSubmitPRReceive")
                LoginOBJ.AllowChangeRequisition = dtLogin.Rows(0)("AllowChangeRequisition")
                LoginOBJ.AllowEditPermission = dtLogin.Rows(0)("AllowEditPermission")
                LoginOBJ.AllowAnotherSection = dtLogin.Rows(0)("AllowAnotherSection")

                LoginOBJ.Sections = dtLogin.Rows(0)("Section") 'IT,CONT

                If dtLogin.Rows(0)("Section").ToString.IndexOf(",") > 0 Then
                    LoginOBJ.Section = dtLogin.Rows(0)("Section").ToString.Split(",")(0).Trim
                Else
                    LoginOBJ.Section = dtLogin.Rows(0)("Section") 'IT
                End If

                LoginOBJ.FilterItemCode = dtLogin.Rows(0)("FilterItemCode")

                LoginOBJ.Message = ""

                If dtLogin.Rows(0)("PreparerEmpCode") <> "" Then
                    LoginOBJ.PRPreparerEmpCode = dtLogin.Rows(0)("PreparerEmpCode")
                Else
                    LoginOBJ.AUTHENTICATED = False
                    LoginOBJ.Message = String.Format("PR Preparer employee code for {0} does not set.", strSite)
                    If dtLogin.Rows(0)("PreparerName") = "" Then
                        LoginOBJ.Message = String.Format("PR Preparer for {0} does not set.", strSite)
                    End If
                End If


                'Else
                '    LoginOBJ.AUTHENTICATED = False
                '    LoginOBJ.Message = "Error!<br>User not found. please contact IT HO"
            Else
                LoginOBJ.AUTHENTICATED = False
                LoginOBJ.Message = "This user not authorized to access database."
            End If
            Dim OUGroup As String = LoginOBJ.OUGroup
            If OUGroup <> "" Then
                OUGroup = OUGroup.Substring(1, OUGroup.Length - 1).Replace(",", "','") ',HOSMC,HOSF -> 'HOSMC','HOSF'
            End If
            'Dim dtConnection As DataTable = Adapter.CheckRules(LoginOBJ, OUGroup)
            'If dtConnection.Rows.Count > 0 Then

            'Else
            '    LoginOBJ.AUTHENTICATED = False
            '    LoginOBJ.Message = "This user not authorized to access database."
            '    If LoginOBJ.OUGroup <> "" Then
            '        LoginOBJ.Message += "<br><u>Member of group</u>" & LoginOBJ.OUGroup.Replace(",", "<br>")
            '    End If
            'End If
        End If
        Return LoginOBJ
    End Function

    'Public Function DomainList() As DataTable
    '    Return Adapter.DomainList()
    'End Function

    'Public Function CheckRules(ByVal Group As String) As RulesOBJ
    '    Dim dt As DataTable = Adapter.CheckRules(Group)
    '    Dim RulesOBJ As New RulesOBJ
    '    If dt.Rows.Count > 0 Then
    '        RulesOBJ.RulesOBJ(dt.Rows(0))
    '    End If
    '    Return RulesOBJ
    'End Function

    Public Function GetConnection(ByVal LoginOBJ As LoginOBJ) As DataTable
        Return Adapter.GetConnection(LoginOBJ)
    End Function
End Class
