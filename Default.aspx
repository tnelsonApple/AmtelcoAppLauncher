<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="AmtelcoAppLauncher._Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
    <br />
    <asp:Accordion ID="Accordion1" runat="server" SelectedIndex="-1" 
            HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
            ContentCssClass="accordionContent" FadeTransitions="false" FramesPerSecond="40" 
            TransitionDuration="1" AutoSize="None" RequireOpenedPane="false" 
        SuppressHeaderPostbacks="true" Height="353px" Width="836px" >

        <Panes>
            <asp:AccordionPane ID="apTelephoneAgent" runat="server" Visible="false">
                <Header><a href="" class="accordionLink">Telephone Agent</a></Header>
                <Content>
                    
                            <table>
                                <tr>
                                    <td>Choose Office / Audio Group</td>
                                    <td>
                                        <asp:DropDownList ID="ddlOfficeAudioGroup" runat="server" AutoPostBack="True" AppendDataBoundItems="true" 
                                            DataSourceID="SqlDataSource1" DataTextField="name" DataValueField="id" 
                                            onselectedindexchanged="ddlOfficeAudioGroup_SelectedIndexChanged">
                                            <asp:ListItem Value="">-- Choose --</asp:ListItem>
                                        </asp:DropDownList>
    
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
                                            SelectCommand="SELECT [name], [id] FROM [tblOfficeAudioGroups]">
                                        </asp:SqlDataSource>
                                    </td>
                                </tr>
                                <div runat="server" id="divDynamicInfStations" visible="false">
                                <tr>
                                    <td colspan="2">
                                        <asp:Button ID="btnGetDynamicInfStation" runat="server" 
                                            Text="Get Available Station #" onclick="btnGetDynamicInfStation_Click" />
                                    </td>
                                </tr>
                                    
                                </div>
                                <div runat="server" id="divStaticInfStations" visible="false">
                                <tr>
                                    <td>Choose your station #:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlChooseStaticStation" runat="server" AutoPostBack="true" AppendDataBoundItems="true" 
                                            DataSourceID="SqlDataSource2" DataTextField="workstationName" 
                                            DataValueField="id" 
                                            onselectedindexchanged="ddlChooseStaticStation_SelectedIndexChanged">
                                            <asp:ListItem Value="">-- Choose --</asp:ListItem>
                                        </asp:DropDownList>
    
                                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
                                            SelectCommand="SELECT [id], [workstationName] FROM [tblInfinityStations] WHERE ([officeAudioID] = @officeAudioID)">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="ddlOfficeAudioGroup" Name="officeAudioID" 
                                                    PropertyName="SelectedValue" Type="Int32" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </td>
                                </tr>
                                </div>
                                <div runat="server" id="divShowInfStationLaunch" visible="false">
                                    <asp:HiddenField ID="hfInfstation" runat="server" />
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Label ID="lblInfStationNum" Font-Bold="true" ForeColor="Red" runat="server" Text="Label"></asp:Label><br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Audio Instructions:</td>
                                        <td><asp:Label ID="lblAudioInfo" Font-Bold="true" ForeColor="Black" runat="server" Text="Label"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="btnLaunchInfinity" runat="server" OnClick="btnLaunchInfinity_Click" 
                                                Text="Launch Infinity Telephone Agent" />
                                        </td>
                                    </tr>
                                </div>
                                <div runat="server" id="divGetNewTAStation" visible="false">
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="btnGetNewTelephoneAgent" runat="server" 
                                                Text="Get a new Station Number" onclick="btnGetNewTelephoneAgent_Click" />
                                        </td>
                                    </tr>
                                </div>
                            </table>

                </Content>
            </asp:AccordionPane>

            <asp:AccordionPane ID="apSupervisor" runat="server" Visible="false">
                <Header><a href="" class="accordionLink">Supervisor</a></Header>
                <Content>
                    <table>
                    <asp:HiddenField ID="hfSupStation" runat="server" />
                        <div runat="server" id="divGetSupStation">
                        <tr>
                            <td>
                                <asp:Button ID="btnGetSupStation" runat="server" Text="Get Available Station #" 
                                    onclick="btnGetSupStation_Click" />
                            </td>
                        </tr>
                        </div>
                        <div runat="server" id="divLaunchSupStation" visible="false">
                        <tr>
                            <td>
                                <asp:Label ID="lblSupStationNum" Font-Bold="true" ForeColor="Red" runat="server" Text="Label"></asp:Label><br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnLaunchSup" runat="server" Text="Launch Infinity Supervisor" 
                                    onclick="btnLaunchSup_Click" />
                            </td>
                        </tr>
                        </div>
                        <div runat="server" id="divGetNewSupStation" visible="false">
                        <tr>
                            <td>
                                <asp:Button ID="btnGetNewSup" runat="server" Text="Get a new Station Number" 
                                    onclick="btnGetNewSup_Click" />
                            </td>
                        </tr>
                        </div>
                    </table>
                </Content>
            </asp:AccordionPane>

            <asp:AccordionPane ID="apOCSupervisor" runat="server" Visible="false">
                <Header><a href="" class="accordionLink">On-Call Supervisor</a></Header>
                <Content>
                    <table>
                    <asp:HiddenField ID="hfOCStation" runat="server" />
                        <div runat="server" id="divGetOCSupStation">
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnGetOCSupStation" runat="server" 
                                    Text="Get Available Station #" onclick="btnGetOCSupStation_Click" />
                            </td>
                        </tr>
                        </div>
                        <div runat="server" id="divLaunchOCSupStation" visible="false">
                        <tr>
                            <td>
                                <asp:Label ID="lblOCSupStationNum" Font-Bold="true" ForeColor="Red" runat="server" Text="Label"></asp:Label><br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnLaunchOCSup" runat="server" Text="Launch On-Call Supervisor" 
                                    onclick="btnLaunchOCSup_Click" />
                            </td>
                        </tr>
                        </div>
                        <div runat="server" id="divGetNewOCSupStation" visible="false">
                        <tr>
                            <td>
                                <asp:Button ID="btnGetNewOCSup" runat="server" Text="Get a new Station Number" 
                                    onclick="btnGetNewOCSup_Click" />
                            </td>
                        </tr>
                        </div>
                    </table>
                </Content>
            </asp:AccordionPane>

            <asp:AccordionPane ID="apISSupervisor" runat="server" Visible="false">
                <Header><a href="" class="accordionLink">IS Supervisor</a></Header>
                <Content>
                    <table>
                    <asp:HiddenField ID="hfISStation" runat="server" />
                        <div runat="server" id="divGetISSupStation">
                        <tr>
                            <td>
                                <asp:Button ID="btnGetISSupStation" runat="server" 
                                    Text="Get Available Profile" onclick="btnGetISSupStation_Click" />
                            </td>
                        </tr>
                        </div>
                        <div runat="server" id="divLaunchISSupStation" visible="false">
                        <tr>
                            <td>
                                <asp:Label ID="lblISSupStationNum" Font-Bold="true" ForeColor="Red" runat="server" Text="Label"></asp:Label><br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnLaunchISSup" runat="server" Text="Launch IS Supervisor" 
                                    onclick="btnLaunchISSup_Click" />
                            </td>
                        </tr>
                        </div>
                        <div runat="server" id="divGetNewISSupStation" visible="false">
                        <tr>
                            <td>
                                <asp:Button ID="btnGetNewISSup" runat="server" Text="Get a new Profile" 
                                    onclick="btnGetNewISSup_Click" />
                            </td>
                        </tr>
                        </div>
                    </table>
                </Content>
            </asp:AccordionPane>
        </Panes>
    </asp:Accordion>
    <asp:Button runat="server" ID="btnGetNewInfStationPlaceholder"  style="display:none" />
    <asp:ModalPopupExtender ID="mpeGetNewInfStation" 
            runat="server"
            TargetControlID="btnGetNewInfStationPlaceholder"
            PopupControlID="pnlGetNewInfStation"
            BackgroundCssClass="modalBackground"
            CancelControlID="btnGetNewInfCancel">
    </asp:ModalPopupExtender>
    <asp:Panel runat="Server" ID="pnlGetNewInfStation" BorderStyle="Solid" CssClass="modalPopup">
        <table>
            <asp:HiddenField ID="hfStationType" runat="server" />
            <asp:HiddenField ID="hfStationNum" runat="server" />
            <tr>
                <td>Document any issues or error messages recieved:</td>
            </tr>
            <tr>
                <td> 
                    <asp:TextBox ID="txtInfErrorMessage" runat="server" TextMode="MultiLine" Rows="6" Columns="45"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnGetNewInfOK" runat="server" Text="OK" 
                        onclick="btnGetNewInfOK_Click" />&nbsp;
                    <asp:Button ID="btnGetNewInfCancel" runat="server" Text="Cancel" />
                </td>
            </tr>
        </table>
        
    </asp:Panel>

    </ContentTemplate>
                    </asp:UpdatePanel>
    
    
    
    
    
    

    
    
    
</asp:Content>
