using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ModelBase
{
	[Table("OdemeTablosu")]
	public abstract class OdemeTablosuModelBase
	{
		[Key]
		[Required(ErrorMessage = "BosUyari")]
		[MaxLength(255, ErrorMessage = "UzunlukUyari")]
		[Column("OdemeID", Order = 0)]
		public virtual string OdemeID { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[ForeignKey("OdemeTipiTablosu")]
		[Column("OdemeTipiID", Order = 1)]
		public virtual int OdemeTipiID { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[MaxLength(36, ErrorMessage = "UzunlukUyari")]
		[ForeignKey("KatilimciTablosu")]
		[Column("KatilimciID", Order = 2)]
		public virtual string KatilimciID { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[Column("Durum", Order = 3)]
		public virtual bool Durum { get; set; }

		[DataType(DataType.DateTime, ErrorMessage = "GecersizUyari")]
		[Column("OdemeTarihi", Order = 4)]
		public virtual DateTime? OdemeTarihi { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[MaxLength(536870910, ErrorMessage = "UzunlukUyari")]
		[Column("OdemeParametreleri", Order = 5)]
		public virtual string OdemeParametreleri { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[MaxLength(255, ErrorMessage = "UzunlukUyari")]
		[Column("DovizUcret", Order = 6)]
		public virtual string DovizUcret { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[MaxLength(255, ErrorMessage = "UzunlukUyari")]
		[Column("TurkLirasiUcret", Order = 7)]
		public virtual string TurkLirasiUcret { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[MaxLength(255, ErrorMessage = "UzunlukUyari")]
		[Column("KurUcret", Order = 8)]
		public virtual string KurUcret { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[MaxLength(255, ErrorMessage = "UzunlukUyari")]
		[Column("KatilimciTipiOdaTipiUcret", Order = 9)]
		public virtual string KatilimciTipiOdaTipiUcret { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[MaxLength(255, ErrorMessage = "UzunlukUyari")]
		[Column("TransferUcret", Order = 10)]
		public virtual string TransferUcret { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[MaxLength(255, ErrorMessage = "UzunlukUyari")]
		[Column("KursUcret", Order = 11)]
		public virtual string KursUcret { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[MaxLength(255, ErrorMessage = "UzunlukUyari")]
		[Column("EtkinlikUcret", Order = 12)]
		public virtual string EtkinlikUcret { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[MaxLength(255, ErrorMessage = "UzunlukUyari")]
		[Column("Hash", Order = 13)]
		public virtual string Hash { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[DataType(DataType.DateTime, ErrorMessage = "GecersizUyari")]
		[Column("GuncellenmeTarihi", Order = 14)]
		public virtual DateTime GuncellenmeTarihi { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[DataType(DataType.DateTime, ErrorMessage = "GecersizUyari")]
		[Column("EklenmeTarihi", Order = 15)]
		public virtual DateTime EklenmeTarihi { get; set; }


		public static int OzellikSayisi { get { return typeof(OdemeTablosuModelBase).GetProperties().Count(x => !x.GetAccessors()[0].IsStatic); }}

		public static string SQLSutunSorgusu { get { return string.Join(", ", typeof(OdemeTablosuModelBase).GetProperties().Where(x => !x.GetAccessors()[0].IsStatic).OrderBy(x => (x.GetCustomAttributes(typeof(ColumnAttribute), true).First() as ColumnAttribute).Order).Select(x => $"[OdemeTablosu].[{x.Name}]")); }}

		public virtual string BaseJsonModel()
		{
			return JsonConvert.SerializeObject(this);
		}

	}
}