using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using ModelBase;
using Model;

namespace ModelRelation
{
	public abstract class TransferTablosuModelRelation : TransferTablosuModelBase
	{
		public virtual KatilimciTablosuModel KatilimciBilgisi { get; set; }
		public virtual TransferTipiTablosuModel TransferTipiBilgisi { get; set; }

		public virtual string RelationJsonModel()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}