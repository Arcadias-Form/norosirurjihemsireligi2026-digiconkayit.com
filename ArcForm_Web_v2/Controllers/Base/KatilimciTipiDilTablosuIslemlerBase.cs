using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using VeritabaniIslemSaglayici.Access;

namespace VeritabaniIslemMerkeziBase
{
	public abstract class KatilimciTipiDilTablosuIslemlerBase
	{
		public VTOperatorleri VTIslem;

		public List<KatilimciTipiDilTablosuModel> VeriListe;

		public SurecBilgiModel SModel;
		public SurecVeriModel<KatilimciTipiDilTablosuModel> SDataModel;
		public SurecVeriModel<IList<KatilimciTipiDilTablosuModel>> SDataListModel;

		public KatilimciTipiDilTablosuIslemlerBase()
		{
			VTIslem = new VTOperatorleri();
		}

		public KatilimciTipiDilTablosuIslemlerBase(OleDbTransaction Transcation)
		{
			VTIslem = new VTOperatorleri(Transcation);
		}

		public virtual SurecBilgiModel YeniKayitEkle(KatilimciTipiDilTablosuModel YeniKayit)
		{
			VTIslem.SetCommandText("INSERT INTO KatilimciTipiDilTablosu (KatilimciTipiID, DilID, KatilimciTipi, GuncellenmeTarihi, EklenmeTarihi) VALUES (@KatilimciTipiID, @DilID, @KatilimciTipi, @GuncellenmeTarihi, @EklenmeTarihi)");
			VTIslem.AddWithValue("KatilimciTipiID", YeniKayit.KatilimciTipiID);
			VTIslem.AddWithValue("DilID", YeniKayit.DilID);
			VTIslem.AddWithValue("KatilimciTipi", YeniKayit.KatilimciTipi);
			VTIslem.AddWithValue("GuncellenmeTarihi", YeniKayit.GuncellenmeTarihi);
			VTIslem.AddWithValue("EklenmeTarihi", YeniKayit.EklenmeTarihi);
			return VTIslem.ExecuteNonQuery();
		}

		public virtual SurecBilgiModel KayitGuncelle(KatilimciTipiDilTablosuModel GuncelKayit)
		{
			VTIslem.SetCommandText("UPDATE KatilimciTipiDilTablosu SET KatilimciTipiID=@KatilimciTipiID, DilID=@DilID, KatilimciTipi=@KatilimciTipi, GuncellenmeTarihi=@GuncellenmeTarihi, EklenmeTarihi=@EklenmeTarihi WHERE KatilimciTipiDilID=@KatilimciTipiDilID");
			VTIslem.AddWithValue("KatilimciTipiID", GuncelKayit.KatilimciTipiID);
			VTIslem.AddWithValue("DilID", GuncelKayit.DilID);
			VTIslem.AddWithValue("KatilimciTipi", GuncelKayit.KatilimciTipi);
			VTIslem.AddWithValue("GuncellenmeTarihi", GuncelKayit.GuncellenmeTarihi);
			VTIslem.AddWithValue("EklenmeTarihi", GuncelKayit.EklenmeTarihi);
			VTIslem.AddWithValue("KatilimciTipiDilID", GuncelKayit.KatilimciTipiDilID);
			return VTIslem.ExecuteNonQuery();
		}

		public virtual SurecBilgiModel KayitSil(int KatilimciTipiDilID)
		{
			VTIslem.SetCommandText("DELETE FROM KatilimciTipiDilTablosu WHERE KatilimciTipiDilID=@KatilimciTipiDilID");
			VTIslem.AddWithValue("KatilimciTipiDilID", KatilimciTipiDilID);
			return VTIslem.ExecuteNonQuery();
		}

		public virtual SurecVeriModel<KatilimciTipiDilTablosuModel> KayitBilgisi(int KatilimciTipiDilID)
		{
			VTIslem.SetCommandText($"SELECT {KatilimciTipiDilTablosuModel.SQLSutunSorgusu} FROM KatilimciTipiDilTablosu WHERE KatilimciTipiDilID = @KatilimciTipiDilID");
			VTIslem.AddWithValue("KatilimciTipiDilID", KatilimciTipiDilID);
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
					SDataModel = new SurecVeriModel<KatilimciTipiDilTablosuModel>
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
				SDataModel = new SurecVeriModel<KatilimciTipiDilTablosuModel>
				{
					Sonuc = SModel.Sonuc,
					KullaniciMesaji = SModel.KullaniciMesaji,
					HataBilgi = SModel.HataBilgi
				};
			}
			VTIslem.CloseConnection();
			return SDataModel;
		}

		public virtual SurecVeriModel<IList<KatilimciTipiDilTablosuModel>> KayitBilgileri()
		{
			VTIslem.SetCommandText($"SELECT {KatilimciTipiDilTablosuModel.SQLSutunSorgusu} FROM KatilimciTipiDilTablosu");
			VTIslem.OpenConnection();
			SModel = VTIslem.ExecuteReader(CommandBehavior.Default);
			if (SModel.Sonuc.Equals(Sonuclar.Basarili))
			{
				VeriListe = new List<KatilimciTipiDilTablosuModel>();
				while (SModel.Reader.Read())
				{
					if (KayitBilgisiAl().Sonuc.Equals(Sonuclar.Basarili))
					{
						VeriListe.Add(SDataModel.Veriler);
					}
					else
					{
						SDataListModel = new SurecVeriModel<IList<KatilimciTipiDilTablosuModel>>{
							Sonuc = SDataModel.Sonuc,
							KullaniciMesaji = SDataModel.KullaniciMesaji,
							HataBilgi = SDataModel.HataBilgi
						};
						VTIslem.CloseConnection();
						return SDataListModel;
					}
				}
				SDataListModel = new SurecVeriModel<IList<KatilimciTipiDilTablosuModel>>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri listesi başarıyla çekildi",
					Veriler = VeriListe
				};
			}
			else
			{
				SDataListModel = new SurecVeriModel<IList<KatilimciTipiDilTablosuModel>>{
					Sonuc = SModel.Sonuc,
					KullaniciMesaji = SModel.KullaniciMesaji,
					HataBilgi = SModel.HataBilgi
				};
			}
			VTIslem.CloseConnection();
			return SDataListModel;
		}

		SurecVeriModel<KatilimciTipiDilTablosuModel> KayitBilgisiAl()
		{
			try
			{
				SDataModel = new SurecVeriModel<KatilimciTipiDilTablosuModel>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri bilgisi başarıyla çekilmiştir.",
					Veriler = new KatilimciTipiDilTablosuModel
					{
						KatilimciTipiDilID = SModel.Reader.GetInt32(0),
						KatilimciTipiID = SModel.Reader.GetInt32(1),
						DilID = SModel.Reader.GetString(2),
						KatilimciTipi = SModel.Reader.GetString(3),
						GuncellenmeTarihi = SModel.Reader.GetDateTime(4),
						EklenmeTarihi = SModel.Reader.GetDateTime(5),
					}
				};

			}
			catch (InvalidCastException ex)
			{
				SDataModel = new SurecVeriModel<KatilimciTipiDilTablosuModel>{
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
				SDataModel = new SurecVeriModel<KatilimciTipiDilTablosuModel>{
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

		public virtual SurecVeriModel<KatilimciTipiDilTablosuModel> KayitBilgisiAl(int Baslangic, DbDataReader Reader)
		{
			try
			{
				SDataModel = new SurecVeriModel<KatilimciTipiDilTablosuModel>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri bilgisi başarıyla çekilmiştir.",
					Veriler = new KatilimciTipiDilTablosuModel{
						KatilimciTipiDilID = Reader.GetInt32(Baslangic + 0),
						KatilimciTipiID = Reader.GetInt32(Baslangic + 1),
						DilID = Reader.GetString(Baslangic + 2),
						KatilimciTipi = Reader.GetString(Baslangic + 3),
						GuncellenmeTarihi = Reader.GetDateTime(Baslangic + 4),
						EklenmeTarihi = Reader.GetDateTime(Baslangic + 5),
					}
				};
			}
			catch (InvalidCastException ex)
			{
				SDataModel = new SurecVeriModel<KatilimciTipiDilTablosuModel>{
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
				SDataModel = new SurecVeriModel<KatilimciTipiDilTablosuModel>{
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

		public virtual SurecVeriModel<IList<KatilimciTipiDilTablosuModel>> KatilimciTipiBilgileri(int KatilimciTipiID)
		{
			VTIslem.SetCommandText($"SELECT {KatilimciTipiDilTablosuModel.SQLSutunSorgusu} FROM KatilimciTipiDilTablosu WHERE KatilimciTipiID=@KatilimciTipiID");
			VTIslem.AddWithValue("KatilimciTipiID", KatilimciTipiID);
			VTIslem.OpenConnection();
			SModel = VTIslem.ExecuteReader(CommandBehavior.Default);
			if (SModel.Sonuc.Equals(Sonuclar.Basarili))
			{
				VeriListe = new List<KatilimciTipiDilTablosuModel>();
				while (SModel.Reader.Read())
				{
					if (KayitBilgisiAl().Sonuc.Equals(Sonuclar.Basarili))
					{
						VeriListe.Add(SDataModel.Veriler);
					}
					else
					{
						SDataListModel = new SurecVeriModel<IList<KatilimciTipiDilTablosuModel>>{
							Sonuc = SDataModel.Sonuc,
							KullaniciMesaji = SDataModel.KullaniciMesaji,
							HataBilgi = SDataModel.HataBilgi
						};
						VTIslem.CloseConnection();
						return SDataListModel;
					}
				}
				SDataListModel = new SurecVeriModel<IList<KatilimciTipiDilTablosuModel>>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri listesi başarıyla çekildi",
					Veriler = VeriListe
				};
			}
			else
			{
				SDataListModel = new SurecVeriModel<IList<KatilimciTipiDilTablosuModel>>{
					Sonuc = SModel.Sonuc,
					KullaniciMesaji = SModel.KullaniciMesaji,
					HataBilgi = SModel.HataBilgi
				};
			}
			VTIslem.CloseConnection();
			return SDataListModel;
		}

		public virtual SurecVeriModel<IList<KatilimciTipiDilTablosuModel>> DilBilgileri(string DilID)
		{
			VTIslem.SetCommandText($"SELECT {KatilimciTipiDilTablosuModel.SQLSutunSorgusu} FROM KatilimciTipiDilTablosu WHERE DilID=@DilID");
			VTIslem.AddWithValue("DilID", DilID);
			VTIslem.OpenConnection();
			SModel = VTIslem.ExecuteReader(CommandBehavior.Default);
			if (SModel.Sonuc.Equals(Sonuclar.Basarili))
			{
				VeriListe = new List<KatilimciTipiDilTablosuModel>();
				while (SModel.Reader.Read())
				{
					if (KayitBilgisiAl().Sonuc.Equals(Sonuclar.Basarili))
					{
						VeriListe.Add(SDataModel.Veriler);
					}
					else
					{
						SDataListModel = new SurecVeriModel<IList<KatilimciTipiDilTablosuModel>>{
							Sonuc = SDataModel.Sonuc,
							KullaniciMesaji = SDataModel.KullaniciMesaji,
							HataBilgi = SDataModel.HataBilgi
						};
						VTIslem.CloseConnection();
						return SDataListModel;
					}
				}
				SDataListModel = new SurecVeriModel<IList<KatilimciTipiDilTablosuModel>>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri listesi başarıyla çekildi",
					Veriler = VeriListe
				};
			}
			else
			{
				SDataListModel = new SurecVeriModel<IList<KatilimciTipiDilTablosuModel>>{
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