using Newtonsoft.Json;
using System;
using ModelRelation;

namespace Model
{
	public partial class DilTablosuModel : DilTablosuModelRelation
	{

		public virtual string FullJsonModel()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}