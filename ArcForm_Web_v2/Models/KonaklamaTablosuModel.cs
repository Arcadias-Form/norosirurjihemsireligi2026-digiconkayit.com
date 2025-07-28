using Newtonsoft.Json;
using System;
using ModelRelation;

namespace Model
{
	public partial class KonaklamaTablosuModel : KonaklamaTablosuModelRelation
	{

		public virtual string FullJsonModel()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}