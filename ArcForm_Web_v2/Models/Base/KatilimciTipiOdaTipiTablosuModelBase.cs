using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ModelBase
{
	[Table("KatilimciTipiOdaTipiTablosu")]
	public abstract class KatilimciTipiOdaTipiTablosuModelBase
	{
		[Key]
		[Required(ErrorMessage = "BosUyari")]
		[Column("KatilimciTipiOdaTipiID", Order = 0)]
		public virtual int KatilimciTipiOdaTipiID { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[ForeignKey("KatilimciTipiTablosu")]
		[Column("KatilimciTipiID", Order = 1)]
		public virtual int KatilimciTipiID { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[ForeignKey("OdaTipiTablosu")]
		[Column("OdaTipiID", Order = 2)]
		public virtual int OdaTipiID { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[Column("CokErkenUcret", Order = 3)]
		public virtual decimal CokErkenUcret { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[Column("ErkenUcret", Order = 4)]
		public virtual decimal ErkenUcret { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[Column("NormalUcret", Order = 5)]
		public virtual decimal NormalUcret { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[DataType(DataType.DateTime, ErrorMessage = "GecersizUyari")]
		[Column("GuncellenmeTarihi", Order = 6)]
		public virtual DateTime GuncellenmeTarihi { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[DataType(DataType.DateTime, ErrorMessage = "GecersizUyari")]
		[Column("EklenmeTarihi", Order = 7)]
		public virtual DateTime EklenmeTarihi { get; set; }


		public static int OzellikSayisi { get { return typeof(KatilimciTipiOdaTipiTablosuModelBase).GetProperties().Count(x => !x.GetAccessors()[0].IsStatic); }}

		public static string SQLSutunSorgusu { get { return string.Join(", ", typeof(KatilimciTipiOdaTipiTablosuModelBase).GetProperties().Where(x => !x.GetAccessors()[0].IsStatic).OrderBy(x => (x.GetCustomAttributes(typeof(ColumnAttribute), true).First() as ColumnAttribute).Order).Select(x => $"[KatilimciTipiOdaTipiTablosu].[{x.Name}]")); }}

		public virtual string BaseJsonModel()
		{
			return JsonConvert.SerializeObject(this);
		}

	}
}