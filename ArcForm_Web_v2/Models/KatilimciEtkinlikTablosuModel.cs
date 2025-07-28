using Newtonsoft.Json;
using System;
using ModelRelation;

namespace Model
{
	public partial class KatilimciEtkinlikTablosuModel : KatilimciEtkinlikTablosuModelRelation
	{

		public virtual string FullJsonModel()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}