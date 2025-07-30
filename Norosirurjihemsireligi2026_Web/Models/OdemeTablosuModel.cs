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
            MainRow[12] = KatilimciBilgisi.FaturaUnvan;
            MainRow[13] = KatilimciBilgisi.FaturaAdres;
            MainRow[14] = KatilimciBilgisi.VergiDairesi;
            MainRow[15] = KatilimciBilgisi.VergiNo;
            MainRow[16] = OdemeTipiBilgisi.OdemeTipiDilBilgisi.First().OdemeTipi;
            MainRow[17] = $"{DovizUcret}";
            MainRow[18] = $"{OdemeTarihi:dd.MM.yyyy HH:mm}";

            return MainRow;
        }

        public virtual string FullJsonModel()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}