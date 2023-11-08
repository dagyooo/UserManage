using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManage.Common;
using UserManage.Model;
using UserManage.SQL;

namespace UserManage.Service
{
    public class MainService
    {
        public MainService() { }


        /// <summary>
        /// 사용자 목록 조회
        /// </summary>
        /// <param name="sName"></param>
        /// <param name="sPhoneNo"></param>
        /// <param name="sEmail"></param>
        /// <returns></returns>
        public ObservableCollection<User>? UserList(String? sName, String? sPhoneNo, String? sEmail)
        {
            ObservableCollection<User> colUser = new ObservableCollection<User>();
            List<String> litConditions = new List<String>(); // 조건 담을 list

            try
            {
                // 빈값이 아니면 조건절로 처리
                // 1. 빈값 아닌 값 담기
                // 2. AND로 묶기

                // 이름
                if (!string.IsNullOrEmpty(sName))
                {
                    litConditions.Add($"NAME LIKE '%{sName}%'");
                }

                // 연락처
                if (!string.IsNullOrEmpty(sPhoneNo))
                {
                    litConditions.Add($"PHONENO LIKE '%{sPhoneNo}%'");
                }

                // 이메일
                if (!string.IsNullOrEmpty(sEmail))
                {
                    litConditions.Add($"EMAIL LIKE '%{sEmail}%'");
                }

                // AND로 묶기
                String sWhereStr = litConditions != null && litConditions.Count > 0 ? "WHERE " + string.Join(" AND ", litConditions) : "";
                String[] arrColumns = new String[] { Codes.TableColumn.ID, Codes.TableColumn.STATE, Codes.TableColumn.NAME, Codes.TableColumn.AGE
                                                            ,Codes.TableColumn. PHONENO, Codes.TableColumn.EMAIL, Codes.TableColumn.REGDT, Codes.TableColumn.CANDELETEYN };

                // DB 조회하기
                DataTable dtUser = DBUtil.ExecuteSelectQuery(Codes.TableName.USERS, arrColumns, sWhereStr);

                foreach (DataRow row in dtUser.Rows)
                {
                    User user = new User
                    {
                        Id = Convert.ToInt32(row[Codes.TableColumn.ID]),
                        State = Convert.ToInt32(row[Codes.TableColumn.STATE]),
                        Name = row[Codes.TableColumn.NAME].ToString() ?? "",
                        Age = Convert.ToInt32(row[Codes.TableColumn.AGE]),
                        PhoneNo = row[Codes.TableColumn.PHONENO].ToString() ?? "",
                        Email = row[Codes.TableColumn.EMAIL].ToString() ?? "",
                        RegDt = row[Codes.TableColumn.REGDT].ToString() ?? "",
                        CanDeleteYn = row[Codes.TableColumn.CANDELETEYN].ToString() ?? "Y"
                    };
                    colUser.Add(user);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }

            return colUser;
        }


        /// <summary>
        /// 사용자 추가
        /// </summary>
        /// <param name="sName"></param>
        /// <param name="sAge"></param>
        /// <param name="sPhoneNo"></param>
        /// <param name="sEmail"></param>
        /// <returns></returns>
        public Boolean AddUserList(String sName, String sAge, String sPhoneNo, String? sEmail)
        {
            sName = DBUtil.ConvertorStringValue(sName);
            sPhoneNo = DBUtil.ConvertorStringValue(sPhoneNo);
            sEmail = DBUtil.ConvertorStringValue(sEmail ?? String.Empty);

            String[] arrColumns = new String[] { Codes.TableColumn.STATE, Codes.TableColumn.NAME, Codes.TableColumn.AGE, Codes.TableColumn.PHONENO, Codes.TableColumn.EMAIL };
            String[] arrValues = new String[] { Codes.UserState.USE.ToString(), sName, sAge, sPhoneNo, sEmail};

            // DB 추가하기
            int nRet = DBUtil.ExecuteInsertQuery(Codes.TableName.USERS, arrColumns, arrValues);

            return nRet > 0;
        }


        /// <summary>
        /// 사용자 삭제
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Boolean DeleteUserList(int id)
        {
            String sWhereClause = $"ID = {id}";
            int nRet = DBUtil.ExecuteDeleteQuery(Codes.TableName.USERS, sWhereClause);

            return nRet > 0;
        }
    }
}
