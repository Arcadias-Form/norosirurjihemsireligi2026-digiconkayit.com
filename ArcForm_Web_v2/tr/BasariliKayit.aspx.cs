using Microsoft.AspNet.FriendlyUrls;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using VeritabaniIslemMerkezi;

namespace ArcForm_Web_v2.tr
{
    public partial class BasariliKayit : Page
    {
        IList<string> segment;
        SurecVeriModel<OdemeTablosuModel> SDataModel;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                segment = Request.GetFriendlyUrlSegments();

                if (segment.Count.Equals(1))
                {
                    SDataModel = new OdemeTablosuIslemler().KayitBilgisi(segment.First(), "tr");

                    if (SDataModel.Sonuc.Equals(Sonuclar.Basarili) && SDataModel.Veriler.Durum)
                    {
                        lblAdSoyad.Text = SDataModel.Veriler.KatilimciBilgisi.AdSoyad;
                        lblOdemeID.Text = SDataModel.Veriler.OdemeID;
                        PnlKrediKarti.Visible = SDataModel.Veriler.OdemeTipiID.Equals(1);
                        PnlBankaBilgisi.Visible = SDataModel.Veriler.OdemeTipiID.Equals(2);
                    }
                    else
                    {
                        Response.Redirect("~/tr", true);
                    }
                }
                else
                {
                    Response.Redirect("~/tr", true);
                }
            }
        }
    }
}