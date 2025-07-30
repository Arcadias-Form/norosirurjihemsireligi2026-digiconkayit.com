using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using VeritabaniIslemMerkezi;
using VeritabaniIslemMerkezi.Access;

namespace Norosirurjihemsireligi2026_Web.tr
{
    public partial class Default : Page
    {
        string TemelUcret = $"0,00 {Sabitler.KurSimgesi}";

        ListItem selectItem = new ListItem("Seçiniz", string.Empty);
        StringBuilder Uyarilar = new StringBuilder();
        BilgiKontrolMerkezi Kontrol = new BilgiKontrolMerkezi();

        SurecBilgiModel SModel;
        SurecVeriModel<KatilimciTipiOdaTipiTablosuModel> SDataKatilimciTipiOdaTipiModel;
        SurecVeriModel<TransferTipiTablosuModel> SDataTransferTipiModel;
        SurecVeriModel<IList<KursTipiTablosuModel>> SDataKursTipiListModel;
        SurecVeriModel<IList<EtkinlikTablosuModel>> SDataEtkinlikListModel;

        KatilimciTablosuModel KModel;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlKatilimciTipi.DataBind();
                ddlKatilimciTipi.Items.Insert(0, selectItem);

                ddlOtel.DataBind();
                ddlOtel.Items.Insert(0, selectItem);

                ddlKatilimciTipiOdaTipi.DataBind();
                ddlKatilimciTipiOdaTipi.Items.Insert(0, selectItem);

                ddlTransferTipi.DataBind();
                ddlTransferTipi.Items.Insert(0, selectItem);

                rptKursListesi.DataBind();
                fld_Kurs.Visible = !rptKursListesi.Items.Count.Equals(0);
                tr_KursUcret.Visible = fld_Kurs.Visible;

                rptEtkinlikListesi.DataBind();
                fld_Etkinlik.Visible = !rptEtkinlikListesi.Items.Count.Equals(0);
                tr_EtkinlikUcret.Visible = fld_Etkinlik.Visible;

                ddlOdemeTipi.DataBind();
                ddlOdemeTipi.Items.Insert(0, selectItem);

                lblKatilimciTipiOdaTipiUcret.Text = TemelUcret;
                hfKatilimciTipiOdaTipiUcret.Value = TemelUcret;

                lblTransferUcret.Text = TemelUcret;
                hfTransferUcret.Value = TemelUcret;

                lblKursUcret.Text = TemelUcret;
                hfKursUcret.Value = TemelUcret;

                lblEtkinlikUcret.Text = TemelUcret;
                hfEtkinlikUcret.Value = TemelUcret;

                lblToplamUcret.Text = TemelUcret;
                hfToplamUcret.Value = TemelUcret;
            }
        }

        protected void ddlKatilimciTipi_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlOtel.DataBind();
            ddlOtel.Items.Insert(0, selectItem);
            ddlOtel_SelectedIndexChanged(sender, e);

            tr_Konaklama.Visible = int.TryParse(ddlKatilimciTipi.SelectedValue, out int KatilimciTipiID);

            if ((sender as Control).ID.Equals("ddlKatilimciTipi"))
            {
                FiyatHesaplama();
            }
        }

        protected void ddlOtel_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlKatilimciTipiOdaTipi.DataBind();
            ddlKatilimciTipiOdaTipi.Items.Insert(0, selectItem);

            ddlKatilimciTpiOdaTipi_SelectedIndexChanged(sender, e);

            tr_OdaTipi.Visible = !string.IsNullOrEmpty(ddlOtel.SelectedValue);

            if ((sender as Control).ID.Equals("ddlOtel"))
            {
                FiyatHesaplama();
            }
        }

        protected void ddlKatilimciTpiOdaTipi_SelectedIndexChanged(object sender, EventArgs e)
        {
            Kontrol.Temizle(txtGirisTarihi);
            Kontrol.Temizle(txtCikisTarihi);
            Kontrol.Temizle(txtRefakatci);

            tr_GirisTarihi.Visible = false;
            tr_CikisTarihi.Visible = false;
            tr_Refakatci.Visible = false;

            if (int.TryParse(ddlKatilimciTipiOdaTipi.SelectedValue, out int KatilimciTipiOdaTipiID))
            {
                if (SDataKatilimciTipiOdaTipiModel is null)
                    SDataKatilimciTipiOdaTipiModel = new KatilimciTipiOdaTipiTablosuIslemler().KayitBilgisi(KatilimciTipiOdaTipiID);


                txtGirisTarihi.Enabled = SDataKatilimciTipiOdaTipiModel.Veriler.OdaTipiBilgisi.TarihSecim;
                txtCikisTarihi.Enabled = txtGirisTarihi.Enabled;

                if (txtGirisTarihi.Enabled)
                {
                    BilgiKontrolMerkezi.UyariEkrani(this, $"datePickerOption.startDate = new Date({SDataKatilimciTipiOdaTipiModel.Veriler.OdaTipiBilgisi.BaslangicTarihi.Year}, {SDataKatilimciTipiOdaTipiModel.Veriler.OdaTipiBilgisi.BaslangicTarihi.Month - 1}, {SDataKatilimciTipiOdaTipiModel.Veriler.OdaTipiBilgisi.BaslangicTarihi.Day}); datePickerOption.endDate = new Date({SDataKatilimciTipiOdaTipiModel.Veriler.OdaTipiBilgisi.BitisTarihi.Year}, {SDataKatilimciTipiOdaTipiModel.Veriler.OdaTipiBilgisi.BitisTarihi.Month - 1}, {SDataKatilimciTipiOdaTipiModel.Veriler.OdaTipiBilgisi.BitisTarihi.Day});", false);
                }
                else
                {
                    txtGirisTarihi.Text = SDataKatilimciTipiOdaTipiModel.Veriler.OdaTipiBilgisi.BaslangicTarihi.ToShortDateString();
                    txtCikisTarihi.Text = SDataKatilimciTipiOdaTipiModel.Veriler.OdaTipiBilgisi.BitisTarihi.ToShortDateString();

                }

                tr_GirisTarihi.Visible = true;
                tr_CikisTarihi.Visible = true;
                tr_Refakatci.Visible = SDataKatilimciTipiOdaTipiModel.Veriler.OdaTipiBilgisi.RefakatciDurum;
            }

            if ((sender as Control).ID.Equals("ddlKatilimciTipiOdaTipi"))
            {
                FiyatHesaplama();
            }
        }

        protected void rptIcerikListesi_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            HiddenField hf = e.Item.FindControl("hfIcerikSecim") as HiddenField;
            ImageButton imgbtn = e.Item.FindControl("imgbtn") as ImageButton;

            if (Convert.ToBoolean(hf.Value))
            {
                hf.Value = "false";
                imgbtn.ImageUrl = "~/Gorseller/checkBox.png";
            }
            else
            {
                hf.Value = "true";
                imgbtn.ImageUrl = "~/Gorseller/checkBox_checked.png";
            }

            FiyatHesaplama();
        }

        protected void ddlOdemeTipi_SelectedIndexChanged(object sender, EventArgs e)
        {
            PnlBankaBilgisi.Visible = ddlOdemeTipi.SelectedValue.Equals("1");
        }

        protected void lnkbtnKayitOl_Click(object sender, EventArgs e)
        {
            string KatilimciID = new KatilimciTablosuIslemler().YeniKatilimciID();

            KModel = new KatilimciTablosuModel
            {
                KatilimciID = KatilimciID,
                DilID = "tr",
                AdSoyad = Kontrol.KelimeKontrol(txtAdSoyad, "Ad & Soyad boş bırakılamaz.", ref Uyarilar),
                Cinsiyet = Kontrol.KelimeKontrol(ddlCinsiyet, "Cinsiyet seçiniz", ref Uyarilar),
                ePosta = Kontrol.ePostaKontrol(txtePosta, "e-Posta boş bırakılamaz", "Geçersiz eposta adresi girdiniz", ref Uyarilar),
                CepTelefonu = Kontrol.KelimeKontrol(txtTelefon, "Cep Telefonu boş bırakılamaz", ref Uyarilar),
                Kurum = txtKurum.Text,
                KimlikNo = txtKimlikNo.Text,
                FaturaTipi = Kontrol.KelimeKontrol(ddlFaturaTipi, "Fatura tipini seçiniz", ref Uyarilar),
                FaturaUnvan = Kontrol.KelimeKontrol(txtFaturaUnvan, "Fatura unvan boş bırakılamaz", ref Uyarilar),
                FaturaAdres = Kontrol.KelimeKontrol(txtFaturaAdres, "Fatura adres boş bırakılamaz", ref Uyarilar),
                VergiDairesi = txtVergiDairesi.Text,
                VergiNo = txtVergiNo.Text,
                EklenmeTarihi = Kontrol.Simdi(),
                GuncellenmeTarihi = Kontrol.Simdi(),

                KonaklamaBilgisi = new KonaklamaTablosuModel
                {
                    KatilimciID = KatilimciID,
                    KatilimciTipiOdaTipiID = Kontrol.TamSayiyaKontrol(ddlOtel, "Konaklama seçiniz", "Geçersiz otel seçildi", ref Uyarilar).Equals(0) ? 0 : Kontrol.TamSayiyaKontrol(ddlKatilimciTipiOdaTipi, "Oda Tipi seçiniz", "Geçersiz oda tipi seçildi", ref Uyarilar),
                    GirisTarihi = Kontrol.TariheKontrol(txtGirisTarihi, "Giriş tarihinizi seçiniz", "Geçersiz giriş tarihi seçildi", ref Uyarilar),
                    CikisTarihi = Kontrol.TariheKontrol(txtCikisTarihi, "Cıkış tarihi seçiniz", "Geçersiz çıkış tarihi seçildi", ref Uyarilar),
                    Refakatci = tr_Refakatci.Visible ? Kontrol.KelimeKontrol(txtRefakatci, "Refakatçi boş bırakılamaz", ref Uyarilar) : string.Empty,
                    GuncellenmeTarihi = Kontrol.Simdi(),
                    EklenmeTarihi = Kontrol.Simdi()
                },

                TransferBilgisi = new TransferTablosuModel
                {
                    KatilimciID = KatilimciID,
                    TransferTipiID = 12,
                    GuncellenmeTarihi = Kontrol.Simdi(),
                    EklenmeTarihi = Kontrol.Simdi()
                },

                KatilimciKursBilgisi = rptKursListesi.Items.Cast<RepeaterItem>().Where(x => Convert.ToBoolean((x.FindControl("hfIcerikSecim") as HiddenField).Value)).Select(x => new KatilimciKursTablosuModel { KatilimciKursID = $"{KatilimciID}|{(x.FindControl("imgbtn") as ImageButton).CommandArgument}", KatilimciID = KatilimciID, KursTipiID = int.Parse((x.FindControl("imgbtn") as ImageButton).CommandArgument), GuncellenmeTarihi = Kontrol.Simdi(), EklenmeTarihi = Kontrol.Simdi() }).ToList(),

                KatilimciEtkinlikBilgisi = rptEtkinlikListesi.Items.Cast<RepeaterItem>().Where(x => Convert.ToBoolean((x.FindControl("hfIcerikSecim") as HiddenField).Value)).Select(x => new KatilimciEtkinlikTablosuModel { KatilimciEtkinlikID = $"{KatilimciID}|{(x.FindControl("imgbtn") as ImageButton).CommandArgument}", KatilimciID = KatilimciID, EtkinlikID = int.Parse((x.FindControl("imgbtn") as ImageButton).CommandArgument), GuncellenmeTarihi = Kontrol.Simdi(), EklenmeTarihi = Kontrol.Simdi() }).ToList(),

                OdemeBilgisi = new OdemeTablosuModel
                {
                    OdemeID = $"NRSH{DateTime.Now:yyyyMMddHHmmss}{new Random().Next(10, 99)}",
                    KatilimciID = KatilimciID,
                    OdemeTipiID = Kontrol.TamSayiyaKontrol(ddlOdemeTipi, "Ödeme tipi seçiniz", "Geçersiz ödeme tipi seçildi", ref Uyarilar),
                    Durum = false,
                    OdemeTarihi = null,
                    OdemeParametreleri = "Ödeme cevabı bekleniyor",
                    DovizUcret = hfToplamUcret.Value,
                    KurUcret = $"0,00 ₺",
                    TurkLirasiUcret = $"0,00 ₺",
                    KatilimciTipiOdaTipiUcret = hfKatilimciTipiOdaTipiUcret.Value,
                    TransferUcret = hfTransferUcret.Value,
                    KursUcret = hfKursUcret.Value,
                    EtkinlikUcret = hfEtkinlikUcret.Value,
                    Hash = "Hash hesaplaması bekleniyor",
                    GuncellenmeTarihi = Kontrol.Simdi(),
                    EklenmeTarihi = Kontrol.Simdi()
                }
            };

            if (!KModel.KonaklamaBilgisi.GirisTarihi.Equals(DateTime.MinValue) && !KModel.KonaklamaBilgisi.CikisTarihi.Equals(DateTime.MinValue) && KModel.KonaklamaBilgisi.GirisTarihi >= KModel.KonaklamaBilgisi.CikisTarihi)
            {
                Uyarilar.Append("<p>Giriş tarihi, çıkış tarihine eşit ya da sonra olamaz. Tarihlerinizi kontrol ediniz.</p>");
            }

            if (string.IsNullOrEmpty(Uyarilar.ToString()))
            {
                using (OleDbConnection cnn = ConnectionBuilder.DefaultConnection())
                {
                    ConnectionBuilder.OpenConnection(cnn);
                    using (OleDbTransaction trn = cnn.BeginTransaction())
                    {
                        SModel = new KatilimciTablosuIslemler(trn).YeniKayitEkle(KModel);
                        if (SModel.Sonuc.Equals(Sonuclar.Basarili))
                        {
                            SModel = new KonaklamaTablosuIslemler(trn).YeniKayitEkle(KModel.KonaklamaBilgisi);
                            if (SModel.Sonuc.Equals(Sonuclar.Basarili))
                            {
                                SModel = new TransferTablosuIslemler(trn).YeniKayitEkle(KModel.TransferBilgisi);
                                if (SModel.Sonuc.Equals(Sonuclar.Basarili))
                                {
                                    foreach (KatilimciKursTablosuModel Item in KModel.KatilimciKursBilgisi)
                                    {
                                        SModel = new KatilimciKursTablosuIslemler(trn).YeniKayitEkle(Item);

                                        if (SModel.Sonuc.Equals(Sonuclar.Basarisiz))
                                            break;
                                    }

                                    if (SModel.Sonuc.Equals(Sonuclar.Basarili))
                                    {
                                        foreach (KatilimciEtkinlikTablosuModel Item in KModel.KatilimciEtkinlikBilgisi)
                                        {
                                            SModel = new KatilimciEtkinlikTablosuIslemler(trn).YeniKayitEkle(Item);

                                            if (SModel.Sonuc.Equals(Sonuclar.Basarisiz))
                                                break;
                                        }

                                        if (SModel.Sonuc.Equals(Sonuclar.Basarili))
                                        {
                                            SModel = new OdemeTablosuIslemler(trn).YeniKayitEkle(KModel.OdemeBilgisi);
                                            if (SModel.Sonuc.Equals(Sonuclar.Basarili))
                                            {
                                                trn.Commit();
                                                if (KModel.OdemeBilgisi.OdemeTipiID.Equals(1))
                                                {
                                                    KModel.OdemeBilgisi.Durum = true;
                                                    KModel.OdemeBilgisi.OdemeTarihi = Kontrol.Simdi();
                                                    KModel.OdemeBilgisi.OdemeParametreleri = "Banka Havalesi";

                                                    new OdemeTablosuIslemler().OdemeDurumGuncelle(KModel.OdemeBilgisi);
                                                    new MailGonderimIslemleri().KayitBilgilendirme(KModel.OdemeBilgisi.OdemeID, "tr");

                                                    Response.Redirect($"~/tr/BasariliKayit/{KModel.OdemeBilgisi.OdemeID}");
                                                }
                                                else
                                                {
                                                    Response.Redirect($"~/tr/Odeme/{KModel.OdemeBilgisi.OdemeID}");
                                                }
                                            }
                                            else
                                            {
                                                trn.Rollback();
                                                BilgiKontrolMerkezi.UyariEkrani(this, $"UyariBilgilendirme('', 'Ödeme bilgileriniz kaydedilirken hata meydana geldi. Hata mesajı: {SModel.HataBilgi.HataMesaji}', false);", false);
                                            }
                                        }
                                        else
                                        {
                                            trn.Rollback();
                                            BilgiKontrolMerkezi.UyariEkrani(this, $"UyariBilgilendirme('', 'Etkinlik bilgileriniz kaydedilirken hata meydana geldi. Hata mesajı: {SModel.HataBilgi.HataMesaji}', false);", false);
                                        }
                                    }
                                    else
                                    {
                                        trn.Rollback();
                                        BilgiKontrolMerkezi.UyariEkrani(this, $"UyariBilgilendirme('', 'Kurs bilgileriniz kaydedilirken hata meydana geldi. Hata mesajı: {SModel.HataBilgi.HataMesaji}', false);", false);
                                    }
                                }
                                else
                                {
                                    trn.Rollback();
                                    BilgiKontrolMerkezi.UyariEkrani(this, $"UyariBilgilendirme('', 'Transfer bilgileriniz kaydedilirken hata meydana geldi. Hata mesajı: {SModel.HataBilgi.HataMesaji}', false);", false);
                                }
                            }
                            else
                            {
                                trn.Rollback();
                                BilgiKontrolMerkezi.UyariEkrani(this, $"UyariBilgilendirme('', 'Konaklama bilgileriniz kaydedilirken hata meydana geldi. Hata mesajı: {SModel.HataBilgi.HataMesaji}', false);", false);
                            }
                        }
                        else
                        {
                            trn.Rollback();
                            BilgiKontrolMerkezi.UyariEkrani(this, $"UyariBilgilendirme('', 'Katılımcı bilgileriniz kaydedilirken hata meydana geldi. Hata mesajı: {SModel.HataBilgi.HataMesaji}', false);", false);
                        }
                    }
                    ConnectionBuilder.CloseConnection(cnn);
                }
            }
            else
            {
                BilgiKontrolMerkezi.UyariEkrani(this, $"UyariBilgilendirme('', '{Uyarilar}', false)", false);
            }
        }

        protected void FiyatHesaplama(object sender, EventArgs e)
        {
            FiyatHesaplama();
        }

        void FiyatHesaplama()
        {
            decimal
                Katilimci_Konaklama = 0.00m,
                Transfer = 0.00m,
                Kurs = 0.00m,
                Etkinlik = 0.00m,
                RefakatciCarpani = 1.00m;

            if (int.TryParse(ddlKatilimciTipiOdaTipi.SelectedValue, out int KatilimciTipiOdaTipiID))
            {
                if (SDataKatilimciTipiOdaTipiModel is null)
                    SDataKatilimciTipiOdaTipiModel = new KatilimciTipiOdaTipiTablosuIslemler().KayitBilgisi(KatilimciTipiOdaTipiID);


                if (txtGirisTarihi.Enabled)
                {
                    if (DateTime.TryParse(txtGirisTarihi.Text, out DateTime GirisTarihi) && DateTime.TryParse(txtCikisTarihi.Text, out DateTime CikisTarihi) && GirisTarihi < CikisTarihi)
                    {
                        Katilimci_Konaklama = SDataKatilimciTipiOdaTipiModel.Veriler.FormUcret * (CikisTarihi - GirisTarihi).Days;
                    }
                }
                else
                {
                    Katilimci_Konaklama = SDataKatilimciTipiOdaTipiModel.Veriler.FormUcret;
                }

                if (SDataKatilimciTipiOdaTipiModel.Veriler.OdaTipiBilgisi.RefakatciDurum)
                {
                    RefakatciCarpani = SDataKatilimciTipiOdaTipiModel.Veriler.OdaTipiBilgisi.RefakatciCarpan;
                }
            }

            if (int.TryParse(ddlTransferTipi.SelectedValue, out int TransferTipiID))
            {
                if (SDataTransferTipiModel is null)
                    SDataTransferTipiModel = new TransferTipiTablosuIslemler().KayitBilgisi(TransferTipiID);

                Transfer = SDataTransferTipiModel.Veriler.FormUcret * RefakatciCarpani;
            }

            foreach (int KursTipiID in rptKursListesi.Items.Cast<RepeaterItem>().Where(x => Convert.ToBoolean((x.FindControl("hfIcerikSecim") as HiddenField).Value)).Select(x => Int32.Parse((x.FindControl("imgbtn") as ImageButton).CommandArgument)))
            {
                if (SDataKursTipiListModel is null)
                    SDataKursTipiListModel = new KursTipiTablosuIslemler().KayitBilgileri();

                Kurs += SDataKursTipiListModel.Veriler.First(x => x.KursTipiID.Equals(KursTipiID)).FormUcret;
            }

            foreach (int EtkinlikID in rptEtkinlikListesi.Items.Cast<RepeaterItem>().Where(x => Convert.ToBoolean((x.FindControl("hfIcerikSecim") as HiddenField).Value)).Select(x => Int32.Parse((x.FindControl("imgbtn") as ImageButton).CommandArgument)))
            {
                if (SDataEtkinlikListModel is null)
                    SDataEtkinlikListModel = new EtkinlikTablosuIslemler().KayitBilgileri();

                Etkinlik += SDataEtkinlikListModel.Veriler.First(x => x.EtkinlikID.Equals(EtkinlikID)).FormUcret * RefakatciCarpani;
            }

            lblKatilimciTipiOdaTipiUcret.Text = $"{Katilimci_Konaklama:0.00} {Sabitler.KurSimgesi}";
            hfKatilimciTipiOdaTipiUcret.Value = lblKatilimciTipiOdaTipiUcret.Text;

            lblTransferUcret.Text = $"{Transfer:0.00} {Sabitler.KurSimgesi}";
            hfTransferUcret.Value = lblTransferUcret.Text;

            lblKursUcret.Text = $"{Kurs:0.00} {Sabitler.KurSimgesi}";
            hfKursUcret.Value = lblKursUcret.Text;

            lblEtkinlikUcret.Text = $"{Etkinlik:0.00} {Sabitler.KurSimgesi}";
            hfEtkinlikUcret.Value = lblEtkinlikUcret.Text;

            lblToplamUcret.Text = $"{Katilimci_Konaklama + Transfer + Kurs + Etkinlik:0.00} {Sabitler.KurSimgesi}";
            hfToplamUcret.Value = $"{Katilimci_Konaklama + Transfer + Kurs + Etkinlik:0.00} {Sabitler.KurSimgesi}";
        }
    }
}