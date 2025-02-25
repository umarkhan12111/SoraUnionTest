using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Reqnroll;
using Reqnroll.CommonModels;
using System;
using System.Data;
using DataTable = Reqnroll.DataTable;

namespace Assignment.StepDefinitions
{
    [Binding]
    public sealed class StepDefs
    {
        ChromeDriver driver = new ChromeDriver();
        string username_Id = "txt-username";
        string password_Id = "txt-password";
        string login_Btn = "btn-login";
        string successfull_login_url = "https://katalon-demo-cura.herokuapp.com/#appointment";
      static  string app_url  = "https://katalon-demo-cura.herokuapp.com/";
        string menu_btn_id = "menu-toggle";
        string login_btn_Xpath = "//a[@href=\"profile.php#login\"]";
        string error_msg_Xpath = "//p[@class=\"lead text-danger\"]";
        string unsuccessfull_error_msg = "Login failed! Please ensure the username and password are valid.";
        string facility_drpdown_Id = "combo_facility";
        string[] healthcareprogram_Id = { "radio_program_medicare", "radio_program_medicaid", "radio_program_none" };
        string comments_textbox_Id = "txt_comment";
        string bookappointment_btn_Id = "btn-book-appointment";
        string date_textbox_Id = "txt_visit_date";
        string history_btn_Xpath = "//a[@href=\"history.php#history\"]";
        string historypage_facility_id = "facility";
        string historypage_comment_id = "comment";
        string historypage_program_id = "program";
        string historypage_dateXpath = "//div[@class=\"panel-heading\" and contains(., '26/02/2025')]";



        #region HelperFunctions
        public void Wait(int sec)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(sec);

         
        }

        public void login()
        {
            openApp();
            GivenUserOpenLoginPage();
            GivenUserEnterPasswordAsAndUsernameAs("ThisIsNotAPassword", "John Doe");
            driver.FindElement(By.Id(login_Btn)).Click();


        }

        public void SelectDropdown(string value)
        {
            SelectElement sl = new SelectElement( driver.FindElement(By.Id(facility_drpdown_Id)));
            sl.SelectByValue(value);
   
        }

        public void click( By by)
        {
            driver.FindElement(by).Click();          

        }



        public void HandlePopUp()
        {
            IAlert popup = driver.SwitchTo().Alert();
            popup.Dismiss();
        }

        #endregion

        [BeforeScenario]
        [Scope(Tag = "History")]
        public void CreateAppointment()
        {
            openApp();
            login();
            Wait(2);
            SelectDropdown("Tokyo CURA Healthcare Center");
            GivenUserEnterDate();
            ThenAppointmentCanBeMade();

        }

        [BeforeScenario]
        [Scope(Feature = "Login")]
        [Scope(Feature = "Appointment")]
        public void openApp()
        {
            driver.Navigate().GoToUrl(app_url);

        }



        [Given("user Open login page")]
        public void GivenUserOpenLoginPage()
        {
            driver.FindElement(By.Id(menu_btn_id)).Click();
            Wait(2);

            driver.FindElement(By.XPath(login_btn_Xpath)).Click();
        }


        [Given("user enter password as {string} and username as {string}")]
        public void GivenUserEnterPasswordAsAndUsernameAs(string password, string username)
        {
          
            driver.FindElement(By.Id(username_Id)).SendKeys(username);
            Wait(2);
            driver.FindElement(By.Id(password_Id)).SendKeys(password);
            Wait(2);

        }

        [When("the user click on login button")]
        public void WhenTheUserClickOnLoginButton()
        {
            driver.FindElement(By.Id(login_Btn)).Click();

        }

        [Then("login should be unsuccessfull")]
        public void ThenLoginShouldBeUnsuccessfull()
        {

            string error = driver.FindElement(By.XPath(error_msg_Xpath)).Text;
           
            Assert.That(error.Equals(unsuccessfull_error_msg), "Assert failed");

        }

        [Then("login should be successfull")]
        public void ThenLoginShouldBeSuccessfull()
        {
            string url = driver.Url.ToString();

            Assert.That(url.Equals(successfull_login_url), "Assert failed");

        }

        [Then("User see the Login page UI")]
        public void ThenUserSeeTheLoginPageUI()
        {
            Assert.Multiple(() =>
            {
                Assert.That(driver.FindElement(By.Id(username_Id)).Displayed, Is.True);
                Assert.That(driver.FindElement(By.Id(password_Id)).Displayed, Is.True);
                Assert.That(driver.FindElement(By.Id(login_Btn)).Displayed, Is.True);

            });

        }

        [Given("user is at {string} page")]
        public void GivenUserIsAttPage(string p0)
        {
           
            if (p0.Equals("History"))
            {
                Wait(2);
                click(By.Id(menu_btn_id));
                click(By.XPath(history_btn_Xpath));
                successfull_login_url = "https://katalon-demo-cura.herokuapp.com/history.php#history";
            }
            else
            {
                login();
            }
            string url = driver.Url.ToString();
            Assert.That(url.Equals(successfull_login_url), "Assert failed");

        }

        [When("user does not enter date")]
        public void WhenUserDoesNotEnterDate()
        {
            SelectDropdown("Tokyo CURA Healthcare Center");
            driver.FindElement(By.Id(bookappointment_btn_Id)).Click();

        }
        [Given("user enter date")]
        public void GivenUserEnterDate()
        {
            Wait(2);
            driver.FindElement(By.Id(date_textbox_Id)).SendKeys("12/12/2020");
            driver.FindElement(By.Id(date_textbox_Id)).SendKeys(Keys.Enter);

        }


        [Then("appointment cannot be made")]
        public void ThenAppointmentCannotBeMade()
        {

            Assert.That(driver.Url.ToString(), Is.EqualTo(successfull_login_url), "Assert failed");
        }
       
        [When("user selects different values from {string}")]
        public void WhenUserSelectsDifferentValuesFrom(string p0)
        {
            Wait(2);
            SelectDropdown(p0);
        }


        [Then("appointment can be made")]
        public void ThenAppointmentCanBeMade()
        {
            driver.FindElement(By.Id(bookappointment_btn_Id)).Click();
            Assert.That( driver.FindElement(By.XPath("//h2")).Text, Is.EqualTo("Appointment Confirmation"));

        }

        [Given("user verify the {string},  {string}, {string} and {string}")]
        public void GivenUserVerifyTheAnd(string p0, string p1, string p2, string comments)
        {
            string Xpath = String.Format("//div[@class=\"panel-heading\" and contains(., '{0}')]", p0);

            Assert.Multiple(() =>
            {
                
                Assert.That(driver.FindElement(By.XPath(Xpath)).Text, Is.EqualTo(p0));
                Assert.That(driver.FindElement(By.Id(historypage_comment_id)).Text, Is.EqualTo(comments));
                Assert.That(driver.FindElement(By.Id(historypage_facility_id)).Text, Is.EqualTo(p1));
                Assert.That(driver.FindElement(By.Id(historypage_program_id)).Text, Is.EqualTo(p2));


            });
        }


        [AfterScenario]
        public void teardown()
        {
            driver.Quit();
        }
    }
}
