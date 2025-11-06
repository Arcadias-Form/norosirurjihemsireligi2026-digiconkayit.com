<%@ Page Title="" Language="C#" MasterPageFile="~/tr/tr.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Norosirurjihemsireligi2026_Web.tr.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="tr_Head" runat="server">
    <link rel="stylesheet" type="text/css" href="<%=ResolveClientUrl("~/css/bootstrap-datepicker.min.css") %>" />
    <script type="text/javascript" src="<%=ResolveClientUrl("~/js/bootstrap-datepicker.min.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveClientUrl("~/js/bootstrap-datepicker.tr.min.js") %>"></script>
    <script type="text/javascript" src="<%=ResolveClientUrl("~/js/jquery.inputmask.min.js") %>"></script>
    <script type="text/javascript">
        function pageLoad(sender, args) {
            datePickerOption.language = "tr";
            datePickerOption.placeholder = "Tarih seçmek için tıklayınız.";
            $('#<%= txtTelefon.ClientID%>').inputmask("(599) 999 99 99", { onincomplete: () => { $('#<%= txtTelefon.ClientID%>').val(''); } });
            $('#<%= txtKimlikNo.ClientID%>').inputmask("99999999999", { onincomplete: () => { $('#<%= txtTelefon.ClientID%>').val(''); } });
            setDatePicker();
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="tr_Content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UPnlGenel" runat="server" class="row">
        <ContentTemplate>
            <div class="col-md-12 mt-3">
                <fieldset>
                    <legend>Kişisel Bilgiler</legend>
                    <table class="AlseinTable">
                        <tr>
                            <td>*</td>
                            <td>Ad & Soyad</td>
                            <td>
                                <asp:TextBox ID="txtAdSoyad" runat="server" CssClass="form-control" onchange="toUpper(this);" onkeyup="toUpper(this);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>*</td>
                            <td>Kimlik No</td>
                            <td>
                                <asp:TextBox ID="txtKimlikNo" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>*</td>
                            <td>Cinsiyet</td>
                            <td>
                                <asp:DropDownList ID="ddlCinsiyet" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Seçiniz" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Erkek" Value="Erkek"></asp:ListItem>
                                    <asp:ListItem Text="Kadın" Value="Kadın"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>*</td>
                            <td>e-Posta</td>
                            <td>
                                <asp:TextBox ID="txtePosta" runat="server" CssClass="form-control" TextMode="EMail"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>*</td>
                            <td>Cep Telefonu</td>
                            <td>
                                <asp:TextBox ID="txtTelefon" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>Kurum</td>
                            <td>
                                <asp:TextBox ID="txtKurum" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </fieldset>

                <fieldset>
                    <legend>Fatura Bilgileri</legend>
                    <table class="AlseinTable">
                        <tr>
                            <td>*</td>
                            <td>Fatura Tipi</td>
                            <td>
                                <asp:DropDownList ID="ddlFaturaTipi" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Seçiniz" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Kişisel" Value="Kişisel"></asp:ListItem>
                                    <asp:ListItem Text="Kurumsal" Value="Kurumsal"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>*</td>
                            <td>Fatura Unvan / Ad & Soyad</td>
                            <td>
                                <asp:TextBox ID="txtFaturaUnvan" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>*</td>
                            <td>Fatura Adresi</td>
                            <td>
                                <asp:TextBox ID="txtFaturaAdres" runat="server" CssClass="form-control" TextMode="MultiLine" Style="height: 80px; resize: none;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>Vergi Dairesi</td>
                            <td>
                                <asp:TextBox ID="txtVergiDairesi" runat="server" CssClass="form-control"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>Vergi No / Kimlik No</td>
                            <td>
                                <asp:TextBox ID="txtVergiNo" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                        </tr>

                    </table>
                </fieldset>

                <fieldset>
                    <legend>Katılım &  Konaklama Bilgileri</legend>
                    <table class="AlseinTable">
                        <tr>
                            <td>*</td>
                            <td>Katılımcı Tipi</td>
                            <td>
                                <asp:DropDownList ID="ddlKatilimciTipi" runat="server" CssClass="form-control" AutoPostBack="true" DataSourceID="OleDbKatilimciTipiListesi" DataTextField="KatilimciTipi" DataValueField="KatilimciTipiID" onchange="showLoadingIcon();" OnSelectedIndexChanged="ddlKatilimciTipi_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="tr_Konaklama" runat="server" visible="false">
                            <td>*</td>
                            <td>Konaklama</td>
                            <td>
                                <asp:DropDownList ID="ddlOtel" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlOtel_SelectedIndexChanged" AutoPostBack="true" DataSourceID="OleDbOtelListesi" DataTextField="Otel" DataValueField="OtelID" onchange="showLoadingIcon();"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="tr_OdaTipi" runat="server" visible="false">
                            <td>*</td>
                            <td>Oda Tipi</td>
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
                            <td>Refakatçi Ad & Soyad</td>
                            <td>
                                <asp:TextBox ID="txtRefakatci" runat="server" CssClass="form-control" onchange="toUpper(this);" onkeyup="toUpper(this);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="tr_Transfer" runat="server" visible="false">
                            <td>*</td>
                            <td>Transfer Tipi</td>
                            <td>
                                <asp:DropDownList ID="ddlTransferTipi" runat="server" CssClass="form-control" AutoPostBack="true" DataSourceID="OleDbTransferTipiListesi" DataTextField="TransferTipi" DataValueField="TransferTipiID" onchange="showLoadingIcon();" OnSelectedIndexChanged="FiyatHesaplama"></asp:DropDownList>
                            </td>
                        </tr>
                    </table>

                </fieldset>

                <fieldset id="fld_Kurs" runat="server" visible="true">
                    <legend>Kurs(lar)</legend>
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
                    <legend>Etkinlik(ler)</legend>
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
                    <legend>Ödeme Bilgileri</legend>
                    <table class="AlseinTable">
                        <tr>
                            <td>*</td>
                            <td>Ödeme Tipi</td>
                            <td>
                                <asp:DropDownList ID="ddlOdemeTipi" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlOdemeTipi_SelectedIndexChanged" DataSourceID="OleDbOdemeTipiListesi" DataTextField="OdemeTipi" DataValueField="OdemeTipiID" onchange="showLoadingIcon();"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>Kayıt ve Konaklama Ücreti</td>
                            <td>
                                <asp:Label ID="lblKatilimciTipiOdaTipiUcret" runat="server" CssClass="form-control"></asp:Label>
                                <asp:HiddenField ID="hfKatilimciTipiOdaTipiUcret" runat="server" Visible="false" />
                            </td>
                        </tr>
                        <tr id="tr_transferUcret" runat="server" visible="false">
                            <td>&nbsp;</td>
                            <td>Transfer Ücreti</td>
                            <td>
                                <asp:Label ID="lblTransferUcret" runat="server" CssClass="form-control"></asp:Label>
                                <asp:HiddenField ID="hfTransferUcret" runat="server" Visible="false" />
                            </td>
                        </tr>
                        <tr id="tr_KursUcret" runat="server" visible="true">
                            <td>&nbsp;</td>
                            <td>Kurs Ücreti</td>
                            <td>
                                <asp:Label ID="lblKursUcret" runat="server" CssClass="form-control"></asp:Label>
                                <asp:HiddenField ID="hfKursUcret" runat="server" Visible="false" />
                            </td>
                        </tr>
                        <tr id="tr_EtkinlikUcret" runat="server" visible="true">
                            <td>&nbsp;</td>
                            <td>Etkinlik Ücreti</td>
                            <td>
                                <asp:Label ID="lblEtkinlikUcret" runat="server" CssClass="form-control"></asp:Label>
                                <asp:HiddenField ID="hfEtkinlikUcret" runat="server" Visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>Toplam</td>
                            <td>
                                <asp:Label ID="lblToplamUcret" runat="server" CssClass="form-control" Enabled="false"></asp:Label>
                                <asp:HiddenField ID="hfToplamUcret" runat="server" Visible="false" />
                            </td>
                        </tr>
                    </table>
                </fieldset>



                <div class="alert alert-warning border border-danger rounded p-4" style="font-size: 1rem; font-weight: 500; line-height: 1.6;">
                    <h5 class="text-danger font-weight-bold mb-3">İPTAL KOŞULLARI</h5>
                    <ul class="mb-2">
                        <li>01 Şubat 2026 <strong>tarihinden önce</strong> yapılan iptal taleplerinde, yatırılmış olan sponsorluk ücretinin <strong>%10’u banka masrafı</strong> kesildikten sonra iade edilecektir.</li>
                        <li>01 Şubat 2026 <strong>tarihinden sonra</strong> yapılan iptallerde <strong>hiçbir iade yapılmayacaktır.</strong></li>
                        <li>01 Şubat 2026 tarihinden sonra sadece <strong>isim değişiklikleri</strong> kabul edilecektir.</li>
                    </ul>
                    <p class="mb-0"><strong>Not:</strong> Tüm iadeler <strong>kongre sonrasında</strong> yapılacaktır.</p>
                </div>


                <asp:Panel ID="PnlBankaBilgisi" runat="server" Visible="False">
                    <fieldset class="mt-3">
                        <legend>Hesap Bilgileri</legend>
                        <table class="AlseinTable">
                            <tr>
                                <td>&nbsp;</td>
                                <td>Banka Adı </td>
                                <td><span class="form-control">AKBANK</span></td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>Şube Adı / Kodu</td>
                                <td><span class="form-control">HASANPAŞA ŞUBE - 0235 </span></td>
                            </tr>

                            <tr>
                                <td>&nbsp;</td>
                                <td>Hesap Adı</td>
                                <td><span class="form-control">AKTİV TURİZM SEYEHAT VE KARGO TAŞIMACILIĞI</span></td>
                            </tr>

                            <tr>
                                <td>&nbsp;</td>
                                <td>TL IBAN</td>
                                <td><span class="form-control">TR36 0004 6002 3588 8000 1624 39 </span></td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>EURO IBAN</td>
                                <td><span class="form-control">TR80 0004 6002 3503 6000 1296 99 </span></td>
                            </tr>

                            <tr>
                                <td>&nbsp;</td>
                                <td>Açıklama </td>
                                <td><span class="form-control">ISTKONGRETND_2026 </span></td>
                            </tr>
                        </table>
                        <p class="text-center" style="color: #fff">Ödemenizi takip edebilmemiz için, dekontunuzu <a href="mailto:tnd2026@honestglobal.com">tnd2026@honestglobal.com</a> adresine sipariş numarası içerecek şekilde göndermeyi unutmayınız.</p>
                    </fieldset>
                </asp:Panel>

                <div class="mt-3 text-center">
                    <a href="../Dosyalar/KVKK.docx" target="_blank" class="btn btn-primary mt-3">KVKK Metnini Görüntüleyin</a>
                </div>


                <p class="text-center mt-4">
                    <asp:LinkButton ID="lnkbtnKayitOl" runat="server" CssClass="btn btn-success btn-x" OnClick="lnkbtnKayitOl_Click" OnClientClick="showLoadingIcon();">
                        <i class="fa fa-check"></i>&nbsp;Kaydımı Tamamla
                    </asp:LinkButton>
                    <asp:ImageButton ID="ImgLodingIcon" runat="server" CssClass="LoadingIcon" ImageUrl="~/Gorseller/loadspinner.gif" />
                </p>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:SqlDataSource runat="server" ID="OleDbKatilimciTipiListesi" ConnectionString='<%$ ConnectionStrings:Norosirurjihemsireligi2026_Web %>' ProviderName='<%$ ConnectionStrings:Norosirurjihemsireligi2026_Web.ProviderName %>' SelectCommand="SELECT [KatilimciTipiDilTablosu].* FROM [KatilimciTipiDilTablosu] INNER JOIN [KatilimciTipiTablosu] ON [KatilimciTipiDilTablosu].[KatilimciTipiID] = [KatilimciTipiTablosu].[KatilimciTipiID] WHERE [KatilimciTipiDilTablosu].[DilID] = 'tr' ORDER BY [KatilimciTipiTablosu].[Sira]"></asp:SqlDataSource>

    <asp:SqlDataSource runat="server" ID="OleDbOtelListesi" ConnectionString='<%$ ConnectionStrings:Norosirurjihemsireligi2026_Web %>' ProviderName='<%$ ConnectionStrings:Norosirurjihemsireligi2026_Web.ProviderName %>' SelectCommand="SELECT * FROM [OtelTablosu] WHERE OtelID <> 1 ORDER BY Sira"></asp:SqlDataSource>

    <asp:SqlDataSource runat="server" ID="OleDbKatilimciTipiOdaTipiListesi" ConnectionString='<%$ ConnectionStrings:Norosirurjihemsireligi2026_Web %>' ProviderName='<%$ ConnectionStrings:Norosirurjihemsireligi2026_Web.ProviderName %>' SelectCommand="SELECT [KatilimciTipiOdaTipiTablosu].[KatilimciTipiOdaTipiID], [OdaTipiDilTablosu].[OdaTipi] FROM ( [OdaTipiDilTablosu] INNER JOIN [OdaTipiTablosu] ON [OdaTipiDilTablosu].[OdaTipiID] = [OdaTipiTablosu].[OdaTipiID] ) INNER JOIN [KatilimciTipiOdaTipiTablosu] ON [OdaTipiTablosu].[OdaTipiID] = [KatilimciTipiOdaTipiTablosu].[OdaTipiID] WHERE [OdaTipiDilTablosu].[DilID] = 'tr' AND [KatilimciTipiOdaTipiTablosu].[KatilimciTipiID] = ? AND [OdaTipiTablosu].[OtelID] = ? ORDER BY [OdaTipiTablosu].[Sira]">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlKatilimciTipi" PropertyName="SelectedValue" Name="?" DefaultValue="0" Type="Int32" />
            <asp:ControlParameter ControlID="ddlOtel" PropertyName="SelectedValue" Name="?" DefaultValue="0" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource runat="server" ID="OleDbTransferTipiListesi" ConnectionString='<%$ ConnectionStrings:Norosirurjihemsireligi2026_Web %>' ProviderName='<%$ ConnectionStrings:Norosirurjihemsireligi2026_Web.ProviderName %>' SelectCommand="SELECT [TransferTipiDilTablosu].* FROM [TransferTipiDilTablosu] INNER JOIN [TransferTipiTablosu] ON [TransferTipiDilTablosu].[TransferTipiID] = [TransferTipiTablosu].[TransferTipiID] WHERE [TransferTipiDilTablosu].[DilID] = 'tr' ORDER BY Sira"></asp:SqlDataSource>

    <asp:SqlDataSource runat="server" ID="OleDbKursListesi" ConnectionString='<%$ ConnectionStrings:Norosirurjihemsireligi2026_Web %>' ProviderName='<%$ ConnectionStrings:Norosirurjihemsireligi2026_Web.ProviderName %>' SelectCommand="SELECT [KursTipiDilTablosu].* FROM [KursTipiDilTablosu] INNER JOIN [KursTipiTablosu] ON [KursTipiDilTablosu].[KursTipiID] = [KursTipiTablosu].[KursTipiID] WHERE [KursTipiDilTablosu].[DilID] = 'tr' ORDER BY Sira"></asp:SqlDataSource>

    <asp:SqlDataSource runat="server" ID="OleDbEtkinlikListesi" ConnectionString='<%$ ConnectionStrings:Norosirurjihemsireligi2026_Web %>' ProviderName='<%$ ConnectionStrings:Norosirurjihemsireligi2026_Web.ProviderName %>' SelectCommand="SELECT [EtkinlikDilTablosu].* FROM [EtkinlikDilTablosu] INNER JOIN [EtkinlikTablosu] ON [EtkinlikTablosu].[EtkinlikID] = [EtkinlikDilTablosu].[EtkinlikID] WHERE [EtkinlikDilTablosu].[DilID] = 'tr' ORDER BY Sira"></asp:SqlDataSource>

    <asp:SqlDataSource runat="server" ID="OleDbOdemeTipiListesi" ConnectionString='<%$ ConnectionStrings:Norosirurjihemsireligi2026_Web %>' ProviderName='<%$ ConnectionStrings:Norosirurjihemsireligi2026_Web.ProviderName %>' SelectCommand="SELECT * FROM [OdemeTipiDilTablosu] WHERE [DilID] = 'tr'"></asp:SqlDataSource>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="tr_SubContent" runat="server">
</asp:Content>
