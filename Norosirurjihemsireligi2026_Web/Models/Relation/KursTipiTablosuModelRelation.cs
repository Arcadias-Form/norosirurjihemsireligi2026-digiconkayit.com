using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using ModelBase;
using Model;

namespace ModelRelation
{
	public abstract class KursTipiTablosuModelRelation : KursTipiTablosuModelBase
	{
		public virtual IList<KatilimciKursTablosuModel> KatilimciKursBilgisi { get; set; }
		public virtual IList<KursTipiDilTablosuModel> KursTipiDilBilgisi { get; set; }

		public virtual string RelationJsonModel()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}