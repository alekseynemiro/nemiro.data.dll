Imports Nemiro.Data
Imports Nemiro.Data.Sql

Module Module1

  Sub Main()
    UnitTestHelper.Main.WriteLine("..SqlClient config testing (VB):")
    Using client As New SqlClient()
      UnitTestHelper.Main.WriteLine("....ConnectionMode: {0}", client.ConnectionMode)
      If client.ConnectionMode = CType([Enum].Parse(GetType(ConnectionMode), My.Settings.NeDataSqlConnectionMode), ConnectionMode) Then
        UnitTestHelper.Main.WriteLine("....Successfully!")
      Else
        UnitTestHelper.Main.WriteLine("....Fail...")
      End If
      UnitTestHelper.Main.WriteLine("....CommandType: {0}", client.CommandType)
      If client.CommandType = CType([Enum].Parse(GetType(TypeCommand), My.Settings.NeDataSqlCommandType), TypeCommand) Then
        UnitTestHelper.Main.WriteLine("....Successfully!")
      Else
        UnitTestHelper.Main.WriteLine("....Fail...")
      End If
      UnitTestHelper.Main.WriteLine("....CacheType: {0}", client.CacheType)
      If client.CacheType = CType([Enum].Parse(GetType(CachingType), My.Settings.NeDataSqlCacheType), CachingType) Then
        UnitTestHelper.Main.WriteLine("....Successfully!")
      Else
        UnitTestHelper.Main.WriteLine("....Fail...")
      End If
      UnitTestHelper.Main.WriteLine("....CacheDuration: {0}", client.CacheDuration)
      If client.CacheDuration = My.Settings.NeDataSqlCacheDuration Then
        UnitTestHelper.Main.WriteLine("....Successfully!")
      Else
        UnitTestHelper.Main.WriteLine("....Fail...")
      End If
      UnitTestHelper.Main.WriteLine("....CacheBufferSize: {0}", client.CacheBufferSize)
      If client.CacheBufferSize = My.Settings.NeDataSqlCacheBufferSize Then
        UnitTestHelper.Main.WriteLine("....Successfully!")
      Else
        UnitTestHelper.Main.WriteLine("....Fail...")
      End If
      UnitTestHelper.Main.WriteLine("....CacheCustom: {0}", client.CacheCustom)
      If client.CacheCustom = Type.GetType(My.Settings.NeDataSqlCacheCustom) Then
        UnitTestHelper.Main.WriteLine("....Successfully!")
      Else
        UnitTestHelper.Main.WriteLine("....Fail...")
      End If
      UnitTestHelper.Main.WriteLine("....CacheCustomArgs: {0}", client.CacheCustomArgs)
      If client.CacheCustomArgs.Length = My.Settings.NeDataSqlCacheCustomArgs.Split(","c).Length Then
        UnitTestHelper.Main.WriteLine("....Successfully!")
      Else
        UnitTestHelper.Main.WriteLine("....Fail...")
      End If
      UnitTestHelper.Main.WriteLine("....CacheBufferAccessTimeout: {0}", client.CacheBufferAccessTimeout)
      If client.CacheBufferAccessTimeout = TimeSpan.Parse(My.Settings.NeDataSqlCacheBufferAccessTimeout) Then
        UnitTestHelper.Main.WriteLine("....Successfully!")
      Else
        UnitTestHelper.Main.WriteLine("....Fail...")
      End If
      UnitTestHelper.Main.WriteLine("....CacheAccessTimeout: {0}", client.CacheAccessTimeout)
      If client.CacheAccessTimeout = TimeSpan.Parse(My.Settings.NeDataSqlCacheAccessTimeout) Then
        UnitTestHelper.Main.WriteLine("....Successfully!")
      Else
        UnitTestHelper.Main.WriteLine("....Fail...")
      End If
      UnitTestHelper.Main.WriteLine("....CacheBinder: {0}", client.CacheBinder)
      If client.CacheBinder = Type.GetType(My.Settings.NeDataSqlCacheBinder) Then
        UnitTestHelper.Main.WriteLine("....Successfully!")
      Else
        UnitTestHelper.Main.WriteLine("....Fail...")
      End If
    End Using
  End Sub

End Module
