using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using VeritabaniIslemMerkeziBase;

namespace VeritabaniIslemMerkezi
{
	public partial class TransferTipiTablosuIslemler : TransferTipiTablosuIslemlerBase
	{
		public TransferTipiTablosuIslemler() : base() { }

		public TransferTipiTablosuIslemler(OleDbTransaction tran) : base (tran) { }
	}
}
