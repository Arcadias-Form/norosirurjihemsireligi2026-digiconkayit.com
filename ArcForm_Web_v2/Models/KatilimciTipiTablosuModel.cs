using Newtonsoft.Json;
using System;
using ModelRelation;

namespace Model
{
	public partial class KatilimciTipiTablosuModel : KatilimciTipiTablosuModelRelation
	{
        public virtual string FullJsonModel()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}