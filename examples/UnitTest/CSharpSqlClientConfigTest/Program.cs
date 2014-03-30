using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nemiro.Data;
using Nemiro.Data.Sql;

namespace CSharpSqlClientConfigTest
{
  class Program
  {
    static void Main(string[] args)
    {
      UnitTestHelper.Main.WriteLine("..SqlClient config testing (C#):");
      using (SqlClient client = new SqlClient())
      {
        /*UnitTestHelper.Main.WriteLine("....ConnectionString: {0}", client.ConnectionString);
        if (client.ConnectionString == Properties.Settings.Default.LocalSqlServer)
        {
          UnitTestHelper.Main.WriteLine("....Successfully!");
        }
        else
        {
          UnitTestHelper.Main.WriteLine("....Fail...");
        }*/

        UnitTestHelper.Main.WriteLine("....ConnectionMode: {0}", client.ConnectionMode);
        if (client.ConnectionMode == (ConnectionMode)Enum.Parse(typeof(ConnectionMode), Properties.Settings.Default.NeDataSqlConnectionMode))
        {
          UnitTestHelper.Main.WriteLine("....Successfully!");
        }
        else
        {
          UnitTestHelper.Main.WriteLine("....Fail...");
        }

        UnitTestHelper.Main.WriteLine("....CommandType: {0}", client.CommandType);
        if (client.CommandType == (TypeCommand)Enum.Parse(typeof(TypeCommand), Properties.Settings.Default.NeDataSqlCommandType))
        {
          UnitTestHelper.Main.WriteLine("....Successfully!");
        }
        else
        {
          UnitTestHelper.Main.WriteLine("....Fail...");
        }

        UnitTestHelper.Main.WriteLine("....CacheType: {0}", client.CacheType);
        if (client.CacheType == (CachingType)Enum.Parse(typeof(CachingType), Properties.Settings.Default.NeDataSqlCacheType))
        {
          UnitTestHelper.Main.WriteLine("....Successfully!");
        }
        else
        {
          UnitTestHelper.Main.WriteLine("....Fail...");
        }

        UnitTestHelper.Main.WriteLine("....CacheDuration: {0}", client.CacheDuration);
        if (client.CacheDuration == Properties.Settings.Default.NeDataSqlCacheDuration)
        {
          UnitTestHelper.Main.WriteLine("....Successfully!");
        }
        else
        {
          UnitTestHelper.Main.WriteLine("....Fail...");
        }

        UnitTestHelper.Main.WriteLine("....CacheBufferSize: {0}", client.CacheBufferSize);
        if (client.CacheBufferSize == Properties.Settings.Default.NeDataSqlCacheBufferSize)
        {
          UnitTestHelper.Main.WriteLine("....Successfully!");
        }
        else
        {
          UnitTestHelper.Main.WriteLine("....Fail...");
        }

        UnitTestHelper.Main.WriteLine("....CacheCustom: {0}", client.CacheCustom);
        if (client.CacheCustom == Type.GetType(Properties.Settings.Default.NeDataSqlCacheCustom))
        {
          UnitTestHelper.Main.WriteLine("....Successfully!");
        }
        else
        {
          UnitTestHelper.Main.WriteLine("....Fail...");
        }

        UnitTestHelper.Main.WriteLine("....CacheCustomArgs: {0}", client.CacheCustomArgs);
        if (client.CacheCustomArgs.Length == Properties.Settings.Default.NeDataSqlCacheCustomArgs.Split(',').Length)
        {
          UnitTestHelper.Main.WriteLine("....Successfully!");
        }
        else
        {
          UnitTestHelper.Main.WriteLine("....Fail...");
        }

        UnitTestHelper.Main.WriteLine("....CacheBufferAccessTimeout: {0}", client.CacheBufferAccessTimeout);
        if (client.CacheBufferAccessTimeout == TimeSpan.Parse(Properties.Settings.Default.NeDataSqlCacheBufferAccessTimeout))
        {
          UnitTestHelper.Main.WriteLine("....Successfully!");
        }
        else
        {
          UnitTestHelper.Main.WriteLine("....Fail...");
        }

        UnitTestHelper.Main.WriteLine("....CacheAccessTimeout: {0}", client.CacheAccessTimeout);
        if (client.CacheAccessTimeout == TimeSpan.Parse(Properties.Settings.Default.NeDataSqlCacheAccessTimeout))
        {
          UnitTestHelper.Main.WriteLine("....Successfully!");
        }
        else
        {
          UnitTestHelper.Main.WriteLine("....Fail...");
        }

        UnitTestHelper.Main.WriteLine("....CacheBinder: {0}", client.CacheBinder);
        if (client.CacheBinder == Type.GetType(Properties.Settings.Default.NeDataSqlCacheBinder))
        {
          UnitTestHelper.Main.WriteLine("....Successfully!");
        }
        else
        {
          UnitTestHelper.Main.WriteLine("....Fail...");
        }

      }
    }
  }
}
