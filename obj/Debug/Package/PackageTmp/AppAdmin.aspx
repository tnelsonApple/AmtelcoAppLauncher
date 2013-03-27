<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AppAdmin.aspx.cs" Inherits="AmtelcoAppLauncher.AppAdmin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Application/Site Settings</h2>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            

    <asp:Accordion ID="Accordion1" runat="server" SelectedIndex="-1" 
            HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
            ContentCssClass="accordionContent" FadeTransitions="false" FramesPerSecond="40" 
            TransitionDuration="1" AutoSize="None" RequireOpenedPane="false" 
        SuppressHeaderPostbacks="true" Height="100%" Width="100%" >

        <Panes>
            <asp:AccordionPane ID="apExes" runat="server">
                <Header><a href="" class="accordionLink">Executable File Locations</a></Header>
                <Content>
                    
                    <table>
                        <tr>
                            <td>Telephone Agent:</td>
                            <td><asp:TextBox ID="txtDisplayTelephoneAgentExe" Width="400" runat="server" Enabled="false"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Supervisor:</td>
                            <td><asp:TextBox ID="txtDisplaySupervisorExe" Width="400" runat="server" Enabled="false"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>On-Call Supervisor:</td>
                            <td><asp:TextBox ID="txtDisplayOCSupervisorExe" Width="400" runat="server" Enabled="false"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>IS Supervisor:</td>
                            <td><asp:TextBox ID="txtDisplayISSupervisorExe" Width="400" runat="server" Enabled="false"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="2"><asp:Button ID="btnEditExes" runat="server" Text="Edit" /></td>
                        </tr>
                    </table>

                    <asp:ModalPopupExtender ID="mpeEditExes" 
                            runat="server"
                            TargetControlID="btnEditExes"
                            PopupControlID="pnlEditExes"
                            BackgroundCssClass="modalBackground"
                            CancelControlID="btnCancelExes">
                    </asp:ModalPopupExtender>
                    <asp:Panel runat="Server" ID="pnlEditExes" BorderStyle="Solid" CssClass="modalPopup">
                        <table>
                            <tr>
                                <td>Telephone Agent:</td>
                                <td><asp:TextBox ID="txtTelephoneAgentExe" Width="400" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Supervisor:</td>
                                <td><asp:TextBox ID="txtSupervisorExe" Width="400" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>On-Call Supervisor:</td>
                                <td><asp:TextBox ID="txtOCSupervisorExe" Width="400" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>IS Supervisor:</td>
                                <td><asp:TextBox ID="txtISSupervisorExe" Width="400" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnSaveExes" runat="server" Text="Save" onclick="btnSaveExes_Click" />&nbsp;
                                    <asp:Button ID="btnCancelExes" runat="server" Text="Cancel" />
                                </td>
                            </tr>
                        </table>
    
                    </asp:Panel>

                        
                   
                </Content>
            </asp:AccordionPane>

            <asp:AccordionPane ID="apOfficeAudioGroups" runat="server">
                <Header><a href="" class="accordionLink">Office / Audio Groups</a></Header>
                <Content>
                    
                    <table>
                        <tr>
                            <td valign="top">
                                Groups:<br />
                                <asp:ListBox ID="lbOfficeAudioGroups" runat="server" DataSourceID="SqlDataSource1" AutoPostBack="true" 
                                    DataTextField="name" DataValueField="id" Height="134px" Width="179px" onselectedindexchanged="lbOfficeAudioGroups_SelectedIndexChanged">
                                </asp:ListBox>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
                                    SelectCommand="SELECT [id], [name] FROM [tblOfficeAudioGroups] WHERE ([siteCode] = @siteCode)">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue='<%$ AppSettings:siteCode %>' Name="siteCode" Type="String" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                            <td valign="top">
                                <table>
                                            
                                    <tr><td colspan="2">&nbsp;</td></tr>
                                    <tr>
                                        <td>Name:</td>
                                        <td><asp:TextBox ID="txtDisplayGroupName" runat="server" Enabled="false"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Default Audio Instructions:</td>
                                        <td><asp:TextBox ID="txtDisplayDefaultAudioInfo" Rows="8" Columns="55" runat="server" TextMode="MultiLine" Enabled="false"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Stations are Static:</td>
                                        <td><asp:CheckBox ID="cbDisplayStaticStations" runat="server" Enabled="false" /></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Button ID="btnAddOfficeAudioGroup" OnClick="btnAddOfficeAudioGroup_Click" runat="server" Text="Add" />&nbsp;<asp:Button runat="server" ID="btnAddEditGroupsPlaceholder"  style="display:none" />
                                            <asp:Button ID="btnEditOfficeAudioGroup" Enabled="false" OnClick="btnEditOfficeAudioGroup_Click" runat="server" Text="Edit" />&nbsp;
                                            <asp:Button ID="btnDeleteOfficeAudioGroup" Enabled="false" OnClick="btnDeleteOfficeAudioGroup_Click" runat="server" Text="Delete" />
                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" 
                                                TargetControlID="btnDeleteOfficeAudioGroup" ConfirmText="All stations currently assigned to this group will be set to Unassigned.  Are you sure you want to delete this Group?" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <asp:ModalPopupExtender ID="mpeEditOfficeAudioGroups" 
                            runat="server"
                            TargetControlID="btnAddEditGroupsPlaceholder"
                            PopupControlID="pnlEditOfficeAudioGroups"
                            BackgroundCssClass="modalBackground"
                            CancelControlID="btnAddEditGroupsCancel">
                    </asp:ModalPopupExtender>
                    <asp:Panel runat="Server" ID="pnlEditOfficeAudioGroups" BorderStyle="Solid" CssClass="modalPopup">
                        <table>
                        <asp:HiddenField ID="hfAddorEditGroup" runat="server" />
                        <asp:HiddenField ID="hfCurrentGroup" runat="server" />
                            <tr>
                                <td>Name:</td>
                                <td><asp:TextBox ID="txtGroupName" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Default Audio Instructions:</td>
                                <td><asp:TextBox ID="txtDefaultAudioInfo"  Rows="8" Columns="55" runat="server" TextMode="MultiLine"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Stations are Static:</td>
                                <td><asp:CheckBox ID="cbStaticStations" runat="server" /></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnAddEditGroupsSave" OnClick="btnAddEditGroupsSave_Click" runat="server" Text="Save" />&nbsp;
                                    <asp:Button ID="btnAddEditGroupsCancel" runat="server" Text="Cancel"/>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                        
                </Content>
            </asp:AccordionPane>

            <asp:AccordionPane ID="apInfinityStations" runat="server">
                <Header><a href="" class="accordionLink">Infinity Stations / Ports</a></Header>
                <Content>
                    <asp:Panel ID="Panel1" runat="server">
                        &nbsp;<asp:Label ID="Label6" ForeColor="Black" Font-Bold="true" runat="server" Text="Legend:"></asp:Label>&nbsp;&nbsp;
                        <asp:Label ID="Label3" ForeColor="White" BackColor="Gray" runat="server" Text="Unassigned"></asp:Label>&nbsp;
                        <asp:Label ID="Label1" ForeColor="Black" BackColor="Yellow" runat="server" Text="Telephone Agent"></asp:Label>&nbsp;
                        <asp:Label ID="Label2" ForeColor="White" BackColor="Red" runat="server" Text="Infinity Supervisor"></asp:Label>&nbsp;
                        <asp:Label ID="Label4" ForeColor="White" BackColor="Blue" runat="server" Text="On-Call Supervisor"></asp:Label><br />
                        <asp:Button ID="btnAssignMultipleStations" runat="server" Text="Assign Multiple Stations / Ports" OnClick="btnAssignMultipleStations_Click" /><asp:Button runat="server" ID="btnAssignMultiplePlaceholder"  style="display:none" />
                    </asp:Panel>
                    <br /><asp:Button runat="server" ID="btnEditInfinityStationPlaceholder"  style="display:none" />
                    
                    <asp:PlaceHolder ID="phInfinityStations" runat="server">
                    </asp:PlaceHolder>
                    <br />
                    
                    <br />
                    <asp:ModalPopupExtender ID="mpeEditInfinityStation" 
                            runat="server"
                            TargetControlID="btnEditInfinityStationPlaceholder"
                            PopupControlID="pnlEditInfinityStation"
                            BackgroundCssClass="modalBackground"
                            CancelControlID="btnCancelInfinityStation">
                    </asp:ModalPopupExtender>
                    <asp:Panel ID="pnlEditInfinityStation" runat="server" BorderStyle="Solid" CssClass="modalPopup">
                        <table>
                            <asp:HiddenField ID="hfStationID" runat="server" />
                            <tr>
                                <td>Station (Port) Number:</td>
                                <td><asp:TextBox ID="txtStationNumber" runat="server" Enabled="false"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Station Type:</td>
                                <td>
                                    <asp:DropDownList ID="ddlStationType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStationType_SelectedIndexChanged">
                                        <asp:ListItem Value="Unassigned">Unassigned</asp:ListItem>
                                        <asp:ListItem Value="TelephoneAgent">Telephone Agent</asp:ListItem>
                                        <asp:ListItem Value="Supervisor">Infinity Supervisor</asp:ListItem>
                                        <asp:ListItem Value="OCSupervisor">On-Call Supervisor</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <div runat="server" id="divTelephoneAgentDetails" visible="false">
                            <tr>
                                <td>Office / Audio Group:</td>
                                <td><asp:DropDownList ID="ddlOfficeAudioGroups" runat="server" AppendDataBoundItems="true" AutoPostBack="true" 
                                    DataSourceID="SqlDataSource2" DataTextField="name" DataValueField="id" OnSelectedIndexChanged="ddlOfficeAudioGroups_SelectedIndexChanged">
                                    <asp:ListItem Value="">-- Choose Office / Audio Group --</asp:ListItem>
                                </asp:DropDownList>
    
                                <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
                                    SelectCommand="SELECT [id], [name] FROM [tblOfficeAudioGroups]">
                                </asp:SqlDataSource></td>
                            </tr>
                            <div runat="server" id="divDisplayWorkstationName" visible="false">
                            <tr>
                                <td>Workstation Name:</td>
                                <td><asp:TextBox ID="txtInfWorkstationName" runat="server"></asp:TextBox></td>
                            </tr>
                            </div>
                            <tr>
                                <td>Audio Information:</td>
                                <td><asp:TextBox ID="txtInfStationAudioInfo" Rows="8" Columns="55" TextMode="MultiLine" runat="server"></asp:TextBox></td>
                            </tr>
                            </div>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnSaveInfinityStation" OnClick="btnSaveInfinityStation_Click" runat="server" Text="Save" />&nbsp;
                                    <asp:Button ID="btnCancelInfinityStation" runat="server" Text="Cancel"/>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    
                    <asp:ModalPopupExtender ID="mpeAssignMultipleStations" 
                            runat="server"
                            TargetControlID="btnAssignMultiplePlaceholder"
                            PopupControlID="pnlAssignMultipleStations"
                            BackgroundCssClass="modalBackground"
                            CancelControlID="btnCancelMultiple">
                    </asp:ModalPopupExtender>
                    <asp:Panel ID="pnlAssignMultipleStations" runat="server" BorderStyle="Solid" CssClass="modalPopup">
                        <table>
                            <tr>
                                <td>Station Type:</td>
                                <td>
                                    <asp:DropDownList ID="ddlMultipleStationType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMultipleStationType_SelectedIndexChanged">
                                        <asp:ListItem Value="">-- Choose Station Type --</asp:ListItem>
                                        <asp:ListItem Value="TelephoneAgent">Telephone Agent</asp:ListItem>
                                        <asp:ListItem Value="Supervisor">Infinity Supervisor</asp:ListItem>
                                        <asp:ListItem Value="OCSupervisor">On-Call Supervisor</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <div runat="server" ID="divMultipleTelephoneAgent" visible="false">
                            <tr>
                                <td>Office / Audio Group:</td>
                                <td><asp:DropDownList ID="ddlMultipleOfficeAudioGroup" runat="server" AppendDataBoundItems="true" AutoPostBack="true" 
                                    DataSourceID="SqlDataSource3" DataTextField="name" DataValueField="id" OnSelectedIndexChanged="ddlMultipleOfficeAudioGroup_SelectedIndexChanged">
                                    <asp:ListItem Value="">-- Choose Office / Audio Group --</asp:ListItem>
                                </asp:DropDownList>
    
                                <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
                                    SelectCommand="SELECT [id], [name] FROM [tblOfficeAudioGroups]">
                                </asp:SqlDataSource></td>
                            </tr>
                            </div>
                            </table>
                                    <asp:PlaceHolder ID="phMultipleStations" runat="server" Visible="false">
                                    <asp:Panel ID="Panel2" runat="server">
                                        <br />
                                        &nbsp;<asp:Label ID="Label7" ForeColor="Black" Font-Bold="true" runat="server" Text="Legend:"></asp:Label>&nbsp;&nbsp;
                                        <asp:Label ID="Label8" ForeColor="White" BackColor="Gray" runat="server" Text="Unassigned"></asp:Label>&nbsp;
                                        <asp:Label ID="Label9" ForeColor="Black" BackColor="Yellow" runat="server" Text="Telephone Agent"></asp:Label>&nbsp;
                                        <asp:Label ID="Label10" ForeColor="White" BackColor="Red" runat="server" Text="Infinity Supervisor"></asp:Label>&nbsp;
                                        <asp:Label ID="Label11" ForeColor="White" BackColor="Blue" runat="server" Text="On-Call Supervisor"></asp:Label>
                                        <br />
                                    </asp:Panel>
                                        <table>
                                            <tr>
                                                <td><asp:CheckBox ID="CheckBox1" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox2" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox3" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox4" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox5" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox6" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox7" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox8" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox9" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox10" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox11" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox12" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox13" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox14" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox15" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox16" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox17" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox18" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox19" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox20" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td><asp:CheckBox ID="CheckBox21" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox22" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox23" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox24" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox25" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox26" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox27" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox28" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox29" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox30" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox31" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox32" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox33" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox34" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox35" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox36" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox37" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox38" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox39" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox40" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td><asp:CheckBox ID="CheckBox41" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox42" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox43" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox44" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox45" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox46" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox47" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox48" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox49" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox50" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox51" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox52" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox53" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox54" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox55" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox56" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox57" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox58" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox59" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox60" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td><asp:CheckBox ID="CheckBox61" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox62" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox63" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox64" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox65" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox66" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox67" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox68" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox69" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox70" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox71" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox72" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox73" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox74" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox75" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox76" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox77" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox78" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox79" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox80" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td><asp:CheckBox ID="CheckBox81" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox82" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox83" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox84" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox85" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox86" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox87" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox88" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox89" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox90" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox91" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox92" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox93" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox94" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox95" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox96" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox97" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox98" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox99" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox100" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td><asp:CheckBox ID="CheckBox101" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox102" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox103" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox104" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox105" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox106" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox107" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox108" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox109" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox110" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox111" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox112" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox113" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox114" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox115" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox116" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox117" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox118" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox119" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox120" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td><asp:CheckBox ID="CheckBox121" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox122" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox123" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox124" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox125" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox126" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox127" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox128" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox129" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox130" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox131" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox132" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox133" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox134" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox135" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox136" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox137" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox138" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox139" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox140" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td><asp:CheckBox ID="CheckBox141" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox142" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox143" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox144" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox145" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox146" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox147" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox148" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox149" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox150" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox151" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox152" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox153" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox154" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox155" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox156" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox157" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox158" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox159" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox160" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td><asp:CheckBox ID="CheckBox161" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox162" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox163" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox164" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox165" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox166" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox167" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox168" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox169" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox170" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox171" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox172" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox173" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox174" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox175" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox176" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox177" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox178" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox179" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox180" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td><asp:CheckBox ID="CheckBox181" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox182" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox183" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox184" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox185" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox186" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox187" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox188" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox189" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox190" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox191" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox192" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox193" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox194" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox195" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox196" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox197" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox198" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox199" runat="server" /></td>
                                                <td><asp:CheckBox ID="CheckBox200" runat="server" /></td>
                                            </tr>
                                        </table>
                                    </asp:PlaceHolder>
                                
                                    <asp:Button ID="btnSaveMultiple" runat="server" Text="Save" OnClick="btnSaveMultiple_Click" />&nbsp;
                                    <asp:Button ID="btnCancelMultiple" runat="server" Text="Cancel"/>
                                
                    </asp:Panel>
                </Content>
            </asp:AccordionPane>

            <asp:AccordionPane ID="apISProfiles" runat="server">
                <Header><a href="" class="accordionLink">IS Supervisor Profiles</a></Header>
                <Content>
                    <table>
                        <tr>
                            <td valign="top">
                                Profiles:<br />
                                <asp:ListBox ID="lbISStations" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlISStations_SelectedIndexChanged" 
                                    DataSourceID="SqlDataSource4" DataTextField="profileName" DataValueField="id" 
                                    Rows="10" Width="132px">
                                </asp:ListBox>
                                <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
                                    SelectCommand="SELECT [id], [profileName] + ' (' + CONVERT(varchar(10),[stationNum]) + ')' as 'profileName' FROM [tblISStations] ORDER BY [stationNum]">
                                </asp:SqlDataSource>
                            </td>
                            <td valign="top">
                                <br />
                                <asp:Button ID="btnAddISStation" runat="server" Text="Add" OnClick="btnAddISStation_Click" />&nbsp;<asp:Button runat="server" ID="btnAddEditISStationPlaceholder"  style="display:none" />
                                <asp:Button ID="btnEditISStation" Enabled="false" runat="server" Text="Edit" OnClick="btnEditISStation_Click" />&nbsp;
                                <asp:Button ID="btnDeleteISStation" Enabled="false" runat="server" Text="Delete" OnClick="btnDeleteISStation_Click" />
                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" 
                                    TargetControlID="btnDeleteISStation" ConfirmText="Are you sure you want to delete this IS Profile?" />
                            </td>
                        </tr>
                    </table>
                    <asp:ModalPopupExtender ID="mpeEditISStations" 
                            runat="server"
                            TargetControlID="btnAddEditISStationPlaceholder"
                            PopupControlID="pnlEditISStation"
                            BackgroundCssClass="modalBackground"
                            CancelControlID="btnCancelEditISStation">
                    </asp:ModalPopupExtender>
                    <asp:Panel ID="pnlEditISStation" runat="server" BorderStyle="Solid" CssClass="modalPopup">
                        <table>   
                            <asp:HiddenField ID="hfISAddorEdit" runat="server" />
                            <tr><td colspan="2">&nbsp;</td></tr>
                            <tr>
                                <td>Profile Name:</td>
                                <td><asp:TextBox ID="txtISProfileName" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Station Number:</td>
                                <td><asp:TextBox ID="txtISStationNum" runat="server" Columns="5" MaxLength="3"></asp:TextBox>
                                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtISStationNum" ValidationExpression="^\d+$" ID="RegularExpressionValidator1" runat="server" ErrorMessage="Station # must be numeric" ForeColor="Red"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnSaveEditISStation" OnClick="btnSaveEditISStation_Click" runat="server" Text="Save" />&nbsp;
                                    <asp:Button ID="btnCancelEditISStation" runat="server" Text="Cancel"/>
                                </td>
                            </tr>
                        </table>
                        
                     </asp:Panel>
                </Content>
            </asp:AccordionPane>
        </Panes>
    </asp:Accordion>

        </ContentTemplate>
    </asp:UpdatePanel>
    
    <br />
    
    
    
    
    
    
    
    <br />

</asp:Content>
