using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using VeritabaniIslemSaglayici.Access;

namespace VeritabaniIslemMerkeziBase
{
	public abstract class KatilimciTablosuIslemlerBase
	{
		public VTOperatorleri VTIslem;

		public List<KatilimciTablosuModel> VeriListe;

		public SurecBilgiModel SModel;
		public SurecVeriModel<KatilimciTablosuModel> SDataModel;
		public SurecVeriModel<IList<KatilimciTablosuModel>> SDataListModel;

		public KatilimciTablosuIslemlerBase()
		{
			VTIslem = new VTOperatorleri();
		}

		public KatilimciTablosuIslemlerBase(OleDbTransaction Transcation)
		{
			VTIslem = new VTOperatorleri(Transcation);
		}

		public virtual SurecBilgiModel YeniKayitEkle(KatilimciTablosuModel YeniKayit)
		{
			VTIslem.SetCommandText("INSERT INTO KatilimciTablosu (KatilimciID, DilID, AdSoyad, Cinsiyet, ePosta, CepTelefonu, Kurum, KimlikNo, FaturaTipi, FaturaUnvan, FaturaAdres, VergiDairesi, VergiNo, GuncellenmeTarihi, EklenmeTarihi) VALUES (@KatilimciID, @DilID, @AdSoyad, @Cinsiyet, @ePosta, @CepTelefonu, @Kurum, @KimlikNo, @FaturaTipi, @FaturaUnvan, @FaturaAdres, @VergiDairesi, @VergiNo, @GuncellenmeTarihi, @EklenmeTarihi)");
			VTIslem.AddWithValue("KatilimciID", YeniKayit.KatilimciID);
			VTIslem.AddWithValue("DilID", YeniKayit.DilID);
			VTIslem.AddWithValue("AdSoyad", YeniKayit.AdSoyad);
			VTIslem.AddWithValue("Cinsiyet", YeniKayit.Cinsiyet);
			VTIslem.AddWithValue("ePosta", YeniKayit.ePosta);
			VTIslem.AddWithValue("CepTelefonu", YeniKayit.CepTelefonu);
			VTIslem.AddWithValue("Kurum", YeniKayit.Kurum);
			VTIslem.AddWithValue("KimlikNo", YeniKayit.KimlikNo);
			VTIslem.AddWithValue("FaturaTipi", YeniKayit.FaturaTipi);
			VTIslem.AddWithValue("FaturaUnvan", YeniKayit.FaturaUnvan);
			VTIslem.AddWithValue("FaturaAdres", YeniKayit.FaturaAdres);
			VTIslem.AddWithValue("VergiDairesi", YeniKayit.VergiDairesi);
			VTIslem.AddWithValue("VergiNo", YeniKayit.VergiNo);
			VTIslem.AddWithValue("GuncellenmeTarihi", YeniKayit.GuncellenmeTarihi);
			VTIslem.AddWithValue("EklenmeTarihi", YeniKayit.EklenmeTarihi);
			return VTIslem.ExecuteNonQuery();
		}

		public virtual SurecBilgiModel KayitGuncelle(KatilimciTablosuModel GuncelKayit)
		{
			VTIslem.SetCommandText("UPDATE KatilimciTablosu SET DilID=@DilID, AdSoyad=@AdSoyad, Cinsiyet=@Cinsiyet, ePosta=@ePosta, CepTelefonu=@CepTelefonu, Kurum=@Kurum, KimlikNo=@KimlikNo, FaturaTipi=@FaturaTipi, FaturaUnvan=@FaturaUnvan, FaturaAdres=@FaturaAdres, VergiDairesi=@VergiDairesi, VergiNo=@VergiNo, GuncellenmeTarihi=@GuncellenmeTarihi, EklenmeTarihi=@EklenmeTarihi WHERE KatilimciID=@KatilimciID");
			VTIslem.AddWithValue("DilID", GuncelKayit.DilID);
			VTIslem.AddWithValue("AdSoyad", GuncelKayit.AdSoyad);
			VTIslem.AddWithValue("Cinsiyet", GuncelKayit.Cinsiyet);
			VTIslem.AddWithValue("ePosta", GuncelKayit.ePosta);
			VTIslem.AddWithValue("CepTelefonu", GuncelKayit.CepTelefonu);
			VTIslem.AddWithValue("Kurum", GuncelKayit.Kurum);
			VTIslem.AddWithValue("KimlikNo", GuncelKayit.KimlikNo);
			VTIslem.AddWithValue("FaturaTipi", GuncelKayit.FaturaTipi);
			VTIslem.AddWithValue("FaturaUnvan", GuncelKayit.FaturaUnvan);
			VTIslem.AddWithValue("FaturaAdres", GuncelKayit.FaturaAdres);
			VTIslem.AddWithValue("VergiDairesi", GuncelKayit.VergiDairesi);
			VTIslem.AddWithValue("VergiNo", GuncelKayit.VergiNo);
			VTIslem.AddWithValue("GuncellenmeTarihi", GuncelKayit.GuncellenmeTarihi);
			VTIslem.AddWithValue("EklenmeTarihi", GuncelKayit.EklenmeTarihi);
			VTIslem.AddWithValue("KatilimciID", GuncelKayit.KatilimciID);
			return VTIslem.ExecuteNonQuery();
		}

		public virtual SurecBilgiModel KayitSil(string KatilimciID)
		{
			VTIslem.SetCommandText("DELETE FROM KatilimciTablosu WHERE KatilimciID=@KatilimciID");
			VTIslem.AddWithValue("KatilimciID", KatilimciID);
			return VTIslem.ExecuteNonQuery();
		}

		public virtual SurecVeriModel<KatilimciTablosuModel> KayitBilgisi(string KatilimciID)
		{
			VTIslem.SetCommandText($"SELECT {KatilimciTablosuModel.SQLSutunSorgusu} FROM KatilimciTablosu WHERE KatilimciID = @KatilimciID");
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
					SDataModel = new SurecVeriModel<KatilimciTablosuModel>
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
				SDataModel = new SurecVeriModel<KatilimciTablosuModel>
				{
					Sonuc = SModel.Sonuc,
					KullaniciMesaji = SModel.KullaniciMesaji,
					HataBilgi = SModel.HataBilgi
				};
			}
			VTIslem.CloseConnection();
			return SDataModel;
		}

		public virtual SurecVeriModel<IList<KatilimciTablosuModel>> KayitBilgileri()
		{
			VTIslem.SetCommandText($"SELECT {KatilimciTablosuModel.SQLSutunSorgusu} FROM KatilimciTablosu");
			VTIslem.OpenConnection();
			SModel = VTIslem.ExecuteReader(CommandBehavior.Default);
			if (SModel.Sonuc.Equals(Sonuclar.Basarili))
			{
				VeriListe = new List<KatilimciTablosuModel>();
				while (SModel.Reader.Read())
				{
					if (KayitBilgisiAl().Sonuc.Equals(Sonuclar.Basarili))
					{
						VeriListe.Add(SDataModel.Veriler);
					}
					else
					{
						SDataListModel = new SurecVeriModel<IList<KatilimciTablosuModel>>{
							Sonuc = SDataModel.Sonuc,
							KullaniciMesaji = SDataModel.KullaniciMesaji,
							HataBilgi = SDataModel.HataBilgi
						};
						VTIslem.CloseConnection();
						return SDataListModel;
					}
				}
				SDataListModel = new SurecVeriModel<IList<KatilimciTablosuModel>>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri listesi başarıyla çekildi",
					Veriler = VeriListe
				};
			}
			else
			{
				SDataListModel = new SurecVeriModel<IList<KatilimciTablosuModel>>{
					Sonuc = SModel.Sonuc,
					KullaniciMesaji = SModel.KullaniciMesaji,
					HataBilgi = SModel.HataBilgi
				};
			}
			VTIslem.CloseConnection();
			return SDataListModel;
		}

		SurecVeriModel<KatilimciTablosuModel> KayitBilgisiAl()
		{
			try
			{
				SDataModel = new SurecVeriModel<KatilimciTablosuModel>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri bilgisi başarıyla çekilmiştir.",
					Veriler = new KatilimciTablosuModel
					{
						KatilimciID = SModel.Reader.GetString(0),
						DilID = SModel.Reader.GetString(1),
						AdSoyad = SModel.Reader.GetString(2),
						Cinsiyet = SModel.Reader.GetString(3),
						ePosta = SModel.Reader.GetString(4),
						CepTelefonu = SModel.Reader.GetString(5),
						Kurum = SModel.Reader.GetString(6),
						KimlikNo = SModel.Reader.GetString(7),
						FaturaTipi = SModel.Reader.GetString(8),
						FaturaUnvan = SModel.Reader.GetString(9),
						FaturaAdres = SModel.Reader.GetString(10),
						VergiDairesi = SModel.Reader.GetString(11),
						VergiNo = SModel.Reader.GetString(12),
						GuncellenmeTarihi = SModel.Reader.GetDateTime(13),
						EklenmeTarihi = SModel.Reader.GetDateTime(14),
					}
				};

			}
			catch (InvalidCastException ex)
			{
				SDataModel = new SurecVeriModel<KatilimciTablosuModel>{
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
				SDataModel = new SurecVeriModel<KatilimciTablosuModel>{
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

		public virtual SurecVeriModel<KatilimciTablosuModel> KayitBilgisiAl(int Baslangic, DbDataReader Reader)
		{
			try
			{
				SDataModel = new SurecVeriModel<KatilimciTablosuModel>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri bilgisi başarıyla çekilmiştir.",
					Veriler = new KatilimciTablosuModel{
						KatilimciID = Reader.GetString(Baslangic + 0),
						DilID = Reader.GetString(Baslangic + 1),
						AdSoyad = Reader.GetString(Baslangic + 2),
						Cinsiyet = Reader.GetString(Baslangic + 3),
						ePosta = Reader.GetString(Baslangic + 4),
						CepTelefonu = Reader.GetString(Baslangic + 5),
						Kurum = Reader.GetString(Baslangic + 6),
						KimlikNo = Reader.GetString(Baslangic + 7),
						FaturaTipi = Reader.GetString(Baslangic + 8),
						FaturaUnvan = Reader.GetString(Baslangic + 9),
						FaturaAdres = Reader.GetString(Baslangic + 10),
						VergiDairesi = Reader.GetString(Baslangic + 11),
						VergiNo = Reader.GetString(Baslangic + 12),
						GuncellenmeTarihi = Reader.GetDateTime(Baslangic + 13),
						EklenmeTarihi = Reader.GetDateTime(Baslangic + 14),
					}
				};
			}
			catch (InvalidCastException ex)
			{
				SDataModel = new SurecVeriModel<KatilimciTablosuModel>{
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
				SDataModel = new SurecVeriModel<KatilimciTablosuModel>{
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

		public virtual SurecVeriModel<IList<KatilimciTablosuModel>> DilBilgileri(string DilID)
		{
			VTIslem.SetCommandText($"SELECT {KatilimciTablosuModel.SQLSutunSorgusu} FROM KatilimciTablosu WHERE DilID=@DilID");
			VTIslem.AddWithValue("DilID", DilID);
			VTIslem.OpenConnection();
			SModel = VTIslem.ExecuteReader(CommandBehavior.Default);
			if (SModel.Sonuc.Equals(Sonuclar.Basarili))
			{
				VeriListe = new List<KatilimciTablosuModel>();
				while (SModel.Reader.Read())
				{
					if (KayitBilgisiAl().Sonuc.Equals(Sonuclar.Basarili))
					{
						VeriListe.Add(SDataModel.Veriler);
					}
					else
					{
						SDataListModel = new SurecVeriModel<IList<KatilimciTablosuModel>>{
							Sonuc = SDataModel.Sonuc,
							KullaniciMesaji = SDataModel.KullaniciMesaji,
							HataBilgi = SDataModel.HataBilgi
						};
						VTIslem.CloseConnection();
						return SDataListModel;
					}
				}
				SDataListModel = new SurecVeriModel<IList<KatilimciTablosuModel>>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri listesi başarıyla çekildi",
					Veriler = VeriListe
				};
			}
			else
			{
				SDataListModel = new SurecVeriModel<IList<KatilimciTablosuModel>>{
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