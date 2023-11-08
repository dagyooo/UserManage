using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
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

        [ObservableProperty]
        private User? _SelectedUser;        // 선택한 유저

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

        /// <summary>
        /// 조회 커맨드
        /// </summary>
        [RelayCommand]
        private void SearchUser()
        {
            Task.Run(async () =>
            {
                var item = await Task.Run(() =>
                {
                    var result = mMainService.UserList(SWName, SWPhoneNo, SWEmail);
                    Task.Delay(1000).Wait(); // 1초 고의지연
                    return result;
                });
               
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (item != null)
                    {
                        UserList = item;
                    }
                });
            });
        }

        /// <summary>
        /// 전체 조회 커맨드
        /// </summary>
        [RelayCommand]
        private void SearchAllUser()
        {
            SWName = String.Empty;
            SWPhoneNo = String.Empty;
            SWEmail = String.Empty;

            SearchUser();
        }

        /// <summary>
        /// 추가 열기 커맨드
        /// </summary>
        [RelayCommand]
        private void OpenAddUser()
        {
            IsAdd = !IsAdd;
        }


        /// <summary>
        /// 사용자 추가 커맨드
        /// </summary>
        [RelayCommand]
        private void AddUser()
        {
            // 입력값 VALID 체크
            if (String.IsNullOrEmpty(AddUserName) || String.IsNullOrEmpty(AddUserAge) || String.IsNullOrEmpty(AddUserPhoneNo))
            {
                MessageBox.Show("이름, 나이, 연락처는 필수항목입니다.");
                return;
            }

            Task.Run(async () =>
            {
                var bRet = await Task.Run(() =>
                {
                    Boolean result = mMainService.AddUserList(AddUserName, AddUserAge, AddUserPhoneNo, AddUserEmail);
                    Task.Delay(1000).Wait(); // 1초 고의 지연
                    return result;
                });

                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (bRet)
                    {
                        MessageBox.Show("등록되었습니다.");

                        AddUserName = String.Empty;
                        AddUserAge = String.Empty;
                        AddUserPhoneNo = String.Empty;
                        AddUserEmail = String.Empty;

                        SearchAllUser();   // 성공 시 다시 전체 조회
                    }
                    else
                    {
                        MessageBox.Show("등록에 실패하였습니다.");
                    }
                });
            });
        }

        /// <summary>
        /// 사용자 삭제 커맨드
        /// </summary>
        [RelayCommand]
        private void DeleteUser()
        {
            if(SelectedUser != null)
            {
                if ("Y".Equals(SelectedUser.CanDeleteYn))
                {
                    Task.Run(async () =>
                    {
                        var bRet = await Task.Run(() =>
                        {
                            Boolean result = mMainService.DeleteUserList(SelectedUser.Id);
                            Task.Delay(1000).Wait(); // 1초 고의 지연
                            return result;
                        });

                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            if (bRet)
                            {
                                MessageBox.Show("삭제되었습니다.");
                                SearchUser(); // 성공 시 다시 조회
                            }
                            else
                            {
                                MessageBox.Show("삭제에 실패하였습니다.");
                            }
                        });
                    });

                }
                else
                {
                    MessageBox.Show("삭제할 수 없는 사용자입니다.");
                }
            }
            else
            {
                MessageBox.Show("삭제할 사용자를 선택해 주세요.");
            }
        }
    }
}
