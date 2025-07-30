using Newtonsoft.Json;
using System;
using ModelRelation;

namespace Model
{
	public partial class TransferTipiDilTablosuModel : TransferTipiDilTablosuModelRelation
	{

		public virtual string FullJsonModel()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}