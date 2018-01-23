using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using QuickHand.View;
using static QuickHand.Utils.FunctionObject;

namespace QuickHand.Utils
{
    public class ChromeUtil
    {
        IWebDriver driver;

        const string functionClickATag = "function clickAtag(className, text){ var result = false; var parents = document.querySelectorAll(className); for (var i = 0; i<parents.length; i++) { if(parents[i].textContent == text){ parents[i].getElementsByTagName('a')[0].click(); result = true; break; } } }";
        const string functionClickJustAtag = "function clickJustAtag(text){ var result = false; var aTags = document.getElementsByTagName('a'); for (var i = 0; i<aTags.length; i++) { if(aTags[i].textContent == text){ aTags[i].click(); result = true; break; } } }";
        const string functionClickAtagWithClass = "function clickAtagWithClass(className, text){ var result = false; var targets = document.querySelectorAll(className); for (var i = 0; i<targets.length; i++) { if(targets[i].textContent == text){ targets[i].click(); result = true; } } }";
        const string functionClickDown = "function clickDown(index){ if(index > 0 && index <= 10){ var parents = document.querySelectorAll('.askpriceB'); var trs = parents[0].querySelectorAll('tr.down');trs[trs.length - index].querySelector('a').click();}}";
        const string functionClickLastDown = "function clickLastDown(){ var parents = document.querySelectorAll('.askpriceB'); var trs = parents[0].querySelectorAll('tr.down'); trs[trs.length - 1].querySelector('a').click(); }";
        const string functionClickUp = "function clickUp(index){ if(index > 0 && index <= 10){ var parents = document.querySelectorAll('.askpriceB'); var trs = parents[0].querySelectorAll('tr.up'); trs[index - 1].querySelector('a').click(); }}";
        const string functionClickFirstUp = "function clickFistUp(){ var parents = document.querySelectorAll('.askpriceB'); var trs = parents[0].querySelectorAll('tr.up'); trs[0].querySelector('a').click(); }";
        const string functionFindItems = "function findItems(className, text) { var count = 0; var targets = document.querySelectorAll(className); for (var i = 0; i<targets.length; i++) { if (targets[i].textContent == text) { count++; }} return count;}";
        const string functionControlDetail = "function clickDetail(detail){  if(detail > 0){    var btnPlus = document.querySelector('#root > div > div > div.mainB > section.ty01 > div > div.rightB > article:nth-child(1) > span.orderB > div > dl > dd:nth-child(6) > div > a.plus')    for (var i = 0; i<detail; i++) {      btnPlus.click();    }}else if(detail< 0){    var btnMinus = document.querySelector('#root > div > div > div.mainB > section.ty01 > div > div.rightB > article:nth-child(1) > span.orderB > div > dl > dd:nth-child(6) > div > a.minus')    for (var i = 0; i<(detail* -1); i++) {      btnMinus.click();    }  }};";
        const string functionScript = functionClickATag + functionClickJustAtag + functionClickAtagWithClass + functionClickDown + functionClickUp + functionClickLastDown + functionClickFirstUp + functionFindItems;


        private static ChromeUtil instance;
        public static ChromeUtil getInstance()
        {
            if(instance == null)
            {
                instance = new ChromeUtil();
            }
            return instance;
        }


        public void StartChrome(string url)
        {
            var driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;

            if(driver != null)
            {
                MessageDialog dialog = new MessageDialog("실행중인 크롬이 있습니다. 재시작하겠습니까?", true);
                dialog.Owner = App.Current.MainWindow;
                if(dialog.ShowDialog() == true)
                {
                    try
                    {
                        driver.Close();
                        driver.Quit();
                        driver = null;
                    }
                    catch (Exception e)
                    {
                        driver.Quit();
                    }
                    finally
                    {
                        driver = null;
                    }
                }
                else
                {
                    return;
                }
                
            }
            driver = new ChromeDriver(driverService, new ChromeOptions());
            driver.Navigate().GoToUrl(url);
        }

        public void StopChrome()
        {
            if (driver != null)
            {
                try
                {
                    driver.Close();
                    driver.Quit();
                }
                catch (Exception e)
                {
                    driver.Quit();
                }
                finally
                {
                    driver = null;
                }
            }
        }

        public void CheckChrome()
        {
            if (driver != null)
            {
                try
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("console.log('keep alive');");
                }
                catch (Exception e)
                {
                    if (e.Message.Contains("reachable"))
                    {
                        driver.Quit();
                        driver = null;
                    }
                }
            }
        }

        public void CurrentBuyPercent(string percent)
        {
            string exeScript = "clickAtag('.t2', '매수'); clickLastDown(); clickAtag('.layerB', '%'); clickJustAtag('" + percent + "'); clickAtag('.ty04', '매수');  clickJustAtag('매수확인');" + functionScript;
            //string exeScript = "clickAtag('.t2', '매수'); clickLastDown(); clickAtag('.layerB', '%'); clickJustAtag('" + percent + "'); clickAtag('.ty04', '매수'); " + functionScript;
            ((IJavaScriptExecutor)driver).ExecuteScript(exeScript);
        }

        public void CurrentSellPercent(string percent)
        {
            string exeScript = "clickAtag('.t3', '매도'); clickFistUp(); clickAtag('.layerB', '%'); clickJustAtag('" + percent + "'); clickAtag('.ty02', '매도');  clickJustAtag('매도확인');" + functionScript;
            //string exeScript = "clickAtag('.t3', '매도'); clickFistUp(); clickAtag('.layerB', '%'); clickJustAtag('" + percent + "'); clickAtag('.ty02', '매도'); " + functionScript;
            ((IJavaScriptExecutor)driver).ExecuteScript(exeScript);
        }

        public void BuyPercent(string percent)
        {
            string exeScript = "clickAtag('.layerB', '%'); clickJustAtag('" + percent + "'); clickAtag('.ty04', '매수');  clickJustAtag('매수확인');" + functionScript;
            ((IJavaScriptExecutor)driver).ExecuteScript(exeScript);
        }

        public void SellPercent(string percent)
        {
            string exeScript = "clickAtag('.layerB', '%'); clickJustAtag('" + percent + "'); clickAtag('.ty02', '매도');  clickJustAtag('매도확인');" + functionScript;
            ((IJavaScriptExecutor)driver).ExecuteScript(exeScript);
        }

        public void CancelOrder()
        {
            string exeScript = "clickAtag('.t4', '거래내역'); clickAtagWithClass('.btn', '취소'); clickJustAtag('주문 취소'); setTimeout(function() { clickJustAtag('확인');}, 200); " + functionScript;
            ((IJavaScriptExecutor)driver).ExecuteScript(exeScript);
        }

        private void clickDetail(int detail)
        {
            //Thread.Sleep(200);
            if (detail != 3)
            {
                int count = (detail - 3) * -1;
                if (count > 0)
                {
                    var btnPlus = driver.FindElement(By.XPath("//*[@id='root']/div/div/div[2]/section[1]/div/div[2]/article[1]/span[2]/div/dl/dd[3]/div/a[2]"));
                    for (int i = 0; i < count; i++)
                    {
                        btnPlus.Click();
                    }
                }
                else if (count < 0)
                {
                    var btnMinus = driver.FindElement(By.XPath("//*[@id='root']/div/div/div[2]/section[1]/div/div[2]/article[1]/span[2]/div/dl/dd[3]/div/a[1]"));
                    for (int i = 0; i < count * -1; i++)
                    {
                        btnMinus.Click();
                    }
                }
            }
        }

        public void CurrentBuyPercent(FunctionObject.CommandType askType, int offset, int detail, FunctionObject.AmountType aType, double amount)
        {
            string exeScript2 = "";
            if (askType == FunctionObject.CommandType.Buy)
            {
                exeScript2 = "clickAtag('.t2', '매수'); clickUp(" + offset + "); " + functionScript;
            }
            else if(askType == FunctionObject.CommandType.Sell)
            {
                exeScript2 = "clickAtag('.t2', '매수'); clickDown(" + offset + "); " + functionScript;
            }
            else
            {
                return;
            }
            
            // 0 3 1 2 2 1 3 0
            ((IJavaScriptExecutor)driver).ExecuteScript(exeScript2);
            clickDetail(detail);
            BuyPercent(aType, amount);
            Debug.Print(aType.ToString() + "------------------------");
        }

        public void CurrentSellPercent(CommandType askType, int offset, int detail, FunctionObject.AmountType aType, double amount)
        {
            string exeScript2 = "";
            if (askType == FunctionObject.CommandType.Buy)
            {
                exeScript2 = "clickAtag('.t3', '매도'); clickUp(" + offset + "); " + functionScript;

            }
            else if (askType == FunctionObject.CommandType.Sell)
            {
                exeScript2 = "clickAtag('.t3', '매도'); clickDown(" + offset + "); " + functionScript;

            }
            else
            {
                return;
            }

            ((IJavaScriptExecutor)driver).ExecuteScript(exeScript2);
            clickDetail(detail);
            SellPercent(aType, amount);
        }

        public void BuyPercent(FunctionObject.AmountType aType, double amount)
        {
            //string exeScript3 = "clickAtag('.ty04', '매수'); " + functionScript;
            string exeScript3 = "clickAtag('.ty04', '매수');  clickJustAtag('매수확인'); " + functionScript;
            if (aType == FunctionObject.AmountType.KRW)
            {
                
                var typeItem = driver.FindElement(By.XPath("//*[@id='root']/div/div/div[2]/section[1]/div/div[2]/article[1]/span[2]/div/dl/dt[3]/i"));
                if (typeItem.Text.Contains("KRW"))
                {
                    var item = driver.FindElement(By.XPath("//*[@id='root']/div/div/div[2]/section[1]/div/div[2]/article[1]/span[2]/div/dl/dd[2]/input"));
                    var priceElement = driver.FindElement(By.XPath("//*[@id='root']/div/div/div[2]/section[1]/div/div[2]/article[1]/span[2]/div/dl/dd[3]/div/input"));
                    //*[@id="root"]/div/div/div[2]/section[1]/div/div[2]/article[1]/span[2]/div/dl/dt[3]/i
                    int price = int.Parse(priceElement.GetAttribute("value"), System.Globalization.NumberStyles.AllowThousands);
                    if (price != 0 && amount != 0)
                    {
                        double result = amount / (double)price;
                        item.SendKeys(result.ToString("F8"));
                    }
                    ((IJavaScriptExecutor)driver).ExecuteScript(exeScript3);
                }
                else
                {
                    throw new Exception("KRW 마켓에서만 이용가능합니다.");
                }

            }
            else if (aType == FunctionObject.AmountType.BTC)
            {
                var typeItem = driver.FindElement(By.XPath("//*[@id='root']/div/div/div[2]/section[1]/div/div[2]/article[1]/span[2]/div/dl/dt[3]/i"));
                if (typeItem.Text.Contains("BTC"))
                {
                    var item = driver.FindElement(By.XPath("//*[@id='root']/div/div/div[2]/section[1]/div/div[2]/article[1]/span[2]/div/dl/dd[2]/input"));
                    var priceElement = driver.FindElement(By.XPath("//*[@id='root']/div/div/div[2]/section[1]/div/div[2]/article[1]/span[2]/div/dl/dd[3]/div/input"));
                    //*[@id="root"]/div/div/div[2]/section[1]/div/div[2]/article[1]/span[2]/div/dl/dt[3]/i
                    double price = double.Parse(priceElement.GetAttribute("value"));
                    if (price != 0 && amount != 0)
                    {
                        double result = amount / (double)price;
                        item.SendKeys(result.ToString());
                    }
                    ((IJavaScriptExecutor)driver).ExecuteScript(exeScript3);
                }
                else
                {
                    throw new Exception("BTC 마켓에서만 이용가능합니다.");
                }
            }
            else
            {
                //string exeScript = "clickAtag('.layerB', '%'); clickJustAtag('" + FunctionObject.getAmountTypeText(aType) + "'); clickAtag('.ty04', '매수'); " + functionScript;
                string exeScript = "clickAtag('.layerB', '%'); clickJustAtag('" + FunctionObject.getAmountTypeText(aType) + "'); clickAtag('.ty04', '매수');  clickJustAtag('매수확인'); " + functionScript;
                ((IJavaScriptExecutor)driver).ExecuteScript(exeScript);
            }
        }

        public void SellPercent(FunctionObject.AmountType aType, double amount)
        {
            //string exeScript3 = "clickAtag('.ty02', '매도'); " + functionScript;
            string exeScript3 = "clickAtag('.ty02', '매도'); clickJustAtag('매도확인'); " + functionScript;
            if (aType == FunctionObject.AmountType.KRW)
            {
                var typeItem = driver.FindElement(By.XPath("//*[@id='root']/div/div/div[2]/section[1]/div/div[2]/article[1]/span[2]/div/dl/dt[3]/i"));
                if (typeItem.Text.Contains("KRW"))
                {
                    var item = driver.FindElement(By.XPath("//*[@id='root']/div/div/div[2]/section[1]/div/div[2]/article[1]/span[2]/div/dl/dd[2]/input"));
                    var priceElement = driver.FindElement(By.XPath("//*[@id='root']/div/div/div[2]/section[1]/div/div[2]/article[1]/span[2]/div/dl/dd[3]/div/input"));
                    //*[@id="root"]/div/div/div[2]/section[1]/div/div[2]/article[1]/span[2]/div/dl/dt[3]/i
                    int price = int.Parse(priceElement.GetAttribute("value"), System.Globalization.NumberStyles.AllowThousands);
                    if (price != 0 && amount != 0)
                    {
                        double result = amount / (double)price;
                        item.SendKeys(result.ToString());
                    }
                    ((IJavaScriptExecutor)driver).ExecuteScript(exeScript3);
                }
                else
                {
                    throw new Exception("KRW 마켓에서만 이용가능합니다.");
                }

            }
            else if (aType == FunctionObject.AmountType.BTC)
            {
                var typeItem = driver.FindElement(By.XPath("//*[@id='root']/div/div/div[2]/section[1]/div/div[2]/article[1]/span[2]/div/dl/dt[3]/i"));
                if (typeItem.Text.Contains("BTC"))
                {
                    var item = driver.FindElement(By.XPath("//*[@id='root']/div/div/div[2]/section[1]/div/div[2]/article[1]/span[2]/div/dl/dd[2]/input"));
                    var priceElement = driver.FindElement(By.XPath("//*[@id='root']/div/div/div[2]/section[1]/div/div[2]/article[1]/span[2]/div/dl/dd[3]/div/input"));
                    //*[@id="root"]/div/div/div[2]/section[1]/div/div[2]/article[1]/span[2]/div/dl/dt[3]/i
                    double price = double.Parse(priceElement.GetAttribute("value"));
                    if (price != 0 && amount != 0)
                    {
                        double result = amount / (double)price;
                        item.SendKeys(result.ToString());
                    }
                    ((IJavaScriptExecutor)driver).ExecuteScript(exeScript3);
                }
                else
                {
                    throw new Exception("BTC 마켓에서만 이용가능합니다.");
                }
            }
            else
            {
                //string exeScript = "clickAtag('.layerB', '%'); clickJustAtag('" + FunctionObject.getAmountTypeText(aType) + "'); clickAtag('.ty02', '매도'); " + functionScript;
                string exeScript = "clickAtag('.layerB', '%'); clickJustAtag('" + FunctionObject.getAmountTypeText(aType) + "'); clickAtag('.ty02', '매도'); clickJustAtag('매도확인'); " + functionScript;
                ((IJavaScriptExecutor)driver).ExecuteScript(exeScript);
            }
        }

       
    }
}
