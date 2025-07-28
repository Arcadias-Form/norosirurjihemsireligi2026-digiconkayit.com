using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using VeritabaniIslemSaglayici.Access;

namespace VeritabaniIslemMerkeziBase
{
	public abstract class TransferTipiDilTablosuIslemlerBase
	{
		public VTOperatorleri VTIslem;

		public List<TransferTipiDilTablosuModel> VeriListe;

		public SurecBilgiModel SModel;
		public SurecVeriModel<TransferTipiDilTablosuModel> SDataModel;
		public SurecVeriModel<IList<TransferTipiDilTablosuModel>> SDataListModel;

		public TransferTipiDilTablosuIslemlerBase()
		{
			VTIslem = new VTOperatorleri();
		}

		public TransferTipiDilTablosuIslemlerBase(OleDbTransaction Transcation)
		{
			VTIslem = new VTOperatorleri(Transcation);
		}

		public virtual SurecBilgiModel YeniKayitEkle(TransferTipiDilTablosuModel YeniKayit)
		{
			VTIslem.SetCommandText("INSERT INTO TransferTipiDilTablosu (TransferTipiID, DilID, TransferTipi, GuncellenmeTarihi, EklenmeTarihi) VALUES (@TransferTipiID, @DilID, @TransferTipi, @GuncellenmeTarihi, @EklenmeTarihi)");
			VTIslem.AddWithValue("TransferTipiID", YeniKayit.TransferTipiID);
			VTIslem.AddWithValue("DilID", YeniKayit.DilID);
			VTIslem.AddWithValue("TransferTipi", YeniKayit.TransferTipi);
			VTIslem.AddWithValue("GuncellenmeTarihi", YeniKayit.GuncellenmeTarihi);
			VTIslem.AddWithValue("EklenmeTarihi", YeniKayit.EklenmeTarihi);
			return VTIslem.ExecuteNonQuery();
		}

		public virtual SurecBilgiModel KayitGuncelle(TransferTipiDilTablosuModel GuncelKayit)
		{
			VTIslem.SetCommandText("UPDATE TransferTipiDilTablosu SET TransferTipiID=@TransferTipiID, DilID=@DilID, TransferTipi=@TransferTipi, GuncellenmeTarihi=@GuncellenmeTarihi, EklenmeTarihi=@EklenmeTarihi WHERE TransferTipiDilID=@TransferTipiDilID");
			VTIslem.AddWithValue("TransferTipiID", GuncelKayit.TransferTipiID);
			VTIslem.AddWithValue("DilID", GuncelKayit.DilID);
			VTIslem.AddWithValue("TransferTipi", GuncelKayit.TransferTipi);
			VTIslem.AddWithValue("GuncellenmeTarihi", GuncelKayit.GuncellenmeTarihi);
			VTIslem.AddWithValue("EklenmeTarihi", GuncelKayit.EklenmeTarihi);
			VTIslem.AddWithValue("TransferTipiDilID", GuncelKayit.TransferTipiDilID);
			return VTIslem.ExecuteNonQuery();
		}

		public virtual SurecBilgiModel KayitSil(int TransferTipiDilID)
		{
			VTIslem.SetCommandText("DELETE FROM TransferTipiDilTablosu WHERE TransferTipiDilID=@TransferTipiDilID");
			VTIslem.AddWithValue("TransferTipiDilID", TransferTipiDilID);
			return VTIslem.ExecuteNonQuery();
		}

		public virtual SurecVeriModel<TransferTipiDilTablosuModel> KayitBilgisi(int TransferTipiDilID)
		{
			VTIslem.SetCommandText($"SELECT {TransferTipiDilTablosuModel.SQLSutunSorgusu} FROM TransferTipiDilTablosu WHERE TransferTipiDilID = @TransferTipiDilID");
			VTIslem.AddWithValue("TransferTipiDilID", TransferTipiDilID);
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
					SDataModel = new SurecVeriModel<TransferTipiDilTablosuModel>
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
				SDataModel = new SurecVeriModel<TransferTipiDilTablosuModel>
				{
					Sonuc = SModel.Sonuc,
					KullaniciMesaji = SModel.KullaniciMesaji,
					HataBilgi = SModel.HataBilgi
				};
			}
			VTIslem.CloseConnection();
			return SDataModel;
		}

		public virtual SurecVeriModel<IList<TransferTipiDilTablosuModel>> KayitBilgileri()
		{
			VTIslem.SetCommandText($"SELECT {TransferTipiDilTablosuModel.SQLSutunSorgusu} FROM TransferTipiDilTablosu");
			VTIslem.OpenConnection();
			SModel = VTIslem.ExecuteReader(CommandBehavior.Default);
			if (SModel.Sonuc.Equals(Sonuclar.Basarili))
			{
				VeriListe = new List<TransferTipiDilTablosuModel>();
				while (SModel.Reader.Read())
				{
					if (KayitBilgisiAl().Sonuc.Equals(Sonuclar.Basarili))
					{
						VeriListe.Add(SDataModel.Veriler);
					}
					else
					{
						SDataListModel = new SurecVeriModel<IList<TransferTipiDilTablosuModel>>{
							Sonuc = SDataModel.Sonuc,
							KullaniciMesaji = SDataModel.KullaniciMesaji,
							HataBilgi = SDataModel.HataBilgi
						};
						VTIslem.CloseConnection();
						return SDataListModel;
					}
				}
				SDataListModel = new SurecVeriModel<IList<TransferTipiDilTablosuModel>>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri listesi başarıyla çekildi",
					Veriler = VeriListe
				};
			}
			else
			{
				SDataListModel = new SurecVeriModel<IList<TransferTipiDilTablosuModel>>{
					Sonuc = SModel.Sonuc,
					KullaniciMesaji = SModel.KullaniciMesaji,
					HataBilgi = SModel.HataBilgi
				};
			}
			VTIslem.CloseConnection();
			return SDataListModel;
		}

		SurecVeriModel<TransferTipiDilTablosuModel> KayitBilgisiAl()
		{
			try
			{
				SDataModel = new SurecVeriModel<TransferTipiDilTablosuModel>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri bilgisi başarıyla çekilmiştir.",
					Veriler = new TransferTipiDilTablosuModel
					{
						TransferTipiDilID = SModel.Reader.GetInt32(0),
						TransferTipiID = SModel.Reader.GetInt32(1),
						DilID = SModel.Reader.GetString(2),
						TransferTipi = SModel.Reader.GetString(3),
						GuncellenmeTarihi = SModel.Reader.GetDateTime(4),
						EklenmeTarihi = SModel.Reader.GetDateTime(5),
					}
				};

			}
			catch (InvalidCastException ex)
			{
				SDataModel = new SurecVeriModel<TransferTipiDilTablosuModel>{
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
				SDataModel = new SurecVeriModel<TransferTipiDilTablosuModel>{
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

		public virtual SurecVeriModel<TransferTipiDilTablosuModel> KayitBilgisiAl(int Baslangic, DbDataReader Reader)
		{
			try
			{
				SDataModel = new SurecVeriModel<TransferTipiDilTablosuModel>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri bilgisi başarıyla çekilmiştir.",
					Veriler = new TransferTipiDilTablosuModel{
						TransferTipiDilID = Reader.GetInt32(Baslangic + 0),
						TransferTipiID = Reader.GetInt32(Baslangic + 1),
						DilID = Reader.GetString(Baslangic + 2),
						TransferTipi = Reader.GetString(Baslangic + 3),
						GuncellenmeTarihi = Reader.GetDateTime(Baslangic + 4),
						EklenmeTarihi = Reader.GetDateTime(Baslangic + 5),
					}
				};
			}
			catch (InvalidCastException ex)
			{
				SDataModel = new SurecVeriModel<TransferTipiDilTablosuModel>{
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
				SDataModel = new SurecVeriModel<TransferTipiDilTablosuModel>{
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

		public virtual SurecVeriModel<IList<TransferTipiDilTablosuModel>> TransferTipiBilgileri(int TransferTipiID)
		{
			VTIslem.SetCommandText($"SELECT {TransferTipiDilTablosuModel.SQLSutunSorgusu} FROM TransferTipiDilTablosu WHERE TransferTipiID=@TransferTipiID");
			VTIslem.AddWithValue("TransferTipiID", TransferTipiID);
			VTIslem.OpenConnection();
			SModel = VTIslem.ExecuteReader(CommandBehavior.Default);
			if (SModel.Sonuc.Equals(Sonuclar.Basarili))
			{
				VeriListe = new List<TransferTipiDilTablosuModel>();
				while (SModel.Reader.Read())
				{
					if (KayitBilgisiAl().Sonuc.Equals(Sonuclar.Basarili))
					{
						VeriListe.Add(SDataModel.Veriler);
					}
					else
					{
						SDataListModel = new SurecVeriModel<IList<TransferTipiDilTablosuModel>>{
							Sonuc = SDataModel.Sonuc,
							KullaniciMesaji = SDataModel.KullaniciMesaji,
							HataBilgi = SDataModel.HataBilgi
						};
						VTIslem.CloseConnection();
						return SDataListModel;
					}
				}
				SDataListModel = new SurecVeriModel<IList<TransferTipiDilTablosuModel>>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri listesi başarıyla çekildi",
					Veriler = VeriListe
				};
			}
			else
			{
				SDataListModel = new SurecVeriModel<IList<TransferTipiDilTablosuModel>>{
					Sonuc = SModel.Sonuc,
					KullaniciMesaji = SModel.KullaniciMesaji,
					HataBilgi = SModel.HataBilgi
				};
			}
			VTIslem.CloseConnection();
			return SDataListModel;
		}

		public virtual SurecVeriModel<IList<TransferTipiDilTablosuModel>> DilBilgileri(string DilID)
		{
			VTIslem.SetCommandText($"SELECT {TransferTipiDilTablosuModel.SQLSutunSorgusu} FROM TransferTipiDilTablosu WHERE DilID=@DilID");
			VTIslem.AddWithValue("DilID", DilID);
			VTIslem.OpenConnection();
			SModel = VTIslem.ExecuteReader(CommandBehavior.Default);
			if (SModel.Sonuc.Equals(Sonuclar.Basarili))
			{
				VeriListe = new List<TransferTipiDilTablosuModel>();
				while (SModel.Reader.Read())
				{
					if (KayitBilgisiAl().Sonuc.Equals(Sonuclar.Basarili))
					{
						VeriListe.Add(SDataModel.Veriler);
					}
					else
					{
						SDataListModel = new SurecVeriModel<IList<TransferTipiDilTablosuModel>>{
							Sonuc = SDataModel.Sonuc,
							KullaniciMesaji = SDataModel.KullaniciMesaji,
							HataBilgi = SDataModel.HataBilgi
						};
						VTIslem.CloseConnection();
						return SDataListModel;
					}
				}
				SDataListModel = new SurecVeriModel<IList<TransferTipiDilTablosuModel>>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri listesi başarıyla çekildi",
					Veriler = VeriListe
				};
			}
			else
			{
				SDataListModel = new SurecVeriModel<IList<TransferTipiDilTablosuModel>>{
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