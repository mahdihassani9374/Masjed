namespace Varesin.Mvc.Models
{
    public class SearchModel<TSearch, TData>
    {
        public SearchModel(TSearch searchModel, TData model)
        {
            Search = searchModel;
            Data = model;
        }
        public TSearch Search { get; set; }
        public TData Data { get; set; }
    }
}
