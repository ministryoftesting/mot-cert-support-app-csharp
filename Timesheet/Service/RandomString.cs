namespace Timesheet.Service
{
    public class RandomString
    {
        private int length;
        private Random random;

        public RandomString(int length, Random random)
        {
            this.length = length;
            this.random = random;
        }

        public string NextString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }

}