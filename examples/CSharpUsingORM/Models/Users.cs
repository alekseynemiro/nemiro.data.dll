using System;
using System.Data;
using Nemiro.Data;
using Nemiro.Data.Sql;

#region ...

// This code was generated from SQL Server table <users>
// by means of program DB2Class v.3.0.4.82
// You can change this code.
// But do not generate the code again if you change it manually.
// For more information, look at homepage:
// http://data.nemiro.net

// You can specify any connection string to the SQL Server database.
// Use the constructor overload.
// For example: [Table("users", "LocalSqlServer")] (LocalSqlServer is default value)
// or: [Table("users", "data source=(local);initial catalog=;user id=;password=;")]

// Use <Load>, <Save> and <Delete> methods to work with data.
// Use the <Exists> method, to verify the existence of records in the database.
// <GetChanges> method returns a list of changes (used in conjunction with the <Load> method.).
// To create a table in the database, use the <CreateTable> method.
// Use <ToXml> and <ToJson> method for serialize object to Xml and Json.
// For deserialize object from string, use <LoadXml> and <LoadJson> methods.

#endregion

namespace CSharpUsingORM.Models
{

  /// <summary>
  /// The class of table "users".
  /// </summary>
  /// <remarks>
  /// Aleksey Nemiro, 3/23/2014
  /// mailto:aleksey@nemiro.ru
  /// http://aleksey.nemiro.ru
  /// </remarks>
  [Table("users")]
  public class Users : BaseObject
  {

    #region ..properties..

    /// <summary>
    /// Identifier (field: id_users)
    /// </summary>
    [Column("id_users", SqlDbType.UniqueIdentifier, ColumnAttributeFlags.PrimaryKey)]
    public System.Guid IdUsers { get; set; }

    /// <summary>
    /// (field: display_name, size: 50 chars)
    /// </summary>
    [Column("display_name", SqlDbType.NVarChar, Size = 50, Default = "")]
    public string DisplayName { get; set; }

    /// <summary>
    /// First name (field: first_name, size: 50 chars)
    /// </summary>
    [Column("first_name", SqlDbType.NVarChar, Size = 50, Default = "")]
    public string FirstName { get; set; }

    /// <summary>
    /// Last name (field: last_name, size: 50 chars)
    /// </summary>
    [Column("last_name", SqlDbType.NVarChar, Size = 50, Default = "")]
    public string LastName { get; set; }

    /// <summary>
    /// Date of birth (field: birthday)
    /// </summary>
    [Column("birthday", SqlDbType.Date, Default = ColumnDefaultValues.Now)]
    public DateTime Birthday { get; set; }

    /// <summary>
    /// (field: points)
    /// </summary>
    [Column("points", SqlDbType.Int, Default = 0)]
    public int Points { get; set; }

    /// <summary>
    /// Status of deleting (field: is_deleted)
    /// </summary>
    [Column("is_deleted", SqlDbType.Bit, Default = false)]
    public bool IsDeleted { get; set; }

    /// <summary>
    /// (field: date_updated)
    /// </summary>
    [Column("date_updated", SqlDbType.DateTime, Default = ColumnDefaultValues.Now)]
    public DateTime DateUpdated { get; set; }

    /// <summary>
    /// Date created (field: date_created)
    /// </summary>
    [Column("date_created", SqlDbType.DateTime, Default = ColumnDefaultValues.Now)]
    public DateTime DateCreated { get; set; }

    /// <summary>
    /// E-Mail (field: email, size: 50 chars)
    /// </summary>
    [Column("email", SqlDbType.NVarChar, Size = 50, Default = "")]
    public string Email { get; set; }

    /// <summary>
    /// (field: homepage, size: 200 chars)
    /// </summary>
    [Column("homepage", SqlDbType.NVarChar, Size = 200, Default = "")]
    public string Homepage { get; set; }

    #endregion
    #region ..constructor/destructor..

    // IMPORTANT DO NOT FORGET TO CALL THE BASE CLASS CONSTRUCTOR
    // public Users : base() {}

    /// <summary>
    /// Creates an empty instance of an object.
    /// </summary>
    public Users() : base() { }

    /// <summary>
    /// Creates an instance of an object based on DataRow.
    /// </summary>
    public Users(DataRow r) : base(r) { }

    /// <summary>
    /// Gets entry from a database with the specified identifier and creates instance of an object.
    /// </summary>
    /// <param name="id_users">Identifier</param>
    public Users(System.Guid id_users) : base(id_users) { }

    /// <summary>
    /// Gets entry from a database with the specified identifier and creates instance of an object.
    /// </summary>
    /// <param name="id_users">Identifier</param>
    public Users(string id_users) : base()
    {
      if (String.IsNullOrEmpty(id_users)) return;
      this.IdUsers = new Guid(id_users);
      base.Load();
    }

    /// <summary>
    /// Gets entry from a database with the specified identifier and creates instance of an object with caching.
    /// </summary>
    /// <param name="id_users">Identifier</param>
    /// <param name="cacheDuration">Duration caching (in seconds). Minus one or zero - without caching (default).</param>
    /// <remarks>
    /// Be careful when using caching. Do not use Save and Delete methods, if the object was created with caching.
    /// </remarks>
    public Users(System.Guid id_users, int cacheDuration) : base(id_users, cacheDuration) { }

    #endregion
    #region ..methods..

    // Use this region to functions and methods.
    // You can override <Load>, <Save> and <Delete> methods.
    // public new void [Load|Save|Delete]()

    // You can use an overload of the <Load> method to load data from DataRow:
    // base.Load(DataRow);

    #endregion
    #region ..static methods..

    // Use this region to static functions and methods.

    /// <summary>
    /// Returns entity collection of database entries.
    /// </summary>
    /// <param name="page">Page, starting at 1.</param>
    /// <param name="recordsPerPage">Entries on one page. Zero - all entries.</param>
    /// <param name="includeFields">List of fields you want to include in the query. By default, all fields.</param>
    /// <param name="sort">Sort order of the entries. Anonymous type: property name - is field name; property value - is sort order (<c>ASC</c> (default) or <c>DESC</c>).</param>
    /// <param name="filter">When specifying a filter is added to the query design <c>WHERE</c> indicating the contents <paramref name="filter"/> as is.</param>
    /// <remarks>
    /// <example>
    /// <code>
    /// var result = Test.GetList(10, 1, new string[] { ""id"", ""name"", ""date_created"" }, new { id = OrderBy.DESC }, ""name LIKE '%test%'"");
    /// </code>
    /// </example>
    /// Use <c>NextPage</c> and <c>PreviousPage</c> methods for changes page index.
    /// Use <c>TotalRecords</c> property to get the number of records.
    /// Use <c>CurrentPage</c> property to get current page index.
    /// </remarks>
    public static DataObjectCollection<Users> GetList(int recordsPerPage = 0, int page = 1, string[] includeFields = null, object sort = null, string filter = null)
    {
      return new DataObjectCollection<Users>(BaseObject.GetList(typeof(Users), recordsPerPage, page, includeFields, sort, filter));
    }

    #endregion

  }

}