namespace Airbnb.Application.Contracts.v1.Base
{
    public abstract class BaseQueryStringParameters
    {
        const int _maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 8;
        public virtual int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > _maxPageSize ? _maxPageSize : value; }
        }
    }
}
