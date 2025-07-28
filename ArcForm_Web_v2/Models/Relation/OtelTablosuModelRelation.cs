using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using ModelBase;
using Model;

namespace ModelRelation
{
	public abstract class OtelTablosuModelRelation : OtelTablosuModelBase
	{
		public virtual IList<OdaTipiTablosuModel> OdaTipiBilgisi { get; set; }

		public virtual string RelationJsonModel()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}