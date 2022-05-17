Imports System
Imports System.Diagnostics
Imports System.IO

Imports Devart.Data.Oracle
Imports System.Data
Imports System.Configuration

Module Module1


    Public InsertInto As String = "insert into refnotifyprofile 
(LASTUPDATEUSERNAME,LASTUPDATEDATE,PLANTCODE,APPLICATION,USERNAME,ROLESEQID,EMAILTYPE,PROFILETYPESEQID,PROFILETYPEVALUE)
VALUES ('JAMES.BUTLER','16-MAY-2022','ALL','MTT',"

    Public USERNAME As String = "CARLA.JAMISON"


    Dim NOTIFICATIONS1 As String = "1,'FUTURE',1,'WEEKLY');"
    Dim NOTIFICATIONS2 As String = "1,'FUTURE',7,'2');"
    Dim NOTIFICATIONS3 As String = "1,'FUTURE',10,'NEXT 14 DAYS');"
    Dim NOTIFICATIONS4 As String = "4,'ENTERED',1,'IMMEDIATE');"
    Dim NOTIFICATIONS5 As String = "4,'FUTURE',1,'WEEKLY');"
    Dim NOTIFICATIONS6 As String = "4,'FUTURE',7,'2');"
    Dim NOTIFICATIONS7 As String = "4,'FUTURE',10,'NEXT 14 DAYS');"
    Dim NOTIFICATIONS8 As String = "5,'FUTURE',1,'WEEKLY');"
    Dim NOTIFICATIONS9 As String = "5,'FUTURE',7,'2');"
    Dim NOTIFICATIONS10 As String = "5,'FUTURE',10,'NEXT 14 DAYS');"


    Dim FilePath As String = ""


    Public Property strPath As String

    Public Property ConnectionString As String

    Public Property Msomething As String


    Sub Main()


        strPath = Directory.GetCurrentDirectory & "\insertNotifications.txt"
        Dim _notification As String = ""

        'AddToTraceLog(strPath, InsertInto & "'" & USERNAME & "'," & NOTIFICATIONS1)

        'AddToTraceLog(strPath, InsertInto & "'" & USERNAME & "'," & NOTIFICATIONS2)

        'AddToTraceLog(strPath, InsertInto & "'" & USERNAME & "'," & NOTIFICATIONS3)

        'AddToTraceLog(strPath, InsertInto & "'" & USERNAME & "'," & NOTIFICATIONS4)

        'AddToTraceLog(strPath, InsertInto & "'" & USERNAME & "'," & NOTIFICATIONS5)

        'AddToTraceLog(strPath, InsertInto & "'" & USERNAME & "'," & NOTIFICATIONS6)

        'AddToTraceLog(strPath, InsertInto & "'" & USERNAME & "'," & NOTIFICATIONS7)

        'AddToTraceLog(strPath, InsertInto & "'" & USERNAME & "'," & NOTIFICATIONS8)

        'AddToTraceLog(strPath, InsertInto & "'" & USERNAME & "'," & NOTIFICATIONS9)

        'AddToTraceLog(strPath, InsertInto & "'" & USERNAME & "'," & NOTIFICATIONS10)


        'Console.WriteLine("Hello World!")
        'Console.ReadLine()


        'Console.WriteLine(InsertInto & "'" & USERNAME & "'," & NOTIFICATIONS1)
        'Console.WriteLine(InsertInto & "'" & USERNAME & "'," & NOTIFICATIONS2)
        'Console.WriteLine(InsertInto & "'" & USERNAME & "'," & NOTIFICATIONS3)
        'Console.WriteLine(InsertInto & "'" & USERNAME & "'," & NOTIFICATIONS4)
        'Console.WriteLine(InsertInto & "'" & USERNAME & "'," & NOTIFICATIONS5)
        'Console.WriteLine(InsertInto & "'" & USERNAME & "'," & NOTIFICATIONS6)
        'Console.WriteLine(InsertInto & "'" & USERNAME & "'," & NOTIFICATIONS7)
        'Console.WriteLine(InsertInto & "'" & USERNAME & "'," & NOTIFICATIONS8)
        'Console.WriteLine(InsertInto & "'" & USERNAME & "'," & NOTIFICATIONS9)
        'Console.WriteLine(InsertInto & "'" & USERNAME & "'," & NOTIFICATIONS10)

        'Console.WriteLine(InsertInto)
        'Console.ReadLine()


        ReadData()

        Console.WriteLine("Finished")
        Console.ReadLine()
    End Sub


    Public Function ReadData()

        Dim ds As DataSet
        Dim username As String = ""
        ds = GetOracleDataSet("select upper(username) from refemployee where 1=1 and siteid = '31' and inactive_flag = 'N' order by username")

        If ds IsNot Nothing Then
            If ds.Tables.Count = 1 Then
                Dim dr As Data.DataTableReader = ds.Tables(0).CreateDataReader
                If dr IsNot Nothing Then
                    While dr.Read


                        'dr.Read()

                        'Person Name
                        username = Trim(dr.Item(0))

                        If username.Length > 0 Then

                            AddToTraceLog(strPath, InsertInto & "'" & username & "'," & NOTIFICATIONS1)

                            AddToTraceLog(strPath, InsertInto & "'" & username & "'," & NOTIFICATIONS2)

                            AddToTraceLog(strPath, InsertInto & "'" & username & "'," & NOTIFICATIONS3)

                            AddToTraceLog(strPath, InsertInto & "'" & username & "'," & NOTIFICATIONS4)

                            AddToTraceLog(strPath, InsertInto & "'" & username & "'," & NOTIFICATIONS5)

                            AddToTraceLog(strPath, InsertInto & "'" & username & "'," & NOTIFICATIONS6)

                            AddToTraceLog(strPath, InsertInto & "'" & username & "'," & NOTIFICATIONS7)

                            AddToTraceLog(strPath, InsertInto & "'" & username & "'," & NOTIFICATIONS8)

                            AddToTraceLog(strPath, InsertInto & "'" & username & "'," & NOTIFICATIONS9)

                            AddToTraceLog(strPath, InsertInto & "'" & username & "'," & NOTIFICATIONS10)


                        End If

                    End While
                End If
            End If
        End If

        Return True
    End Function


    Public Sub AddToTraceLog(ByVal Filename As String, ByVal Entry As String)


        Dim s As String = ""
        s += Entry + vbCrLf


        Dim Retry As Long
        Retry = 10
        Do While Retry > 0
            Try
                Dim Stream As New IO.FileStream(Filename, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Stream.Seek(0, IO.SeekOrigin.End)
                Stream.Write(System.Text.Encoding.Default.GetBytes(s.ToCharArray), 0, System.Text.ASCIIEncoding.ASCII.GetByteCount(s.ToCharArray))
                Stream.Close()
                Retry = 0
            Catch ex As Exception
                Throw
            End Try
        Loop
    End Sub



    Function GetOracleDataSet(ByVal sql As String, Optional ByVal connection As String = "", Optional ByVal provider As String = "") As DataSet
        Dim conCust As OracleConnection = Nothing
        Dim cmdSql As OracleCommand = Nothing

        Dim ds As New DataSet
        Dim myDataAdapter As New OracleDataAdapter()


        Try
            If connection.Length = 0 Then
                connection = ConfigurationManager.ConnectionStrings.Item("connectionRCFAPRD").ConnectionString
            End If

            conCust = New OracleConnection(connection)
            conCust.Open()
            ds.EnforceConstraints = False
            cmdSql = New OracleCommand(sql, conCust)

            myDataAdapter = New OracleDataAdapter(cmdSql)
            ds.Tables.Add("ResultTable")
            ds.Tables("ResultTable").BeginLoadData()
            myDataAdapter.Fill(ds.Tables("ResultTable"))
            ds.Tables("ResultTable").EndLoadData()

        Catch ex As Exception
            ds = Nothing
            'Return Nothing
            Throw 'ApplicationException("GetOracleDataSet - " & sql, ex)
        Finally
            GetOracleDataSet = ds
            conCust.Close()
            If Not conCust Is Nothing Then conCust = Nothing
            If Not cmdSql Is Nothing Then cmdSql = Nothing

            If Not myDataAdapter Is Nothing Then myDataAdapter = Nothing
            If Not ds Is Nothing Then ds = Nothing
        End Try
    End Function



End Module
