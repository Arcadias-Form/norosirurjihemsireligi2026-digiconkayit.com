<%@ Page Title="" Language="C#" MasterPageFile="~/tr/tr.master" AutoEventWireup="true" CodeBehind="BasariliKayit.aspx.cs" Inherits="Norosirurjihemsireligi2026_Web.tr.BasariliKayit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="tr_Head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="tr_Content" runat="server">
    <div class="container mt-5">
        <div class="row">
            <div class="col-lg-12 text-center">
                <h5 class="baslik d-none">Başarılı Kayıt</h5>
                <p>
                    <asp:Image ID="ImgOk" runat="server" CssClass="w-50" Style="max-width: 180px;" ImageUrl="~/Gorseller/tick.png" />
                </p>
                <div style="color: white;">
                    <p>
                        Sayın
                        <asp:Label ID="lblAdSoyad" runat="server" Text=""></asp:Label>,
                    </p>
                    <asp:Panel ID="PnlKrediKarti" runat="server" Visible="false" CssClass="text-center">
                        <p>
                            Kaydınız için teşekkür ederiz.
                        </p>
                        <p>
                            E-postanıza yazılı bir onay gönderildi.
                        </p>
                        <p>
                            İhtiyacınız olabilecek her türlü yardım için <a href="mailto:tnd2026@honestglobal.com">tnd2026@honestglobal.com</a> iletişime geçebilirsiniz.
                        </p>
                    </asp:Panel>

                    <asp:Panel ID="PnlBankaBilgisi" runat="server" CssClass="text-center" Visible="false">
                        <p>
                            Kayıt başvurunuz için çok teşekkür ederiz.
                        </p>
                        <p>
                            Banka havalesini gerçekleştirmek için lütfen aşağıda banka hesabtan ödemenizi gerçekleştiriniz.
                        </p>
                        <p>Ödemenizi takip edebilmemiz için, dekontunuzu <a href="mailto:tnd2026@honestglobal.com">tnd2026@honestglobal.com</a> adresine sipariş numarası içerecek şekilde göndermeyi unutmayınız.</p>
                    </asp:Panel>

                    <p class="text-center">
                        <u>Sipariş Numarası:</u>
                        <asp:Label ID="lblOdemeID" runat="server" Font-Bold="true"></asp:Label>
                    </p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="tr_SubContent" runat="server">
</asp:Content>
