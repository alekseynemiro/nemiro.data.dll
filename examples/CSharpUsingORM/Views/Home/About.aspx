<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="aboutTitle" ContentPlaceHolderID="TitleContent" runat="server">
   About Nemiro.Data.dll
</asp:Content>

<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2>About Nemiro.Data.dll</h2>
    <p>Nemiro.Data.dll library is designed to work with databases Microsoft SQL Server.</p>
    <p>The library contains helper classes for working with data.</p>
    <p>And also implements an object-oriented data access (Object-Relational Mapping, ORM).</p>
    <p>And also implements caching data.</p>

    <h3>SYSTEM REQUIREMENTS</h3>
    <ul>
      <li>Microsoft Windows or later with .NET Framework 4.0 or 4.5</li>
      <li>Microsoft SQL Server 2005, 2008, 2008R2, 2012 or later</li>
      <li>Microsoft Visual Studio 2010 or later (not necessarily)</li>
    </ul>
    <p>&nbsp;</p>

    <h3><a href="http://data.nemiro.net" target="_blank">Nemiro.Data.dll homepage</a></h3>

    <h3><a href="https://github.com/alekseynemiro/nemiro.data.dll/wiki/License-for-Use-and-Distribution" target="_blank">License for Use and Distribution</a></h3>

</asp:Content>
