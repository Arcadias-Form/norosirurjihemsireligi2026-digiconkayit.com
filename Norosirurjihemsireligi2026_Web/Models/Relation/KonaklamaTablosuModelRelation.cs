using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using ModelBase;
using Model;

namespace ModelRelation
{
	public abstract class KonaklamaTablosuModelRelation : KonaklamaTablosuModelBase
	{
		public virtual KatilimciTablosuModel KatilimciBilgisi { get; set; }
		public virtual KatilimciTipiOdaTipiTablosuModel KatilimciTipiOdaTipiBilgisi { get; set; }

		public virtual string RelationJsonModel()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}