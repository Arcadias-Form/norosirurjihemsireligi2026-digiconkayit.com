<%@ Page Title="" Language="C#" MasterPageFile="~/en/en.master" AutoEventWireup="true" CodeBehind="RegistrationSuccess.aspx.cs" Inherits="ArcForm_Web_v2.en.RegistrationSuccess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="en_Head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="en_Content" runat="server">
    <div class="row">
        <div class="col-lg-12 text-center">
            <h5 class="baslik d-none">Registration Success</h5>
            <p>
                <asp:Image ID="ImgOk" runat="server" CssClass="w-50" Style="max-width: 180px;" ImageUrl="~/Gorseller/tick.png" />
            </p>
            <div style="color: white;">
                <p>
                    Dear
                    <asp:Label ID="lblAdSoyad" runat="server" Text=""></asp:Label>,
                </p>
                <asp:Panel ID="PnlKrediKarti" runat="server" Visible="false" CssClass="text-center">
                    <p>
                        Thank you for your registration.
                    </p>
                    <p>
                        A written confirmation has been sent to your email.
                    </p>
                    <p>
                        You can contact <a href="mailto:....@....">....@....</a> for any help you may need.
                    </p>
                </asp:Panel>

                <asp:Panel ID="PnlBankaBilgisi" runat="server" CssClass="text-center" Visible="false">
                    <p>
                        Thank you very much for your registration application.
                    </p>
                    <p>
                        To make a bank transfer, please make your payment from the bank account below.
                    </p>
                    <p>In order for us to track your payment, please do not forget to send your receipt to <a href="mailto:....@....">....@....</a>, including the order number.</p>
                </asp:Panel>

                <p class="text-center">
                    <u>Order No :</u>
                    <asp:Label ID="lblOdemeID" runat="server" Font-Bold="true"></asp:Label>
                </p>
            </div>

        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="en_SubContent" runat="server">
</asp:Content>
