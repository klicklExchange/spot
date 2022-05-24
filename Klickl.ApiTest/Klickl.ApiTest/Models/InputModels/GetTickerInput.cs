namespace Klickl.ApiTest.Models.InputModels
{
    public class GetTickerInput : InputDto
    {
        string symbol;
        public string Symbol
        {
            set
            {
                symbol = value;
            }
            get
            {
                return symbol.Replace("-", "/");
            }
        }
    }
}
