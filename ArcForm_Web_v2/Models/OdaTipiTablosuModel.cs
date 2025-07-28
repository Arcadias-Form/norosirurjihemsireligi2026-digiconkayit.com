using Newtonsoft.Json;
using System;
using ModelRelation;

namespace Model
{
	public partial class OdaTipiTablosuModel : OdaTipiTablosuModelRelation
	{

		public virtual string FullJsonModel()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}