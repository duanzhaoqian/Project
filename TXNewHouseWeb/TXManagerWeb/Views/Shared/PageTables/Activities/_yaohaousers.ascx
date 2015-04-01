<%@ Control Language="C#" Inherits="WebViewUserControl<List<TXModel.AdminPVM.PVM_NH_YaoHaoUsers>>" %>
<table class="DataTableA" border="0" cellpadding="0" cellspacing="0" width="100%">
        <thead>
            <tr>
                <th width="10%">
                    序号
                </th>
                <th width="10%">
                    报名人
                </th>
                <th width="10%">
                    性别
                </th>
                <th width="10%">
                    身份证号
                </th>
                 <th width="10%">
                     户口复印件
                </th> 
                <th width="10%">
                    手机号
                </th>
                <th width="10%">
                    摇号号码
                </th> 
                <th width="12%">
                    报名时间
                </th>
            </tr>
        </thead>
        <tbody class="test">
        <%if (Model != null && Model.Count > 0)
            { %>
                <% foreach (var item in Model)
                {%>
                <tr>
                    <td>
                    <%=item.RowID %>
                    </td>
                    <td>
                    <%=item.Name %>
                    </td>
                    <td>
                    <%=item.Sex==1?"男":item.Sex==2?"女":"未知" %>
                    </td>
                    <td>
                    <%=item.IDCard %>
                    </td>
                    <td>
                    <%=item.InnerCode %>
                    </td>
                    <td>
                        <%=item.Mobile %>
                    </td>
                    <td>
                    <%=item.Num %>
                    </td>
                    <td>
                    <%=item.CreateTime %>
                    </td>
                </tr>
                <% }%>
            <%}else
            {%>
            <tr>
                <td colspan="9">
                    暂无数据！
                </td>
            </tr>
            <%}%>
        </tbody>
    </table>

