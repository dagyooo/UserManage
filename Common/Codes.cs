using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManage.Common
{
    public static class Codes
    {
        /// <summary>
        /// 사용자 상태 코드
        /// </summary>
        public static class UserState
        {
            public const int USE = 0;       // 사용
            public const int EXPIRE = 1;    // 만료
            public const int PAUSE = 2;     // 중지
        }

        /// <summary>
        /// DB 테이블명
        /// </summary>
        public static class TableName
        {
            public const String USERS = "Users";  // 사용자
        }

        public static class TableColumn
        {
            

            public const String ID = "Id";
            public const String STATE = "State";
            public const String NAME = "Name";
            public const String AGE = "Age";
            public const String PHONENO = "PhoneNo";
            public const String EMAIL = "Email";
            public const String REGDT = "RegDt";
            public const String CANDELETEYN = "CanDeleteYn";
        }

    }
}
