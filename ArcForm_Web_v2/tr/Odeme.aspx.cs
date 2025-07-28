using Microsoft.AspNet.FriendlyUrls;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using VeritabaniIslemMerkezi;

namespace ArcForm_Web_v2.tr
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
            Kontrol.KelimeKontrol(txtCVV2, "CVV2 boş bırakılamaz", ref Uyarilar);

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