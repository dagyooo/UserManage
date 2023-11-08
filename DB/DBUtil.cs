using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManage.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace UserManage.SQL
{
    public class DBUtil
    {
        public delegate void DataMappingDelegate(SqliteDataReader reader);

        public const String DB_CONNECTION_STR = "Data Source=manage.db";

        /// <summary>
        /// SELECT 문 
        /// </summary>
        /// <param name="sTableName">테이블명</param>
        /// <param name="arrColumns">컬럼명</param>
        /// <param name="sWhere">조건절</param>
        /// <param name="sOrderby">정렬</param>
        /// <returns></returns>
        public static DataTable? ExecuteSelectQuery(String sTableName, String[] arrColumns, String sWhere = "", String sOrderby = "")
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (var connection = new SqliteConnection(DB_CONNECTION_STR))
                {
                    connection.Open();

                    String sColumnsStr = (arrColumns != null && arrColumns.Length > 0) ? String.Join(", ", arrColumns) : "*";
                    String sQuery = $"SELECT {sColumnsStr} FROM {sTableName} {sWhere} {sOrderby}";

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = sQuery;

                        using (var reader = command.ExecuteReader())
                        {
                            dataTable.Load(reader);
                        }
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace );
                Console.WriteLine(e.Message);
                return null;
            }
           

            return dataTable;
        }

        /// <summary>
        /// DELETE 문
        /// </summary>
        /// <param name="sTableName"></param>
        /// <param name="sWhereClause"></param>
        /// <returns></returns>
        public static int ExecuteDeleteQuery(String sTableName, String sWhereClause = "")
        {
            try
            {
                using (var connection = new SqliteConnection(DB_CONNECTION_STR))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction()) // 트랜잭션 시작
                    {
                        String aQuery = $"DELETE FROM {sTableName} WHERE {sWhereClause}";

                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = aQuery;
                            int nRet = command.ExecuteNonQuery();
                            transaction.Commit();
                            return nRet;
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine(e.Message);
                return -1;
            }
        }


        /// <summary>
        /// INSERT 문
        /// </summary>
        /// <param name="sTableName"></param>
        /// <param name="arrColumns"></param>
        /// <param name="accValues"></param>
        /// <returns></returns>
        public static int ExecuteInsertQuery(String sTableName, String[] arrColumns, String[] accValues)
        {
            try
            {
                using (var connection = new SqliteConnection(DB_CONNECTION_STR))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction()) // 트랜잭션 시작
                    {
                        String sColumnsStr = (arrColumns != null && arrColumns.Length > 0) ? String.Join(", ", arrColumns) : "*";
                        String sValuesStr = (accValues != null && accValues.Length > 0) ? String.Join(", ", accValues) : "*";

                        String aQuery = $"INSERT INTO {sTableName} ({sColumnsStr}) VALUES ({sValuesStr})";

                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = aQuery;
                            int nRet = command.ExecuteNonQuery();
                            transaction.Commit();
                            return nRet;
                        }
                    }

                    }
                }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine(e.Message);
                return -1;
            }
        }

        /// <summary>
        /// VARCHAR 형 VALUE값으로 변환
        /// </summary>
        /// <param name="sValue"></param>
        /// <returns></returns>
        public static String ConvertorStringValue(String sValue)
        {
            return String.IsNullOrEmpty(sValue) ? sValue : "\'" + sValue + "\'";
        }
    }
}
