using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using VeritabaniIslemMerkeziBase;

namespace VeritabaniIslemMerkezi
{
	public partial class KatilimciKursTablosuIslemler : KatilimciKursTablosuIslemlerBase
	{
		public KatilimciKursTablosuIslemler() : base() { }

		public KatilimciKursTablosuIslemler(OleDbTransaction tran) : base (tran) { }

        public SurecVeriModel<IList<KatilimciKursTablosuModel>> KatilimciBilgileri(string KatilimciID, string DilID)
        {
            int
                KatilimciKursIX = 0,
                KursTipiIX = KatilimciKursIX + KatilimciKursTablosuModel.OzellikSayisi,
                KursTipiDilIX = KursTipiIX + KursTipiTablosuModel.OzellikSayisi;

            VTIslem.SetCommandText($"SELECT {KatilimciKursTablosuModel.SQLSutunSorgusu}, {KursTipiTablosuModel.SQLSutunSorgusu}, {KursTipiDilTablosuModel.SQLSutunSorgusu} FROM ( KatilimciKursTablosu INNER JOIN KursTipiTablosu ON KatilimciKursTablosu.KursTipiID = KursTipiTablosu.KursTipiID ) INNER JOIN KursTipiDilTablosu ON KursTipiTablosu.KursTipiID = KursTipiDilTablosu.KursTipiID WHERE KatilimciID=@KatilimciID AND KursTipiDilTablosu.DilID = @DilID ORDER BY KursTipiTablosu.Sira");
            VTIslem.AddWithValue("KatilimciID", KatilimciID);
            VTIslem.AddWithValue("DilID", DilID);
            VTIslem.OpenConnection();
            SModel = VTIslem.ExecuteReader(CommandBehavior.Default);
            if (SModel.Sonuc.Equals(Sonuclar.Basarili))
            {
                VeriListe = new List<KatilimciKursTablosuModel>();
                while (SModel.Reader.Read())
                {
                    if (KayitBilgisiAl(KatilimciKursIX, SModel.Reader).Sonuc.Equals(Sonuclar.Basarili))
                    {
                        SDataModel.Veriler.KursTipiBilgisi = new KursTipiTablosuIslemler().KayitBilgisiAl(KursTipiIX, SModel.Reader).Veriler;
                        SDataModel.Veriler.KursTipiBilgisi.KursTipiDilBilgisi = new List<KursTipiDilTablosuModel> { new KursTipiDilTablosuIslemler().KayitBilgisiAl(KursTipiDilIX, SModel.Reader).Veriler };
                        VeriListe.Add(SDataModel.Veriler);
                    }
                    else
                    {
                        SDataListModel = new SurecVeriModel<IList<KatilimciKursTablosuModel>>
                        {
                            Sonuc = SDataModel.Sonuc,
                            KullaniciMesaji = SDataModel.KullaniciMesaji,
                            HataBilgi = SDataModel.HataBilgi
                        };
                        VTIslem.CloseConnection();
                        return SDataListModel;
                    }
                }
                SDataListModel = new SurecVeriModel<IList<KatilimciKursTablosuModel>>
                {
                    Sonuc = Sonuclar.Basarili,
                    KullaniciMesaji = "Veri listesi baþarýyla çekildi",
                    Veriler = VeriListe
                };
            }
            else
            {
                SDataListModel = new SurecVeriModel<IList<KatilimciKursTablosuModel>>
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
