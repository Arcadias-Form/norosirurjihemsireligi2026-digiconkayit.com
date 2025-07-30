using Microsoft.AspNet.FriendlyUrls;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using VeritabaniIslemMerkezi;

namespace Norosirurjihemsireligi2026_Web.en
{
    public partial class RegistrationSuccess : Page
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
                    SDataModel = new OdemeTablosuIslemler().KayitBilgisi(segment.First(), "en");

                    if (SDataModel.Sonuc.Equals(Sonuclar.Basarili) && SDataModel.Veriler.Durum)
                    {
                        lblAdSoyad.Text = SDataModel.Veriler.KatilimciBilgisi.AdSoyad;
                        lblOdemeID.Text = SDataModel.Veriler.OdemeID;
                        PnlKrediKarti.Visible = SDataModel.Veriler.OdemeTipiID.Equals(1);
                        PnlBankaBilgisi.Visible = SDataModel.Veriler.OdemeTipiID.Equals(2);
                    }
                    else
                    {
                        Response.Redirect("~/en", true);
                    }
                }
                else
                {
                    Response.Redirect("~/en", true);
                }
            }
        }
    }
}