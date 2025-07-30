using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using ModelBase;
using Model;

namespace ModelRelation
{
	public abstract class KatilimciTablosuModelRelation : KatilimciTablosuModelBase
	{
		public virtual IList<KatilimciEtkinlikTablosuModel> KatilimciEtkinlikBilgisi { get; set; }
		public virtual IList<KatilimciKursTablosuModel> KatilimciKursBilgisi { get; set; }
		public virtual KonaklamaTablosuModel KonaklamaBilgisi { get; set; }
		public virtual OdemeTablosuModel OdemeBilgisi { get; set; }
		public virtual TransferTablosuModel TransferBilgisi { get; set; }
		public virtual DilTablosuModel DilBilgisi { get; set; }

		public virtual string RelationJsonModel()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}