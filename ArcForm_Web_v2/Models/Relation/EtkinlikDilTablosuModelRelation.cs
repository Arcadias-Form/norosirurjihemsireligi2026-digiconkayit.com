using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using ModelBase;
using Model;

namespace ModelRelation
{
	public abstract class EtkinlikDilTablosuModelRelation : EtkinlikDilTablosuModelBase
	{
		public virtual EtkinlikTablosuModel EtkinlikBilgisi { get; set; }
		public virtual DilTablosuModel DilBilgisi { get; set; }

		public virtual string RelationJsonModel()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}