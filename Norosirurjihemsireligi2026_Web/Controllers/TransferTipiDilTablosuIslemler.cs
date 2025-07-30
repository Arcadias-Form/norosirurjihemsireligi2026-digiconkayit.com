using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using VeritabaniIslemMerkeziBase;

namespace VeritabaniIslemMerkezi
{
	public partial class TransferTipiDilTablosuIslemler : TransferTipiDilTablosuIslemlerBase
	{
		public TransferTipiDilTablosuIslemler() : base() { }

		public TransferTipiDilTablosuIslemler(OleDbTransaction tran) : base (tran) { }
	}
}
