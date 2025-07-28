using Newtonsoft.Json;
using System;
using ModelRelation;

namespace Model
{
	public partial class EtkinlikDilTablosuModel : EtkinlikDilTablosuModelRelation
	{

		public virtual string FullJsonModel()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}