using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNet.FriendlyUrls;
using Model;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Xml;
using VeritabaniIslemMerkezi;
using static System.Net.WebRequestMethods;

namespace Norosirurjihemsireligi2026_Web.tr
{
    public partial class Odeme : Page
    {
        IList<string> segments;

        StringBuilder Uyarilar = new StringBuilder();
        BilgiKontrolMerkezi Kontrol = new BilgiKontrolMerkezi();

        SurecVeriModel<OdemeTablosuModel> SDataModel;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                segments = Request.GetFriendlyUrlSegments();

                if (segments.Count.Equals(1))
                {
                    SDataModel = new OdemeTablosuIslemler().KayitBilgisi(segments.First(), "tr");

                    if (SDataModel.Sonuc.Equals(Sonuclar.Basarili) && SDataModel.Veriler.OdemeTipiID.Equals(2) && !SDataModel.Veriler.Durum && SDataModel.Veriler.OdemeTarihi is null)
                    {
                        lblAdSoyad.Text = $"{SDataModel.Veriler.KatilimciBilgisi.AdSoyad}";
                        hfePosta.Value = SDataModel.Veriler.KatilimciBilgisi.ePosta;
                        lblDovizUcret.Text = SDataModel.Veriler.DovizUcret;
                        hfDovizUcret.Value = lblDovizUcret.Text.Replace(Sabitler.KurSimgesi, string.Empty).Trim();

                        lblOdemeID.Text = SDataModel.Veriler.OdemeID;
                        hfOdemeID.Value = SDataModel.Veriler.OdemeID;
                    }
                    else
                    {
                        Response.Redirect("~/tr");
                    }
                }
                else
                {
                    Response.Redirect("~/tr");
                }
            }
        }


        protected void lnkbtnKayitOl_Click(object sender, EventArgs e)
        {

            Kontrol.BoolKontrol(ddlKrediKartiUlke.SelectedValue, "Kredi kartınızın ait olduğu ülkeyi seçiniz", "Geçersiz ülke seçildi", ref Uyarilar);
            Kontrol.KelimeKontrol(txtKrediKartNo, "Kredi kart numarası boş bırakılamaz", ref Uyarilar);
            Kontrol.KelimeKontrol(txtAy, "Ay boş bırakılamaz", ref Uyarilar);
            Kontrol.KelimeKontrol(txtYil, "Yıl boş bırakılamaz", ref Uyarilar);


            if (string.IsNullOrEmpty(Uyarilar.ToString()))
            {
                OdemeTablosuModel OModel = null;
                string currencyCode = string.Empty;
                string amount = string.Empty;

                if (!Convert.ToBoolean(ddlKrediKartiUlke.SelectedValue))
                {
                    currencyCode = "978";
                    amount = $"{hfDovizUcret.Value.Replace(",", ".")}";
                    OModel = new OdemeTablosuModel
                    {
                        OdemeID = hfOdemeID.Value,
                        TurkLirasiUcret = "0,00 ₺",
                        KurUcret = "0,00 ₺"
                    };
                }
                else //TL
                {
                    TCMBKur(out decimal Euro);
                    currencyCode = "949";
                    amount = $"{(Convert.ToDecimal(hfDovizUcret.Value.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture) * Euro):0.00}".Replace(",", ".");
                    OModel = new OdemeTablosuModel
                    {
                        OdemeID = hfOdemeID.Value,
                        TurkLirasiUcret = $"{(Convert.ToDecimal(hfDovizUcret.Value.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture) * Euro):0.00}₺",
                        KurUcret = $"{Euro:0.00} ₺"
                    };
                }
              

                Dictionary<string, string> postParams = new Dictionary<string, string>();

                postParams.Add("paymentModel", "3D_PAY");
                postParams.Add("txnCode", "3000");
                postParams.Add("merchantSafeId", "2025100814321528455172D992BA4244");
                postParams.Add("terminalSafeId", "202510081432152944DE3F6EAF7CA626");
                postParams.Add("orderId", $"{hfOdemeID.Value.ToString()}");
                postParams.Add("lang", "TR");
                postParams.Add("amount", $"{amount}");
                postParams.Add("ccbRewardAmount", "0.00");
                postParams.Add("pcbRewardAmount", "0.00");
                postParams.Add("xcbRewardAmount", "0.00");
                postParams.Add("currencyCode", $"{currencyCode}");
                postParams.Add("installCount", "1");
                postParams.Add("okUrl", $"{Request.Url.Scheme}://{Request.Url.Authority}{Request.ApplicationPath}/tr/OdemeBasarili/{hfOdemeID.Value}");
                postParams.Add("failUrl", $"{Request.Url.Scheme}://{Request.Url.Authority}{Request.ApplicationPath}/tr/OdemeBasarisiz/{hfOdemeID.Value}");

                postParams.Add("emailAddress", $"{hfePosta.Value.ToString()}");
                postParams.Add("creditCard", $"{txtKrediKartNo.Text.Replace(" ", "")}");
                postParams.Add("expiredDate", $"{txtAy.Text.Trim()}{txtYil.Text.Trim()}");
                postParams.Add("cvv", $"{txtCVV2.Text.Trim()}");
                postParams.Add("randomNumber", getRandomNumberBase16());
                postParams.Add("requestDateTime", $"{DateTime.Now:yyyy-MM-ddTHH:mm:ss.fff}");

                string SecretKey = "32303235313030383134333231353237343274723173325f355f7337377235765f737338353332353738385f3138747667737474767676673533763567313232";

                StringBuilder sBuilder = new StringBuilder()
                    .Append(postParams["paymentModel"])
                    .Append(postParams["txnCode"])
                    .Append(postParams["merchantSafeId"])
                    .Append(postParams["terminalSafeId"])
                    .Append(postParams["orderId"])
                    .Append(postParams["lang"])
                    .Append(postParams["amount"])
                    .Append(postParams["ccbRewardAmount"])
                    .Append(postParams["pcbRewardAmount"])
                    .Append(postParams["xcbRewardAmount"])
                    .Append(postParams["currencyCode"])
                    .Append(postParams["installCount"])
                    .Append(postParams["okUrl"])
                    .Append(postParams["failUrl"])
                    .Append(postParams["emailAddress"])
                    .Append(string.Empty)
                    .Append(string.Empty)
                    .Append(string.Empty)
                    .Append(string.Empty)
                    .Append(postParams["creditCard"])
                    .Append(postParams["expiredDate"])
                    .Append(postParams["cvv"])
                    .Append(string.Empty)
                    .Append(postParams["randomNumber"])
                    .Append(postParams["requestDateTime"])
                    .Append(string.Empty)
                    .Append(string.Empty)
                    .Append(string.Empty)
                    .Append(string.Empty)
                    .Append(string.Empty)
                    .Append(string.Empty)
                    .Append(string.Empty)
                    ;

                using (HMACSHA512 hmac = new HMACSHA512(Encoding.UTF8.GetBytes(SecretKey)))
                {
                    string hash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(sBuilder.ToString())));
                    postParams.Add("hash", hash);
                    OModel.OdemeTarihi = new BilgiKontrolMerkezi().Simdi();
                    OModel.Hash = hash;
                }

                new OdemeTablosuIslemler().HashGuncelleme(OModel);

                using (HttpClient hc = new HttpClient())
                {
                    using (HttpResponseMessage response = hc.PostAsync(
                        "https://virtualpospaymentgateway.akbank.com/securepay",
                        new FormUrlEncodedContent(postParams)
                    ).GetAwaiter().GetResult())
                    {
                        string responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        System.IO.File.WriteAllText(Server.MapPath(
                            $"~/Dosyalar/PaymentLog/Prepare/{hfOdemeID.Value}_Html_{DateTime.Now:yyyy.MM.dd_HH.mm.ss}.log"),
                            responseContent
                        );
                        Response.Write(responseContent);
                    }
                }


            }
        }
        public static string getRandomNumberBase16(int length = 128)
        {
            byte[] data = new byte[length];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(length);
            foreach (byte b in data)
            {
                result.Append((b % 16).ToString("X"));
            }
            return result.ToString();
        }

        void TCMBKur(out decimal Euro)
        {
            Euro = 0.00m;

            bool Durum = false;
            do
            {
                try
                {
                    XmlDocument xmlVerisi = new XmlDocument();
                    xmlVerisi.Load("https://www.tcmb.gov.tr/kurlar/today.xml");
                    string Bulten = xmlVerisi.SelectSingleNode("Tarih_Date").Attributes["Bulten_No"].Value;
                    string Tarih = xmlVerisi.SelectSingleNode("Tarih_Date").Attributes["Tarih"].Value;
                    Euro = decimal.Round(Convert.ToDecimal(xmlVerisi.SelectSingleNode(string.Format("Tarih_Date/Currency[@Kod='{0}']/BanknoteSelling", "EUR")).InnerText.Replace('.', ',')), 4);
                    Durum = true;
                }
                catch (Exception)
                {
                    Durum = false;
                }
            } while (!Durum);
        }
    }
}
