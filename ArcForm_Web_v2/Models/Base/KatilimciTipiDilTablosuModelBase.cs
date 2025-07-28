using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ModelBase
{
	[Table("KatilimciTipiDilTablosu")]
	public abstract class KatilimciTipiDilTablosuModelBase
	{
		[Key]
		[Required(ErrorMessage = "BosUyari")]
		[Column("KatilimciTipiDilID", Order = 0)]
		public virtual int KatilimciTipiDilID { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[ForeignKey("KatilimciTipiTablosu")]
		[Column("KatilimciTipiID", Order = 1)]
		public virtual int KatilimciTipiID { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[MaxLength(255, ErrorMessage = "UzunlukUyari")]
		[ForeignKey("DilTablosu")]
		[Column("DilID", Order = 2)]
		public virtual string DilID { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[MaxLength(255, ErrorMessage = "UzunlukUyari")]
		[Column("KatilimciTipi", Order = 3)]
		public virtual string KatilimciTipi { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[DataType(DataType.DateTime, ErrorMessage = "GecersizUyari")]
		[Column("GuncellenmeTarihi", Order = 4)]
		public virtual DateTime GuncellenmeTarihi { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[DataType(DataType.DateTime, ErrorMessage = "GecersizUyari")]
		[Column("EklenmeTarihi", Order = 5)]
		public virtual DateTime EklenmeTarihi { get; set; }


		public static int OzellikSayisi { get { return typeof(KatilimciTipiDilTablosuModelBase).GetProperties().Count(x => !x.GetAccessors()[0].IsStatic); }}

		public static string SQLSutunSorgusu { get { return string.Join(", ", typeof(KatilimciTipiDilTablosuModelBase).GetProperties().Where(x => !x.GetAccessors()[0].IsStatic).OrderBy(x => (x.GetCustomAttributes(typeof(ColumnAttribute), true).First() as ColumnAttribute).Order).Select(x => $"[KatilimciTipiDilTablosu].[{x.Name}]")); }}

		public virtual string BaseJsonModel()
		{
			return JsonConvert.SerializeObject(this);
		}

	}
}