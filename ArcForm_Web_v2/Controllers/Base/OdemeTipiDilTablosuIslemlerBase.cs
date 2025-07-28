using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using VeritabaniIslemSaglayici.Access;

namespace VeritabaniIslemMerkeziBase
{
	public abstract class OdemeTipiDilTablosuIslemlerBase
	{
		public VTOperatorleri VTIslem;

		public List<OdemeTipiDilTablosuModel> VeriListe;

		public SurecBilgiModel SModel;
		public SurecVeriModel<OdemeTipiDilTablosuModel> SDataModel;
		public SurecVeriModel<IList<OdemeTipiDilTablosuModel>> SDataListModel;

		public OdemeTipiDilTablosuIslemlerBase()
		{
			VTIslem = new VTOperatorleri();
		}

		public OdemeTipiDilTablosuIslemlerBase(OleDbTransaction Transcation)
		{
			VTIslem = new VTOperatorleri(Transcation);
		}

		public virtual SurecBilgiModel YeniKayitEkle(OdemeTipiDilTablosuModel YeniKayit)
		{
			VTIslem.SetCommandText("INSERT INTO OdemeTipiDilTablosu (OdemeTipiID, DilID, OdemeTipi, GuncellenmeTarihi, EklenmeTarihi) VALUES (@OdemeTipiID, @DilID, @OdemeTipi, @GuncellenmeTarihi, @EklenmeTarihi)");
			VTIslem.AddWithValue("OdemeTipiID", YeniKayit.OdemeTipiID);
			VTIslem.AddWithValue("DilID", YeniKayit.DilID);
			VTIslem.AddWithValue("OdemeTipi", YeniKayit.OdemeTipi);
			VTIslem.AddWithValue("GuncellenmeTarihi", YeniKayit.GuncellenmeTarihi);
			VTIslem.AddWithValue("EklenmeTarihi", YeniKayit.EklenmeTarihi);
			return VTIslem.ExecuteNonQuery();
		}

		public virtual SurecBilgiModel KayitGuncelle(OdemeTipiDilTablosuModel GuncelKayit)
		{
			VTIslem.SetCommandText("UPDATE OdemeTipiDilTablosu SET OdemeTipiID=@OdemeTipiID, DilID=@DilID, OdemeTipi=@OdemeTipi, GuncellenmeTarihi=@GuncellenmeTarihi, EklenmeTarihi=@EklenmeTarihi WHERE OdemeTipiDilID=@OdemeTipiDilID");
			VTIslem.AddWithValue("OdemeTipiID", GuncelKayit.OdemeTipiID);
			VTIslem.AddWithValue("DilID", GuncelKayit.DilID);
			VTIslem.AddWithValue("OdemeTipi", GuncelKayit.OdemeTipi);
			VTIslem.AddWithValue("GuncellenmeTarihi", GuncelKayit.GuncellenmeTarihi);
			VTIslem.AddWithValue("EklenmeTarihi", GuncelKayit.EklenmeTarihi);
			VTIslem.AddWithValue("OdemeTipiDilID", GuncelKayit.OdemeTipiDilID);
			return VTIslem.ExecuteNonQuery();
		}

		public virtual SurecBilgiModel KayitSil(int OdemeTipiDilID)
		{
			VTIslem.SetCommandText("DELETE FROM OdemeTipiDilTablosu WHERE OdemeTipiDilID=@OdemeTipiDilID");
			VTIslem.AddWithValue("OdemeTipiDilID", OdemeTipiDilID);
			return VTIslem.ExecuteNonQuery();
		}

		public virtual SurecVeriModel<OdemeTipiDilTablosuModel> KayitBilgisi(int OdemeTipiDilID)
		{
			VTIslem.SetCommandText($"SELECT {OdemeTipiDilTablosuModel.SQLSutunSorgusu} FROM OdemeTipiDilTablosu WHERE OdemeTipiDilID = @OdemeTipiDilID");
			VTIslem.AddWithValue("OdemeTipiDilID", OdemeTipiDilID);
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
					SDataModel = new SurecVeriModel<OdemeTipiDilTablosuModel>
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
				SDataModel = new SurecVeriModel<OdemeTipiDilTablosuModel>
				{
					Sonuc = SModel.Sonuc,
					KullaniciMesaji = SModel.KullaniciMesaji,
					HataBilgi = SModel.HataBilgi
				};
			}
			VTIslem.CloseConnection();
			return SDataModel;
		}

		public virtual SurecVeriModel<IList<OdemeTipiDilTablosuModel>> KayitBilgileri()
		{
			VTIslem.SetCommandText($"SELECT {OdemeTipiDilTablosuModel.SQLSutunSorgusu} FROM OdemeTipiDilTablosu");
			VTIslem.OpenConnection();
			SModel = VTIslem.ExecuteReader(CommandBehavior.Default);
			if (SModel.Sonuc.Equals(Sonuclar.Basarili))
			{
				VeriListe = new List<OdemeTipiDilTablosuModel>();
				while (SModel.Reader.Read())
				{
					if (KayitBilgisiAl().Sonuc.Equals(Sonuclar.Basarili))
					{
						VeriListe.Add(SDataModel.Veriler);
					}
					else
					{
						SDataListModel = new SurecVeriModel<IList<OdemeTipiDilTablosuModel>>{
							Sonuc = SDataModel.Sonuc,
							KullaniciMesaji = SDataModel.KullaniciMesaji,
							HataBilgi = SDataModel.HataBilgi
						};
						VTIslem.CloseConnection();
						return SDataListModel;
					}
				}
				SDataListModel = new SurecVeriModel<IList<OdemeTipiDilTablosuModel>>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri listesi başarıyla çekildi",
					Veriler = VeriListe
				};
			}
			else
			{
				SDataListModel = new SurecVeriModel<IList<OdemeTipiDilTablosuModel>>{
					Sonuc = SModel.Sonuc,
					KullaniciMesaji = SModel.KullaniciMesaji,
					HataBilgi = SModel.HataBilgi
				};
			}
			VTIslem.CloseConnection();
			return SDataListModel;
		}

		SurecVeriModel<OdemeTipiDilTablosuModel> KayitBilgisiAl()
		{
			try
			{
				SDataModel = new SurecVeriModel<OdemeTipiDilTablosuModel>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri bilgisi başarıyla çekilmiştir.",
					Veriler = new OdemeTipiDilTablosuModel
					{
						OdemeTipiDilID = SModel.Reader.GetInt32(0),
						OdemeTipiID = SModel.Reader.GetInt32(1),
						DilID = SModel.Reader.GetString(2),
						OdemeTipi = SModel.Reader.GetString(3),
						GuncellenmeTarihi = SModel.Reader.GetDateTime(4),
						EklenmeTarihi = SModel.Reader.GetDateTime(5),
					}
				};

			}
			catch (InvalidCastException ex)
			{
				SDataModel = new SurecVeriModel<OdemeTipiDilTablosuModel>{
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
				SDataModel = new SurecVeriModel<OdemeTipiDilTablosuModel>{
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

		public virtual SurecVeriModel<OdemeTipiDilTablosuModel> KayitBilgisiAl(int Baslangic, DbDataReader Reader)
		{
			try
			{
				SDataModel = new SurecVeriModel<OdemeTipiDilTablosuModel>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri bilgisi başarıyla çekilmiştir.",
					Veriler = new OdemeTipiDilTablosuModel{
						OdemeTipiDilID = Reader.GetInt32(Baslangic + 0),
						OdemeTipiID = Reader.GetInt32(Baslangic + 1),
						DilID = Reader.GetString(Baslangic + 2),
						OdemeTipi = Reader.GetString(Baslangic + 3),
						GuncellenmeTarihi = Reader.GetDateTime(Baslangic + 4),
						EklenmeTarihi = Reader.GetDateTime(Baslangic + 5),
					}
				};
			}
			catch (InvalidCastException ex)
			{
				SDataModel = new SurecVeriModel<OdemeTipiDilTablosuModel>{
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
				SDataModel = new SurecVeriModel<OdemeTipiDilTablosuModel>{
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

		public virtual SurecVeriModel<IList<OdemeTipiDilTablosuModel>> OdemeTipiBilgileri(int OdemeTipiID)
		{
			VTIslem.SetCommandText($"SELECT {OdemeTipiDilTablosuModel.SQLSutunSorgusu} FROM OdemeTipiDilTablosu WHERE OdemeTipiID=@OdemeTipiID");
			VTIslem.AddWithValue("OdemeTipiID", OdemeTipiID);
			VTIslem.OpenConnection();
			SModel = VTIslem.ExecuteReader(CommandBehavior.Default);
			if (SModel.Sonuc.Equals(Sonuclar.Basarili))
			{
				VeriListe = new List<OdemeTipiDilTablosuModel>();
				while (SModel.Reader.Read())
				{
					if (KayitBilgisiAl().Sonuc.Equals(Sonuclar.Basarili))
					{
						VeriListe.Add(SDataModel.Veriler);
					}
					else
					{
						SDataListModel = new SurecVeriModel<IList<OdemeTipiDilTablosuModel>>{
							Sonuc = SDataModel.Sonuc,
							KullaniciMesaji = SDataModel.KullaniciMesaji,
							HataBilgi = SDataModel.HataBilgi
						};
						VTIslem.CloseConnection();
						return SDataListModel;
					}
				}
				SDataListModel = new SurecVeriModel<IList<OdemeTipiDilTablosuModel>>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri listesi başarıyla çekildi",
					Veriler = VeriListe
				};
			}
			else
			{
				SDataListModel = new SurecVeriModel<IList<OdemeTipiDilTablosuModel>>{
					Sonuc = SModel.Sonuc,
					KullaniciMesaji = SModel.KullaniciMesaji,
					HataBilgi = SModel.HataBilgi
				};
			}
			VTIslem.CloseConnection();
			return SDataListModel;
		}

		public virtual SurecVeriModel<IList<OdemeTipiDilTablosuModel>> DilBilgileri(string DilID)
		{
			VTIslem.SetCommandText($"SELECT {OdemeTipiDilTablosuModel.SQLSutunSorgusu} FROM OdemeTipiDilTablosu WHERE DilID=@DilID");
			VTIslem.AddWithValue("DilID", DilID);
			VTIslem.OpenConnection();
			SModel = VTIslem.ExecuteReader(CommandBehavior.Default);
			if (SModel.Sonuc.Equals(Sonuclar.Basarili))
			{
				VeriListe = new List<OdemeTipiDilTablosuModel>();
				while (SModel.Reader.Read())
				{
					if (KayitBilgisiAl().Sonuc.Equals(Sonuclar.Basarili))
					{
						VeriListe.Add(SDataModel.Veriler);
					}
					else
					{
						SDataListModel = new SurecVeriModel<IList<OdemeTipiDilTablosuModel>>{
							Sonuc = SDataModel.Sonuc,
							KullaniciMesaji = SDataModel.KullaniciMesaji,
							HataBilgi = SDataModel.HataBilgi
						};
						VTIslem.CloseConnection();
						return SDataListModel;
					}
				}
				SDataListModel = new SurecVeriModel<IList<OdemeTipiDilTablosuModel>>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri listesi başarıyla çekildi",
					Veriler = VeriListe
				};
			}
			else
			{
				SDataListModel = new SurecVeriModel<IList<OdemeTipiDilTablosuModel>>{
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