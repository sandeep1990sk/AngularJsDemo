using System;
using SQLite;

namespace myCIIEmployee
{
	public interface ISQLite
	{
		SQLiteConnection GetConnection();
	}
}

