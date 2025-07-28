using Newtonsoft.Json;
using System;
using ModelRelation;

namespace Model
{
	public partial class KursTipiDilTablosuModel : KursTipiDilTablosuModelRelation
	{

		public virtual string FullJsonModel()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}