using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using ModelBase;
using Model;

namespace ModelRelation
{
	public abstract class KatilimciTipiOdaTipiTablosuModelRelation : KatilimciTipiOdaTipiTablosuModelBase
	{
		public virtual IList<KonaklamaTablosuModel> KonaklamaBilgisi { get; set; }
		public virtual KatilimciTipiTablosuModel KatilimciTipiBilgisi { get; set; }
		public virtual OdaTipiTablosuModel OdaTipiBilgisi { get; set; }

		public virtual string RelationJsonModel()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}