/*
 * This example demonstrates using SqlClient class from <Nemiro.Data.dll> library.
 * Please make sure that the project is added <Nemiro.Data.dll> reference.
 * For more information, please visit:
 * https://github.com/alekseynemiro/nemiro.data.dll
 * 
 * -----------------------------------------------------------------------------------
 * 
 * Пример демонстрирует работу с классом SqlClient библиотеки <Nemiro.Data.dll>.
 * Пожалуйста, убедитесь в том, что библиотека <Nemiro.Data.dll> подключена к проекту.
 * Дополнительную информацию вы можете найти на сайте:
 * https://github.com/alekseynemiro/nemiro.data.dll
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// Import the namespace from <Nemiro.Data.dll> for convenience.
// Импортируйте пространства имен из библиотеки <Nemiro.Data.dll> для удобства работы с ней.
using Nemiro.Data;
using Nemiro.Data.Sql;
// and System.Data
using System.Data;

namespace CSharpUsingSqlClient
{
  class Program
  {

    /*
     * The connection string to the database, you can find the application parameters: 
     * Project -> Properties, tab <Settings>.
     * 
     * You can set your own connection string when initializing <SqlClient> class:
     * using (SqlClient client = new SqlClient(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\example.mdf;Integrated Security=True;User Instance=True"))
     * {
     *   //...
     * }
     * And also you can specify the names of the parameters of settings, 
     * which contain the connection strings to the database:
     * using (SqlClient client = new SqlClient("CustomConnectionString"))
     * {
     *   //...
     * } 
     * -----------------------------------------------------------------------------------
     * Строку соединения с базой данных вы можете найти в параметрах приложения:
     * Проект -> Свойства, вкладка <Параметры>.
     * 
     * Вы можете явно указать строку соединения при инициализации
     * экземпляра класса <SqlClient>:
     * using (SqlClient client = new SqlClient(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\example.mdf;Integrated Security=True;User Instance=True"))
     * {
     *   //...
     * }
     * 
     * Помимо указания самой строки соединения с базой данных, 
     * можно указать имя параметра настроек приложения, который содержит строку соединения:
     * using (SqlClient client = new SqlClient("CustomConnectionString"))
     * {
     *   //...
     * } 
     */

    static void Main(string[] args)
    {

      /*
       * To interact with the database, use the following methods:
       * GetData          - executes the query and returns a DataSet;
       * GetTable         - executes the query and returns a DataTable;
       * GetRow           - executes the query and returns a DataRow;
       * ExecuteScalar    - executes the query and returns value of first field first row;
       * ExecuteNonQuery  - executes the query.
       * -----------------------------------------------------------------------------------
       * Для работы с базой данных существуют следующие основные методы:
       * GetData          - выполняет запрос и возвращает DataSet;
       * GetTable         - выполняет запрос и возвращает DataTable;
       * GetRow           - выполняет запрос и возвращает DataRow;
       * ExecuteScalar    - выполняет запрос и возвращает первый столбец 
       *                    первой строки из полученного набора данных;
       * ExecuteNonQuery  - выполняет запрос и возвращает количество 
       *                    задействованных в инструкции строк данных.
       */


      // Code for the console. Do not look it. Look at the code in methods.
      // -----------------------------------------------------------------------------------
      // Это код для реализации работы консоли. В нем нет ничего интересного.
      // Код работы с базой данных находится в методах: 
      // ShowEntries, ShowEntry и AddPhonebookEntry.
      while (true)
      {
        Console.WriteLine("Please select the type of operation you want to perform:");
        Console.WriteLine("1 - Show all phonebook entries.");
        Console.WriteLine("2 - Show specified phonebook entry.");
        Console.WriteLine("3 - Add new entry to phonebook.");
        Console.WriteLine("4 - Exit.");

        string cmd = Console.ReadLine();
        while (!"1234".Any(c => c == cmd.First()))
        {
          Console.WriteLine("Please enter 1, 2, 3 or 4.");
          cmd = Console.ReadLine();
        }

        if (cmd == "1")
        {
          ShowEntries();
        }
        else if (cmd == "2")
        {
          Console.WriteLine("Please enter the identifier of phonebook entry:");
          int id = 0;
          while (!int.TryParse(Console.ReadLine(), out id))
          {
            Console.WriteLine("ERROR: Integer expected!");
            Console.WriteLine("Please enter the identifier of phonebook entry:");
          }
          ShowEntry(id);
        }
        else if (cmd == "3")
        {
          string firstName, lastName, gender, homePhone, mobilePhone, officePhone;
          DateTime birthday;
          Console.WriteLine("Please enter a first name:");
          firstName = Console.ReadLine();
          Console.WriteLine("Please enter a last name:");
          lastName = Console.ReadLine();
          Console.WriteLine("Please enter a gender (male or female):");
          gender = Console.ReadLine();
          Console.WriteLine("Please enter a home phone number:");
          homePhone = Console.ReadLine();
          Console.WriteLine("Please enter a mobile phone number:");
          mobilePhone = Console.ReadLine();
          Console.WriteLine("Please enter a office phone number:");
          officePhone = Console.ReadLine();
          Console.WriteLine("Please enter a birthday:");
          while (!DateTime.TryParse(Console.ReadLine(), out birthday))
          {
            Console.WriteLine("Please enter a valid birthday:");
          }
          int entryId = AddPhonebookEntry(firstName, lastName, gender, birthday, homePhone, mobilePhone, officePhone);
          Console.WriteLine("New entry has been added to database. ID: {0}", entryId);
        }
        else
        {
          return;
        }
        Console.WriteLine();
      }
    }

    /// <summary>
    /// <para lang="EN">Method adds an entry in the phone book.</para>
    /// <para lang="RU">Метод добавляет запись в телефонную книгу.</para>
    /// </summary>
    /// <returns>
    /// <para lang="EN">Returns the identifier of the added entry.</para>
    /// <para lang="RU">Возвращает идентификатор добавленной записи.</para>
    /// </returns>
    private static int AddPhonebookEntry(string firstName, string lastName, string gender, DateTime birthday, string homePhone, string mobilePhone, string officePhone)
    {
      using (SqlClient client = new SqlClient())
      {
        // build a database query
        // формируем запрос к базе данных
        client.CommandText = 
        @"INSERT INTO phone_book (first_name, last_name, home_phone, mobile_phone, office_phone, birthday, gender, date_created)
          VALUES (@first_name, @last_name, @home_phone, @mobile_phone, @office_phone, @birthday, @gender, @date_created);
          SELECT SCOPE_IDENTITY();";
        // add query parameters
        // добавляем параметры запроса
        client.Parameters.Add("@first_name", SqlDbType.NVarChar, 50).Value = firstName;
        client.Parameters.Add("@last_name", SqlDbType.NVarChar, 50).Value = lastName;
        client.Parameters.Add("@home_phone", SqlDbType.NVarChar, 50).Value = homePhone;
        client.Parameters.Add("@mobile_phone", SqlDbType.NVarChar, 50).Value = mobilePhone;
        client.Parameters.Add("@office_phone", SqlDbType.NVarChar, 50).Value = officePhone;
        client.Parameters.Add("@birthday", SqlDbType.DateTime).Value = birthday;
        client.Parameters.Add("@gender", SqlDbType.Char, 1).Value = gender;
        client.Parameters.Add("@date_created", SqlDbType.DateTime).Value = DateTime.Now;
        // execute the query and return the result
        // выполняем запрос и возвращаем результат
        return Convert.ToInt32(client.ExecuteScalar());
      }
    }

    /// <summary>
    /// <para lang="EN">The method outputs to the console all phonebook.</para>
    /// <para lang="RU">Метод выводит в консоль все записи телефонной книги.</para>
    /// </summary>
    private static void ShowEntries()
    {
      using (SqlClient client = new SqlClient())
      {
        // build a database query
        // формируем запрос к базе данных
        client.CommandText = "SELECT * FROM phone_book ORDER BY id ASC";
        // execute the query and displays the result
        // выполняем запрос и выводим результат
        Console.WriteLine
        (
          "{0,-4}{1,-15}{2,-15}{3,-3}{4,-12}{5,-15}{6,-15}",
          "ID", "FName", "tLName", "", "Birthday",
          "Home", "Mobile"
        );
        DataTable table = client.GetTable();
        foreach (DataRow row in table.Rows)
        {
          Console.WriteLine
          (
            "{0,-4}{1,-15}{2,-15}{3,-3}{4,-12}{5,-15}{6,-15}",
            row["id"], row["first_name"], row["last_name"], row["gender"], 
            Convert.ToDateTime(row["birthday"]).ToShortDateString(),
            row["home_phone"], row["mobile_phone"]
          );
        }
      }
    }

    /// <summary>
    /// <para lang="EN">The method outputs to the console phonebook entry by id.</para>
    /// <para lang="RU">Метод выводит в консоль указанную запись.</para>
    /// </summary>
    private static void ShowEntry(int id)
    {
      using (SqlClient client = new SqlClient())
      {
        // build a database query
        // формируем запрос к базе данных
        client.CommandText = "SELECT * FROM phone_book WHERE id = @id";
        client.Parameters.Add("@id", SqlDbType.Int).Value = id;

        // execute the query and displays the result
        // выполняем запрос и выводим результат
        DataRow row = client.GetRow();
        if (row == null)
        {
          Console.WriteLine("Data not found...");
          return;
        }

        Console.WriteLine("ID:           {0}", row["id"]);
        Console.WriteLine("First Name:   {0}", row["first_name"]);
        Console.WriteLine("Last Name:    {0}", row["last_name"]);
        Console.WriteLine("Gender:       {0}", row["gender"]);
        Console.WriteLine("Birthday:     {0}", Convert.ToDateTime(row["birthday"]).ToShortDateString());
        Console.WriteLine("Home Phone:   {0}", row["home_phone"]);
        Console.WriteLine("Mobile Phone: {0}", row["mobile_phone"]);
        Console.WriteLine("Office Phone: {0}", row["office_phone"]);
      }
    }

  }

}
