using Newtonsoft.Json;
using System;
using ModelRelation;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Model
{
	public partial class OdemeTablosuModel : OdemeTablosuModelRelation
	{
        public DataRow DRow(DataRow MainRow)
        {
            MainRow[0] = OdemeID;
            MainRow[1] = KatilimciBilgisi.AdSoyad;
            MainRow[2] = KatilimciBilgisi.KimlikNo;
            MainRow[3] = KatilimciBilgisi.ePosta;
            MainRow[4] = KatilimciBilgisi.CepTelefonu;
            MainRow[5] = KatilimciBilgisi.Kurum;
            MainRow[6] = KatilimciBilgisi.KonaklamaBilgisi.KatilimciTipiOdaTipiBilgisi.KatilimciTipiBilgisi.KatilimciTipiDilBilgisi.First().KatilimciTipi;
            MainRow[7] = KatilimciBilgisi.KonaklamaBilgisi.KatilimciTipiOdaTipiBilgisi.OdaTipiBilgisi.OtelBilgisi.Otel;
            MainRow[8] = KatilimciBilgisi.KonaklamaBilgisi.KatilimciTipiOdaTipiBilgisi.OdaTipiBilgisi.OdaTipiDilBilgisi.First().OdaTipi;
            MainRow[9] = KatilimciBilgisi.KonaklamaBilgisi.GirisTarihi.ToShortDateString();
            MainRow[10] = KatilimciBilgisi.KonaklamaBilgisi.CikisTarihi.ToShortDateString();
            MainRow[11] = KatilimciBilgisi.KonaklamaBilgisi.Refakatci;

            MainRow[12] = KatilimciBilgisi.TransferBilgisi.TransferTipiBilgisi.TransferTipiDilBilgisi.First().TransferTipi;



            MainRow[13] = KatilimciBilgisi.FaturaUnvan;
            MainRow[14] = KatilimciBilgisi.FaturaAdres;
            MainRow[15] = KatilimciBilgisi.VergiDairesi;
            MainRow[16] = KatilimciBilgisi.VergiNo;
            MainRow[17] = OdemeTipiBilgisi.OdemeTipiDilBilgisi.First().OdemeTipi;
            MainRow[18] = $"{DovizUcret}";
            MainRow[19] = $"{OdemeTarihi:dd.MM.yyyy HH:mm}";

            return MainRow;
        }

        public virtual string FullJsonModel()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}