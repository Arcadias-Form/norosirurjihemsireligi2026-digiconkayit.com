using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ModelBase
{
	[Table("EtkinlikDilTablosu")]
	public abstract class EtkinlikDilTablosuModelBase
	{
		[Key]
		[Required(ErrorMessage = "BosUyari")]
		[Column("EtkinlikDilID", Order = 0)]
		public virtual int EtkinlikDilID { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[ForeignKey("EtkinlikTablosu")]
		[Column("EtkinlikID", Order = 1)]
		public virtual int EtkinlikID { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[MaxLength(255, ErrorMessage = "UzunlukUyari")]
		[ForeignKey("DilTablosu")]
		[Column("DilID", Order = 2)]
		public virtual string DilID { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[MaxLength(255, ErrorMessage = "UzunlukUyari")]
		[Column("Etkinlik", Order = 3)]
		public virtual string Etkinlik { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[DataType(DataType.DateTime, ErrorMessage = "GecersizUyari")]
		[Column("GuncellenmeTarihi", Order = 4)]
		public virtual DateTime GuncellenmeTarihi { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[DataType(DataType.DateTime, ErrorMessage = "GecersizUyari")]
		[Column("EtkinlikTarihi", Order = 5)]
		public virtual DateTime EtkinlikTarihi { get; set; }


		public static int OzellikSayisi { get { return typeof(EtkinlikDilTablosuModelBase).GetProperties().Count(x => !x.GetAccessors()[0].IsStatic); }}

		public static string SQLSutunSorgusu { get { return string.Join(", ", typeof(EtkinlikDilTablosuModelBase).GetProperties().Where(x => !x.GetAccessors()[0].IsStatic).OrderBy(x => (x.GetCustomAttributes(typeof(ColumnAttribute), true).First() as ColumnAttribute).Order).Select(x => $"[EtkinlikDilTablosu].[{x.Name}]")); }}

		public virtual string BaseJsonModel()
		{
			return JsonConvert.SerializeObject(this);
		}

	}
}