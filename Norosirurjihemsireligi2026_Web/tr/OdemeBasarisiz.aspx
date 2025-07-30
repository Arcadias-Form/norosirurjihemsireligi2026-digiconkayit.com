<%@ Page Title="" Language="C#" MasterPageFile="~/tr/tr.master" AutoEventWireup="true" CodeBehind="OdemeBasarisiz.aspx.cs" Inherits="Norosirurjihemsireligi2026_Web.tr.OdemeBasarisiz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="tr_Head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="tr_Content" runat="server">
    <div class="row">
        <div class="col-lg-12 text-center">
            <fieldset style="color: red">
                <legend>Ödeme Başarısız</legend>
                <p>
                    <asp:Image ID="ImgFail" runat="server" CssClass="w-50" Style="max-width: 180px;" ImageUrl="~/Gorseller/Failed.png" />
                </p>
                <p>
                    Sayın
                <asp:Label ID="lblAdSoyad" runat="server"></asp:Label>
                </p>
                <p>
                    İşlemin başarısız olduğunu lütfen unutmayın, bankalar bazen güvenlik nedeniyle kartları bloke ettiğinden, bir kez daha denemeden önce lütfen bankanızla iletişime geçerek nedenini açıklığa kavuşturun. Lütfen kredi kartınızı uluslararası ödemelerde kullanacağınızı ve sizin tarafınızdan yetkilendirildiğini bildirin.
                </p>
                <p>
                    Lütfen online ödemelere açık olduğundan emin olunuz.
                </p>
                <p>
                    Lütfen ödeme için yeterli limitin olduğundan emin olun.
                </p>
                <p style="color: black">
                    <u>Sipariş No:</u>
                    <asp:Label ID="lblOdemeID" runat="server" Font-Bold="true"></asp:Label>
                </p>
            </fieldset>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="tr_SubContent" runat="server">
</asp:Content>
