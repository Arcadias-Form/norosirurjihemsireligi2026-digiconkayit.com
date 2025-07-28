using Microsoft.AspNet.FriendlyUrls;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using VeritabaniIslemMerkezi;

namespace ArcForm_Web_v2.tr
{
    public partial class OdemeBasarisiz : Page
    {
        IList<string> segment;

        StringBuilder Parametreler = new StringBuilder();
        SurecVeriModel<OdemeTablosuModel> SDataModel;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                segment = Request.GetFriendlyUrlSegments();

                if (segment.Count.Equals(1))
                {
                    IEnumerator formValues = Request.Form.GetEnumerator();
                    while (formValues.MoveNext())
                    {
                        Parametreler.Append(formValues.Current.ToString()).Append(" : ").Append(Request.Form.Get(formValues.Current.ToString())).Append(" ///// ");
                    }
                    File.WriteAllText(Server.MapPath($"~/Dosyalar/PaymentLog/Fail/{segment.First()}_{DateTime.Now:yyyy.MM.dd HH.mm.ss}.log"), Parametreler.ToString().Replace(" ///// ", "\r\n"));

                    SDataModel = new OdemeTablosuIslemler().KayitBilgisi(segment.First(), "tr");
                    if (SDataModel.Sonuc.Equals(Sonuclar.Basarili) && !SDataModel.Veriler.Durum && SDataModel.Veriler.OdemeTarihi is null /* && Banka kontrolü */)
                    {
                        SDataModel.Veriler.Durum = false;
                        SDataModel.Veriler.OdemeParametreleri = "";
                        SDataModel.Veriler.OdemeTarihi = new BilgiKontrolMerkezi().Simdi();

                        new OdemeTablosuIslemler().OdemeDurumGuncelle(SDataModel.Veriler);

                        lblAdSoyad.Text = SDataModel.Veriler.KatilimciBilgisi.AdSoyad;
                        lblOdemeID.Text = SDataModel.Veriler.OdemeID;
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