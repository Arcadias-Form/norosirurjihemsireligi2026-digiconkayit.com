using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using ModelBase;
using Model;

namespace ModelRelation
{
	public abstract class DilTablosuModelRelation : DilTablosuModelBase
	{
		public virtual IList<EtkinlikDilTablosuModel> EtkinlikDilBilgisi { get; set; }
		public virtual IList<KatilimciTablosuModel> KatilimciBilgisi { get; set; }
		public virtual IList<KatilimciTipiDilTablosuModel> KatilimciTipiDilBilgisi { get; set; }
		public virtual IList<KursTipiDilTablosuModel> KursTipiDilBilgisi { get; set; }
		public virtual IList<OdaTipiDilTablosuModel> OdaTipiDilBilgisi { get; set; }
		public virtual IList<OdemeTipiDilTablosuModel> OdemeTipiDilBilgisi { get; set; }
		public virtual IList<TransferTipiDilTablosuModel> TransferTipiDilBilgisi { get; set; }

		public virtual string RelationJsonModel()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}