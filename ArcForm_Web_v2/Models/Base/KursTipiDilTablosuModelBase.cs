using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ModelBase
{
	[Table("KursTipiDilTablosu")]
	public abstract class KursTipiDilTablosuModelBase
	{
		[Key]
		[Required(ErrorMessage = "BosUyari")]
		[Column("KursTipiDilID", Order = 0)]
		public virtual int KursTipiDilID { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[ForeignKey("KursTipiTablosu")]
		[Column("KursTipiID", Order = 1)]
		public virtual int KursTipiID { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[MaxLength(255, ErrorMessage = "UzunlukUyari")]
		[ForeignKey("DilTablosu")]
		[Column("DilID", Order = 2)]
		public virtual string DilID { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[MaxLength(255, ErrorMessage = "UzunlukUyari")]
		[Column("KursTipi", Order = 3)]
		public virtual string KursTipi { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[DataType(DataType.DateTime, ErrorMessage = "GecersizUyari")]
		[Column("GuncellenmeTarihi", Order = 4)]
		public virtual DateTime GuncellenmeTarihi { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[DataType(DataType.DateTime, ErrorMessage = "GecersizUyari")]
		[Column("EklenmeTarihi", Order = 5)]
		public virtual DateTime EklenmeTarihi { get; set; }


		public static int OzellikSayisi { get { return typeof(KursTipiDilTablosuModelBase).GetProperties().Count(x => !x.GetAccessors()[0].IsStatic); }}

		public static string SQLSutunSorgusu { get { return string.Join(", ", typeof(KursTipiDilTablosuModelBase).GetProperties().Where(x => !x.GetAccessors()[0].IsStatic).OrderBy(x => (x.GetCustomAttributes(typeof(ColumnAttribute), true).First() as ColumnAttribute).Order).Select(x => $"[KursTipiDilTablosu].[{x.Name}]")); }}

		public virtual string BaseJsonModel()
		{
			return JsonConvert.SerializeObject(this);
		}

	}
}