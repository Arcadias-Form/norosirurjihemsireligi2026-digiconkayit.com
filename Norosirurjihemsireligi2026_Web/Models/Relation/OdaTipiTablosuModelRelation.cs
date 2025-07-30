using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using ModelBase;
using Model;

namespace ModelRelation
{
	public abstract class OdaTipiTablosuModelRelation : OdaTipiTablosuModelBase
	{
		public virtual IList<KatilimciTipiOdaTipiTablosuModel> KatilimciTipiOdaTipiBilgisi { get; set; }
		public virtual IList<OdaTipiDilTablosuModel> OdaTipiDilBilgisi { get; set; }
		public virtual OtelTablosuModel OtelBilgisi { get; set; }

		public virtual string RelationJsonModel()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}