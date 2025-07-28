using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using VeritabaniIslemMerkeziBase;

namespace VeritabaniIslemMerkezi
{
    public partial class KatilimciEtkinlikTablosuIslemler : KatilimciEtkinlikTablosuIslemlerBase
    {
        public KatilimciEtkinlikTablosuIslemler() : base() { }

        public KatilimciEtkinlikTablosuIslemler(OleDbTransaction tran) : base(tran) { }

        public SurecVeriModel<IList<KatilimciEtkinlikTablosuModel>> KatilimciBilgileri(string KatilimciID, string DilID)
        {
            int
                KatilimciEtkinlikIX = 0,
                EtkinlikIX = KatilimciEtkinlikIX + KatilimciEtkinlikTablosuModel.OzellikSayisi,
                EtkinlikDilIX = EtkinlikIX + EtkinlikTablosuModel.OzellikSayisi;

            VTIslem.SetCommandText($"SELECT {KatilimciEtkinlikTablosuModel.SQLSutunSorgusu}, {EtkinlikTablosuModel.SQLSutunSorgusu}, {EtkinlikDilTablosuModel.SQLSutunSorgusu} FROM ( KatilimciEtkinlikTablosu INNER JOIN EtkinlikTablosu ON KatilimciEtkinlikTablosu.EtkinlikID = EtkinlikTablosu.EtkinlikID ) INNER JOIN EtkinlikDilTablosu ON EtkinlikDilTablosu.EtkinlikID = EtkinlikTablosu.EtkinlikID WHERE KatilimciID=@KatilimciID AND EtkinlikDilTablosu.DilID = @DilID ORDER BY EtkinlikTablosu.Sira");
            VTIslem.AddWithValue("KatilimciID", KatilimciID);
            VTIslem.AddWithValue("DilID", DilID);
            VTIslem.OpenConnection();
            SModel = VTIslem.ExecuteReader(CommandBehavior.Default);
            if (SModel.Sonuc.Equals(Sonuclar.Basarili))
            {
                VeriListe = new List<KatilimciEtkinlikTablosuModel>();
                while (SModel.Reader.Read())
                {
                    if (KayitBilgisiAl(KatilimciEtkinlikIX, SModel.Reader).Sonuc.Equals(Sonuclar.Basarili))
                    {
                        SDataModel.Veriler.EtkinlikBilgisi = new EtkinlikTablosuIslemler().KayitBilgisiAl(EtkinlikIX, SModel.Reader).Veriler;
                        SDataModel.Veriler.EtkinlikBilgisi.EtkinlikDilBilgisi = new List<EtkinlikDilTablosuModel> { new EtkinlikDilTablosuIslemler().KayitBilgisiAl(EtkinlikDilIX, SModel.Reader).Veriler };
                        VeriListe.Add(SDataModel.Veriler);
                    }
                    else
                    {
                        SDataListModel = new SurecVeriModel<IList<KatilimciEtkinlikTablosuModel>>
                        {
                            Sonuc = SDataModel.Sonuc,
                            KullaniciMesaji = SDataModel.KullaniciMesaji,
                            HataBilgi = SDataModel.HataBilgi
                        };
                        VTIslem.CloseConnection();
                        return SDataListModel;
                    }
                }
                SDataListModel = new SurecVeriModel<IList<KatilimciEtkinlikTablosuModel>>
                {
                    Sonuc = Sonuclar.Basarili,
                    KullaniciMesaji = "Veri listesi baþarýyla çekildi",
                    Veriler = VeriListe
                };
            }
            else
            {
                SDataListModel = new SurecVeriModel<IList<KatilimciEtkinlikTablosuModel>>
                {
                    Sonuc = SModel.Sonuc,
                    KullaniciMesaji = SModel.KullaniciMesaji,
                    HataBilgi = SModel.HataBilgi
                };
            }
            VTIslem.CloseConnection();
            return SDataListModel;
        }
    }
}
