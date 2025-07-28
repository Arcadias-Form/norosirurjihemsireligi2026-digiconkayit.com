using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using VeritabaniIslemSaglayici.Access;

namespace VeritabaniIslemMerkeziBase
{
	public abstract class EtkinlikDilTablosuIslemlerBase
	{
		public VTOperatorleri VTIslem;

		public List<EtkinlikDilTablosuModel> VeriListe;

		public SurecBilgiModel SModel;
		public SurecVeriModel<EtkinlikDilTablosuModel> SDataModel;
		public SurecVeriModel<IList<EtkinlikDilTablosuModel>> SDataListModel;

		public EtkinlikDilTablosuIslemlerBase()
		{
			VTIslem = new VTOperatorleri();
		}

		public EtkinlikDilTablosuIslemlerBase(OleDbTransaction Transcation)
		{
			VTIslem = new VTOperatorleri(Transcation);
		}

		public virtual SurecBilgiModel YeniKayitEkle(EtkinlikDilTablosuModel YeniKayit)
		{
			VTIslem.SetCommandText("INSERT INTO EtkinlikDilTablosu (EtkinlikID, DilID, Etkinlik, GuncellenmeTarihi, EtkinlikTarihi) VALUES (@EtkinlikID, @DilID, @Etkinlik, @GuncellenmeTarihi, @EtkinlikTarihi)");
			VTIslem.AddWithValue("EtkinlikID", YeniKayit.EtkinlikID);
			VTIslem.AddWithValue("DilID", YeniKayit.DilID);
			VTIslem.AddWithValue("Etkinlik", YeniKayit.Etkinlik);
			VTIslem.AddWithValue("GuncellenmeTarihi", YeniKayit.GuncellenmeTarihi);
			VTIslem.AddWithValue("EtkinlikTarihi", YeniKayit.EtkinlikTarihi);
			return VTIslem.ExecuteNonQuery();
		}

		public virtual SurecBilgiModel KayitGuncelle(EtkinlikDilTablosuModel GuncelKayit)
		{
			VTIslem.SetCommandText("UPDATE EtkinlikDilTablosu SET EtkinlikID=@EtkinlikID, DilID=@DilID, Etkinlik=@Etkinlik, GuncellenmeTarihi=@GuncellenmeTarihi, EtkinlikTarihi=@EtkinlikTarihi WHERE EtkinlikDilID=@EtkinlikDilID");
			VTIslem.AddWithValue("EtkinlikID", GuncelKayit.EtkinlikID);
			VTIslem.AddWithValue("DilID", GuncelKayit.DilID);
			VTIslem.AddWithValue("Etkinlik", GuncelKayit.Etkinlik);
			VTIslem.AddWithValue("GuncellenmeTarihi", GuncelKayit.GuncellenmeTarihi);
			VTIslem.AddWithValue("EtkinlikTarihi", GuncelKayit.EtkinlikTarihi);
			VTIslem.AddWithValue("EtkinlikDilID", GuncelKayit.EtkinlikDilID);
			return VTIslem.ExecuteNonQuery();
		}

		public virtual SurecBilgiModel KayitSil(int EtkinlikDilID)
		{
			VTIslem.SetCommandText("DELETE FROM EtkinlikDilTablosu WHERE EtkinlikDilID=@EtkinlikDilID");
			VTIslem.AddWithValue("EtkinlikDilID", EtkinlikDilID);
			return VTIslem.ExecuteNonQuery();
		}

		public virtual SurecVeriModel<EtkinlikDilTablosuModel> KayitBilgisi(int EtkinlikDilID)
		{
			VTIslem.SetCommandText($"SELECT {EtkinlikDilTablosuModel.SQLSutunSorgusu} FROM EtkinlikDilTablosu WHERE EtkinlikDilID = @EtkinlikDilID");
			VTIslem.AddWithValue("EtkinlikDilID", EtkinlikDilID);
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
					SDataModel = new SurecVeriModel<EtkinlikDilTablosuModel>
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
				SDataModel = new SurecVeriModel<EtkinlikDilTablosuModel>
				{
					Sonuc = SModel.Sonuc,
					KullaniciMesaji = SModel.KullaniciMesaji,
					HataBilgi = SModel.HataBilgi
				};
			}
			VTIslem.CloseConnection();
			return SDataModel;
		}

		public virtual SurecVeriModel<IList<EtkinlikDilTablosuModel>> KayitBilgileri()
		{
			VTIslem.SetCommandText($"SELECT {EtkinlikDilTablosuModel.SQLSutunSorgusu} FROM EtkinlikDilTablosu");
			VTIslem.OpenConnection();
			SModel = VTIslem.ExecuteReader(CommandBehavior.Default);
			if (SModel.Sonuc.Equals(Sonuclar.Basarili))
			{
				VeriListe = new List<EtkinlikDilTablosuModel>();
				while (SModel.Reader.Read())
				{
					if (KayitBilgisiAl().Sonuc.Equals(Sonuclar.Basarili))
					{
						VeriListe.Add(SDataModel.Veriler);
					}
					else
					{
						SDataListModel = new SurecVeriModel<IList<EtkinlikDilTablosuModel>>{
							Sonuc = SDataModel.Sonuc,
							KullaniciMesaji = SDataModel.KullaniciMesaji,
							HataBilgi = SDataModel.HataBilgi
						};
						VTIslem.CloseConnection();
						return SDataListModel;
					}
				}
				SDataListModel = new SurecVeriModel<IList<EtkinlikDilTablosuModel>>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri listesi başarıyla çekildi",
					Veriler = VeriListe
				};
			}
			else
			{
				SDataListModel = new SurecVeriModel<IList<EtkinlikDilTablosuModel>>{
					Sonuc = SModel.Sonuc,
					KullaniciMesaji = SModel.KullaniciMesaji,
					HataBilgi = SModel.HataBilgi
				};
			}
			VTIslem.CloseConnection();
			return SDataListModel;
		}

		SurecVeriModel<EtkinlikDilTablosuModel> KayitBilgisiAl()
		{
			try
			{
				SDataModel = new SurecVeriModel<EtkinlikDilTablosuModel>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri bilgisi başarıyla çekilmiştir.",
					Veriler = new EtkinlikDilTablosuModel
					{
						EtkinlikDilID = SModel.Reader.GetInt32(0),
						EtkinlikID = SModel.Reader.GetInt32(1),
						DilID = SModel.Reader.GetString(2),
						Etkinlik = SModel.Reader.GetString(3),
						GuncellenmeTarihi = SModel.Reader.GetDateTime(4),
						EtkinlikTarihi = SModel.Reader.GetDateTime(5),
					}
				};

			}
			catch (InvalidCastException ex)
			{
				SDataModel = new SurecVeriModel<EtkinlikDilTablosuModel>{
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
				SDataModel = new SurecVeriModel<EtkinlikDilTablosuModel>{
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

		public virtual SurecVeriModel<EtkinlikDilTablosuModel> KayitBilgisiAl(int Baslangic, DbDataReader Reader)
		{
			try
			{
				SDataModel = new SurecVeriModel<EtkinlikDilTablosuModel>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri bilgisi başarıyla çekilmiştir.",
					Veriler = new EtkinlikDilTablosuModel{
						EtkinlikDilID = Reader.GetInt32(Baslangic + 0),
						EtkinlikID = Reader.GetInt32(Baslangic + 1),
						DilID = Reader.GetString(Baslangic + 2),
						Etkinlik = Reader.GetString(Baslangic + 3),
						GuncellenmeTarihi = Reader.GetDateTime(Baslangic + 4),
						EtkinlikTarihi = Reader.GetDateTime(Baslangic + 5),
					}
				};
			}
			catch (InvalidCastException ex)
			{
				SDataModel = new SurecVeriModel<EtkinlikDilTablosuModel>{
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
				SDataModel = new SurecVeriModel<EtkinlikDilTablosuModel>{
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

		public virtual SurecVeriModel<IList<EtkinlikDilTablosuModel>> EtkinlikBilgileri(int EtkinlikID)
		{
			VTIslem.SetCommandText($"SELECT {EtkinlikDilTablosuModel.SQLSutunSorgusu} FROM EtkinlikDilTablosu WHERE EtkinlikID=@EtkinlikID");
			VTIslem.AddWithValue("EtkinlikID", EtkinlikID);
			VTIslem.OpenConnection();
			SModel = VTIslem.ExecuteReader(CommandBehavior.Default);
			if (SModel.Sonuc.Equals(Sonuclar.Basarili))
			{
				VeriListe = new List<EtkinlikDilTablosuModel>();
				while (SModel.Reader.Read())
				{
					if (KayitBilgisiAl().Sonuc.Equals(Sonuclar.Basarili))
					{
						VeriListe.Add(SDataModel.Veriler);
					}
					else
					{
						SDataListModel = new SurecVeriModel<IList<EtkinlikDilTablosuModel>>{
							Sonuc = SDataModel.Sonuc,
							KullaniciMesaji = SDataModel.KullaniciMesaji,
							HataBilgi = SDataModel.HataBilgi
						};
						VTIslem.CloseConnection();
						return SDataListModel;
					}
				}
				SDataListModel = new SurecVeriModel<IList<EtkinlikDilTablosuModel>>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri listesi başarıyla çekildi",
					Veriler = VeriListe
				};
			}
			else
			{
				SDataListModel = new SurecVeriModel<IList<EtkinlikDilTablosuModel>>{
					Sonuc = SModel.Sonuc,
					KullaniciMesaji = SModel.KullaniciMesaji,
					HataBilgi = SModel.HataBilgi
				};
			}
			VTIslem.CloseConnection();
			return SDataListModel;
		}

		public virtual SurecVeriModel<IList<EtkinlikDilTablosuModel>> DilBilgileri(string DilID)
		{
			VTIslem.SetCommandText($"SELECT {EtkinlikDilTablosuModel.SQLSutunSorgusu} FROM EtkinlikDilTablosu WHERE DilID=@DilID");
			VTIslem.AddWithValue("DilID", DilID);
			VTIslem.OpenConnection();
			SModel = VTIslem.ExecuteReader(CommandBehavior.Default);
			if (SModel.Sonuc.Equals(Sonuclar.Basarili))
			{
				VeriListe = new List<EtkinlikDilTablosuModel>();
				while (SModel.Reader.Read())
				{
					if (KayitBilgisiAl().Sonuc.Equals(Sonuclar.Basarili))
					{
						VeriListe.Add(SDataModel.Veriler);
					}
					else
					{
						SDataListModel = new SurecVeriModel<IList<EtkinlikDilTablosuModel>>{
							Sonuc = SDataModel.Sonuc,
							KullaniciMesaji = SDataModel.KullaniciMesaji,
							HataBilgi = SDataModel.HataBilgi
						};
						VTIslem.CloseConnection();
						return SDataListModel;
					}
				}
				SDataListModel = new SurecVeriModel<IList<EtkinlikDilTablosuModel>>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri listesi başarıyla çekildi",
					Veriler = VeriListe
				};
			}
			else
			{
				SDataListModel = new SurecVeriModel<IList<EtkinlikDilTablosuModel>>{
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