using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ModelBase
{
	[Table("KonaklamaTablosu")]
	public abstract class KonaklamaTablosuModelBase
	{
		[Key]
		[Required(ErrorMessage = "BosUyari")]
		[Column("KonaklamaID", Order = 0)]
		public virtual int KonaklamaID { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[MaxLength(36, ErrorMessage = "UzunlukUyari")]
		[ForeignKey("KatilimciTablosu")]
		[Column("KatilimciID", Order = 1)]
		public virtual string KatilimciID { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[ForeignKey("KatilimciTipiOdaTipiTablosu")]
		[Column("KatilimciTipiOdaTipiID", Order = 2)]
		public virtual int KatilimciTipiOdaTipiID { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[DataType(DataType.DateTime, ErrorMessage = "GecersizUyari")]
		[Column("GirisTarihi", Order = 3)]
		public virtual DateTime GirisTarihi { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[DataType(DataType.DateTime, ErrorMessage = "GecersizUyari")]
		[Column("CikisTarihi", Order = 4)]
		public virtual DateTime CikisTarihi { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[MaxLength(255, ErrorMessage = "UzunlukUyari")]
		[Column("Refakatci", Order = 5)]
		public virtual string Refakatci { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[DataType(DataType.DateTime, ErrorMessage = "GecersizUyari")]
		[Column("GuncellenmeTarihi", Order = 6)]
		public virtual DateTime GuncellenmeTarihi { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[DataType(DataType.DateTime, ErrorMessage = "GecersizUyari")]
		[Column("EklenmeTarihi", Order = 7)]
		public virtual DateTime EklenmeTarihi { get; set; }


		public static int OzellikSayisi { get { return typeof(KonaklamaTablosuModelBase).GetProperties().Count(x => !x.GetAccessors()[0].IsStatic); }}

		public static string SQLSutunSorgusu { get { return string.Join(", ", typeof(KonaklamaTablosuModelBase).GetProperties().Where(x => !x.GetAccessors()[0].IsStatic).OrderBy(x => (x.GetCustomAttributes(typeof(ColumnAttribute), true).First() as ColumnAttribute).Order).Select(x => $"[KonaklamaTablosu].[{x.Name}]")); }}

		public virtual string BaseJsonModel()
		{
			return JsonConvert.SerializeObject(this);
		}

	}
}