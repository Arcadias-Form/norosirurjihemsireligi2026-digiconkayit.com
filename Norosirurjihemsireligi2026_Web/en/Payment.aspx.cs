using Microsoft.AspNet.FriendlyUrls;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using VeritabaniIslemMerkezi;

namespace Norosirurjihemsireligi2026_Web.en
{
    public partial class Payment : Page
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
                    SDataModel = new OdemeTablosuIslemler().KayitBilgisi(segments.First(), "en");

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
                        Response.Redirect("~/en");
                    }
                }
                else
                {
                    Response.Redirect("~/en");
                }
            }
        }


        protected void lnkbtnKayitOl_Click(object sender, EventArgs e)
        {
            Kontrol.BoolKontrol(ddlKrediKartiUlke.SelectedValue, "Please select country of credit card.", "Invalid country has selected", ref Uyarilar);
            Kontrol.KelimeKontrol(txtKrediKartNo, "Credit card no cannot be empty.", ref Uyarilar);
            Kontrol.KelimeKontrol(txtAy, "Exp. month cannot be empty.", ref Uyarilar);
            Kontrol.KelimeKontrol(txtYil, "Exp. year cannot be empty.", ref Uyarilar);
            Kontrol.KelimeKontrol(txtCVV2, "CVV2 cannot be empty.", ref Uyarilar);

            if (string.IsNullOrEmpty(Uyarilar.ToString()))
            {
                // Bankaya göre kod düzeneği gelecek.
            }
            else
            {
                BilgiKontrolMerkezi.UyariEkrani(this, $"UyariBilgilendirme('', '{Uyarilar}', false);", true, true);
            }


        }
    }
}