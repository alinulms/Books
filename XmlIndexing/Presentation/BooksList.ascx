<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BooksList.ascx.cs" Inherits="Books.Presentation.BooksList" %>
<asp:GridView runat="server" DataSource="<%# AllBooks%>"></asp:GridView>