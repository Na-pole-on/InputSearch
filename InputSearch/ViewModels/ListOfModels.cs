using BusinessLayer.Dtos;

namespace InputSearch.ViewModels
{
    public class ListOfModels
    {
        public int step = 9;
        public int getPartiesStep = 9;

        public int searchStep = 0;
        public string lastName = "";

        public int filterStep = 0;
        public FilterDTO? filter;
    }
}
