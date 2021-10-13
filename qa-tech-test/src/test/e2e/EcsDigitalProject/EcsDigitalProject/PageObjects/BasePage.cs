using EcsDigitalProject.UtilityHelpers;

namespace EcsDigitalProject.PageObjects
{
    public class BasePage : DriverManager
    {
        /// <summary>
        /// Note that this class inherit from DriverManager class
        /// </summary>
        public T CurrentPage<T>() where T : BasePage, new()
        {
            var page = new T { Driver = Driver };
            return page;
        }
    }
}