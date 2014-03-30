using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Nemiro.Data.Caching;

// add refrence
// System.Runtime.Caching

namespace CSharpUsingFileCache
{
  class Program
  {

    static void Main(string[] args)
    {
      Console.WriteLine("Test FileCache from Nemiro.Data v{0}", Assembly.GetAssembly(typeof(Nemiro.Data.Sql.SqlClient)).GetName().Version);
      Console.WriteLine("");

      // создаем список различных вариантов файлового кэша 
      // для проведения серии тестов
      FileCache[] fileCacheTests = 
      {
        // default FileCache
        // файловый кэш с параметрами по умолчанию
        CacheManager.FileCache,
        // FileCache with a specific file storage
        // файловый кэш с указанием хранилища файлов
        //CacheManager.GetFileCache(@"C:\cache\myapp"),
        // FileCache with a specific file storage, BufferSize 
        // and specific BufferAccessTimeout
        // файловый кэш с указанием хранилища файлов, объемом буфера в 100 Мб 
        // и временем ожидания освобождения буфера
        new FileCache(@"C:\cache\myapp", 102400, null, TimeSpan.Zero, new TimeSpan(0, 0, 1)),
        // FileCache with a specific file storage and without BufferSize
        // файловый кэш без использования буфера
        new FileCache(@"C:\cache\myapp", 0),
        // FileCache with a specific file storage and without BufferSize,
        // but with specific AccessTimeout
        // файловый кэш без использования буфера, 
        // но с указанием времени ожидания освобождения файлов записей кэша
        new FileCache(@"C:\cache\myapp", 0, null, new TimeSpan(0, 0, 1), TimeSpan.Zero),
        // FileCache with a specific file storage, BufferSize, 
        // BufferAccessTimeout and AccessTimeout
        // файловый кэш c буфером, заданным временем ожиданения освобождения буфера
        // и с указанием времени ожидания освобождения файлов записей кэша
        new FileCache(@"C:\cache\myapp", 102400, null, new TimeSpan(0, 0, 1),  new TimeSpan(0, 0, 1))
      };

      // листаем список конфигураций файлового кэша и тестируем их
      foreach (FileCache fileCache in fileCacheTests)
      {
        // выводим информацию о конфигурации тестируемого файлового кэша
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Testing FileCache");
        Console.WriteLine("{");
        Console.WriteLine("  CachePath: {0}", fileCache.CachePath);
        Console.WriteLine("  BufferSize: {0}", fileCache.BufferSize);
        Console.WriteLine("  BufferAccessTimeout: {0}", fileCache.BufferAccessTimeout.ToString());
        Console.WriteLine("  AccessTimeout: {0}", fileCache.AccessTimeout.ToString());
        Console.WriteLine("}");
        Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.Gray;
        // pause 1 sec.
        Thread.Sleep(1000);

        if (true) //set false for ignore
        {
          #region simple values

          // добавляем простые данные в кэш сроком на один час
          Console.WriteLine("Caching of simple values...");
          fileCache.Add("int", 123, DateTimeOffset.Now.AddHours(1));
          fileCache.Add("double", 3.14, DateTimeOffset.Now.AddHours(1));
          fileCache.Add("string", "hello, world!", DateTimeOffset.Now.AddHours(1));
          fileCache.Add("date", DateTime.Now, DateTimeOffset.Now.AddHours(1));
          Console.WriteLine("Cached!");
          Console.WriteLine("");
          // pause 1 sec.
          Thread.Sleep(1000);

          // выводим данные из кэша в консоль
          Console.WriteLine("Output data from cache...");
          Console.WriteLine("int: {0}", fileCache["int"]);
          Console.WriteLine("double: {0}", fileCache["double"]);
          Console.WriteLine("string: {0}", fileCache["string"]);
          Console.WriteLine("date: {0}", fileCache["date"]);
          Console.WriteLine("Successfully!");
          Console.WriteLine("");
          // pause 1 sec.
          Thread.Sleep(1000);

          #endregion
          #region caching and wait for delete

          // кжшируем данные на 5 секунд
          Console.WriteLine("Caching data for 5 seconds...");
          fileCache.Add("int5", 12345, DateTimeOffset.Now.AddSeconds(5));
          Console.WriteLine("Cached!");
          // делаем пузу на 5 секунд
          Console.WriteLine("Wait 5 seconds...");
          Thread.Sleep(5000);

          // проверяем, если данных в кэше нет, значит все работает правильно
          Console.WriteLine("Output data from cache...");
          if (fileCache["int5"] == null)
          {
            Console.WriteLine("Data has been removed from cache.");
            Console.WriteLine("Successfully!");
          }
          else
          {
            Console.WriteLine("Error. Data has not removed.");
            Console.WriteLine("Wait 1 seconds...");
            Thread.Sleep(1000);
            if (fileCache["int5"] == null)
            {
              Console.WriteLine("Data has been removed from cache.");
              Console.WriteLine("Successfully!");
            }
            else
            {
              Console.WriteLine("Error. Data has not removed.");
            }
          }

          Console.WriteLine("");
          // pause 1 sec.
          Thread.Sleep(1000);

          #endregion
          #region caching dataSet

          // добавляем в кэш DataSet
          Console.WriteLine("Caching DataSet...");
          DataTable DT = new System.Data.DataTable();
          DT.Columns.Add("id");
          DT.Columns.Add("first_name");
          DT.Columns.Add("last_name");
          DT.Rows.Add("1", "Ivan", "Ivanushkin");
          DT.Rows.Add("2", "Masha", "Ivanova");
          DataSet DS = new DataSet();
          DS.Tables.Add(DT);
          fileCache.Add("DataSet", DS, DateTime.Now.AddMonths(1));
          Console.WriteLine("Cached!");
          Console.WriteLine("");
          // pause 1 sec.
          Thread.Sleep(1000);

          // получаем данные из кэша и выводим
          Console.WriteLine("Output data from cache...");
          foreach (DataRow row in ((DataSet)fileCache["DataSet"]).Tables[0].Rows)
          {
            Console.WriteLine("{0}\t{1}\t{2}", row[0], row[1], row[2]);
          }
          Console.WriteLine("Successfully!");
          Console.WriteLine("");
          // pause 1 sec.
          Thread.Sleep(1000);

          #endregion
          #region caching hashtable

          // кэшируем Hashtable
          Console.WriteLine("Caching Hashtable...");
          var h = new Hashtable();
          h.Add("1", "1");
          h.Add("test", DateTime.Now);
          fileCache.Add("hashtable", h, DateTime.Now.AddSeconds(180));
          Console.WriteLine("Cached!");
          Console.WriteLine("");
          // pause 1 sec.
          Thread.Sleep(1000);
          // выводим  Hashtable их кэша
          Console.WriteLine("Output data from cache...");
          Console.WriteLine(((Hashtable)fileCache["hashtable"])["test"]);
          Console.WriteLine("Successfully!");
          Console.WriteLine("");
          // pause 1 sec.
          Thread.Sleep(1000);

          #endregion
          #region caching dictionary

          // кэшируем Dictionary
          Console.WriteLine("Caching Dictionary<string, object>...");
          var d = new Dictionary<string, object>();
          d.Add("date", DateTime.Now);
          d.Add("string", "hello, world!");
          fileCache.Add("dictionary", d, DateTime.Now.AddMinutes(20));
          Console.WriteLine("Cached!");
          Console.WriteLine("");
          // pause 1 sec.
          Thread.Sleep(1000);

          Console.WriteLine("Output data from cache...");
          Console.WriteLine(((Dictionary<string, object>)fileCache["dictionary"])["string"]);
          Console.WriteLine("Successfully!");
          Console.WriteLine("");
          // pause 1 sec.
          Thread.Sleep(1000);

          #endregion
          #region caching custom object

          Console.WriteLine("Caching custom object...");
          var co = new CustomObject
          {
            Amount = 100,
            DateCreated = DateTime.Now,
            ID = Guid.NewGuid(),
            Name = "Test"
          };
          fileCache.Add("custom", co, DateTime.Now.AddMinutes(30));
          Console.WriteLine("Cached!");
          Console.WriteLine("");
          // pause 1 sec.
          Thread.Sleep(1000);

          Console.WriteLine("Output data from cache...");

          var co2 = (CustomObject)fileCache["custom"];

          Console.WriteLine("ID: {0}", co2.ID);
          Console.WriteLine("Name: {0}", co2.Name);
          Console.WriteLine("Amount: {0}", co2.Amount);
          Console.WriteLine("Date: {0}", co2.DateCreated);
          Console.WriteLine("Successfully!");
          Console.WriteLine("");
          // pause 1 sec.
          Thread.Sleep(1000);

          #endregion
          #region caching custom object collection

          Console.WriteLine("Caching custom object collection...");
          var coc = new List<CustomObject>();
          for (int i = 1; i <= 10; i++)
          {
            coc.Add
            (
              new CustomObject
              {
                Amount = 100,
                DateCreated = DateTime.Now,
                ID = Guid.NewGuid(),
                Name = "Test"
              }
            );
          }
          fileCache.Add("customCollection", coc, DateTime.Now.AddMinutes(30));
          Console.WriteLine("Cached!");
          Console.WriteLine("");
          // pause 1 sec.
          Thread.Sleep(1000);

          Console.WriteLine("Output data from cache...");

          var coc2 = (List<CustomObject>)fileCache["customCollection"];
          Console.WriteLine("Count: {0}", coc2.Count);
          Console.WriteLine("Successfully!");
          Console.WriteLine("");
          // pause 1 sec.
          Thread.Sleep(1000);

          #endregion
        }
        #region multi-threads caching

        Console.WriteLine("Multi-threads caching...");

        var sw = new Stopwatch();
        sw.Start();
        int errors = 0, ok = 0;
        var tasks = new List<Thread>();
        Random rnd = new Random(DateTime.Now.Millisecond);
        for (int i = 1; i <= 100; i++)
        {
          tasks.Add
          (new Thread
            (
              () =>
              {
                try
                {
                  // get data from cache
                  var getData = fileCache["anyData"];
                  if (getData == null)
                  {
                    // create new data
                    List<byte> anyData = new List<byte>();
                    int jc = rnd.Next(1024, 524288);
                    for (int j = 0; j <= jc; j++)
                    {
                      anyData.Add((byte)rnd.Next(0, 255));
                    }
                    fileCache.Add("anyData", anyData, DateTimeOffset.Now.AddHours(1));
                  }
                  // counter
                  ok++;
                }
                catch(Exception ex)
                {
                  //Console.WriteLine(ex.Message);
                  errors++;
                }
              }
            )
          );
          tasks.Last().Start();
        }

        // wait
        while (tasks.Count(t => t.IsAlive) > 0)
        {
          if (ok + errors == 100) break;
          if (sw.ElapsedMilliseconds > 30000)
          {
            Console.WriteLine("Timeout...");
            break;
          }
          Thread.Sleep(100);
        }

        tasks.ForEach(t => t.Abort());
        tasks = null;

        sw.Stop();

        // выводим результат в консоль
        Console.WriteLine("");
        Console.WriteLine("Successfully: {0}; Errors: {1}", ok, errors);
        Console.WriteLine("Total time: {0}", sw.Elapsed.ToString());
        Console.WriteLine("");
        // pause 1 sec.
        Thread.Sleep(1000);

        #endregion
        #region multi-threads caching2

        // in the threads can be access errors
        // it's normal :)
        // property <AccessTimeout> can partially solve this problem

        // в потоках могут быть ошибки доступа, особенно при частой перезаписи данных
        // без этого никуда :)
        // свойство <AccessTimeout> позволяет частично решить эту проблему

        sw.Restart();
        Console.WriteLine("Multi-threads rewriting data caching...");

        errors = 0; ok = 0;
        rnd = new Random(DateTime.Now.Millisecond);
        tasks = new List<Thread>();
        for (int i = 1; i <= 100; i++)
        {
          tasks.Add
          (
            new Thread
            (
              () =>
              {
                try
                {
                  // get data from cache
                  var getData = fileCache["anyData"];
                  // create new data
                  List<byte> anyData = new List<byte>();
                  int jc = rnd.Next(1024, 524288);
                  for (int j = 0; j <= jc; j++)
                  {
                    anyData.Add((byte)rnd.Next(0, 255));
                  }
                  fileCache.Add("anyData", anyData, DateTimeOffset.Now.AddHours(1));
                  // counter
                  ok++;
                }
                catch (Exception ex)
                {
                  //Console.WriteLine(ex.Message);
                  errors++;
                }
              }
            )
          );
          tasks.Last().Start();
        }

        // wait
        while (tasks.Count(t => t.IsAlive) > 0)
        {
          if (ok + errors == 100) break;
          if (sw.ElapsedMilliseconds > 30000)
          {
            Console.WriteLine("");
            Console.WriteLine("Timeout...");
            break;
          }
          Thread.Sleep(100);
        }

        tasks.ForEach(t => t.Abort());
        tasks = null;

        sw.Stop();

        // выводим результат в консоль
        Console.WriteLine("");
        Console.WriteLine("Successfully: {0}; Errors: {1}", ok, errors);
        Console.WriteLine("Total time: {0}", sw.Elapsed.ToString());
        Console.WriteLine("");
        // pause 1 sec.
        Thread.Sleep(1000);

        #endregion
        #region multi-threads caching3

        sw.Restart();
        Console.WriteLine("Multi-threads different data caching...");

        errors = 0; ok = 0;
        rnd = new Random(DateTime.Now.Millisecond);
        tasks = new List<Thread>();
        for (int i = 1; i <= 100; i++)
        {
          tasks.Add
          (
            new Thread
            (
              () =>
              {
                try
                {
                  // get data from cache
                  var id = Guid.NewGuid().ToString();
                  var getData = fileCache[id];
                  // create new data
                  List<byte> anyData = new List<byte>();
                  int jc = rnd.Next(1024, 524288);
                  for (int j = 0; j <= jc; j++)
                  {
                    anyData.Add((byte)rnd.Next(0, 255));
                  }
                  fileCache.Add(id, anyData, DateTimeOffset.Now.AddHours(1));
                  // counter
                  ok++;
                }
                catch (Exception ex)
                {
                  //Console.WriteLine(ex.Message);
                  errors++;
                }
              }
            )
          );
          tasks.Last().Start();
        }

        // wait
        while (tasks.Count(t => t.IsAlive) > 0)
        {
          if (ok + errors == 100) break;
          if (sw.ElapsedMilliseconds > 30000)
          {
            Console.WriteLine("");
            Console.WriteLine("Timeout...");
            break;
          }
          Thread.Sleep(100);
        }

        tasks.ForEach(t => t.Abort());
        tasks = null;

        sw.Stop();

        // выводим результат в консоль
        Console.WriteLine("");
        Console.WriteLine("Successfully: {0}; Errors: {1}", ok, errors);
        Console.WriteLine("Total time: {0}", sw.Elapsed.ToString());
        Console.WriteLine("");
        // pause 1 sec.
        Thread.Sleep(1000);

        #endregion
        #region cleanup

        // удаляем все данные из кэша
        Console.WriteLine("Cleanup cache...");
        var cleanupResult = fileCache.CleanupAll();
        // выводим результат очистки кэша в консоль
        Console.WriteLine("Removed: {0} ({1:0.00} Kb); Errors: {2}", cleanupResult.Removed, (double)cleanupResult.RemovedSize / 1024, cleanupResult.Errors);
        Console.WriteLine("");
        // pause 1 sec.
        Thread.Sleep(1000);

        #endregion

        Console.Beep();
        Console.WriteLine("");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.WriteLine("");
        Console.BackgroundColor = ConsoleColor.Gray;
        Console.WriteLine("                                      ");
        Console.BackgroundColor = ConsoleColor.Black;
        Console.WriteLine("");
      }

      Console.WriteLine("");
      Console.WriteLine("Complete!");
      Console.WriteLine("");
      Console.WriteLine("Press any key to exit...");
      Console.ReadKey();

    }

  }
}
