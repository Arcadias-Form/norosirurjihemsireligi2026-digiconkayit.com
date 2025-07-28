using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using VeritabaniIslemMerkeziBase;

namespace VeritabaniIslemMerkezi
{
	public partial class TransferTablosuIslemler : TransferTablosuIslemlerBase
	{
		public TransferTablosuIslemler() : base() { }

		public TransferTablosuIslemler(OleDbTransaction tran) : base (tran) { }
	}
}
