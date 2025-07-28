using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using VeritabaniIslemMerkeziBase;

namespace VeritabaniIslemMerkezi
{
	public partial class EtkinlikTablosuIslemler : EtkinlikTablosuIslemlerBase
	{
		public EtkinlikTablosuIslemler() : base() { }

		public EtkinlikTablosuIslemler(OleDbTransaction tran) : base (tran) { }
	}
}
