<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewCourse.aspx.cs" Inherits="WebApplication6.Course" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Course Management</title>
</head>
<body>
   <form id="form1" runat="server">
        <div>
            <h2>Add Course</h2>
            <asp:Label ID="lblCourseName" runat="server" Text="Course Name:"></asp:Label>
            <asp:TextBox ID="txtCourseName" runat="server"></asp:TextBox><br />

            <asp:Label ID="lblCourseDesc" runat="server" Text="Course Description:"></asp:Label>
            <asp:TextBox ID="txtCourseDesc" runat="server" TextMode="MultiLine"></asp:TextBox><br />

            <asp:Label ID="lblCoursePrice" runat="server" Text="Course Price:"></asp:Label>
            <asp:TextBox ID="txtCoursePrice" runat="server"></asp:TextBox><br />

            <asp:Label ID="lblCourseDuration" runat="server" Text="Course Duration:"></asp:Label>
            <asp:TextBox ID="txtCourseDuration" runat="server"></asp:TextBox><br />

            <asp:Label ID="lblCourseCat" runat="server" Text="Course Category:"></asp:Label>
            <asp:TextBox ID="txtCourseCat" runat="server"></asp:TextBox><br />

            <asp:Label ID="lblImageUpload" runat="server" Text="Course Image:"></asp:Label>
            <asp:FileUpload ID="FileUpload1" runat="server" /><br />

            <asp:Button ID="btnSignup" runat="server" Text="Add Course" OnClick="btnSignup_Click" /><br />

            <h2>Update/Delete Course</h2>
            <asp:Label ID="lblCourseId" runat="server" Text="Course ID:"></asp:Label>
            <asp:TextBox ID="IDboxUD" runat="server"></asp:TextBox><br />

            <asp:Button ID="FetchData" runat="server" Text="Fetch Data" OnClick="FetchData_Click" /><br />

            <asp:Button ID="Updatelbl" runat="server" Text="Update Course" OnClick="Updatelbl_Click" />
            <asp:Button ID="Deletelbl" runat="server" Text="Delete Course" OnClick="Deletelbl_Click" /><br />

            <asp:Label ID="Label1" runat="server" Text="Update Status" Visible="False"></asp:Label>
            <asp:Label ID="Label2" runat="server" Text="Delete Status" Visible="False"></asp:Label>
           
            <h2>Course List</h2>
             <asp:Image ID="imgCourse" runat="server"
                    ImageUrl='<%# Eval("courseimage", "data:image/jpeg;base64,{0}") %>' 
                    Width="100px" Height="100px" />
           <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
    <Columns>
        <asp:BoundField DataField="courseid" HeaderText="Course ID" />
        <asp:BoundField DataField="coursetitle" HeaderText="Title" />
        <asp:BoundField DataField="coursedescription" HeaderText="Description" />
        <asp:BoundField DataField="courseprice" HeaderText="Price" />
        <asp:BoundField DataField="courseduration" HeaderText="Duration" />
        <asp:BoundField DataField="coursecategory" HeaderText="Category" />
        <asp:TemplateField HeaderText="Image">
            <ItemTemplate>
                <asp:Image ID="imgCourse" runat="server" ImageUrl='<%# GetImageUrl(Eval("courseimage")) %>' Width="100px" Height="100px" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
        </div>
    </form>
</body>
</html>
