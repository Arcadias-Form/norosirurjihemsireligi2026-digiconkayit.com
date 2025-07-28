using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ModelBase
{
	[Table("KatilimciEtkinlikTablosu")]
	public abstract class KatilimciEtkinlikTablosuModelBase
	{
		[Key]
		[Required(ErrorMessage = "BosUyari")]
		[MaxLength(255, ErrorMessage = "UzunlukUyari")]
		[Column("KatilimciEtkinlikID", Order = 0)]
		public virtual string KatilimciEtkinlikID { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[MaxLength(36, ErrorMessage = "UzunlukUyari")]
		[ForeignKey("KatilimciTablosu")]
		[Column("KatilimciID", Order = 1)]
		public virtual string KatilimciID { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[ForeignKey("EtkinlikTablosu")]
		[Column("EtkinlikID", Order = 2)]
		public virtual int EtkinlikID { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[DataType(DataType.DateTime, ErrorMessage = "GecersizUyari")]
		[Column("GuncellenmeTarihi", Order = 3)]
		public virtual DateTime GuncellenmeTarihi { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[DataType(DataType.DateTime, ErrorMessage = "GecersizUyari")]
		[Column("EklenmeTarihi", Order = 4)]
		public virtual DateTime EklenmeTarihi { get; set; }


		public static int OzellikSayisi { get { return typeof(KatilimciEtkinlikTablosuModelBase).GetProperties().Count(x => !x.GetAccessors()[0].IsStatic); }}

		public static string SQLSutunSorgusu { get { return string.Join(", ", typeof(KatilimciEtkinlikTablosuModelBase).GetProperties().Where(x => !x.GetAccessors()[0].IsStatic).OrderBy(x => (x.GetCustomAttributes(typeof(ColumnAttribute), true).First() as ColumnAttribute).Order).Select(x => $"[KatilimciEtkinlikTablosu].[{x.Name}]")); }}

		public virtual string BaseJsonModel()
		{
			return JsonConvert.SerializeObject(this);
		}

	}
}