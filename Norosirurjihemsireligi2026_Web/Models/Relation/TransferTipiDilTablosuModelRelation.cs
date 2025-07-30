using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using ModelBase;
using Model;

namespace ModelRelation
{
	public abstract class TransferTipiDilTablosuModelRelation : TransferTipiDilTablosuModelBase
	{
		public virtual TransferTipiTablosuModel TransferTipiBilgisi { get; set; }
		public virtual DilTablosuModel DilBilgisi { get; set; }

		public virtual string RelationJsonModel()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}