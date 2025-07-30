using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ModelBase
{
	[Table("KatilimciTablosu")]
	public abstract class KatilimciTablosuModelBase
	{
		[Key]
		[Required(ErrorMessage = "BosUyari")]
		[MaxLength(36, ErrorMessage = "UzunlukUyari")]
		[Column("KatilimciID", Order = 0)]
		public virtual string KatilimciID { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[MaxLength(255, ErrorMessage = "UzunlukUyari")]
		[ForeignKey("DilTablosu")]
		[Column("DilID", Order = 1)]
		public virtual string DilID { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[MaxLength(255, ErrorMessage = "UzunlukUyari")]
		[Column("AdSoyad", Order = 2)]
		public virtual string AdSoyad { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[MaxLength(255, ErrorMessage = "UzunlukUyari")]
		[Column("Cinsiyet", Order = 3)]
		public virtual string Cinsiyet { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[EmailAddress(ErrorMessage = "GecersizUyari")]
		[MaxLength(255, ErrorMessage = "UzunlukUyari")]
		[Column("ePosta", Order = 4)]
		public virtual string ePosta { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[MaxLength(255, ErrorMessage = "UzunlukUyari")]
		[Column("CepTelefonu", Order = 5)]
		public virtual string CepTelefonu { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[MaxLength(255, ErrorMessage = "UzunlukUyari")]
		[Column("Kurum", Order = 6)]
		public virtual string Kurum { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[MaxLength(255, ErrorMessage = "UzunlukUyari")]
		[Column("KimlikNo", Order = 7)]
		public virtual string KimlikNo { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[MaxLength(255, ErrorMessage = "UzunlukUyari")]
		[Column("FaturaTipi", Order = 8)]
		public virtual string FaturaTipi { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[MaxLength(255, ErrorMessage = "UzunlukUyari")]
		[Column("FaturaUnvan", Order = 9)]
		public virtual string FaturaUnvan { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[MaxLength(536870910, ErrorMessage = "UzunlukUyari")]
		[Column("FaturaAdres", Order = 10)]
		public virtual string FaturaAdres { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[MaxLength(255, ErrorMessage = "UzunlukUyari")]
		[Column("VergiDairesi", Order = 11)]
		public virtual string VergiDairesi { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[MaxLength(255, ErrorMessage = "UzunlukUyari")]
		[Column("VergiNo", Order = 12)]
		public virtual string VergiNo { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[DataType(DataType.DateTime, ErrorMessage = "GecersizUyari")]
		[Column("GuncellenmeTarihi", Order = 13)]
		public virtual DateTime GuncellenmeTarihi { get; set; }

		[Required(ErrorMessage = "BosUyari")]
		[DataType(DataType.DateTime, ErrorMessage = "GecersizUyari")]
		[Column("EklenmeTarihi", Order = 14)]
		public virtual DateTime EklenmeTarihi { get; set; }


		public static int OzellikSayisi { get { return typeof(KatilimciTablosuModelBase).GetProperties().Count(x => !x.GetAccessors()[0].IsStatic); }}

		public static string SQLSutunSorgusu { get { return string.Join(", ", typeof(KatilimciTablosuModelBase).GetProperties().Where(x => !x.GetAccessors()[0].IsStatic).OrderBy(x => (x.GetCustomAttributes(typeof(ColumnAttribute), true).First() as ColumnAttribute).Order).Select(x => $"[KatilimciTablosu].[{x.Name}]")); }}

		public virtual string BaseJsonModel()
		{
			return JsonConvert.SerializeObject(this);
		}

	}
}