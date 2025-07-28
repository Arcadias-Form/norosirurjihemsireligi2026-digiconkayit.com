using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using ModelBase;
using Model;

namespace ModelRelation
{
	public abstract class OdaTipiDilTablosuModelRelation : OdaTipiDilTablosuModelBase
	{
		public virtual OdaTipiTablosuModel OdaTipiBilgisi { get; set; }
		public virtual DilTablosuModel DilBilgisi { get; set; }

		public virtual string RelationJsonModel()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}