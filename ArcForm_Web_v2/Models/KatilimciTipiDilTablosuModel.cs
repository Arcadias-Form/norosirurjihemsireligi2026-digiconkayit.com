using Newtonsoft.Json;
using System;
using ModelRelation;

namespace Model
{
	public partial class KatilimciTipiDilTablosuModel : KatilimciTipiDilTablosuModelRelation
	{

		public virtual string FullJsonModel()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}