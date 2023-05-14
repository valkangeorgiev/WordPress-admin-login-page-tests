using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;



namespace WordPress_admin_login_page_tests.Tests

{
    public class LogInPageTestsWithMozilla
    {
        private WebDriver driver;
        private LogInPage page;
        private const string baseUrl = "http://localhost:8000/wp-login.php";
        private const string correctPassword = "SuS4IOWVG374L#*(38";
        private const string correctUsername = "valkan.georgiev";
        private const string correctEmailAddress = "valkan.georgiev1@gmail.com";

        [SetUp]
        public void Setup()
        {
 
            this.driver = new FirefoxDriver(); // We can duplicate and run the same test cases that we have
                                              // in different browser.

            driver.Navigate().GoToUrl(baseUrl);
          

        }

       // [TearDown]
       // public void CloseBrowser()
       // {
       //     this.driver.Quit();
       //     
       //  }

        [Test, Order(1)]
        public void TryToLoginWithValidUsernameAndValidPassword()
        {
          Assert.That(driver.Title.Contains("Log In ‹ testwebsiteforTOC — WordPress"));
       
        }

        




    }
}

