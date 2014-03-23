<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<News>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Model == null ? "Not found" : Model.Title%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <%if (Model == null) {%>

    <h2>Sorry, news not found...</h2>

  <%} else { %>

    <h2><%=Model.Title%></h2>
    <%=Model.MainText.Replace("\r\n", "<br />")%>

  <%}%>
</asp:Content>
