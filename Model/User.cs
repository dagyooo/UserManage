using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManage.Model
{
    public partial class User : ObservableObject
    {
        // 조건
        // 사용자 필수 항목 : 이름, 나이, 연락처

        private int nId;
        public int Id
        {
            get { return nId; }
            set { SetProperty(ref nId, value); }
        }

        private int nState;                           // 회원 상태
        public int State
        {
            get { return nState; }
            set { SetProperty(ref nState, value); }
        }

        private String sName = String.Empty;          // 이름
        public String Name
        {
            get { return sName; }
            set { SetProperty(ref sName, value); }
        }

        private int nAge;                             // 나이
        public int Age
        {
            get { return nAge; }
            set { SetProperty(ref nAge, value); }
        }

        private String sPhoneNo = String.Empty;       // 연락처
        public String PhoneNo
        {
            get { return sPhoneNo; }
            set { SetProperty(ref sPhoneNo, value); }
        }

        private String? sEmail;                       // 이메일
        public String? Email
        {
            get { return sEmail; }
            set { SetProperty(ref sEmail, value); }
        }

        private String sRegDt = String.Empty;         // 등록일
        public String RegDt
        {
            get { return sRegDt; }
            set { SetProperty(ref sRegDt, value); }
        }

        private String sCanDeleteYn = "Y";              // 삭제 가능 여부
        public String CanDeleteYn
        {
            get { return sCanDeleteYn; }
            set { SetProperty(ref sCanDeleteYn, value); }
        }



    }
}
