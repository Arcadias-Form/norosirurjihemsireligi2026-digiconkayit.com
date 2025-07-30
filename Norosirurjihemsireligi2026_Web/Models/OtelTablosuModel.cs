using Newtonsoft.Json;
using System;
using ModelRelation;

namespace Model
{
	public partial class OtelTablosuModel : OtelTablosuModelRelation
	{

		public virtual string FullJsonModel()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}