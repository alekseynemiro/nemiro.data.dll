<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<News>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	News editor
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <h2>News editor</h2>
  <%if (Model == null) {%>

    <h2>Sorry, news not found...</h2>

  <%} else { %>

    <%using (Html.BeginForm()) {%>
      <%=Html.ValidationSummary(true)%>
      <table>
        <tr>
          <td style="width:150px;">Title:</td>
          <td><%=Html.TextBoxFor(m => m.Title, new { style = "width: 350px;" })%></td>
        </tr>
        <tr>
          <td>Description:</td>
          <td><%=Html.TextAreaFor(m => m.Description, new { rows = 5, style = "width: 350px;" })%></td>
        </tr>
        <tr>
          <td colspan="2">
          News text:<br />
          <%=Html.TextAreaFor(m => m.MainText, new { rows = 15, style = "width: 500px;" })%>
          </td>
        </tr>
      </table>
      <input type="submit" value="Save" />
      <%=Html.HiddenFor(m => m.IdNews)%>
    <%}%>

  <%}%>
</asp:Content>
