using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using ModelBase;
using Model;

namespace ModelRelation
{
	public abstract class EtkinlikTablosuModelRelation : EtkinlikTablosuModelBase
	{
		public virtual IList<EtkinlikDilTablosuModel> EtkinlikDilBilgisi { get; set; }
		public virtual IList<KatilimciEtkinlikTablosuModel> KatilimciEtkinlikBilgisi { get; set; }

		public virtual string RelationJsonModel()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}