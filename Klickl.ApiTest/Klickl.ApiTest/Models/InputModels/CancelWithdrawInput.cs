﻿namespace Klickl.ApiTest.Models.InputModels
{
    public class CancelWithdrawInput : InputDto
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
        /// <summary>
        /// 提币申请Id
        /// </summary>
        public string WithdrawID { set; get; }
    }
}
