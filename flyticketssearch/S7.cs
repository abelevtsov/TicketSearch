using System;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace FlyTicketsSearch
{
    public class S7 : SearchBase
    {
        public S7()
            : this("http://travelwith.s7.ru/selectExactDateSearchFlights.action?TA=2&TC=2&TI=0&CUR=RUB&FLC=1&FLX=false&RDMPTN=false&SC1=ECONOMY&PROMO=&FSC1=1&DD1=2015-07-06&DT1=00%3A00%3A00&DA1=MOW&DP1=CITY_MOW_RU&AA1=SIP&AP1=AIR_SIP_UA&LAN=ru&ibe_conversation=O5ZZ1PPPCGDD785HPTXGEDVBSYA6TN5X")
        {
            
        }

        private S7(string url) : base(url)
        {
        }

        public async override Task<int> Search()
        {
            try
            {
                var banner = WebDriver.FindElements(By.Id("easyXDM_flocktory_default6903_provider")).FirstOrDefault();
                if (banner != null)
                {
                    banner.FindElement(By.ClassName("Wrapper-close2")).Click();
                }
            }
            catch
            {
            }

            var items = WebDriver.FindElement(By.ClassName("items"));
            var active = items.FindElement(By.ClassName("active"));
            var itemCost = active.FindElement(By.ClassName("item-cost")).FindElement(By.TagName("span")).Text;
            var cost = int.Parse(itemCost.Replace(" ", string.Empty).Replace("Руб.", string.Empty));
            if (cost < 4000)
            {
                return cost;
            }

            await Task.Delay(3 * 1000);
            try
            {
                WebDriver.FindElement(By.ClassName("ibe_shop_select-expand")).Click();
                WebDriver.FindElement(By.Id("submitButton")).Click();
            }
            catch
            {
            }

            return await Search();
        }
    }
}
