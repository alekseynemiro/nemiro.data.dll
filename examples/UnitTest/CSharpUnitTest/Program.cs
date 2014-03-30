using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using Nemiro.Data;
using Nemiro.Data.Sql;
using System.Data;
using System.Threading;
using System.Diagnostics;

namespace UnitTest
{
  class Program
  {

    // explicit connection string
    // явная строка соединения с базой данных
    private static string _ConnectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\example.mdf;Integrated Security=True;User Instance=True";

    static void Main(string[] args)
    {
      if (File.Exists("report.log"))
      {
        File.Delete("report.log");
      }

      UnitTestHelper.Main.WriteLine("Unit Test Nemiro.Data v{0}", Assembly.GetAssembly(typeof(Nemiro.Data.Sql.SqlClient)).GetName().Version);

      #region ..SqlClient..

      Console.ForegroundColor = ConsoleColor.Yellow;
      UnitTestHelper.Main.WriteLine("SqlClient test...");
      Console.ForegroundColor = ConsoleColor.Gray;

      UnitTestHelper.Main.WriteLine("..Default connection string test:");
      using (SqlClient client = new SqlClient())
      {
        client.ExecuteNonQuery("SELECT TOP 1 id FROM users");
      }
      UnitTestHelper.Main.WriteLine("..Successfully!");
      UnitTestHelper.Main.Pause("..");

      UnitTestHelper.Main.WriteLine("..Custom name connection string test:");
      using (SqlClient client = new SqlClient("CustomConnectionString"))
      {
        client.ExecuteNonQuery("SELECT TOP 1 id FROM users");
      }
      UnitTestHelper.Main.WriteLine("..Successfully!");
      UnitTestHelper.Main.Pause("..");

      UnitTestHelper.Main.WriteLine("..Explicit connection string test:");
      using (SqlClient client = new SqlClient(_ConnectionString))
      {
        client.ExecuteNonQuery("SELECT TOP 1 id FROM users");
      }
      UnitTestHelper.Main.WriteLine("..Successfully!");

      UnitTestHelper.Main.WriteLine("..Generating data for test..");
      using (SqlClient client = new SqlClient())
      {
        client.ExecuteNonQuery("DELETE FROM users");
        DataTable tbl = new DataTable("users");
        tbl.Columns.Add("first_name", typeof(string));
        tbl.Columns.Add("last_name", typeof(string));
        tbl.Columns.Add("birthday", typeof(DateTime));
        tbl.Rows.Add("Aleksey", "Fedorov", new DateTime(1980, 1, 1));
        tbl.Rows.Add("Fedor", "Alekseev", new DateTime(1981, 3, 15));
        tbl.Rows.Add("Anna", "Fedorova", new DateTime(1985, 7, 10));
        tbl.Rows.Add("Tasha", "Egorova", new DateTime(1986, 1, 8));
        tbl.Rows.Add("Ivan", "Ivanov", new DateTime(1982, 2, 1));
        tbl.Rows.Add("Jhon", "Smith", new DateTime(1985, 5, 7));
        tbl.Rows.Add("Roman", "Gavrilov", new DateTime(1980, 9, 15));
        tbl.Rows.Add("Elena", "Smith", new DateTime(1983, 3, 13));
        tbl.Rows.Add("Julia", "Petrova", new DateTime(1987, 1, 22));
        tbl.Rows.Add("Petr", "Romanov", new DateTime(1989, 12, 21));
        tbl.Rows.Add("Svetlana", "Elenovich", new DateTime(1980, 6, 17));
        tbl.Rows.Add("Regima", "Albertova", new DateTime(1978, 11, 10));
        client.CopyTableToServer(tbl);
      }
      UnitTestHelper.Main.WriteLine("..Successfully!");

      Console.ForegroundColor = ConsoleColor.White;
      UnitTestHelper.Main.WriteLine("..SqlClient(Int32):");
      Console.ForegroundColor = ConsoleColor.Gray;
      using (SqlClient client = new SqlClient(300))
      {
        UnitTestHelper.Main.WriteLine("....CacheDuration: {0}", client.CacheDuration);
        if (client.CacheDuration == 300)
        {
          UnitTestHelper.Main.WriteLine("....Successfully!");
        }
        else
        {
          UnitTestHelper.Main.WriteLine("....Fail...");
        }
        SqlClientMethods(client, true);
      }
      UnitTestHelper.Main.WriteLine("..Complete!");
      UnitTestHelper.Main.Pause("..");
      
      Console.ForegroundColor = ConsoleColor.White;
      UnitTestHelper.Main.WriteLine("..SqlClient(String, Int32):");
      Console.ForegroundColor = ConsoleColor.Gray;
      using (SqlClient client = new SqlClient(_ConnectionString, 300))
      {
        UnitTestHelper.Main.WriteLine("....CacheDuration: {0}", client.CacheDuration);
        if (client.CacheDuration == 300)
        {
          UnitTestHelper.Main.WriteLine("....Successfully!");
        }
        else
        {
          UnitTestHelper.Main.WriteLine("....Fail...");
        }
        SqlClientMethods(client, true);
      }
      UnitTestHelper.Main.WriteLine("..Complete!");
      UnitTestHelper.Main.Pause("..");

      Console.ForegroundColor = ConsoleColor.White;
      UnitTestHelper.Main.WriteLine("..SqlClient(String, String):");
      Console.ForegroundColor = ConsoleColor.Gray;
      using (SqlClient client = new SqlClient(_ConnectionString, @"C:\cache\unitTest"))
      {
        client.CacheDuration = 300;
        UnitTestHelper.Main.WriteLine("....CacheDuration: {0}", client.CacheDuration);
        if (client.CacheDuration == 300)
        {
          UnitTestHelper.Main.WriteLine("....Successfully!");
        }
        else
        {
          UnitTestHelper.Main.WriteLine("....Fail...");
        }
        UnitTestHelper.Main.WriteLine("....CachePath: {0}", client.CachePath);
        if (client.CachePath == @"C:\cache\unitTest")
        {
          UnitTestHelper.Main.WriteLine("....Successfully!");
        }
        else
        {
          UnitTestHelper.Main.WriteLine("....Fail...");
        }
        SqlClientMethods(client, true);
      }
      UnitTestHelper.Main.WriteLine("..Complete!");
      UnitTestHelper.Main.Pause("..");

      Console.ForegroundColor = ConsoleColor.White;
      UnitTestHelper.Main.WriteLine("..SqlClient(Type, Int32):");
      Console.ForegroundColor = ConsoleColor.Gray;
      using (SqlClient client = new SqlClient(typeof(UnitTestHelper.MyCache), 300))
      {
        UnitTestHelper.Main.WriteLine("....CacheDuration: {0}", client.CacheDuration);
        if (client.CacheDuration == 300)
        {
          UnitTestHelper.Main.WriteLine("....Successfully!");
        }
        else
        {
          UnitTestHelper.Main.WriteLine("....Fail...");
        }
        UnitTestHelper.Main.WriteLine("....CacheCustom: {0}", client.CacheCustom.ToString());
        if (client.CacheCustom == typeof(UnitTestHelper.MyCache))
        {
          UnitTestHelper.Main.WriteLine("....Successfully!");
        }
        else
        {
          UnitTestHelper.Main.WriteLine("....Fail...");
        }
        SqlClientMethods(client, true);
      }
      UnitTestHelper.Main.WriteLine("..Complete!");
      UnitTestHelper.Main.Pause("..");

      Console.ForegroundColor = ConsoleColor.White;
      UnitTestHelper.Main.WriteLine("..SqlClient(Type, Object[]):");
      Console.ForegroundColor = ConsoleColor.Gray;
      using (SqlClient client = new SqlClient(typeof(UnitTestHelper.MyCache), new object[] { 1, "test", DateTime.Now }))
      {
        client.CacheDuration = 300;
        UnitTestHelper.Main.WriteLine("....CacheDuration: {0}", client.CacheDuration);
        if (client.CacheDuration == 300)
        {
          UnitTestHelper.Main.WriteLine("....Successfully!");
        }
        else
        {
          UnitTestHelper.Main.WriteLine("....Fail...");
        }
        UnitTestHelper.Main.WriteLine("....CacheCustom: {0}", client.CacheCustom.ToString());
        if (client.CacheCustom == typeof(UnitTestHelper.MyCache))
        {
          UnitTestHelper.Main.WriteLine("....Successfully!");
        }
        else
        {
          UnitTestHelper.Main.WriteLine("....Fail...");
        }
        UnitTestHelper.Main.WriteLine("....CacheCustomArgs: {0}", String.Join(", ", client.CacheCustomArgs.Select(itm => itm.ToString())));
        if (client.CacheCustomArgs.Length == 3)
        {
          UnitTestHelper.Main.WriteLine("....Successfully!");
        }
        else
        {
          UnitTestHelper.Main.WriteLine("....Fail...");
        }
        //SqlClientMethods(client, true);
      }
      UnitTestHelper.Main.WriteLine("..Complete!");
      UnitTestHelper.Main.Pause("..");

      Console.ForegroundColor = ConsoleColor.White;
      UnitTestHelper.Main.WriteLine("..SqlClient(Type, Object[], Int32):");
      Console.ForegroundColor = ConsoleColor.Gray;
      using (SqlClient client = new SqlClient(typeof(UnitTestHelper.MyCache), new object[] { 1, "test", DateTime.Now }, 300))
      {
        UnitTestHelper.Main.WriteLine("....CacheDuration: {0}", client.CacheDuration);
        if (client.CacheDuration == 300)
        {
          UnitTestHelper.Main.WriteLine("....Successfully!");
        }
        else
        {
          UnitTestHelper.Main.WriteLine("....Fail...");
        }
        UnitTestHelper.Main.WriteLine("....CacheCustom: {0}", client.CacheCustom.ToString());
        if (client.CacheCustom == typeof(UnitTestHelper.MyCache))
        {
          UnitTestHelper.Main.WriteLine("....Successfully!");
        }
        else
        {
          UnitTestHelper.Main.WriteLine("....Fail...");
        }
        UnitTestHelper.Main.WriteLine("....CacheCustomArgs: {0}", String.Join(", ", client.CacheCustomArgs.Select(itm => itm.ToString())));
        if (client.CacheCustomArgs.Length == 3)
        {
          UnitTestHelper.Main.WriteLine("....Successfully!");
        }
        else
        {
          UnitTestHelper.Main.WriteLine("....Fail...");
        }
        //SqlClientMethods(client, true);
      }
      UnitTestHelper.Main.WriteLine("..Complete!");
      UnitTestHelper.Main.Pause("..");

      Console.ForegroundColor = ConsoleColor.White;
      UnitTestHelper.Main.WriteLine("..SqlClient(String, Int32, String, Type, Object[], Type):");
      Console.ForegroundColor = ConsoleColor.Gray;
      using (SqlClient client = new SqlClient("", 300, @"C:\cache\unitTest", null, null, typeof(UnitTestHelper.MyCacheBinder)))
      {
        UnitTestHelper.Main.WriteLine("....CacheDuration: {0}", client.CacheDuration);
        if (client.CacheDuration == 300)
        {
          UnitTestHelper.Main.WriteLine("....Successfully!");
        }
        else
        {
          UnitTestHelper.Main.WriteLine("....Fail...");
        }
        UnitTestHelper.Main.WriteLine("....CachePath: {0}", client.CachePath);
        if (client.CachePath == @"C:\cache\unitTest")
        {
          UnitTestHelper.Main.WriteLine("....Successfully!");
        }
        else
        {
          UnitTestHelper.Main.WriteLine("....Fail...");
        }
        UnitTestHelper.Main.WriteLine("....CacheBinder: {0}", client.CacheBinder.ToString());
        if (client.CacheBinder == typeof(UnitTestHelper.MyCacheBinder))
        {
          UnitTestHelper.Main.WriteLine("....Successfully!");
        }
        else
        {
          UnitTestHelper.Main.WriteLine("....Fail...");
        }
        SqlClientMethods(client, true);
      }
      UnitTestHelper.Main.WriteLine("..Complete!");
      UnitTestHelper.Main.Pause("..");


      Console.ForegroundColor = ConsoleColor.White;
      UnitTestHelper.Main.WriteLine("..SqlClient with FileCache:");
      Console.ForegroundColor = ConsoleColor.Gray;
      using (SqlClient client = new SqlClient())
      {
        client.CacheType = CachingType.File;
        client.CachePath = @"C:\cache\unitTest";
        client.CacheDuration = 300;
        UnitTestHelper.Main.WriteLine("....CacheType: {0}", client.CacheType.ToString());
        if (client.CacheType == CachingType.File)
        {
          UnitTestHelper.Main.WriteLine("....Successfully!");
        }
        else
        {
          UnitTestHelper.Main.WriteLine("....Fail...");
        }
        UnitTestHelper.Main.WriteLine("....CachePath: {0}", client.CachePath);
        if (client.CachePath == @"C:\cache\unitTest")
        {
          UnitTestHelper.Main.WriteLine("....Successfully!");
        }
        else
        {
          UnitTestHelper.Main.WriteLine("....Fail...");
        }
        UnitTestHelper.Main.WriteLine("....CacheDuration: {0}", client.CacheDuration);
        if (client.CacheDuration == 300)
        {
          UnitTestHelper.Main.WriteLine("....Successfully!");
        }
        else
        {
          UnitTestHelper.Main.WriteLine("....Fail...");
        }

        SqlClientMethods(client, true);
      }
      UnitTestHelper.Main.WriteLine("..Complete!");
      UnitTestHelper.Main.Pause("..");

      Console.ForegroundColor = ConsoleColor.White;
      UnitTestHelper.Main.WriteLine("..SqlClient with FileCache (default path):");
      Console.ForegroundColor = ConsoleColor.Gray;
      using (SqlClient client = new SqlClient())
      {
        client.CacheType = CachingType.File;
        client.CacheDuration = 300;
        UnitTestHelper.Main.WriteLine("....CacheType: {0}", client.CacheType.ToString());
        if (client.CacheType == CachingType.File)
        {
          UnitTestHelper.Main.WriteLine("....Successfully!");
        }
        else
        {
          UnitTestHelper.Main.WriteLine("....Fail...");
        }
        UnitTestHelper.Main.WriteLine("....CachePath: {0}", client.CachePath);
        if (String.IsNullOrEmpty(client.CachePath))
        {
          UnitTestHelper.Main.WriteLine("....Successfully!");
        }
        else
        {
          UnitTestHelper.Main.WriteLine("....Fail...");
        }
        UnitTestHelper.Main.WriteLine("....CacheDuration: {0}", client.CacheDuration);
        if (client.CacheDuration == 300)
        {
          UnitTestHelper.Main.WriteLine("....Successfully!");
        }
        else
        {
          UnitTestHelper.Main.WriteLine("....Fail...");
        }

        SqlClientMethods(client, true);
      }
      UnitTestHelper.Main.WriteLine("..Complete!");
      UnitTestHelper.Main.Pause("..");

      Console.ForegroundColor = ConsoleColor.White;
      UnitTestHelper.Main.WriteLine("..SqlClient with FileCache (default path, custom binder):");
      Console.ForegroundColor = ConsoleColor.Gray;
      using (SqlClient client = new SqlClient(null, 0, null, null, null, typeof(UnitTestHelper.MyCacheBinder)))
      {
        client.CacheType = CachingType.File;
        client.CacheDuration = 300;
        UnitTestHelper.Main.WriteLine("....CacheType: {0}", client.CacheType.ToString());
        if (client.CacheType == CachingType.File)
        {
          UnitTestHelper.Main.WriteLine("....Successfully!");
        }
        else
        {
          UnitTestHelper.Main.WriteLine("....Fail...");
        }
        UnitTestHelper.Main.WriteLine("....CachePath: {0}", client.CachePath);
        if (String.IsNullOrEmpty(client.CachePath))
        {
          UnitTestHelper.Main.WriteLine("....Successfully!");
        }
        else
        {
          UnitTestHelper.Main.WriteLine("....Fail...");
        }
        UnitTestHelper.Main.WriteLine("....CacheBinder: {0}", client.CacheBinder.ToString());
        if (client.CacheBinder == typeof(UnitTestHelper.MyCacheBinder))
        {
          UnitTestHelper.Main.WriteLine("....Successfully!");
        }
        else
        {
          UnitTestHelper.Main.WriteLine("....Fail...");
        }
        UnitTestHelper.Main.WriteLine("....CacheDuration: {0}", client.CacheDuration);
        if (client.CacheDuration == 300)
        {
          UnitTestHelper.Main.WriteLine("....Successfully!");
        }
        else
        {
          UnitTestHelper.Main.WriteLine("....Fail...");
        }

        SqlClientMethods(client, true);
      }
      UnitTestHelper.Main.WriteLine("..Complete!");
      UnitTestHelper.Main.Pause("..");

      Console.ForegroundColor = ConsoleColor.White;
      UnitTestHelper.Main.WriteLine("..SqlClient without cache:");
      Console.ForegroundColor = ConsoleColor.Gray;
      using (SqlClient client = new SqlClient())
      {
        UnitTestHelper.Main.WriteLine("....CacheDuration: {0}", client.CacheDuration);
        if (client.CacheDuration == 0)
        {
          UnitTestHelper.Main.WriteLine("....Successfully!");
        }
        else
        {
          UnitTestHelper.Main.WriteLine("....Fail...");
        }

        SqlClientMethods(client, false);
      }
      UnitTestHelper.Main.WriteLine("..Complete!");
      UnitTestHelper.Main.Pause("..");

      Console.ForegroundColor = ConsoleColor.White;
      UnitTestHelper.Main.WriteLine("..SqlClient cache objects list:");
      Console.ForegroundColor = ConsoleColor.Gray;
      Nemiro.Data.Caching.CacheManager.Items.ForEach(itm => UnitTestHelper.Main.WriteLine("....{0}", itm.ToString()));
      if (Nemiro.Data.Caching.CacheManager.Items.Count == 4)
      {
        UnitTestHelper.Main.WriteLine("....Successfully!");
      }
      else
      {
        UnitTestHelper.Main.WriteLine("....Fail...");
      }
      UnitTestHelper.Main.Pause("..");

      if (File.Exists("CSharpSqlClientConfigTest.exe"))
      {
        var psi = new ProcessStartInfo("CSharpSqlClientConfigTest.exe") { UseShellExecute = false };
        var p = Process.Start(psi);
        p.WaitForExit();
      }
      else
      {
        UnitTestHelper.Main.WriteLine("..File \"CSharpSqlClientConfigTest.exe\" not found...");
        UnitTestHelper.Main.WriteLine("..Skip...");
      }

      UnitTestHelper.Main.Pause("..");

      if (File.Exists("VBSqlClientConfigTest.exe"))
      {
        var psi = new ProcessStartInfo("VBSqlClientConfigTest.exe") { UseShellExecute = false };
        var p = Process.Start(psi);
        p.WaitForExit();
      }
      else
      {
        UnitTestHelper.Main.WriteLine("..File \"VBSqlClientConfigTest.exe\" not found...");
        UnitTestHelper.Main.WriteLine("..Skip...");
      }

      UnitTestHelper.Main.Pause("..");

      Console.ForegroundColor = ConsoleColor.Yellow;
      UnitTestHelper.Main.WriteLine("Complete!"); 
      Console.ForegroundColor = ConsoleColor.Gray;
    
      #endregion
      #region ..SqlAdmin..

      Console.ForegroundColor = ConsoleColor.Yellow;
      UnitTestHelper.Main.WriteLine("SqlAdmin test...");
      Console.ForegroundColor = ConsoleColor.Gray;

      using (SqlAdmin admin = new SqlAdmin())
      {
        UnitTestHelper.Main.WriteLine("..TableIsExists");
        bool result = false;
        if (result = admin.TableIsExists("test"))
        {
          UnitTestHelper.Main.WriteLine("..Result: {0}", result);
          UnitTestHelper.Main.WriteLine("..Successfully!");

          UnitTestHelper.Main.Pause("..");

          UnitTestHelper.Main.WriteLine("..DeleteTable");
          admin.DeleteTable("test");
          UnitTestHelper.Main.WriteLine("..Successfully!");
        }
        else
        {
          UnitTestHelper.Main.WriteLine("..Result: {0}", result);
          UnitTestHelper.Main.WriteLine("..Successfully!");
        }

        UnitTestHelper.Main.Pause("..");

        UnitTestHelper.Main.WriteLine("..CreateTable");
        DataTable table = new DataTable("test");
        table.Columns.Add(new DataColumn("id", typeof(int)) { AutoIncrement = true, AllowDBNull = false });
        table.Columns.Add(new DataColumn("short_text", typeof(string)) { MaxLength = 100 });
        table.Columns.Add("text", typeof(string));
        table.Columns.Add("date_created", typeof(DateTime));
        admin.CreateTable(table);
        UnitTestHelper.Main.WriteLine("..Successfully!");

        UnitTestHelper.Main.Pause("..");

        UnitTestHelper.Main.WriteLine("..ClearTable");
        admin.ClearTable("test");
        UnitTestHelper.Main.WriteLine("..Successfully!");

        UnitTestHelper.Main.Pause("..");

        UnitTestHelper.Main.WriteLine("..DeleteTable");
        admin.DeleteTable("test");
        UnitTestHelper.Main.WriteLine("..Successfully!");

        UnitTestHelper.Main.Pause("..");

        UnitTestHelper.Main.WriteLine("..GetAllTables");
        DataTable tables0 = admin.GetAllTables();
        foreach (DataRow row in tables0.Rows)
        {
          UnitTestHelper.Main.WriteLine("....{0}\t{1}\t{2}\t{3}", row["TABLE_NAME"], row["TABLE_SCHEMA"], row["TABLE_CATALOG"], row["TABLE_TYPE"]);
        }
        UnitTestHelper.Main.WriteLine("..Successfully!");

        UnitTestHelper.Main.Pause("..");

        UnitTestHelper.Main.WriteLine("..GetAllTablesName");
        string[] tables = admin.GetAllTablesName();
        foreach (string tableName in tables)
        {
          UnitTestHelper.Main.WriteLine("....{0}", tableName);
        }
        UnitTestHelper.Main.WriteLine("..Successfully!");

        UnitTestHelper.Main.Pause("..");

        UnitTestHelper.Main.WriteLine("..GetAllTablesSize");
        DataTable tablesSize = admin.GetAllTablesSize();
        foreach (DataRow row in tablesSize.Rows)
        {
          UnitTestHelper.Main.WriteLine("....{0}\t{1}\t{2}", row["table_name"], row["rows"], row["data_size"]);
        }
        UnitTestHelper.Main.WriteLine("..Successfully!");

        UnitTestHelper.Main.WriteLine("..GetSqlServerVersion");
        UnitTestHelper.Main.WriteLine("....{0}", admin.GetSqlServerVersion().ToString());
        UnitTestHelper.Main.WriteLine("..Successfully!");

        UnitTestHelper.Main.Pause("..");

        UnitTestHelper.Main.WriteLine("..GetTableColums");
        DataTable tableColumns = admin.GetTableColums("users");
        foreach (DataRow row in tableColumns.Rows)
        {
          UnitTestHelper.Main.WriteLine("....{0}", row["column_name"]);
        }
        UnitTestHelper.Main.WriteLine("..Successfully!");

        UnitTestHelper.Main.Pause("..");

        UnitTestHelper.Main.WriteLine("..GetTablePrimaryKey");
        UnitTestHelper.Main.WriteLine("....{0}", admin.GetTablePrimaryKey("users"));
        UnitTestHelper.Main.WriteLine("..Successfully!");

        UnitTestHelper.Main.Pause("..");

        Console.ForegroundColor = ConsoleColor.Yellow;
        UnitTestHelper.Main.WriteLine("Complete!");
        Console.ForegroundColor = ConsoleColor.Gray;
      }

      #endregion
      #region ..ORM Test..

      Console.ForegroundColor = ConsoleColor.Yellow;
      UnitTestHelper.Main.WriteLine("ORM test...");
      Console.ForegroundColor = ConsoleColor.Gray;

      TestTable tt = new TestTable();
      UnitTestHelper.Main.WriteLine("..TableIsExists");
      bool ttResult = false;
      if (!(ttResult = tt.TableExists()))
      {
        UnitTestHelper.Main.WriteLine("..Result: {0}", ttResult);
        UnitTestHelper.Main.WriteLine("..Successfully!");
        UnitTestHelper.Main.Pause("..");
        UnitTestHelper.Main.WriteLine("..CreateTable");
        tt.CreateTable();
        UnitTestHelper.Main.WriteLine("..Successfully!");
      }
      else
      {
        UnitTestHelper.Main.WriteLine("..Result: {0}", ttResult);
        UnitTestHelper.Main.WriteLine("..DeleteTable");
        new SqlAdmin().DeleteTable(tt.TableName);
        UnitTestHelper.Main.WriteLine("..CreateTable");
        tt.CreateTable();
        UnitTestHelper.Main.WriteLine("..Successfully!");
      }

      UnitTestHelper.Main.Pause("..");

      UnitTestHelper.Main.WriteLine("..Generating data for test..");
      for (int i = 1; i <= 100; i++)
      {
        tt = new TestTable();
        tt.Guid1 = Guid.NewGuid();
        tt.Value = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10);
        tt.DateCreated = DateTime.Now;
        tt.Save();
      }
      UnitTestHelper.Main.WriteLine("..Successfully!");

      UnitTestHelper.Main.Pause("..");

      UnitTestHelper.Main.WriteLine("..Get entries list..");
      UnitTestHelper.Main.WriteLine("..Count: {0}", TestTable.GetList().Count);
      UnitTestHelper.Main.WriteLine("..Successfully!");

      UnitTestHelper.Main.Pause("..");

      UnitTestHelper.Main.WriteLine("..Get entries list by pages..");
      for (int i = 1; i <= 10; i++)
      {
        var list = TestTable.GetList(10, i);
        UnitTestHelper.Main.WriteLine("..Count: {0}; from id {1} to {2}", list.Count, list.First().Id, list.Last().Id);
      }
      UnitTestHelper.Main.WriteLine("..Successfully!");

      UnitTestHelper.Main.Pause("..");

      Random rnd = new Random(DateTime.Now.Millisecond);
      for (int i = 1; i <= 5; i++)
      {
        int id = rnd.Next(1, 100);
        UnitTestHelper.Main.WriteLine("..Get data by id {0}:", id);
        tt = new TestTable(id);
        if (tt.Id <= 0)
        {
          UnitTestHelper.Main.WriteLine("..Fail...");
        }
        else
        {
          UnitTestHelper.Main.WriteLine("..{0}", tt);
          UnitTestHelper.Main.WriteLine("..Successfully!");
        }
      }

      UnitTestHelper.Main.Pause("..");

      for (int i = 1; i <= 5; i++)
      {
        int id = rnd.Next(1, 100);
        UnitTestHelper.Main.WriteLine("..Get data by id {0}, change and save:", id);
        tt = new TestTable(id);
        if (tt.Id <= 0)
        {
          UnitTestHelper.Main.WriteLine("..Fail...");
        }
        else
        {
          UnitTestHelper.Main.WriteLine("..{0}", tt);
          tt.Value = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10);
          tt.Date4 = DateTime.Now;
          tt.Save();
          UnitTestHelper.Main.WriteLine("..{0}", tt);
          UnitTestHelper.Main.WriteLine("..Successfully!");
        }
      }

      UnitTestHelper.Main.Pause("..");

      for (int i = 1; i <= 5; i++)
      {
        int id = rnd.Next(1, 100);
        UnitTestHelper.Main.WriteLine("..Get data by id {0} with caching:", id);
        tt = new TestTable(id, 5);
        if (tt.Id <= 0)
        {
          UnitTestHelper.Main.WriteLine("..Fail...");
        }
        else
        {
          UnitTestHelper.Main.WriteLine("..{0}", tt);
          UnitTestHelper.Main.WriteLine("..Change and save");
          tt.Value = "";
          tt.Save();
          UnitTestHelper.Main.WriteLine("..Get data again");
          tt = new TestTable(id, 5);
          if (tt.Id <= 0 || String.IsNullOrEmpty(tt.Value))
          {
            UnitTestHelper.Main.WriteLine("..Fail...");
          }
          UnitTestHelper.Main.WriteLine("..Wait 5 sec.");
          Thread.Sleep(5100);
          UnitTestHelper.Main.WriteLine("..Get data again");
          tt = new TestTable(id, 5);
          if (tt.Id <= 0 || !String.IsNullOrEmpty(tt.Value))
          {
            UnitTestHelper.Main.WriteLine("..Fail...");
          }
          else
          {
            UnitTestHelper.Main.WriteLine("..Successfully!");
          }
        }
      }

      UnitTestHelper.Main.Pause("..");

      for (int i = 1; i <= 5; i++)
      {
        int id = rnd.Next(1, 100);
        UnitTestHelper.Main.WriteLine("..Get data by id {0} and delete:", id);
        tt = new TestTable(id);
        if (tt.Id <= 0)
        {
          UnitTestHelper.Main.WriteLine("..Fail...");
        }
        else
        {
          UnitTestHelper.Main.WriteLine("..{0}", tt);
          tt.Delete();
          tt = new TestTable(id);
          if (tt.Id <= 0)
          {
            UnitTestHelper.Main.WriteLine("..Successfully!");
          }
          else
          {
            UnitTestHelper.Main.WriteLine("..Fail...");
          }
        }
      }

      UnitTestHelper.Main.Pause("..");

      Console.ForegroundColor = ConsoleColor.Yellow;
      UnitTestHelper.Main.WriteLine("Complete!");
      Console.ForegroundColor = ConsoleColor.Gray;

      #endregion

      UnitTestHelper.Main.WriteLine("Press any key to exit...");

      Console.ReadKey();
    }

    private static void SqlClientMethods(SqlClient client, bool checkCache)
    {
      Console.ForegroundColor = ConsoleColor.Cyan;
      UnitTestHelper.Main.WriteLine("....ExecuteScalar");
      Console.ForegroundColor = ConsoleColor.Gray;
      UnitTestHelper.Main.WriteLine("......Query execution...");
      object result = client.ExecuteScalar("SELECT TOP 1 id FROM users");
      UnitTestHelper.Main.WriteLine("......Result: {0}", result);
      UnitTestHelper.Main.WriteLine("......LastQueryResultsFromCache: {0}", client.LastQueryResultsFromCache);

      if (result != null && !client.LastQueryResultsFromCache)
      {
        UnitTestHelper.Main.Pause("......");
        UnitTestHelper.Main.WriteLine("......Query execution...");
        result = client.ExecuteScalar("SELECT TOP 1 id FROM users");
        UnitTestHelper.Main.WriteLine("......Result: {0}", result);
        UnitTestHelper.Main.WriteLine("......LastQueryResultsFromCache: {0}", client.LastQueryResultsFromCache);
        SqlClientCacheMethodsCheck(client, checkCache, "......");
      }
      else
      {
        UnitTestHelper.Main.WriteLine("......Skip...");
      }

      UnitTestHelper.Main.Pause("......");

      Console.ForegroundColor = ConsoleColor.Cyan;
      UnitTestHelper.Main.WriteLine("....ExecuteNonQuery");
      Console.ForegroundColor = ConsoleColor.Gray;
      UnitTestHelper.Main.WriteLine("......Query execution...");
      int result2 = client.ExecuteNonQuery("UPDATE users SET last_name = 'Test' WHERE id = (SELECT TOP 1 id FROM users ORDER BY NEWID())");
      UnitTestHelper.Main.WriteLine("......Result: {0}", result2);
      UnitTestHelper.Main.WriteLine("......LastQueryResultsFromCache: {0}", client.LastQueryResultsFromCache);


      if (result2 > 0 && !client.LastQueryResultsFromCache)
      {
        UnitTestHelper.Main.Pause("......");
        UnitTestHelper.Main.WriteLine("......Query execution...");
        result2 = client.ExecuteNonQuery("UPDATE users SET last_name = 'Test' WHERE id = (SELECT TOP 1 id FROM users ORDER BY NEWID())");
        UnitTestHelper.Main.WriteLine("......Result: {0}", result2);
        UnitTestHelper.Main.WriteLine("......LastQueryResultsFromCache: {0}", client.LastQueryResultsFromCache);
        if (checkCache)
        {
          if (!client.LastQueryResultsFromCache)
          {
            UnitTestHelper.Main.WriteLine("......Successfully!");
          }
          else
          {
            UnitTestHelper.Main.WriteLine("......Fail...");
          }
        }
      }
      else
      {
        UnitTestHelper.Main.WriteLine("......Skip...");
      }
      UnitTestHelper.Main.Pause("......");

      Console.ForegroundColor = ConsoleColor.Cyan;
      UnitTestHelper.Main.WriteLine("....GetData");
      Console.ForegroundColor = ConsoleColor.Gray;
      UnitTestHelper.Main.WriteLine("......Query execution...");
      DataSet data = client.GetData("SELECT TOP 10 * FROM users");
      UnitTestHelper.Main.WriteLine("......Result: {0}", data.Tables.Count);
      UnitTestHelper.Main.WriteLine("......LastQueryResultsFromCache: {0}", client.LastQueryResultsFromCache);

      if (data.Tables.Count > 0 && data.Tables[0].Rows.Count > 0 && !client.LastQueryResultsFromCache)
      {
        UnitTestHelper.Main.Pause("......");
        UnitTestHelper.Main.WriteLine("......Query execution...");
        data = client.GetData("SELECT TOP 10 * FROM users");
        UnitTestHelper.Main.WriteLine("......Result: {0}", data.Tables.Count);
        UnitTestHelper.Main.WriteLine("......LastQueryResultsFromCache: {0}", client.LastQueryResultsFromCache);
        SqlClientCacheMethodsCheck(client, checkCache, "......");
      }
      else
      {
        UnitTestHelper.Main.WriteLine("......Skip...");
      }

      UnitTestHelper.Main.Pause("......");

      Console.ForegroundColor = ConsoleColor.Cyan;
      UnitTestHelper.Main.WriteLine("....GetTable");
      Console.ForegroundColor = ConsoleColor.Gray;
      UnitTestHelper.Main.WriteLine("......Query execution...");
      DataTable table = client.GetTable("SELECT TOP 10 * FROM users");
      UnitTestHelper.Main.WriteLine("......Result: {0}", table.Rows.Count);
      UnitTestHelper.Main.WriteLine("......LastQueryResultsFromCache: {0}", client.LastQueryResultsFromCache);

      if (table.Rows.Count > 0 && !client.LastQueryResultsFromCache)
      {
        UnitTestHelper.Main.Pause("......");
        UnitTestHelper.Main.WriteLine("......Query execution...");
        table = client.GetTable("SELECT TOP 10 * FROM users");
        UnitTestHelper.Main.WriteLine("......Result: {0}", table.Rows.Count);
        UnitTestHelper.Main.WriteLine("......LastQueryResultsFromCache: {0}", client.LastQueryResultsFromCache);
        SqlClientCacheMethodsCheck(client, checkCache, "......");
      }
      else
      {
        UnitTestHelper.Main.WriteLine("......Skip...");
      }

      UnitTestHelper.Main.Pause("......");

      Console.ForegroundColor = ConsoleColor.Cyan;
      UnitTestHelper.Main.WriteLine("....GetRow");
      Console.ForegroundColor = ConsoleColor.Gray;
      UnitTestHelper.Main.WriteLine("......Query execution...");
      DataRow row = client.GetRow("SELECT TOP 1 * FROM users");
      UnitTestHelper.Main.WriteLine("......Result: {0}", row);
      UnitTestHelper.Main.WriteLine("......LastQueryResultsFromCache: {0}", client.LastQueryResultsFromCache);

      if (row != null && !client.LastQueryResultsFromCache)
      {
        UnitTestHelper.Main.Pause("......");
        UnitTestHelper.Main.WriteLine("......Query execution...");
        row = client.GetRow("SELECT TOP 1 * FROM users");
        UnitTestHelper.Main.WriteLine("......Result: {0}", row);
        UnitTestHelper.Main.WriteLine("......LastQueryResultsFromCache: {0}", client.LastQueryResultsFromCache);
        SqlClientCacheMethodsCheck(client, checkCache, "......");
      }
      else
      {
        UnitTestHelper.Main.WriteLine("......Skip...");
      }

      UnitTestHelper.Main.Pause("......");

      Nemiro.Data.Caching.CacheManager.Items.ForEach(itm => itm.ToList().ForEach(x => itm.Remove(x.Key)));

      UnitTestHelper.Main.WriteLine("....Complete!");
    }
    private static void SqlClientCacheMethodsCheck(SqlClient client, bool checkCache, string prefix = "")
    {
      if (checkCache)
      {
        if (client.LastQueryResultsFromCache)
        {
          UnitTestHelper.Main.WriteLine(String.Format("{0}Successfully!", prefix));
        }
        else
        {
          UnitTestHelper.Main.WriteLine(String.Format("{0}Fail...", prefix));
        }
      }
      else
      {
        if (!client.LastQueryResultsFromCache)
        {
          UnitTestHelper.Main.WriteLine(String.Format("{0}Successfully!", prefix));
        }
        else
        {
          UnitTestHelper.Main.WriteLine(String.Format("{0}Fail...", prefix));
        }
      }
    }

  }
}
