using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using VeritabaniIslemMerkeziBase;

namespace VeritabaniIslemMerkezi
{
	public partial class KatilimciTipiOdaTipiTablosuIslemler : KatilimciTipiOdaTipiTablosuIslemlerBase
	{
		public KatilimciTipiOdaTipiTablosuIslemler() : base() { }

		public KatilimciTipiOdaTipiTablosuIslemler(OleDbTransaction tran) : base (tran) { }

        public override SurecVeriModel<KatilimciTipiOdaTipiTablosuModel> KayitBilgisi(int KatilimciTipiOdaTipiID)
        {
            int
                KatilimciTipiOdaTipiIX = 0,
                OdaTipiIX = KatilimciTipiOdaTipiIX + KatilimciTipiOdaTipiTablosuModel.OzellikSayisi;

            VTIslem.SetCommandText($"SELECT {KatilimciTipiOdaTipiTablosuModel.SQLSutunSorgusu}, {OdaTipiTablosuModel.SQLSutunSorgusu} FROM KatilimciTipiOdaTipiTablosu INNER JOIN OdaTipiTablosu ON KatilimciTipiOdaTipiTablosu.OdaTipiID = OdaTipiTablosu.OdaTipiID WHERE KatilimciTipiOdaTipiID = @KatilimciTipiOdaTipiID");
            VTIslem.AddWithValue("KatilimciTipiOdaTipiID", KatilimciTipiOdaTipiID);
            VTIslem.OpenConnection();
            SModel = VTIslem.ExecuteReader(CommandBehavior.SingleResult);
            if (SModel.Sonuc.Equals(Sonuclar.Basarili))
            {
                while (SModel.Reader.Read())
                {
                    if (KayitBilgisiAl(KatilimciTipiOdaTipiIX, SModel.Reader).Sonuc.Equals(Sonuclar.Basarili))
                    {
                        SDataModel.Veriler.OdaTipiBilgisi = new OdaTipiTablosuIslemler().KayitBilgisiAl(OdaTipiIX, SModel.Reader).Veriler;
                    }
                }
                if (SDataModel is null)
                {
                    SDataModel = new SurecVeriModel<KatilimciTipiOdaTipiTablosuModel>
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
                SDataModel = new SurecVeriModel<KatilimciTipiOdaTipiTablosuModel>
                {
                    Sonuc = SModel.Sonuc,
                    KullaniciMesaji = SModel.KullaniciMesaji,
                    HataBilgi = SModel.HataBilgi
                };
            }
            VTIslem.CloseConnection();
            return SDataModel;
        }
    }
}
