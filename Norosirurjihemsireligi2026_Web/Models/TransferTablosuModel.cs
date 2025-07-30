using Newtonsoft.Json;
using System;
using ModelRelation;

namespace Model
{
	public partial class TransferTablosuModel : TransferTablosuModelRelation
	{

		public virtual string FullJsonModel()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}