using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using VeritabaniIslemSaglayici.Access;

namespace VeritabaniIslemMerkeziBase
{
	public abstract class OdaTipiTablosuIslemlerBase
	{
		public VTOperatorleri VTIslem;

		public List<OdaTipiTablosuModel> VeriListe;

		public SurecBilgiModel SModel;
		public SurecVeriModel<OdaTipiTablosuModel> SDataModel;
		public SurecVeriModel<IList<OdaTipiTablosuModel>> SDataListModel;

		public OdaTipiTablosuIslemlerBase()
		{
			VTIslem = new VTOperatorleri();
		}

		public OdaTipiTablosuIslemlerBase(OleDbTransaction Transcation)
		{
			VTIslem = new VTOperatorleri(Transcation);
		}

		public virtual SurecBilgiModel YeniKayitEkle(OdaTipiTablosuModel YeniKayit)
		{
			VTIslem.SetCommandText("INSERT INTO OdaTipiTablosu (OtelID, RefakatciDurum, RefakatciCarpan, BaslangicTarihi, BitisTarihi, TarihSecim, Sira, GuncellenmeTarihi, EklenmeTarihi) VALUES (@OtelID, @RefakatciDurum, @RefakatciCarpan, @BaslangicTarihi, @BitisTarihi, @TarihSecim, @Sira, @GuncellenmeTarihi, @EklenmeTarihi)");
			VTIslem.AddWithValue("OtelID", YeniKayit.OtelID);
			VTIslem.AddWithValue("RefakatciDurum", YeniKayit.RefakatciDurum);
			VTIslem.AddWithValue("RefakatciCarpan", YeniKayit.RefakatciCarpan);
			VTIslem.AddWithValue("BaslangicTarihi", YeniKayit.BaslangicTarihi);
			VTIslem.AddWithValue("BitisTarihi", YeniKayit.BitisTarihi);
			VTIslem.AddWithValue("TarihSecim", YeniKayit.TarihSecim);
			VTIslem.AddWithValue("Sira", YeniKayit.Sira);
			VTIslem.AddWithValue("GuncellenmeTarihi", YeniKayit.GuncellenmeTarihi);
			VTIslem.AddWithValue("EklenmeTarihi", YeniKayit.EklenmeTarihi);
			return VTIslem.ExecuteNonQuery();
		}

		public virtual SurecBilgiModel KayitGuncelle(OdaTipiTablosuModel GuncelKayit)
		{
			VTIslem.SetCommandText("UPDATE OdaTipiTablosu SET OtelID=@OtelID, RefakatciDurum=@RefakatciDurum, RefakatciCarpan=@RefakatciCarpan, BaslangicTarihi=@BaslangicTarihi, BitisTarihi=@BitisTarihi, TarihSecim=@TarihSecim, Sira=@Sira, GuncellenmeTarihi=@GuncellenmeTarihi, EklenmeTarihi=@EklenmeTarihi WHERE OdaTipiID=@OdaTipiID");
			VTIslem.AddWithValue("OtelID", GuncelKayit.OtelID);
			VTIslem.AddWithValue("RefakatciDurum", GuncelKayit.RefakatciDurum);
			VTIslem.AddWithValue("RefakatciCarpan", GuncelKayit.RefakatciCarpan);
			VTIslem.AddWithValue("BaslangicTarihi", GuncelKayit.BaslangicTarihi);
			VTIslem.AddWithValue("BitisTarihi", GuncelKayit.BitisTarihi);
			VTIslem.AddWithValue("TarihSecim", GuncelKayit.TarihSecim);
			VTIslem.AddWithValue("Sira", GuncelKayit.Sira);
			VTIslem.AddWithValue("GuncellenmeTarihi", GuncelKayit.GuncellenmeTarihi);
			VTIslem.AddWithValue("EklenmeTarihi", GuncelKayit.EklenmeTarihi);
			VTIslem.AddWithValue("OdaTipiID", GuncelKayit.OdaTipiID);
			return VTIslem.ExecuteNonQuery();
		}

		public virtual SurecBilgiModel KayitSil(int OdaTipiID)
		{
			VTIslem.SetCommandText("DELETE FROM OdaTipiTablosu WHERE OdaTipiID=@OdaTipiID");
			VTIslem.AddWithValue("OdaTipiID", OdaTipiID);
			return VTIslem.ExecuteNonQuery();
		}

		public virtual SurecVeriModel<OdaTipiTablosuModel> KayitBilgisi(int OdaTipiID)
		{
			VTIslem.SetCommandText($"SELECT {OdaTipiTablosuModel.SQLSutunSorgusu} FROM OdaTipiTablosu WHERE OdaTipiID = @OdaTipiID");
			VTIslem.AddWithValue("OdaTipiID", OdaTipiID);
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
					SDataModel = new SurecVeriModel<OdaTipiTablosuModel>
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
				SDataModel = new SurecVeriModel<OdaTipiTablosuModel>
				{
					Sonuc = SModel.Sonuc,
					KullaniciMesaji = SModel.KullaniciMesaji,
					HataBilgi = SModel.HataBilgi
				};
			}
			VTIslem.CloseConnection();
			return SDataModel;
		}

		public virtual SurecVeriModel<IList<OdaTipiTablosuModel>> KayitBilgileri()
		{
			VTIslem.SetCommandText($"SELECT {OdaTipiTablosuModel.SQLSutunSorgusu} FROM OdaTipiTablosu");
			VTIslem.OpenConnection();
			SModel = VTIslem.ExecuteReader(CommandBehavior.Default);
			if (SModel.Sonuc.Equals(Sonuclar.Basarili))
			{
				VeriListe = new List<OdaTipiTablosuModel>();
				while (SModel.Reader.Read())
				{
					if (KayitBilgisiAl().Sonuc.Equals(Sonuclar.Basarili))
					{
						VeriListe.Add(SDataModel.Veriler);
					}
					else
					{
						SDataListModel = new SurecVeriModel<IList<OdaTipiTablosuModel>>{
							Sonuc = SDataModel.Sonuc,
							KullaniciMesaji = SDataModel.KullaniciMesaji,
							HataBilgi = SDataModel.HataBilgi
						};
						VTIslem.CloseConnection();
						return SDataListModel;
					}
				}
				SDataListModel = new SurecVeriModel<IList<OdaTipiTablosuModel>>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri listesi başarıyla çekildi",
					Veriler = VeriListe
				};
			}
			else
			{
				SDataListModel = new SurecVeriModel<IList<OdaTipiTablosuModel>>{
					Sonuc = SModel.Sonuc,
					KullaniciMesaji = SModel.KullaniciMesaji,
					HataBilgi = SModel.HataBilgi
				};
			}
			VTIslem.CloseConnection();
			return SDataListModel;
		}

		SurecVeriModel<OdaTipiTablosuModel> KayitBilgisiAl()
		{
			try
			{
				SDataModel = new SurecVeriModel<OdaTipiTablosuModel>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri bilgisi başarıyla çekilmiştir.",
					Veriler = new OdaTipiTablosuModel
					{
						OdaTipiID = SModel.Reader.GetInt32(0),
						OtelID = SModel.Reader.GetInt32(1),
						RefakatciDurum = SModel.Reader.GetBoolean(2),
						RefakatciCarpan = SModel.Reader.GetInt32(3),
						BaslangicTarihi = SModel.Reader.GetDateTime(4),
						BitisTarihi = SModel.Reader.GetDateTime(5),
						TarihSecim = SModel.Reader.GetBoolean(6),
						Sira = SModel.Reader.GetInt32(7),
						GuncellenmeTarihi = SModel.Reader.GetDateTime(8),
						EklenmeTarihi = SModel.Reader.GetDateTime(9),
					}
				};

			}
			catch (InvalidCastException ex)
			{
				SDataModel = new SurecVeriModel<OdaTipiTablosuModel>{
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
				SDataModel = new SurecVeriModel<OdaTipiTablosuModel>{
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

		public virtual SurecVeriModel<OdaTipiTablosuModel> KayitBilgisiAl(int Baslangic, DbDataReader Reader)
		{
			try
			{
				SDataModel = new SurecVeriModel<OdaTipiTablosuModel>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri bilgisi başarıyla çekilmiştir.",
					Veriler = new OdaTipiTablosuModel{
						OdaTipiID = Reader.GetInt32(Baslangic + 0),
						OtelID = Reader.GetInt32(Baslangic + 1),
						RefakatciDurum = Reader.GetBoolean(Baslangic + 2),
						RefakatciCarpan = Reader.GetInt32(Baslangic + 3),
						BaslangicTarihi = Reader.GetDateTime(Baslangic + 4),
						BitisTarihi = Reader.GetDateTime(Baslangic + 5),
						TarihSecim = Reader.GetBoolean(Baslangic + 6),
						Sira = Reader.GetInt32(Baslangic + 7),
						GuncellenmeTarihi = Reader.GetDateTime(Baslangic + 8),
						EklenmeTarihi = Reader.GetDateTime(Baslangic + 9),
					}
				};
			}
			catch (InvalidCastException ex)
			{
				SDataModel = new SurecVeriModel<OdaTipiTablosuModel>{
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
				SDataModel = new SurecVeriModel<OdaTipiTablosuModel>{
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

		public virtual SurecVeriModel<IList<OdaTipiTablosuModel>> OtelBilgileri(int OtelID)
		{
			VTIslem.SetCommandText($"SELECT {OdaTipiTablosuModel.SQLSutunSorgusu} FROM OdaTipiTablosu WHERE OtelID=@OtelID");
			VTIslem.AddWithValue("OtelID", OtelID);
			VTIslem.OpenConnection();
			SModel = VTIslem.ExecuteReader(CommandBehavior.Default);
			if (SModel.Sonuc.Equals(Sonuclar.Basarili))
			{
				VeriListe = new List<OdaTipiTablosuModel>();
				while (SModel.Reader.Read())
				{
					if (KayitBilgisiAl().Sonuc.Equals(Sonuclar.Basarili))
					{
						VeriListe.Add(SDataModel.Veriler);
					}
					else
					{
						SDataListModel = new SurecVeriModel<IList<OdaTipiTablosuModel>>{
							Sonuc = SDataModel.Sonuc,
							KullaniciMesaji = SDataModel.KullaniciMesaji,
							HataBilgi = SDataModel.HataBilgi
						};
						VTIslem.CloseConnection();
						return SDataListModel;
					}
				}
				SDataListModel = new SurecVeriModel<IList<OdaTipiTablosuModel>>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri listesi başarıyla çekildi",
					Veriler = VeriListe
				};
			}
			else
			{
				SDataListModel = new SurecVeriModel<IList<OdaTipiTablosuModel>>{
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