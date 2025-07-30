using Newtonsoft.Json;
using System;
using ModelRelation;

namespace Model
{
	public partial class KatilimciKursTablosuModel : KatilimciKursTablosuModelRelation
	{

		public virtual string FullJsonModel()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}