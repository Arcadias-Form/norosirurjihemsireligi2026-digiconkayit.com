using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ModelBase
{
	[Table("TransferTipiTablosu")]
	public abstract class TransferTipiTablosuModelBase
	{
		[Key]
		[Required(ErrorMessage = "BosUyari")]
		[Column("TransferTipiID", Order = 0)]
		public virtual int TransferTipiID { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[Column("CokErkenUcret", Order = 1)]
		public virtual decimal CokErkenUcret { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[Column("ErkenUcret", Order = 2)]
		public virtual decimal ErkenUcret { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[Column("NormalUcret", Order = 3)]
		public virtual decimal NormalUcret { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[Column("Sira", Order = 4)]
		public virtual int Sira { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[DataType(DataType.DateTime, ErrorMessage = "GecersizUyari")]
		[Column("GuncellenmeTarihi", Order = 5)]
		public virtual DateTime GuncellenmeTarihi { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[DataType(DataType.DateTime, ErrorMessage = "GecersizUyari")]
		[Column("EklenmeTarihi", Order = 6)]
		public virtual DateTime EklenmeTarihi { get; set; }


		public static int OzellikSayisi { get { return typeof(TransferTipiTablosuModelBase).GetProperties().Count(x => !x.GetAccessors()[0].IsStatic); }}

		public static string SQLSutunSorgusu { get { return string.Join(", ", typeof(TransferTipiTablosuModelBase).GetProperties().Where(x => !x.GetAccessors()[0].IsStatic).OrderBy(x => (x.GetCustomAttributes(typeof(ColumnAttribute), true).First() as ColumnAttribute).Order).Select(x => $"[TransferTipiTablosu].[{x.Name}]")); }}

		public virtual string BaseJsonModel()
		{
			return JsonConvert.SerializeObject(this);
		}

	}
}