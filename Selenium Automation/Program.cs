using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium_Automation
{
    [TestFixture]
    class Program
    {
        //Create a reference for Firefox browser
        public static IWebDriver driver = new ChromeDriver();
        static void Main(string[] args)
        {
        }

        [Test]
        public void Test1()
        {
            driver.Navigate().GoToUrl("https:///www.google.com");
            //Maximaze the window
            driver.Manage().Window.Maximize();

            //Find the searchbox
            IWebElement searchBox = driver.FindElement(By.Name("q"));
            //Enter the text into the searchbox
            searchBox.SendKeys("demoqa.com");
            searchBox.Submit();

            //Get the Xpath of the h3 element (demoqa.com text) and then click it
            string descriptionTextXPath = ("//*[@id=\"rso\"]/div[1]/div/div/div/div/div/div[1]/a/h3");
            IWebElement h3Element = driver.FindElement(By.XPath(descriptionTextXPath));
            h3Element.Click();

            //Here I need to do the same thing for the Intereactions h5 element

            string secondDescriptionTextPath = ("//*[@id=\"app\"]/div/div/div[2]/div/div[5]/div/div[3]/h5");
            IWebElement h5Element = driver.FindElement(By.XPath(secondDescriptionTextPath));
            h5Element.Click();

            //Scroll to the droppable element(I've tried many things here, I even tried selecting the element and then it told me that it was a li element)
            /*
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("javascript:window.scrollBy(250,1000)");
            */
            // JavaScript Executor to scroll to element

            //Click the droppable element
            //IWebElement droppable = driver.FindElement(By.Id("item-3"));
            //driver.FindElement(By.CssSelector("li:nth-child(3).#item-3")).Click();

            //I've tried the steps above, I've even tried different things with n-th child but they didn't work so I creaded a loop that 
            //loops through the links and then finds the Droppable link and clicks it
            IList<IWebElement> links = driver.FindElements(By.Id("item-3"));
            ((IJavaScriptExecutor)driver)
            .ExecuteScript(" window.scrollTo(250, 1100); ", links);
            links.First(element => element.Text == "Droppable").Click();

            foreach (var link in links)
            {
                if (link.Text == "Droppable")
                {
                    link.Click();
                    break;
                }
            }

            var drag = driver.FindElement(By.Id("draggable"));
            var drop = driver.FindElement(By.Id("droppable"));

            DragAndDrop(drag, drop);

            //This sections prints out the text in the Dropped container
            IWebElement printText = driver.FindElement(By.XPath("//*[@id=\"droppable\"]/p"));

            if (printText.Text == "Dropped!")
            {
                Console.WriteLine("Dropped!");
            }
            else
            {
                Console.WriteLine("Not dropped brah!");
            }

            //Screenshot
            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            ss.SaveAsFile("D://Screenshot.png", ScreenshotImageFormat.Png);

            //Clicking the widgets section first
            
            string widgetsTextPath = ("//*[@id=\"app\"]/div/div/div[2]/div[1]/div/div/div[4]/span/div");
            IWebElement widgets = driver.FindElement(By.XPath(widgetsTextPath));
            widgets.Click();

            //Scrool to the Tool Tips and click it
            /*
            ((IJavaScriptExecutor)driver)
            .ExecuteScript(" window.scrollTo(0, 1000); ", links);
            driver.FindElement(By.XPath("//*[@id=\"item - 6\"]")).Click();
            */
            IList<IWebElement> links2 = driver.FindElements(By.TagName("li"));
            //This f**** me the most, because the moment I change the value it doesn't work
            // on the Droppable and likewise
            ((IJavaScriptExecutor)driver)
            .ExecuteScript(" window.scrollTo(250, 1000); ", links2);
            links2.First(element2 => element2.Text == "Tool Tips").Click();

            foreach (var link2 in links2)
            {
                if (link2.Text == "Tool Tips")
                {
                    link2.Click();
                    break;
                }
            }


            //Moving the cursor to the tootTip Button
            IWebElement inputButton = driver.FindElement(By.Id("toolTipButton"));
            Actions cursorAction = new Actions(driver);
            cursorAction.MoveToElement(inputButton);

            Console.WriteLine("You hovered over the Button");


        }
        //drag and drop method
        public static void DragAndDrop(IWebElement element1, IWebElement element2)
        {
            Actions action = new Actions(driver);
            action.DragAndDrop(element1, element2).Build().Perform();
        }

        [Test]
        public void Test2()
        {
            //Going to google and searching "cheese"
            driver.Navigate().GoToUrl("https:///www.google.com");
            IWebElement googleSearch = driver.FindElement(By.Name("q"));
            googleSearch.SendKeys("cheese");
            googleSearch.Submit();

            string expectedNumber;
            int actualNumber = 777;

            //I've tried converting the expectedNumber to int
            //I got confused so Ill move onto the 3.Test
            IWebElement numberResult = driver.FindElement(By.Id("result-stats"));
            expectedNumber = numberResult.ToString();

            //Comparing the results
            Assert.AreEqual(actualNumber, expectedNumber, "There is too much cheese on the internet");
        }

        [Test]
        public void Test3()
        {
            //Navigating to the page
            driver.Navigate().GoToUrl("https:///orangehrm-demo-7x.orangehrmlive.com//");
            //Maximize the window
            driver.Manage().Window.Maximize();
            //Find the login Button
            IWebElement loginButton = driver.FindElement(By.Name("Submit"));
            loginButton.Click();

            //Find the recruitment button
            IWebElement recruitmentButton = driver.FindElement(By.XPath("//*[@id=\"menu_recruitment_viewRecruitmentModule\"]/a"));
            recruitmentButton.Click();

            //Looping the links and finding the candidates link
            IList<IWebElement> links3 = driver.FindElements(By.TagName("a"));
            links3.First(element => element.Text == "Candidates").Click();

            foreach (var link in links3)
            {
                if (link.Text == "Candidates")
                {
                    link.Click();
                    break;
                }
            }

            //I've tried something here but I haven't succeeded
            /*
            IList<IWebElement> candidates;
            candidates.Count()
            */

            //Clicking the green button
            IWebElement greenButton = driver.FindElement(By.XPath("//*[@id=\"addItemBtn\"]/i"));
            greenButton.Click();

            //Filling out the form
            IWebElement firstName = driver.FindElement(By.Id("addCandidate_firstName"));
            firstName.SendKeys("QA");

            IWebElement lastName = driver.FindElement(By.Id("addCandidate_lastName"));
            lastName.SendKeys("Automation");

            IWebElement email = driver.FindElement(By.Id("addCandidate_email"));
            email.SendKeys("qaautomation@gmail.com");

            //Selecting the vacancy and then clicking the link inside it
            
            IWebElement vacancy = driver.FindElement(By.Id("textarea_addCandidate_vacancy"));
            var selectVacancy = new SelectElement(vacancy);
            selectVacancy.SelectByText("Credit Analyst");

            //Selecting the date
            IWebElement calendar = driver.FindElement(By.Id("applied_date"));
            calendar.Click();

            IWebElement todayButton = driver.FindElement(By.XPath("//*[@id=\"applied_date_root\"]/div/div/div/div/div[3]/button[1]"));
            todayButton.Click();

            //Uploading the resume
            IWebElement uploadResume = driver.FindElement(By.Id("addCandidate_resume"));
            uploadResume.Click();

            // enter the file path onto the file-selection input field
            uploadResume.SendKeys("D:\\resume.doc");

            //Saving the changes
            IWebElement saveButton = driver.FindElement(By.Id("saveCandidateButton"));
            saveButton.Click();

            //Asserting the candidates
            /*Unfortunately it is late and my brain stopped working and I can't think
             * stright, in the assert method id probably create some kind of loop to check 
             * if the number of candidates has been increased by 1
             */

            //I got confused with the checkbox since it shows me ::after and I couldnt copy an element

            //Logging out
            IWebElement userDropdown = driver.FindElement(By.Id("user-dropdown"));
            userDropdown.Click();

            IWebElement logOut = driver.FindElement(By.Id("logoutLink"));
            logOut.Click();
        }


        [TearDown]
        public void CloseTest()
        {
            //Close the browser
            //driver.Quit();
        }
    }
}
