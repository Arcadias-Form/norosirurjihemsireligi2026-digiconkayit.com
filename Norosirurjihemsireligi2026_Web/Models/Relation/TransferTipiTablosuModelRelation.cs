using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using ModelBase;
using Model;

namespace ModelRelation
{
	public abstract class TransferTipiTablosuModelRelation : TransferTipiTablosuModelBase
	{
		public virtual IList<TransferTablosuModel> TransferBilgisi { get; set; }
		public virtual IList<TransferTipiDilTablosuModel> TransferTipiDilBilgisi { get; set; }

		public virtual string RelationJsonModel()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}