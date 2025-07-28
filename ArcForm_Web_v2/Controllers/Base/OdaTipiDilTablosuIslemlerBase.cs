using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using VeritabaniIslemSaglayici.Access;

namespace VeritabaniIslemMerkeziBase
{
	public abstract class OdaTipiDilTablosuIslemlerBase
	{
		public VTOperatorleri VTIslem;

		public List<OdaTipiDilTablosuModel> VeriListe;

		public SurecBilgiModel SModel;
		public SurecVeriModel<OdaTipiDilTablosuModel> SDataModel;
		public SurecVeriModel<IList<OdaTipiDilTablosuModel>> SDataListModel;

		public OdaTipiDilTablosuIslemlerBase()
		{
			VTIslem = new VTOperatorleri();
		}

		public OdaTipiDilTablosuIslemlerBase(OleDbTransaction Transcation)
		{
			VTIslem = new VTOperatorleri(Transcation);
		}

		public virtual SurecBilgiModel YeniKayitEkle(OdaTipiDilTablosuModel YeniKayit)
		{
			VTIslem.SetCommandText("INSERT INTO OdaTipiDilTablosu (OdaTipiID, DilID, OdaTipi, GuncellenmeTarihi, EklenmeTarihi) VALUES (@OdaTipiID, @DilID, @OdaTipi, @GuncellenmeTarihi, @EklenmeTarihi)");
			VTIslem.AddWithValue("OdaTipiID", YeniKayit.OdaTipiID);
			VTIslem.AddWithValue("DilID", YeniKayit.DilID);
			VTIslem.AddWithValue("OdaTipi", YeniKayit.OdaTipi);
			VTIslem.AddWithValue("GuncellenmeTarihi", YeniKayit.GuncellenmeTarihi);
			VTIslem.AddWithValue("EklenmeTarihi", YeniKayit.EklenmeTarihi);
			return VTIslem.ExecuteNonQuery();
		}

		public virtual SurecBilgiModel KayitGuncelle(OdaTipiDilTablosuModel GuncelKayit)
		{
			VTIslem.SetCommandText("UPDATE OdaTipiDilTablosu SET OdaTipiID=@OdaTipiID, DilID=@DilID, OdaTipi=@OdaTipi, GuncellenmeTarihi=@GuncellenmeTarihi, EklenmeTarihi=@EklenmeTarihi WHERE OdaTipiDilID=@OdaTipiDilID");
			VTIslem.AddWithValue("OdaTipiID", GuncelKayit.OdaTipiID);
			VTIslem.AddWithValue("DilID", GuncelKayit.DilID);
			VTIslem.AddWithValue("OdaTipi", GuncelKayit.OdaTipi);
			VTIslem.AddWithValue("GuncellenmeTarihi", GuncelKayit.GuncellenmeTarihi);
			VTIslem.AddWithValue("EklenmeTarihi", GuncelKayit.EklenmeTarihi);
			VTIslem.AddWithValue("OdaTipiDilID", GuncelKayit.OdaTipiDilID);
			return VTIslem.ExecuteNonQuery();
		}

		public virtual SurecBilgiModel KayitSil(int OdaTipiDilID)
		{
			VTIslem.SetCommandText("DELETE FROM OdaTipiDilTablosu WHERE OdaTipiDilID=@OdaTipiDilID");
			VTIslem.AddWithValue("OdaTipiDilID", OdaTipiDilID);
			return VTIslem.ExecuteNonQuery();
		}

		public virtual SurecVeriModel<OdaTipiDilTablosuModel> KayitBilgisi(int OdaTipiDilID)
		{
			VTIslem.SetCommandText($"SELECT {OdaTipiDilTablosuModel.SQLSutunSorgusu} FROM OdaTipiDilTablosu WHERE OdaTipiDilID = @OdaTipiDilID");
			VTIslem.AddWithValue("OdaTipiDilID", OdaTipiDilID);
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
					SDataModel = new SurecVeriModel<OdaTipiDilTablosuModel>
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
				SDataModel = new SurecVeriModel<OdaTipiDilTablosuModel>
				{
					Sonuc = SModel.Sonuc,
					KullaniciMesaji = SModel.KullaniciMesaji,
					HataBilgi = SModel.HataBilgi
				};
			}
			VTIslem.CloseConnection();
			return SDataModel;
		}

		public virtual SurecVeriModel<IList<OdaTipiDilTablosuModel>> KayitBilgileri()
		{
			VTIslem.SetCommandText($"SELECT {OdaTipiDilTablosuModel.SQLSutunSorgusu} FROM OdaTipiDilTablosu");
			VTIslem.OpenConnection();
			SModel = VTIslem.ExecuteReader(CommandBehavior.Default);
			if (SModel.Sonuc.Equals(Sonuclar.Basarili))
			{
				VeriListe = new List<OdaTipiDilTablosuModel>();
				while (SModel.Reader.Read())
				{
					if (KayitBilgisiAl().Sonuc.Equals(Sonuclar.Basarili))
					{
						VeriListe.Add(SDataModel.Veriler);
					}
					else
					{
						SDataListModel = new SurecVeriModel<IList<OdaTipiDilTablosuModel>>{
							Sonuc = SDataModel.Sonuc,
							KullaniciMesaji = SDataModel.KullaniciMesaji,
							HataBilgi = SDataModel.HataBilgi
						};
						VTIslem.CloseConnection();
						return SDataListModel;
					}
				}
				SDataListModel = new SurecVeriModel<IList<OdaTipiDilTablosuModel>>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri listesi başarıyla çekildi",
					Veriler = VeriListe
				};
			}
			else
			{
				SDataListModel = new SurecVeriModel<IList<OdaTipiDilTablosuModel>>{
					Sonuc = SModel.Sonuc,
					KullaniciMesaji = SModel.KullaniciMesaji,
					HataBilgi = SModel.HataBilgi
				};
			}
			VTIslem.CloseConnection();
			return SDataListModel;
		}

		SurecVeriModel<OdaTipiDilTablosuModel> KayitBilgisiAl()
		{
			try
			{
				SDataModel = new SurecVeriModel<OdaTipiDilTablosuModel>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri bilgisi başarıyla çekilmiştir.",
					Veriler = new OdaTipiDilTablosuModel
					{
						OdaTipiDilID = SModel.Reader.GetInt32(0),
						OdaTipiID = SModel.Reader.GetInt32(1),
						DilID = SModel.Reader.GetString(2),
						OdaTipi = SModel.Reader.GetString(3),
						GuncellenmeTarihi = SModel.Reader.GetDateTime(4),
						EklenmeTarihi = SModel.Reader.GetDateTime(5),
					}
				};

			}
			catch (InvalidCastException ex)
			{
				SDataModel = new SurecVeriModel<OdaTipiDilTablosuModel>{
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
				SDataModel = new SurecVeriModel<OdaTipiDilTablosuModel>{
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

		public virtual SurecVeriModel<OdaTipiDilTablosuModel> KayitBilgisiAl(int Baslangic, DbDataReader Reader)
		{
			try
			{
				SDataModel = new SurecVeriModel<OdaTipiDilTablosuModel>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri bilgisi başarıyla çekilmiştir.",
					Veriler = new OdaTipiDilTablosuModel{
						OdaTipiDilID = Reader.GetInt32(Baslangic + 0),
						OdaTipiID = Reader.GetInt32(Baslangic + 1),
						DilID = Reader.GetString(Baslangic + 2),
						OdaTipi = Reader.GetString(Baslangic + 3),
						GuncellenmeTarihi = Reader.GetDateTime(Baslangic + 4),
						EklenmeTarihi = Reader.GetDateTime(Baslangic + 5),
					}
				};
			}
			catch (InvalidCastException ex)
			{
				SDataModel = new SurecVeriModel<OdaTipiDilTablosuModel>{
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
				SDataModel = new SurecVeriModel<OdaTipiDilTablosuModel>{
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

		public virtual SurecVeriModel<IList<OdaTipiDilTablosuModel>> OdaTipiBilgileri(int OdaTipiID)
		{
			VTIslem.SetCommandText($"SELECT {OdaTipiDilTablosuModel.SQLSutunSorgusu} FROM OdaTipiDilTablosu WHERE OdaTipiID=@OdaTipiID");
			VTIslem.AddWithValue("OdaTipiID", OdaTipiID);
			VTIslem.OpenConnection();
			SModel = VTIslem.ExecuteReader(CommandBehavior.Default);
			if (SModel.Sonuc.Equals(Sonuclar.Basarili))
			{
				VeriListe = new List<OdaTipiDilTablosuModel>();
				while (SModel.Reader.Read())
				{
					if (KayitBilgisiAl().Sonuc.Equals(Sonuclar.Basarili))
					{
						VeriListe.Add(SDataModel.Veriler);
					}
					else
					{
						SDataListModel = new SurecVeriModel<IList<OdaTipiDilTablosuModel>>{
							Sonuc = SDataModel.Sonuc,
							KullaniciMesaji = SDataModel.KullaniciMesaji,
							HataBilgi = SDataModel.HataBilgi
						};
						VTIslem.CloseConnection();
						return SDataListModel;
					}
				}
				SDataListModel = new SurecVeriModel<IList<OdaTipiDilTablosuModel>>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri listesi başarıyla çekildi",
					Veriler = VeriListe
				};
			}
			else
			{
				SDataListModel = new SurecVeriModel<IList<OdaTipiDilTablosuModel>>{
					Sonuc = SModel.Sonuc,
					KullaniciMesaji = SModel.KullaniciMesaji,
					HataBilgi = SModel.HataBilgi
				};
			}
			VTIslem.CloseConnection();
			return SDataListModel;
		}

		public virtual SurecVeriModel<IList<OdaTipiDilTablosuModel>> DilBilgileri(string DilID)
		{
			VTIslem.SetCommandText($"SELECT {OdaTipiDilTablosuModel.SQLSutunSorgusu} FROM OdaTipiDilTablosu WHERE DilID=@DilID");
			VTIslem.AddWithValue("DilID", DilID);
			VTIslem.OpenConnection();
			SModel = VTIslem.ExecuteReader(CommandBehavior.Default);
			if (SModel.Sonuc.Equals(Sonuclar.Basarili))
			{
				VeriListe = new List<OdaTipiDilTablosuModel>();
				while (SModel.Reader.Read())
				{
					if (KayitBilgisiAl().Sonuc.Equals(Sonuclar.Basarili))
					{
						VeriListe.Add(SDataModel.Veriler);
					}
					else
					{
						SDataListModel = new SurecVeriModel<IList<OdaTipiDilTablosuModel>>{
							Sonuc = SDataModel.Sonuc,
							KullaniciMesaji = SDataModel.KullaniciMesaji,
							HataBilgi = SDataModel.HataBilgi
						};
						VTIslem.CloseConnection();
						return SDataListModel;
					}
				}
				SDataListModel = new SurecVeriModel<IList<OdaTipiDilTablosuModel>>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri listesi başarıyla çekildi",
					Veriler = VeriListe
				};
			}
			else
			{
				SDataListModel = new SurecVeriModel<IList<OdaTipiDilTablosuModel>>{
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