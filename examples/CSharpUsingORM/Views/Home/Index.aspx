<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DataObjectCollection<News>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Object-Relational Mapping
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <h2>News list</h2>
  <p>This is an example of a list of news.</p>
  <p>News stored in the database in table &lt;news&gt;. Each news is an object that is automatically created based on the data retrieved from the database.</p>
  <p><%=Html.ActionLink("Add News", "EditNews")%></p>
  <hr />

  <%if (Model == null) {%>

    Sorry, news not found...

  <%} else {%>

    <%foreach(var n in Model){%>
      <div style="margin-bottom:12px;">
        <h3><%=Html.ActionLink(n.Title, "ShowNews", new { id = n.IdNews })%></h3>
        <%=n.Description%>
        <div style="margin-top:8px;font-size:x-small">
          <%=Html.ActionLink("Details", "ShowNews", new { id = n.IdNews })%>
          &middot;
          <%=Html.ActionLink("Edit", "EditNews", new { id = n.IdNews })%>
          &middot;
          <%=Html.ActionLink("Delete", "DeleteNews", new { id = n.IdNews })%>
        </div>
      </div>
    <%}%>

    <%--page list--%>
    <hr />
    <div>
    <%for (int i = 1; i <= Model.TotalPages; i++)
      {
        Response.Write(Html.ActionLink(i.ToString(), "Index", new { page = i }));
        if (i != Model.TotalPages) { Response.Write("&middot;"); }
      }
    %>
    </div>

  <%} %>

</asp:Content>
