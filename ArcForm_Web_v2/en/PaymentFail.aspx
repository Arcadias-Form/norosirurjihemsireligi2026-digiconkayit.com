<%@ Page Title="" Language="C#" MasterPageFile="~/en/en.master" AutoEventWireup="true" CodeBehind="PaymentFail.aspx.cs" Inherits="ArcForm_Web_v2.en.PaymentFail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="en_Head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="en_Content" runat="server">
    <div class="row">
        <div class="col-lg-12 text-center">
            <fieldset>
                <legend>Payment Fail</legend>
                <p>
                    <asp:Image ID="ImgFail" runat="server" CssClass="w-50" Style="max-width: 180px;" ImageUrl="~/Gorseller/Failed.png" />
                </p>
                <div style="color: white">
                    <p>
                        Dear
                        <asp:Label ID="lblAdSoyad" runat="server"></asp:Label>
                    </p>
                    <p>
                        Please note that if the transaction fails, please contact your bank to clarify the reason before trying again, as banks sometimes block cards for security reasons. Please inform us that you will use your credit card for international payments and that it is authorized by you.
                    </p>
                    <p>
                        Please make sure it is open for online payments.
                    </p>
                    <p>
                        Please make sure you have enough limit for payment.
                    </p>
                    <p style="color: black">
                        <u>Order No:</u>
                        <asp:Label ID="lblOdemeID" runat="server" Font-Bold="true"></asp:Label>
                    </p>
                </div>

            </fieldset>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="en_SubContent" runat="server">
</asp:Content>
