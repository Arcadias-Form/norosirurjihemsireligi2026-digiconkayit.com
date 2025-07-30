using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using VeritabaniIslemSaglayici.Access;

namespace VeritabaniIslemMerkeziBase
{
	public abstract class KonaklamaTablosuIslemlerBase
	{
		public VTOperatorleri VTIslem;

		public List<KonaklamaTablosuModel> VeriListe;

		public SurecBilgiModel SModel;
		public SurecVeriModel<KonaklamaTablosuModel> SDataModel;
		public SurecVeriModel<IList<KonaklamaTablosuModel>> SDataListModel;

		public KonaklamaTablosuIslemlerBase()
		{
			VTIslem = new VTOperatorleri();
		}

		public KonaklamaTablosuIslemlerBase(OleDbTransaction Transcation)
		{
			VTIslem = new VTOperatorleri(Transcation);
		}

		public virtual SurecBilgiModel YeniKayitEkle(KonaklamaTablosuModel YeniKayit)
		{
			VTIslem.SetCommandText("INSERT INTO KonaklamaTablosu (KatilimciID, KatilimciTipiOdaTipiID, GirisTarihi, CikisTarihi, Refakatci, GuncellenmeTarihi, EklenmeTarihi) VALUES (@KatilimciID, @KatilimciTipiOdaTipiID, @GirisTarihi, @CikisTarihi, @Refakatci, @GuncellenmeTarihi, @EklenmeTarihi)");
			VTIslem.AddWithValue("KatilimciID", YeniKayit.KatilimciID);
			VTIslem.AddWithValue("KatilimciTipiOdaTipiID", YeniKayit.KatilimciTipiOdaTipiID);
			VTIslem.AddWithValue("GirisTarihi", YeniKayit.GirisTarihi);
			VTIslem.AddWithValue("CikisTarihi", YeniKayit.CikisTarihi);
			VTIslem.AddWithValue("Refakatci", YeniKayit.Refakatci);
			VTIslem.AddWithValue("GuncellenmeTarihi", YeniKayit.GuncellenmeTarihi);
			VTIslem.AddWithValue("EklenmeTarihi", YeniKayit.EklenmeTarihi);
			return VTIslem.ExecuteNonQuery();
		}

		public virtual SurecBilgiModel KayitGuncelle(KonaklamaTablosuModel GuncelKayit)
		{
			VTIslem.SetCommandText("UPDATE KonaklamaTablosu SET KatilimciID=@KatilimciID, KatilimciTipiOdaTipiID=@KatilimciTipiOdaTipiID, GirisTarihi=@GirisTarihi, CikisTarihi=@CikisTarihi, Refakatci=@Refakatci, GuncellenmeTarihi=@GuncellenmeTarihi, EklenmeTarihi=@EklenmeTarihi WHERE KonaklamaID=@KonaklamaID");
			VTIslem.AddWithValue("KatilimciID", GuncelKayit.KatilimciID);
			VTIslem.AddWithValue("KatilimciTipiOdaTipiID", GuncelKayit.KatilimciTipiOdaTipiID);
			VTIslem.AddWithValue("GirisTarihi", GuncelKayit.GirisTarihi);
			VTIslem.AddWithValue("CikisTarihi", GuncelKayit.CikisTarihi);
			VTIslem.AddWithValue("Refakatci", GuncelKayit.Refakatci);
			VTIslem.AddWithValue("GuncellenmeTarihi", GuncelKayit.GuncellenmeTarihi);
			VTIslem.AddWithValue("EklenmeTarihi", GuncelKayit.EklenmeTarihi);
			VTIslem.AddWithValue("KonaklamaID", GuncelKayit.KonaklamaID);
			return VTIslem.ExecuteNonQuery();
		}

		public virtual SurecBilgiModel KayitSil(int KonaklamaID)
		{
			VTIslem.SetCommandText("DELETE FROM KonaklamaTablosu WHERE KonaklamaID=@KonaklamaID");
			VTIslem.AddWithValue("KonaklamaID", KonaklamaID);
			return VTIslem.ExecuteNonQuery();
		}

		public virtual SurecVeriModel<KonaklamaTablosuModel> KayitBilgisi(int KonaklamaID)
		{
			VTIslem.SetCommandText($"SELECT {KonaklamaTablosuModel.SQLSutunSorgusu} FROM KonaklamaTablosu WHERE KonaklamaID = @KonaklamaID");
			VTIslem.AddWithValue("KonaklamaID", KonaklamaID);
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
					SDataModel = new SurecVeriModel<KonaklamaTablosuModel>
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
				SDataModel = new SurecVeriModel<KonaklamaTablosuModel>
				{
					Sonuc = SModel.Sonuc,
					KullaniciMesaji = SModel.KullaniciMesaji,
					HataBilgi = SModel.HataBilgi
				};
			}
			VTIslem.CloseConnection();
			return SDataModel;
		}

		public virtual SurecVeriModel<IList<KonaklamaTablosuModel>> KayitBilgileri()
		{
			VTIslem.SetCommandText($"SELECT {KonaklamaTablosuModel.SQLSutunSorgusu} FROM KonaklamaTablosu");
			VTIslem.OpenConnection();
			SModel = VTIslem.ExecuteReader(CommandBehavior.Default);
			if (SModel.Sonuc.Equals(Sonuclar.Basarili))
			{
				VeriListe = new List<KonaklamaTablosuModel>();
				while (SModel.Reader.Read())
				{
					if (KayitBilgisiAl().Sonuc.Equals(Sonuclar.Basarili))
					{
						VeriListe.Add(SDataModel.Veriler);
					}
					else
					{
						SDataListModel = new SurecVeriModel<IList<KonaklamaTablosuModel>>{
							Sonuc = SDataModel.Sonuc,
							KullaniciMesaji = SDataModel.KullaniciMesaji,
							HataBilgi = SDataModel.HataBilgi
						};
						VTIslem.CloseConnection();
						return SDataListModel;
					}
				}
				SDataListModel = new SurecVeriModel<IList<KonaklamaTablosuModel>>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri listesi başarıyla çekildi",
					Veriler = VeriListe
				};
			}
			else
			{
				SDataListModel = new SurecVeriModel<IList<KonaklamaTablosuModel>>{
					Sonuc = SModel.Sonuc,
					KullaniciMesaji = SModel.KullaniciMesaji,
					HataBilgi = SModel.HataBilgi
				};
			}
			VTIslem.CloseConnection();
			return SDataListModel;
		}

		SurecVeriModel<KonaklamaTablosuModel> KayitBilgisiAl()
		{
			try
			{
				SDataModel = new SurecVeriModel<KonaklamaTablosuModel>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri bilgisi başarıyla çekilmiştir.",
					Veriler = new KonaklamaTablosuModel
					{
						KonaklamaID = SModel.Reader.GetInt32(0),
						KatilimciID = SModel.Reader.GetString(1),
						KatilimciTipiOdaTipiID = SModel.Reader.GetInt32(2),
						GirisTarihi = SModel.Reader.GetDateTime(3),
						CikisTarihi = SModel.Reader.GetDateTime(4),
						Refakatci = SModel.Reader.GetString(5),
						GuncellenmeTarihi = SModel.Reader.GetDateTime(6),
						EklenmeTarihi = SModel.Reader.GetDateTime(7),
					}
				};

			}
			catch (InvalidCastException ex)
			{
				SDataModel = new SurecVeriModel<KonaklamaTablosuModel>{
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
				SDataModel = new SurecVeriModel<KonaklamaTablosuModel>{
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

		public virtual SurecVeriModel<KonaklamaTablosuModel> KayitBilgisiAl(int Baslangic, DbDataReader Reader)
		{
			try
			{
				SDataModel = new SurecVeriModel<KonaklamaTablosuModel>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri bilgisi başarıyla çekilmiştir.",
					Veriler = new KonaklamaTablosuModel{
						KonaklamaID = Reader.GetInt32(Baslangic + 0),
						KatilimciID = Reader.GetString(Baslangic + 1),
						KatilimciTipiOdaTipiID = Reader.GetInt32(Baslangic + 2),
						GirisTarihi = Reader.GetDateTime(Baslangic + 3),
						CikisTarihi = Reader.GetDateTime(Baslangic + 4),
						Refakatci = Reader.GetString(Baslangic + 5),
						GuncellenmeTarihi = Reader.GetDateTime(Baslangic + 6),
						EklenmeTarihi = Reader.GetDateTime(Baslangic + 7),
					}
				};
			}
			catch (InvalidCastException ex)
			{
				SDataModel = new SurecVeriModel<KonaklamaTablosuModel>{
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
				SDataModel = new SurecVeriModel<KonaklamaTablosuModel>{
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

		public virtual SurecVeriModel<KonaklamaTablosuModel> KatilimciBilgisi(string KatilimciID)
		{
			VTIslem.SetCommandText($"SELECT {KonaklamaTablosuModel.SQLSutunSorgusu}FROM KonaklamaTablosu WHERE KatilimciID=@KatilimciID");
			VTIslem.AddWithValue("KatilimciID", KatilimciID);
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
					SDataModel = new SurecVeriModel<KonaklamaTablosuModel>
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
				SDataModel = new SurecVeriModel<KonaklamaTablosuModel>
				{
					Sonuc = SModel.Sonuc,
					KullaniciMesaji = SModel.KullaniciMesaji,
					HataBilgi = SModel.HataBilgi
				};
			}
			VTIslem.CloseConnection();
			return SDataModel;
		}

		public virtual SurecVeriModel<IList<KonaklamaTablosuModel>> KatilimciTipiOdaTipiBilgileri(int KatilimciTipiOdaTipiID)
		{
			VTIslem.SetCommandText($"SELECT {KonaklamaTablosuModel.SQLSutunSorgusu} FROM KonaklamaTablosu WHERE KatilimciTipiOdaTipiID=@KatilimciTipiOdaTipiID");
			VTIslem.AddWithValue("KatilimciTipiOdaTipiID", KatilimciTipiOdaTipiID);
			VTIslem.OpenConnection();
			SModel = VTIslem.ExecuteReader(CommandBehavior.Default);
			if (SModel.Sonuc.Equals(Sonuclar.Basarili))
			{
				VeriListe = new List<KonaklamaTablosuModel>();
				while (SModel.Reader.Read())
				{
					if (KayitBilgisiAl().Sonuc.Equals(Sonuclar.Basarili))
					{
						VeriListe.Add(SDataModel.Veriler);
					}
					else
					{
						SDataListModel = new SurecVeriModel<IList<KonaklamaTablosuModel>>{
							Sonuc = SDataModel.Sonuc,
							KullaniciMesaji = SDataModel.KullaniciMesaji,
							HataBilgi = SDataModel.HataBilgi
						};
						VTIslem.CloseConnection();
						return SDataListModel;
					}
				}
				SDataListModel = new SurecVeriModel<IList<KonaklamaTablosuModel>>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri listesi başarıyla çekildi",
					Veriler = VeriListe
				};
			}
			else
			{
				SDataListModel = new SurecVeriModel<IList<KonaklamaTablosuModel>>{
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