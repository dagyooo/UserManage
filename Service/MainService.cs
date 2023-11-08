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

        public void UserList(String? sName, String? sPhoneNo, String? sEmail, ObservableCollection<User> colUser)
        {
            // null이 아니면 Clear, null이면 새로 생성
            if(colUser != null)
            {
                colUser.Clear();
            }
            else
            {
                colUser = new ObservableCollection<User>();
            }

            // 빈값이 아니면 조건절로 처리
            // 1. 빈값 아닌 값 담기
            // 2. AND로 묶기

            List<String> litConditions = new List<String>(); // 조건 담을 list

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

            DataTable dtUser = DBUtil.ExecuteSelectQuery(Codes.TableName.USERS, Codes.TableColumn.USERS_COLUMNS, sWhereStr);

            foreach (DataRow row in dtUser.Rows)
            {
                User user = new User
                {
                    Id = Convert.ToInt32(row[Codes.TableColumn.ID]),
                    State = Convert.ToInt32(row[Codes.TableColumn.STATE]),
                    Name = row[Codes.TableColumn.NAME].ToString() ?? "",
                    Age = Convert.ToInt32(row[Codes.TableColumn.AGE]),
                    PhoneNo = row[Codes.TableColumn.PHONENO].ToString() ?? "",
                    Email = row[Codes.TableColumn.EMAIL].ToString() ?? "" , 
                    RegDt = row[Codes.TableColumn.REGDT].ToString() ?? "" ,
                    CanDeleteYn = row[Codes.TableColumn.CANDELETEYN].ToString() ?? "Y"
                };
                colUser.Add(user);
            }
        }
    }
}
