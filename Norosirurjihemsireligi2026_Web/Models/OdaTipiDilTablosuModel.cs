using Newtonsoft.Json;
using System;
using ModelRelation;

namespace Model
{
	public partial class OdaTipiDilTablosuModel : OdaTipiDilTablosuModelRelation
	{

		public virtual string FullJsonModel()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}