using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using VeritabaniIslemSaglayici.Access;

namespace VeritabaniIslemMerkeziBase
{
	public abstract class TransferTipiTablosuIslemlerBase
	{
		public VTOperatorleri VTIslem;

		public List<TransferTipiTablosuModel> VeriListe;

		public SurecBilgiModel SModel;
		public SurecVeriModel<TransferTipiTablosuModel> SDataModel;
		public SurecVeriModel<IList<TransferTipiTablosuModel>> SDataListModel;

		public TransferTipiTablosuIslemlerBase()
		{
			VTIslem = new VTOperatorleri();
		}

		public TransferTipiTablosuIslemlerBase(OleDbTransaction Transcation)
		{
			VTIslem = new VTOperatorleri(Transcation);
		}

		public virtual SurecBilgiModel YeniKayitEkle(TransferTipiTablosuModel YeniKayit)
		{
			VTIslem.SetCommandText("INSERT INTO TransferTipiTablosu (CokErkenUcret, ErkenUcret, NormalUcret, Sira, GuncellenmeTarihi, EklenmeTarihi) VALUES (@CokErkenUcret, @ErkenUcret, @NormalUcret, @Sira, @GuncellenmeTarihi, @EklenmeTarihi)");
			VTIslem.AddWithValue("CokErkenUcret", YeniKayit.CokErkenUcret.ToString().Replace(",", "."));
			VTIslem.AddWithValue("ErkenUcret", YeniKayit.ErkenUcret.ToString().Replace(",", "."));
			VTIslem.AddWithValue("NormalUcret", YeniKayit.NormalUcret.ToString().Replace(",", "."));
			VTIslem.AddWithValue("Sira", YeniKayit.Sira);
			VTIslem.AddWithValue("GuncellenmeTarihi", YeniKayit.GuncellenmeTarihi);
			VTIslem.AddWithValue("EklenmeTarihi", YeniKayit.EklenmeTarihi);
			return VTIslem.ExecuteNonQuery();
		}

		public virtual SurecBilgiModel KayitGuncelle(TransferTipiTablosuModel GuncelKayit)
		{
			VTIslem.SetCommandText("UPDATE TransferTipiTablosu SET CokErkenUcret=@CokErkenUcret, ErkenUcret=@ErkenUcret, NormalUcret=@NormalUcret, Sira=@Sira, GuncellenmeTarihi=@GuncellenmeTarihi, EklenmeTarihi=@EklenmeTarihi WHERE TransferTipiID=@TransferTipiID");
			VTIslem.AddWithValue("CokErkenUcret", GuncelKayit.CokErkenUcret.ToString().Replace(",", "."));
			VTIslem.AddWithValue("ErkenUcret", GuncelKayit.ErkenUcret.ToString().Replace(",", "."));
			VTIslem.AddWithValue("NormalUcret", GuncelKayit.NormalUcret.ToString().Replace(",", "."));
			VTIslem.AddWithValue("Sira", GuncelKayit.Sira);
			VTIslem.AddWithValue("GuncellenmeTarihi", GuncelKayit.GuncellenmeTarihi);
			VTIslem.AddWithValue("EklenmeTarihi", GuncelKayit.EklenmeTarihi);
			VTIslem.AddWithValue("TransferTipiID", GuncelKayit.TransferTipiID);
			return VTIslem.ExecuteNonQuery();
		}

		public virtual SurecBilgiModel KayitSil(int TransferTipiID)
		{
			VTIslem.SetCommandText("DELETE FROM TransferTipiTablosu WHERE TransferTipiID=@TransferTipiID");
			VTIslem.AddWithValue("TransferTipiID", TransferTipiID);
			return VTIslem.ExecuteNonQuery();
		}

		public virtual SurecVeriModel<TransferTipiTablosuModel> KayitBilgisi(int TransferTipiID)
		{
			VTIslem.SetCommandText($"SELECT {TransferTipiTablosuModel.SQLSutunSorgusu} FROM TransferTipiTablosu WHERE TransferTipiID = @TransferTipiID");
			VTIslem.AddWithValue("TransferTipiID", TransferTipiID);
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
					SDataModel = new SurecVeriModel<TransferTipiTablosuModel>
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
				SDataModel = new SurecVeriModel<TransferTipiTablosuModel>
				{
					Sonuc = SModel.Sonuc,
					KullaniciMesaji = SModel.KullaniciMesaji,
					HataBilgi = SModel.HataBilgi
				};
			}
			VTIslem.CloseConnection();
			return SDataModel;
		}

		public virtual SurecVeriModel<IList<TransferTipiTablosuModel>> KayitBilgileri()
		{
			VTIslem.SetCommandText($"SELECT {TransferTipiTablosuModel.SQLSutunSorgusu} FROM TransferTipiTablosu");
			VTIslem.OpenConnection();
			SModel = VTIslem.ExecuteReader(CommandBehavior.Default);
			if (SModel.Sonuc.Equals(Sonuclar.Basarili))
			{
				VeriListe = new List<TransferTipiTablosuModel>();
				while (SModel.Reader.Read())
				{
					if (KayitBilgisiAl().Sonuc.Equals(Sonuclar.Basarili))
					{
						VeriListe.Add(SDataModel.Veriler);
					}
					else
					{
						SDataListModel = new SurecVeriModel<IList<TransferTipiTablosuModel>>{
							Sonuc = SDataModel.Sonuc,
							KullaniciMesaji = SDataModel.KullaniciMesaji,
							HataBilgi = SDataModel.HataBilgi
						};
						VTIslem.CloseConnection();
						return SDataListModel;
					}
				}
				SDataListModel = new SurecVeriModel<IList<TransferTipiTablosuModel>>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri listesi başarıyla çekildi",
					Veriler = VeriListe
				};
			}
			else
			{
				SDataListModel = new SurecVeriModel<IList<TransferTipiTablosuModel>>{
					Sonuc = SModel.Sonuc,
					KullaniciMesaji = SModel.KullaniciMesaji,
					HataBilgi = SModel.HataBilgi
				};
			}
			VTIslem.CloseConnection();
			return SDataListModel;
		}

		SurecVeriModel<TransferTipiTablosuModel> KayitBilgisiAl()
		{
			try
			{
				SDataModel = new SurecVeriModel<TransferTipiTablosuModel>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri bilgisi başarıyla çekilmiştir.",
					Veriler = new TransferTipiTablosuModel
					{
						TransferTipiID = SModel.Reader.GetInt32(0),
						CokErkenUcret = SModel.Reader.GetDecimal(1),
						ErkenUcret = SModel.Reader.GetDecimal(2),
						NormalUcret = SModel.Reader.GetDecimal(3),
						Sira = SModel.Reader.GetInt32(4),
						GuncellenmeTarihi = SModel.Reader.GetDateTime(5),
						EklenmeTarihi = SModel.Reader.GetDateTime(6),
					}
				};

			}
			catch (InvalidCastException ex)
			{
				SDataModel = new SurecVeriModel<TransferTipiTablosuModel>{
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
				SDataModel = new SurecVeriModel<TransferTipiTablosuModel>{
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

		public virtual SurecVeriModel<TransferTipiTablosuModel> KayitBilgisiAl(int Baslangic, DbDataReader Reader)
		{
			try
			{
				SDataModel = new SurecVeriModel<TransferTipiTablosuModel>{
					Sonuc = Sonuclar.Basarili,
					KullaniciMesaji = "Veri bilgisi başarıyla çekilmiştir.",
					Veriler = new TransferTipiTablosuModel{
						TransferTipiID = Reader.GetInt32(Baslangic + 0),
						CokErkenUcret = Reader.GetDecimal(Baslangic + 1),
						ErkenUcret = Reader.GetDecimal(Baslangic + 2),
						NormalUcret = Reader.GetDecimal(Baslangic + 3),
						Sira = Reader.GetInt32(Baslangic + 4),
						GuncellenmeTarihi = Reader.GetDateTime(Baslangic + 5),
						EklenmeTarihi = Reader.GetDateTime(Baslangic + 6),
					}
				};
			}
			catch (InvalidCastException ex)
			{
				SDataModel = new SurecVeriModel<TransferTipiTablosuModel>{
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
				SDataModel = new SurecVeriModel<TransferTipiTablosuModel>{
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

	}
}