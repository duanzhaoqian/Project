using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Test> list = new List<Test>();
            for (int i = 0; i < 100; i++)
            {
                list.Add(new Test() { Name = "b" + i, Age = 10, Address = "123", Id = i });
            }
            DataTable dataTable = Command.DataConvert.ListToDataTable(list);
            dataTable.TableName = "Test";
            int num = UpdateData(dataTable, ConfigurationManager.ConnectionStrings["test"].ConnectionString);
            Console.WriteLine(num);
            Console.ReadKey();
        }

        //    public static void UpdateData(DataTable dt, string connectionString)
        //    {

        //        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        //        {
        //            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
        //            {
        //                DataSet dtSet = new DataSet();
        //                sqlConnection.Open();
        //                sqlDataAdapter.SelectCommand = new SqlCommand("select * from test ", sqlConnection);

        //                sqlDataAdapter.UpdateCommand = new SqlCommand("update test set name=@name,age=@age,address=@address where id=@id", sqlConnection);
        //                sqlDataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 50,
        //                    "Name"));
        //                sqlDataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@age", SqlDbType.Int, 4, "Age"));
        //                sqlDataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@address", SqlDbType.NVarChar, 50,
        //                "Address"));
        //                sqlDataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.Int, 4, "Id"));
        //                sqlDataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.None;


        //                sqlDataAdapter.InsertCommand = new SqlCommand("Insert into test(id,name,age,address) values (@id,@name,@age,@address)", sqlConnection);
        //                sqlDataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 50,
        //"Name"));
        //                sqlDataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@age", SqlDbType.Int, 4, "Age"));
        //                sqlDataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@address", SqlDbType.NVarChar, 50,
        //                "Address"));
        //                sqlDataAdapter.InsertCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.Int, 4, "Id"));
        //                sqlDataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;

        //                sqlDataAdapter.DeleteCommand = new SqlCommand("delete from test where id =@id ", sqlConnection);
        //                sqlDataAdapter.DeleteCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.Int, 4, "Id"));
        //                sqlDataAdapter.DeleteCommand.UpdatedRowSource = UpdateRowSource.None;
        //                sqlDataAdapter.Fill(dtSet);
        //                //ChangeTable(dtSet.Tables[0], dt);
        //                DataRowCollection dtRowCollection = dtSet.Tables[0].Rows;
        //                foreach (DataRow dataRow in dtRowCollection)
        //                {
        //                    dataRow["Name"] = "111";
        //                }
        //                int n = 1;
        //                sqlDataAdapter.Update(dtSet);
        //            }
        //        }
        //    }

        public static int UpdateData(DataTable dt, string connectionString)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    List<SqlParameter> list = new List<SqlParameter>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        stringBuilder.AppendLine("update test set name=@name" + i + ",age=@age" + i + ",address=@address" + i + " where id=@id" + i + "");
                        list.Add(new SqlParameter("@name" + i, dt.Rows[i]["Name"]));
                        list.Add(new SqlParameter("@age" + i, dt.Rows[i]["Age"]));
                        list.Add(new SqlParameter("@address" + i, dt.Rows[i]["Address"]));
                        list.Add(new SqlParameter("@id" + i, dt.Rows[i]["Id"]));

                    }
                    sqlCommand.CommandText = stringBuilder.ToString();
                    sqlCommand.Parameters.AddRange(list.ToArray());
                    sqlConnection.Open();
                    return sqlCommand.ExecuteNonQuery();

                }
            }
        }
        private static void ChangeTable(DataTable SourceTable, DataTable dt)
        {
            SourceTable.BeginLoadData();
            foreach (DataRow dataRow in dt.Rows)
            {
                object[] objects = new object[dt.Columns.Count];
                for (int i = 0; i < objects.Length; i++)
                {
                    objects[i] = dataRow[i];
                }
                SourceTable.LoadDataRow(objects, false);
            }
            SourceTable.EndLoadData();
        }
    }

    public class Test
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int age;
        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
    }
}
