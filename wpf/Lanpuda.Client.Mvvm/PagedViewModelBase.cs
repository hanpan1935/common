using DevExpress.Mvvm.DataAnnotations;
using HandyControl.Collections;
using HandyControl.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Client.Mvvm
{
    public abstract class PagedViewModelBase<T> : RootViewModelBase where T : IEntityDto<Guid>
    {
        public ManualObservableCollection<T> PagedDatas
        {
            get { return GetProperty(() => PagedDatas); }
            set { SetProperty(() => PagedDatas, value); }
        }

        public  T? SelectedModel
        {
            get { return GetProperty(() => SelectedModel); }
            set { SetProperty(() => SelectedModel, value); }
        }



        protected long _maxPageCount;
        public long MaxPageCount
        {
            get { return _maxPageCount; }
            set 
            { 
                SetValue(ref _maxPageCount, value); 
            }
        }

        protected int _dataCountPerPage;
        public int DataCountPerPage
        {
            get { return _dataCountPerPage; }
            set { SetValue(ref _dataCountPerPage, value, changedCallback: async ()=> { await OnDataCountPerPageChanged(); }); }
        }

        protected int _pageIndex;
        public int PageIndex
        {
            get { return _pageIndex; }
            set { SetValue(ref _pageIndex, value); }
        }


        private long _totalCount;
        public long TotalCount
        {
            get 
            { 
                return _totalCount; 
            }
            set 
            {
                _totalCount = value;
                long pageQuantity = _totalCount / DataCountPerPage;
                long yushu = _totalCount % DataCountPerPage;
                if (yushu > 0)
                {
                    pageQuantity += 1;
                }
                MaxPageCount = pageQuantity;
            }
        }

        public int SkipCount
        {
            get
            {
                int count = (this.PageIndex - 1) * DataCountPerPage;
                return count;
            }
        }

        public virtual Dictionary<string, int> PageSizeList { get; set; }


        public PagedViewModelBase()
        {
            PagedDatas = new ManualObservableCollection<T>();
            PageSizeList = new Dictionary<string, int>();
            PageSizeList.Add("10条/页", 10);
            PageSizeList.Add("15条/页", 15);
            PageSizeList.Add("30条/页", 30);
            PageSizeList.Add("100条/页", 100);
            PageSizeList.Add("500条/页", 500);
            _dataCountPerPage = 15;
            _pageIndex = 1;
        }


        [AsyncCommand]
        public async Task PageUpdatedAsync(FunctionEventArgs<int> info)
        {
            this.PageIndex = info.Info;
            await QueryAsync();
        }


        private async Task OnDataCountPerPageChanged()
        {
            await QueryAsync();
        }


        [AsyncCommand]
        public async Task QueryAsync()
        {
            Guid selectedId = Guid.Empty;
            if (this.SelectedModel != null)
            {
                selectedId = SelectedModel.Id;
            }
            await GetPagedDatasAsync();
            if (selectedId == Guid.Empty)
            {
                this.SelectedModel = this.PagedDatas.FirstOrDefault();
            }
            else
            {
                var selected = this.PagedDatas.Where(m => m.Id == selectedId).FirstOrDefault();
                if (selected != null) 
                {
                    this.SelectedModel = selected; 
                }
                else
                {
                    this.SelectedModel = this.PagedDatas.FirstOrDefault();
                }
            }
           
        }


        protected abstract Task GetPagedDatasAsync();


        /// <summary>
        /// 根据数据库的数据总数计算分页
        /// </summary>
        //private void SetTotalCountCallback()
        //{
        //    long pageQuantity = TotalCount / DataCountPerPage;
        //    long yushu = TotalCount % DataCountPerPage;

        //    if (yushu > 0)
        //    {
        //        pageQuantity += 1;
        //    }
        //    MaxPageCount = pageQuantity;
        //}


        //[Command]
        //public async Task PageSizeChanged(SelectionChangedEventArgs e)
        //{
        //     await this.GetPagedDatasAsync();
        //}

        //protected virtual void CalculatMaxPageCount(long totalCount)
        //{
        //    long pageQuantity = totalCount / DataCountPerPage;
        //    long yushu = totalCount % DataCountPerPage;

        //    if (yushu > 0)
        //    {
        //        pageQuantity += 1;
        //    }
        //    MaxPageCount = pageQuantity;
        //}



    }
}
