namespace YanZhiwei.DotNet.Mvc.Learn.Models
{
    public enum Sex
    {
        Male,
        Female
    }

    public class Worker
    {
        public int ID { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public Sex Gender { get; set; }

        public double Rating { get; set; }
    }
}