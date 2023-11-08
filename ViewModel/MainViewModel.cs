using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManage.Common;
using UserManage.Model;
using UserManage.Service;

namespace UserManage.ViewModel
{
    partial class MainViewModel : ObservableObject
    {
        private MainService mMainService;

        [ObservableProperty]
        private ObservableCollection<User> _UserList;   // 유저 목록 

        [ObservableProperty]
        private String? _SWName;    // 검색어-이름

        [ObservableProperty]
        private String? _SWPhoneNo; // 검색어-연락처

        [ObservableProperty]
        private String? _SWEmail;   // 검색어-이메일

        [ObservableProperty]
        private Boolean _IsAdd = false;

        [ObservableProperty]
        private String? _AddUserName;       // 사용자 추가-이름

       [ObservableProperty]
        private String? _AddUserAge;            // 사용자 추가-나이

        [ObservableProperty]
        private String? _AddUserPhoneNo;    // 사용자 추가-연락처

        [ObservableProperty]
        private String? _AddUserEmail;      // 사용자 추가-이메일


        public MainViewModel()
        {
            mMainService = new MainService();
            UserList = new ObservableCollection<User>();
            InitData();
        }

        /// <summary>
        /// 기본 데이터 초기화
        /// </summary>
        private void InitData()
        {
            SearchUser();
        }

        [RelayCommand]
        private void SearchUser()
        {
            mMainService.UserList(SWName, SWPhoneNo, SWEmail, UserList);
        }

        [RelayCommand]
        private void OpenAddUser()
        {
            IsAdd = !IsAdd;
        }

        [RelayCommand]
        private void DeleteUser()
        {

        }

        partial void OnAddUserAgeChanging(String? oldValue, String? newValue)
        {
            if(!IsNumber(newValue))
            {
                AddUserAge = oldValue;
                OnPropertyChanged(nameof(AddUserAge));
            }
        }

        private bool IsNumber(String? text)
        {
            if (int.TryParse(text, out int result))
            {
                return true; // 숫자
            }
            return false; // 숫자가 아닐때
        }



    }
}
