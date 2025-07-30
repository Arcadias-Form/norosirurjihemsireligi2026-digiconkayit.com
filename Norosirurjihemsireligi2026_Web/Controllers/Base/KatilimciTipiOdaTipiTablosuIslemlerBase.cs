using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using VeritabaniIslemSaglayici.Access;

namespace VeritabaniIslemMerkeziBase
{
	public abstract class KatilimciTipiOdaTipiTablosuIslemlerBase
	{
		public VTOperatorleri VTIslem;

		public List<KatilimciTipiOdaTipiTablosuModel> VeriListe;

		public SurecBilgiModel SModel;
		public SurecVeriModel<KatilimciTipiOdaTipiTablosuModel> SDataModel;
		public SurecVeriModel<IList<KatilimciTipiOdaTipiTablosuModel>> SDataListModel;

		public KatilimciTipiOdaTipiTablosuIslemlerBase()
		{
			VTIslem = new VTOperatorleri();
		}

		public KatilimciTipiOdaTipiTablosuIslemlerBase(OleDbTransaction Transcation)
		{
			VTIslem = new VTOperatorleri(Transcation);
		}

		public virtual SurecBilgiModel YeniKayitEkle(KatilimciTipiOdaTipiTablosuModel YeniKayit)
		{
			VTIslem.SetCommandText("INSERT INTO KatilimciTipiOdaTipiTablosu (KatilimciTipiID, OdaTipiID, CokErkenUcret, ErkenUcret, NormalUcret, GuncellenmeTarihi, EklenmeTarihi) VALUES (@KatilimciTipiID, @OdaTipiID, @CokErkenUcret, @ErkenUcret, @NormalUcret, @GuncellenmeTarihi, @EklenmeTarihi)");
			VTIslem.AddWithValue("KatilimciTipiID", YeniKayit.KatilimciTipiID);
			VTIslem.AddWithValue("OdaTipiID", YeniKayit.OdaTipiID);
			VTIslem.AddWithValue("CokErkenUcret", YeniKayit.CokErkenUcret.ToString().Replace(",", "."));
			VTIslem.AddWithValue("ErkenUcret", YeniKayit.ErkenUcret.ToString().Replace(",", "."));
			VTIslem.AddWithValue("NormalUcret", YeniKayit.NormalUcret.ToString().Replace(",", "."));
			VTIslem.AddWithValue("GuncellenmeTarihi", YeniKayit.GuncellenmeTarihi);
			VTIslem.AddWithValue("EklenmeTarihi", YeniKayit.EklenmeTarihi);
			return VTIslem.ExecuteNonQuery();
		}

		public virtual SurecBilgiModel KayitGuncelle(KatilimciTipiOdaTipiTablosuModel GuncelKayit)
		{
			VTIslem.SetCommandText("UPDATE KatilimciTipiOdaTipiTablosu SET KatilimciTipiID=@KatilimciTipiID, OdaTipiID=@OdaTipiID, CokErkenUcret=@CokErkenUcret, ErkenUcret=@ErkenUcret, NormalUcret=@NormalUcret, GuncellenmeTarihi=@GuncellenmeTarihi, EklenmeTarihi=@EklenmeTarihi WHERE KatilimciTipiOdaTipiID=@KatilimciTipiOdaTipiID");
			VTIslem.AddWithValue("KatilimciTipiID", GuncelKayit.KatilimciTipiID);
			VTIslem.AddWithValue("OdaTipiID", GuncelKayit.OdaTipiID);
			VTIslem.AddWithValue("CokErkenUcret", GuncelKayit.CokErkenUcret.ToString().Replace(",", "."));
			VTIslem.AddWithValue("ErkenUcret", GuncelKayit.ErkenUcret.ToString().Replace(",", "."));
			VTIslem.AddWithValue("NormalUcret", GuncelKayit.NormalUcret.ToString().Replace(",", "."));
			VTIslem.AddWithValue("GuncellenmeTarihi", GuncelKayit.GuncellenmeTarihi);
			VTIslem.AddWithValue("EklenmeTarihi", GuncelKayit.EklenmeTarihi);
			VTIslem.AddWithValue("KatilimciTipiOdaTipiID", GuncelKayit.KatilimciTipiOdaTipiID);
			return VTIslem.ExecuteNonQuery();
		}

		public virtual SurecBilgiModel KayitSil(int KatilimciTipiOdaTipiID)
		{
			VTIslem.SetCommandText("DELETE FROM KatilimciTipiOdaTipiTablosu WHERE KatilimciTipiOdaTipiID=@KatilimciTipiOdaTipiID");
			VTIslem.AddWithValue("KatilimciTipiOdaTipiID", KatilimciTipiOdaTipiID);
			return VTIslem.ExecuteNonQuery();
		}

		public virtual SurecVeriModel<KatilimciTipiOdaTipiTablosuModel> KayitBilgisi(int KatilimciTipiOdaTipiID)
		{
			VTIslem.SetCommandText($"SELECT {KatilimciTipiOdaTipiTablosuModel.SQLSutunSorgusu} FROM KatilimciTipiOdaTipiTablosu WHERE KatilimciTipiOdaTipiID = @KatilimciTipiOdaTipiID");
			VTIslem.AddWithValue("KatilimciTipiOdaTipiID", KatilimciTipiOdaTipiID);
			VTIslem.OpenConnection();
			SModel = VTIslem.ExecuteReader(CommandBehavior.SingleResult);
			if (SModel.Sonuc.Equals(Sonuclar.Basarili))
			{
				while (SModel.Reader.Read())
				{
					KayitBilgisiAl();
				}
				if (SDataModel is null)
				{
					SDataModel = new SurecVeriModel<KatilimciTipiOdaTipiTablosuModel>
					{
						Sonuc = Sonuclar.VeriBulunamadi,
						KullaniciMesaji = "Belirtilen kayıt bulunamamıştır",
						HataBilgi = new HataBilgileri
						{
							HataAlinanKayitID = 0,
							HataKodu = 0,
							HataMesaji = "Belirtilen kayıt bulunamamıştır"
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

		public virtual SurecVeriModel<IList<KatilimciTipiOdaTipiTablosuModel>> KayitBilgileri()
		{
			VTIslem.SetCommandText($"SELECT {KatilimciTipiOdaTipiTablosuModel.SQLSutunSorgusu} FROM KatilimciTipiOdaTipiTablosu");
			VTIslem.OpenConnection();
			SModel = VTIslem.ExecuteReader(CommandBehavior.Default);
			if (SModel.Sonuc.Equals(Sonuclar.Basarili))
			{
				VeriListe = new List<KatilimciTipiOdaTipiTablosuModel>();
				while (SModel.Reader.Read())
				{
					if (KayitBilgisiAl().Sonuc.Equals(Sonuclar.Basarili))
					{
						VeriListe.Add(SDataModel.Veriler);
					}
					else
					{
						SDataListModel = new SurecVeriModel<IList<KatilimciTipiOdaTipiTablosuModel>>{
							Sonuc = SDataModel.Sonuc,
							KullaniciMesaji = SDataModel.KullaniciMesaji,
							HataBilgi = SDataModel.HataBilgi
						};
						VTIslem.CloseConnection();
						return SDataListModel;
					}
				}
				SDataListModel = new SurecVeriModel<IList<KatilimciTipiOdaTipiTablosuModel>>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri listesi başarıyla çekildi",
					Veriler = VeriListe
				};
			}
			else
			{
				SDataListModel = new SurecVeriModel<IList<KatilimciTipiOdaTipiTablosuModel>>{
					Sonuc = SModel.Sonuc,
					KullaniciMesaji = SModel.KullaniciMesaji,
					HataBilgi = SModel.HataBilgi
				};
			}
			VTIslem.CloseConnection();
			return SDataListModel;
		}

		SurecVeriModel<KatilimciTipiOdaTipiTablosuModel> KayitBilgisiAl()
		{
			try
			{
				SDataModel = new SurecVeriModel<KatilimciTipiOdaTipiTablosuModel>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri bilgisi başarıyla çekilmiştir.",
					Veriler = new KatilimciTipiOdaTipiTablosuModel
					{
						KatilimciTipiOdaTipiID = SModel.Reader.GetInt32(0),
						KatilimciTipiID = SModel.Reader.GetInt32(1),
						OdaTipiID = SModel.Reader.GetInt32(2),
						CokErkenUcret = SModel.Reader.GetDecimal(3),
						ErkenUcret = SModel.Reader.GetDecimal(4),
						NormalUcret = SModel.Reader.GetDecimal(5),
						GuncellenmeTarihi = SModel.Reader.GetDateTime(6),
						EklenmeTarihi = SModel.Reader.GetDateTime(7),
					}
				};

			}
			catch (InvalidCastException ex)
			{
				SDataModel = new SurecVeriModel<KatilimciTipiOdaTipiTablosuModel>{
					Sonuc = Sonuclar.Basarisiz,
					KullaniciMesaji = "Veri bilgisi çekilirken hatalı atama yapılmaya çalışıldı",
					HataBilgi = new HataBilgileri{
						HataMesaji = string.Format(@"{0}", ex.Message.Replace("'", "ʼ")),
						HataKodu = ex.HResult,
						HataAlinanKayitID = SModel.Reader.GetValue(0)
					}
				};
			}
			catch (Exception ex)
			{
				SDataModel = new SurecVeriModel<KatilimciTipiOdaTipiTablosuModel>{
					Sonuc = Sonuclar.Basarisiz,
					KullaniciMesaji = "Veri bilgisi çekilirken hatalı atama yapılmaya çalışıldı",
					HataBilgi = new HataBilgileri{
						HataMesaji = string.Format(@"{0}", ex.Message.Replace("'", "ʼ")),
						HataKodu = ex.HResult,
						HataAlinanKayitID = SModel.Reader.GetValue(0)
					}
				};
			}
			return SDataModel;
		}

		public virtual SurecVeriModel<KatilimciTipiOdaTipiTablosuModel> KayitBilgisiAl(int Baslangic, DbDataReader Reader)
		{
			try
			{
				SDataModel = new SurecVeriModel<KatilimciTipiOdaTipiTablosuModel>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri bilgisi başarıyla çekilmiştir.",
					Veriler = new KatilimciTipiOdaTipiTablosuModel{
						KatilimciTipiOdaTipiID = Reader.GetInt32(Baslangic + 0),
						KatilimciTipiID = Reader.GetInt32(Baslangic + 1),
						OdaTipiID = Reader.GetInt32(Baslangic + 2),
						CokErkenUcret = Reader.GetDecimal(Baslangic + 3),
						ErkenUcret = Reader.GetDecimal(Baslangic + 4),
						NormalUcret = Reader.GetDecimal(Baslangic + 5),
						GuncellenmeTarihi = Reader.GetDateTime(Baslangic + 6),
						EklenmeTarihi = Reader.GetDateTime(Baslangic + 7),
					}
				};
			}
			catch (InvalidCastException ex)
			{
				SDataModel = new SurecVeriModel<KatilimciTipiOdaTipiTablosuModel>{
					Sonuc = Sonuclar.Basarisiz,
					KullaniciMesaji = "Veri bilgisi çekilirken hatalı atama yapılmaya çalışıldı",
					HataBilgi = new HataBilgileri{
						HataMesaji = string.Format(@"{0}", ex.Message.Replace("'", "ʼ")),
						HataKodu = ex.HResult,
						HataAlinanKayitID = Reader.GetValue(0)
					}
				};
			}
			catch (Exception ex)
			{
				SDataModel = new SurecVeriModel<KatilimciTipiOdaTipiTablosuModel>{
					Sonuc = Sonuclar.Basarisiz,
					KullaniciMesaji = "Veri bilgisi çekilirken hatalı atama yapılmaya çalışıldı",
						HataBilgi = new HataBilgileri{
						HataMesaji = string.Format(@"{0}", ex.Message.Replace("'", "ʼ")),
						HataKodu = ex.HResult,
						HataAlinanKayitID = Reader.GetValue(0)
					}
				};
			}
			return SDataModel;
		}

		public virtual SurecVeriModel<IList<KatilimciTipiOdaTipiTablosuModel>> KatilimciTipiBilgileri(int KatilimciTipiID)
		{
			VTIslem.SetCommandText($"SELECT {KatilimciTipiOdaTipiTablosuModel.SQLSutunSorgusu} FROM KatilimciTipiOdaTipiTablosu WHERE KatilimciTipiID=@KatilimciTipiID");
			VTIslem.AddWithValue("KatilimciTipiID", KatilimciTipiID);
			VTIslem.OpenConnection();
			SModel = VTIslem.ExecuteReader(CommandBehavior.Default);
			if (SModel.Sonuc.Equals(Sonuclar.Basarili))
			{
				VeriListe = new List<KatilimciTipiOdaTipiTablosuModel>();
				while (SModel.Reader.Read())
				{
					if (KayitBilgisiAl().Sonuc.Equals(Sonuclar.Basarili))
					{
						VeriListe.Add(SDataModel.Veriler);
					}
					else
					{
						SDataListModel = new SurecVeriModel<IList<KatilimciTipiOdaTipiTablosuModel>>{
							Sonuc = SDataModel.Sonuc,
							KullaniciMesaji = SDataModel.KullaniciMesaji,
							HataBilgi = SDataModel.HataBilgi
						};
						VTIslem.CloseConnection();
						return SDataListModel;
					}
				}
				SDataListModel = new SurecVeriModel<IList<KatilimciTipiOdaTipiTablosuModel>>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri listesi başarıyla çekildi",
					Veriler = VeriListe
				};
			}
			else
			{
				SDataListModel = new SurecVeriModel<IList<KatilimciTipiOdaTipiTablosuModel>>{
					Sonuc = SModel.Sonuc,
					KullaniciMesaji = SModel.KullaniciMesaji,
					HataBilgi = SModel.HataBilgi
				};
			}
			VTIslem.CloseConnection();
			return SDataListModel;
		}

		public virtual SurecVeriModel<IList<KatilimciTipiOdaTipiTablosuModel>> OdaTipiBilgileri(int OdaTipiID)
		{
			VTIslem.SetCommandText($"SELECT {KatilimciTipiOdaTipiTablosuModel.SQLSutunSorgusu} FROM KatilimciTipiOdaTipiTablosu WHERE OdaTipiID=@OdaTipiID");
			VTIslem.AddWithValue("OdaTipiID", OdaTipiID);
			VTIslem.OpenConnection();
			SModel = VTIslem.ExecuteReader(CommandBehavior.Default);
			if (SModel.Sonuc.Equals(Sonuclar.Basarili))
			{
				VeriListe = new List<KatilimciTipiOdaTipiTablosuModel>();
				while (SModel.Reader.Read())
				{
					if (KayitBilgisiAl().Sonuc.Equals(Sonuclar.Basarili))
					{
						VeriListe.Add(SDataModel.Veriler);
					}
					else
					{
						SDataListModel = new SurecVeriModel<IList<KatilimciTipiOdaTipiTablosuModel>>{
							Sonuc = SDataModel.Sonuc,
							KullaniciMesaji = SDataModel.KullaniciMesaji,
							HataBilgi = SDataModel.HataBilgi
						};
						VTIslem.CloseConnection();
						return SDataListModel;
					}
				}
				SDataListModel = new SurecVeriModel<IList<KatilimciTipiOdaTipiTablosuModel>>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri listesi başarıyla çekildi",
					Veriler = VeriListe
				};
			}
			else
			{
				SDataListModel = new SurecVeriModel<IList<KatilimciTipiOdaTipiTablosuModel>>{
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