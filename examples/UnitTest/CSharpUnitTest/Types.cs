using System;
using System.Data;
using Nemiro.Data;
using Nemiro.Data.Sql;

#region ...

// This code was generated from SQL Server table <types>
// by means of program DB2Class v.3.0.4.100
// You can change this code.
// But do not generate the code again if you change it manually.
// For more information, look at homepage:
// http://data.nemiro.net

// You can specify any connection string to the SQL Server database.
// Use the constructor overload.
// For example: [Table("types", "LocalSqlServer")] (LocalSqlServer is default value)
// or: [Table("types", "data source=(local);initial catalog=;user id=;password=;")]

// Use <Load>, <Save> and <Delete> methods to work with data.
// Use the <Exists> method, to verify the existence of records in the database.
// <GetChanges> method returns a list of changes (used in conjunction with the <Load> method.).
// To create a table in the database, use the <CreateTable> method.
// Use <ToXml> and <ToJson> method for serialize object to Xml and Json.
// For deserialize object from string, use <LoadXml> and <LoadJson> methods.

#endregion

namespace UnitTest
{

  /// <summary>
  /// The class of table "types".
  /// </summary>
  [Table("types")]
  public class Types : BaseObject
  {

    #region ..properties..

    /// <summary>
    /// Identifier (field: id)
    /// </summary>
    [Column("id", SqlDbType.Int, ColumnAttributeFlags.PrimaryKey | ColumnAttributeFlags.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// (field: type_bigint)
    /// </summary>
    [Column("type_bigint", SqlDbType.BigInt, Default = 0)]
    public long TypeBigint { get; set; }

    /// <summary>
    /// (field: type_binary_50, size: 50 chars)
    /// </summary>
    [Column("type_binary_50", SqlDbType.Binary, Size = 50)]
    public byte[] TypeBinary50 { get; set; }

    /// <summary>
    /// (field: type_bit)
    /// </summary>
    [Column("type_bit", SqlDbType.Bit, Default = false)]
    public bool TypeBit { get; set; }

    /// <summary>
    /// (field: type_char_10, size: 10 chars)
    /// </summary>
    [Column("type_char_10", SqlDbType.Char, Size = 10)]
    public string TypeChar10 { get; set; }

    /// <summary>
    /// (field: type_date)
    /// </summary>
    [Column("type_date", SqlDbType.Date, Default = ColumnDefaultValues.Now)]
    public DateTime TypeDate { get; set; }

    /// <summary>
    /// (field: type_datetime)
    /// </summary>
    [Column("type_datetime", SqlDbType.DateTime, Default = ColumnDefaultValues.Now)]
    public DateTime TypeDatetime { get; set; }

    /// <summary>
    /// (field: type_datetime2)
    /// </summary>
    [Column("type_datetime2", SqlDbType.DateTime2, Default = ColumnDefaultValues.Now)]
    public DateTime TypeDatetime2 { get; set; }

    #endregion
    #region ..constructor/destructor..

    // IMPORTANT DO NOT FORGET TO CALL THE BASE CLASS CONSTRUCTOR
    // public Types : base() {}

    /// <summary>
    /// Creates an empty instance of an object.
    /// </summary>
    public Types() : base() { }

    /// <summary>
    /// Creates an instance of an object based on DataRow.
    /// </summary>
    public Types(DataRow r) : base(r) { }

    /// <summary>
    /// Gets entry from a database with the specified identifier and creates instance of an object.
    /// </summary>
    /// <param name="id">Identifier</param>
    public Types(int id) : base(id) { }

    /// <summary>
    /// Gets entry from a database with the specified identifier and creates instance of an object with caching.
    /// </summary>
    /// <param name="id">Identifier</param>
    /// <param name="cacheDuration">Duration caching (in seconds). Minus one or zero - without caching (default).</param>
    /// <remarks>
    /// Be careful when using caching. Do not use Save and Delete methods, if the object was created with caching.
    /// </remarks>
    public Types(int id, int cacheDuration) : base(id, cacheDuration) { }

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
    public static DataObjectCollection<Types> GetList(int recordsPerPage = 0, int page = 1, string[] includeFields = null, object sort = null, string filter = null)
    {
      return new DataObjectCollection<Types>(BaseObject.GetList(typeof(Types), recordsPerPage, page, includeFields, sort, filter));
    }

    #endregion

  }

}