using Newtonsoft.Json;
using System;
using ModelRelation;

namespace Model
{
	public partial class OdemeTipiDilTablosuModel : OdemeTipiDilTablosuModelRelation
	{

		public virtual string FullJsonModel()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}