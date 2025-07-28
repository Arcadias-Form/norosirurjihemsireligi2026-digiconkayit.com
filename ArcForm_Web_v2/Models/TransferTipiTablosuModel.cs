using Newtonsoft.Json;
using System;
using ModelRelation;

namespace Model
{
	public partial class TransferTipiTablosuModel : TransferTipiTablosuModelRelation
	{
        public decimal FormUcret
        {
            get
            {
                switch (Sabitler.Donem)
                {
                    case Sabitler.Donemler.CokErkenDonem:
                        return CokErkenUcret;

                    case Sabitler.Donemler.ErkenDonem:
                        return ErkenUcret;

                    default:
                    case Sabitler.Donemler.NormalDonem:
                        return NormalUcret;
                }
            }

        }

        public virtual string FullJsonModel()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}