using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using VeritabaniIslemMerkeziBase;

namespace VeritabaniIslemMerkezi
{
    public partial class OdemeTablosuIslemler : OdemeTablosuIslemlerBase
    {
        public OdemeTablosuIslemler() : base() { }

        public OdemeTablosuIslemler(OleDbTransaction tran) : base(tran) { }

        public SurecBilgiModel OdemeDurumGuncelle(OdemeTablosuModel GuncelKayit)
        {
            VTIslem.SetCommandText("UPDATE OdemeTablosu SET Durum = @Durum, OdemeTarihi = @OdemeTarihi, OdemeParametreleri = @OdemeParametreleri WHERE OdemeID = @OdemeID");
            VTIslem.AddWithValue("OdemeDurumu", GuncelKayit.Durum);
            VTIslem.AddWithValue("OdemeTarihi", GuncelKayit.OdemeTarihi);
            VTIslem.AddWithValue("OdemeParametreleri", GuncelKayit.OdemeParametreleri);
            VTIslem.AddWithValue("OdemeID", GuncelKayit.OdemeID);
            return VTIslem.ExecuteNonQuery();
        }

        public SurecBilgiModel HashGuncelleme(OdemeTablosuModel GuncelKayit)
        {
            VTIslem.SetCommandText("UPDATE OdemeTablosu SET TurkLirasiUcret = @TurkLirasiUcret, KurUcret = @KurUcret, Hash = @Hash, GuncellenmeTarihi = @GuncellenmeTarihi WHERE OdemeID = @OdemeID");
            VTIslem.AddWithValue("TurkLirasiUcret", GuncelKayit.TurkLirasiUcret);
            VTIslem.AddWithValue("KurUcret", GuncelKayit.KurUcret);
            VTIslem.AddWithValue("Hash", GuncelKayit.Hash);
            VTIslem.AddWithValue("GuncellenmeTarihi", new BilgiKontrolMerkezi().Simdi());
            VTIslem.AddWithValue("OdemeID", GuncelKayit.OdemeID);
            return VTIslem.ExecuteNonQuery();
        }

        public SurecVeriModel<OdemeTablosuModel> KayitBilgisi(string OdemeID, string DilID)
        {
            int
               OdemeIX = 0,

               OdemeTipiIX = OdemeIX + OdemeTablosuModel.OzellikSayisi,
               OdemeTipiDilIX = OdemeTipiIX + OdemeTipiTablosuModel.OzellikSayisi,

               KatilimciIX = OdemeTipiDilIX + OdemeTipiDilTablosuModel.OzellikSayisi,

               KonaklamaIX = KatilimciIX + KatilimciTablosuModel.OzellikSayisi,
               KatilimciTipiOdaTipiIX = KonaklamaIX + KonaklamaTablosuModel.OzellikSayisi,

               KatilimciTipiIX = KatilimciTipiOdaTipiIX + KatilimciTipiOdaTipiTablosuModel.OzellikSayisi,
               KatilimciTipiDilIX = KatilimciTipiIX + KatilimciTipiTablosuModel.OzellikSayisi,

               OdaTipiIX = KatilimciTipiDilIX + KatilimciTipiDilTablosuModel.OzellikSayisi,
               OdaTipiDilIX = OdaTipiIX + OdaTipiTablosuModel.OzellikSayisi,

               OtelIX = OdaTipiDilIX + OdaTipiDilTablosuModel.OzellikSayisi,
               TransferIX = OtelIX + OtelTablosuModel.OzellikSayisi,
               TransferTipiIX = TransferIX + TransferTablosuModel.OzellikSayisi,
               TransferTipiDilIX = TransferTipiIX + TransferTipiTablosuModel.OzellikSayisi;

            VTIslem.SetCommandText($"SELECT {OdemeTablosuModel.SQLSutunSorgusu}, {OdemeTipiTablosuModel.SQLSutunSorgusu}, {OdemeTipiDilTablosuModel.SQLSutunSorgusu}, {KatilimciTablosuModel.SQLSutunSorgusu}, {KonaklamaTablosuModel.SQLSutunSorgusu},{KatilimciTipiOdaTipiTablosuModel.SQLSutunSorgusu}, {KatilimciTipiTablosuModel.SQLSutunSorgusu}, {KatilimciTipiDilTablosuModel.SQLSutunSorgusu}, {OdaTipiTablosuModel.SQLSutunSorgusu}, {OdaTipiDilTablosuModel.SQLSutunSorgusu}, {OtelTablosuModel.SQLSutunSorgusu}, {TransferTablosuModel.SQLSutunSorgusu}, {TransferTipiTablosuModel.SQLSutunSorgusu}, {TransferTipiDilTablosuModel.SQLSutunSorgusu} FROM ( ( ( ( ( ( ( ( ( ( ( ( OdemeTablosu INNER JOIN OdemeTipiTablosu ON OdemeTablosu.OdemeTipiID = OdemeTipiTablosu.OdemeTipiID ) INNER JOIN OdemeTipiDilTablosu ON OdemeTipiTablosu.OdemeTipiID = OdemeTipiDilTablosu.OdemeTipiID ) INNER JOIN KatilimciTablosu ON OdemeTablosu.KatilimciID = KatilimciTablosu.KatilimciID ) INNER JOIN KonaklamaTablosu ON KatilimciTablosu.KatilimciID = KonaklamaTablosu.KatilimciID ) INNER JOIN KatilimciTipiOdaTipiTablosu ON KonaklamaTablosu.KatilimciTipiOdaTipiID = KatilimciTipiOdaTipiTablosu.KatilimciTipiOdaTipiID ) INNER JOIN KatilimciTipiTablosu ON KatilimciTipiOdaTipiTablosu.KatilimciTipiID = KatilimciTipiTablosu.KatilimciTipiID ) INNER JOIN KatilimciTipiDilTablosu ON KatilimciTipiTablosu.KatilimciTipiID = KatilimciTipiDilTablosu.KatilimciTipiID ) INNER JOIN OdaTipiTablosu ON KatilimciTipiOdaTipiTablosu.OdaTipiID = OdaTipiTablosu.OdaTipiID ) INNER JOIN OdaTipiDilTablosu ON OdaTipiDilTablosu.OdaTipiID = OdaTipiTablosu.OdaTipiID ) INNER JOIN OtelTablosu ON OdaTipiTablosu.OtelID = OtelTablosu.OtelID ) INNER JOIN TransferTablosu ON KatilimciTablosu.KatilimciID = TransferTablosu.KatilimciID ) INNER JOIN TransferTipiTablosu ON TransferTipiTablosu.TransferTipiID = TransferTablosu.TransferTipiID ) INNER JOIN TransferTipiDilTablosu ON TransferTipiDilTablosu.TransferTipiID = TransferTipiTablosu.TransferTipiID WHERE OdemeID = @OdemeID AND OdemeTipiDilTablosu.DilID = @DilID AND KatilimciTipiDilTablosu.DilID = OdemeTipiDilTablosu.DilID AND OdaTipiDilTablosu.DilID = KatilimciTipiDilTablosu.DilID AND TransferTipiDilTablosu.DilID = OdaTipiDilTablosu.DilID");
            VTIslem.AddWithValue("OdemeID", OdemeID);
            VTIslem.AddWithValue("DilID", DilID);
            VTIslem.OpenConnection();
            SModel = VTIslem.ExecuteReader(CommandBehavior.SingleResult);
            if (SModel.Sonuc.Equals(Sonuclar.Basarili))
            {
                while (SModel.Reader.Read())
                {
                    if (KayitBilgisiAl(OdemeIX, SModel.Reader).Sonuc.Equals(Sonuclar.Basarili))
                    {
                        SDataModel.Veriler.OdemeTipiBilgisi = new OdemeTipiTablosuIslemler().KayitBilgisiAl(OdemeTipiIX, SModel.Reader).Veriler;
                        SDataModel.Veriler.OdemeTipiBilgisi.OdemeTipiDilBilgisi = new List<OdemeTipiDilTablosuModel> { new OdemeTipiDilTablosuIslemler().KayitBilgisiAl(OdemeTipiDilIX, SModel.Reader).Veriler };
                        SDataModel.Veriler.KatilimciBilgisi = new KatilimciTablosuIslemler().KayitBilgisiAl(KatilimciIX, SModel.Reader).Veriler;
                        SDataModel.Veriler.KatilimciBilgisi.KonaklamaBilgisi = new KonaklamaTablosuIslemler().KayitBilgisiAl(KonaklamaIX, SModel.Reader).Veriler;
                        SDataModel.Veriler.KatilimciBilgisi.KonaklamaBilgisi.KatilimciTipiOdaTipiBilgisi = new KatilimciTipiOdaTipiTablosuIslemler().KayitBilgisiAl(KatilimciTipiOdaTipiIX, SModel.Reader).Veriler;
                        SDataModel.Veriler.KatilimciBilgisi.KonaklamaBilgisi.KatilimciTipiOdaTipiBilgisi.KatilimciTipiBilgisi = new KatilimciTipiTablosuIslemler().KayitBilgisiAl(KatilimciTipiIX, SModel.Reader).Veriler;
                        SDataModel.Veriler.KatilimciBilgisi.KonaklamaBilgisi.KatilimciTipiOdaTipiBilgisi.KatilimciTipiBilgisi.KatilimciTipiDilBilgisi = new List<KatilimciTipiDilTablosuModel> { new KatilimciTipiDilTablosuIslemler().KayitBilgisiAl(KatilimciTipiDilIX, SModel.Reader).Veriler };
                        SDataModel.Veriler.KatilimciBilgisi.KonaklamaBilgisi.KatilimciTipiOdaTipiBilgisi.OdaTipiBilgisi = new OdaTipiTablosuIslemler().KayitBilgisiAl(OdaTipiIX, SModel.Reader).Veriler;
                        SDataModel.Veriler.KatilimciBilgisi.KonaklamaBilgisi.KatilimciTipiOdaTipiBilgisi.OdaTipiBilgisi.OdaTipiDilBilgisi = new List<OdaTipiDilTablosuModel> { new OdaTipiDilTablosuIslemler().KayitBilgisiAl(OdaTipiDilIX, SModel.Reader).Veriler };
                        SDataModel.Veriler.KatilimciBilgisi.KonaklamaBilgisi.KatilimciTipiOdaTipiBilgisi.OdaTipiBilgisi.OtelBilgisi = new OtelTablosuIslemler().KayitBilgisiAl(OtelIX, SModel.Reader).Veriler;
                        SDataModel.Veriler.KatilimciBilgisi.TransferBilgisi = new TransferTablosuIslemler().KayitBilgisiAl(TransferIX, SModel.Reader).Veriler;
                        SDataModel.Veriler.KatilimciBilgisi.TransferBilgisi.TransferTipiBilgisi = new TransferTipiTablosuIslemler().KayitBilgisiAl(TransferTipiIX, SModel.Reader).Veriler;
                        SDataModel.Veriler.KatilimciBilgisi.TransferBilgisi.TransferTipiBilgisi.TransferTipiDilBilgisi = new List<TransferTipiDilTablosuModel> { new TransferTipiDilTablosuIslemler().KayitBilgisiAl(TransferTipiDilIX, SModel.Reader).Veriler };

                        SDataModel.Veriler.KatilimciBilgisi.KatilimciKursBilgisi = new KatilimciKursTablosuIslemler().KatilimciBilgileri(SDataModel.Veriler.KatilimciID, DilID).Veriler;
                        SDataModel.Veriler.KatilimciBilgisi.KatilimciEtkinlikBilgisi = new KatilimciEtkinlikTablosuIslemler().KatilimciBilgileri(SDataModel.Veriler.KatilimciID, DilID).Veriler;
                    }
                }
                if (SDataModel is null)
                {
                    SDataModel = new SurecVeriModel<OdemeTablosuModel>
                    {
                        Sonuc = Sonuclar.VeriBulunamadi,
                        KullaniciMesaji = "Belirtilen kayýt bulunamamýþtýr",
                        HataBilgi = new HataBilgileri
                        {
                            HataAlinanKayitID = 0,
                            HataKodu = 0,
                            HataMesaji = "Belirtilen kayýt bulunamamýþtýr"
                        }
                    };
                }
            }
            else
            {
                SDataModel = new SurecVeriModel<OdemeTablosuModel>
                {
                    Sonuc = SModel.Sonuc,
                    KullaniciMesaji = SModel.KullaniciMesaji,
                    HataBilgi = SModel.HataBilgi
                };
            }
            VTIslem.CloseConnection();
            return SDataModel;
        }

        public SurecVeriModel<DataTable> Rapor(string DilID)
        {
            SurecVeriModel<DataTable> SDataRapor = new SurecVeriModel<DataTable>();
            int
               OdemeIX = 0,
               OdemeTipiIX = OdemeIX + OdemeTablosuModel.OzellikSayisi,
               OdemeTipiDilIX = OdemeTipiIX + OdemeTipiTablosuModel.OzellikSayisi,
               KatilimciIX = OdemeTipiDilIX + OdemeTipiDilTablosuModel.OzellikSayisi,
               KonaklamaIX = KatilimciIX + KatilimciTablosuModel.OzellikSayisi,
               KatilimciTipiOdaTipiIX = KonaklamaIX + KonaklamaTablosuModel.OzellikSayisi,
               KatilimciTipiIX = KatilimciTipiOdaTipiIX + KatilimciTipiOdaTipiTablosuModel.OzellikSayisi,
               KatilimciTipiDilIX = KatilimciTipiIX + KatilimciTipiTablosuModel.OzellikSayisi,
               OdaTipiIX = KatilimciTipiDilIX + KatilimciTipiDilTablosuModel.OzellikSayisi,
               OdaTipiDilIX = OdaTipiIX + OdaTipiTablosuModel.OzellikSayisi,
               OtelIX = OdaTipiDilIX + OdaTipiDilTablosuModel.OzellikSayisi,
               TransferIX = OtelIX + OtelTablosuModel.OzellikSayisi,
               TransferTipiIX = TransferIX + TransferTablosuModel.OzellikSayisi,
               TransferTipiDilIX = TransferTipiIX + TransferTipiTablosuModel.OzellikSayisi,
               KatilimciKursIX = TransferTipiDilIX + TransferTipiDilTablosuModel.OzellikSayisi,
               KursTipiIX = KatilimciKursIX + KatilimciKursTablosuModel.OzellikSayisi,
               KursTipiDilIX = KursTipiIX + KursTipiTablosuModel.OzellikSayisi,
               KatilimciEtkinlikIX = KursTipiDilIX + KursTipiDilTablosuModel.OzellikSayisi,
               EtkinlikIX = KatilimciEtkinlikIX + KatilimciEtkinlikTablosuModel.OzellikSayisi,
               EtkinlikDilIX = EtkinlikIX + EtkinlikTablosuModel.OzellikSayisi;

            VTIslem.SetCommandText($"SELECT {OdemeTablosuModel.SQLSutunSorgusu}, {OdemeTipiTablosuModel.SQLSutunSorgusu}, {OdemeTipiDilTablosuModel.SQLSutunSorgusu}, {KatilimciTablosuModel.SQLSutunSorgusu}, {KonaklamaTablosuModel.SQLSutunSorgusu}, {KatilimciTipiOdaTipiTablosuModel.SQLSutunSorgusu}, {KatilimciTipiTablosuModel.SQLSutunSorgusu}, {KatilimciTipiDilTablosuModel.SQLSutunSorgusu}, {OdaTipiTablosuModel.SQLSutunSorgusu}, {OdaTipiDilTablosuModel.SQLSutunSorgusu}, {OtelTablosuModel.SQLSutunSorgusu}, {TransferTablosuModel.SQLSutunSorgusu}, {TransferTipiTablosuModel.SQLSutunSorgusu}, {TransferTipiDilTablosuModel.SQLSutunSorgusu}, {KatilimciKursTablosuModel.SQLSutunSorgusu}, {KursTipiTablosuModel.SQLSutunSorgusu}, {KursTipiDilTablosuModel.SQLSutunSorgusu}, {KatilimciEtkinlikTablosuModel.SQLSutunSorgusu}, {EtkinlikTablosuModel.SQLSutunSorgusu}, {EtkinlikDilTablosuModel.SQLSutunSorgusu} FROM ( ( ( ( ( ( ( ( ( ( ( ( ( ( ( ( ( ( OdemeTablosu INNER JOIN OdemeTipiTablosu ON OdemeTablosu.OdemeTipiID = OdemeTipiTablosu.OdemeTipiID ) INNER JOIN OdemeTipiDilTablosu ON OdemeTipiTablosu.OdemeTipiID = OdemeTipiDilTablosu.OdemeTipiID ) INNER JOIN KatilimciTablosu ON OdemeTablosu.KatilimciID = KatilimciTablosu.KatilimciID ) INNER JOIN KonaklamaTablosu ON KatilimciTablosu.KatilimciID = KonaklamaTablosu.KatilimciID ) INNER JOIN KatilimciTipiOdaTipiTablosu ON KonaklamaTablosu.KatilimciTipiOdaTipiID = KatilimciTipiOdaTipiTablosu.KatilimciTipiOdaTipiID ) INNER JOIN KatilimciTipiTablosu ON KatilimciTipiOdaTipiTablosu.KatilimciTipiID = KatilimciTipiTablosu.KatilimciTipiID ) INNER JOIN KatilimciTipiDilTablosu ON KatilimciTipiTablosu.KatilimciTipiID = KatilimciTipiDilTablosu.KatilimciTipiID ) INNER JOIN OdaTipiTablosu ON KatilimciTipiOdaTipiTablosu.OdaTipiID = OdaTipiTablosu.OdaTipiID ) INNER JOIN OdaTipiDilTablosu ON OdaTipiTablosu.OdaTipiID = OdaTipiDilTablosu.OdaTipiID ) INNER JOIN OtelTablosu ON OdaTipiTablosu.OtelID = OtelTablosu.OtelID ) INNER JOIN TransferTablosu ON KatilimciTablosu.KatilimciID = TransferTablosu.KatilimciID ) INNER JOIN TransferTipiTablosu ON TransferTipiTablosu.TransferTipiID = TransferTablosu.TransferTipiID ) INNER JOIN TransferTipiDilTablosu ON TransferTipiTablosu.TransferTipiID = TransferTipiDilTablosu.TransferTipiID ) LEFT JOIN KatilimciKursTablosu ON KatilimciKursTablosu.KatilimciID = KatilimciTablosu.KatilimciID ) LEFT JOIN KursTipiTablosu ON KatilimciKursTablosu.KursTipiID = KursTipiTablosu.KursTipiID ) LEFT JOIN KursTipiDilTablosu ON KursTipiDilTablosu.KursTipiID = KursTipiTablosu.KursTipiID ) LEFT JOIN KatilimciEtkinlikTablosu ON KatilimciTablosu.KatilimciID = KatilimciEtkinlikTablosu.KatilimciID ) LEFT JOIN EtkinlikTablosu ON KatilimciEtkinlikTablosu.EtkinlikID = EtkinlikTablosu.EtkinlikID ) LEFT JOIN EtkinlikDilTablosu ON EtkinlikTablosu.EtkinlikID = EtkinlikDilTablosu.EtkinlikID WHERE OdemeTablosu.Durum = @Durum AND OdemeTipiDilTablosu.DilID = @DilID AND KatilimciTipiDilTablosu.DilID = OdemeTipiDilTablosu.DilID AND OdaTipiDilTablosu.DilID = KatilimciTipiDilTablosu.DilID AND TransferTipiDilTablosu.DilID = OdaTipiDilTablosu.DilID AND ( KursTipiDilTablosu.DilID IS NULL OR KursTipiDilTablosu.DilID = TransferTipiDilTablosu.DilID ) AND ( EtkinlikDilTablosu.DilID IS NULL OR EtkinlikDilTablosu.DilID = TransferTipiDilTablosu.DilID ) ORDER BY OdemeTablosu.OdemeTarihi DESC");
            VTIslem.AddWithValue("Durum", true);
            VTIslem.AddWithValue("DilID", DilID);

            VTIslem.OpenConnection();

            SModel = VTIslem.ExecuteReader(CommandBehavior.Default);
            if (SModel.Sonuc.Equals(Sonuclar.Basarili))
            {
                VeriListe = new List<OdemeTablosuModel>();
                while (SModel.Reader.Read())
                {
                    OdemeTablosuModel OModel = VeriListe.FirstOrDefault(x => x.OdemeID.Equals(SModel.Reader.GetString(OdemeIX)));
                    if (OModel is null)
                    {
                        if (KayitBilgisiAl(OdemeIX, SModel.Reader).Sonuc.Equals(Sonuclar.Basarili))
                        {
                            OModel = SDataModel.Veriler;
                            OModel.OdemeTipiBilgisi = new OdemeTipiTablosuIslemler().KayitBilgisiAl(OdemeTipiIX, SModel.Reader).Veriler;
                            OModel.OdemeTipiBilgisi.OdemeTipiDilBilgisi = new List<OdemeTipiDilTablosuModel> { new OdemeTipiDilTablosuIslemler().KayitBilgisiAl(OdemeTipiDilIX, SModel.Reader).Veriler };
                            OModel.KatilimciBilgisi = new KatilimciTablosuIslemler().KayitBilgisiAl(KatilimciIX, SModel.Reader).Veriler;
                            OModel.KatilimciBilgisi.KonaklamaBilgisi = new KonaklamaTablosuIslemler().KayitBilgisiAl(KonaklamaIX, SModel.Reader).Veriler;
                            OModel.KatilimciBilgisi.KonaklamaBilgisi.KatilimciTipiOdaTipiBilgisi = new KatilimciTipiOdaTipiTablosuIslemler().KayitBilgisiAl(KatilimciTipiOdaTipiIX, SModel.Reader).Veriler;
                            OModel.KatilimciBilgisi.KonaklamaBilgisi.KatilimciTipiOdaTipiBilgisi.KatilimciTipiBilgisi = new KatilimciTipiTablosuIslemler().KayitBilgisiAl(KatilimciTipiIX, SModel.Reader).Veriler;
                            OModel.KatilimciBilgisi.KonaklamaBilgisi.KatilimciTipiOdaTipiBilgisi.KatilimciTipiBilgisi.KatilimciTipiDilBilgisi = new List<KatilimciTipiDilTablosuModel> { new KatilimciTipiDilTablosuIslemler().KayitBilgisiAl(KatilimciTipiDilIX, SModel.Reader).Veriler }; 
                            OModel.KatilimciBilgisi.KonaklamaBilgisi.KatilimciTipiOdaTipiBilgisi.OdaTipiBilgisi = new OdaTipiTablosuIslemler().KayitBilgisiAl(OdaTipiIX, SModel.Reader).Veriler;
                            OModel.KatilimciBilgisi.KonaklamaBilgisi.KatilimciTipiOdaTipiBilgisi.OdaTipiBilgisi.OdaTipiDilBilgisi = new List<OdaTipiDilTablosuModel> { new OdaTipiDilTablosuIslemler().KayitBilgisiAl(OdaTipiDilIX, SModel.Reader).Veriler };
                            OModel.KatilimciBilgisi.KonaklamaBilgisi.KatilimciTipiOdaTipiBilgisi.OdaTipiBilgisi.OtelBilgisi = new OtelTablosuIslemler().KayitBilgisiAl(OtelIX, SModel.Reader).Veriler;
                            OModel.KatilimciBilgisi.TransferBilgisi = new TransferTablosuIslemler().KayitBilgisiAl(TransferIX, SModel.Reader).Veriler;
                            OModel.KatilimciBilgisi.TransferBilgisi.TransferTipiBilgisi = new TransferTipiTablosuIslemler().KayitBilgisiAl(TransferTipiIX, SModel.Reader).Veriler;
                            OModel.KatilimciBilgisi.TransferBilgisi.TransferTipiBilgisi.TransferTipiDilBilgisi = new List<TransferTipiDilTablosuModel> { new TransferTipiDilTablosuIslemler().KayitBilgisiAl(TransferTipiDilIX, SModel.Reader).Veriler };
                            VeriListe.Add(OModel);
                        }
                        else
                        {
                            VTIslem.CloseConnection();
                            return new SurecVeriModel<DataTable>
                            {
                                Sonuc = SDataModel.Sonuc,
                                KullaniciMesaji = SDataModel.KullaniciMesaji,
                                HataBilgi = SDataModel.HataBilgi,
                                Veriler = null
                            };
                        }

                    }

                    if (!SModel.Reader.IsDBNull(KatilimciKursIX))
                    {
                        if (OModel.KatilimciBilgisi.KatilimciKursBilgisi is null)
                            OModel.KatilimciBilgisi.KatilimciKursBilgisi = new List<KatilimciKursTablosuModel>();

                        KatilimciKursTablosuModel KKModel = OModel.KatilimciBilgisi.KatilimciKursBilgisi.FirstOrDefault(x => x.KatilimciKursID.Equals(SModel.Reader.GetString(KatilimciKursIX)));

                        if (KKModel is null)
                        {
                            KKModel = new KatilimciKursTablosuIslemler().KayitBilgisiAl(KatilimciKursIX, SModel.Reader).Veriler;
                            KKModel.KursTipiBilgisi = new KursTipiTablosuIslemler().KayitBilgisiAl(KursTipiIX, SModel.Reader).Veriler;
                            KKModel.KursTipiBilgisi.KursTipiDilBilgisi = new List<KursTipiDilTablosuModel> { new KursTipiDilTablosuIslemler().KayitBilgisiAl(KursTipiDilIX, SModel.Reader).Veriler };
                            OModel.KatilimciBilgisi.KatilimciKursBilgisi.Add(KKModel);
                        }
                    }

                    if (!SModel.Reader.IsDBNull(KatilimciEtkinlikIX))
                    {
                        if (OModel.KatilimciBilgisi.KatilimciEtkinlikBilgisi is null)
                            OModel.KatilimciBilgisi.KatilimciEtkinlikBilgisi = new List<KatilimciEtkinlikTablosuModel>();

                        KatilimciEtkinlikTablosuModel KEModel = OModel.KatilimciBilgisi.KatilimciEtkinlikBilgisi.FirstOrDefault(x => x.KatilimciEtkinlikID.Equals(SModel.Reader.GetString(KatilimciEtkinlikIX)));

                        if (KEModel is null)
                        {
                            KEModel = new KatilimciEtkinlikTablosuIslemler().KayitBilgisiAl(KatilimciEtkinlikIX, SModel.Reader).Veriler;
                            KEModel.EtkinlikBilgisi = new EtkinlikTablosuIslemler().KayitBilgisiAl(EtkinlikIX, SModel.Reader).Veriler;
                            KEModel.EtkinlikBilgisi.EtkinlikDilBilgisi = new List<EtkinlikDilTablosuModel> { new EtkinlikDilTablosuIslemler().KayitBilgisiAl(EtkinlikDilIX, SModel.Reader).Veriler };
                            OModel.KatilimciBilgisi.KatilimciEtkinlikBilgisi.Add(KEModel);
                        }
                    }
                }

                SDataRapor = new SurecVeriModel<DataTable>
                {
                    Sonuc = SModel.Sonuc,
                    KullaniciMesaji = SModel.KullaniciMesaji,
                    HataBilgi = SModel.HataBilgi,
                    Veriler = new DataTable("Rapor")
                };

                SDataRapor.Veriler.Columns.AddRange(new DataColumn[] {
                    new DataColumn("Sipariþ No", Type.GetType("System.String")),
                    new DataColumn("Ad & Soyad", Type.GetType("System.String")),
                    new DataColumn("Kimlik No", Type.GetType("System.String")),
                    new DataColumn("e-Posta", Type.GetType("System.String")),
                    new DataColumn("Cep Telefonu", Type.GetType("System.String")),
                    new DataColumn("Kurum", Type.GetType("System.String")),
                    new DataColumn("Katýlýmcý Tipi", Type.GetType("System.String")),
                    new DataColumn("Otel", Type.GetType("System.String")),
                    new DataColumn("Oda Tipi", Type.GetType("System.String")),
                    new DataColumn("Giriþ Tarihi", Type.GetType("System.String")),
                    new DataColumn("Çýkýþ Tarihi", Type.GetType("System.String")),
                    new DataColumn("Refakatçi", Type.GetType("System.String")),
                    new DataColumn("Fatura Ünvan", Type.GetType("System.String")),
                    new DataColumn("Fatura Adresi", Type.GetType("System.String")),
                    new DataColumn("Vergi Dairesi", Type.GetType("System.String")),
                    new DataColumn("Vergi No", Type.GetType("System.String")),
                    new DataColumn("Ödeme Tipi", Type.GetType("System.String")),
                    new DataColumn("Ücret", Type.GetType("System.String")),
                    new DataColumn("Kayýt Tarihi", Type.GetType("System.String"))
                });

                foreach (OdemeTablosuModel Item in VeriListe)
                {
                    SDataRapor.Veriler.Rows.Add(Item.DRow(SDataRapor.Veriler.NewRow()));
                }
            }
            else
            {
                SDataRapor = new SurecVeriModel<DataTable>
                {
                    Sonuc = SModel.Sonuc,
                    KullaniciMesaji = SModel.KullaniciMesaji,
                    HataBilgi = SModel.HataBilgi,
                    Veriler = null
                };
            }
            VTIslem.CloseConnection();
            return SDataRapor;
        }
    }
}
