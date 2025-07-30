using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using ModelBase;
using Model;

namespace ModelRelation
{
	public abstract class KatilimciKursTablosuModelRelation : KatilimciKursTablosuModelBase
	{
		public virtual KatilimciTablosuModel KatilimciBilgisi { get; set; }
		public virtual KursTipiTablosuModel KursTipiBilgisi { get; set; }

		public virtual string RelationJsonModel()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}