using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ModelBase
{
	[Table("OdaTipiTablosu")]
	public abstract class OdaTipiTablosuModelBase
	{
		[Key]
		[Required(ErrorMessage = "BosUyari")]
		[Column("OdaTipiID", Order = 0)]
		public virtual int OdaTipiID { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[ForeignKey("OtelTablosu")]
		[Column("OtelID", Order = 1)]
		public virtual int OtelID { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[Column("RefakatciDurum", Order = 2)]
		public virtual bool RefakatciDurum { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[Column("RefakatciCarpan", Order = 3)]
		public virtual int RefakatciCarpan { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[DataType(DataType.DateTime, ErrorMessage = "GecersizUyari")]
		[Column("BaslangicTarihi", Order = 4)]
		public virtual DateTime BaslangicTarihi { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[DataType(DataType.DateTime, ErrorMessage = "GecersizUyari")]
		[Column("BitisTarihi", Order = 5)]
		public virtual DateTime BitisTarihi { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[Column("TarihSecim", Order = 6)]
		public virtual bool TarihSecim { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[Column("Sira", Order = 7)]
		public virtual int Sira { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[DataType(DataType.DateTime, ErrorMessage = "GecersizUyari")]
		[Column("GuncellenmeTarihi", Order = 8)]
		public virtual DateTime GuncellenmeTarihi { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[DataType(DataType.DateTime, ErrorMessage = "GecersizUyari")]
		[Column("EklenmeTarihi", Order = 9)]
		public virtual DateTime EklenmeTarihi { get; set; }


		public static int OzellikSayisi { get { return typeof(OdaTipiTablosuModelBase).GetProperties().Count(x => !x.GetAccessors()[0].IsStatic); }}

		public static string SQLSutunSorgusu { get { return string.Join(", ", typeof(OdaTipiTablosuModelBase).GetProperties().Where(x => !x.GetAccessors()[0].IsStatic).OrderBy(x => (x.GetCustomAttributes(typeof(ColumnAttribute), true).First() as ColumnAttribute).Order).Select(x => $"[OdaTipiTablosu].[{x.Name}]")); }}

		public virtual string BaseJsonModel()
		{
			return JsonConvert.SerializeObject(this);
		}

	}
}