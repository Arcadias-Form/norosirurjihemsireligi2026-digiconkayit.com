<%@ Page Title="" Language="C#" MasterPageFile="~/en/en.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Norosirurjihemsireligi2026_Web.en.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="en_Head" runat="server">
    <link rel="stylesheet" type="text/css" href="<%=ResolveClientUrl("~/css/bootstrap-datepicker.min.css") %>" />
    <script type="text/javascript" src="<%=ResolveClientUrl("~/js/bootstrap-datepicker.min.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveClientUrl("~/js/bootstrap-datepicker.tr.min.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveClientUrl("~/js/jquery.inputmask.min.js") %>"></script>
    <script type="text/javascript">
        function pageLoad(sender, args) {
            datePickerOption.language = "en";
            datePickerOption.placeholder = "Please click pickup date.";
            setDatePicker();
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="en_Content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UPnlGenel" runat="server" class="row">
        <ContentTemplate>
            <div class="col-md-12 mt-3">
                <fieldset>
                    <legend>Personal Information</legend>
                    <table class="AlseinTable">
                        <tr>
                            <td>*</td>
                            <td>Name & Surname</td>
                            <td>
                                <asp:TextBox ID="txtAdSoyad" runat="server" CssClass="form-control" onchange="toUpper(this);" onkeyup="toUpper(this);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>Passaport No</td>
                            <td>
                                <asp:TextBox ID="txtKimlikNo" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>*</td>
                            <td>Gender</td>
                            <td>
                                <asp:DropDownList ID="ddlCinsiyet" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Select" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                                    <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>*</td>
                            <td>e-Mail</td>
                            <td>
                                <asp:TextBox ID="txtePosta" runat="server" CssClass="form-control" TextMode="EMail"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>*</td>
                            <td>Contact Phone</td>
                            <td>
                                <asp:TextBox ID="txtTelefon" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>Institution</td>
                            <td>
                                <asp:TextBox ID="txtKurum" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </fieldset>

                <fieldset>
                    <legend>Invoice Information</legend>
                    <table class="AlseinTable">
                        <tr>
                            <td>*</td>
                            <td>Invoice Type</td>
                            <td>
                                <asp:DropDownList ID="ddlFaturaTipi" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Select" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Individual" Value="Individual"></asp:ListItem>
                                    <asp:ListItem Text="Institutional" Value="Institutional"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>*</td>
                            <td>Invoice Title / Name & Surname</td>
                            <td>
                                <asp:TextBox ID="txtFaturaUnvan" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>*</td>
                            <td>Invoice Address</td>
                            <td>
                                <asp:TextBox ID="txtFaturaAdres" runat="server" CssClass="form-control" TextMode="MultiLine" Style="height: 80px; resize: none;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>Tax Office</td>
                            <td>
                                <asp:TextBox ID="txtVergiDairesi" runat="server" CssClass="form-control"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>Tax No / ID</td>
                            <td>
                                <asp:TextBox ID="txtVergiNo" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                        </tr>

                    </table>
                </fieldset>

                <fieldset>
                    <legend>Registration, Accommodation & Transfer Informations</legend>
                    <table class="AlseinTable">
                        <tr>
                            <td>*</td>
                            <td>Registration Type</td>
                            <td>
                                <asp:DropDownList ID="ddlKatilimciTipi" runat="server" CssClass="form-control" AutoPostBack="true" DataSourceID="OleDbKatilimciTipiListesi" DataTextField="KatilimciTipi" DataValueField="KatilimciTipiID" onchange="showLoadingIcon();" OnSelectedIndexChanged="ddlKatilimciTipi_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="tr_Konaklama" runat="server" visible="false">
                            <td>*</td>
                            <td>Accommodation</td>
                            <td>
                                <asp:DropDownList ID="ddlOtel" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlOtel_SelectedIndexChanged" AutoPostBack="true" DataSourceID="OleDbOtelListesi" DataTextField="Otel" DataValueField="OtelID" onchange="showLoadingIcon();"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="tr_OdaTipi" runat="server" visible="false">
                            <td>*</td>
                            <td>Room Type</td>
                            <td>
                                <asp:DropDownList ID="ddlKatilimciTipiOdaTipi" runat="server" onchange="showLoadingIcon();" CssClass="form-control" OnSelectedIndexChanged="ddlKatilimciTpiOdaTipi_SelectedIndexChanged" AutoPostBack="true" DataSourceID="OleDbKatilimciTipiOdaTipiListesi" DataTextField="OdaTipi" DataValueField="KatilimciTipiOdaTipiID" onhange="showLoadingIcon();"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="tr_GirisTarihi" runat="server" visible="false">
                            <td>*</td>
                            <td>Giriş Tarihi</td>
                            <td>
                                <asp:TextBox ID="txtGirisTarihi" runat="server" CssClass="form-control x-date" onchange="showLoadingIcon();" OnTextChanged="FiyatHesaplama" AutoPostBack="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="tr_CikisTarihi" runat="server" visible="false">
                            <td>*</td>
                            <td>Çıkış Tarihi</td>
                            <td>
                                <asp:TextBox ID="txtCikisTarihi" runat="server" CssClass="form-control x-date" onchange="showLoadingIcon();" OnTextChanged="FiyatHesaplama" AutoPostBack="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="tr_Refakatci" runat="server" visible="false">
                            <td>*</td>
                            <td>Accommpaying' Person</td>
                            <td>
                                <asp:TextBox ID="txtRefakatci" runat="server" CssClass="form-control" onchange="toUpper(this);" onkeyup="toUpper(this);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>*</td>
                            <td>Transfer Type</td>
                            <td>
                                <asp:DropDownList ID="ddlTransferTipi" runat="server" CssClass="form-control" AutoPostBack="true" DataSourceID="OleDbTransferTipiListesi" DataTextField="TransferTipi" DataValueField="TransferTipiID" onchange="showLoadingIcon();" OnSelectedIndexChanged="FiyatHesaplama"></asp:DropDownList>
                            </td>
                        </tr>
                    </table>

                </fieldset>

                <fieldset id="fld_Kurs" runat="server" visible="true">
                    <legend>Course(s)</legend>
                    <table class="table table-striped">
                        <tbody>
                            <asp:Repeater ID="rptKursListesi" runat="server" ClientIDMode="AutoID" OnItemCommand="rptIcerikListesi_ItemCommand" DataSourceID="OleDbKursListesi">
                                <ItemTemplate>
                                    <tr>
                                        <td class="form-control">
                                            <asp:ImageButton ID="imgbtn" runat="server" CommandArgument='<%# Eval("KursTipiID") %>' ImageUrl="~/Gorseller/checkBox.png" Style="width: 20px;" OnClientClick="showLoadingIcon();" />
                                            <asp:Label ID="lblIcerik" runat="server" Text='<%#Eval("KursTipi") %>' AssociatedControlID='<%# Container.FindControl("imgbtn").ID %>'></asp:Label>
                                            <asp:HiddenField ID="hfIcerikSecim" runat="server" Visible="false" Value="false" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </fieldset>

                <fieldset id="fld_Etkinlik" runat="server" visible="true">
                    <legend>Event(s)</legend>
                    <table class="table table-striped">
                        <tbody>
                            <asp:Repeater ID="rptEtkinlikListesi" runat="server" ClientIDMode="AutoID" OnItemCommand="rptIcerikListesi_ItemCommand" DataSourceID="OleDbEtkinlikListesi">
                                <ItemTemplate>
                                    <tr>
                                        <td class="form-control">
                                            <asp:ImageButton ID="imgbtn" runat="server" CommandArgument='<%# Eval("EtkinlikID") %>' ImageUrl="~/Gorseller/checkBox.png" Style="width: 20px;" OnClientClick="showLoadingIcon();" />
                                            <asp:Label ID="lblIcerik" runat="server" Text='<%#Eval("Etkinlik") %>' AssociatedControlID='<%# Container.FindControl("imgbtn").ID %>'></asp:Label>
                                            <asp:HiddenField ID="hfIcerikSecim" runat="server" Visible="false" Value="false" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </fieldset>

                <fieldset>
                    <legend>Payment Information</legend>
                    <table class="AlseinTable">
                        <tr>
                            <td>*</td>
                            <td>Payment Type</td>
                            <td>
                                <asp:DropDownList ID="ddlOdemeTipi" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlOdemeTipi_SelectedIndexChanged" DataSourceID="OleDbOdemeTipiListesi" DataTextField="OdemeTipi" DataValueField="OdemeTipiID" onchange="showLoadingIcon();"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>Registration & Accommodation Fee</td>
                            <td>
                                <asp:Label ID="lblKatilimciTipiOdaTipiUcret" runat="server" CssClass="form-control"></asp:Label>
                                <asp:HiddenField ID="hfKatilimciTipiOdaTipiUcret" runat="server" Visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>Transfer Fee</td>
                            <td>
                                <asp:Label ID="lblTransferUcret" runat="server" CssClass="form-control"></asp:Label>
                                <asp:HiddenField ID="hfTransferUcret" runat="server" Visible="false" />
                            </td>
                        </tr>
                        <tr id="tr_KursUcret" runat="server" visible="true">
                            <td>&nbsp;</td>
                            <td>Course(s) Fee</td>
                            <td>
                                <asp:Label ID="lblKursUcret" runat="server" CssClass="form-control"></asp:Label>
                                <asp:HiddenField ID="hfKursUcret" runat="server" Visible="false" />
                            </td>
                        </tr>
                        <tr id="tr_EtkinlikUcret" runat="server" visible="true">
                            <td>&nbsp;</td>
                            <td>Event(s) Fee</td>
                            <td>
                                <asp:Label ID="lblEtkinlikUcret" runat="server" CssClass="form-control"></asp:Label>
                                <asp:HiddenField ID="hfEtkinlikUcret" runat="server" Visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>Total</td>
                            <td>
                                <asp:Label ID="lblToplamUcret" runat="server" CssClass="form-control" Enabled="false"></asp:Label>
                                <asp:HiddenField ID="hfToplamUcret" runat="server" Visible="false" />
                            </td>
                        </tr>
                    </table>
                </fieldset>

                <asp:Panel ID="PnlBankaBilgisi" runat="server" Visible="False">
                    <fieldset class="mt-3">
                        <legend>Bank Transfer Information</legend>
                        <table class="AlseinTable">
                            <tr>
                                <td>&nbsp;</td>
                                <td>Account</td>
                                <td><span class="form-control">*** </span></td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>Bank</td>
                                <td><span class="form-control">*** </span></td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>Branch</td>
                                <td><span class="form-control">*** </span></td>
                            </tr>

                            <tr>
                                <td>&nbsp;</td>
                                <td>Account No</td>
                                <td><span class="form-control">*** </span></td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>IBAN</td>
                                <td><span class="form-control">*** </span></td>
                            </tr>
                        </table>
                        <p class="text-center" style="color: #fff">In order for us to track your payment, please do not forget to send your receipt to <a href="mailto:....@....">....@....</a>, including the order number.</p>
                    </fieldset>
                </asp:Panel>

                <p class="text-center">
                    <asp:LinkButton ID="lnkbtnKayitOl" runat="server" CssClass="btn btn-success btn-x" OnClick="lnkbtnKayitOl_Click" OnClientClick="showLoadingIcon();">
                        <i class="fa fa-check"></i>&nbsp;Complete Registration
                    </asp:LinkButton>
                    <asp:ImageButton ID="ImgLodingIcon" runat="server" CssClass="LoadingIcon" ImageUrl="~/Gorseller/loadspinner.gif" />
                </p>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:SqlDataSource runat="server" ID="OleDbKatilimciTipiListesi" ConnectionString='<%$ ConnectionStrings:Norosirurjihemsireligi2026_Web %>' ProviderName='<%$ ConnectionStrings:Norosirurjihemsireligi2026_Web.ProviderName %>' SelectCommand="SELECT [KatilimciTipiDilTablosu].* FROM [KatilimciTipiDilTablosu] INNER JOIN [KatilimciTipiTablosu] ON [KatilimciTipiDilTablosu].[KatilimciTipiID] = [KatilimciTipiTablosu].[KatilimciTipiID] WHERE [KatilimciTipiDilTablosu].[DilID] = 'en' ORDER BY [KatilimciTipiTablosu].[Sira]"></asp:SqlDataSource>

    <asp:SqlDataSource runat="server" ID="OleDbOtelListesi" ConnectionString='<%$ ConnectionStrings:Norosirurjihemsireligi2026_Web %>' ProviderName='<%$ ConnectionStrings:Norosirurjihemsireligi2026_Web.ProviderName %>' SelectCommand="SELECT * FROM [OtelTablosu] ORDER BY Sira"></asp:SqlDataSource>

    <asp:SqlDataSource runat="server" ID="OleDbKatilimciTipiOdaTipiListesi" ConnectionString='<%$ ConnectionStrings:Norosirurjihemsireligi2026_Web %>' ProviderName='<%$ ConnectionStrings:Norosirurjihemsireligi2026_Web.ProviderName %>' SelectCommand="SELECT [KatilimciTipiOdaTipiTablosu].[KatilimciTipiOdaTipiID], [OdaTipiDilTablosu].[OdaTipi] FROM ( [OdaTipiDilTablosu] INNER JOIN [OdaTipiTablosu] ON [OdaTipiDilTablosu].[OdaTipiID] = [OdaTipiTablosu].[OdaTipiID] ) INNER JOIN [KatilimciTipiOdaTipiTablosu] ON [OdaTipiTablosu].[OdaTipiID] = [KatilimciTipiOdaTipiTablosu].[OdaTipiID] WHERE [OdaTipiDilTablosu].[DilID] = 'en' AND [KatilimciTipiOdaTipiTablosu].[KatilimciTipiID] = ? AND [OdaTipiTablosu].[OtelID] = ? ORDER BY [OdaTipiTablosu].[Sira]">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlKatilimciTipi" PropertyName="SelectedValue" Name="?" DefaultValue="0" Type="Int32" />
            <asp:ControlParameter ControlID="ddlOtel" PropertyName="SelectedValue" Name="?" DefaultValue="0" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource runat="server" ID="OleDbTransferTipiListesi" ConnectionString='<%$ ConnectionStrings:Norosirurjihemsireligi2026_Web %>' ProviderName='<%$ ConnectionStrings:Norosirurjihemsireligi2026_Web.ProviderName %>' SelectCommand="SELECT [TransferTipiDilTablosu].* FROM [TransferTipiDilTablosu] INNER JOIN [TransferTipiTablosu] ON [TransferTipiDilTablosu].[TransferTipiID] = [TransferTipiTablosu].[TransferTipiID] WHERE [TransferTipiDilTablosu].[DilID] = 'en' ORDER BY Sira"></asp:SqlDataSource>

    <asp:SqlDataSource runat="server" ID="OleDbKursListesi" ConnectionString='<%$ ConnectionStrings:Norosirurjihemsireligi2026_Web %>' ProviderName='<%$ ConnectionStrings:Norosirurjihemsireligi2026_Web.ProviderName %>' SelectCommand="SELECT [KursTipiDilTablosu].* FROM [KursTipiDilTablosu] INNER JOIN [KursTipiTablosu] ON [KursTipiDilTablosu].[KursTipiID] = [KursTipiTablosu].[KursTipiID] WHERE [KursTipiDilTablosu].[DilID] = 'en' ORDER BY Sira"></asp:SqlDataSource>

    <asp:SqlDataSource runat="server" ID="OleDbEtkinlikListesi" ConnectionString='<%$ ConnectionStrings:Norosirurjihemsireligi2026_Web %>' ProviderName='<%$ ConnectionStrings:Norosirurjihemsireligi2026_Web.ProviderName %>' SelectCommand="SELECT [EtkinlikDilTablosu].* FROM [EtkinlikDilTablosu] INNER JOIN [EtkinlikTablosu] ON [EtkinlikTablosu].[EtkinlikID] = [EtkinlikDilTablosu].[EtkinlikID] WHERE [EtkinlikDilTablosu].[DilID] = 'en' ORDER BY Sira"></asp:SqlDataSource>

    <asp:SqlDataSource runat="server" ID="OleDbOdemeTipiListesi" ConnectionString='<%$ ConnectionStrings:Norosirurjihemsireligi2026_Web %>' ProviderName='<%$ ConnectionStrings:Norosirurjihemsireligi2026_Web.ProviderName %>' SelectCommand="SELECT * FROM [OdemeTipiDilTablosu] WHERE [DilID] = 'en'"></asp:SqlDataSource>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="en_SubContent" runat="server">
</asp:Content>
