using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace Data
{
	public class PizzaDB : IPizzaDB
	{
		private ConnectionDB _conn;

		public bool Delete(int id)
		{
			bool status;

			string sql = string.Format(Pizza.DELETE, id);
			using (_conn = new ConnectionDB())
			{
				status = _conn.ExecQuery(sql);
			}

			return status;
		}

		public bool Insert(Pizza pizza)
		{
			bool status;

			string sql = string.Format(Pizza.INSERT, pizza.Descricao, pizza.Valor);
			using (_conn = new ConnectionDB()) {
				status = _conn.ExecQuery(sql);
			};

			return status;
		}

		public Pizza SelectById(int id)
		{
			using (_conn = new ConnectionDB())
			{
				string sql = string.Format(Pizza.SELECTBYID, id);
				var returnData = _conn.ExecQueryReturn(sql);
				return TransformSQLReaderToList(returnData)[0];
			}
		}

		public bool Update(Pizza pizza)
		{
			bool status = false;
			string sql = string.Format(Pizza.UPDATE, pizza.Id, pizza.Descricao, pizza.Valor);

			using (_conn = new ConnectionDB())
			{
				status = _conn.ExecQuery(sql);
			}
			return status;
		}

		List<Pizza> IPizzaDB.Select()
		{
			using (_conn = new ConnectionDB())
			{
				string sql = Pizza.SELECT;
				var returnData = _conn.ExecQueryReturn(sql);
				return TransformSQLReaderToList(returnData);
			}
		}

		private List<Pizza> TransformSQLReaderToList(SqlDataReader returData)
		{
			var list = new List<Pizza>();

			while (returData.Read())
			{
				var item = new Pizza()
				{
					Id = int.Parse(returData["id"].ToString()),
					Descricao = returData["descricao"].ToString(),
					Valor = decimal.Parse(returData["valor"].ToString())
				};

				list.Add(item);
			}
			return list;
		}



	}
}
