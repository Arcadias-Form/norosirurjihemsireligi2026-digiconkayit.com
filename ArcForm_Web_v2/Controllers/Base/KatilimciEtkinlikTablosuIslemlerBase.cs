using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using VeritabaniIslemSaglayici.Access;

namespace VeritabaniIslemMerkeziBase
{
	public abstract class KatilimciEtkinlikTablosuIslemlerBase
	{
		public VTOperatorleri VTIslem;

		public List<KatilimciEtkinlikTablosuModel> VeriListe;

		public SurecBilgiModel SModel;
		public SurecVeriModel<KatilimciEtkinlikTablosuModel> SDataModel;
		public SurecVeriModel<IList<KatilimciEtkinlikTablosuModel>> SDataListModel;

		public KatilimciEtkinlikTablosuIslemlerBase()
		{
			VTIslem = new VTOperatorleri();
		}

		public KatilimciEtkinlikTablosuIslemlerBase(OleDbTransaction Transcation)
		{
			VTIslem = new VTOperatorleri(Transcation);
		}

		public virtual SurecBilgiModel YeniKayitEkle(KatilimciEtkinlikTablosuModel YeniKayit)
		{
			VTIslem.SetCommandText("INSERT INTO KatilimciEtkinlikTablosu (KatilimciEtkinlikID, KatilimciID, EtkinlikID, GuncellenmeTarihi, EklenmeTarihi) VALUES (@KatilimciEtkinlikID, @KatilimciID, @EtkinlikID, @GuncellenmeTarihi, @EklenmeTarihi)");
			VTIslem.AddWithValue("KatilimciEtkinlikID", YeniKayit.KatilimciEtkinlikID);
			VTIslem.AddWithValue("KatilimciID", YeniKayit.KatilimciID);
			VTIslem.AddWithValue("EtkinlikID", YeniKayit.EtkinlikID);
			VTIslem.AddWithValue("GuncellenmeTarihi", YeniKayit.GuncellenmeTarihi);
			VTIslem.AddWithValue("EklenmeTarihi", YeniKayit.EklenmeTarihi);
			return VTIslem.ExecuteNonQuery();
		}

		public virtual SurecBilgiModel KayitGuncelle(KatilimciEtkinlikTablosuModel GuncelKayit)
		{
			VTIslem.SetCommandText("UPDATE KatilimciEtkinlikTablosu SET KatilimciID=@KatilimciID, EtkinlikID=@EtkinlikID, GuncellenmeTarihi=@GuncellenmeTarihi, EklenmeTarihi=@EklenmeTarihi WHERE KatilimciEtkinlikID=@KatilimciEtkinlikID");
			VTIslem.AddWithValue("KatilimciID", GuncelKayit.KatilimciID);
			VTIslem.AddWithValue("EtkinlikID", GuncelKayit.EtkinlikID);
			VTIslem.AddWithValue("GuncellenmeTarihi", GuncelKayit.GuncellenmeTarihi);
			VTIslem.AddWithValue("EklenmeTarihi", GuncelKayit.EklenmeTarihi);
			VTIslem.AddWithValue("KatilimciEtkinlikID", GuncelKayit.KatilimciEtkinlikID);
			return VTIslem.ExecuteNonQuery();
		}

		public virtual SurecBilgiModel KayitSil(string KatilimciEtkinlikID)
		{
			VTIslem.SetCommandText("DELETE FROM KatilimciEtkinlikTablosu WHERE KatilimciEtkinlikID=@KatilimciEtkinlikID");
			VTIslem.AddWithValue("KatilimciEtkinlikID", KatilimciEtkinlikID);
			return VTIslem.ExecuteNonQuery();
		}

		public virtual SurecVeriModel<KatilimciEtkinlikTablosuModel> KayitBilgisi(string KatilimciEtkinlikID)
		{
			VTIslem.SetCommandText($"SELECT {KatilimciEtkinlikTablosuModel.SQLSutunSorgusu} FROM KatilimciEtkinlikTablosu WHERE KatilimciEtkinlikID = @KatilimciEtkinlikID");
			VTIslem.AddWithValue("KatilimciEtkinlikID", KatilimciEtkinlikID);
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
					SDataModel = new SurecVeriModel<KatilimciEtkinlikTablosuModel>
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
				SDataModel = new SurecVeriModel<KatilimciEtkinlikTablosuModel>
				{
					Sonuc = SModel.Sonuc,
					KullaniciMesaji = SModel.KullaniciMesaji,
					HataBilgi = SModel.HataBilgi
				};
			}
			VTIslem.CloseConnection();
			return SDataModel;
		}

		public virtual SurecVeriModel<IList<KatilimciEtkinlikTablosuModel>> KayitBilgileri()
		{
			VTIslem.SetCommandText($"SELECT {KatilimciEtkinlikTablosuModel.SQLSutunSorgusu} FROM KatilimciEtkinlikTablosu");
			VTIslem.OpenConnection();
			SModel = VTIslem.ExecuteReader(CommandBehavior.Default);
			if (SModel.Sonuc.Equals(Sonuclar.Basarili))
			{
				VeriListe = new List<KatilimciEtkinlikTablosuModel>();
				while (SModel.Reader.Read())
				{
					if (KayitBilgisiAl().Sonuc.Equals(Sonuclar.Basarili))
					{
						VeriListe.Add(SDataModel.Veriler);
					}
					else
					{
						SDataListModel = new SurecVeriModel<IList<KatilimciEtkinlikTablosuModel>>{
							Sonuc = SDataModel.Sonuc,
							KullaniciMesaji = SDataModel.KullaniciMesaji,
							HataBilgi = SDataModel.HataBilgi
						};
						VTIslem.CloseConnection();
						return SDataListModel;
					}
				}
				SDataListModel = new SurecVeriModel<IList<KatilimciEtkinlikTablosuModel>>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri listesi başarıyla çekildi",
					Veriler = VeriListe
				};
			}
			else
			{
				SDataListModel = new SurecVeriModel<IList<KatilimciEtkinlikTablosuModel>>{
					Sonuc = SModel.Sonuc,
					KullaniciMesaji = SModel.KullaniciMesaji,
					HataBilgi = SModel.HataBilgi
				};
			}
			VTIslem.CloseConnection();
			return SDataListModel;
		}

		SurecVeriModel<KatilimciEtkinlikTablosuModel> KayitBilgisiAl()
		{
			try
			{
				SDataModel = new SurecVeriModel<KatilimciEtkinlikTablosuModel>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri bilgisi başarıyla çekilmiştir.",
					Veriler = new KatilimciEtkinlikTablosuModel
					{
						KatilimciEtkinlikID = SModel.Reader.GetString(0),
						KatilimciID = SModel.Reader.GetString(1),
						EtkinlikID = SModel.Reader.GetInt32(2),
						GuncellenmeTarihi = SModel.Reader.GetDateTime(3),
						EklenmeTarihi = SModel.Reader.GetDateTime(4),
					}
				};

			}
			catch (InvalidCastException ex)
			{
				SDataModel = new SurecVeriModel<KatilimciEtkinlikTablosuModel>{
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
				SDataModel = new SurecVeriModel<KatilimciEtkinlikTablosuModel>{
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

		public virtual SurecVeriModel<KatilimciEtkinlikTablosuModel> KayitBilgisiAl(int Baslangic, DbDataReader Reader)
		{
			try
			{
				SDataModel = new SurecVeriModel<KatilimciEtkinlikTablosuModel>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri bilgisi başarıyla çekilmiştir.",
					Veriler = new KatilimciEtkinlikTablosuModel{
						KatilimciEtkinlikID = Reader.GetString(Baslangic + 0),
						KatilimciID = Reader.GetString(Baslangic + 1),
						EtkinlikID = Reader.GetInt32(Baslangic + 2),
						GuncellenmeTarihi = Reader.GetDateTime(Baslangic + 3),
						EklenmeTarihi = Reader.GetDateTime(Baslangic + 4),
					}
				};
			}
			catch (InvalidCastException ex)
			{
				SDataModel = new SurecVeriModel<KatilimciEtkinlikTablosuModel>{
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
				SDataModel = new SurecVeriModel<KatilimciEtkinlikTablosuModel>{
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

		public virtual SurecVeriModel<IList<KatilimciEtkinlikTablosuModel>> KatilimciBilgileri(string KatilimciID)
		{
			VTIslem.SetCommandText($"SELECT {KatilimciEtkinlikTablosuModel.SQLSutunSorgusu} FROM KatilimciEtkinlikTablosu WHERE KatilimciID=@KatilimciID");
			VTIslem.AddWithValue("KatilimciID", KatilimciID);
			VTIslem.OpenConnection();
			SModel = VTIslem.ExecuteReader(CommandBehavior.Default);
			if (SModel.Sonuc.Equals(Sonuclar.Basarili))
			{
				VeriListe = new List<KatilimciEtkinlikTablosuModel>();
				while (SModel.Reader.Read())
				{
					if (KayitBilgisiAl().Sonuc.Equals(Sonuclar.Basarili))
					{
						VeriListe.Add(SDataModel.Veriler);
					}
					else
					{
						SDataListModel = new SurecVeriModel<IList<KatilimciEtkinlikTablosuModel>>{
							Sonuc = SDataModel.Sonuc,
							KullaniciMesaji = SDataModel.KullaniciMesaji,
							HataBilgi = SDataModel.HataBilgi
						};
						VTIslem.CloseConnection();
						return SDataListModel;
					}
				}
				SDataListModel = new SurecVeriModel<IList<KatilimciEtkinlikTablosuModel>>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri listesi başarıyla çekildi",
					Veriler = VeriListe
				};
			}
			else
			{
				SDataListModel = new SurecVeriModel<IList<KatilimciEtkinlikTablosuModel>>{
					Sonuc = SModel.Sonuc,
					KullaniciMesaji = SModel.KullaniciMesaji,
					HataBilgi = SModel.HataBilgi
				};
			}
			VTIslem.CloseConnection();
			return SDataListModel;
		}

		public virtual SurecVeriModel<IList<KatilimciEtkinlikTablosuModel>> EtkinlikBilgileri(int EtkinlikID)
		{
			VTIslem.SetCommandText($"SELECT {KatilimciEtkinlikTablosuModel.SQLSutunSorgusu} FROM KatilimciEtkinlikTablosu WHERE EtkinlikID=@EtkinlikID");
			VTIslem.AddWithValue("EtkinlikID", EtkinlikID);
			VTIslem.OpenConnection();
			SModel = VTIslem.ExecuteReader(CommandBehavior.Default);
			if (SModel.Sonuc.Equals(Sonuclar.Basarili))
			{
				VeriListe = new List<KatilimciEtkinlikTablosuModel>();
				while (SModel.Reader.Read())
				{
					if (KayitBilgisiAl().Sonuc.Equals(Sonuclar.Basarili))
					{
						VeriListe.Add(SDataModel.Veriler);
					}
					else
					{
						SDataListModel = new SurecVeriModel<IList<KatilimciEtkinlikTablosuModel>>{
							Sonuc = SDataModel.Sonuc,
							KullaniciMesaji = SDataModel.KullaniciMesaji,
							HataBilgi = SDataModel.HataBilgi
						};
						VTIslem.CloseConnection();
						return SDataListModel;
					}
				}
				SDataListModel = new SurecVeriModel<IList<KatilimciEtkinlikTablosuModel>>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri listesi başarıyla çekildi",
					Veriler = VeriListe
				};
			}
			else
			{
				SDataListModel = new SurecVeriModel<IList<KatilimciEtkinlikTablosuModel>>{
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